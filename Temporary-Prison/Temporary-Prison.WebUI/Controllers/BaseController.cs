using log4net;
using StructureMap.Attributes;
using System.Web.Mvc;
using Temporary_Prison.Business;

namespace Temporary_Prison.Controllers
{
    public class BaseController : Controller
    {
       
          //  protected IPrisonProvider prisonProvider { get; set; }

           // [SetterProperty]
           /* public IPrisonProvider Provider
            {
                set { prisonProvider = value; }
            }
            */
           protected readonly ILog log = LogManager.GetLogger("LOGGER");
        
    }
}