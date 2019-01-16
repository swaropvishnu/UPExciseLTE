using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using UPExciseLTE.Models;

namespace UPExciseLTE
{
    public static class CommonConfig
    {
        public static string Conn()
        {
            try
            {
                string Comm = "";
                try
                {
                     
                    //if (UserSession.dbAddress != null && UserSession.dbAddress.Trim() != String.Empty)
                    if (HttpContext.Current.Session["tbl_Session"]!=null )
                    {
                        string UName = "sa", Pass = "nic";
                        if (UserSession.dbAddress== "10.135.30.166")
                        {
                            UName = "sa"; Pass = "nic";
                        }                        
                        if (UserSession.dbAddress == "10.135.30.244")
                        {
                            UName = "sa"; Pass = "nic";
                        }
                         

                        Comm = "Data Source="+ UserSession.dbAddress + ";Initial Catalog=UM_Excise;user="+UName+"; password="+Pass+";";
                    }
                    else
                    {
                        Comm = System.Configuration.ConfigurationManager.ConnectionStrings["constr"].ToString();
                    }
                }
                catch {
                    //Comm = System.Configuration.ConfigurationManager.ConnectionStrings["constr"].ToString();
                }
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