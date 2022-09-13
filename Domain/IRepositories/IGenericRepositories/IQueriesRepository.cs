using Domain.Common.DTO.Pagination;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Domain.IRepositories.IGenericRepositories
{
    public interface IQueriesRepository<T> where T : class
    {
        IQueryable<T> Query();
        void Attach(T obj);
        List<T> BindList(DataTable dt);
        SqlParameter CreateSqlParameter(string ParameterName, object value);
        IQueryable<T> ExecuteSqlQuery(string sqlQuery, params SqlParameter[] parameters);
        DataTable GetDataTableFromQuery(string sqlQuery, List<SqlParameter> parameters);
        DataTable GetDataTableFromQuery(string sqlQuery, params SqlParameter[] parameters);
        DataSet GetDataSetFromQuery(string sqlQuery, params SqlParameter[] parameters);
        List<T> ExecuteSqlStoredProcedure(string sqlQuery, params SqlParameter[] parameters);
        List<T> ExecuteSqlStoredProcedure(string sqlQuery, Pagination pagination, List<SqlParameter> parameters);
        Task<PaginatedList<TResponse>> ExecuteSqlStoredProcedureAsync<TResponse>(string sqlQuery, Pagination pagination, List<SqlParameter> parameters) where TResponse : class;
    }
}
