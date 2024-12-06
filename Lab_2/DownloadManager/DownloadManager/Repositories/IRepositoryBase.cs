using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace DownloadManager.Repositories
{
    public interface IRepositoryBase<T> where T : class
    {
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>>? predicate = default,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = default);
        Task<T?> GetFirstOrDefaultAsync(
            Expression<Func<T, bool>>? predicate = default,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = default);
    }
}
