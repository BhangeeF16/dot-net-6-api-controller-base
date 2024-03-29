﻿using Domain.Entities.GeneralModule;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.UsersModule
{
    [Table("User")]
    public class User : BaseEntity
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Email { get; set; }
        [Required]
        [MaxLength(450)]
        public string? Password { get; set; }

        [Required]
        [MaxLength(50)]
        public string? FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string? LastName { get; set; }
        public DateTime DOB { get; set; }
        [Required]
        [MaxLength(50)]
        public string? PhoneNumber { get; set; }
        [MaxLength(450)]
        public string? Address { get; set; }
        public Gender? Gender { get; set; }
        public Ethnicity? Ethnicity { get; set; }
        public CommunicationPreference? CommunicationPreference { get; set; }
        [MaxLength(50)]
        public string? ImageKey { get; set; }
        public bool IsPasswordChanged { get; set; } = false;

        [ForeignKey("Role")]
        public int fk_RoleID { get; set; }

        public virtual UserRole? Role { get; set; }
        public virtual UserStripeDetail? StripeDetail { get; set; }
        public virtual UserShippingAddress? ShippingAddress { get; set; }
    }
}
