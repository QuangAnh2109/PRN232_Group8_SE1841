using Api.Models;

namespace Api.Repository.Interface
{
    public interface IUserRepository
    {
        Task<int> AddUserAsync(List<User> users);
    }
}
