using Domain.Entities.UsersModule;

namespace Infrastructure.Persistence
{
    public static class SeedData
    {
        private static readonly List<UserRole> roles = new()
        {
            new UserRole
            {
                ID = 1,
                RoleName = "Application Admin",
                CanAddUser = true,
                CanEditUser = true,
                CanDeleteUser = true,
            }, new UserRole
            {
                ID = 2,
                RoleName = "User",
            }
        };
        public static List<UserRole> Roles { get => roles; }
    }
}
