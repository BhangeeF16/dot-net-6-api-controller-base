using Domain.Entities.GeneralModule;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.CorporateModule
{
    [Table("Corporate")]
    public class Corporate : BaseEntity
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(50)]
        public string? Company { get; set; }
        [MaxLength(50)]
        public string? HeadQuarterName { get; set; }
        [MaxLength(50)]
        public string? HeadQuarterContact { get; set; }
        [MaxLength(50)]
        public string? Email { get; set; }
        [MaxLength(50)]
        public string? Logo { get; set; }
        public int NumberOfEmployees { get; set; }

        public virtual List<CorporateJob>? PostedJobs { get; set; }
    }
}
