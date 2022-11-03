using Domain.Entities.GeneralModule;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.PostModule
{
    [Table("PostReaction")]
    public class PostReaction : BaseEntity
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("PostReactionType")]
        public int fk_PostReactionTypeID { get; set; }
        public virtual PostReactionType? PostReactionType { get; set; }


        [ForeignKey("Post")]
        public int fk_PostID { get; set; }
        public virtual Post? Post { get; set; }
    }
}
