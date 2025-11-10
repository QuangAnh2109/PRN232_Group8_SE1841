using Api.DTO;

namespace Api.Services.Interface
{
    public interface ITimesheetService
    {
        Task<IEnumerable<TimesheetResponseDTO>> GetTimesheetsAsync(int? classId = null);
        Task<TimesheetResponseDTO> GetTimesheetByIdAsync(int id);
        Task<TimesheetResponseDTO> CreateTimesheetAsync(CreateTimesheetDTO createTimesheetDTO, int createdBy);
        Task<TimesheetResponseDTO> UpdateTimesheetAsync(int id, UpdateTimesheetDTO updateTimesheetDTO, int updatedBy);
        Task DeleteTimesheetAsync(int id, int deletedBy);
    }
}
