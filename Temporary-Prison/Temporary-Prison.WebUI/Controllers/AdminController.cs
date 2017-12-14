using System.Collections.Generic;
using System.Web.Mvc;
using Temporary_Prison.Business.Providers;
using Temporary_Prison.Common.Models;
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
        public ActionResult Index(int? page, string search, string currentFilter)
        {
         
                int pageNum = 1;
                int pageSize= 5;

                if (search != null)
                {
                    pageNum = 1;
                }
                else
                {
                    if (currentFilter != null)
                    {
                        search = currentFilter;
                        pageNum = page ?? 1;
                    }
                    else
                    {
                        search = "";
                        pageNum = page ?? 1;
                    }
                }

                ViewBag.CurrentFilter = search;

                List<User> listUsers = new List<User>() { new User {UserName = "gen" } };
                
                return View(listUsers.ToPagedList(pageNum,pageSize));
           
        }
    }
}