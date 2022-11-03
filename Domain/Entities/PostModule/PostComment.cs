using Domain.Entities.GeneralModule;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.PostModule
{
    [Table("PostComment")]
    public class PostComment : BaseEntity
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(1000)]
        public string? Comment { get; set; }

        [ForeignKey("Post")]
        public int fk_PostID { get; set; }
        public virtual Post? Post { get; set; }
    }
}
