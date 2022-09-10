using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.UsersModule
{
    public class PermissionsToRole
    {
        #region User Permissions

        [Required(ErrorMessage = "Permission is Required")]
        public bool CanAddUser { get; set; }
        [Required(ErrorMessage = "Permission is Required")]
        public bool CanViewUsers { get; set; }

        [Required(ErrorMessage = "Permission is Required")]
        public bool CanEditUser { get; set; }

        [Required(ErrorMessage = "Permission is Required")]
        public bool CanDeleteUser { get; set; }

        #endregion

    }
}
