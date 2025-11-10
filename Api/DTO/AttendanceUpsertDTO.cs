using System.ComponentModel.DataAnnotations;

namespace Api.DTO
{
    public class AttendanceUpsertDTO
    {
        [Required]
        public int TimesheetId { get; set; }

        [Required]
        public IEnumerable<AttendanceEntryDTO> Records { get; set; } = Array.Empty<AttendanceEntryDTO>();
    }

    public class AttendanceEntryDTO
    {
        [Required]
        public int StudentId { get; set; }

        [Required]
        public bool IsPresent { get; set; }

        [MaxLength(500)]
        public string? Note { get; set; }
    }
}
