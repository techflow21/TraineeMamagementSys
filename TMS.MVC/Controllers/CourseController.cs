using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TMS.BLL.Interfaces;
using TMS.DAL.Dtos;

namespace TMS.MVC.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly ICareerPathService _careerPathService;

        public CourseController(ICourseService courseService, ICareerPathService careerPathService)
        {
            _careerPathService = careerPathService;
            _courseService = courseService;
        }


        public async Task<IActionResult> Index()
        {
            var courses = await _courseService.GetAllCoursesAsync();
            return View(courses);
        }


        public async Task<IActionResult> Create()
        {
            var course = new CourseDto();
            course.CareerPaths = await GetCareerPaths();

            return View(course);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseDto courseDto)
        {
            if (ModelState.IsValid)
            {
                courseDto.CareerPaths = await GetCareerPaths();

                await _courseService.AddCourseAsync(courseDto);
                return RedirectToAction(nameof(Index));
            }
            return View(courseDto);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CourseDto courseDto)
        {
            if (id != courseDto.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    await _courseService.UpdateCourseAsync(courseDto);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(courseDto);
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _courseService.GetCourseByIdAsync(id.Value);

            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }


        public async Task<IActionResult> Delete(int id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _courseService.DeleteCourseAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }


        public async Task<IEnumerable<SelectListItem>> GetCareerPaths()
        {
            var careerPaths = await _careerPathService.GetAllCareerPathsAsync();
            var selectListItems = careerPaths.Select(cp => new SelectListItem
            {
                Value = cp.Id.ToString(),
                Text = cp.Name
            });

            return selectListItems;
        }
    }
}
