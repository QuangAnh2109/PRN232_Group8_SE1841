using Api.Models;

namespace Api.Repository.Interface
{
    public interface IUserRepository
    {
        Task AddUserAsync(IEnumerable<User> users);
    }
}
