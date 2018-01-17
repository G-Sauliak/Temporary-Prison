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
using Temporary_Prison.Dependencies.MapperRegistry;
using Temporary_Prison.WebMapperProfile;
using Temporary_Prison.Models;
using X.PagedList;
using Temporary_Prison.Business.SiteConfigService;
using System.Web;
using Temporary_Prison.Business.Extensions;
using System;
using System.Drawing;
using System.IO;

namespace Temporary_Prison.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IUserProvider userProvider;
        private readonly ILog log = LogManager.GetLogger("LOGGER");
        private readonly IUserManager userManager;
        private readonly IConfigService siteConfigService;

        public AdminController() : this(new UserProvider(), new UserManager(), new ConfigService())
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new WebMapper());
            });
        }

        public AdminController(IUserProvider userProvider, IUserManager userManager, IConfigService siteConfigService)
        {
            this.userProvider = userProvider;
            this.userManager = userManager;
            this.siteConfigService = siteConfigService;
        }

        // GET: Admin/index
        public ActionResult Index(int? page, int? currentTotal)
        {
            var pageSize = siteConfigService.UserPagedSize;
            var _currentTotal = currentTotal ?? default(int);

            var pageNum = page ?? 1;
            var skip = (pageNum - 1) * pageSize;

            ViewBag.RedirectUrl = Url.Action("Index", "Admin");

            var listUsers = userProvider.GetUsersForPagedList(skip, pageSize, ref _currentTotal);

            if (listUsers != null)
            {
                var usersPagedList = new StaticPagedList<User>(listUsers, pageNum, pageSize, _currentTotal);
                return View(usersPagedList);
            }
            return View(default(StaticPagedList<User>));
        }

        // GET: /Admin/EditUser
        public ActionResult EditUser(string userName)
        {
            ViewBag.RedirectUrl = Url.Action("Index", "Admin");

            if (string.IsNullOrEmpty(userName))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = userProvider.GetUserByName(userName);

            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            var userEditModel = Mapper.Map<User, EditUserViewModel>(user);

            var userAdnRoles = userManager.GetUserAndRoles(userName);

            var userAndRolesViewModel = Mapper.Map<UserAndRoles, UserAndRolesViewModel>(userAdnRoles);
            userEditModel.UserAndRoles = userAndRolesViewModel;

            var missingRoles = userManager.GetMissingRoles(userName);
            ViewBag.listOfMissingRoles = new SelectList(missingRoles);

            return View(userEditModel);
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
                ModelState.AddModelError(string.Empty, ce.Message);
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
                ModelState.AddModelError(string.Empty, ex.Message);
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
        public ActionResult AddRole(FormCollection form, string userName)
        {
            var newRole = form["listOfMissingRoles"];

            if (!string.IsNullOrEmpty(newRole))
            {
                var allRoles = userProvider.GetAllRoles();

                if (allRoles.Contains(newRole))
                {
                    userManager.AddToRole(userName, newRole);

                    var missingRoles = userManager.GetMissingRoles(userName);
                    var userAndRoles = userManager.GetUserAndRoles(userName);

                    var userAndRolesViewModel = Mapper.Map<UserAndRoles, UserAndRolesViewModel>(userAndRoles);

                    ViewBag.listOfMissingRoles = new SelectList(missingRoles);

                    return PartialView("ListExistingRoles", userAndRolesViewModel);
                }
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "non-existent role");
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, " please select role");
        }

        [AjaxOnly]
        public ActionResult DeleteRole(string userName, string roleName)
        {
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(roleName))
            {
                var user = userProvider.GetUserByName(userName);

                if (user.Roles.Contains(roleName))
                {
                    userManager.RemoveFromRoles(userName, roleName);

                    var missingRoles = userManager.GetMissingRoles(userName);
                    var userAndRoles = userManager.GetUserAndRoles(userName);

                    var userAndRolesViewModel = Mapper.Map<UserAndRoles, UserAndRolesViewModel>(userAndRoles);

                    ViewBag.listOfMissingRoles = new SelectList(missingRoles);

                    return PartialView("ListExistingRoles", userAndRolesViewModel);
                }
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "non-existent role");
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Please select role");
        }

        private ActionResult RedirectToLocal(string redirectUrl)
        {
            if (Url.IsLocalUrl(redirectUrl))
            {
                return Redirect(redirectUrl);
            }
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public ActionResult SiteConfig()
        {
            ViewBag.CurrentEdit = "Site";

            var model = Mapper.Map<ConfigService, SiteConfigViewModel>(siteConfigService as ConfigService);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SiteConfig(SiteConfigViewModel model, HttpPostedFileBase postPhotoImage, HttpPostedFileBase postAvatarImage)
        {
            if (ModelState.IsValid)
            {
                var newDefaultAvatarPath = default(string);
                var newDefaulPhotoOfPriosnersPath = default(string);

                if (!string.IsNullOrEmpty(model.AvatarPath) && model.AvatarPath != siteConfigService.AvatarPath)
                {
                    newDefaultAvatarPath = Server.MapPath($"~/{siteConfigService.ContentPath}/{model.AvatarPath}");
                    if (!Directory.Exists(newDefaultAvatarPath))
                    {
                        Directory.CreateDirectory(newDefaultAvatarPath);
                    }
                }
                if (!string.IsNullOrEmpty(model.PhotoPath) && model.PhotoPath != siteConfigService.PhotoPath)
                {
                    newDefaulPhotoOfPriosnersPath = Server.MapPath($"~/{siteConfigService.ContentPath}/{model.PhotoPath}");
                    if (!Directory.Exists(newDefaulPhotoOfPriosnersPath))
                    {
                        Directory.CreateDirectory(newDefaulPhotoOfPriosnersPath);
                    }
                }

                try
                {

                    if (postAvatarImage != null && postAvatarImage.ContentLength > 0)
                    {
                        var photoExtensions = Path.GetExtension(postAvatarImage.FileName);
                        var avatarName = string.Concat(postAvatarImage.FileName, photoExtensions);
                        if (avatarName != siteConfigService.DefaultNoAvatar)
                        {
                            var newDefaultAvatar = Image.FromStream(postAvatarImage.InputStream);

                            if (ImageHelper.IsSupportedFormat(postAvatarImage, model.AllowedPhotoTypes ?? siteConfigService.AllowedPhotoTypes))
                            {
                                var avatarSavePath = Path
                                 .Combine(Server.MapPath($"~/{siteConfigService.ContentPath}/{newDefaultAvatarPath ?? siteConfigService.AvatarPath}"), avatarName);

                                newDefaultAvatar.ResizeProportional(new Size( 
                                (model.AvatarWidth.HasValue)
                                ? model.AvatarWidth.Value
                                : siteConfigService.AvatarWidth,
                                (model.AvatarHeight.HasValue)
                                ? model.AvatarHeight.Value
                                : siteConfigService.AvatarHeight
                                ))
                                .SaveToFolder(avatarSavePath);
                                model.DefaultNoAvatar = avatarName;
                            }
                        }
                    }
                }
                catch (ArgumentException ae)
                {
                    ModelState.AddModelError(string.Empty, "incorrect file");
                }
              
                Mapper.Map<SiteConfigViewModel, ConfigService>(model);

                Redirect("SiteConfig");
            }
            return View(model);
        }
    }
}