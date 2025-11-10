using Api.Constants;
using Api.DTO;
using Api.Repository.Interface;
using Api.Services.Interface;

namespace Api.Services.Implement;

public class DashboardService : IDashboardService
{
    private readonly IUserRepository _userRepository;
    private readonly ICenterRepository _centerRepository;
    private readonly IClassRepository _classRepository;
    public DashboardService(IUserRepository userRepository, ICenterRepository centerRepository, IClassRepository classRepository)
    {
        _userRepository = userRepository;
        _centerRepository = centerRepository;
        _classRepository = classRepository;
    }
    
    public async Task<DashboardAdminDto> GetAdminDashboardAsync()
    {
        var users = await _userRepository.GetAllUsersAsync();
        var centers = await _centerRepository.GetAllCentersAsync();
        var classes = await _classRepository.GetAllClassesAsync();

        var dashboard = new DashboardAdminDto();
        foreach (var user in users)
        {
            dashboard.TotalUsers++;
            switch (user.Role.Id)
            {
                case DefaultValues.TeacherRoleId:
                    dashboard.TotalTeachers++;
                    break;
                case DefaultValues.StudentRoleId:
                    dashboard.TotalStudents++;
                    break;
            }
        }
        foreach (var center in centers)
        {
            dashboard.TotalCenters++;
            if (center.IsActive)
            {
                dashboard.ActiveCenters++;
            }
        }

        foreach (var classItem in classes)
        {
            dashboard.TotalClasses++;
            if (classItem.IsActive)
            {
                dashboard.ActiveClasses++;
            }
        }
        return dashboard;
    }
}