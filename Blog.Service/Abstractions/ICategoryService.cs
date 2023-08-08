using Blog.Entity.DTOs.Categories;
using Blog.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.Abstractions
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetAllCategoriesWithNonDeleted();
        Task<Category> GetCategoryByGuid(Guid id);
        Task CreateCategoryAsync(CategoryAddDto categoryAddDto);
        Task<string> UpdateCategoryAsync(CategoryUpdateDto categoryUpdateDto);
        Task<string> SafeDeleteCategoryAsync(Guid categoryId);
    }
}
