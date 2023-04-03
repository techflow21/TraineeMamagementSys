using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TMS.BLL.Interfaces;
using TMS.DAL.Dtos;

namespace TMS.MVC.Controllers
{
    public class InstructorController : Controller
    {
        private readonly IInstructorService _instructorService;
        private readonly ICourseService _courseService;

        public InstructorController(IInstructorService instructorService, ICourseService courseService)
        {
            _instructorService = instructorService;
            _courseService = courseService;
        }

        public async Task<IActionResult> Index()
        {
            var instructors = await _instructorService.GetAllInstructorsAsync();
            return View(instructors);
        }


        public async Task<IActionResult> Details(int id)
        {
            var instructor = await _instructorService.GetInstructorByIdAsync(id);

            if (instructor == null)
            {
                return NotFound();
            }
            return View(instructor);
        }


        public async Task<IActionResult> Create()
        {
            var instructor = new InstructorDto();
            instructor.Courses = await GetCoursesAsync();
            return View(instructor);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InstructorDto instructorDto)
        {
            if (ModelState.IsValid)
            {
                instructorDto.Courses = await GetCoursesAsync();

                await _instructorService.AddInstructorAsync(instructorDto);
                return RedirectToAction(nameof(Index));
            }
            return View(instructorDto);
        }



        public async Task<IActionResult> Edit(int id)
        {
            var instructor = await _instructorService.GetInstructorByIdAsync(id);

            if (instructor == null)
            {
                return NotFound();
            }

            instructor.Courses = await GetCoursesAsync();

            return View(instructor);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Email,Address,HireDate,SelectedCourseIds")] InstructorDto instructorDto)
        {
            if (id != instructorDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _instructorService.UpdateInstructorAsync(instructorDto);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _instructorService.InstructorExistsAsync(instructorDto.Id))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            instructorDto.Courses = await GetCoursesAsync();
            return View(instructorDto);
        }



        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var instructor = await _instructorService.GetInstructorByIdAsync(id);
            if (instructor == null)
            {
                return NotFound();
            }
            await _instructorService.DeleteInstructorAsync(id);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IEnumerable<SelectListItem>> GetCoursesAsync()
        {
            var careerPaths = await _courseService.GetAllCoursesAsync();
            var selectListItems = careerPaths.Select(cp => new SelectListItem
            {
                Value = cp.Id.ToString(),
                Text = cp.Title
            });

            return selectListItems;
        }
    }
}
