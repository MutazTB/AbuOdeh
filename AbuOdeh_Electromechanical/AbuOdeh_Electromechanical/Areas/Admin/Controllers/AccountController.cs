using AbuOdeh_Electromechanical.Areas.Admin.Models;
using AbuOdeh_Electromechanical.Repository.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Newtonsoft.Json;
using System.Text;

namespace AbuOdeh_Electromechanical.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {

        private IUserService _user;

        public AccountController(IUserService user)
        {
            _user = user;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegister userRegister)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                await _user.Register(userRegister);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View();
            }

            return View("Login");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLogin userLogin)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                await _user.Login(userLogin);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View();
            }

            return Redirect("/Admin/Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _user.Logout();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return RedirectToAction("Login");
        }

    }
}
