using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UPExciseLTE.Models;
using UPExciseLTE.DAL;
using System.Data;
using System.Web.Mvc;

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
        public static List<SelectListItem> fillBrewery()
        {
            List<SelectListItem> breweryList = new List<SelectListItem>();
            CMODataEntryBLL.bindDropDownHnGrid("proc_ddlDetail", breweryList, "BRE", UserSession.LoggedInUserId.ToString().Trim(), "Z");
            return breweryList;
        }
        public static List<SelectListItem> fillState()
        {
            List<SelectListItem> StateList = new List<SelectListItem>();
            CMODataEntryBLL.bindDropDownHnGrid("proc_ddlDetail", StateList, "STATE", "", "S");
            return StateList;
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
        #region MasterForm_Gaurav
        public List<BrandMaster> GetBrandList(int BrandId, string LiquorType, string LicenceType, string LicenseNo, short BreweryId, short DistrictCode, int TehsilCode, string Status)
        {
            List<BrandMaster> lstBrand = new List<BrandMaster>();
            DataSet ds = new CommonDA().GetBrandDetail(BrandId, LiquorType, LicenceType, LicenseNo, BreweryId, DistrictCode, TehsilCode, Status);
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            { 
               foreach (DataRow dr in ds.Tables[0].Rows)
               {
                   lstBrand.Add(FillBrand(dr));
               }
            }
            return lstBrand;
        }
        public BrandMaster GetBrand(int BrandId, string LiquorType, string LicenceType, string LicenseNo, short BreweryId, short DistrictCode, int TehsilCode, string Status)
        {
            DataSet ds = new CommonDA().GetBrandDetail(BrandId, LiquorType, LicenceType, LicenseNo, BreweryId, DistrictCode, TehsilCode, Status);
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                return FillBrand(ds.Tables[0].Rows[0]);
            }
            else
            {
                return new BrandMaster();
            }
         }
        private BrandMaster FillBrand(DataRow dr)
        {
            BrandMaster brand = new BrandMaster();
            brand.AdditionalDuty = float.Parse(dr["AdditionalDuty"].ToString().Trim());
            brand.BrandId = int.Parse(dr["BrandId"].ToString().Trim());
            brand.brandID_incrpt = new Crypto().Encrypt(dr["BrandId"].ToString().Trim());
            brand.BrandName = dr["BrandName"].ToString().Trim();
            brand.BrandRegistrationNumber = dr["BrandRegistrationNumber"].ToString().Trim();
            brand.BreweryId = short.Parse(dr["BreweryId"].ToString().Trim());
            brand.ExciseDuty = float.Parse(dr["ExciseDuty"].ToString().Trim());
            brand.LicenceNo = dr["LicenceNo"].ToString().Trim();
            brand.LicenceType = dr["LicenceType"].ToString().Trim();
            brand.LiquorType = dr["LiquorType"].ToString().Trim();
            brand.MRP = float.Parse(dr["MRP"].ToString().Trim());
            brand.QuantityInBottleML = int.Parse(dr["QuantityInBottleML"].ToString().Trim());
            brand.QuantityInCase = int.Parse(dr["QuantityInCase"].ToString().Trim());
            brand.Remark = dr["Remark"].ToString().Trim();
            brand.Strength = float.Parse(dr["Strength"].ToString().Trim());
            brand.XFactoryPrice = float.Parse(dr["XFactoryPrice"].ToString().Trim());
            brand.IsFinal = bool.Parse(dr["IsFinal"].ToString().Trim());
            brand.AlcoholType = "";
            brand.SPType = 2;
            return brand;
        }
        #endregion
    }
}