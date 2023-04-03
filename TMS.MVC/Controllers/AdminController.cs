using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TMS.DAL.Entities;

namespace TMS.MVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IPasswordHasher<AppUser> _passwordHasher;

        public AdminController(UserManager<AppUser> userManager, IPasswordHasher<AppUser> passwordHasher)
        {
            _userManager = userManager;
            _passwordHasher = passwordHasher;
        }


        public IActionResult Index()
        {
            return View(_userManager.Users);
        }


        public ViewResult Create() => View();


        [HttpPost]
        public async Task<IActionResult> Create(AppUser appUserModel)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new AppUser
                {
                    FirstName = appUserModel.FirstName,
                    LastName = appUserModel.LastName,
                    UserName = appUserModel.UserName,
                    Email = appUserModel.Email,
                    PhoneNumber = appUserModel.PhoneNumber
                };

                IdentityResult result = await _userManager.CreateAsync(appUser, appUserModel.Email);

                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                {
                    foreach (IdentityError error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
            }
            return View(appUserModel);
        }


        public async Task<IActionResult> Update(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
                return View(user);
            else
                return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> Update(string id, string email, string password)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                if (!string.IsNullOrEmpty(email))
                    user.Email = email;
                else
                    ModelState.AddModelError("", "Email cannot be empty");

                if (!string.IsNullOrEmpty(password))
                    user.PasswordHash = _passwordHasher.HashPassword(user, password);
                else
                    ModelState.AddModelError("", "Password cannot be empty");

                if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
                {
                    IdentityResult result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                        return RedirectToAction("Index");
                    else
                        Errors(result);
                }
            }
            else
                ModelState.AddModelError("", "User Not Found");
            return View(user);
        }


        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }
    }
}
