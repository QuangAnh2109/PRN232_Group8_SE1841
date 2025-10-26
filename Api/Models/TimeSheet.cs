using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    public class Timesheet : BaseEntity
    {
        [Key]
        public int TimesheetId { get; set; }

        [Required]
        [ForeignKey(nameof(Api.Models.Class))]
        public int ClassId { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public bool IsOnline { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = null!;

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public Class Class { get; set; } = null!;
    }
}
