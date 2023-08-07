using Blog.Data.Context;
using Blog.Data.Repositories.Abstractions;
using Blog.Data.Repositories.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.UnitOWorks
{
    public class UnitOfWork : IUnitOfWork //dependency ınjectionda kullanmamız gerekir
    {
        private readonly AppDbContext dbContext;

        public UnitOfWork(AppDbContext dbContext)
        {
            this.dbContext=dbContext;
        }
        public async ValueTask DisposeAsync()
        {
            await dbContext.DisposeAsync();
        }

        public int Save()
        {
           return dbContext.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await dbContext.SaveChangesAsync();
        }

        IRepository<T> IUnitOfWork.GetRepository<T>() //ıunitofwork çağırıp ordan get repository metotunu çağırıp repository sınıflarındaki metotlarımıza ulaşmaızı sağlıycak.
        {
            return new Repository<T>(dbContext);
        }
    }
}
