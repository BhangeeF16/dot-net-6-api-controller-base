using Domain.Common.RequestModels.CandidateModule;

namespace Domain.IServices.IEntityServices.ICandidateModule
{
    public interface ICandidateService
    {
        bool UploadCandidateResumeRequest(UploadResumeRequest model);


        Task<bool> GenerateMyResumeAsync();
        Task<bool> GenerateResumeOfCandidateAsync(int CandidateProfileID);

        Task<bool> UpsertCandidateInfoAsync(UpsertJobExperienceRequest request);
        Task<bool> UpsertCandidateInfoAsync(UpsertEducationExperienceRequest request);
        Task<bool> UpsertCandidateInfoAsync(UpsertCandidateInfoRequest request);
    }
}
