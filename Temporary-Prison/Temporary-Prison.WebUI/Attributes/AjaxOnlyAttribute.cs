using System.Net;
using System.Reflection;
using System.Web.Mvc;

namespace Temporary_Prison.Attributes
{
    public class AjaxOnlyAttribute : ActionMethodSelectorAttribute
    {

        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            return controllerContext.HttpContext.Request.IsAjaxRequest();
        }
    }
}