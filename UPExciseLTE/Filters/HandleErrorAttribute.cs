using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UPExciseLTE.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class HandleErrorAttribute : FilterAttribute//, IExceptionFilter
    {
        //protected override void OnException(ExceptionContext filterContext)
        //{
        //    string action = filterContext.RouteData.Values["action"].ToString();
        //    Exception e = filterContext.Exception;
        //    filterContext.ExceptionHandled = true;
        //    filterContext.Result = new ViewResult()
        //    {
        //        ViewName = "Error"
        //    };
        //}
    }
}