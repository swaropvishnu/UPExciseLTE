using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;

namespace UPExciseLTE
{
    public static class CommonConfig
    {
        public static string Conn()
        {
            try
            {
                
                string Comm = System.Configuration.ConfigurationManager.ConnectionStrings["constr"].ToString();
                return Comm;
            }
            catch(Exception e)
            {
                return e.ToString();
            }
        }
        public static string Conndb2()
        {
            string Comm = System.Configuration.ConfigurationManager.ConnectionStrings["conStrbe_unnao"].ToString();
            return Comm;
        }
    }
}