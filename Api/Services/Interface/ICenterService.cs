using Api.DTO;

namespace Api.Services.Interface
{
    public interface ICenterService
    {
        Task<PaginationResult<CenterDto>> GetAllCentersWithPaginationAsync(int page, int limit);
        Task ChangeCenterActivityAsync(int id, bool isActive);
        Task<CenterDetailDto> GetCenterDetailsByIdAsync(int id);
    }
}
