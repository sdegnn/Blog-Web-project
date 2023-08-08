using Blog.Entity.Entities;
using Blog.Data.UnitOWorks;
using Blog.Service.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Entity.DTOs.Articles;
using AutoMapper;
using Blog.Entity.DTOs.Categories;
using Microsoft.AspNetCore.Http;
using Blog.Service.Extensions;
using System.Security.Claims;
using Blog.Service.Helpers.Images;
using Blog.Entity.Enums;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Blog.Service.Concreate
{
    public class ArticleService : IArticleService //bir servis oluşturup test etmek için oluşturduk
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ClaimsPrincipal _user;
        private readonly IImageHelper ımageHelper;
        public ArticleService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, IImageHelper ımageHelper) //ınterface ekleyrek kod bloğunu yeniden yazmamız mümnkün olmadığı ya da kod fazlalığı yaratacağı için bu şekilde interface kullanılabilir hale getirdik.
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this._httpContextAccessor = httpContextAccessor;
            _user = httpContextAccessor.HttpContext.User;
            this.ımageHelper = ımageHelper;
        }

        public async Task CreateArticleAsync(ArticleAddDto articleAddDto)
        {
            //var userId = Guid.Parse("A0672378-C7F9-4C88-96A2-B33796483CCB"); //Hazır olan userdan aldık
            //var article = new Article() //yeni article oluşturmuş olduk
            //{
            //    //Title = articleAddDto.Title,
            //    //Content = articleAddDto.Content,
            //    //CategoryId=articleAddDto.CategoryId,
            //    //UserId=userId
            //};

            /* var userId = Guid.Parse("A0672378-C7F9-4C88-96A2-B33796483CCB"); *///Hazır olan userdan aldık
            var userId = _user.GetLoggedInUserId();
            var userEmail = _user.GetLoggedInEmail();
            var imageUpload = await ımageHelper.Upload(articleAddDto.Title, articleAddDto.Photo, ImageType.Post);
            Image image = new(imageUpload.FullName, articleAddDto.Photo.ContentType, userEmail);
            await unitOfWork.GetRepository<Image>().AddAsync(image);

            var article = new Article(articleAddDto.Title, articleAddDto.Content, userEmail, userId, articleAddDto.CategoryId, image.Id);

            article.Image = image;
            await unitOfWork.GetRepository<Article>().AddAsync(article);
            await unitOfWork.SaveAsync();
        }

        public async Task<List<ArticleDto>> GetAllArticleWithNonDeletedAsync()
        {

            var articles = await unitOfWork.GetRepository<Article>().GetAllAsync(x => !x.IsDeleted, x => x.Category);
            var map = mapper.Map<List<ArticleDto>>(articles);

            return map;

        }
        public async Task<ArticleDto> GetArticleWithCategoryNonDeletedAsync(Guid articleId) //bir tane ıdye göre çekicek yapıda düzenleriz.
        {

            var article = await unitOfWork.GetRepository<Article>().GetAsync(x => !x.IsDeleted && x.Id == articleId, x => x.Category, i => i.Image);
            var map = mapper.Map<ArticleDto>(article); //burada artık bir article dönmüş oldu.

            return map;

        }
        public async Task<string> UpdateArticleAsync(ArticleUpdateDto articleUpdateDto)
        {
            var userEmail = _user.GetLoggedInEmail();
            var article = await unitOfWork.GetRepository<Article>().GetAsync(x => !x.IsDeleted && x.Id == articleUpdateDto.Id, x => x.Category, i => i.Image);


            if (articleUpdateDto.Photo != null)
            {
                if (article.ImageId != null)
                {
                    ımageHelper.Delete(article.Image.FileName);
                }

                
                var imageUpload = await ımageHelper.Upload(articleUpdateDto.Title, articleUpdateDto.Photo, ImageType.Post);
                Image image = new(imageUpload.FullName, articleUpdateDto.Photo.ContentType, userEmail);
                await unitOfWork.GetRepository<Image>().AddAsync(image);


            article.Title = articleUpdateDto.Title;
            article.Content = articleUpdateDto.Content;
            article.CategoryId = articleUpdateDto.CategoryId;
            article.ModifiedDate = DateTime.Now;
            article.ModifiedBy = userEmail;
            article.ImageId = image.Id;

            }
          


            await unitOfWork.GetRepository<Article>().UpdateAsync(article);
            await unitOfWork.SaveAsync();

            return article.Title;

        }

        public async Task<string> SafeDeleteArticleAsync(Guid aritcleId)
        {
            var userEmail = _user.GetLoggedInEmail();
            var article = await unitOfWork.GetRepository<Article>().GetByGuidAsync(aritcleId); //getasync yapısına gerek yok sadece guid çekerek safedelete ,işlemiin gerçekleşitircez bir sorgu kısmı yok
            article.IsDeleted = true; //silinmiş olması 
            article.DeletedDate = DateTime.Now;
            article.DeletedBy = userEmail;
            await unitOfWork.GetRepository<Article>().UpdateAsync(article); //article çağırıp updateasync ile update yapmış olduk.
            await unitOfWork.SaveAsync();
            return article.Title;

        }




    }
}
