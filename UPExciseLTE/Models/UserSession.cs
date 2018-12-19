using System;
using System.Web;


namespace UPExciseLTE.Models
{
    public class UserSession
    {
        protected static UserManager LoggedInUser
        {
            get
            {
                UserManager user = new UserManager();
                return user;
            }
        }

        /// <summary>
        /// Readonly property for returning logged in user's user Id
        /// </summary>
        public static int LoggedInUserId
        {
            get
            {
                return LoggedInUser.UserId;
            }
            set
            {
                LoggedInUser.UserId = value;
            }
        }

        /// <summary>
        /// Readonly property for returning logged in user's user name (login id)
        /// </summary>
        public static string LoggedInUserName
        {
            get
            {
                return LoggedInUser.UserName;
            }
            set
            {
                LoggedInUser.UserName = value;
            }
        }

        public static int LoggedInUserTypeLevel
        {
            get
            {
                return LoggedInUser.UserTypeLevel;
            }
            set
            {
                LoggedInUser.UserTypeLevel = value;
            }
        }

        public static string LoggedInUserType
        {
            get
            {
                return LoggedInUser.UserType;
            }
            set
            {
                LoggedInUser.UserType = value;
            }
        }

        public static string LoggedInUserLevelId
        {
            get
            {
                return LoggedInUser.UserLevel;
            }
            set
            {
                LoggedInUser.UserLevel = value;
            }
        }


        public static string LoggedInUserDepartmentName
        {
            get
            {
                return LoggedInUser.DepartmentName;
            }
            set
            {
                LoggedInUser.DepartmentName = value;
            }
        }



        //public static int LoggedInUserDepartmentId
        //{
        //    get
        //    {
        //        return LoggedInUser.DepartmentId;
        //    }
        //    set
        //    {
        //        LoggedInUser.DepartmentId = value;
        //    }
        //}

        //public static int LoggedInUserPostId
        //{
        //    get
        //    {
        //        return LoggedInUser.PostId;
        //    }
        //    set
        //    {
        //        LoggedInUser.PostId = value;
        //    }
        //}

        public static int LoggedInUserStateId
        {
            get
            {
                return LoggedInUser.UserStateId;
            }
            set
            {
                LoggedInUser.UserStateId = value;
            }
        }

        public static int LoggedInUserDivisionId
        {
            get
            {
                return LoggedInUser.UserDivisionId;
            }
            set
            {
                LoggedInUser.UserDivisionId = value;
            }
        }

        public static int LoggedInUserTehsilId
        {
            get
            {
                return LoggedInUser.UserTehsilId;
            }
            set
            {
                LoggedInUser.UserTehsilId = value;
            }
        }

        public static int LoggedInUserBlockId
        {
            get
            {
                return LoggedInUser.UserBlockId;
            }
            set
            {
                LoggedInUser.UserBlockId = value;
            }
        }


        public static int LoggedInUserThanaId
        {
            get
            {
                return LoggedInUser.UserThanaId;
            }
            set
            {
                LoggedInUser.UserThanaId = value;
            }
        }



        public static int LoggedInUserDistictId
        {
            get
            {
                return LoggedInUser.UserDistictId;
            }
            set
            {
                LoggedInUser.UserDistictId = value;
            }
        }

        public static string LoggedInName
        {
            get
            {
                return LoggedInUser.Name;
            }
            set
            {
                LoggedInUser.Name = value;
            }
        }


        //public static string LoggedInPostName
        //{
        //    get
        //    {
        //        return LoggedInUser.PostName;
        //    }
        //    set
        //    {
        //        LoggedInUser.PostName = value;
        //    }
        //}


        public static string Lastlogin
        {
            get
            {
                return LoggedInUser.Lastlogin;
            }
            set
            {
                LoggedInUser.Lastlogin = value;
            }
        }
        public static string PushName
        {
            get
            {
                return LoggedInUser.PushName;
            }
            set
            {
                LoggedInUser.PushName = value;
            }
        }
        public static string dbAddress
        {
            get
            {
                return LoggedInUser.dbAddress;
            }
            set
            {
                LoggedInUser.dbAddress = value;
            }
        }
        
        public static string LoggedInUserAccess
        {
            get
            {
                return LoggedInUser.UserType;
            }
            set
            {
                LoggedInUser.UserType = value;
            }
        }
        //public static string Sec_Code
        //{
        //    get
        //    {
        //        return LoggedInUser.Sec_Code;
        //    }
        //    set
        //    {
        //        LoggedInUser.Sec_Code = value;
        //    }
        //}

        //profilepic
        //public static string ProfilePhoto
        //{
        //    get
        //    {
        //        return LoggedInUser.profilephoto;
        //    }
        //    set
        //    {
        //        LoggedInUser.profilephoto = value;
        //    }
        //}
        //public static short yojanacode
        //{
        //    get
        //    {
        //        return LoggedInUser.yojanacode;
        //    }
        //    set
        //    {
        //        LoggedInUser.yojanacode = value;
        //    }
        //}
        //public short yojana_code { get; set; }

    }
}