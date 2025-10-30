using Api.Models;

namespace Api.Repository.Interface
{
    public interface ICenterRepository
    {
        Task<int> AddCenterAsync(List<Center> centers);
    }
}
