using TMS.DAL.Dtos;

namespace TMS.BLL.Interfaces
{
    public interface ICourseService
    {
        Task AddCourseAsync(CourseDto courseDto);
        Task DeleteCourseAsync(int id);
        Task<IEnumerable<CourseDto>> GetAllCoursesAsync();
        Task<CourseDto> GetCourseByIdAsync(int id);
        Task UpdateCourseAsync(CourseDto courseDto);
        //Task<IEnumerable<CourseDto>> GetCoursesByIdsAsync(IEnumerable<int> courseIds);

    }
}
