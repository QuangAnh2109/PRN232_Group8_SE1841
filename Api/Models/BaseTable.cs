using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class BaseEntity
    {
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public int CreatedBy { get; set; }

        [Required]
        public int UpdatedBy { get; set; }

        [Required]
        public int RecordNumber { get; set; } = 1;

        public bool? IsDeleted { get; set; } = false;
    }
}
