using Domain.Entities.GeneralModule;
using Domain.Entities.PostModule;
using Domain.Entities.UsersModule;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.CorporateModule
{
    [Table("CorporateJob")]
    public class CorporateJob : BaseEntity
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(450)]
        public string? JobTitle { get; set; }
        public WorkPlaceType WorkPlaceType { get; set; }
        public JobType JobType { get; set; }
        [MaxLength(1000)]
        public string? Location { get; set; }

        [ForeignKey("Corporate")]
        public int fk_CorporateID { get; set; }

        [ForeignKey("Post")]
        public int fk_PostID { get; set; }

        [ForeignKey("JobPostedByProfile")]
        public int fk_JobPostedByProfileID { get; set; }

        public virtual Post? Post { get; set; }
        public virtual Corporate? Corporate { get; set; }
        public virtual List<JobApplicant>? Applicants { get; set; }
        public virtual UserCorporateProfile? JobPostedByProfile { get; set; }
    }
}
