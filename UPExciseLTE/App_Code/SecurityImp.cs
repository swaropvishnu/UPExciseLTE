using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.Security;

namespace UPExciseLTE
{
    public class SecurityImp
    {
        string url = "http://jansunwai.up.nic.in/";
        public void Anti_Hijack()
        {
            HttpContext HttpC = HttpContext.Current;

            if (HttpC.Session["__Token"] == null | HttpC.Request.Cookies["__Token"] == null)
            {
                HttpC.Response.Redirect(Logout("IGRS"));
            }
            else if (HttpC.Session["__Token"].ToString() != HttpC.Request.Cookies["__Token"].Value)
            {
                HttpC.Response.Redirect(Logout("IGRS"));
            }
            else
            {
                HttpC.Session["__Token"] = GetMD5(GetRandomNo());
                HttpC.Request.Cookies.Remove("__Token");
                HttpC.Response.Cookies.Add(new HttpCookie("__Token", HttpC.Session["__Token"].ToString()));
            }
        }


        public string Logout(string p)
        {
            return url + "ErrorPageS.aspx";
        }

        public string GetMD5(string pwd)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(pwd.ToLower(), "MD5").ToLower();
        }

        public string GetRandomNo()
        {
            Random randomno = new Random();
            Int64 no = 0;
            no = randomno.Next(9, 999);

            return no.ToString();
        }
    }
}