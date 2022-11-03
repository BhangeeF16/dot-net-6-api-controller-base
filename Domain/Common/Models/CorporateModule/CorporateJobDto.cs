using Domain.Entities.GeneralModule;
using Domain.Entities.PostModule;
using Domain.Entities.UsersModule;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Common.Models.CorporateModule
{
    [Table("CorporateJob")]
    public class CorporateJobDto : BaseEntity
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Corporate")]
        public int fk_CorporateID { get; set; }

        [ForeignKey("Post")]
        public int fk_PostID { get; set; }

        [ForeignKey("JobPostedByProfile")]
        public int fk_JobPostedByProfileID { get; set; }

        public virtual Post? Post { get; set; }
        public virtual CorporateDto? Corporate { get; set; }
        public virtual List<JobApplicantDto>? Applicants { get; set; }
        public virtual UserCorporateProfile? JobPostedByProfile { get; set; }
    }
}
