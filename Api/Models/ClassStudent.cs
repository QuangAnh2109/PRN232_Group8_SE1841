using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    public class ClassStudent : BaseEntity<int>
    {
        [Required]
        [ForeignKey(nameof(Api.Models.User))]
        public int StudentId { get; set; }

        [Required]
        [ForeignKey(nameof(Api.Models.Class))]
        public int ClassId { get; set; }

        [Required]
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

        public User Student { get; set; } = null!;

        public Class Class { get; set; } = null!;
    }
}
