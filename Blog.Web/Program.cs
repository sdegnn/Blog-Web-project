using Blog.Data.Context;
using Blog.Data.Extensions;
using Blog.Entity.Entities;
using Blog.Service;
using Blog.Service.Concreate;
using Blog.Service.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NToastNotify;

var builder = WebApplication.CreateBuilder(args);

builder.Services.LoadDataLayerExtension(builder.Configuration);
builder.Services.LoadSericeLayerExtensions();
builder.Services.AddSession();
// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddNToastNotifyToastr(new ToastrOptions()
    {
        PositionClass=ToastPositions.TopRight,
        TimeOut=9000,
        
    })
    .AddRazorRuntimeCompilation();
builder.Services.AddIdentity<AppUser, AppRole>(opt =>
{
    //�ifre i�in gereklilikleri olu�tururuz.
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireUppercase = false;

}
) //�dentity yap�lanmas�n� ekledik
   .AddRoleManager<RoleManager<AppRole>>()//role tabanl� authentication olu�turduu�umuz i�in
   .AddEntityFrameworkStores<AppDbContext>()
   .AddDefaultTokenProviders();
builder.Services.ConfigureApplicationCookie(config =>
{
    config.LoginPath = new PathString("/Admin/Auth/Login");//admin paneline girmek isterse url bilmesi �nemsiz olmaks�z�n.admin sayfas�na istek at�ld���nda bize login i�lemini g�ndermil olucak.auth authenticon anlam�na login index sayfam�z olucak
    config.LogoutPath = new PathString("/Admin/Auth/Logout"); //��k�� yapmas� i�in
    config.Cookie = new CookieBuilder
    {
        Name = "Blog",
        HttpOnly = true, //sadece http de tutucaz
        SameSite = SameSiteMode.Strict,
        SecurePolicy = CookieSecurePolicy.SameAsRequest //http https kilit i�areti olan g�venli bir site olan site taraf�ndan istek alabilrisniz.
        //cookiesecrucepolicy.always yaparsak canl�ya al�rken kilit i�areti olan sadece g�venli olan� se�memiz daha iyi olur. 


    };
    config.SlidingExpiration = true;
    config.ExpireTimeSpan = TimeSpan.FromDays(7); //bu cookienin sistemde ne kadar tutulaca��.siteye giri� yapt�ktan sonra 7 g�n login a��k kalacak
    config.AccessDeniedPath = new PathString("/Admin/Auth/AccessDenied"); //yetksiz giri� oldu�unda bunu sizin yetkiniz yok login olun denilen sayfaya y�nlendirecek.


});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseNToastNotify();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapAreaControllerRoute(
        name: "Admin",
        areaName:"Admin", 
        pattern: "Admin/{controller=Home}/{action=Index}/{id?}");

    
    endpoints.MapDefaultControllerRoute(); //admin panelini �a��rmazsam default de�eri �a��r�r.

});
app.Run();
