using AutoMapper;
using System.Collections.Generic;
using System.Web.Mvc;
using Temporary_Prison.Business.Providers;
using Temporary_Prison.Common.Models;
using Temporary_Prison.Models;
using X.PagedList;

namespace Temporary_Prison.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserProvider userProvider;

        public AdminController(IUserProvider userProvider)
        {
            this.userProvider = userProvider;
        }

        // GET: Admin/index
        [Authorize(Roles = "Admin")]
        public ActionResult Index(int? page, string search, string currentFilter,int? currentTotal)
        {
            const int pageSize = 4;
            var totalCount = default(int);
            int _currentTotal = currentTotal ?? default(int);
            ViewBag.RedirectUrl = Url.Action("Index","Admin");


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

            var listUsers = userProvider.GetUsersForPagedList(skip, pageSize, ref totalCount);
            var usersPagedList = new StaticPagedList<User>(listUsers, pageNum, pageSize, totalCount);

            return View(usersPagedList);
        }

        // GET: /Admin/EditUser
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult EditUser(string userName,string redirectUrl)
        {
            var userModel = default(UserViewModel);
            var user = userProvider.GetUserByName(userName);
            if (user == null)
            {
                RedirectToLocal(redirectUrl);
            }
            userModel = Mapper.Map<User, UserViewModel>(user);

            return View(userModel);
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