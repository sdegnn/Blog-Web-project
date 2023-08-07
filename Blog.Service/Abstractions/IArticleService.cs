﻿using Blog.Entity.DTOs.Articles;
using Blog.Entity.Entities;

namespace Blog.Service.Abstractions
{
    public interface IArticleService
    {
        Task<List<ArticleDto>> GetAllArticleWithNonDeletedAsync();
        Task CreateArticleAsync(ArticleAddDto articleAddDto);
        Task<ArticleDto> GetArticleWithCategoryNonDeletedAsync(Guid articleId);
        Task<string> UpdateArticleAsync(ArticleUpdateDto articleUpdateDto);
        Task<string> SafeDeleteArticleAsync(Guid articleId);
    }
}
