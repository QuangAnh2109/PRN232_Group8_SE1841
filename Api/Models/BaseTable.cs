using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public abstract class BaseEntity<TKey> where TKey : struct, IEquatable<TKey>
    {
        [Key]
        public TKey Id { get; set; }

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

        [Required]
        public bool IsActive { get; set; } = true;

        public bool? IsDeleted { get; set; } = false;
    }
}
