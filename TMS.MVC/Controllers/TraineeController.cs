using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TMS.BLL.Interfaces;
using TMS.DAL.Dtos;

namespace TMS.MVC.Controllers
{
    public class TraineeController : Controller
    {
        private readonly ITraineeService _traineeService;

        public TraineeController(ITraineeService traineeService)
        {
            _traineeService = traineeService;
        }


        public async Task<IActionResult> Index()
        {
            var trainees = await _traineeService.GetAllTraineesAsync();
            return View(trainees);
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainee = await _traineeService.GetTraineeByIdAsync(id.Value);

            if (trainee == null)
            {
                return NotFound();
            }

            return View(trainee);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TraineeDto traineeDto)
        {
            if (ModelState.IsValid)
            {
                await _traineeService.AddTraineeAsync(traineeDto);
                return RedirectToAction(nameof(Index));
            }

            return View(traineeDto);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainee = await _traineeService.GetTraineeByIdAsync(id.Value);

            if (trainee == null)
            {
                return NotFound();
            }

            return View(trainee);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TraineeDto traineeDto)
        {
            if (id != traineeDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _traineeService.UpdateTraineeAsync(traineeDto);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _traineeService.TraineeExists(traineeDto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(traineeDto);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainee = await _traineeService.GetTraineeByIdAsync(id.Value);

            if (trainee == null)
            {
                return NotFound();
            }

            return View(trainee);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _traineeService.DeleteTraineeAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
