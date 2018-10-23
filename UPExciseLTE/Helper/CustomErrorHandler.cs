using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;

namespace UPExciseLTE.Helper
{
    public class CustomErrorHandler : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            Exception ex = default(Exception);
            
            //ex = GetLastError().InnerException;
            //CMODataEntryBLL.InsertErrLog(HttpContext.Current.Request.Url.ToString(), ex.ToString());

            //var Logger = log4net.LogManager.GetLogger("SomeLoggerHere");
            //Logger.LogException(filterContext.Exception.StackTrace);
            //filterContext.ExceptionHandled = true;
            //filterContext.HttpContext.Response.Clear();
            //if (filterContext.HttpContext.Session != null) filterContext.HttpContext.Session.RemoveAll();
            //// set this to true so that IIS 7 does not use its own error pages
            //filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;

        }
    }
}