using System.ComponentModel;
using Api.Models;
using Api.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Api.Constants;
using Api.DTO;

namespace Api.Repository.Implement
{
    public class UserRepository : IUserRepository
    {
        private readonly ILogger<UserRepository> _logger;
        private readonly AppDbContext _context;

        public UserRepository(ILogger<UserRepository> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<PaginationResult<UserAdminDto>> GetAllUsersWithPaginationAsync(int page, int limit)
        {
            try
            {
                if (page < 0) page = DefaultValues.DefaultPage;
                if (limit < 0) limit = DefaultValues.DefaultLimit;

                var totalUsers = await _context.Users.CountAsync();

                var totalPages = (int)Math.Ceiling(totalUsers / (double)limit);

                var skip = (page - 1) * limit;

                var users = await _context.Users
                    .Where(u => u.IsDeleted == false)
                    .Select(user => new UserAdminDto
                    {
                        Id = user.Id,
                        Username = user.Username,
                        FullName = user.FullName,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        CenterName = user.Center.Name,
                        RoleName = user.Role.Name,
                        Status = user.IsActive ? "Active" : "Inactive"
                    })
                    .Skip(skip)
                    .Take(limit)
                    .ToListAsync();

                return new PaginationResult<UserAdminDto>
                {
                    Items = users,
                    TotalPages = totalPages,
                    CurrentPage = page,
                    TotalItems = totalUsers
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy dữ liệu người dùng: {Message}", ex.Message);
                throw;
            }
        }

        public async Task UpdateUserAndResetTokenAsync(User user)
        {
            try
            {
                var affectedRows = await _context.Users
                    .Where(u => u.Id == user.Id && 
                        u.RecordNumber == user.RecordNumber && 
                        u.IsDeleted == false && (
                            u.IsActive != user.IsActive ||
                            u.RoleId != user.RoleId
                        )
                    )
                    .ExecuteUpdateAsync(setter => setter
                        .SetProperty(u => u.IsActive, user.IsActive)
                        .SetProperty(u => u.RoleId, user.RoleId)
                        .SetProperty(u => u.LastModifiedTime, DateTime.UtcNow)
                        .SetProperty(u => u.RecordNumber, c => c.RecordNumber + 1)
                        .SetProperty(u => u.UpdatedAt, DateTime.UtcNow)
                        .SetProperty(u => u.UpdatedBy, user.UpdatedBy)
                    );

                if (affectedRows == 0)
                {
                    throw new Exception("Dữ liệu không thay đổi");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Lỗi khi cập nhật người dùng: {Message}", e.Message);
                throw;
            }
        }

        public async Task<User> GetUsersByIdAsync(int id)
        {
            try
            {
                return await _context.Users
                    .Where(u => id == u.Id && u.IsDeleted == false)
                    .FirstAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy người dùng theo ID: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<User> GetUsersLoginAsync(string username)
        {
            try
            {
                return await _context.Users
                    .Where(u => username == u.Username && u.IsDeleted == false && u.IsActive == true)
                    .FirstAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy người dùng theo username: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<UserDetailAdminDto> GetUserDetailsByIdAsync(int id)
        {
            try
            {
                var userDetail = await _context.Users
                    .Where(u => u.Id == id && u.IsDeleted == false)
                    .Select(u => new UserDetailAdminDto
                    {
                        Id = u.Id,
                        Username = u.Username,
                        FullName = u.FullName,
                        Email = u.Email,
                        PhoneNumber = u.PhoneNumber,
                        RoleName = u.Role.Name,
                        UserStatus = u.IsActive ? "Active" : "Inactive",
                        CenterId = u.CenterId,
                        CenterName = u.Center.Name,
                        CenterAddress = u.Center.Address,
                        CenterEmail = u.Center.Email,
                        CenterStatus = u.Center.IsActive ? "Active" : "Inactive",
                        CreatedAt = u.CreatedAt,
                        CreatedBy = u.CreatedBy,
                    })
                    .FirstAsync();

                userDetail.Classes = await _context.ClassStudents
                    .Where(cs => cs.StudentId == id && cs.IsDeleted == false)
                    .Select(cs => new ClassInfo
                    {
                        ClassId = cs.ClassId,
                        ClassName = cs.Class.Name,
                        JoinedAt = cs.JoinedAt,
                        StartDate = cs.Class.StartDate,
                        EndDate = cs.Class.EndDate,
                        Status = cs.Class.IsActive ? "Active" : "Inactive"
                    })
                    .ToListAsync();
                
                return userDetail;
            }
            catch (Exception e)
            {
                _logger.LogError("Lỗi khi lấy thông tin chi tiết sinh viên theo ID: {Message}", e.Message);
                throw;
            }
        }

        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }
}