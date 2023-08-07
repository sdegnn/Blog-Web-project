using Blog.Core.Entities;
using Blog.Data.Context;
using Blog.Data.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Repositories.Concretes
{
    public class Repository<T>:IRepository<T> where T : class,IEntityBase,new() //entity classları gelicek kullanılacak o yüzden generic tipinde repository oluşturduk daha sonra entity interfaceden türeme hiçbir sınıf kullanamasın diye etiketleme kullandık ve son olarak newlenebilir bir class olduğunu gösterdik
    {
        private readonly AppDbContext dbContext; //sadece okuma amaçlı kullanıldığını gösteren readonly

        public Repository(AppDbContext dbContext) 
        {
            this.dbContext = dbContext;
        }
        private DbSet<T> Table { get => dbContext.Set<T>(); } //t nesnesi her bir entity kapsar.t nesnesini dbcontext set ettikbunu yapmasaydık her seferinde metotlarda set etmemiz gerkeirdi.
        //article gibi bir t nesnesinin birden fazla değeri olduğu için list tipinde dönmesini isteriz.çünkü async olarak tüm article istiyoruz.
        public async Task<List<T>> GetAllAsync(Expression<Func<T,bool>>predicate=null,params Expression<Func<T, object>>[] includeProperties) 
            //expression<func<>> tipinde veri belirleyip değerleriğini t alıp bool döner demek veya object döner demek.
            //bunu yapmamızın nedeni örneğin bir articleın içindeki file name almak istediğimizde bu filenamein nerden geliceğini bilmem gerekiyor bunun içinde
            //ımage i article a include etmem gerekir.
        {
            IQueryable<T> query = Table; //ıqueryable türünde dbcontext nesnesinin set edilmiş halini al
            if(predicate!= null) query = query.Where(predicate);

            if (includeProperties.Any())//içinde herhangi bir tane varsa
            {
                foreach(var item in includeProperties) //foreachle bunları dön
                {
                    query=query.Include(item); //include itemi dahil etmiş olucaz
                }
            }
            return await query.ToListAsync();
                
            

        }
        public async Task AddAsync(T entity) //task==void async olarak programlama adı //sırayla değil atılan istekleri aynı anda çalıştırır.
        {
             await Table.AddAsync(entity); //
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T,object>>[] includeProperties)
        {
            IQueryable<T> query = Table; //spesifik bir adet nesne dönmesini istediğim için null değerini kaldırdık.yukarda
            query=query.Where(predicate);

            if (includeProperties.Any())//içinde herhangi bir tane varsa
            {
                foreach (var item in includeProperties) //foreachle bunları dön
                {
                    query = query.Include(item); //include itemi dahil etmiş olucaz
                }
            }

            return await query.SingleAsync();

        }

        public async Task<T> GetByGuidAsync(Guid Id)
        {
            return await Table.FindAsync(Id);
        }

        public async Task<T> UpdateAsync(T entity) //update normalde async olarak gitmez.
        {
            await Task.Run(()=>Table.Update(entity));
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            await Task.Run(() => Table.Remove(entity));
            
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
           return await Table.AnyAsync(predicate);

        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null)
        {
            return await Table.CountAsync(predicate);
        }


    }
}
