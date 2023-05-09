using Infrastructure.Models;
using System.Linq.Expressions;

namespace Infrastructure.Repository
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<IQueryable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        IQueryable<T> Where(Expression<Func<T, bool>> func);
    }
}
