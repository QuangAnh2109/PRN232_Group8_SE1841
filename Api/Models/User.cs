using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    public class User : BaseEntity
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Username { get; set; } = null!;

        [Required]
        [MaxLength(200)]
        public string FullName { get; set; } = null!;

        [Required]
        [MaxLength(255)]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Phone]
        public string? PhoneNumber { get; set; }

        public string? AvatarUrl { get; set; }

        [Required]
        public string PasswordHash { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Api.Models.Center))]
        public int CenterId { get; set; }

        [Required]
        public bool IsActive { get; set; }
        
        [Required]
        public DateTime LastModifiedTime { get; set; }

        [Required]
        [ForeignKey(nameof(Role))]
        public int RoleId { get; set; }

        [Required]
        [ForeignKey("RoleId")]
        public Role? Role { get; set; }
    }
}
