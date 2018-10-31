using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UPExciseLTE.Models;
using UPExciseLTE.DAL;
using System.Data;

namespace UPExciseLTE.BLL
{
    public class CommonBL
    {
         
        public static DateTime Setdate(string Cdate)
        {
            char[] a = { ',' };
            string[] date = Cdate.Split('/');
            DateTime dt = new DateTime(int.Parse(date[2].ToString()), int.Parse(date[1].ToString()), int.Parse(date[0].ToString()));
            return dt;
        }

        public static LoginModal GetUserDetail(LoginModal objUserData)
        {
            try
            {
                CommonDA CommonDA = new CommonDA();
                return CommonDA.GetUserDetail(objUserData);
            }
            catch
            {
                throw;
            }
        }

        public static String UpdateUserDetail(LoginModal objUserData)
        {
            try
            {
                CommonDA CommonDA = new CommonDA();
                return CommonDA.UpdateUserDetail(objUserData);
            }
            catch
            {
                throw;
            }
        }
    }
}