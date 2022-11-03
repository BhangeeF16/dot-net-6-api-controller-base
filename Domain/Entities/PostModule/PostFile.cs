﻿using Domain.Entities.GeneralModule;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.PostModule
{
    [Table("PostFile")]
    public class PostFile : BaseEntity
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(1000)]
        public string? FileKey { get; set; }
        [MaxLength(450)]
        public string? MimeType { get; set; }


        [ForeignKey("Post")]
        public int fk_PostID { get; set; }
        public virtual Post? Post { get; set; }
    }
}
