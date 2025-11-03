using Api.Models;

namespace Api.Repository.Interface
{
    public interface ICenterRepository
    {
        Task<int> AddCenterAsync(IEnumerable<Center> centers);
    }
}
