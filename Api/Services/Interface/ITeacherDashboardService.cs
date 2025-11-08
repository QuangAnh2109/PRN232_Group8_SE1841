using Api.DTO;

namespace Api.Services.Interface
{
    public interface ITeacherDashboardService
    {
        Task<TeacherDashboardDTO> GetDashboardAsync(int userId, string? role);
    }
}
