using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class Role : BaseEntity<int>
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        [MaxLength(255)]
        public string? Description { get; set; }

        [Required]
        public int PermissionLevel { get; set; }

        public ICollection<User>? Users { get; set; }
    }
}
