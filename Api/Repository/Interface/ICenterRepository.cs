using Api.DTO;
using Api.Models;

namespace Api.Repository.Interface
{
    public interface ICenterRepository
    {
        Task AddCenterAsync(Center center);
        Task UpdateCenterAsync(Center center);
        Task<PaginationResult<CenterDto>> GetAllCentersWithPaginationAsync(int page, int limit);
        Task<CenterDetailDto> GetCenterDetailsByIdAsync(int id);
    }
}
