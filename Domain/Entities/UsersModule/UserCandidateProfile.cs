using Domain.Entities.CandidateModule;
using Domain.Entities.CorporateModule;
using Domain.Entities.GeneralModule;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.UsersModule
{
    [Table("UserCandidateProfile")]
    public class UserCandidateProfile : BaseEntity
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("User")]
        public int fk_UserID { get; set; }

        [MaxLength(50)]
        public string? ContactEmail { get; set; }
        [MaxLength(50)]
        public string? ContactPhone { get; set; }
        [MaxLength(50)]
        public string? JobTitle { get; set; }
        [MaxLength(100)]
        public string? FacecbookUserName { get; set; }
        [MaxLength(100)]
        public string? LinkedInUserName { get; set; }
        [MaxLength(5000)]
        public string? About { get; set; }

        public virtual User? User { get; set; }
        public virtual List<CandidateResumeUploadDetail>? CandidateResumeUploadDetails { get; set; }
        public virtual List<JobApplicant>? JobApplications { get; set; }
        public virtual List<CandidateJobExperience>? JobExperiences { get; set; }
        public virtual List<CandidateEducationExperience>? EducationExperiences { get; set; }

    }
}
