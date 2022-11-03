using Domain.Entities.GeneralModule;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.PostModule
{
    [Table("PostType")]
    public class PostType : BaseEntity
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(100)]
        public string? Label { get; set; }
        [MaxLength(1000)]
        public string? Description { get; set; }

    }
}
