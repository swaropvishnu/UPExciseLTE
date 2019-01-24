using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UPExciseLTE.Models;

namespace UPExciseLTE.Filters
{
    public class UserAuditFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            string actionName = filterContext.ActionDescriptor.ActionName;
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var request = filterContext.HttpContext.Request;

            AuditTrail objaudit = new AuditTrail();

            if (HttpContext.Current.Session["tbl_Session"] == null)
            {
                objaudit.UserID = 0;
            }
            else
            {
                objaudit.UserID = Convert.ToInt64(UserSession.LoggedInUserId);
            }
            objaudit.UsersAuditID = 0;
            objaudit.SessionID = HttpContext.Current.Session.SessionID;
            objaudit.IPAddress = request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? request.UserHostAddress;
            objaudit.PageAccessed = request.RawUrl;
            objaudit.LoggedInAt = DateTime.Now;
            if (actionName == "logout")
            {
                objaudit.LoggedOutAt = DateTime.Now;
            }

            objaudit.LoginStatus = "A";
            objaudit.ControllerName = controllerName;
            objaudit.ActionName = actionName;

            //AllSampleCodeEntities context = new AllSampleCodeEntities();
            //context.AuditTBs.Add(objaudit);
            //context.SaveChanges();

            //Finishes executing the Action as normal 
            CMODataEntryBLL.InsertAudit(objaudit);
            base.OnActionExecuting(filterContext);


        }
    }
}