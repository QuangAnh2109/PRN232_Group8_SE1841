using Api.Constants;
using Api.DTO;
using Api.Models;
using Api.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Api.Repository.Implement;

public class CenterRepository : ICenterRepository
{
    private readonly ILogger<CenterRepository> _logger;
    private readonly AppDbContext _context;

    public CenterRepository(ILogger<CenterRepository> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }
    
    public async Task AddCenterAsync(Center center)
    {
        await _context.Centers.AddAsync(center);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCenterAsync(Center center)
    {
        try
        {
            var affectedRows = await _context.Centers
                .Where(c => c.Id == center.Id && 
                    c.RecordNumber == center.RecordNumber && 
                    c.IsDeleted == false && (
                        c.Name != center.Name ||
                        c.ManagerId != center.ManagerId ||
                        c.Address != center.Address ||
                        c.Email != center.Email ||
                        c.PhoneNumber != center.PhoneNumber ||
                        c.IsActive != center.IsActive
                    )
                )
                .ExecuteUpdateAsync(setter => setter
                    .SetProperty(c => c.Name, center.Name)
                    .SetProperty(c => c.ManagerId, center.ManagerId)
                    .SetProperty(c => c.Address, center.Address)
                    .SetProperty(c => c.Email, center.Email)
                    .SetProperty(c => c.PhoneNumber, center.PhoneNumber)
                    .SetProperty(c => c.IsActive, center.IsActive)
                    .SetProperty(c => c.RecordNumber, c => c.RecordNumber + 1)
                    .SetProperty(c => c.UpdatedAt, DateTime.UtcNow)
                    .SetProperty(c => c.UpdatedBy, center.UpdatedBy)
                );

            if (affectedRows == 0)
            {
                throw new Exception("Dữ liệu không thay đổi");
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Lỗi khi cập nhật Center: {Message}", e.Message);
            throw;
        }
    }

    public async Task<PaginationResult<CenterDto>> GetAllCentersWithPaginationAsync(int page, int limit)
    {
        try
        {
            if (page < 0) page = DefaultValues.DefaultPage;
            if (limit < 0) limit = DefaultValues.DefaultLimit;

            var totalCenters = await _context.Centers.CountAsync();

            var totalPages = (int)Math.Ceiling(totalCenters / (double)limit);

            var skip = (page - 1) * limit;

            var centers = await _context.Centers
                .Where(c => c.IsDeleted == false)
                .Select(center => new CenterDto
                {
                    Id = center.Id,
                    Name = center.Name,
                    ManagerName = center.Manager.FullName,
                    Address = center.Address,
                    Email = center.Email,
                    PhoneNumber = center.PhoneNumber,
                    Status = center.IsActive ? "Active" : "Inactive",
                    CreatedAt = center.CreatedAt
                })
                .Skip(skip)
                .Take(limit)
                .ToListAsync();

            return new PaginationResult<CenterDto>
            {
                Items = centers,
                TotalPages = totalPages,
                CurrentPage = page,
                TotalItems = totalCenters
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Lỗi khi lấy dữ liệu center: {Message}", ex.Message);
            throw;
        }
    }

    public async Task<CenterDetailDto> GetCenterDetailsByIdAsync(int id)
    {
        try
        {
            var centerDetail = await _context.Centers
                .Where(c => c.Id == id && c.IsDeleted == false)
                .Select(c => new CenterDetailDto
                {
                    Center = new CenterDto
                    {
                        Id = c.Id,
                        Name = c.Name,
                        ManagerName = c.Manager.FullName,
                        Address = c.Address,
                        Email = c.Email,
                        PhoneNumber = c.PhoneNumber,
                        Status = c.IsActive ? "Active" : "Inactive",
                        CreatedAt = c.CreatedAt
                    }
                })
                .FirstAsync();

            centerDetail.Classes = await _context.Classes
                .Where(c => c.CenterId == id && c.IsDeleted == false)
                .Select(c => new ClassDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    CenterName = c.Center.Name,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    Status = c.IsActive ? "Active" : "Inactive"
                })
                .ToListAsync();

            centerDetail.Users = await _context.Users
                .Where(u => u.CenterId == id && u.IsDeleted == false)
                .Select(u => new UserAdminDto
                {
                    Id = u.Id,
                    Username = u.Username,
                    FullName = u.FullName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    CenterName = u.Center.Name,
                    RoleName = u.Role.Name,
                    Status = u.IsActive ? "Active" : "Inactive"
                })
                .ToListAsync();
            
            return centerDetail;
        }
        catch (Exception e)
        {
            _logger.LogError("Lỗi khi lấy thông tin chi tiết center theo ID: {Message}", e.Message);
            throw;
        }
    }

    public Task<Center> GetCenterByIdAsync(int id)
    {
        return _context.Centers
            .Where(c => c.Id == id && c.IsDeleted == false)
            .FirstAsync();
    }
}