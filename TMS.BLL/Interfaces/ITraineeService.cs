
using TMS.DAL.Dtos;

namespace TMS.BLL.Interfaces
{
    public interface ITraineeService
    {
        Task<IEnumerable<TraineeDto>> GetAllTraineesAsync();
        Task<TraineeDto> GetTraineeByIdAsync(int id);
        Task AddTraineeAsync(TraineeDto traineeDto);
        Task UpdateTraineeAsync(TraineeDto traineeDto);
        Task DeleteTraineeAsync(int id);
        Task<bool> TraineeExists(int id);
    }
}
