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
            Email = "admin@acme.com",
            Password = PasswordHasher.GeneratePasswordHash("Admin123!"),
            DOB = DateTime.UtcNow,
            Gender = Domain.Entities.GeneralModule.Gender.Male,
            Ethnicity = Domain.Entities.GeneralModule.Ethnicity.Asian,
            CommunicationPreference = Domain.Entities.GeneralModule.CommunicationPreference.Both,
            PhoneNumber = String.Empty,
            Address = String.Empty,
            ImageKey = String.Empty,
            fk_RoleID = 1,
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
                RoleName = "Admin",
                CanViewUsers = true,
                CanAddUser = false,
                CanEditUser = false,
                CanDeleteUser = false,
            },
            new UserRole
            {
                ID = 3,
                RoleName = "User",
                CanViewUsers = true,
                CanAddUser = false,
                CanEditUser = false,
                CanDeleteUser = false,
            }
        };

        public static User User { get => user; }
        public static List<UserRole> Roles { get => roles; }
    }
}
