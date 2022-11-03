using Domain.Entities.GeneralModule;
using Domain.Entities.UsersModule;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.CandidateModule
{
    [Table("CandidateEducationExperience")]
    public class CandidateEducationExperience : BaseEntity
    {
        [Key]
        public int ID { get; set; }

        public LevelOfEdujcation LevelOfEdujcation { get; set; }

        [ForeignKey("EducationSubject")]
        public int fk_SubjectID { get; set; }

        [MaxLength(50)]
        public string? InstitueName { get; set; }
        [MaxLength(5000)]
        public string? Details { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? EndedAt { get; set; }
        public bool IsPresentExperience { get; set; }

        [ForeignKey("CandidateProfile")]
        public int fk_CandidateProfileID { get; set; }

        public virtual UserCandidateProfile? CandidateProfile { get; set; }
        public virtual EducationSubject? EducationSubject { get; set; }
    }

    [Table("EducationSubject")]
    public class EducationSubject : BaseEntity
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(50)]
        public string? Subject { get; set; }

        public virtual List<CandidateEducationExperience>? EducationExperiences { get; set; }
    }
}
