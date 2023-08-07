using Blog.Service.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")] //areayı adminle ilişkilendirdik
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IArticleService articleService;

        public HomeController(IArticleService articleService)
        {
            this.articleService=articleService;
            
        }
        public async Task<IActionResult> Index()
        {
            var articles = await articleService.GetAllArticleWithNonDeletedAsync();

            return View(articles);
        }
    }
}
