using Api.Constants;
using Api.DTO;
using Api.Models;
using Api.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Api.Services.Implement
{
    public class TeacherDashboardService : ITeacherDashboardService
    {
        private readonly AppDbContext _context;

        public TeacherDashboardService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TeacherDashboardDTO> GetDashboardAsync(int userId, string? role)
        {
            bool isTeacher = string.Equals(role, DefaultValues.TeacherRole, StringComparison.OrdinalIgnoreCase);

            var classQuery = _context.Classes
                .Where(c => c.IsDeleted == false);

            if (isTeacher)
            {
                classQuery = classQuery.Where(c => c.TeacherId == userId);
            }

            var classIds = await classQuery.Select(c => c.Id).ToListAsync();

            var totalClasses = classIds.Count;
            var activeClasses = await _context.Classes
                .Where(c => classIds.Contains(c.Id) && c.IsActive)
                .CountAsync();
            var inactiveClasses = totalClasses - activeClasses;

            var totalStudents = await _context.ClassStudents
                .Where(cs => classIds.Contains(cs.ClassId) && cs.IsDeleted == false)
                .Select(cs => cs.StudentId)
                .Distinct()
                .CountAsync();

            var classDetails = await classQuery
                .Include(c => c.Center)
                .Select(c => new
                {
                    c.Id,
                    c.Name,
                    CenterName = c.Center.Name,
                    c.IsActive
                })
                .ToListAsync();

            var studentCounts = await _context.ClassStudents
                .Where(cs => classIds.Contains(cs.ClassId) && cs.IsDeleted == false)
                .GroupBy(cs => cs.ClassId)
                .Select(g => new { ClassId = g.Key, Count = g.Count() })
                .ToDictionaryAsync(g => g.ClassId, g => g.Count);

            var classSummaries = classDetails
                .Select(cd => new ClassSummaryDTO
                {
                    ClassId = cd.Id,
                    ClassName = cd.Name,
                    CenterName = cd.CenterName,
                    IsActive = cd.IsActive,
                    StudentCount = studentCounts.TryGetValue(cd.Id, out var count) ? count : 0
                })
                .OrderByDescending(cs => cs.IsActive)
                .ThenBy(cs => cs.ClassName)
                .ToList();

            var timesheetQuery = _context.Timesheets
                .Include(t => t.Class)
                    .ThenInclude(c => c.Center)
                .Where(t => t.IsDeleted == false);

            if (classIds.Any())
            {
                timesheetQuery = timesheetQuery.Where(t => classIds.Contains(t.ClassId));
            }
            else
            {
                timesheetQuery = timesheetQuery.Where(t => false);
            }

            var now = DateTime.UtcNow;
            var startOfWeek = StartOfWeekUtc(now);
            var endOfWeek = startOfWeek.AddDays(7);

            var sessionsThisWeek = await timesheetQuery
                .Where(t => t.StartTime >= startOfWeek && t.StartTime < endOfWeek)
                .CountAsync();

            var upcomingSessions = await timesheetQuery
                .Where(t => t.StartTime >= startOfWeek)
                .OrderBy(t => t.StartTime)
                .Take(100)
                .Select(t => new UpcomingSessionDTO
                {
                    TimesheetId = t.Id,
                    ClassId = t.ClassId,
                    ClassName = t.Class.Name,
                    Title = t.Title,
                    CenterName = t.Class.Center.Name,
                    StartTime = t.StartTime,
                    IsOnline = t.IsOnline
                })
                .ToListAsync();

            return new TeacherDashboardDTO
            {
                TotalClasses = totalClasses,
                ActiveClasses = activeClasses,
                InactiveClasses = inactiveClasses,
                TotalStudents = totalStudents,
                SessionsThisWeek = sessionsThisWeek,
                UpcomingSessions = upcomingSessions,
                Classes = classSummaries
            };
        }

        private static DateTime StartOfWeekUtc(DateTime date)
        {
            var diff = (7 + (date.DayOfWeek - DayOfWeek.Monday)) % 7;
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, DateTimeKind.Utc).AddDays(-diff);
        }
    }
}
