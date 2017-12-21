using System.Web.Mvc;
using Temporary_Prison.Business.Services;
using Temporary_Prison.Models;
using Temporary_Prison.Business.LogInState;

namespace Temporary_Prison.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILoginService loginService;

        public AccountController(ILoginService loginService)
        {
            this.loginService = loginService;
        }

        // GET: Account/Login
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            ViewBag.ReturnUrl = Url.Action("Index","Home");
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Check user name and password");
                return View(model);
            }

            var result = loginService.PasswordLogIn(model.Login, model.Password);

            switch (result)
            {
                case LogInStatus.Success:
                    return RedirectToLocal(returnUrl);

                case LogInStatus.Failure:
                    ModelState.AddModelError(string.Empty, "Unable to log in");
                    break;
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            loginService.Logout();
            return RedirectToAction("Index", "Home");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}