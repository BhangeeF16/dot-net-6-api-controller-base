using Domain.Common.Utilities;
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
                CanAddUser = true,
                CanEditUser = true,
                CanDeleteUser = true,
            }
        };

        public static List<UserRole> Roles { get => roles; }
        public static User User { get => user; }
    }
}
