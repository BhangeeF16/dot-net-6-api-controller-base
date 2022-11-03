using Domain.Entities.GeneralModule;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.PostModule
{
    [Table("PostReactionType")]
    public class PostReactionType : BaseEntity
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(100)]
        public string? Label { get; set; }
        [MaxLength(1000)]
        public string? IconKey { get; set; }
    }
}
