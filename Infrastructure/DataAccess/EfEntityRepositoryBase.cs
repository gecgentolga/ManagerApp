using System.Linq.Expressions;
using Application.DataAccess;
using Domain.Entities.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess;

public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
    where TEntity : class, IEntity, new()
    where TContext : DbContext
{
    protected readonly TContext _context;

    public EfEntityRepositoryBase(TContext context)
    {
        _context = context;
    }

    public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
    {
        return filter == null
            ? _context.Set<TEntity>().ToList()
            : _context.Set<TEntity>().Where(filter).ToList();
    }

    public TEntity Get(Expression<Func<TEntity, bool>> filter)
    {
        return _context.Set<TEntity>().SingleOrDefault(filter);
    }

    public void Add(TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);
    }

    public void Update(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
    }

    public void Delete(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}