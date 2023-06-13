using System.Linq.Expressions;

namespace Application.Interfaces.Repositories;

public interface IRepository<T> where T : class
{
    Task<T> CreateAsync(T entity);
    Task<IQueryable<T>> GetAsync(Expression<Func<T, bool>> expression);
    Task<T?> GetByIdAsync(Guid id);
    Task<T> UpdateAsync(T entity);
    Task<bool> DeleteAsync(Guid id);

}
