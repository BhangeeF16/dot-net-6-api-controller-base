using Domain.Entities.CorporateModule;
using Domain.Entities.GeneralModule;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.UsersModule
{
    [Table("UserCorporateProfile")]
    public class UserCorporateProfile : BaseEntity
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Corporate")]
        public int fk_CorporateID { get; set; }

        [ForeignKey("User")]
        public int fk_UserID { get; set; }

        public virtual User? User { get; set; }
        public virtual Corporate? Corporate { get; set; }
        public virtual List<CorporateJob>? PostedJobs { get; set; }
    }
}
