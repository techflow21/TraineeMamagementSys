
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TMS.BLL.Interfaces;
using TMS.DAL.Context;
using TMS.DAL.Dtos;
using TMS.DAL.Entities;

namespace TMS.BLL.Implementation
{
    public class TraineeService : ITraineeService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public TraineeService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TraineeDto>> GetAllTraineesAsync()
        {
            var trainees = await _context.Trainees.ToListAsync();
            return _mapper.Map<IEnumerable<TraineeDto>>(trainees);
        }

        public async Task<TraineeDto> GetTraineeByIdAsync(int id)
        {
            var trainee = await _context.Trainees.FindAsync(id);
            return _mapper.Map<TraineeDto>(trainee);
        }

        public async Task AddTraineeAsync(TraineeDto traineeDto)
        {
            var trainee = _mapper.Map<Trainee>(traineeDto);
            _context.Trainees.Add(trainee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTraineeAsync(TraineeDto traineeDto)
        {
            var trainee = await _context.Trainees.FindAsync(traineeDto.Id);
            if (trainee != null)
            {
                _mapper.Map(traineeDto, trainee);
                _context.Entry(trainee).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteTraineeAsync(int id)
        {
            var trainee = await _context.Trainees.FindAsync(id);
            if (trainee != null)
            {
                _context.Trainees.Remove(trainee);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> TraineeExists(int id)
        {
            return await _context.Trainees.AnyAsync(e => e.Id == id);
        }
    }
}
