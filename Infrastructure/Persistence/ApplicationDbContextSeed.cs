using Domain.Entities.CandidateModule;
using Domain.Entities.PostModule;
using Domain.Entities.UsersModule;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {

        public static void SeedSampleDataAsync(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>().HasData(SeedData.Roles);
            //modelBuilder.Entity<User>().HasData(SeedData.User);

            modelBuilder.Entity<PostViewSetting>().HasData(SeedData.ViewSettings);
            modelBuilder.Entity<PostCommentSetting>().HasData(SeedData.CommentSettings);
            modelBuilder.Entity<PostReactionType>().HasData(SeedData.ReactionTypes);
            modelBuilder.Entity<PostType>().HasData(SeedData.PostTypes);
            modelBuilder.Entity<EducationSubject>().HasData(SeedData.EducationSubjects);
        }
    }

}
