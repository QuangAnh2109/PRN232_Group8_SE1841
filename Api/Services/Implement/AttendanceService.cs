using Api.Constants;
using Api.DTO;
using Api.Models;
using Api.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Api.Services.Implement
{
    public class AttendanceService : IAttendanceService
    {
        private readonly AppDbContext _context;

        public AttendanceService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AttendanceRecordDTO>> GetTimesheetAttendanceAsync(int timesheetId)
        {
            var timesheet = await _context.Timesheets
                .Include(t => t.Class)
                .FirstOrDefaultAsync(t => t.Id == timesheetId && t.IsDeleted == false);

            if (timesheet == null)
            {
                throw new KeyNotFoundException($"Timesheet with ID {timesheetId} not found");
            }

            var classStudents = await _context.ClassStudents
                .Where(cs => cs.ClassId == timesheet.ClassId && cs.IsDeleted == false)
                .Include(cs => cs.Student)
                .ToListAsync();

            var attendanceRecords = await _context.Attendance
                .Where(a => a.TimesheetId == timesheetId && a.IsDeleted == false)
                .ToListAsync();

            var attendanceLookup = attendanceRecords.ToDictionary(a => a.StudentId, a => a);

            return classStudents.Select(cs =>
            {
                attendanceLookup.TryGetValue(cs.StudentId, out var record);
                return new AttendanceRecordDTO
                {
                    StudentId = cs.StudentId,
                    StudentName = cs.Student.FullName,
                    StudentEmail = cs.Student.Email,
                    IsPresent = record?.IsAttenddance,
                    Note = record?.Note
                };
            }).OrderBy(r => r.StudentName).ToList();
        }

        public async Task UpsertAttendanceAsync(AttendanceUpsertDTO upsertDTO, int updatedBy)
        {
            var timesheet = await _context.Timesheets
                .Include(t => t.Class)
                .FirstOrDefaultAsync(t => t.Id == upsertDTO.TimesheetId && t.IsDeleted == false);

            if (timesheet == null)
            {
                throw new KeyNotFoundException($"Timesheet with ID {upsertDTO.TimesheetId} not found");
            }

            var classStudentIds = await _context.ClassStudents
                .Where(cs => cs.ClassId == timesheet.ClassId && cs.IsDeleted == false)
                .Select(cs => cs.StudentId)
                .ToListAsync();

            var attendanceRecords = await _context.Attendance
                .Where(a => a.TimesheetId == upsertDTO.TimesheetId && a.IsDeleted == false)
                .ToDictionaryAsync(a => a.StudentId, a => a);

            foreach (var entry in upsertDTO.Records)
            {
                if (!classStudentIds.Contains(entry.StudentId))
                {
                    throw new ArgumentException($"Student {entry.StudentId} does not belong to class {timesheet.ClassId}");
                }

                if (attendanceRecords.TryGetValue(entry.StudentId, out var existing))
                {
                    existing.IsAttenddance = entry.IsPresent;
                    existing.Note = entry.Note;
                    existing.UpdatedAt = DateTime.UtcNow;
                    existing.UpdatedBy = updatedBy;
                    existing.RecordNumber++;
                }
                else
                {
                    var now = DateTime.UtcNow;
                    var attendance = new Attendance
                    {
                        TimesheetId = upsertDTO.TimesheetId,
                        StudentId = entry.StudentId,
                        IsAttenddance = entry.IsPresent,
                        Note = entry.Note,
                        CreatedAt = now,
                        UpdatedAt = now,
                        CreatedBy = updatedBy,
                        UpdatedBy = updatedBy,
                        RecordNumber = 1,
                        IsActive = true,
                        IsDeleted = false
                    };

                    _context.Attendance.Add(attendance);
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
