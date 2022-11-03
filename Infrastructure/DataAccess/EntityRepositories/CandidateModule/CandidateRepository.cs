using Domain.Common.DataAccessHelpers;
using Domain.Entities.CandidateModule;
using Domain.Entities.UsersModule;
using Domain.IRepositories.ICandidateRepositories;
using Domain.IRepositories.IGenericRepositories;
using Infrastructure.DataAccess.GenericRepositories;
using Infrastructure.Persistence;

namespace Infrastructure.DataAccess.EntityRepositories.CandidateModule
{
    public class CandidateRepository : GenericRepository<UserCandidateProfile>, ICandidateRepository
    {
        #region Constructors and locals

        private readonly ApplicationDbContext _dbContext;
        private readonly ConnectionInfo _connectionInfo;
        public CandidateRepository(ApplicationDbContext context, ConnectionInfo connectionInfo) : base(context, connectionInfo)
        {
            _connectionInfo = connectionInfo;
            _dbContext = context;
        }

        #endregion

        #region Inner Repositories

        public IGenericRepository<CandidateEducationExperience>? _educationExperienceRepository;
        public IGenericRepository<CandidateEducationExperience> EducationExperienceRepository
        {
            get
            {
                if (_educationExperienceRepository == null)
                    _educationExperienceRepository = new GenericRepository<CandidateEducationExperience>(_context, _connectionInfo);
                return _educationExperienceRepository;
            }
        }
        
        public IGenericRepository<CandidateJobExperience>? _candidateJobExperienceRepository;
        public IGenericRepository<CandidateJobExperience> JobExperienceRepository
        {
            get
            {
                if (_candidateJobExperienceRepository == null)
                    _candidateJobExperienceRepository = new GenericRepository<CandidateJobExperience>(_context, _connectionInfo);
                return _candidateJobExperienceRepository;
            }
        }
        
        public IGenericRepository<EducationSubject>? _educationSubjectRepository;
        public IGenericRepository<EducationSubject> EducationSubjectRepository
        {
            get
            {
                if (_educationSubjectRepository == null)
                    _educationSubjectRepository = new GenericRepository<EducationSubject>(_context, _connectionInfo);
                return _educationSubjectRepository;
            }
        }
        
        public IGenericRepository<CandidateResumeUploadDetail>? _candidateResumeUploadDetailRepository;
        public IGenericRepository<CandidateResumeUploadDetail> CandidateResumeUploadDetailRepository
        {
            get
            {
                if (_candidateResumeUploadDetailRepository == null)
                    _candidateResumeUploadDetailRepository = new GenericRepository<CandidateResumeUploadDetail>(_context, _connectionInfo);
                return _candidateResumeUploadDetailRepository;
            }
        }

        #endregion
    }
}
