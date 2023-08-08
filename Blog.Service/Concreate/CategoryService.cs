﻿using AutoMapper;
using Blog.Data.UnitOWorks;
using Blog.Entity.DTOs.Articles;
using Blog.Entity.DTOs.Categories;
using Blog.Entity.Entities;
using Blog.Service.Abstractions;
using Blog.Service.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.Concreate
{
    public class CategoryService:ICategoryService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ClaimsPrincipal _user;
        public CategoryService(IUnitOfWork unitOfWork,IMapper mapper,IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this._httpContextAccessor = httpContextAccessor;
            _user = httpContextAccessor.HttpContext.User;

        }

        public async Task<List<CategoryDto>> GetAllCategoriesWithNonDeleted()
        {
            var userId = _user.GetLoggedInUserId;
            var userEmail = _user.GetLoggedInEmail;

            var categories= await unitOfWork.GetRepository<Category>().GetAllAsync(x => !x.IsDeleted); //category dto döndüğümüz için mapleme işlemi yapmamız gerekir.
            var map=mapper.Map<List<CategoryDto>>(categories);
            return map;

            

        }
        public async Task CreateCategoryAsync(CategoryAddDto categoryAddDto)
        {
         
            var userEmail = _user.GetLoggedInEmail();

            Category category =new(categoryAddDto.Name,userEmail); 
            await unitOfWork.GetRepository<Category>().AddAsync(category);
            await unitOfWork.SaveAsync();

           

        }
        public async Task<Category> GetCategoryByGuid(Guid id)
        {
            var category= await unitOfWork.GetRepository<Category>().GetByGuidAsync(id);
            return category;

        }
        public async Task<string> UpdateCategoryAsync(CategoryUpdateDto categoryUpdateDto)
        {
            var userEmail = _user.GetLoggedInEmail();
            var category = await unitOfWork.GetRepository<Category>().GetAsync(x => !x.IsDeleted && x.Id == categoryUpdateDto.Id);
            category.Name = categoryUpdateDto.Name;
            category.ModifiedBy = userEmail;
            category.ModifiedDate=DateTime.Now;
            //category.Id.


           
            await unitOfWork.GetRepository<Category>().UpdateAsync(category);
            await unitOfWork.SaveAsync();
            return category.Name;



        }
        public async Task<string> SafeDeleteCategoryAsync(Guid categoryId)
        {
            var userEmail = _user.GetLoggedInEmail();
            var category = await unitOfWork.GetRepository<Category>().GetByGuidAsync(categoryId);
            category.IsDeleted = true; //silinmiş olması 
            category.DeletedDate = DateTime.Now;
            category.DeletedBy = userEmail;
            await unitOfWork.GetRepository<Category>().UpdateAsync(category); 
            await unitOfWork.SaveAsync();
            return category.Name;

        }
    }
}
