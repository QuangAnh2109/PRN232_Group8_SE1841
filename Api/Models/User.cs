using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    public class User : BaseEntity<int>
    {
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

        [Required]
        public string PasswordHash { get; set; } = null!;

        [ForeignKey(nameof(Api.Models.Center))]
        public int? CenterId { get; set; }
        
        public Center? Center { get; set; } = null!;
        
        [Required]
        public DateTime LastModifiedTime { get; set; }

        [Required]
        [ForeignKey(nameof(Api.Models.Role))]
        public int RoleId { get; set; }

        [Required]
        public Role Role { get; set; } = null!;
    }
}
