using Domain.Entities.GeneralModule;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.UsersModule
{
    [Table("User")]
    public class User : BaseEntity
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Username is must rquired")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Incorrect Password")]
        public string Password { get; set; }


        [Required(ErrorMessage = "First name is required")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required")]
        public string? LastName { get; set; }
        public DateTime DOB { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public Gender? Gender { get; set; }
        public Ethnicity? Ethnicity { get; set; }
        public CommunicationPreference? CommunicationPreference { get; set; }
        public string? ImageKey { get; set; }
        public bool? IsOnBoarded { get; set; }
        public bool IsPasswordChanged { get; set; }

        [ForeignKey("Role")]
        public int fk_RoleID { get; set; }

        public virtual UserRole? Role { get; set; }
    }
}
