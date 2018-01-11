using log4net;
using System;

namespace Temporary_Prison
{
    public partial class MvcApplication : System.Web.HttpApplication
    {
        private static readonly ILog log = LogManager.GetLogger("LOGGER");

        void Application_Error(Object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError().GetBaseException() ;
            
            log.Error($"MvcApplication_Error \n Message: {ex.Message} \n StackTrace: {ex.StackTrace}");
        }
    }
}