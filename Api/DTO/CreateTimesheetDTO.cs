using System.ComponentModel.DataAnnotations;

namespace Api.DTO
{
    public class CreateTimesheetDTO
    {
        [Required]
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
    }
}
