namespace Api.DTO
{
    public class AttendanceRecordDTO
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public string StudentEmail { get; set; } = string.Empty;
        public bool? IsPresent { get; set; }
        public string? Note { get; set; }
    }
}
