#region Imports

using Domain.Common.DataAccessHelpers;
using Domain.IRepositories.IGenericRepositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

#endregion

namespace Infrastructure.DataAccess.GenericRepositories;

public class GenericRepository<T> : QueriesRepository<T>, IGenericRepository<T> where T : class
{
    protected readonly ApplicationDbContext _context;

    public GenericRepository(ApplicationDbContext context, ConnectionInfo connectionInfo) : base(context, connectionInfo)
    {
        _context = context;
    }
    public async Task<T> GetAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }
    public async Task<T> GetByString(string id)
    {
        return await _context.Set<T>().FindAsync(id);
    }
    public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
    {
        var query = _context.Set<T>().AsQueryable();
        if (includes != null && includes.Length > 0)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }
        return await query.ToListAsync();
    }
    public IQueryable<T> GetAllQueryable()
    {
        return _context.Set<T>().AsQueryable();
    }
    public async Task AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
    }
    public void Add(T entity)
    {
        _context.Set<T>().Add(entity);
    }
    public async Task AddRangeAsync(IEnumerable<T> entity)
    {
        await _context.Set<T>().AddRangeAsync(entity);
    }
    public async Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
    {
        var query = _context.Set<T>().AsQueryable();
        if (includes != null && includes.Length > 0)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }
        return await query.Where(predicate).ToListAsync();
    }
    public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().FirstOrDefaultAsync(predicate);
    }
    public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
    {
        var query = _context.Set<T>().AsQueryable();
        if (includes != null && includes.Length > 0)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }
        return await query.FirstOrDefaultAsync(predicate) ?? default;
    }
    public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().AnyAsync(predicate);
    }
    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }
    public void DeleteRange(IEnumerable<T> entities)
    {
        _context.Set<T>().RemoveRange(entities);
    }
    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
    }
    public IEnumerable<T> ExecWithStoreProcedure(string query, params object[] parameters)
    {
        return _context.Set<T>().FromSqlRaw(query, parameters).ToList();
    }
}
