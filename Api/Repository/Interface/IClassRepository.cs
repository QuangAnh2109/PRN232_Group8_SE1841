using Api.Models;

namespace Api.Repository.Interface
{
    public interface IClassRepository
    {
        Task<IEnumerable<Class>> GetAllClassesAsync();
    }
}
