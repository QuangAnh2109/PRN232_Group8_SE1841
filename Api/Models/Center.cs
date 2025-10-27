using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    public class Center : BaseEntity
    {
        [Key]
        public int CenterId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Api.Models.User))]
        public int ManagerId { get; set; }

        [MaxLength(200)]
        public string? Address { get; set; } = null!;

        [Required]
        [MaxLength(255)]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = null!;
        

        public ICollection<User>? Users { get; set; }

        public ICollection<Class>? Classes { get; set; }
    }
}
