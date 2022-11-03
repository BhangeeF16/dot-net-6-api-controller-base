using Domain.Entities.CandidateModule;
using Domain.Entities.GeneralModule;
using Domain.Entities.UsersModule;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Common.Models.CorporateModule
{
    [Table("JobApplicant")]
    public class JobApplicantDto : AuditableEntity
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("CorporateJob")]
        public int fk_CorporateJobID { get; set; }

        [ForeignKey("CandidateResume")]
        public int fk_CandidateResumeID { get; set; }

        [ForeignKey("CandidateProfile")]
        public int fk_CandidateProfileID { get; set; }

        public virtual CorporateJobDto? CorporateJob { get; set; }
        public virtual CandidateResumeUploadDetail? CandidateResume { get; set; }
        public virtual UserCandidateProfile? CandidateProfile { get; set; }
    }
}
