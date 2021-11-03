using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace JobApplication.Repository
{
    public interface IRepository<T> where T : class
    {


        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        Task<int> Add(T entity);
        Task<int> AddRange(IEnumerable<T> entities);
        Task<int> Remove(T entity);
        Task<int> RemoveRange(IEnumerable<T> entities);
        Task<int> UpdateAsync(T entity);
        Task<int> DeleteAsync(T entity);
    }
}
