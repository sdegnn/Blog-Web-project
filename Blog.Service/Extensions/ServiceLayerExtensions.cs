using Blog.Entity.Entities;
using Blog.Service.Abstractions;
using Blog.Service.Concreate;
using Blog.Service.FluentValidation;
using Blog.Service.Helpers.Images;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.Extensions
{
    public static class ServiceLayerExtensions //static metot barındırığı için static olmalı
    {
        public static IServiceCollection LoadSericeLayerExtensions(this IServiceCollection services) //config kullanmadığımız için kaldırdık 
        {
            var assembly=Assembly.GetExecutingAssembly(); //assembly dediğimiz kısım bu sınıfın çağırıldığı automapperın add edildiği katmanın ismi tüm katmandan inherit edenler bulup dependency injection mantıpını uygular.


            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<ICategoryService, CategoryService>(); //ıcategoryservice nesnesini çağırdığımda category service dönmesini isteriz.
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); //kayıtlı olan kullanıcıyı bulmamızı sağlar.
            services.AddScoped<IImageHelper, ImageHelper>();
            services.AddAutoMapper(assembly);
            services.AddControllersWithViews().AddFluentValidation(opt =>
            {
                opt.RegisterValidatorsFromAssemblyContaining<ArticleValidator>();
                opt.DisableDataAnnotationsValidation = true;
                opt.ValidatorOptions.LanguageManager.Culture = new CultureInfo("tr");
            }

            );
            



            return services;

        }
    }
}
