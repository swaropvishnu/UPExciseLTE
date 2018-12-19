using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Data;


namespace UPExciseLTE.Models
{
    public class UserManager
    {
        protected HttpSessionState session;

        //public UserManager(HttpSessionState httpSessionState)
        //{
        //    session = httpSessionState;
        //}

        public UserManager()
        {
            session = HttpContext.Current.Session;
        }

        //public void Dispose()
        //{
        //    session.Clear();
        //    session.Abandon();
        //}


        public DataTable tbl_Session
        {
            get
            {
                return
                    session["tbl_Session"] as DataTable;
            }
            set { session["tbl_Session"] = value; }
        }

        //public DataTable tbl_Session1
        //{
        //    get
        //    {
        //        return
        //        session["tbl_Session1"] as DataTable;
        //    }
        //    set
        //    {
        //        session["tbl_Session1"] = value;
        //    }
        //}

        public string TokenPage
        {
            get
            {
                return
                  HttpContext.Current.Session["__TokenPage"] as string;
            }
            set { HttpContext.Current.Session["__TokenPage"] = value; }
        }



        public string UserName
        {
            get
            {
                return
                    tbl_Session.Rows[0]["UserName"].ToString();
            }
            set { tbl_Session.Rows[0]["UserName"] = value; }
        }

        public int UserId
        {
            get
            {
                return
                   Convert.ToInt32(tbl_Session.Rows[0]["UserId"]);
            }
            set
            {
                tbl_Session.Rows[0]["UserId"] = value;
            }
        }

        public int UserTypeLevel
        {
            get
            {
                return
                   Convert.ToInt32(tbl_Session.Rows[0]["UserTypeLevel"]);
            }
            set
            {
                tbl_Session.Rows[0]["UserTypeLevel"] = value;
            }
        }

        public string UserType
        {
            get
            {
                return
                    tbl_Session.Rows[0]["UserType"].ToString();
            }
            set { tbl_Session.Rows[0]["UserType"] = value; }
        }
        public string UserLevel
        {
            get
            {
                return tbl_Session.Rows[0]["UserLevel"].ToString();
            }
            set { tbl_Session.Rows[0]["UserLevel"] = value; }
        }
        //public int DepartmentId
        //{
        //    get
        //    {
        //        return
        //           Convert.ToInt32(tbl_Session.Rows[0]["DepartmentId"]);
        //    }
        //    set
        //    {
        //        tbl_Session.Rows[0]["DepartmentId"] = value;
        //    }
        //}
        //public int PostId
        //{
        //    get
        //    {
        //        return
        //           Convert.ToInt32(tbl_Session.Rows[0]["PostId"]);
        //    }
        //    set
        //    {
        //        tbl_Session.Rows[0]["PostId"] = value;
        //    }
        //}
        public int UserStateId
        {
            get
            {
                return
                   Convert.ToInt32(tbl_Session.Rows[0]["StateId"]);
            }
            set
            {
                tbl_Session.Rows[0]["StateId"] = value;
            }
        }
        public int UserDivisionId
        {
            get
            {
                return
                   Convert.ToInt32(tbl_Session.Rows[0]["DivisionId"]);
            }
            set
            {
                tbl_Session.Rows[0]["DivisionId"] = value;
            }
        }
        public int UserDistictId
        {
            get
            {
                return
                   Convert.ToInt32(tbl_Session.Rows[0]["DistictId"]);
            }
            set
            {
                tbl_Session.Rows[0]["DistictId"] = value;
            }
        }
        public int UserTehsilId
        {
            get
            {
                return
                   Convert.ToInt32(tbl_Session.Rows[0]["TehshilId"]);
            }
            set
            {
                tbl_Session.Rows[0]["TehshilId"] = value;
            }
        }

        public int UserBlockId
        {
            get
            {
                return
                   Convert.ToInt32(tbl_Session.Rows[0]["BlockId"]);
            }
            set
            {
                tbl_Session.Rows[0]["BlockId"] = value;
            }
        }
        public int UserThanaId
        {
            get
            {
                return
                   Convert.ToInt32(tbl_Session.Rows[0]["ThanaID"]);
            }
            set
            {
                tbl_Session.Rows[0]["ThanaID"] = value;
            }
        }

        public string Name
        {
            get
            {
                return tbl_Session.Rows[0]["Name"].ToString();
            }
            set
            {
                tbl_Session.Rows[0]["Name"] = value;
            }
        }
        //public string PostName
        //{
        //    get
        //    {
        //        return
        //           tbl_Session.Rows[0]["PostName"].ToString();
        //    }
        //    set
        //    {
        //        tbl_Session.Rows[0]["PostName"] = value;
        //    }
        //}


        public string DepartmentName
        {
            get
            {
                return
                   tbl_Session.Rows[0]["DepName"].ToString();
            }
            set
            {
                tbl_Session.Rows[0]["DepName"] = value;
            }
        }

        public string Lastlogin
        {
            get
            {
                return tbl_Session.Rows[0]["LastloginDate"].ToString();
            }
            set
            {
                tbl_Session.Rows[0]["LastloginDate"] = value;
            }
        }
        public string PushName
        {
            get
            {
                return tbl_Session.Rows[0]["PushName"].ToString();
            }
            set
            {
                tbl_Session.Rows[0]["PushName"] = value;
            }
        }
        public string dbAddress
        {
            get
            {
                return tbl_Session.Rows[0]["dbAddress"].ToString();
            }
            set
            {
                tbl_Session.Rows[0]["dbAddress"] = value;
            }
        }
        //public string Sec_Code
        //{
        //    get
        //    {
        //        return tbl_Session.Rows[0]["Sec_Code"].ToString();
        //    }
        //    set
        //    {
        //        tbl_Session.Rows[0]["Sec_Code"] = value;
        //    }
        //}

        //public string Section
        //{
        //    get
        //    {
        //        return  tbl_Session.Rows[0]["Section"].ToString();
        //    }
        //    set
        //    {
        //        tbl_Session.Rows[0]["Section"] = value;
        //    }
        //}


        //public int OfficeId
        //{
        //    get
        //    {
        //        return Convert.ToInt32(tbl_Session.Rows[0]["OfficeID"].ToString());
        //    }
        //    set
        //    {
        //        tbl_Session.Rows[0]["OfficeID"] = value;
        //    }
        //}
        //profile pic
        //public string profilephoto
        //{
        //    get
        //    {

        //        return "data:image/png;base64," + Convert.ToBase64String((Byte[])(tbl_Session.Rows[0]["PhotoContent"]), 0);
        //    }
        //    set
        //    {
        //        string s = "data:image/png;base64," + Convert.ToBase64String((Byte[])(tbl_Session.Rows[0]["PhotoContent"]), 0);
        //           s = value;
        //    }
        //}

        //public short yojanacode
        //{
        //    get
        //    {
        //        return short.Parse(tbl_Session.Rows[0]["scheme_code"].ToString());
        //    }
        //    set
        //    {
        //        tbl_Session.Rows[0]["scheme_code"] = value;
        //    }
        //}



        //public string Password { get; set; }
    }
}