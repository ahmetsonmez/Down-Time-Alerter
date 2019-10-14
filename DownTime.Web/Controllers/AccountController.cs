using System.Collections.Generic;
using System.Security.Claims;
using DownTime.CommonModel.Models;
using DownTime.Services.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace DownTime.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index","Home");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginUserDto model,string returnUrl)
        {

            if (!ModelState.IsValid)
                return View();

            var isLogin = _userService.IsLogin(model);

            if (isLogin == null)
            {
                TempData["ErrorMessage"] = "Login Failed. Check your information.";
                return View(model);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, isLogin.Id.ToString()),
                new Claim(ClaimTypes.Name, isLogin.Email),                
                new Claim("FullName",isLogin.FirstName +  " " +isLogin.LastName),
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal).Wait();
            HttpContext.SignInAsync(principal);

            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return Redirect("/Home/Index");
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}