#region Imports

using System.Linq.Expressions;

#endregion

namespace Domain.IRepositories.IGenericRepositories;

public interface IGenericRepository<T> : IQueriesRepository<T> where T : class
{
    Task<T> GetAsync(int id);
    Task<T> GetByString(string id);
    Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
    Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
    Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
    Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);

    Task AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entity);

    void Update(T entity);

    void Delete(T entity);
    void DeleteRange(IEnumerable<T> entities);

    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);

    IEnumerable<T> ExecWithStoreProcedure(string query, params object[] parameters);
}
