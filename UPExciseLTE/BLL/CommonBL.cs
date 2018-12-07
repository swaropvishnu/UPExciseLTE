﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UPExciseLTE.Models;
using UPExciseLTE.DAL;
using System.Data;
using System.Web.Mvc;
using System.Text;

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
        public static List<SelectListItem> fillBreweryLiceName()
        {
            List<SelectListItem> breweryList = new List<SelectListItem>();
            CMODataEntryBLL.bindDropDownHnGrid("proc_ddlDetail", breweryList, "BLN", UserSession.LoggedInUserId.ToString().Trim(), "Z");
            return breweryList;
        }
        public static List<SelectListItem> fillState(string SelectType)
        {
            List<SelectListItem> StateList = new List<SelectListItem>();
            CMODataEntryBLL.bindDropDownHnGrid("proc_ddlDetail", StateList, "STATE", "", SelectType);
            return StateList;
        }
        public static List<SelectListItem> fillDistict(string SelectType, long districtId1 = 0, long districtId2 = 0)
        {
            List<SelectListItem> StateList = new List<SelectListItem>();
            CMODataEntryBLL.bindDropDownHnGrid("proc_ddlDetail", StateList, "DIST", "", SelectType, districtId1, districtId2);
            return StateList;
        }

        public static List<SelectListItem> fillShops(string SelectType)
        {
            List<SelectListItem> StateList = new List<SelectListItem>();
            CMODataEntryBLL.bindDropDownHnGrid("proc_ddlDetail", StateList, "SHOP", "", SelectType);
            return StateList;
        }

        public static List<SelectListItem> fillLiceseeLicenseNos(string SelectType)
        {
            List<SelectListItem> LicenseeNosList = new List<SelectListItem>();
            CMODataEntryBLL.bindDropDownHnGrid("proc_ddlDetail", LicenseeNosList, "LicenseNo", "", SelectType);
            return LicenseeNosList;
        }


        public static List<SelectListItem> fillBrand(string SelectType)
        {
            List<SelectListItem> BrandList = new List<SelectListItem>();
            CMODataEntryBLL.bindDropDownHnGrid("proc_ddlDetail", BrandList, "BR", UserSession.LoggedInUserId.ToString(), SelectType);
            return BrandList;
        }

        public static List<SelectListItem> fillLicenseTypes(string SelectType, string Type)
        {
            List<SelectListItem> liceseList = new List<SelectListItem>();
            CMODataEntryBLL.bindDropDownHnGrid("proc_ddlDetail", liceseList, Type, UserSession.PushName.ToString(), SelectType);
            return liceseList;
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
        public List<BrandMaster> GetBrandList(int BrandId, string LiquorType, string LicenceType, string LicenseNo, short BreweryId, short DistrictCode, int StateId, string Status)
        {
            List<BrandMaster> lstBrand = new List<BrandMaster>();
            DataSet ds = new CommonDA().GetBrandDetail(BrandId, LiquorType, LicenceType, LicenseNo, BreweryId, DistrictCode, StateId, Status);
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
            try
            {
                brand.BrandId = int.Parse(dr["BrandId"].ToString().Trim());
                brand.brandID_incrpt = new Crypto().Encrypt(dr["BrandId"].ToString().Trim());
                brand.BreweryId = short.Parse(dr["BreweryId"].ToString().Trim());
                brand.BreweryName = dr["BreweryName"].ToString().Trim();
                brand.StateId = int.Parse(dr["BrandId"].ToString().Trim());
                brand.StateName = dr["StateName"].ToString().Trim();
                brand.BrandName = dr["BrandName"].ToString().Trim();
                brand.BrandRegistrationNumber = dr["BrandRegistrationNumber"].ToString().Trim();
                brand.Strength = float.Parse(dr["Strength"].ToString().Trim());
                brand.AlcoholType = dr["AlcoholType"].ToString().Trim();
                brand.LiquorType = dr["LiquorType"].ToString().Trim();
                brand.LicenceType = dr["LicenceType"].ToString().Trim();
                brand.LicenceNo = dr["LicenceNo"].ToString().Trim();
                brand.ExciseTin = dr["ExciseTin"].ToString().Trim();
                brand.XFactoryPrice = float.Parse(dr["XFactoryPrice"].ToString().Trim());
                brand.ConsiderationFees = float.Parse(dr["ConsiderationFees"].ToString().Trim());
                brand.WHMargin = float.Parse(dr["WHMargin"].ToString().Trim());
                brand.WHPrice = float.Parse(dr["WHPrice"].ToString().Trim());
                brand.RetMargin = float.Parse(dr["RetMargin"].ToString().Trim());
                brand.MaxRetPrice = float.Parse(dr["MaxRetPrice"].ToString().Trim());
                brand.AdditionalDuty = float.Parse(dr["AdditionalDuty"].ToString().Trim());
                brand.OriginalRetPrice = float.Parse(dr["OriginalRetPrice"].ToString().Trim());
                brand.ExciseDuty = float.Parse(dr["ExciseDuty"].ToString().Trim());
                brand.MRP = float.Parse(dr["MRP"].ToString().Trim());
                brand.QuantityInCase = int.Parse(dr["QuantityInCase"].ToString().Trim());
                brand.QuantityInBottleML = int.Parse(dr["QuantityInBottleML"].ToString().Trim());
                brand.PackagingType = dr["PackagingType"].ToString().Trim();
                brand.Remark = dr["Remark"].ToString().Trim();
                brand.Reason = dr["Reason"].ToString().Trim();
                brand.SPType = 2;
                brand.BrandStatus = dr["BrandStatus"].ToString().Trim();
                brand.Status = dr["Status1"].ToString().Trim();
            }
            catch (Exception exp) { }
            return brand;
        }
        public List<BottelingPlan> GetBottelingPlanList(DateTime FromDate, DateTime ToDate, short BreweryId, int BrandId, string Mapped, string BatchNo, int PlanId, string Status)
        {
            List<BottelingPlan> lstPlan = new List<BottelingPlan>();
            DataSet ds = new CommonDA().GetBottelingPlanDetail(FromDate, ToDate, BreweryId, BrandId, Mapped, BatchNo, PlanId, Status);
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    lstPlan.Add(FillBottlingPlan(dr));
                }
            }
            return lstPlan;
        }
        private BottelingPlan FillBottlingPlan(DataRow dr)
        {
            BottelingPlan Plan = new BottelingPlan();
            try
            {
                Plan.PlanId = int.Parse(dr["PlanId"].ToString().Trim());
                Plan.EncPlanId = new Crypto().Encrypt(dr["PlanId"].ToString().Trim());
                Plan.DateOfPlan1 = (dr["DateOfPlan1"].ToString().Trim());
                Plan.LiquorType = dr["LiquorType"].ToString().Trim();
                Plan.LicenceType = dr["LicenceType"].ToString().Trim();
                Plan.Brand = dr["BrandName"].ToString().Trim();
                Plan.NumberOfCases = int.Parse(dr["NumberOfCases"].ToString().Trim());
                Plan.BulkLiter = float.Parse(dr["BulkLiter"].ToString().Trim());
                Plan.Status = dr["Status"].ToString().Trim();
                Plan.BrandId = int.Parse(dr["BrandId"].ToString().Trim());
                Plan.MappedOrNot = short.Parse(dr["MappedOrNot"].ToString().Trim());
                Plan.BatchNo = (dr["BatchNo"].ToString().Trim());
                Plan.LiquorType = (dr["Liquor"].ToString().Trim());
                Plan.LicenceType = (dr["LicenceType"].ToString().Trim());
                Plan.LicenseNo = (dr["LicenceNo"].ToString().Trim());
                Plan.State = (dr["state_name_u"].ToString().Trim());
                Plan.BottleCapacity = (dr["QuantityInBottle"].ToString().Trim());
                Plan.Strength = (dr["Strength"].ToString().Trim());
                Plan.TotalUnitQuantity = int.Parse((dr["TotalUnit"].ToString().Trim()));
                Plan.IsQRGenerated = bool.Parse(dr["IsQRGenerated"].ToString().Trim());
                Plan.QunatityInCaseExport = int.Parse(dr["BoxQuantity"].ToString().Trim());
                Plan.ProducedNumberOfCases = int.Parse(dr["ProducedNumberOfCases"].ToString().Trim());
                Plan.ProducedBoxQuantity = float.Parse(dr["ProducedBoxQuantity"].ToString().Trim());
                Plan.ProducedBulkLiter = float.Parse(dr["ProducedBulkLiter"].ToString().Trim());
                Plan.ProducedTotalUnit = int.Parse(dr["ProducedTotalUnit"].ToString().Trim());
                Plan.WastageInNumber = int.Parse(dr["WastageInNumber"].ToString().Trim());
                Plan.WastageBL = float.Parse(dr["WastageBL"].ToString().Trim());
                Plan.IsProductionFinal = short.Parse(dr["IsProductionFinal"].ToString().Trim());
                Plan.TotalRevenue = dr["TotalRevenue"].ToString().Trim();
                Plan.Type = 2;
                Plan.BBTId = int.Parse(dr["BBTID"].ToString().Trim());
                Plan.BBTBulkLiter = float.Parse(dr["BBTBulkLiter"].ToString().Trim());
            }
            catch (Exception) { }
            return Plan;
        }
        public BottelingPlan GetBottelingPlan(DateTime FromDate, DateTime ToDate, short BreweryId, int BrandId, string Mapped, string BatchNo, int PlanId, string Status)
        {

            DataSet ds = new CommonDA().GetBottelingPlanDetail(FromDate, ToDate, BreweryId, BrandId, Mapped, BatchNo, PlanId, Status);
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                return FillBottlingPlan(ds.Tables[0].Rows[0]);
            }
            else
            {
                return new BottelingPlan();
            }
        }
        public List<UnitTank> GetUnitTankDetails(short BreweryId, short UnitTankId, string status)
        {
            List<UnitTank> lstUnitTank = new List<UnitTank>();
            DataSet ds = new CommonDA().GetUnitTank(BreweryId, UnitTankId, status);
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    lstUnitTank.Add(FillUnitTank(dr));
                }
            }
            return lstUnitTank;
        }
        public List<BottlingLine> GetBottlingLineDetails(short BreweryId, int BBTID, int LineId, string status)
        {
            List<BottlingLine> lstUnitTank = new List<BottlingLine>();
            DataSet ds = new CommonDA().GetBottlingLine(BreweryId, BBTID, LineId, status);
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    lstUnitTank.Add(FillBottlingLine(dr));
                }
            }
            return lstUnitTank;
        }
        public UnitTank GetUnitTank(short BreweryId, short UnitTankId, string status)
        {
            DataSet ds = new CommonDA().GetUnitTank(BreweryId, UnitTankId, status);
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                return FillUnitTank(ds.Tables[0].Rows[0]);
            }
            else
            {
                return new UnitTank();
            }
        }
        public BottlingLine GetBottlingLine(short BreweryId, int BBTID, int LineId, string status)
        {
            DataSet ds = new CommonDA().GetBottlingLine(BreweryId, BBTID, LineId, status);
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                return FillBottlingLine(ds.Tables[0].Rows[0]);
            }
            else
            {
                return new BottlingLine();
            }
        }
        private BottlingLine FillBottlingLine(DataRow dr)
        {
            BottlingLine RW = new BottlingLine();
            try
            {
                RW.BottlingLineId = int.Parse(dr["BottlingLineId"].ToString().Trim());
                RW.BottlingLineName = (dr["BottlingLineName"].ToString().Trim());
                RW.BottlingLineStatus = (dr["BottlingLineStatus"].ToString().Trim());
                RW.BBTId = int.Parse((dr["BBTId"].ToString().Trim()));
                RW.BBT = dr["BBTName"].ToString().Trim();
                RW.UnitId = short.Parse((dr["UnitId"].ToString().Trim()));
                RW.Type = 2;
                RW.EncBottlingLineId = new Crypto().Encrypt(dr["BottlingLineId"].ToString().Trim());
            }
            catch (Exception) { }
            return RW;
        }
        private UnitTank FillUnitTank(DataRow dr)
        {
            UnitTank UT = new UnitTank();
            try
            {
                UT.UnitTankId = int.Parse(dr["UnitTankId"].ToString().Trim());
                UT.BreweryId = short.Parse(dr["BreweryId"].ToString().Trim());
                UT.UnitTankName = (dr["UnitTankName"].ToString().Trim());
                UT.UnitTankCapacity = float.Parse(dr["UnitTankCapacity"].ToString().Trim());
                UT.UnitTankBulkLiter = float.Parse(dr["UnitTankBulkLiter"].ToString().Trim());
                UT.UnitTankStrength = float.Parse(dr["UnitTankStrength"].ToString().Trim());
                UT.Enc_UnitTankId = new Crypto().Encrypt(dr["UnitTankId"].ToString().Trim());
                UT.Status = (dr["Status"].ToString().Trim());
                UT.Type = 2;
            }
            catch (Exception) { }
            return UT;
        }
        public static List<SelectListItem> fillUnitTank(string Select)
        {
            List<SelectListItem> breweryList = new List<SelectListItem>();
            CMODataEntryBLL.bindDropDownHnGrid("proc_ddlDetail", breweryList, "UT", UserSession.LoggedInUserId.ToString().Trim(), Select);
            return breweryList;
        }
        public static List<SelectListItem> fillBBT(string Select)
        {
            List<SelectListItem> breweryList = new List<SelectListItem>();
            CMODataEntryBLL.bindDropDownHnGrid("proc_ddlDetail", breweryList, "BBT", UserSession.LoggedInUserId.ToString().Trim(), Select);
            return breweryList;
        }
        public static List<SelectListItem> BottlingLine(string Select, string BBTID)
        {
            List<SelectListItem> breweryList = new List<SelectListItem>();
            CMODataEntryBLL.bindDropDownHnGrid("proc_ddlDetail", breweryList, "BL", BBTID, Select);
            return breweryList;
        }
        public static List<SelectListItem> EmptyDDl(string Select)
        {
            List<SelectListItem> breweryList = new List<SelectListItem>();
            breweryList.Insert(0, new SelectListItem { Text = "--Select--", Value = "-1" });
            return breweryList;
        }
        public string GetGatePassUploadBrandDetailsTable(long GatePassId)
        {
            StringBuilder sb = new StringBuilder();
            DataSet ds = new CommonDA().GetGatePassUploadBrandDetails(GatePassId);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    sb.Append("<div class='row'><table class='table table-striped table-bordered table-hover'><tr><th>Srno</th><th>BrandName</th><th>BatchNo</th><th>DetailDesc</th><th>TotalBL</th><th>Strength</th></tr>");
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>"); sb.Append(dr["Srno"]); sb.Append("</td>");
                        sb.Append("<td>"); sb.Append(dr["BrandName"]); sb.Append("</td>");
                        sb.Append("<td>"); sb.Append(dr["BatchNo"]); sb.Append("</td>");
                        sb.Append("<td>"); sb.Append(dr["DetailDesc"]); sb.Append("</td>");
                        sb.Append("<td>"); sb.Append(dr["TotalBL"]); sb.Append("</td>");
                        sb.Append("<td>"); sb.Append(dr["Strength"]); sb.Append("</td>");
                        sb.Append("</tr>");
                    }


                }
            }

            return sb.ToString();
        }
        public GatePassBrandMapping GetGatePassBrandDetails(long GatePassId)
        {
            GatePassBrandMapping GPBM = new GatePassBrandMapping();
            DataSet ds = new CommonDA().GetGatePassUploadBrandDetails(GatePassId);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GPBM=FillGatePassBrandDetails(ds.Tables[0].Rows[0]);
                }
            }
            return GPBM;
        }
        public GatePassBrandMapping FillGatePassBrandDetails(DataRow dr)
        {
            GatePassBrandMapping GPBM = new GatePassBrandMapping();
            GPBM.Srno = short.Parse(dr["Srno"].ToString().Trim());
            GPBM.BatchNo = (dr["BatchNo"].ToString().Trim());
            GPBM.BrandName = (dr["BrandName"].ToString().Trim());
            GPBM.DetailsDesc = (dr["DetailDesc"].ToString().Trim());
            GPBM.Strength = decimal.Parse((dr["Strength"].ToString().Trim()));
            GPBM.TotalBL = decimal.Parse((dr["TotalBL"].ToString().Trim()));    
            return GPBM;
        }
        public List<GatePassBrandMapping> GetGatePassBrandDetailsList(long GatePassId)
        {
            List<GatePassBrandMapping> lstGPBM = new List<GatePassBrandMapping>();
            DataSet ds = new CommonDA().GetGatePassUploadBrandDetails(GatePassId);
            if (ds != null)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    lstGPBM.Add(FillGatePassBrandDetails(dr));
                }
            }
            return lstGPBM;
        }
        #endregion

        #region Report

        public List<DistrictWholeSaleToRetailorModel> GetGatePassReport()
        {
            var reportModels = new List<DistrictWholeSaleToRetailorModel>();
            DataSet ds = new CommonDA().GetGatePassReport();
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    reportModels.Add(GetDistrictWholeSaleToRetailorModel(dr));
                }
            }
            return reportModels;
        }

        private DistrictWholeSaleToRetailorModel GetDistrictWholeSaleToRetailorModel(DataRow dr)
        {
            DistrictWholeSaleToRetailorModel model = new DistrictWholeSaleToRetailorModel();

            model.RowNum = int.Parse(dr["RowNum"].ToString().Trim());
            model.BrewaryName = dr["BrewaryName"].ToString().Trim();
            model.Revenue = dr["Revenue"].ToString().Trim();
            model.Brand = dr["Brand"].ToString().Trim();
            model.BatchNo = dr["BatchNo"].ToString().Trim();
            model.TotalBottle = dr["TotalBottle"].ToString().Trim();
            model.Date = dr["Date"].ToString().Trim();
            model.VehicleNo = dr["VehicleNo"].ToString().Trim();
            model.Receiver = dr["Receiver"].ToString().Trim();

            //model.RowNum = int.Parse(dr["RowNum"].ToString().Trim());
            //model.Brand = dr["BrandName"].ToString().Trim();
            //model.Size =int.Parse( dr["Size"].ToString().Trim());
            //model.AvailableBottle =int.Parse( dr["AvailableBottle"].ToString().Trim());
            //model.AvailableBox =int.Parse( dr["AvailableBox"].ToString().Trim());
            //model.Duty =decimal.Parse( dr["Duty"].ToString().Trim());
            //model.AddDuty = decimal.Parse(dr["AddDuty"].ToString().Trim());

            return model;
        }

        public List<DistrictWholeSaleToRetailorModel> GetGatePassDetails()
        {
            var reportModels = new List<DistrictWholeSaleToRetailorModel>();
            DataSet ds = new CommonDA().GetGatePassDetails(2);
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    reportModels.Add(GetGatePassDetailModel(dr));
                }
            }
            return reportModels;
        }



        public List<DistrictWholeSaleToRetailorModel> GetBMGatePassDetails(long gatePassId, long brandId, int spType)
        {
            var reportModels = new List<DistrictWholeSaleToRetailorModel>();
            DataSet ds = new CommonDA().GetGatePassDetails(spType, gatePassId, brandId);
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    reportModels.Add(GetGatePassDetailModel(dr));
                }
            }
            return reportModels;
        }



        private DistrictWholeSaleToRetailorModel GetGatePassDetailModel(DataRow dr)
        {
            DistrictWholeSaleToRetailorModel model = new DistrictWholeSaleToRetailorModel();
            // model.RowNum = int.Parse(dr["RowNum"].ToString().Trim());
            model.Brand = dr["BrandName"].ToString().Trim();
            model.Size = int.Parse(dr["Size"].ToString().Trim());
            model.AvailableBottle = int.Parse(dr["AvailableBottle"].ToString().Trim());
            model.AvailableBox = int.Parse(dr["AvailableBox"].ToString().Trim());
            model.DispatchBox = int.Parse(dr["DispatchBox"].ToString().Trim());
            model.DispatchBottle = int.Parse(dr["DispatchBottle"].ToString().Trim());
            model.Duty = decimal.Parse(dr["Duty"].ToString().Trim());
            model.AddDuty = decimal.Parse(dr["AddDuty"].ToString().Trim());
            return model;
        }

        public List<GatePass> GetGatePassDetailsForGatePassList(int spType)
        {
            var reportModels = new List<GatePass>();
            DataSet ds = new CommonDA().GetGatePassDetails(spType);
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    reportModels.Add(GetGatePassDetailModelForPassList(dr));
                }
            }
            return reportModels;
        }



        private GatePass GetGatePassDetailModelForPassList(DataRow dr)
        {
            GatePass model = new GatePass();
            model.GatePassId = int.Parse(dr["GatePassId"].ToString().Trim());
            model.ToDate = Convert.ToDateTime(dr["ToDate"].ToString().Trim());
            model.FromDate = Convert.ToDateTime(dr["FromDate"].ToString().Trim());
            model.GatePassNo = dr["GatePassNo"].ToString().Trim();
            model.VehicleNo = dr["VehicleNo"].ToString().Trim();
            model.AgencyNameAndAddress = dr["AgencyNameAndAddress"].ToString().Trim();
            model.ConsignorName = dr["ConsinorName"].ToString().Trim();
            model.ConsigneeName = dr["ConsineeName"].ToString().Trim();
            model.TotalBox = int.Parse(dr["TotalBox"].ToString().Trim());
            model.TotalBottles = int.Parse(dr["TotalBottles"].ToString().Trim());
            model.TotalLitres = decimal.Parse(dr["TotalLitres"].ToString().Trim());
            model.ConsiderationFees = decimal.Parse(dr["ConsiderationFees"].ToString().Trim());
            model.Encrypt_GatePassID = new Crypto().Encrypt(dr["GatePassId"].ToString().Trim());
            return model;
        }



        public GatePass GenerateGatePass(long gatePassId, int spType1, int spType2)
        {
            var gatePass = new GatePass();
            var reportModels = new List<DistrictWholeSaleToRetailorModel>();
            DataSet ds1 = new CommonDA().GetGatePassDetails(spType1, gatePassId);
            DataSet ds2 = new CommonDA().GetGatePassDetails(spType2, gatePassId);
            if (ds1 != null && ds1.Tables[0] != null && ds1.Tables[0].Rows.Count > 0)
            {

                gatePass.GatePassId = int.Parse(ds1.Tables[0].Rows[0]["GatePassId"].ToString());
                gatePass.ToDate = Convert.ToDateTime(ds1.Tables[0].Rows[0]["ToDate"].ToString());
                gatePass.FromDate = Convert.ToDateTime(ds1.Tables[0].Rows[0]["FromDate"].ToString());
                gatePass.GatePassNo = ds1.Tables[0].Rows[0]["GatePassNo"].ToString().ToString().Trim();
                gatePass.VehicleNo = ds1.Tables[0].Rows[0]["VehicleNo"].ToString().ToString().Trim();
                gatePass.AgencyNameAndAddress = ds1.Tables[0].Rows[0]["AgencyNameAndAddress"].ToString().Trim();
                gatePass.ConsignorName = ds1.Tables[0].Rows[0]["ConsinorName"].ToString().Trim();
                gatePass.ConsigneeName = ds1.Tables[0].Rows[0]["ConsineeName"].ToString().Trim();
                gatePass.TotalBottles = int.Parse(ds1.Tables[0].Rows[0]["TotalBottles"].ToString().Trim());
                gatePass.TotalLitres = decimal.Parse(ds1.Tables[0].Rows[0]["TotalLitres"].ToString().Trim());
                gatePass.ConsiderationFees = decimal.Parse(ds1.Tables[0].Rows[0]["ConsiderationFees"].ToString().Trim());
                gatePass.TotalBottleQuantity = decimal.Parse(ds1.Tables[0].Rows[0]["TotalBottleQuantity"].ToString().Trim());
                gatePass.TotalLitresQuantity = decimal.Parse(ds1.Tables[0].Rows[0]["TotalLitresQuantity"].ToString().Trim());
            }

            if (ds2 != null && ds2.Tables[0] != null && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds2.Tables[0].Rows)
                {
                    reportModels.Add(GetGeneratedPassBrandList(dr));
                }
            }
            gatePass.DistrictWholeSaleToRetailorList = reportModels;
            return gatePass;
        }


        private DistrictWholeSaleToRetailorModel GetGeneratedPassBrandList(DataRow dr)
        {
            var model = new DistrictWholeSaleToRetailorModel();
            model.RowNum = int.Parse(dr["RowNum"].ToString().Trim());
            model.TransitGatePassID = int.Parse(dr["GatePassId"].ToString().Trim());
            model.Brand = dr["Brand"].ToString().Trim();
            model.BatchNo = dr["BatchNo"].ToString().Trim();
            model.BottleQuantity = dr["BottlesQuantity"].ToString().Trim();
            model.AlcoholicStrength = decimal.Parse(dr["Strength"].ToString().Trim());
            model.TotalLitres = decimal.Parse(dr["TotalLitres"].ToString().Trim());
            return model;
        }

        public BrewerytToManufacturerGatePass GetBreweryToManufacturerPassDetailsDetails(int spType)
        {
            var reportModels = new BrewerytToManufacturerGatePass();
            DataSet ds = new CommonDA().GetGatePassDetails(spType);
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                reportModels.Date = ds.Tables[0].Rows[0]["Date"].ToString().Substring(0, 11);
                reportModels.ValidTill = ds.Tables[0].Rows[0]["ValidTill"].ToString().Trim().Substring(0, 11);
                if (spType == 6)
                {
                    reportModels.FromLicenseType = ds.Tables[0].Rows[0]["FromLicenseType"].ToString();
                    reportModels.ToLicenseType = ds.Tables[0].Rows[0]["ToLicenseType"].ToString().Trim();

                }
                if (spType == 7)
                {
                    reportModels.FromLicenseTypeId = short.Parse(ds.Tables[0].Rows[0]["FromLicenceTypeId"].ToString());
                    reportModels.ToLicenseTypeId = short.Parse(ds.Tables[0].Rows[0]["ToLicenceTypeId"].ToString().Trim());
                    reportModels.Address = ds.Tables[0].Rows[0]["Address"].ToString().Trim();
                    reportModels.MDistrictId1 = decimal.Parse(ds.Tables[0].Rows[0]["MDistrictId1"].ToString());
                    reportModels.MDistrictId2 = decimal.Parse(ds.Tables[0].Rows[0]["MDistrictId2"].ToString().Trim());
                    reportModels.MDistrictId3 = decimal.Parse(ds.Tables[0].Rows[0]["MDistrictId3"].ToString().Trim());
                    reportModels.GrossWeight = decimal.Parse(ds.Tables[0].Rows[0]["GrossWeight"].ToString().Trim());
                    reportModels.TareWeight = decimal.Parse(ds.Tables[0].Rows[0]["TareWeight"].ToString().Trim());
                }
                if (spType == 8)
                {
                    reportModels.ShopId = int.Parse(ds.Tables[0].Rows[0]["ShopId"].ToString());
                    reportModels.GatePassNo = ds.Tables[0].Rows[0]["GatePassNo"].ToString();
                    reportModels.LicenseeName = ds.Tables[0].Rows[0]["LicenseeName"].ToString();
                    reportModels.LicenseeAddress = ds.Tables[0].Rows[0]["LicenseeAddress"].ToString();
                }
                if (string.IsNullOrEmpty(ds.Tables[0].Rows[0]["BrandId"].ToString()))
                    reportModels.BrandId = null;
                else
                    reportModels.BrandId = Convert.ToInt64(ds.Tables[0].Rows[0]["BrandId"].ToString());
                if (spType == 6 || spType == 7)
                {
                    reportModels.ConsineeLicenseNo = ds.Tables[0].Rows[0]["ConsineeLicenseNo"].ToString().Trim();
                    reportModels.ConsinorLicenseNo = ds.Tables[0].Rows[0]["ConsinorLicenseNo"].ToString().Trim();
                    reportModels.ConsignorName = ds.Tables[0].Rows[0]["ConsignorName"].ToString().Trim();
                }

                reportModels.VehicleNo = ds.Tables[0].Rows[0]["VehicleNo"].ToString().Trim();
                reportModels.VehicleDriverName = ds.Tables[0].Rows[0]["VehicleDriverName"].ToString().Trim();
                reportModels.AgencyNameAndAddress = ds.Tables[0].Rows[0]["AgencyNameAndAddress"].ToString().Trim();
                reportModels.RouteDetails = ds.Tables[0].Rows[0]["RouteDetails"].ToString().Trim();
                reportModels.ConsigneeName = ds.Tables[0].Rows[0]["ConsigneeName"].ToString().Trim();
                reportModels.GatePassId = Convert.ToInt64(ds.Tables[0].Rows[0]["GatePassId"].ToString().Trim());
                reportModels.Status = ds.Tables[0].Rows[0]["Status"].ToString().Trim();

                reportModels.SP_Type = 6;
            }
            return reportModels;
        }

        //private BrewerytToManufacturerGatePass GetBreweryToManufacturerModel(DataRow dr)
        //{
        //    var model = new BrewerytToManufacturerGatePass();
        //    model.Date = dr["Date"].ToString();
        //    model.ValidTill = dr["ValidTill"].ToString().Trim();
        //    model.FromLicenseType = dr["FromLicenseType"].ToString();
        //    model.ToLicenseType = dr["ToLicenseType"].ToString().Trim();
        //    model.BrandId = int.Parse(dr["BrandId"].ToString().Trim());
        //    model.ConsineeLicenseNo =dr["ConsineeLicenseNo"].ToString().Trim();
        //    model.ConsinorLicenseNo =dr["ConsinorLicenseNo"].ToString().Trim();
        //    model.VehicleNo = dr["VehicleNo"].ToString().Trim();
        //    model.VehicleDriverName = dr["VehicleDriverName"].ToString().Trim();
        //    model.AgencyNameAndAddress = dr["AgencyNameAndAddress"].ToString().Trim();
        //    model.RouteDetails = dr["RouteDetails"].ToString().Trim();
        //    model.ConsignorName = dr["ConsignorName"].ToString().Trim();
        //    model.ConsignorName = dr["ConsignorName"].ToString().Trim();
        //    return model;
        //}

        #endregion

        #region BBTFormation

        public List<BBTMaster> GetBBTMasterList(int bbtId, string status)
        {
            var bbtFormationList = new List<BBTMaster>();
            DataSet ds = new CommonDA().GetBBTMaster(bbtId, status);
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    bbtFormationList.Add(GetBBTMaster(dr));
                }
            }
            return bbtFormationList;
        }

        private BBTMaster GetBBTMaster(DataRow dr)
        {
            var bbtFormation = new BBTMaster();
            try
            {
                bbtFormation.BBTId = int.Parse(dr["BBTId"].ToString());
                bbtFormation.BBTName = dr["BBTName"].ToString().Trim();
                bbtFormation.BBTBulkLiter = decimal.Parse(dr["BBTBulkLiter"].ToString().Trim());
                bbtFormation.BBTCapacity = decimal.Parse(dr["BBTCapacity"].ToString().Trim());
                bbtFormation.Status1 = (dr["Status1"].ToString().Trim());
                bbtFormation.BBTId_Encript = new Crypto().Encrypt(dr["BBTId"].ToString().Trim());
                bbtFormation.SP_Type = 2;
                bbtFormation.Status = dr["Status"].ToString().Trim();
            }
            catch (Exception) { }
            return bbtFormation;
        }
        public List<UTTransferToBBT> GetUTTransferToBBTList(DateTime FromDate, DateTime ToDate, int UnitTankId, string Status, short BreweryId, int BBTId)
        {
            List<UTTransferToBBT> UnitTankBLDetailList = new List<UTTransferToBBT>();
            DataSet ds = new CommonDA().GetUTTransferToBBT(FromDate, ToDate, UnitTankId, Status, BreweryId, BBTId);
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    UnitTankBLDetailList.Add(GetUTTransferToBBTDetail(dr));
                }
            }
            return UnitTankBLDetailList;
        }
        private UTTransferToBBT GetUTTransferToBBTDetail(DataRow dr)
        {
            UTTransferToBBT UTBL = new UTTransferToBBT();
            try
            {
                UTBL.Srno = int.Parse(dr["Srno"].ToString().Trim());
                UTBL.IssuedFromUTId = int.Parse(dr["IssuedFromUTId"].ToString().Trim());
                UTBL.UnitTank = (dr["UnitTankName"].ToString().Trim());
                UTBL.BBTID = int.Parse(dr["BBTId"].ToString().Trim());
                UTBL.BBTName = (dr["BBTName"].ToString().Trim());
                UTBL.IssueBL = float.Parse(dr["IssueBL"].ToString().Trim());
                UTBL.Wastage = float.Parse(dr["Wastage"].ToString().Trim());
                UTBL.TransferDate1 = dr["TransferDate"].ToString().Trim();
                UTBL.Remark = dr["Remark"].ToString().Trim();
                UTBL.PrevBalanceBBT = dr["PrevBalanceBBT"].ToString().Trim();
                UTBL.PrevBalanceUT = dr["PrevBalanceUT"].ToString().Trim();
                UTBL.CurrentBalanceUT = dr["CurrentBalanceUT"].ToString().Trim();
                UTBL.CurrentBalanceBBT = dr["CurrentBalanceBBT"].ToString().Trim();
            }
            catch (Exception) { }
            return UTBL;
        }
        #endregion

        #region GatePass
        public GatePassDetails GetGatePassDetailsG(long GatePassId, DateTime FromDate, DateTime Todate, int UploadValue, string Status,string IsReceive)
        {
            DataSet ds = new CommonDA().GetGatePassDetailsG(GatePassId, FromDate, Todate, UploadValue, Status, IsReceive);
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                return FillGatePassDetails(ds.Tables[0].Rows[0]);
            }
            else
            {
                return new GatePassDetails();
            }
        }
        public List<GatePassDetails> GetGatePassDetailsList(long GatePassId, DateTime FromDate, DateTime Todate, int UploadValue, string Status,string IsReceive)
        {
            List<GatePassDetails> lstGPD = new List<GatePassDetails>();
            DataSet ds = new CommonDA().GetGatePassDetailsG(GatePassId, FromDate, Todate, UploadValue, Status, IsReceive);
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    lstGPD.Add(FillGatePassDetails(dr));
                }
            }
            return lstGPD;
        }
        private GatePassDetails FillGatePassDetails(DataRow dr)
        {
            GatePassDetails GP = new GatePassDetails();
            try
            {
                GP.EncGatePassId = new Crypto().Encrypt(dr["GatePassId"].ToString().Trim());
                GP.Address = dr["Address"].ToString().Trim();
                GP.AgencyNameAndAddress = dr["AgencyNameAndAddress"].ToString().Trim();
                GP.district_code_census1 = int.Parse(dr["district_code_census1"].ToString().Trim());
                GP.district_code_census2 = int.Parse(dr["district_code_census2"].ToString().Trim());
                GP.district_code_census3 = int.Parse(dr["district_code_census3"].ToString().Trim());
                GP.DriverName = dr["DriverName"].ToString().Trim();
                GP.FromConsignorName = dr["FromConsignorName"].ToString().Trim();
                GP.FromDate = DateTime.Parse(dr["FromDate"].ToString().Trim());
                GP.FromDate1 = (dr["FromDate1"].ToString().Trim());
                GP.FromLicenseType = (dr["FromLicenseType"].ToString().Trim());
                GP.GatePassId = long.Parse((dr["GatePassId"].ToString().Trim()));
                GP.EncGatePassId = new Crypto().Encrypt((dr["GatePassId"].ToString().Trim()));
                GP.GatePassNo = (dr["GatePassNo"].ToString().Trim());
                GP.GatePassSourceId = long.Parse((dr["GatePassSourceId"].ToString().Trim()));
                GP.GrossWeight = float.Parse(dr["GrossWeight"].ToString().Trim());
                GP.LicenseeAddress = (dr["LicenseeAddress"].ToString().Trim());
                GP.LicenseeName = (dr["LicenseeName"].ToString().Trim());
                GP.LicenseeNo = (dr["LicenseeNo"].ToString().Trim());
                GP.NetWeight = float.Parse(dr["NetWeight"].ToString().Trim());
                GP.Receiver = dr["Receiver"].ToString().Trim();
                GP.RouteDetails = (dr["RouteDetails"].ToString().Trim());
                GP.ShopId = int.Parse(dr["ShopId"].ToString().Trim());
                GP.ShopName = (dr["ShopName"].ToString().Trim());
                GP.SP_Type = 2;
                GP.Status = (dr["Status"].ToString().Trim());
                GP.TareWeight = float.Parse(dr["TareWeight"].ToString().Trim());
                GP.ToConsigeeName = (dr["ToConsigeeName"].ToString().Trim());
                GP.ToDate = DateTime.Parse(dr["ToDate"].ToString().Trim());
                GP.ToDate1 = (dr["ToDate1"].ToString().Trim());
                GP.ToLicenseType = (dr["ToLicenseType"].ToString().Trim());
                GP.UploadValue = int.Parse(dr["UploadValue"].ToString().Trim());
                GP.VehicleNo = (dr["VehicleNo"].ToString().Trim());
                GP.FromLicenceNo = (dr["FromLicenceNo"].ToString().Trim());
                GP.ToLicenceNo = (dr["ToLicenceNo"].ToString().Trim());
                GP.TotalCase = int.Parse(dr["TotalCase"].ToString().Trim());
                GP.TotalBottle = int.Parse(dr["TotalBottle"].ToString().Trim());
                GP.TotalBL = decimal.Parse(dr["TotalBL"].ToString().Trim());
                GP.TotalConsiderationFees = decimal.Parse(dr["ConsiderationFees"].ToString().Trim());
                GP.InBondValue = decimal.Parse(dr["InBondValue"].ToString().Trim());
                GP.ExportDuty = decimal.Parse(dr["ExportDuty"].ToString().Trim());
                GP.GatePassType = short.Parse(dr["GatePassTypeID"].ToString().Trim());
                GP.CheckPostVia = (dr["CheckPostVia"].ToString().Trim());
                GP.AdditionalConsiFees = decimal.Parse(dr["AdditionalConsiFees"].ToString().Trim());
            }
            catch (Exception) { }
            return GP;
        }
        public FormFL21 GetFormFL21(int FormFL21Id, string FormFLStatus)
        {
            DataSet ds = new CommonDA().GetFormFL21(FormFL21Id, FormFLStatus);
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                return FillFormFL21(ds.Tables[0].Rows[0]);
            }
            else
            {
                return new FormFL21();
            }
        }
        public List<FormFL21> GetFormFL21List(int FormFL21Id, string FormFLStatus)
        {
            List<FormFL21> lstGPD = new List<FormFL21>();
            DataSet ds = new CommonDA().GetFormFL21(FormFL21Id, FormFLStatus);
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    lstGPD.Add(FillFormFL21(dr));
                }
            }
            return lstGPD;
        }
        private FormFL21 FillFormFL21(DataRow dr)
        {
            FormFL21 FL21 = new FormFL21();
            try
            {
                FL21.BoxSize = int.Parse(dr["BoxSize"].ToString().Trim());
                FL21.BrandId = int.Parse(dr["BrandId"].ToString().Trim());
                FL21.DutyCalculated= decimal.Parse(dr["DutyCalculated"].ToString().Trim());
                FL21.EncFL21IDId= new Crypto().Encrypt(dr["FL21Id"].ToString().Trim());
                FL21.FL21ID= int.Parse(dr["FL21Id"].ToString().Trim());
                FL21.FL21Status= (dr["FL21Status"].ToString().Trim());
                FL21.FromConsignorAddress= (dr["ConsignerAddress"].ToString().Trim());
                FL21.FromConsignorName= (dr["ConsignerName"].ToString().Trim());
                FL21.FromLicenceNo= (dr["ConsignerLicenseNo"].ToString().Trim());
                FL21.PermitFees= decimal.Parse(dr["PermitFees"].ToString().Trim());
                FL21.QuantityInBottleML= decimal.Parse(dr["Quantity"].ToString().Trim());
                FL21.RateofPermit = decimal.Parse(dr["RateofPermit"].ToString().Trim());
                FL21.RouteDetails =  (dr["RouteDetails"].ToString().Trim());
                FL21.ToConsigeeAddress =  (dr["ConsigneeAddress"].ToString().Trim());
                FL21.ToConsigeeName =  (dr["ConsigneeName"].ToString().Trim());
                FL21.ToLicenceNo =  (dr["ConsigneeLicenseNo"].ToString().Trim());
                FL21.TotalBL = decimal.Parse (dr["TotalBL"].ToString().Trim());
                FL21.TotalBottle = int.Parse(dr["TotalBottle"].ToString().Trim());
                FL21.TotalCase = int.Parse(dr["TotalCase"].ToString().Trim());
                FL21.TotalFees = decimal.Parse(dr["TotalFees"].ToString().Trim());
                FL21.TransactionNo =(dr["TransactionNo"].ToString().Trim());
                FL21.Brand =(dr["BrandName"].ToString().Trim());
                FL21.SPType = 2;
                FL21.FL21Status1= (dr["FL21Status1"].ToString().Trim());
                FL21.PackagingType= (dr["PackagingType"].ToString().Trim());
                FL21.EntryDate1= (dr["EntryDate1"].ToString().Trim());
            }
            catch (Exception) { }
            return FL21;
        }
        #endregion




    }
}