﻿using AutoMapper;
using Blog.Entity.DTOs.Articles;
using Blog.Entity.DTOs.Categories;
using Blog.Entity.Entities;
using Blog.Service.Abstractions;
using Blog.Service.Concreate;
using Blog.Service.Extensions;
using Blog.Web.ResultMessages;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IValidator<Category> _validator;
        private readonly IMapper _mapper;
        private readonly IToastNotification _toast;

        public CategoryController(ICategoryService categoryService,IValidator<Category> validator,IMapper mapper,IToastNotification toast)
        {
            this._categoryService=categoryService;
            this._validator=validator;
            this._mapper=mapper; 
            this._toast=toast;


        }
        public async Task <IActionResult> Index()
        {
            var categories= await _categoryService.GetAllCategoriesWithNonDeleted();
            return View(categories);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async  Task<IActionResult> Add(CategoryAddDto categoryAddDto)
        {

            var map = _mapper.Map<Category>(categoryAddDto);
            var result = await _validator.ValidateAsync(map);
            if (result.IsValid)
            {
                await _categoryService.CreateCategoryAsync(categoryAddDto);
                _toast.AddSuccessToastMessage(Messages.Category.Add(categoryAddDto.Name), new ToastrOptions { Title = " İşlem Başarılı!" });
                return RedirectToAction("Index", "Category", new { Area = "Admin" });
  
            }
          
                result.AddToModelState(this.ModelState);
                return View();

           
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid categoryId)
        {
            var category= await _categoryService.GetCategoryByGuid(categoryId);
            var map= _mapper.Map<Category,CategoryUpdateDto>(category);
            return View(map);


        }
        [HttpPost]
        public async Task<IActionResult> Update(CategoryUpdateDto categoryUpdateDto)
        {
            var map = _mapper.Map<Category>(categoryUpdateDto);
            var result = await _validator.ValidateAsync(map);
            if (result.IsValid)
            {
                var name=await _categoryService.UpdateCategoryAsync(categoryUpdateDto);
                _toast.AddSuccessToastMessage(Messages.Category.Update(name), new ToastrOptions { Title = " İşlem Başarılı!" });
                return RedirectToAction("Index", "Category", new { Area = "Admin" });

            }
            result.AddToModelState(this.ModelState) ;

            return View();


        }
        public async Task<IActionResult> Delete(Guid categoryId)
        {
            var name = await _categoryService.SafeDeleteCategoryAsync(categoryId);
            _toast.AddSuccessToastMessage(Messages.Category.Delete(name), new ToastrOptions { Title = " İşlem Başarılı!" });
            return RedirectToAction("Index", "Category", new { Area = "Admin" });

        }

    }
}
