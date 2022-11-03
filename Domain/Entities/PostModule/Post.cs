using Domain.Entities.GeneralModule;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.PostModule
{
    [Table("Post")]
    public class Post : BaseEntity
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(1000)]
        public string? Text { get; set; }

        [ForeignKey("CommentSetting")]
        public int fk_CommentSettingID { get; set; }
        [ForeignKey("PostViewSetting")]
        public int fk_PostViewSettingID { get; set; }
        [ForeignKey("PostType")]
        public int fk_PostTypeID { get; set; }

        public virtual PostType? PostType { get; set; }
        public virtual PostCommentSetting? CommentSetting { get; set; }
        public virtual PostViewSetting? PostViewSetting { get; set; }
        public virtual List<PostTag>? Tags { get; set; }
        public virtual List<PostFile>? Files { get; set; }
        public virtual List<PostComment>? Comments { get; set; }
        public virtual List<PostReaction>? Reactions { get; set; }
        public virtual List<PostShare>? Shares { get; set; }
    }
}
