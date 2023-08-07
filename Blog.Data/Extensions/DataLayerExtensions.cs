using Blog.Data.Context;
using Blog.Data.Repositories.Abstractions;
using Blog.Data.Repositories.Concretes;
using Blog.Data.UnitOWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Extensions
{
    public static class DataLayerExtensions //dependency injection yapısını kurmuş olduk her ırepository istek attığımda repository nesnesini getir dedik.
    {
        public static IServiceCollection LoadDataLayerExtension(this IServiceCollection services,IConfiguration config)
        {
            //Irepositroy çağırdığımızda repository nesnesini çağırması gerektiğiyle alakalı bri scoped yazıcaz.Irepository istek atınca repository döndürmeli
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>)); //generic yapı olduğu için type of yapısını kullandık
            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(config.GetConnectionString("DefaultConnection")));
            services.AddScoped<IUnitOfWork, UnitOfWork>(); //ınuitofwork istendiğinde unitofwork örneği oluşturmasını istiycez

            return services;
        }
    }
}
