using Blog.Entity.DTOs.Users;
using Blog.Entity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    
    public class AuthController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public AuthController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager) //user için ayrı bir servis yapısı kurmamıza gerek kalmıyacak.Identity bize bunu sağlıyo o yüzden tekrar servis oluştrup controllerde yazmamıza gerek yok.
        {
            this.userManager=userManager;
            this.signInManager=signInManager;
            
        }
       
        [HttpGet] //ilk başta gelen login sayfasının gelmesini sağladı işlem yaypıp sıgnın yaptığımızda post işlemi yapmış olucak.
        public IActionResult Login() //index değil login işlemi sayfasına gidecek.
        {
            return View();
        }
        [AllowAnonymous] //controller bazında authorize yapmıştık home controllerde burda olmaması için ekleriz login sayfasına ulaşabilmek için sonsuz döngüye girmemesi için 
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            //eğer modelde bir sorun yoksa
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(userLoginDto.Email); //dto email vermii oluruz
                if (user != null)
                {
                    var result = await signInManager.PasswordSignInAsync(user, userLoginDto.Password, userLoginDto.RememberMe, false);
                    if (result.Succeeded)
                    {
                        //admin sayfamın ındexine gidebilirsin.

                        return RedirectToAction("Index", "Home", new { Area = "Admin" });
                    }
                    else
                    {
                        ModelState.AddModelError("", "E-posta adresiniz veya şifreniz yanlıştır.");
                        return View();
                    }
                }
                else
                {

                    ModelState.AddModelError("", "E-posta adresiniz veya şifreniz yanlıştır.");
                    return View();

                }

            }
            else
            {
                return View();
            }

        }
    }
}
