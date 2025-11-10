using Api.DTO;
using Api.Repository.Interface;
using Api.Services.Interface;

namespace Api.Services.Implement;

public class CenterService : ICenterService
{
    private readonly ICenterRepository _centerRepository;
    private readonly ILogger<CenterService> _logger;
    
    public CenterService(ICenterRepository centerRepository, ILogger<CenterService> logger)
    {
        _centerRepository = centerRepository;
        _logger = logger;
    }

    public async Task<PaginationResult<CenterDto>> GetAllCentersWithPaginationAsync(int page, int limit)
    {
        return await _centerRepository.GetAllCentersWithPaginationAsync(page, limit);
    }

    public async Task ChangeCenterActivityAsync(int id, bool isActive)
    {
        var center = await _centerRepository.GetCenterByIdAsync(id);
        center.IsActive = isActive;
        await _centerRepository.UpdateCenterAsync(center);
    }

    public async Task<CenterDetailDto> GetCenterDetailsByIdAsync(int id)
    {
        return await _centerRepository.GetCenterDetailsByIdAsync(id);
    }
}