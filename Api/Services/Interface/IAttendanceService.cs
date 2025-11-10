using Api.DTO;

namespace Api.Services.Interface
{
    public interface IAttendanceService
    {
        Task<IEnumerable<AttendanceRecordDTO>> GetTimesheetAttendanceAsync(int timesheetId);
        Task UpsertAttendanceAsync(AttendanceUpsertDTO upsertDTO, int updatedBy);
    }
}
