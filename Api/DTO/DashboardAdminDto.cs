namespace Api.DTO;

public class DashboardAdminDto
{
    public int TotalStudents { get; set; } = 0;
    public int TotalTeachers { get; set; } = 0;
    public int TotalUsers { get; set; } = 0;
    public int TotalClasses { get; set; } = 0;
    public int ActiveClasses { get; set; } = 0;
    public int TotalCenters { get; set; } = 0;
    public int ActiveCenters { get; set; } = 0;
}