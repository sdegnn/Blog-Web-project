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
    //þifre için gereklilikleri oluþtururuz.
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireUppercase = false;

}
) //ýdentity yapýlanmasýný ekledik
   .AddRoleManager<RoleManager<AppRole>>()//role tabanlý authentication oluþturduuðumuz için
   .AddEntityFrameworkStores<AppDbContext>()
   .AddDefaultTokenProviders();
builder.Services.ConfigureApplicationCookie(config =>
{
    config.LoginPath = new PathString("/Admin/Auth/Login");//admin paneline girmek isterse url bilmesi önemsiz olmaksýzýn.admin sayfasýna istek atýldýðýnda bize login iþlemini göndermil olucak.auth authenticon anlamýna login index sayfamýz olucak
    config.LogoutPath = new PathString("/Admin/Auth/Logout"); //çýkýþ yapmasý için
    config.Cookie = new CookieBuilder
    {
        Name = "Blog",
        HttpOnly = true, //sadece http de tutucaz
        SameSite = SameSiteMode.Strict,
        SecurePolicy = CookieSecurePolicy.SameAsRequest //http https kilit iþareti olan güvenli bir site olan site tarafýndan istek alabilrisniz.
        //cookiesecrucepolicy.always yaparsak canlýya alýrken kilit iþareti olan sadece güvenli olaný seçmemiz daha iyi olur. 


    };
    config.SlidingExpiration = true;
    config.ExpireTimeSpan = TimeSpan.FromDays(7); //bu cookienin sistemde ne kadar tutulacaðý.siteye giriþ yaptýktan sonra 7 gün login açýk kalacak
    config.AccessDeniedPath = new PathString("/Admin/Auth/AccessDenied"); //yetksiz giriþ olduðunda bunu sizin yetkiniz yok login olun denilen sayfaya yönlendirecek.


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

    
    endpoints.MapDefaultControllerRoute(); //admin panelini çaðýrmazsam default deðeri çaðýrýr.

});
app.Run();
