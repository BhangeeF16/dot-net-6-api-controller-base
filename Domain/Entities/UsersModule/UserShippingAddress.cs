using Domain.Entities.GeneralModule;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.UsersModule
{
    [Table("UserShippingAddress")]
    public class UserShippingAddress : BaseEntity
    {
        [Key]
        public int ID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? StreetAddress { get; set; }
        public string? TownCity { get; set; }
        public string? PostCode { get; set; }
        public string? Country { get; set; }
        public string? State { get; set; }

        [ForeignKey("User")]
        public int fk_UserID { get; set; }

        public virtual User? User { get; set; }
    }
}
