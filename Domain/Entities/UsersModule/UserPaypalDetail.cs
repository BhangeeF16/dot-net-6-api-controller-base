using Domain.Entities.GeneralModule;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.UsersModule
{
    [Table("UserPaypalDetail")]
    public class UserPaypalDetail : BaseEntity
    {
        [Key]
        public int ID { get; set; }
        public string? AccountHolderName { get; set; }

        [ForeignKey("User")]
        public int fk_UserID { get; set; }

        public virtual User? User { get; set; }
    }
}
