using Domain.Entities.GeneralModule;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.UsersModule
{
    [Table("UserStripeDetail")]
    public class UserStripeDetail : BaseEntity
    {
        [Key]
        public int ID { get; set; }
        public string? CustomerID { get; set; }
        public string? ConnectAccountID { get; set; }


        [ForeignKey("User")]
        public int fk_UserID { get; set; }

        public virtual User? User { get; set; }
    }
}
