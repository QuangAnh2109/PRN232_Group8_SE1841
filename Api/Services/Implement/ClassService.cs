using Api.DTO;
using Api.Models;
using Api.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Api.Services.Implement
{
    public class ClassService : IClassService
    {
        private readonly AppDbContext _context;

        public ClassService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ClassResponseDTO>> GetAllClassesAsync()
        {
            return await _context.Classes
                .Where(c => c.IsDeleted == false)
                .Include(c => c.Center)
                .Select(c => new ClassResponseDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    CenterId = c.CenterId,
                    CenterName = c.Center.Name,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    IsActive = c.IsActive,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<ClassResponseDTO>> GetStudentClassesAsync(int studentId)
        {
            return await _context.ClassStudents
                .Where(cs => cs.StudentId == studentId && cs.IsDeleted == false)
                .Include(cs => cs.Class)
                .ThenInclude(c => c.Center)
                .Where(cs => cs.Class.IsDeleted == false)
                .Select(cs => new ClassResponseDTO
                {
                    Id = cs.Class.Id,
                    Name = cs.Class.Name,
                    CenterId = cs.Class.CenterId,
                    CenterName = cs.Class.Center.Name,
                    StartDate = cs.Class.StartDate,
                    EndDate = cs.Class.EndDate,
                    IsActive = cs.Class.IsActive,
                    CreatedAt = cs.Class.CreatedAt,
                    UpdatedAt = cs.Class.UpdatedAt
                })
                .ToListAsync();
        }

        public async Task<ClassResponseDTO> GetClassByIdAsync(int id)
        {
            var classEntity = await _context.Classes
                .Where(c => c.Id == id && c.IsDeleted == false)
                .Include(c => c.Center)
                .FirstOrDefaultAsync();

            if (classEntity == null)
                throw new KeyNotFoundException($"Class with ID {id} not found");

            return new ClassResponseDTO
            {
                Id = classEntity.Id,
                Name = classEntity.Name,
                CenterId = classEntity.CenterId,
                CenterName = classEntity.Center.Name,
                StartDate = classEntity.StartDate,
                EndDate = classEntity.EndDate,
                IsActive = classEntity.IsActive,
                CreatedAt = classEntity.CreatedAt,
                UpdatedAt = classEntity.UpdatedAt
            };
        }

        public async Task<IEnumerable<StudentResponseDTO>> GetClassStudentsAsync(int classId)
        {
            return await _context.ClassStudents
                .Where(cs => cs.ClassId == classId && cs.IsDeleted == false)
                .Include(cs => cs.Student)
                .Where(cs => cs.Student.IsDeleted == false)
                .Select(cs => new StudentResponseDTO
                {
                    Id = cs.Student.Id,
                    Username = cs.Student.Username,
                    FullName = cs.Student.FullName,
                    Email = cs.Student.Email,
                    PhoneNumber = cs.Student.PhoneNumber,
                    IsActive = cs.Student.IsActive,
                    CreatedAt = cs.Student.CreatedAt
                })
                .ToListAsync();
        }

        public async Task<ClassResponseDTO> CreateClassAsync(CreateClassDTO createClassDTO, int createdBy)
        {
            var centerExists = await _context.Centers
                .AnyAsync(c => c.Id == createClassDTO.CenterId && c.IsDeleted == false);

            if (!centerExists)
                throw new ArgumentException($"Center with ID {createClassDTO.CenterId} not found");

            var classExists = await _context.Classes
                .AnyAsync(c => c.Name == createClassDTO.Name 
                    && c.CenterId == createClassDTO.CenterId 
                    && c.IsDeleted == false);

            if (classExists)
                throw new InvalidOperationException($"Class '{createClassDTO.Name}' already exists in this center");

            var newClass = new Class
            {
                Name = createClassDTO.Name,
                CenterId = createClassDTO.CenterId,
                StartDate = createClassDTO.StartDate,
                EndDate = createClassDTO.EndDate,
                IsActive = true,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                CreatedBy = createdBy,
                UpdatedBy = createdBy,
                RecordNumber = 1
            };

            _context.Classes.Add(newClass);
            await _context.SaveChangesAsync();

            await _context.Entry(newClass).Reference(c => c.Center).LoadAsync();

            return new ClassResponseDTO
            {
                Id = newClass.Id,
                Name = newClass.Name,
                CenterId = newClass.CenterId,
                CenterName = newClass.Center.Name,
                StartDate = newClass.StartDate,
                EndDate = newClass.EndDate,
                IsActive = newClass.IsActive,
                CreatedAt = newClass.CreatedAt,
                UpdatedAt = newClass.UpdatedAt
            };
        }

        public async Task<ClassResponseDTO> UpdateClassAsync(int id, UpdateClassDTO updateClassDTO, int updatedBy)
        {
            var existingClass = await _context.Classes
                .Include(c => c.Center)
                .FirstOrDefaultAsync(c => c.Id == id && c.IsDeleted == false);

            if (existingClass == null)
                throw new KeyNotFoundException($"Class with ID {id} not found");

            var centerExists = await _context.Centers
                .AnyAsync(c => c.Id == updateClassDTO.CenterId && c.IsDeleted == false);

            if (!centerExists)
                throw new ArgumentException($"Center with ID {updateClassDTO.CenterId} not found");

            var nameConflict = await _context.Classes
                .AnyAsync(c => c.Name == updateClassDTO.Name 
                    && c.CenterId == updateClassDTO.CenterId 
                    && c.Id != id 
                    && c.IsDeleted == false);

            if (nameConflict)
                throw new InvalidOperationException($"Class '{updateClassDTO.Name}' already exists in this center");

            existingClass.Name = updateClassDTO.Name;
            existingClass.CenterId = updateClassDTO.CenterId;
            existingClass.StartDate = updateClassDTO.StartDate;
            existingClass.EndDate = updateClassDTO.EndDate;
            existingClass.UpdatedAt = DateTime.UtcNow;
            existingClass.UpdatedBy = updatedBy;
            existingClass.RecordNumber++;

            await _context.SaveChangesAsync();

            if (existingClass.CenterId != updateClassDTO.CenterId)
            {
                await _context.Entry(existingClass).Reference(c => c.Center).LoadAsync();
            }

            return new ClassResponseDTO
            {
                Id = existingClass.Id,
                Name = existingClass.Name,
                CenterId = existingClass.CenterId,
                CenterName = existingClass.Center.Name,
                StartDate = existingClass.StartDate,
                EndDate = existingClass.EndDate,
                IsActive = existingClass.IsActive,
                CreatedAt = existingClass.CreatedAt,
                UpdatedAt = existingClass.UpdatedAt
            };
        }

        public async Task UpdateClassActiveStatusAsync(ActiveDTO<int> activeDTO, int updatedBy)
        {
            var existingClass = await _context.Classes
                .FirstOrDefaultAsync(c => c.Id == activeDTO.Id && c.IsDeleted == false);

            if (existingClass == null)
                throw new KeyNotFoundException($"Class with ID {activeDTO.Id} not found");

            existingClass.IsActive = activeDTO.IsActive;
            existingClass.UpdatedAt = DateTime.UtcNow;
            existingClass.UpdatedBy = updatedBy;
            existingClass.RecordNumber++;

            await _context.SaveChangesAsync();
        }
    }
}

