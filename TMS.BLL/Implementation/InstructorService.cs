
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TMS.BLL.Interfaces;
using TMS.DAL.Dtos;
using TMS.DAL.Entities;
using TMS.DAL.Interfaces;

namespace TMS.BLL.Implementation
{
    public class InstructorService : IInstructorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IRepository<Instructor> _instructorRepository;
        private IRepository<Course> _courseRepository;
        private readonly IMapper _mapper;

        public InstructorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _instructorRepository = _unitOfWork.GetRepository<Instructor>();
            _courseRepository = _unitOfWork.GetRepository<Course>();
        }


        public async Task<IEnumerable<InstructorDto>> GetAllInstructorsAsync()
        {
            var instructors = await _instructorRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<InstructorDto>>(instructors);
        }


        public async Task<InstructorDto> GetInstructorByIdAsync(int id)
        {
            var instructor = await _instructorRepository.GetByIdAsync(id);

            return _mapper.Map<InstructorDto>(instructor);
        }


        public async Task AddInstructorAsync(InstructorDto instructorDto)
        {
            var instructor = _mapper.Map<Instructor>(instructorDto);

            if (instructorDto.SelectedCourseId.HasValue)
            {
                var course = await _courseRepository.GetByIdAsync(instructorDto.SelectedCourseId.Value);
                instructor.Courses = new List<Course> { course };
            }

            await _instructorRepository.AddAsync(instructor);
            await _unitOfWork.SaveChangesAsync();
        }


        public async Task UpdateInstructorAsync(InstructorDto instructorDto)
        {
            var instructor = await _instructorRepository.GetByIdAsync(instructorDto.Id);

            if (instructor != null)
            {
                _mapper.Map(instructorDto, instructor);
                await _unitOfWork.SaveChangesAsync();
            }
        }


        public async Task DeleteInstructorAsync(int id)
        {
            var instructor = await _instructorRepository.GetByIdAsync(id);
            if (instructor != null)
            {
                await _instructorRepository.DeleteAsync(instructor);
                await _unitOfWork.SaveChangesAsync();
            }
        }


        public async Task<bool> InstructorExistsAsync(int id)
        {
            var instructor = await GetInstructorByIdAsync(id);
            return instructor != null;
        }
    }
}
