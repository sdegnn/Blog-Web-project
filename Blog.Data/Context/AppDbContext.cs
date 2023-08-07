using Blog.Entity.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Context
{
    public class AppDbContext:IdentityDbContext <AppUser,AppRole,Guid,AppUserClaim,AppUserRole, AppUserLogin,AppRoleClaim,AppUserToken>
    {
        protected AppDbContext()
        {
            
        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
        }

        public DbSet<Article> Articles { get; set; } //veritabında kullanacağımız verileri dbset<> ile set ettik.birden fazla olacağı için çoğul bi şekilde set ediyoruz

        public DbSet<Category> Categories { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) //model oluşturulmadan önce konfigürasyon sağlar.
        {
            base.OnModelCreating(builder);//migration oluşturduğumuzda ıdentity dolayı bunu oluşturmazsak hata alabiliriz
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); //assembly aldık execut ettik,bu assemblydeki tüm ıentitytypeconf dan kalıtım alan tüm sınıfların, tüm mapping sınıfların tanımlanmış olmasını bekleriz

        }
    }

}
