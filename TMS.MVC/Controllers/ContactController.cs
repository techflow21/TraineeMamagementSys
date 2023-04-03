using Microsoft.AspNetCore.Mvc;
using TMS.DAL.Dtos;

namespace TMS.MVC.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> SendEmail()
        {
            var email = new EmailDto();
            //course.CareerPaths = await GetCareerPaths();

            return View(email);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendEmail(EmailDto emailDto)
        {
            /*if (ModelState.IsValid)
            {
                courseDto.CareerPaths = await GetCareerPaths();

                await _courseService.AddCourseAsync(courseDto);
                return RedirectToAction(nameof(Index));
            }*/

            return View(emailDto);
        }
    }
}
