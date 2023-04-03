
using TMS.DAL.Dtos;

namespace TMS.BLL.Interfaces
{
    public interface IInstructorService
    {
        Task<IEnumerable<InstructorDto>> GetAllInstructorsAsync();
        Task<InstructorDto> GetInstructorByIdAsync(int id);
        Task AddInstructorAsync(InstructorDto instructorDto);
        Task UpdateInstructorAsync(InstructorDto instructorDto);
        Task DeleteInstructorAsync(int id);
        Task<bool> InstructorExistsAsync(int id);

    }
}
