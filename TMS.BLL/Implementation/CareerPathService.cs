
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TMS.BLL.Interfaces;
using TMS.DAL.Context;
using TMS.DAL.Dtos;
using TMS.DAL.Entities;
using TMS.DAL.Interfaces;

namespace TMS.BLL.Implementation
{
    public class CareerPathService : ICareerPathService
    {
        private readonly IUnitOfWork _unitOfWork;
        private AppDbContext _context;
        private readonly IMapper _mapper;

        public CareerPathService(IUnitOfWork unitOfWork, AppDbContext context, IMapper mapper)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CareerPathDto>> GetAllCareerPathsAsync()
        {
            var careerRepository = _unitOfWork.GetRepository<CareerPath>();
            var careers = await careerRepository.GetAllAsync();
            var careerDtos = _mapper.Map<IEnumerable<CareerPathDto>>(careers);
            return careerDtos;
        }

        public async Task<CareerPathDto> GetCareerPathByIdAsync(int id)
        {
            var careerPath = await _context.CareerPaths.FindAsync(id);
            return _mapper.Map<CareerPathDto>(careerPath);

        }


        public async Task AddCareerPathAsync(CareerPathDto careerPathDto)
        {
            var careerPath = _mapper.Map<CareerPath>(careerPathDto);
            _context.CareerPaths.Add(careerPath);
            await _context.SaveChangesAsync();
        }


        public async Task UpdateCareerPathAsync(CareerPathDto careerPathDto)
        {
            var careerPath = await _context.CareerPaths.FindAsync(careerPathDto.Id);
            if (careerPath != null)
            {
                _mapper.Map(careerPathDto, careerPath);
                _context.Entry(careerPath).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }


        public async Task DeleteCareerPathAsync(int id)
        {
            var careerPath = await _context.CareerPaths.FindAsync(id);
            if (careerPath != null)
            {
                _context.CareerPaths.Remove(careerPath);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> CareerPathExists(int id)
        {
            return await _context.CareerPaths.AnyAsync(c => c.Id == id);
        }
    }
}
