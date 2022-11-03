#nullable disable

using Domain.Entities.CandidateModule;
using Domain.Entities.CorporateModule;
using Domain.Entities.GeneralModule;
using Domain.Entities.PostModule;
using Domain.Entities.UsersModule;
using Domain.IServices.IAuthServices;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        #region Constructors and Locals

        private readonly ICurrentUserService _currentUserService;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentUserService currentUserService) : base(options)
        {
            _currentUserService = currentUserService;
        }

        #endregion

        #region Overrrides

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ApplicationDbContextSeed.SeedSampleDataAsync(modelBuilder);
        }
        public override int SaveChanges()
        {
            var userID = _currentUserService.UserID;
            if (userID == 0)
            {
                return base.SaveChanges();
            }
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = userID;
                        entry.Entity.CreatedAt = DateTime.UtcNow;
                        entry.Entity.IsActive = true;
                        entry.Entity.IsDeleted = false;
                        break;
                    case EntityState.Modified:
                        entry.Entity.ModifiedBy = userID;
                        entry.Entity.ModifiedAt = DateTime.UtcNow;
                        break;
                }
            }
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = userID;
                        entry.Entity.CreatedAt = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.ModifiedBy = userID;
                        entry.Entity.ModifiedAt = DateTime.UtcNow;
                        break;
                }
            }
            return base.SaveChanges();
        }

        #endregion

        #region Modules

        #region Genral Modules

        public virtual DbSet<ApiCallLog> ApiCallLogs { get; set; }
        public virtual DbSet<AppSetting> AppSettings { get; set; }
        public virtual DbSet<MiddlewareLog> MiddlewareLogs { get; set; }

        #endregion

        #region Users Modules

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<UserCandidateProfile> UserCandidateProfiles { get; set; }
        public virtual DbSet<UserCorporateProfile> UserCorporateProfiles { get; set; }

        #endregion

        #region Corporate Modules

        public virtual DbSet<Corporate> Corporates { get; set; }
        public virtual DbSet<CorporateJob> CorporateJobs { get; set; }
        public virtual DbSet<JobApplicant> JobApplicants { get; set; }

        #endregion

        #region Candiate Moduless

        public virtual DbSet<CandidateResumeUploadDetail> CandidateResumeUploadDetails { get; set; }
        public virtual DbSet<CandidateJobExperience> CandidateJobExperiences { get; set; }
        public virtual DbSet<CandidateEducationExperience> CandidateEducationExperiences { get; set; }
        public virtual DbSet<EducationSubject> EducationSubjects { get; set; }

        #endregion

        #region Post Module
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<PostTag> PostTags { get; set; }
        public virtual DbSet<PostReaction> PostReactions { get; set; }
        public virtual DbSet<PostComment> PostComments { get; set; }
        public virtual DbSet<PostShare> PostShares { get; set; }
        public virtual DbSet<PostFile> PostFiles { get; set; }

        public virtual DbSet<PostType> PostTypes { get; set; }
        public virtual DbSet<PostReactionType> PostReactionTypes { get; set; }

        public virtual DbSet<PostCommentSetting> PostCommentSettings { get; set; }
        public virtual DbSet<PostViewSetting> PostViewSettings { get; set; }

        #endregion

        #endregion
    }
}
