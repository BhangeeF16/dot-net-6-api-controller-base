using Domain.Common.DataAccessHelpers.Pagination;
using Domain.Entities.CorporateModule;
using Domain.IRepositories.IGenericRepositories;

namespace Domain.IRepositories.IJobPostModule
{
    public interface IJobRepository : IGenericRepository<CorporateJob>
    {
        IGenericRepository<JobApplicant> JobApplicantRepository { get; }

        Task<PaginatedList<TResponse>> GetJobsSearch<TResponse>(Pagination pagination) where TResponse : class;
    }
}
