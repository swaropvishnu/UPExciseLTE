using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using UPExciseLTE.Controllers;

namespace UPExciseLTE
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
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
                    //Exception ex = default(Exception);

                    //ex = Server.GetLastError().InnerException;
                    //if (ex != null)
                    //{
                    string ex = httpContext.Error.Message;
                    CMODataEntryBLL.InsertErrLog(Request.Url.ToString(), ex);
                    //}
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
            Session["startValue"] = 0;
        }
        //        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        //        {
        //            // Extract the forms authentication cookie
        //            string cookieName = FormsAuthentication.FormsCookieName;
        //            HttpCookie authCookie = Context.Request.Cookies[cookieName];
        //            if (null == authCookie)
        //            {
        //                // There is no authentication cookie.
        //                return;
        //            }
        //            FormsAuthenticationTicket authTicket = null;
        //            try
        //            {
        //                authTicket = FormsAuthentication.Decrypt(authCookie.Value);
        //            }
        //            catch (Exception ex)
        //            {
        //                // Log exception details (omitted for simplicity)
        //                return;
        //            }
        //            if (null == authTicket)
        //            {
        //                // Cookie failed to decrypt.
        //                return;
        //            }
        //            // When the ticket was created, the UserData property was assigned
        //            // a pipe delimited string of role names.
        //            string[2] roles
        //        roles[0] = "One"
        //        roles[1] = "Two"
        //        // Create an Identity object
        //FormsIdentity id = new FormsIdentity(authTicket);
        //            // This principal will flow throughout the request.
        //            GenericPrincipal principal = new GenericPrincipal(id, roles);
        //            // Attach the new principal object to the current HttpContext object
        //            Context.User = principal;
        //        }
    }
}
