using Domain.Common.DataAccessHelpers;
using Domain.Common.DataAccessHelpers.Pagination;
using Domain.Entities.CorporateModule;
using Domain.IRepositories.IGenericRepositories;
using Domain.IRepositories.IJobPostModule;
using Infrastructure.DataAccess.GenericRepositories;
using Infrastructure.Persistence;
using Microsoft.Data.SqlClient;

namespace Infrastructure.DataAccess.EntityRepositories.JobPostModule
{
    public class JobRepository : GenericRepository<CorporateJob>, IJobRepository
    {
        #region Constructors and locals

        private readonly ApplicationDbContext _dbContext;
        private readonly ConnectionInfo _connectionInfo;
        public JobRepository(ApplicationDbContext context, ConnectionInfo connectionInfo) : base(context, connectionInfo)
        {
            _dbContext = context;
            _connectionInfo = connectionInfo;
        }

        #endregion

        #region Methods

        public async Task<PaginatedList<TResponse>> GetJobsSearch<TResponse>(Pagination pagination) where TResponse : class
        {
            return await ExecuteSqlStoredProcedureAsync<TResponse>(StoredProceduresLegend.GetJobPosts, pagination, new List<SqlParameter>());
        }

        #endregion

        #region Modular Repositories

        private IGenericRepository<JobApplicant>? _jobApplicantRepository;
        public IGenericRepository<JobApplicant> JobApplicantRepository
        {
            get
            {
                if (_jobApplicantRepository == null)
                    _jobApplicantRepository = new GenericRepository<JobApplicant>(_context, _connectionInfo);
                return _jobApplicantRepository;
            }
        }

        #endregion
    }
}
