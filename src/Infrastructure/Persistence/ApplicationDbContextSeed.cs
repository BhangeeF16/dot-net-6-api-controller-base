using Domain.Entities.UsersModule;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {

        public static void SeedSampleDataAsync(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(SeedData.User);
            modelBuilder.Entity<UserRole>().HasData(SeedData.Roles);
        }
    }

}
