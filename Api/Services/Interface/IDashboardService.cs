using Api.DTO;

namespace Api.Services.Interface;

public interface IDashboardService
{
    Task<DashboardAdminDto> GetAdminDashboardAsync();
}