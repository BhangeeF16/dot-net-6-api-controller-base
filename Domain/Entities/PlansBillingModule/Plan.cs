using Domain.Entities.GeneralModule;
using Domain.Entities.UsersModule;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.PlansBillingModule
{
    public class Plan : BaseEntity
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(50)]
        public string? Name { get; set; }
        [Precision(18, 2)]
        public decimal Price { get; set; }
        public TimeSpan ExpireAfter { get; set; }

        public int BillEvery { get; set; }
        public BillAfter BillAfter { get; set; }
    }
    public class UserPlan : BaseEntity
    {
        [Key]
        public int ID { get; set; }

        public DateTime PurchaseDate { get; set; }
        public DateTime ExpiryDate { get; set; }

        public bool IsExpired { get; set; }

        [ForeignKey("User")]
        public int fk_UserID { get; set; }
        [ForeignKey("Plan")]
        public int fk_PlanID { get; set; }

        public virtual User? User { get; set; }
        public virtual Plan? Plan { get; set; }
    }
}
