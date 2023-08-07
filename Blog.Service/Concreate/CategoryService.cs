using AutoMapper;
using Blog.Data.UnitOWorks;
using Blog.Entity.DTOs.Categories;
using Blog.Entity.Entities;
using Blog.Service.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.Concreate
{
    public class CategoryService:ICategoryService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CategoryService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
  
        }

        public async Task<List<CategoryDto>> GetAllCategoriesWithNonDeleted()
        {
            var categories= await unitOfWork.GetRepository<Category>().GetAllAsync(x => !x.IsDeleted); //category dto döndüğümüz için mapleme işlemi yapmamız gerekir.
            var map=mapper.Map<List<CategoryDto>>(categories);
            return map;

            

        }
    }
}
