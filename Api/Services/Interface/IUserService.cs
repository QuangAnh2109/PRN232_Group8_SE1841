using Api.DTO;

namespace Api.Services.Interface
{
    public interface IUserService
    {
        Task<PaginationResult<UserAdminDto>> GetAllUsersWithPaginationAsync(int page, int limit);
        Task ChangeUserActivityAsync(int id, bool isActive);
        Task<UserDetailAdminDto> GetUserDetailsByIdAsync(int id);
    }
}
