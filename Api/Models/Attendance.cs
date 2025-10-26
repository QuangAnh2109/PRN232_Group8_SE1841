using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    public class Attendance : BaseEntity
    {
        [Key]
        public int AttendanceId { get; set; }

        [Required]
        [ForeignKey(nameof(Api.Models.Timesheet))]
        public int TimesheetId { get; set; }

        [Required]
        [ForeignKey(nameof(Api.Models.User))]
        public int StudentId { get; set; }

        [Required]
        public bool IsAttenddance { get; set; }

        [MaxLength(500)]
        public string? Note { get; set; }

        public Timesheet Timesheet { get; set; } = null!;
        public User Student { get; set; } = null!;
    }
}
