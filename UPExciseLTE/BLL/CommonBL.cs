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
        public static List<SelectListItem> fillState(string SelectType)
        {
            List<SelectListItem> StateList = new List<SelectListItem>();
            CMODataEntryBLL.bindDropDownHnGrid("proc_ddlDetail", StateList, "STATE", "", SelectType);
            return StateList;
        }
        public static List<SelectListItem> fillDistict(string SelectType,long districtId1=0, long districtId2=0)
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
            CMODataEntryBLL.bindDropDownHnGrid("proc_ddlDetail", BrandList, "BR", UserSession.PushName.ToString(), SelectType);
            return BrandList;
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
                brand.AlcoholType = dr["AlcoholType"].ToString().Trim();
                brand.PackagingType = dr["PackagingType"].ToString().Trim();
                brand.StateId = int.Parse(dr["StateId"].ToString().Trim());
                brand.SPType = 2;
            }
            catch (Exception) { }
            return brand;
        }
        public List<BottelingPlan> GetBottelingPlanList(DateTime FromDate, DateTime ToDate, short BreweryId, int BrandId, string Mapped, string BatchNo, int PlanId, string Status)
        {
            List<BottelingPlan> lstPlan = new List<BottelingPlan>();
            DataSet ds = new CommonDA().GetBottelingPlanDetail(FromDate,ToDate,BreweryId,BrandId,Mapped,BatchNo,PlanId,Status);
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

        private DistrictWholeSaleToRetailorModel GetGatePassDetailModel(DataRow dr)
        {
            DistrictWholeSaleToRetailorModel model = new DistrictWholeSaleToRetailorModel();
            model.RowNum = int.Parse(dr["RowNum"].ToString().Trim());
            model.Brand = dr["BrandName"].ToString().Trim();
            model.Size = int.Parse(dr["Size"].ToString().Trim());
            model.AvailableBottle = int.Parse(dr["AvailableBottle"].ToString().Trim());
            model.AvailableBox = int.Parse(dr["AvailableBox"].ToString().Trim());
            model.Duty = decimal.Parse(dr["Duty"].ToString().Trim());
            model.AddDuty = decimal.Parse(dr["AddDuty"].ToString().Trim());
            return model;
        }

        public List<GatePass> GetGatePassDetailsForGatePassList()
        {
            var reportModels = new List<GatePass>();
            DataSet ds = new CommonDA().GetGatePassDetails(3);
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
            model.GatePassNo= dr["GatePassNo"].ToString().Trim();
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



        public GatePass GenerateGatePass(long gatePassId)
        {
            var gatePass = new GatePass();
            var reportModels = new List<DistrictWholeSaleToRetailorModel>();
            DataSet ds1 = new CommonDA().GetGatePassDetails(5, gatePassId);
            DataSet ds2 = new CommonDA().GetGatePassDetails(4, gatePassId);
            if (ds1 != null && ds1.Tables[0] != null && ds1.Tables[0].Rows.Count > 0)
            {
                
                gatePass.GatePassId = int.Parse(ds1.Tables[0].Rows[0]["GatePassId"].ToString());
                gatePass.Date = ds1.Tables[0].Rows[0]["ToDate"].ToString();
                gatePass.ValidTill = ds1.Tables[0].Rows[0]["FromDate"].ToString();
                gatePass.GatePassNo = ds1.Tables[0].Rows[0]["GatePassNo"].ToString().ToString().Trim();
                gatePass.VehicleNo = ds1.Tables[0].Rows[0]["VehicleNo"].ToString().ToString().Trim();
                gatePass.AgencyNameAndAddress =ds1.Tables[0].Rows[0]["AgencyNameAndAddress"].ToString().Trim();
                gatePass.ConsignorName =ds1.Tables[0].Rows[0]["ConsinorName"].ToString().Trim();
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



        #endregion

        #region BBTFormation

        public List<BBTFormation> GetBBTMasterList(int bbtId,int brandId,string status)
        {
            var bbtFormationList = new List<BBTFormation>();
            DataSet ds = new CommonDA().GetBBTMaster(bbtId,brandId,status);
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    bbtFormationList.Add(GetBBTMaster(dr));
                }
            }
            return bbtFormationList;
        }

        private BBTFormation GetBBTMaster(DataRow dr)
        {
            var bbtFormation=new BBTFormation();
            try
            {
                bbtFormation.RowNum = int.Parse(dr["RowNum"].ToString());
                bbtFormation.BBTId = int.Parse(dr["BBTId"].ToString());
                bbtFormation.BrandID = int.Parse(dr["BrandID"].ToString());
                bbtFormation.BrandName = dr["BrandName"].ToString().Trim();
                bbtFormation.BBTName = dr["BBTName"].ToString().Trim();
                bbtFormation.BBTBulkLiter = decimal.Parse(dr["BBTBulkLiter"].ToString().Trim());
                bbtFormation.BBTCapacity = decimal.Parse(dr["BBTCapacity"].ToString().Trim());
                bbtFormation.BBTId_Encript = new Crypto().Encrypt(dr["BBTId"].ToString().Trim());
                bbtFormation.SP_Type = 2;
                bbtFormation.Status = dr["Status"].ToString().Trim();
            }
            catch (Exception) { }
            return bbtFormation;
        }
        public List<UTTransferToBBT> GetUTTransferToBBTList(DateTime FromDate, DateTime ToDate, int UnitTankId, string Status,short BreweryId,int BBTId)
        {
            List<UTTransferToBBT>  UnitTankBLDetailList = new List<UTTransferToBBT>();
            DataSet ds = new CommonDA().GetUTTransferToBBT(FromDate, ToDate, UnitTankId, Status,BreweryId,BBTId);
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
               UTBL.IssueBL =float.Parse(dr["IssueBL"].ToString().Trim());
               UTBL.Wastage =float.Parse(dr["Wastage"].ToString().Trim());
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




        #endregion




    }
}