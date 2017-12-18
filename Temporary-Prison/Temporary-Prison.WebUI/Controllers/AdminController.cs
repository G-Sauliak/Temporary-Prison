using AutoMapper;
using log4net;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Temporary_Prison.Attributes;
using Temporary_Prison.Business.Exceptions;
using Temporary_Prison.Business.Providers;
using Temporary_Prison.Business.UserManagers;
using Temporary_Prison.Common.Models;
using Temporary_Prison.Models;
using X.PagedList;

namespace Temporary_Prison.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserProvider userProvider;
        private readonly ILog log = LogManager.GetLogger("LOGGER");
        private readonly IUserManager userManager;

        public AdminController(IUserProvider userProvider, IUserManager userManager)
        {
            this.userProvider = userProvider;
            this.userManager = userManager;
        }

        // GET: Admin/index
        [Authorize(Roles = "Admin")]
        public ActionResult Index(int? page, string search, string currentFilter, int? currentTotal)
        {
            const int pageSize = 4;
            var totalCount = default(int);
            int _currentTotal = currentTotal ?? default(int);

            if (_currentTotal != default(int))
            {
                totalCount = _currentTotal;
            }
            if (search != null)
            {
                page = 1;
            }
            else
            {
                search = currentFilter;
            }
            var pageNum = page ?? 1;
            int skip = (pageNum - 1) * pageSize;

            ViewBag.CurrentFilter = search;
            ViewBag.RedirectUrl = Url.Action("Index", "Admin");

            var listUsers = userProvider.GetUsersForPagedList(skip, pageSize, ref totalCount);
            var usersPagedList = new StaticPagedList<User>(listUsers, pageNum, pageSize, totalCount);

            return View(usersPagedList);
        }

        // GET: /Admin/EditUser
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult EditUser(string userName)
        {
            ViewBag.RedirectUrl = Url.Action("Index", "Admin");

            var userModel = default(UserViewModel);
            if (string.IsNullOrEmpty(userName))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = userProvider.GetUserByName(userName);

            if (user == null)
            {
                return HttpNotFound();
            }
            userModel = Mapper.Map<User, UserViewModel>(user);


            return View(userModel);
        }

        //POST: /Admin/EditUser
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult EditUser(UserViewModel model, string redirectUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = Mapper.Map<UserViewModel, User>(model);

            try
            {
                userManager.EditUser(user);
            }
            catch (CreateUserException ce)
            {
                ModelState.AddModelError("",ce.Message);
                return View(model);
            }
            return RedirectToLocal(redirectUrl);
        }
        // GET: Admin/AddUser
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AddUser()
        {
            var roles = userProvider.GetAllRoles();

            ViewBag.Roles = new SelectList(roles);

            return View(new UserViewModel());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteUser(string UserName, string redirectUrl)
        {
            if (string.IsNullOrEmpty(UserName))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (UserName.ToLower() == User.Identity.Name.ToLower())
            {
                ModelState.AddModelError(
                    string.Empty, "Error: Cannot delete the current user");

                return View("Index");
            }

            userManager.DeleteUser(UserName);

            return RedirectToLocal(redirectUrl);
        }
        // POST: Admin/AddUser
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult AddUser(UserViewModel model, string redirectUrl)
        {
            var userRole = Request.Form["Roles"];
            var allRoles = userProvider.GetAllRoles();

            if (!ModelState.IsValid || !allRoles.Contains(userRole))
            {
                var roles = userProvider.GetAllRoles();
                ViewBag.Roles = new SelectList(roles);
                return View(model);
            }

            var user = Mapper.Map<UserViewModel, User>(model);
            user.Roles = GetRoles(userRole);

            try
            {
                userManager.CreateUser(user);

            }
            catch (CreateUserException ex)
            {
                ModelState.AddModelError("", ex.Message);
                var roles = userProvider.GetAllRoles();
                ViewBag.Roles = new SelectList(roles);
                return View(model);
            }

            return RedirectToLocal(redirectUrl);
        }

        [HttpGet]
        [AjaxOnly]
        public JsonResult IsExistsLogin(string userName)
        {
            var isExists = userProvider.IsExistsByLogin(userName);
            return Json(new { isValid = isExists }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [AjaxOnly]
        public JsonResult IsExistsEmail(string email)
        {
            var isExists = userProvider.IsExistsByEmail(email);
            return Json(new { isValid = isExists }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult EditRoles(string userName)
        {
            if (userName == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var roles = userProvider.GetUserByName(userName).Roles;
            var model = new UserAndRolesViewModel
            {
                Roles = roles,
                UserName = userName
            };

            ViewBag.Roles = new SelectList(roles);

            return View(model);
        }

        private ActionResult RedirectToLocal(string redirectUrl)
        {
            if (Url.IsLocalUrl(redirectUrl))
            {
                return Redirect(redirectUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        private string[] GetRoles(string userRole)
        {
            switch (userRole)
            {
                case "Admin": return new string[] { "Admin", "Editor" };
                case "Editor": return new string[] { "Editor" };
                case "User": return new string[] { "User" };
                default: return default(string[]);
            }
        }
    }
}