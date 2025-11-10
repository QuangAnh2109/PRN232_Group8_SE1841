using Api.Services.Interface;
using Api.DTO;
using Api.Repository.Interface;
using Api.Models;

namespace Api.Services.Implement;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<PaginationResult<UserAdminDto>> GetAllUsersWithPaginationAsync(int page, int limit)
    {
        return await _userRepository.GetAllUsersWithPaginationAsync(page, limit);
    }
    
    public async Task ChangeUserActivityAsync(int id, bool isActive)
    {
        var user = await _userRepository.GetUsersByIdAsync(id);
        user.IsActive = isActive;
        await _userRepository.UpdateUserAndResetTokenAsync(user);
    }

    public Task<UserDetailAdminDto> GetUserDetailsByIdAsync(int id)
    {
        return _userRepository.GetUserDetailsByIdAsync(id);
    }
}