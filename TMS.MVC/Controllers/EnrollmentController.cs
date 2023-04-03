using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TMS.BLL.Interfaces;
using TMS.DAL.Dtos;
using TMS.DAL.Entities;

namespace TMS.MVC.Controllers
{
    public class EnrollmentController : Controller
    {
        private readonly IEnrollmentService _enrollmentService;
        private readonly IMapper _mapper;

        public EnrollmentController(IEnrollmentService enrollmentService, IMapper mapper)
        {
            _enrollmentService = enrollmentService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var enrollments = await _enrollmentService.GetAllEnrollmentsAsync();
            var enrollmentDtos = _mapper.Map<IEnumerable<Enrollment>, IEnumerable<EnrollmentDto>>(enrollments);

            return View(enrollmentDtos);
        }

        public async Task<IActionResult> Details(int id)
        {
            var enrollment = await _enrollmentService.GetEnrollmentByIdAsync(id);

            if (enrollment == null)
            {
                return NotFound();
            }

            var enrollmentDto = _mapper.Map<Enrollment, EnrollmentDto>(enrollment);

            return View(enrollmentDto);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EnrollmentDto enrollmentDto)
        {
            if (ModelState.IsValid)
            {
                var enrollment = _mapper.Map<EnrollmentDto, Enrollment>(enrollmentDto);
                await _enrollmentService.AddEnrollmentAsync(enrollment);

                return RedirectToAction(nameof(Index));
            }

            return View(enrollmentDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var enrollment = await _enrollmentService.GetEnrollmentByIdAsync(id);

            if (enrollment == null)
            {
                return NotFound();
            }

            var enrollmentDto = _mapper.Map<Enrollment, EnrollmentDto>(enrollment);

            return View(enrollmentDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EnrollmentDto enrollmentDto)
        {
            if (id != enrollmentDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var enrollment = _mapper.Map<EnrollmentDto, Enrollment>(enrollmentDto);
                await _enrollmentService.UpdateEnrollmentAsync(enrollment);

                return RedirectToAction(nameof(Index));
            }

            return View(enrollmentDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var enrollment = await _enrollmentService.GetEnrollmentByIdAsync(id);

            if (enrollment == null)
            {
                return NotFound();
            }

            var enrollmentDto = _mapper.Map<Enrollment, EnrollmentDto>(enrollment);

            return View(enrollmentDto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enrollment = await _enrollmentService.GetEnrollmentByIdAsync(id);

            if (enrollment == null)
            {
                return NotFound();
            }

            await _enrollmentService.DeleteEnrollmentAsync(enrollment);

            return RedirectToAction(nameof(Index));
        }
    }
}
