using Domain.Entities.GeneralModule;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.PostModule
{
    [Table("PostShare")]
    public class PostShare : BaseEntity
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Post")]
        public int fk_PostID { get; set; }
        public virtual Post? Post { get; set; }
    }
}
