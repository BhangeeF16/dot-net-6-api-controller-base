using Domain.Entities.GeneralModule;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Common.Models.CorporateModule
{
    [Table("Corporate")]
    public class CorporateDto : BaseEntity
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(50)]
        public string? Company { get; set; }
        [MaxLength(50)]
        public string? HeadQuarterName { get; set; }
        [MaxLength(50)]
        public string? HeadQuarterContact { get; set; }
        public virtual List<CorporateJobDto>? PostedJobs { get; set; }
    }
}
