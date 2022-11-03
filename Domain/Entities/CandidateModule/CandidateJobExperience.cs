using Domain.Entities.GeneralModule;
using Domain.Entities.UsersModule;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.CandidateModule
{
    [Table("CandidateJobExperience")]
    public class CandidateJobExperience : BaseEntity
    {
        [Key]
        public int ID { get; set; }

        [MaxLength(50)]
        public string? RoleName { get; set; }
        [MaxLength(50)]
        public string? CorporateName { get; set; }
        [MaxLength(5000)]
        public string? Details { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? EndedAt { get; set; }
        public bool IsPresentExperience { get; set; }

        [ForeignKey("CandidateProfile")]
        public int fk_CandidateProfileID { get; set; }

        public virtual UserCandidateProfile? CandidateProfile { get; set; }
    }
}
