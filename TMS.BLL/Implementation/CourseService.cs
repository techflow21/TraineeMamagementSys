using AutoMapper;
using TMS.BLL.Interfaces;
using TMS.DAL.Dtos;
using TMS.DAL.Entities;
using TMS.DAL.Interfaces;

namespace TMS.BLL.Implementation
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRepository<Course> _courseRepository;


        public CourseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _courseRepository = _unitOfWork.GetRepository<Course>();
        }



        public async Task AddCourseAsync(CourseDto courseDto)
        {
            var course = _mapper.Map<Course>(courseDto);
            await _courseRepository.AddAsync(course);
            await _unitOfWork.SaveChangesAsync();
        }


        public async Task DeleteCourseAsync(int id)
        {
            await _courseRepository.DeleteByIdAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }


        public async Task<IEnumerable<CourseDto>> GetAllCoursesAsync()
        {
            var courses = await _courseRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CourseDto>>(courses);

        }


        public async Task<CourseDto> GetCourseByIdAsync(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            return _mapper.Map<CourseDto>(course);
        }


        public async Task UpdateCourseAsync(CourseDto courseDto)
        {
            var course = _mapper.Map<Course>(courseDto);

            await _courseRepository.UpdateAsync(course);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
