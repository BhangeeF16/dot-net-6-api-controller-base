﻿#nullable disable

using Domain.Common.DomainEvent;
using Domain.Entities.GeneralModule;
using Domain.Entities.UsersModule;
using Domain.IServices.IAuthServices;
using Domain.IServices.IUtilities;
using Domain.Models.GeneralModels;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        #region Constructors and Locals

        private readonly IDomainEventDispatcher _dispatcher;
        private readonly ICurrentUserService _currentUserService;
        private readonly ConnectionInfo _connectionInfo;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentUserService currentUserService, ConnectionInfo connectionInfo) : base(options)
        {
            _currentUserService = currentUserService;
            _connectionInfo = connectionInfo;
        }

        //private readonly ICurrentUserService _currentUserService;
        //private readonly ConnectionInfo _connectionInfo;
        //public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentUserService currentUserService, ConnectionInfo connectionInfo, IDomainEventDispatcher dispatcher) : base(options)
        //{
        //    _currentUserService = currentUserService;
        //    _connectionInfo = connectionInfo;
        //    _dispatcher = dispatcher;
        //}

        #endregion

        #region Overrides
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionInfo.ConnectionString, b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ApplicationDbContextSeed.SeedSampleDataAsync(modelBuilder);
        }

        public override int SaveChanges()
        {
            var userID = _currentUserService.ID;
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
            foreach (var entry in ChangeTracker.Entries<UserRole>())
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

            PreSaveChanges().GetAwaiter().GetResult();
            var response = base.SaveChanges();
            PostSaveChanges().GetAwaiter().GetResult();

            return response;
        }
        private async Task PreSaveChanges()
        {
            await Task.CompletedTask;
        }
        private async Task PostSaveChanges()
        {
            await DispatchDomainEvents();
        }
        private async Task DispatchDomainEvents()
        {
            var domainEventEntities = ChangeTracker.Entries<IHasDomainEventEntity>()
                .Select(po => po.Entity)
                .Where(po => po.DomainEvents.Any())
                .ToArray();

            foreach (var entity in domainEventEntities)
            {
                while (entity.DomainEvents.TryTake(out IDomainEvent dev))
                    await _dispatcher.Dispatch(dev);
            }
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

        #endregion

        #endregion
    }
}
