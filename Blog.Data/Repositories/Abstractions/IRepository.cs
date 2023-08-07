using Blog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Repositories.Abstractions
{
    public interface IRepository<T> where T : class,IEntityBase,new() 
    {
        Task AddAsync(T entity);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties);

        Task<T> GetAsync(Expression<Func<T, bool>> predicate,params Expression<Func<T,object>>[] includeProperties); //bir işlem yaparken bu işlemde sadece bir veri dönmesini istiyosam.Bu metotu kullanıcam
        Task<T> GetByGuidAsync(Guid Id); //id yollayıp buna karşılık gelen entity bulmak için metot
        Task<T> UpdateAsync(T entity); //bu gelen entity güncellememi sağlayan metot.
        Task DeleteAsync(T entity); //gelen entity silmemi sağlayacak olan metot.

        Task<bool> AnyAsync(Expression<Func<T,bool>> predicate); 
        Task<int> CountAsync(Expression<Func<T, bool>> predicate=null); //article içinde kaç nesne var
    }
}
