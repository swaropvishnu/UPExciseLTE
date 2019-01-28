using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UPExciseLTE.Models;

namespace UPExciseLTE.App_Code
{
    public class ConnectionConfig
    {
        private string Connection { get; set; }
        private string dbName { get; set; }
        protected static string ConnectionName()
        {
            string str = "";
            if (HttpContext.Current.Session["tbl_Session"] != null)
            {
                return str;
            }
            return str;
        }
    }

}