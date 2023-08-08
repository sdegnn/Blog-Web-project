using AutoMapper;
using Blog.Entity.DTOs.Articles;
using Blog.Entity.Entities;
using Blog.Service.Abstractions;
using Blog.Service.Extensions;
using Blog.Service.Helpers.Images;
using Blog.Web.ResultMessages;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArticleController : Controller
    {
        private readonly IArticleService articleService;
        private readonly ICategoryService categoryService;
        private readonly IMapper mapper;
        private readonly IValidator<Article> validator;
        private readonly IToastNotification toast;
        

        public ArticleController(IArticleService articleService,ICategoryService categoryService,IMapper mapper,IValidator<Article> validator,IToastNotification toast)
        {
            this.articleService = articleService;
            this.categoryService=categoryService;
            this.mapper = mapper;
            this.validator = validator;
            this.toast=toast;
            


        }
        public async Task<IActionResult> Index()
        {
            var articles=await articleService.GetAllArticleWithNonDeletedAsync();
            return View(articles);
        }
        [HttpGet]
        public async Task<IActionResult> Add() //get işlemi olucak.Sayfamız ilk yüklendiğinde formumuzu görücez veri görebilceğimiz kısmı kaydet  tuşuna basabilriz daha sonrasında.
        {
            var categories = await categoryService.GetAllCategoriesWithNonDeleted();
            return View(new ArticleAddDto { Categories=categories});
        }
        [HttpPost]
        public async Task<IActionResult> Add(ArticleAddDto articleAddDto) //POST işlemi olucak kaydete bastıktan sonraki işlemi gerçekleştirir.
        {
            var map=mapper.Map<Article>(articleAddDto);
            var result = await validator.ValidateAsync(map);
            if (result.IsValid) //başarılıysa
            {
                await articleService.CreateArticleAsync(articleAddDto);
                toast.AddSuccessToastMessage(Messages.Article.Add(articleAddDto.Title), new ToastrOptions { Title=" İşlem Başarılı!"});
                return RedirectToAction("Index", "Article", new { Area = "Admin"});
            }
            else
            {
                result.AddToModelState(this.ModelState);//extensionda olan addtomodelstate

            }
            //article servise gidip ekleme işlemi yapıcak olan kısım
            ////await articleService.CreateArticleAsync(articleAddDto);
            RedirectToAction("Index", "Article", new { Area = "Admin" }); //redirect yönlendirme yapar bu işlemi ındex article kısmında new leyerek oluşturur.


            var categories = await categoryService.GetAllCategoriesWithNonDeleted();
            return View(new ArticleAddDto { Categories = categories });
        }
        [HttpGet]
        public async Task<IActionResult> Update(Guid articleId)
        {
            //kategorileri ıd ye göre çekecek olan fonksiyonu yazıcaz.Bunun için o ıd çekicek olan servis yapısını kullanıcaz daha önce yazdığımız metot
            //hepsini çekiyordu,sadece birini alıcak yapıyı tekrar servis yapısında yazarız.(articleservice)
            var article = await articleService.GetArticleWithCategoryNonDeletedAsync(articleId);
            var categories= await categoryService.GetAllCategoriesWithNonDeleted();
            

            var articleUpdateDto=mapper.Map<ArticleUpdateDto>(article); //article,articleupdatedtoya maplenir.
            articleUpdateDto.Categories = categories;
            return View(articleUpdateDto); //hem kategorilerin hem de makalelerin maplenmiş halini görürüz.
        }
        [HttpPost]
        public async Task<IActionResult> Update(ArticleUpdateDto articleUpdateDto)
        {

            var map = mapper.Map<Article>(articleUpdateDto);
            var result = await validator.ValidateAsync(map);

            if (result.IsValid)
            {
                var title = await articleService.UpdateArticleAsync(articleUpdateDto);
                toast.AddSuccessToastMessage(Messages.Article.Update(title), new ToastrOptions() { Title = "İşlem Başarılı" });
                return RedirectToAction("Index", "Article", new { Area = "Admin" });

            }
            else
            {
                result.AddToModelState(this.ModelState);
            }


            var categories = await categoryService.GetAllCategoriesWithNonDeleted();
            articleUpdateDto.Categories = categories;
            return View(articleUpdateDto);
        }
        public async Task<IActionResult> Delete(Guid articleId)
        {
           var title= await articleService.SafeDeleteArticleAsync(articleId);
            toast.AddSuccessToastMessage(Messages.Article.Delete(title), new ToastrOptions() { Title = "İşlem Başarılı" });
            //return view değil sayfada yenileme işlemi olması gerkiyor, makale gitmiş olması için
            return RedirectToAction("Index", "Article", new { Area = "Admin"});

        }
    }
}
