using Api.DTO;
using Api.Models;

namespace Api.Services.Interface
{
    public interface IClassService
    {
        Task<IEnumerable<ClassResponseDTO>> GetAllClassesAsync();
        Task<IEnumerable<ClassResponseDTO>> GetStudentClassesAsync(int studentId);
        Task<ClassResponseDTO> GetClassByIdAsync(int id);
        Task<IEnumerable<StudentResponseDTO>> GetClassStudentsAsync(int classId);
        Task<ClassResponseDTO> CreateClassAsync(CreateClassDTO createClassDTO, int createdBy);
        Task<ClassResponseDTO> UpdateClassAsync(int id, UpdateClassDTO updateClassDTO, int updatedBy);
        Task UpdateClassActiveStatusAsync(ActiveDTO<int> activeDTO, int updatedBy);
    }
}
