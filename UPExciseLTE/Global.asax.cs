using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using UPExciseLTE.Controllers;
using UPExciseLTE.Filters;

namespace UPExciseLTE
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalFilters.Filters.Add(new UserAuditFilter()); // Register UserAuditFilter
            
        }

        

        void Application_Error(object sender, EventArgs e)
        {
            //Code that runs when an unhandled error occurs
            //Exception ex = default(Exception);

            //ex = Server.GetLastError().InnerException;
            //if (ex != null)
            //{ 
            //    ex = Server.GetLastError().InnerException;
            //    CMODataEntryBLL.InsertErrLog(Request.Url.ToString(), ex.ToString());
            //    //HttpContext.Current.Response.Redirect("~/ErrorPageS.aspx",false);
            //    //HttpContext.Current.Response.Redirect("~/ErrorPageS.aspx", false);
            //}
            //Server.ClearError();

            //New Comment By Gaurav 04-12-2018 for Check Error

            HttpContext httpContext = HttpContext.Current;
            if (httpContext != null)
            {
                RequestContext requestContext = ((MvcHandler)httpContext.CurrentHandler).RequestContext;
                if (requestContext.HttpContext.Request.IsAjaxRequest())
                {
                    httpContext.Response.Clear();
                    string controllerName = requestContext.RouteData.GetRequiredString("controller");
                    IControllerFactory factory = ControllerBuilder.Current.GetControllerFactory();
                    IController controller = factory.CreateController(requestContext, controllerName);
                    ControllerContext controllerContext = new ControllerContext(requestContext, (ControllerBase)controller);

                    JsonResult jsonResult = new JsonResult
                    {
                        Data = new { success = false, serverError = "500" },
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                    jsonResult.ExecuteResult(controllerContext);
                    httpContext.Response.End();
                }
                else
                {
                    //Code that runs when an unhandled error occurs
                    
                    Exception ex = Server.GetLastError();
                   
                    CMODataEntryBLL.InsertErrLog(Request.Url.ToString(), ex.ToString());
                   
                    Response.Clear();
                    Server.ClearError();

                    var routeData = new RouteData();
                    routeData.Values["controller"] = "Login";
                    routeData.Values["action"] = "Error";
                    Response.StatusCode = 500;

                    IController controller = new LoginController();
                    var rc = new RequestContext(new HttpContextWrapper(Context), routeData);
                    controller.Execute(rc);
                }
            }
        }


        protected void Session_Start(Object sender, EventArgs e)
        {
            //Session["startValue"] = 0;
        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.
            HttpContext.Current.Response.Redirect("~/Login/Login");
        }

    }
}
