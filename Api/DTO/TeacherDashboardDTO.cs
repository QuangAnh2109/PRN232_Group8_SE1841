namespace Api.DTO
{
    public class TeacherDashboardDTO
    {
        public int TotalClasses { get; set; }
        public int ActiveClasses { get; set; }
        public int InactiveClasses { get; set; }
        public int TotalStudents { get; set; }
        public int SessionsThisWeek { get; set; }
        public IEnumerable<UpcomingSessionDTO> UpcomingSessions { get; set; } = Array.Empty<UpcomingSessionDTO>();
        public IEnumerable<ClassSummaryDTO> Classes { get; set; } = Array.Empty<ClassSummaryDTO>();
    }

    public class UpcomingSessionDTO
    {
        public int TimesheetId { get; set; }
        public int ClassId { get; set; }
        public string ClassName { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string CenterName { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public bool IsOnline { get; set; }
    }

    public class ClassSummaryDTO
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; } = string.Empty;
        public string CenterName { get; set; } = string.Empty;
        public int StudentCount { get; set; }
        public bool IsActive { get; set; }
    }
}
