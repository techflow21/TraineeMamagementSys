using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TMS.BLL.Interfaces;
using TMS.DAL.Dtos;

namespace TMS.MVC.Controllers
{
    public class CareerPathController : Controller
    {
        private readonly ICareerPathService _careerPathService;

        public CareerPathController(ICareerPathService careerPathService)
        {
            _careerPathService = careerPathService;
        }

        public async Task<IActionResult> Index()
        {
            var careerPaths = await _careerPathService.GetAllCareerPathsAsync();
            var model = new List<CareerPathDto>();

            if (careerPaths != null)
            {
                model = careerPaths.Select(cp => new CareerPathDto
                {
                    Id = cp.Id,
                    Name = cp.Name
                }).ToList();
            }

            return View(model);
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var career = await _careerPathService.GetCareerPathByIdAsync(id.Value);

            if (career == null)
            {
                return NotFound();
            }

            return View(career);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CareerPathDto careerDto)
        {
            if (ModelState.IsValid)
            {
                await _careerPathService.AddCareerPathAsync(careerDto);
                return RedirectToAction(nameof(Index));
            }

            return View(careerDto);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var career = await _careerPathService.GetCareerPathByIdAsync(id.Value);

            if (career == null)
            {
                return NotFound();
            }

            return View(career);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CareerPathDto careerDto)
        {
            if (id != careerDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _careerPathService.UpdateCareerPathAsync(careerDto);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _careerPathService.CareerPathExists(careerDto.Id))
                    {
                        return NotFound();
                    }
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(careerDto);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var career = await _careerPathService.GetCareerPathByIdAsync(id.Value);

            if (career == null)
            {
                return NotFound();
            }

            return View(career);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _careerPathService.DeleteCareerPathAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
