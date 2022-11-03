using Domain.Common.DataAccessHelpers.Pagination;
using Domain.Common.Models.JobPostModule;
using Domain.Common.RequestModels.JobPostModule;

namespace Domain.IServices.IEntityServices.IJobPostModule
{
    public interface IJobService
    {
        Task<PaginatedList<JobForJobsPage>> GetJobsSearch(Pagination pagination);


        Task<bool> ApplyToJobAsync(ApplyForJobRequest model);
        Task<bool> CreateJobAsync(CreateJobRequest request);

    }
}
