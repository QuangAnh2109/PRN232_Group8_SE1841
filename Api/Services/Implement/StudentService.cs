using Api.Constants;
using Api.DTO;
using Api.Models;
using Api.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Api.Services.Implement
{
    public class StudentService : IStudentService
    {
        private readonly AppDbContext _context;

        public StudentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StudentResponseDTO>> GetAllStudentsAsync()
        {
            return await _context.Users
                .Where(u => u.RoleId == DefaultValues.StudentRoleId && u.IsDeleted == false)
                .Include(u => u.Role)
                .Select(u => new StudentResponseDTO
                {
                    Id = u.Id,
                    Username = u.Username,
                    FullName = u.FullName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    CenterId = u.CenterId,
                    CenterName = _context.Centers.Where(c => c.Id == u.CenterId).Select(c => c.Name).FirstOrDefault() ?? "",
                    IsActive = u.IsActive,
                    CreatedAt = u.CreatedAt,
                    LastModifiedTime = u.LastModifiedTime
                })
                .ToListAsync();
        }

        public async Task<StudentDetailsDTO> GetStudentDetailsByIdAsync(int id)
        {
            var student = await _context.Users
                .Where(u => u.Id == id && u.RoleId == DefaultValues.StudentRoleId && u.IsDeleted == false)
                .Include(u => u.Role)
                .Select(u => new
                {
                    User = u,
                    CenterName = _context.Centers.Where(c => c.Id == u.CenterId).Select(c => c.Name).FirstOrDefault(),
                    CenterAddress = _context.Centers.Where(c => c.Id == u.CenterId).Select(c => c.Address).FirstOrDefault(),
                    CenterEmail = _context.Centers.Where(c => c.Id == u.CenterId).Select(c => c.Email).FirstOrDefault()
                })
                .FirstOrDefaultAsync();

            if (student == null)
                throw new KeyNotFoundException($"Student with ID {id} not found");

            var classes = await _context.ClassStudents
                .Where(cs => cs.StudentId == id && cs.IsDeleted == false)
                .Include(cs => cs.Class)
                .Select(cs => new StudentClassInfo
                {
                    ClassId = cs.ClassId,
                    ClassName = cs.Class.Name,
                    JoinedAt = cs.JoinedAt,
                    StartDate = cs.Class.StartDate,
                    EndDate = cs.Class.EndDate,
                    IsActive = cs.Class.IsActive
                })
                .ToListAsync();

            return new StudentDetailsDTO
            {
                Id = student.User.Id,
                Username = student.User.Username,
                FullName = student.User.FullName,
                Email = student.User.Email,
                PhoneNumber = student.User.PhoneNumber,
                CenterId = student.User.CenterId,
                CenterName = student.CenterName ?? "",
                CenterAddress = student.CenterAddress ?? "",
                CenterEmail = student.CenterEmail ?? "",
                IsActive = student.User.IsActive,
                CreatedAt = student.User.CreatedAt,
                LastModifiedTime = student.User.LastModifiedTime,
                Classes = classes
            };
        }

        public async Task<StudentResponseDTO> CreateStudentAsync(CreateStudentDTO createStudentDTO, int centerId, int createdBy)
        {
            var centerExists = await _context.Centers
                .AnyAsync(c => c.Id == centerId && c.IsDeleted == false);

            if (!centerExists)
                throw new ArgumentException($"Center with ID {centerId} not found");

            var emailExists = await _context.Users
                .AnyAsync(u => u.Email == createStudentDTO.Email && u.IsDeleted == false);

            if (emailExists)
                throw new InvalidOperationException($"Email '{createStudentDTO.Email}' is already registered");

            var username = createStudentDTO.Email.Split('@')[0] + "_" + DateTime.UtcNow.Ticks.ToString().Substring(12);
            var defaultPassword = "Student@123";
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(defaultPassword);

            var newStudent = new User
            {
                Username = username,
                FullName = createStudentDTO.FullName,
                Email = createStudentDTO.Email,
                PhoneNumber = createStudentDTO.PhoneNumber,
                PasswordHash = passwordHash,
                CenterId = centerId,
                RoleId = DefaultValues.StudentRoleId,
                LastModifiedTime = DateTime.UtcNow,
                IsActive = true,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                CreatedBy = createdBy,
                UpdatedBy = createdBy,
                RecordNumber = 1
            };

            _context.Users.Add(newStudent);
            await _context.SaveChangesAsync();

            var centerName = await _context.Centers
                .Where(c => c.Id == centerId)
                .Select(c => c.Name)
                .FirstOrDefaultAsync();

            return new StudentResponseDTO
            {
                Id = newStudent.Id,
                Username = newStudent.Username,
                FullName = newStudent.FullName,
                Email = newStudent.Email,
                PhoneNumber = newStudent.PhoneNumber,
                CenterId = newStudent.CenterId,
                CenterName = centerName ?? "",
                IsActive = newStudent.IsActive,
                CreatedAt = newStudent.CreatedAt,
                LastModifiedTime = newStudent.LastModifiedTime
            };
        }

        public async Task DeleteStudentAsync(int id)
        {
            var student = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id && u.RoleId == DefaultValues.StudentRoleId && u.IsDeleted == false);

            if (student == null)
                throw new KeyNotFoundException($"Student with ID {id} not found");

            student.IsDeleted = true;
            student.IsActive = false;
            student.UpdatedAt = DateTime.UtcNow;
            student.RecordNumber++;

            var classStudents = await _context.ClassStudents
                .Where(cs => cs.StudentId == id && cs.IsDeleted == false)
                .ToListAsync();

            foreach (var cs in classStudents)
            {
                cs.IsDeleted = true;
                cs.UpdatedAt = DateTime.UtcNow;
                cs.RecordNumber++;
            }

            await _context.SaveChangesAsync();
        }

        public async Task UpdateStudentActiveStatusAsync(ActiveDTO<int> activeDTO, int updatedBy)
        {
            var student = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == activeDTO.Id && u.RoleId == DefaultValues.StudentRoleId && u.IsDeleted == false);

            if (student == null)
                throw new KeyNotFoundException($"Student with ID {activeDTO.Id} not found");

            student.IsActive = activeDTO.IsActive;
            student.UpdatedAt = DateTime.UtcNow;
            student.UpdatedBy = updatedBy;
            student.LastModifiedTime = DateTime.UtcNow;
            student.RecordNumber++;

            await _context.SaveChangesAsync();
        }
    }
}

