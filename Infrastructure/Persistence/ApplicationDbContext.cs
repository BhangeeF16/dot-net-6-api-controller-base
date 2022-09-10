#nullable disable

using Domain.Entities.GeneralModule;
using Domain.Entities.UsersModule;
using Domain.IServices.IAuthServices;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        private readonly ICurrentUserService _currentUserService;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentUserService currentUserService) : base(options)
        {
            _currentUserService = currentUserService;
        }

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


        #region Modules

        #region Genral Modules

        public virtual DbSet<ApiCallLogs> ApiCallLogs { get; set; }
        public virtual DbSet<AppSetting> AppSettings { get; set; }
        public virtual DbSet<MiddlewareLogs> MiddlewareLogs { get; set; }

        #endregion

        #region Users Modules

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }

        #endregion

        #endregion
    }
}
