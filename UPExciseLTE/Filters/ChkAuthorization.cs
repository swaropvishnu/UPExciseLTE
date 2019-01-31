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
            if (!menuAction.Contains("FinalizeFormFL"))
            {
                if (menuAction.Contains("?"))
                {
                    menuAction = menuAction.Substring(0, menuAction.IndexOf("?"));
                }
            }
            string menuControlller = Reverse(arr[1]);
            int res = 0; bool chk = false;
            res = UserDtl.GetMenuValid(UserSession.LoggedInUserId, menuAction, HttpContext.Current.Request.UserHostAddress.ToString());
            if (res > 0 && ChkValidRequest())
                chk = true;
            else
            {
                if (!filterContext.HttpContext.Request.IsAjaxRequest() )
                    chk = false;
                else
                    chk = true;
            }
           //chk true writter for Gaurav 27 jan 2019 for development
           //chk = true;
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


        public static bool ChkValidRequest()
        {
            //bool functionReturnValue = false;

            if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_REFERER"] == null)
            {
                return true;
                //return functionReturnValue;
            }

            if (string.IsNullOrEmpty(System.Web.HttpContext.Current.Request.ServerVariables["HTTP_REFERER"].ToString()))
            {
                return true;
                //return functionReturnValue;
            }

            string DomainURI = null;
            DomainURI = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_REFERER"].ToString();


            //if ((DomainURI.IndexOf("10.135.")) != -1 | (DomainURI.IndexOf("164.100.")) != -1 | (DomainURI.IndexOf("//localhost")) != -1 |  (DomainURI.IndexOf("164.100.180.13")) != -1)
            if ((DomainURI.IndexOf("10.135.")) != -1 | (DomainURI.IndexOf("164.100.")) != -1 | (DomainURI.IndexOf("//localhost")) != -1 )
            {
                return true;
            }
            else
            {
                return false;
            }
            //return functionReturnValue;

        }
    }
}