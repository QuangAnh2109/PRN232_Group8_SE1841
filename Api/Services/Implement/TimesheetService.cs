using Api.DTO;
using Api.Models;
using Api.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Api.Services.Implement
{
    public class TimesheetService : ITimesheetService
    {
        private readonly AppDbContext _context;

        public TimesheetService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TimesheetResponseDTO>> GetTimesheetsAsync(int? classId = null)
        {
            var query = _context.Timesheets
                .Where(t => t.IsDeleted == false)
                .Include(t => t.Class)
                .AsQueryable();

            if (classId.HasValue)
            {
                query = query.Where(t => t.ClassId == classId.Value);
            }

            var timesheets = await query
                .OrderByDescending(t => t.StartTime)
                .Select(t => new TimesheetResponseDTO
                {
                    Id = t.Id,
                    ClassId = t.ClassId,
                    ClassName = t.Class.Name,
                    StartTime = t.StartTime,
                    EndTime = t.EndTime,
                    IsOnline = t.IsOnline,
                    Title = t.Title,
                    Description = t.Description,
                    IsActive = t.IsActive,
                    CreatedAt = t.CreatedAt,
                    UpdatedAt = t.UpdatedAt
                })
                .ToListAsync();

            return timesheets;
        }

        public async Task<TimesheetResponseDTO> GetTimesheetByIdAsync(int id)
        {
            var timesheet = await _context.Timesheets
                .Include(t => t.Class)
                .FirstOrDefaultAsync(t => t.Id == id && t.IsDeleted == false);

            if (timesheet == null)
            {
                throw new KeyNotFoundException($"Timesheet with ID {id} not found");
            }

            return MapToResponse(timesheet);
        }

        public async Task<TimesheetResponseDTO> CreateTimesheetAsync(CreateTimesheetDTO createTimesheetDTO, int createdBy)
        {
            await ValidateClassAsync(createTimesheetDTO.ClassId);
            ValidateTimes(createTimesheetDTO.StartTime, createTimesheetDTO.EndTime);

            var timesheet = new Timesheet
            {
                ClassId = createTimesheetDTO.ClassId,
                StartTime = createTimesheetDTO.StartTime,
                EndTime = createTimesheetDTO.EndTime,
                IsOnline = createTimesheetDTO.IsOnline,
                Title = createTimesheetDTO.Title,
                Description = createTimesheetDTO.Description,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                CreatedBy = createdBy,
                UpdatedBy = createdBy,
                RecordNumber = 1,
                IsActive = true
            };

            _context.Timesheets.Add(timesheet);
            await _context.SaveChangesAsync();

            await _context.Entry(timesheet).Reference(t => t.Class).LoadAsync();

            return MapToResponse(timesheet);
        }

        public async Task<TimesheetResponseDTO> UpdateTimesheetAsync(int id, UpdateTimesheetDTO updateTimesheetDTO, int updatedBy)
        {
            var timesheet = await _context.Timesheets
                .Include(t => t.Class)
                .FirstOrDefaultAsync(t => t.Id == id && t.IsDeleted == false);

            if (timesheet == null)
            {
                throw new KeyNotFoundException($"Timesheet with ID {id} not found");
            }

            await ValidateClassAsync(updateTimesheetDTO.ClassId);
            ValidateTimes(updateTimesheetDTO.StartTime, updateTimesheetDTO.EndTime);

            timesheet.ClassId = updateTimesheetDTO.ClassId;
            timesheet.StartTime = updateTimesheetDTO.StartTime;
            timesheet.EndTime = updateTimesheetDTO.EndTime;
            timesheet.IsOnline = updateTimesheetDTO.IsOnline;
            timesheet.Title = updateTimesheetDTO.Title;
            timesheet.Description = updateTimesheetDTO.Description;
            timesheet.UpdatedAt = DateTime.UtcNow;
            timesheet.UpdatedBy = updatedBy;
            timesheet.RecordNumber++;

            await _context.SaveChangesAsync();

            if (timesheet.Class == null || timesheet.Class.Id != timesheet.ClassId)
            {
                await _context.Entry(timesheet).Reference(t => t.Class).LoadAsync();
            }

            return MapToResponse(timesheet);
        }

        public async Task DeleteTimesheetAsync(int id, int deletedBy)
        {
            var timesheet = await _context.Timesheets
                .Include(t => t.Class)
                .FirstOrDefaultAsync(t => t.Id == id && t.IsDeleted == false);

            if (timesheet == null)
            {
                throw new KeyNotFoundException($"Timesheet with ID {id} not found");
            }

            var attendanceRecords = await _context.Attendance
                .Where(a => a.TimesheetId == id && a.IsDeleted == false)
                .ToListAsync();

            timesheet.IsDeleted = true;
            timesheet.IsActive = false;
            timesheet.UpdatedAt = DateTime.UtcNow;
            timesheet.UpdatedBy = deletedBy;
            timesheet.RecordNumber++;

            foreach (var attendance in attendanceRecords)
            {
                attendance.IsDeleted = true;
                attendance.IsActive = false;
                attendance.UpdatedAt = DateTime.UtcNow;
                attendance.UpdatedBy = deletedBy;
                attendance.RecordNumber++;
            }

            await _context.SaveChangesAsync();
        }

        private async Task ValidateClassAsync(int classId)
        {
            var exists = await _context.Classes
                .AnyAsync(c => c.Id == classId && c.IsDeleted == false);

            if (!exists)
            {
                throw new ArgumentException($"Class with ID {classId} not found");
            }
        }

        private static void ValidateTimes(DateTime startTime, DateTime endTime)
        {
            if (endTime <= startTime)
            {
                throw new ArgumentException("End time must be greater than start time");
            }
        }

        private static TimesheetResponseDTO MapToResponse(Timesheet timesheet)
        {
            return new TimesheetResponseDTO
            {
                Id = timesheet.Id,
                ClassId = timesheet.ClassId,
                ClassName = timesheet.Class?.Name ?? string.Empty,
                StartTime = timesheet.StartTime,
                EndTime = timesheet.EndTime,
                IsOnline = timesheet.IsOnline,
                Title = timesheet.Title,
                Description = timesheet.Description,
                IsActive = timesheet.IsActive,
                CreatedAt = timesheet.CreatedAt,
                UpdatedAt = timesheet.UpdatedAt
            };
        }
    }
}
