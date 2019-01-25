using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using UPExciseLTE.Models;

namespace UPExciseLTE.Filters
{    
    public class ChkAuthorization : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            string url1 = Reverse(url);
            string[] arr = url1.Split('/');
            string menuAction = Reverse(arr[0]);
            if (menuAction.Contains("?"))
            {
                menuAction = menuAction.Substring(0, menuAction.IndexOf('?'));
            }
            string menuControlller = Reverse(arr[1]);
            int res = 0; bool chk = false;
            res = UserDtl.GetMenuValid(UserSession.LoggedInUserId, menuAction, HttpContext.Current.Request.UserHostAddress.ToString());
            if (res > 0)
                chk = true;
            else
            {
                if (!filterContext.HttpContext.Request.IsAjaxRequest())
                    chk = false;
                else
                    chk = true;
            }

            if (chk == false)
            {

                filterContext.Result =
               new RedirectToRouteResult(new RouteValueDictionary
            {
             { "action", "Logout" },
            { "controller", "Login" }
            //{ "returnUrl", filterContext.HttpContext.Request.RawUrl}  
             });

                return;
            }
        }

        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}