using log4net;
using System;
using System.Web;

namespace Temporary_Prison.HttpModule
{
    public class ApplicationError : IHttpModule
    {
        private static readonly ILog log = LogManager.GetLogger("LOGGER");

        public void Dispose()
        {

        }

        public void Init(HttpApplication context)
        {
            context.Error += Application_Error;
        }

        private void Application_Error(object sender, EventArgs e)
        {
            var ex = HttpContext.Current.Server.GetLastError().GetBaseException();
            
            log.Error($"Application_Error \n Message: {ex.Message} \n StackTrace: {ex.StackTrace}");
        }
    }
}