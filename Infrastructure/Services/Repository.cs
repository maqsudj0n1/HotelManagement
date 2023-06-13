using Application.Abstractions;
using Application.Interfaces.Repositories;
using System.Linq.Expressions;

namespace Infrastructure.Services;

public class Repository<T>:IRepository<T> where T : class
{
    private readonly IApplicationDbContext _context;
    public Repository(IApplicationDbContext context)
    {
        _context = context;
    }
    public virtual async Task<T> CreateAsync(T entity)
    {
        _context.Set<T>().Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task<bool> DeleteAsync(Guid Id)
    {
        var entity = _context.Set<T>().Find(Id);
        if (entity != null)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;

    }

    public virtual Task<IQueryable<T>> GetAsync(Expression<Func<T, bool>> expression)
    {
        return Task.FromResult(_context.Set<T>().Where(expression));
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public virtual async Task<T> UpdateAsync(T entity)
    {
        if (entity != null)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        return null;
    }

   
}
