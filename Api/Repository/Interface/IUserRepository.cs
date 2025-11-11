using Api.Models;
using Api.DTO;

namespace Api.Repository.Interface
{
    public interface IUserRepository
    {
        Task<PaginationResult<UserAdminDto>> GetAllUsersWithPaginationAsync(int page, int limit);
        Task UpdateUserAndResetTokenAsync(User users);
        Task<User> GetUsersByIdAsync(int id);
        Task<User> GetUsersLoginAsync(string username);
        Task<UserDetailAdminDto> GetUserDetailsByIdAsync(int id);
        Task AddUserAsync(User user);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task UpdateUserAsync(User user);
    }
}
