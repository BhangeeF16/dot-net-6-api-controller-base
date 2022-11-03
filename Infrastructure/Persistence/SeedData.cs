using Domain.Common.Utilities;
using Domain.Entities.CandidateModule;
using Domain.Entities.PostModule;
using Domain.Entities.UsersModule;

namespace Infrastructure.Persistence
{
    public static class SeedData
    {
        private static readonly User user = new()
        {
            ID = 1,
            FirstName = "Application",
            LastName = "Admin",
            UserName = "admin@codered.com",
            Password = PasswordHasher.GeneratePasswordHash("Admin123!"),
            DOB = DateTime.UtcNow,
            Gender = Domain.Entities.GeneralModule.Gender.Male,
            Ethnicity = Domain.Entities.GeneralModule.Ethnicity.Asian,
            CommunicationPreference = Domain.Entities.GeneralModule.CommunicationPreference.Both,
            PhoneNumber = String.Empty,
            Address = String.Empty,
            ImageKey = String.Empty,
            fk_RoleID = 1,
            IsOnBoarded = true,
            IsPasswordChanged = true,
            IsActive = true,
            IsDeleted = false,
        };
        private static readonly List<UserRole> roles = new()
        {
            new UserRole
            {
                ID = 1,
                RoleName = "Application Admin",
                CanViewUsers = true,
                CanAddUser = true,
                CanEditUser = true,
                CanDeleteUser = true,
            },
            new UserRole
            {
                ID = 2,
                RoleName = "CorporateUser",
                CanViewUsers = true,
                CanAddUser = false,
                CanEditUser = false,
                CanDeleteUser = false,
            },
            new UserRole
            {
                ID = 3,
                RoleName = "CandidateUser",
                CanViewUsers = true,
                CanAddUser = false,
                CanEditUser = false,
                CanDeleteUser = false,
            }
        };
        private static readonly List<PostViewSetting> postViewSettings = new()
        {
            new PostViewSetting
            {
                ID = 1,
                Label = "Anyone",
                Description = "Anyone can view this post"
            },
            new PostViewSetting
            {
                ID = 2,
                Label = "Only Me",
                Description = "No one can view this post"
            },
        };
        private static readonly List<PostCommentSetting> postCommentSettings = new()
        {
            new PostCommentSetting
            {
                ID = 1,
                Label = "Turned Off",
                Description = "No one is allowed to comment"
            },
            new PostCommentSetting
            {
                ID = 2,
                Label = "Only Me",
                Description = "Only I can comment on this post"
            },
            new PostCommentSetting
            {
                ID = 3,
                Label = "Default",
                Description = "Everyone can comment on this post"
            },
        };
        private static readonly List<PostReactionType> postReactionTypes = new()
        {
            new PostReactionType
            {
                ID = 1,
                Label = "Like",
            },
            new PostReactionType
            {
                ID = 2,
                Label = "Curious",
            },
            new PostReactionType
            {
                ID = 3,
                Label = "Helped",
            },
            new PostReactionType
            {
                ID = 4,
                Label = "Support",
            },
        };
        private static readonly List<PostType> postTypes = new()
        {
            new PostType
            {
                ID = 1,
                Label = "Ordinary",
            },
            new PostType
            {
                ID = 2,
                Label = "Job",
            },
            new PostType
            {
                ID = 3,
                Label = "Poll",
            },
            new PostType
            {
                ID = 4,
                Label = "Celebration",
            },
        };
        private static readonly List<EducationSubject> educationSubjects = new()
        {
            new EducationSubject
            {
                ID = 1,
                Subject = "Business",
            },
            new EducationSubject
            {
                ID = 2,
                Subject = "Computer Science",
            },
            new EducationSubject
            {
                ID = 3,
                Subject = "Medical",
            },
            new EducationSubject
            {
                ID = 4,
                Subject = "Engineering",
            },
        };

        public static User User { get => user; }
        public static List<UserRole> Roles { get => roles; }
        public static List<PostViewSetting> ViewSettings{ get => postViewSettings; }
        public static List<PostCommentSetting> CommentSettings { get => postCommentSettings; }
        public static List<PostReactionType> ReactionTypes { get => postReactionTypes; }
        public static List<PostType> PostTypes { get => postTypes; }
        public static List<EducationSubject> EducationSubjects { get => educationSubjects; }
    }
}
