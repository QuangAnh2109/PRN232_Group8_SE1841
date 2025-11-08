using Api.DTO;
using Api.Models;

namespace Api.Services.Interface
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentResponseDTO>> GetAllStudentsAsync();
        Task<StudentDetailsDTO> GetStudentDetailsByIdAsync(int id);
        Task<StudentResponseDTO> CreateStudentAsync(CreateStudentDTO createStudentDTO, int centerId, int createdBy);
        Task DeleteStudentAsync(int id);
        Task UpdateStudentActiveStatusAsync(ActiveDTO<int> activeDTO, int updatedBy);
    }
}
