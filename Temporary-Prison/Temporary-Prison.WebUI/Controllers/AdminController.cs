using AutoMapper;
using log4net;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Security;
using Temporary_Prison.Attributes;
using Temporary_Prison.Business.Exceptions;
using Temporary_Prison.Business.Providers;
using Temporary_Prison.Business.UserManagers;
using Temporary_Prison.Common.Entities;
using Temporary_Prison.Models;
using X.PagedList;

namespace Temporary_Prison.Controllers
{
    [Authorize(Roles = "Admin")]
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
        public ActionResult Index(int? page, int? currentTotal)
        {
            const int pageSize = 4;
            var totalCount = default(int);
            var _currentTotal = currentTotal ?? default(int);

            if (_currentTotal != default(int))
            {
                totalCount = _currentTotal;
            }

            var pageNum = page ?? 1;
            var skip = (pageNum - 1) * pageSize;

            ViewBag.RedirectUrl = Url.Action("Index", "Admin");

            var listUsers = userProvider.GetUsersForPagedList(skip, pageSize, ref totalCount);
            var usersPagedList = new StaticPagedList<User>(listUsers, pageNum, pageSize, totalCount);

            return View(usersPagedList);
        }

        // GET: /Admin/EditUser
        public ActionResult EditUser(string userName)
        {
            ViewBag.RedirectUrl = Url.Action("Index", "Admin");

            var userModel = default(EditUserViewModel);
            if (string.IsNullOrEmpty(userName))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = userProvider.GetUserByName(userName);
            var userRoles = userProvider.GetUserByName(userName).Roles;
            var allRoles = userProvider.GetAllRoles();

            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            userModel = Mapper.Map<User, EditUserViewModel>(user);

            userModel.UserAndRoles = new UserAndRoles()
            {
                UserName = userName,
                Roles = userRoles

            };
            var missingRoles = (from role in allRoles
                                where !userRoles.Contains(role)
                                select role).ToList();

            ViewBag.listOfMissingRoles = new SelectList(missingRoles);

            return View(userModel);
        }

        //POST: /Admin/EditUser
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(EditUserViewModel model, string redirectUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = Mapper.Map<EditUserViewModel, User>(model);
            try
            {
                userManager.EditUser(user);
            }
            catch (CreateOrUpdateUserException ce)
            {
                ModelState.AddModelError("", ce.Message);
                return View(model);
            }
            return RedirectToLocal(redirectUrl);
        }
        // GET: Admin/AddUser
        [HttpGet]
        public ActionResult AddUser()
        {
            var roles = userProvider.GetAllRoles();

            ViewBag.Roles = new SelectList(roles);

            return View(new CreateUserViewModel());
        }


        // POST: Admin/AddUser
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUser(CreateUserViewModel model, string redirectUrl)
        {
            var allRoles = userProvider.GetAllRoles();
            var roleName = model.Roles.First();

            if (!ModelState.IsValid || !allRoles.Contains(roleName))
            {
                ViewBag.Roles = new SelectList(allRoles);
                return View(model);
            }
            var user = Mapper.Map<CreateUserViewModel, User>(model);
            try
            {
                userManager.CreateUser(user);
            }
            catch (CreateOrUpdateUserException ex)
            {
                ModelState.AddModelError("", ex.Message);
                ViewBag.Roles = new SelectList(allRoles);
                return View(model);
            }

            return RedirectToLocal(redirectUrl);
        }

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

        [AjaxOnly]
        [HttpPost]
        public ActionResult AddRole(string userName)
        {
            if (!string.IsNullOrEmpty(Request.Form["listOfMissingRoles"]))
            {
                var newRole = Request.Form["listOfMissingRoles"];
                var allRoles = userProvider.GetAllRoles();

                if (allRoles.Contains(newRole))
                {
                    userManager.AddToRole(userName, newRole);

                    var missingRoles = default(IReadOnlyList<string>);

                    var userAndRoles = GetUserAndRoles(userName, allRoles, out missingRoles);
                    ViewBag.listOfMissingRoles = new SelectList(missingRoles);
                    return PartialView("ListExistingRoles", userAndRoles);
                }
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "non-existent role");
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotFound, "please select role");
        }

        [AjaxOnly]
        public ActionResult DeleteRole(string userName, string roleName)
        {
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(roleName))
            {
                var allRoles = userProvider.GetAllRoles();
                if (allRoles.Contains(roleName))
                {
                    userManager.RemoveFromRoles(userName, roleName);

                    var missingRoles = default(IReadOnlyList<string>);

                    var userAndRoles = GetUserAndRoles(userName, allRoles, out missingRoles);
                    ViewBag.listOfMissingRoles = new SelectList(missingRoles);
                    return PartialView("ListExistingRoles", userAndRoles);
                }
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "non-existent role");
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotFound, "please select role");
        }

        private UserAndRoles GetUserAndRoles(string userName, IReadOnlyList<string> allRoles,
            out IReadOnlyList<string> missingRoles)
        {
            var userRoles = userProvider.GetUserByName(userName).Roles;

            missingRoles = (from role in allRoles
                            where !userRoles.Contains(role)
                            select role).ToList();

            return new UserAndRoles()
            {
                UserName = userName,
                Roles = userRoles
            };
        }

        private ActionResult RedirectToLocal(string redirectUrl)
        {
            if (Url.IsLocalUrl(redirectUrl))
            {
                return Redirect(redirectUrl);
            }
            return RedirectToAction("Index", "Home");
        }

    }
}