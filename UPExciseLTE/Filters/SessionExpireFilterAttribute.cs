using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using UPExciseLTE.Models;



namespace UPExciseLTE.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class SessionExpireFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            // If the browser session or authentication session has expired...
            //if (ctx.Session["tbl_Session"] == null || !filterContext.HttpContext.Request.IsAuthenticated)
            if (ctx.Session["tbl_Session"] == null )
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Logout", controller = "Login" }));
                return;
            }
        }
    }

    public class CheckAuthorization : AuthorizeAttribute
    {
        private string pageName;
        private string ipaddress;
        #region Properties

        /// <span class="code-SummaryComment"><summary></span>
        /// Each page should "know" its name
        /// <span class="code-SummaryComment"></summary></span>
        public string PageName
        {
            get
            {
                return System.IO.Path.GetFileName(
                       System.Web.HttpContext.Current.Request.Url.AbsolutePath);
            } //eof get
            set { pageName = value; } //eof set
        } //eof property MyPageName

        #endregion //Properties

        public string IPAddress
        {
            get
            {
                return HttpContext.Current.Request.UserHostAddress.ToString();
            }
            set
            {
                ipaddress = value;
            }
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (HttpContext.Current.Session["UserID"] == null || !HttpContext.Current.Request.IsAuthenticated)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.HttpContext.Response.StatusCode = 302; //Found Redirection to another page. Here- login page. Check Layout ajaxError() script.
                    filterContext.HttpContext.Response.End();
                }
                else
                {
                    filterContext.Result = new RedirectResult(System.Web.Security.FormsAuthentication.LoginUrl + "?ReturnUrl=" +
                         filterContext.HttpContext.Server.UrlEncode(filterContext.HttpContext.Request.RawUrl));
                }
            }
            else
            {

                int res = 0;
                try
                {
                    if (ChkValidRequest())
                    {
                        res = UserDtl.GetMenuValid(UserSession.LoggedInUserId, PageName, IPAddress);
                        if (res == 0)
                        {

                            HttpContext.Current.Session.Abandon();
                            HttpContext.Current.Response.Redirect("~/logout.aspx?Info=Sorry you unauthorized  to view this page", true);
                        }
                    }
                    //if (!HttpContext.Current.User.Identity.IsAuthenticated || CRMSession.LoggedInUser == null)
                    if (UserSession.LoggedInUserName == null)
                    {
                        // this.RequestLogin();
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Logout", controller = "Login" }));
                        return;
                    }

                    else if (UserSession.LoggedInUserName != null)
                    {
                        res = UserDtl.GetMenuValid(UserSession.LoggedInUserId, PageName, IPAddress);
                        if (res == 0)
                        {

                            HttpContext.Current.Session.Abandon();
                            //HttpContext.Current.Response.Redirect("~/logout.aspx?Info=Sorry you unauthorized  to view this page", true);
                            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Logout", controller = "Login" }));
                            return;
                        }
                    }


                }
                catch (Exception ex)
                {
                    HttpContext.Current.Session.Abandon();
                    //HttpContext.Current.Response.Redirect("~/logout.aspx?Info=Sorry you unauthorized", true);
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Logout", controller = "Login" }));
                    return;
                }
                //base.OnPreInit(e);

            }
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
            if ((DomainURI.IndexOf("10.135.")) != -1 | (DomainURI.IndexOf("164.100.")) != -1 | (DomainURI.IndexOf("//localhost")) != -1 | (DomainURI.IndexOf("164.100.180.122")) != -1)
            {
                return true;
            }
            else
            {
                return false;
            }
            //return functionReturnValue;

        }


        public static int filter_bad_chars(string s)
        {
            string[] sL_Char = {
            "onfocus",          "\"\"",         "=",            "onmouseover",          "prompt",           "%27",          "alert#",           "alert",            "'or",
            "`or",          "`or`",         "'or'",         "'='",
            "`=`",
            "'",
            "`",
            "9,9,9",
            "src",
            "onload",
            "select",
            "drop",
            "insert",
            "delete",
            "xp_",
            "having",
            "union",
            "group",
            "update",
            "script",
            "<script",
            "</script>",
            "language",
            "javascript",
            "vbscript",
            "http",
            "~",
            "$",
            "<",
            ">",
            "%",
            "\\",
            ";",
            "@",
            "_",
            "{",
            "}",
            "^",
            "?",
            "[",
            "]",
            "!",
            "#",
            "&",
            "*"
        };
            int er = 0;
            int sL_Char_Length = sL_Char.Length - 1;
            while (sL_Char_Length >= 0)
            {
                if (s.Contains(sL_Char[sL_Char_Length]))
                {
                    er = 1;
                    break; // TODO: might not be correct. Was : Exit While
                }
                sL_Char_Length -= 1;
            }
            return er;
        }
        


    }
}