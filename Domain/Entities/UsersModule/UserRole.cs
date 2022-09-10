using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.UsersModule
{
    [Table("UserRole")]
    public class UserRole : PermissionsToRole
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "Role is Required")]
        public string? RoleName { get; set; }

        public DateTime? CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public int? ModifiedBy { get; set; }
        public bool? IsActive { get; set; } = true;
        public bool? IsDeleted { get; set; } = false;

    }
}
