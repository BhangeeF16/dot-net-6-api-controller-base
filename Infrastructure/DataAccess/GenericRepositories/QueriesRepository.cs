using Domain.Common.DataAccessHelpers;
using Domain.Entities.GeneralModule.Pagination;
using Domain.IRepositories.IGenericRepositories;
using Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data;

namespace Infrastructure.DataAccess.GenericRepositories
{
    public class QueriesRepository<T> : IQueriesRepository<T> where T : class
    {
        #region Constructors and Locals

        private ApplicationDbContext _context;
        private SqlConnection _connection;
        public QueriesRepository(ApplicationDbContext context, ConnectionInfo connectionInfo)
        {
            _context = context;
            _context.Database.SetCommandTimeout(4800); // Set database request timeout 8 mintutes
            _connection = new SqlConnection(connectionInfo.ConnectionString);
        }

        #endregion

        #region Default Methods

        public void Attach(T entity)
        {
            var set = _context.Set<T>();
            set.Attach(entity);
        }
        public IQueryable<T> Query()
        {
            return _context.Set<T>();
        }
        public List<T> BindList(DataTable dt)
        {
            var serializeString = JsonConvert.SerializeObject(dt);
            return JsonConvert.DeserializeObject<List<T>>(serializeString);
        }
        private static TResponse BindObject<TResponse>(DataTable dt) where TResponse : class
        {
            var serializeString = JsonConvert.SerializeObject(dt);
            return JsonConvert.DeserializeObject<List<TResponse>>(serializeString)?.FirstOrDefault();
        }
        private static PagingData BindPagingData(DataTable dt)
        {
            var serializeString = JsonConvert.SerializeObject(dt);
            return JsonConvert.DeserializeObject<List<PagingData>>(serializeString)?.FirstOrDefault() ?? new PagingData();
        }
        private static List<TResponse> BindList<TResponse>(DataTable dt) where TResponse : class
        {
            var serializeString = JsonConvert.SerializeObject(dt);
            return JsonConvert.DeserializeObject<List<TResponse>>(serializeString);
        }

        #endregion

        public IQueryable<T> ExecuteSqlQuery(string sqlQuery, params SqlParameter[] sqlParameters)
        {
            if (sqlParameters != null)
            {
                return _context.Set<T>().FromSqlRaw(sqlQuery, sqlParameters).IgnoreQueryFilters().AsQueryable();
            }
            return _context.Set<T>().FromSqlRaw(sqlQuery).AsQueryable();
        }
        public List<T> ExecuteSqlStoredProcedure(string sqlQuery, params SqlParameter[] parameters)
        {
            var storedProcedureResult = GetDataTableFromQuery(sqlQuery, parameters);
            return BindList(storedProcedureResult);
        }
        public List<T> ExecuteSqlStoredProcedure(string sqlQuery, Pagination pagination, List<SqlParameter> parameters)
        {
            var totalCountOutput = new SqlParameter("@totalCount", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            parameters.Add(new SqlParameter("@pageNumber", pagination.PageNumber == null ? DBNull.Value : pagination.PageNumber));
            parameters.Add(new SqlParameter("@pageSize", pagination.PageSize == null ? DBNull.Value : pagination.PageSize));
            parameters.Add(new SqlParameter("@sortingCol", pagination.SortingCol == null ? DBNull.Value : pagination.SortingCol));
            parameters.Add(new SqlParameter("@sortDirection", pagination.SortDirection == null ? DBNull.Value : pagination.SortDirection));
            parameters.Add(new SqlParameter("@keyword", pagination.Keyword == null ? DBNull.Value : pagination.Keyword));
            parameters.Add(totalCountOutput);

            var storedProcedureResult = GetDataTableFromQuery(sqlQuery, parameters);
            return BindList(storedProcedureResult);
        }
        public async Task<PaginatedList<TResponse>> ExecuteSqlStoredProcedureAsync<TResponse>(string sqlQuery, Pagination pagination, List<SqlParameter> parameters) where TResponse : class
        {
            var totalCountOutput = new SqlParameter("@totalCount", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            parameters.Add(new SqlParameter("@pageNumber", pagination.PageNumber == null ? DBNull.Value : pagination.PageNumber));
            parameters.Add(new SqlParameter("@pageSize", pagination.PageSize == null ? DBNull.Value : pagination.PageSize));
            parameters.Add(new SqlParameter("@sortingCol", pagination.SortingCol == null ? DBNull.Value : pagination.SortingCol));
            parameters.Add(new SqlParameter("@sortDirection", pagination.SortDirection == null ? DBNull.Value : pagination.SortDirection));
            parameters.Add(new SqlParameter("@keyword", pagination.Keyword == null ? DBNull.Value : pagination.Keyword));
            parameters.Add(totalCountOutput);

            var storedProcedureResult = GetDataTableFromQuery(sqlQuery, parameters);
            var mappedData = BindList<TResponse>(storedProcedureResult);
            var totalCount = Convert.ToInt32(totalCountOutput.Value ?? 0);
            return await mappedData.PaginatedListAsync(pagination.PageNumber ?? 1, pagination.PageSize ?? 10, totalCount, pagination.Keyword ?? string.Empty);
        }

        #region ADO .NET

        public DataSet GetDataSetFromQuery(string sqlQuery, List<SqlParameter> parameters)
        {
            var ds = new DataSet();
            using (_connection)
            {
                _connection.Open();
                var da = new SqlDataAdapter(sqlQuery, _connection);
                da.SelectCommand.Parameters.Clear();
                da.SelectCommand.Parameters.AddRange(parameters.ToArray());
                da.SelectCommand.CommandTimeout = 4800;
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(ds);
            }
            return ds;
        }
        public DataSet GetDataSetFromQuery(string sqlQuery, params SqlParameter[] parameters)
        {
            var ds = new DataSet();
            using (_connection)
            {
                _connection.Open();
                var da = new SqlDataAdapter(sqlQuery, _connection);
                da.SelectCommand.Parameters.Clear();
                da.SelectCommand.Parameters.AddRange(parameters);
                da.SelectCommand.CommandTimeout = 4800;
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(ds);
            }
            return ds;
        }
        public DataTable GetDataTableFromQuery(string sqlQuery, params SqlParameter[] parameters)
        {
            var dt = new DataTable();
            using (_connection)
            {
                _connection.Open();
                var da = new SqlDataAdapter(sqlQuery, _connection);
                da.SelectCommand.Parameters.Clear();
                da.SelectCommand.Parameters.AddRange(parameters);
                da.SelectCommand.CommandTimeout = 4800;
                da.SelectCommand.CommandType = CommandType.Text;
                da.Fill(dt);
            }
            return dt;
        }
        public DataTable GetDataTableFromQuery(string sqlQuery, List<SqlParameter> parameters)
        {
            var dt = new DataTable();
            using (_connection)
            {
                _connection.Open();
                var da = new SqlDataAdapter(sqlQuery, _connection);
                da.SelectCommand.Parameters.Clear();
                da.SelectCommand.Parameters.AddRange(parameters.ToArray());
                da.SelectCommand.CommandTimeout = 4800;
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
            }
            return dt;
        }
        public SqlParameter CreateSqlParameter(string ParameterName, object value)
        {
            return new SqlParameter()
            {
                ParameterName = ParameterName,
                Value = value
            };
        }

        #endregion
    }
}
