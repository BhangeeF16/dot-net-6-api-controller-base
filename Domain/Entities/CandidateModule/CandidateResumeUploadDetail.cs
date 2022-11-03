using Domain.Entities.CorporateModule;
using Domain.Entities.GeneralModule;
using Domain.Entities.UsersModule;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.CandidateModule
{
    [Table("CandidateResumeUploadDetail")]
    public class CandidateResumeUploadDetail : BaseEntity
    {
        [Key]
        public int ID { get; set; }
        public string? ResumeFileKey { get; set; }

        [ForeignKey("CandidateProfile")]
        public int fk_CandidateProfileID { get; set; }

        public virtual UserCandidateProfile? CandidateProfile { get; set; }
        public virtual List<JobApplicant>? Applications { get; set; }
    }
}
