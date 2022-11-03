using Domain.Entities.CandidateModule;
using Domain.Entities.UsersModule;
using Domain.IRepositories.IGenericRepositories;

namespace Domain.IRepositories.ICandidateRepositories;

public interface ICandidateRepository : IGenericRepository<UserCandidateProfile>
{
    IGenericRepository<CandidateEducationExperience> EducationExperienceRepository { get; }
    IGenericRepository<CandidateJobExperience> JobExperienceRepository { get; }
    IGenericRepository<EducationSubject> EducationSubjectRepository { get; }
    IGenericRepository<CandidateResumeUploadDetail> CandidateResumeUploadDetailRepository { get; }

}
