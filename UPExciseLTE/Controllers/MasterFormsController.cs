using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web.Mvc;
using UPExciseLTE.DAL;
using UPExciseLTE.Models;
//using UPExciseLTE.App_Code;
using ClosedXML;
using ClosedXML.Excel;
using System.IO;
using UPExciseLTE.BLL;
using Newtonsoft.Json;

namespace UPExciseLTE.Controllers
{
    public class MasterFormsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult BrandMaster()
        {
            BrandMaster Brand = new BrandMaster();
            if (Request.QueryString["Code"] !=null && Request.QueryString["Code"].Trim()!=string.Empty)
            {
                Brand = new CommonBL().GetBrand(int.Parse(new Crypto().Decrypt(Request.QueryString["Code"].Trim())), "", "", "", -1, -1, -1, "Z");
            }
            string UserId = UserSession.LoggedInUserId.ToString().Trim();   
            
            ViewBag.Brewery =CommonBL.fillBrewery();
            ViewBag.StateList = CommonBL.fillState();
            return View(Brand);
        }
        [HttpPost]
        public ActionResult BrandMaster(BrandMaster B)
        {
            if (B.Remark == null)
            {
                B.Remark = "";
            }
            string str = new CommonDA().InsertUpdateBrand(B);
            ViewBag.Msg = str;
            BrandMaster Brand = new BrandMaster();
            ViewBag.Brewery = CommonBL.fillBrewery();
            ViewBag.StateList = CommonBL.fillState();
            return View(Brand);

        }
        [HttpGet]
        public ActionResult GetBrandDetails()
        {
            List<BrandMaster> lstBrand = new CommonBL().GetBrandList(-1, "", "", "", -1, -1, -1, "Z");
            ViewBag.Brewery = CommonBL.fillBrewery();
            ViewBag.District = CommonBL.fillState();
            return View(lstBrand);
        }
        [HttpPost]
        public ActionResult EditBrand(BrandMaster B)
        {
           
            string str = new CommonDA().InsertUpdateBrand(B);
            ViewBag.Msg = str;
            BrandMaster Brand = new BrandMaster();
            ViewBag.Brewery = CommonBL.fillBrewery() ;
            ViewBag.StateList = CommonBL.fillState();
            Brand = new CommonBL().GetBrand(B.BrandId, "", "", "", -1, -1, -1, "Z");
            return View(Brand);
        }
        [HttpGet]
        public ActionResult FinalizeBrand()
        {
            ViewBag.Brewery = CommonBL.fillBrewery();
            ViewBag.District = CommonBL.fillState();
            List<BrandMaster> lstBrand = new CommonBL().GetBrandList(-1, "", "", "", -1, -1, -1, "Z");
            return View(lstBrand);
        }
        [HttpGet]
        public ActionResult FreezeBrand()
        {
            ViewBag.Brewery = CommonBL.fillBrewery();
            ViewBag.District = CommonBL.fillState();
            List<BrandMaster> lstBrand = new CommonBL().GetBrandList(-1, "", "", "", -1, -1, -1, "P");
            return View(lstBrand);
        }
        [HttpGet]
        public ActionResult BottelingPlan()
        {
            BottelingPlan Plan = new BottelingPlan();
            List<SelectListItem> BrandList = new List<SelectListItem>();
            List<SelectListItem> StateList = new List<SelectListItem>();
            CMODataEntryBLL.bindDropDownHnGrid("proc_ddlDetail", StateList, "STATE", "", "S");
            ViewBag.StateList = StateList;
            Plan.PlanId = -1;
            Plan.Type = 1;
            string UserId = (Session["tbl_Session"] as DataTable).Rows[0]["UserId"].ToString().Trim();
            CMODataEntryBLL.bindDropDownHnGrid_db2("proc_ddlDetail", BrandList, "BR", UserId, "Z");
            ViewBag.Brand = BrandList;
            return View(Plan);
        }
       
        [HttpPost]
        public ActionResult BottelingPlan(BottelingPlan BP)
        {
            BottelingPlan Plan = new BottelingPlan();
            List<SelectListItem> BrandList = new List<SelectListItem>();
            string UserId = (Session["tbl_Session"] as DataTable).Rows[0]["UserId"].ToString().Trim();
            CMODataEntryBLL.bindDropDownHnGrid_db2("proc_ddlDetail", BrandList, "BR", UserId, "Z");
            List<SelectListItem> StateList = new List<SelectListItem>();
            CMODataEntryBLL.bindDropDownHnGrid("proc_ddlDetail", StateList, "STATE", "", "S");
            ViewBag.StateList = StateList;
            ViewBag.Brand = BrandList;
           
            BP.AlcoholicLiter = 0;
            BP.DateOfPlan =   CommonBL.Setdate(BP.DateOfPlan1);
            string str = new CommonDA().InsertUpdatePlan(BP);
            ViewBag.Msg = str;
            Plan.PlanId = -1;
            Plan.Type = 1;
            return View(Plan);
        }
        [HttpGet]
        public ActionResult UpdateBottelingPlan(string A)
        {
            BottelingPlan Plan = new BottelingPlan();
            List<SelectListItem> BrandList = new List<SelectListItem>();
            A = new Crypto().Decrypt(A);
            int Planid = int.Parse(A);
            string UserId = (Session["tbl_Session"] as DataTable).Rows[0]["UserId"].ToString().Trim();
            CMODataEntryBLL.bindDropDownHnGrid_db2("proc_ddlDetail", BrandList, "BR", UserId, "Z");
            List<SelectListItem> StateList = new List<SelectListItem>();
            CMODataEntryBLL.bindDropDownHnGrid("proc_ddlDetail", StateList, "STATE", "", "S");
            ViewBag.StateList = StateList;
            DataSet ds = new CommonDA().GetBottelingPlanDetail(Convert.ToDateTime(String.Format("{0:dd/MM/yyyy}", "01/01/1900"), CultureInfo.CurrentCulture), DateTime.Now, -1, -1, "", "", "", Planid);
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                Plan.PlanId= int.Parse(ds.Tables[0].Rows[0]["PlanId"].ToString().Trim());
                Plan.Type= 2;
                //Plan.DateOfPlan = Convert.ToDateTime(ds.Tables[0].Rows[0]["DateofPlan1"].ToString().Trim());
                Plan.DateOfPlan1 = ds.Tables[0].Rows[0]["DateofPlan"].ToString().Trim();
                Plan.BatchNo = ds.Tables[0].Rows[0]["BatchNo"].ToString().Trim();
                Plan.BrandId = int.Parse(ds.Tables[0].Rows[0]["BrandId"].ToString().Trim());
                Plan.MappedOrNot = short.Parse(ds.Tables[0].Rows[0]["MappedOrNot"].ToString().Trim());
                Plan.LiquorType = ds.Tables[0].Rows[0]["LiquorType"].ToString().Trim();
                Plan.LicenceType = ds.Tables[0].Rows[0]["LicenceType"].ToString().Trim();
                Plan.LicenseNo = ds.Tables[0].Rows[0]["LicenceNo"].ToString().Trim();
                Plan.IsStateSame = bool.Parse(ds.Tables[0].Rows[0]["IsStateSame"].ToString().Trim());
                Plan.NumberOfCases = int.Parse(ds.Tables[0].Rows[0]["NumberOfCases"].ToString().Trim());
                Plan.QunatityInCaseExport = int.Parse(ds.Tables[0].Rows[0]["QunatityInCaseExport"].ToString().Trim());
                Plan.PlanNoofBottling = ds.Tables[0].Rows[0]["PlanNoofBottling"].ToString().Trim();
                Plan.QuantityInBottleML = ds.Tables[0].Rows[0]["QuantityInBottleML"].ToString().Trim();
                Plan.Strength = ds.Tables[0].Rows[0]["AlcoholType"].ToString().Trim();
                Plan.BulkLiter = float.Parse(ds.Tables[0].Rows[0]["BulkLitre"].ToString().Trim());
                Plan.AlcoholicLiter = float.Parse(ds.Tables[0].Rows[0]["AlcoholicLiter"].ToString().Trim());
                Plan.TotalUnitQuantity = int.Parse(ds.Tables[0].Rows[0]["TotalUnitQuantity"].ToString().Trim());
                Plan.StateId = int.Parse(ds.Tables[0].Rows[0]["stateId"].ToString().Trim());
            }
            ViewBag.Brand = BrandList;
            return View(Plan);
        }
        [HttpPost]
        public ActionResult UpdateBottelingPlan(BottelingPlan BP)
        {
            List<SelectListItem> BrandList = new List<SelectListItem>();
            string UserId = (Session["tbl_Session"] as DataTable).Rows[0]["UserId"].ToString().Trim();
            CMODataEntryBLL.bindDropDownHnGrid_db2("proc_ddlDetail", BrandList, "BR", UserId, "Z");
            ViewBag.Brand = BrandList;
            List<SelectListItem> StateList = new List<SelectListItem>();
            CMODataEntryBLL.bindDropDownHnGrid("proc_ddlDetail", StateList, "STATE", "", "S");
            ViewBag.StateList = StateList;
           
            BP.Type = 2;
            BP.AlcoholicLiter = 0;
            BP.DateOfPlan = BLL.CommonBL.Setdate(BP.DateOfPlan1);
            
            string str = new CommonDA().InsertUpdatePlan(BP);
            ViewBag.Msg = str;
            return View(BP);
        }
        [HttpGet]
        public ActionResult BottelingPlanList()
        {
            List<BottelingPlan> BPList = new List<BottelingPlan>();
            List<SelectListItem> BrandList = new List<SelectListItem>();
            string UserId = (Session["tbl_Session"] as DataTable).Rows[0]["UserId"].ToString().Trim();
            CMODataEntryBLL.bindDropDownHnGrid_db2("proc_ddlDetail", BrandList, "BR", UserId, "Z");
            ViewBag.Brand = BrandList;
            DataSet ds = new CommonDA().GetBottelingPlanDetail(DateTime.Now,DateTime.Now,-1,-1,"Z","Z","Z",-1);
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                try
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        BottelingPlan Plan = new BottelingPlan();
                        Plan.PlanId = int.Parse(dr["PlanId"].ToString().Trim());
                        Plan.EncPlanId = new Crypto().Encrypt(dr["PlanId"].ToString().Trim());
                        Plan.DateOfPlan1 =(dr["DateOfPlan"].ToString().Trim());
                        Plan.LiquorType = dr["LiquorType"].ToString().Trim();
                        Plan.LicenceType = dr["LiquorType"].ToString().Trim();
                        Plan.Brand = dr["BrandName"].ToString().Trim();
                        Plan.Capacity = dr["Capacity"].ToString().Trim();
                        Plan.QunatityInCaseExport = int.Parse(dr["QuantityInCase"].ToString().Trim());
                        Plan.Quantity = dr["TotalUnitQuantity"].ToString().Trim();
                        Plan.PlanNoofBottling = dr["PlanNoofBottling"].ToString().Trim();
                        Plan.Status = dr["Status"].ToString().Trim();
                        Plan.NumberOfCases = int.Parse(dr["NumberOfCases"].ToString().Trim());
                        BPList.Add(Plan);
                    }
                }
                catch (Exception exp) { }
            }
            return View(BPList);
        }
        [HttpGet]
        public ActionResult ProductionPlanList()
        {
            List<BottelingPlan> BPList = new List<BottelingPlan>();
            List<SelectListItem> BrandList = new List<SelectListItem>();
            string UserId = (Session["tbl_Session"] as DataTable).Rows[0]["UserId"].ToString().Trim();
            CMODataEntryBLL.bindDropDownHnGrid_db2("proc_ddlDetail", BrandList, "BR", UserId, "Z");
            ViewBag.Brand = BrandList;
            DataSet ds = new CommonDA().GetProducePlanDetail(DateTime.Now, DateTime.Now, -1, -1, "Z", "Z", "Z", -1);
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                try
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        BottelingPlan Plan = new BottelingPlan();
                        Plan.PlanId = int.Parse(dr["PlanId"].ToString().Trim());
                        Plan.EncPlanId = new Crypto().Encrypt(dr["PlanId"].ToString().Trim());
                        Plan.DateOfPlan1 = (dr["DateOfPlan"].ToString().Trim());
                        Plan.LiquorType = dr["LiquorType"].ToString().Trim();
                        Plan.LicenceType = dr["LiquorType"].ToString().Trim();
                        Plan.Brand = dr["BrandName"].ToString().Trim();
                        Plan.Capacity = dr["Capacity"].ToString().Trim();
                        Plan.QunatityInCaseExport = int.Parse(dr["QuantityInCase"].ToString().Trim());
                        Plan.Quantity = dr["TotalUnitQuantity"].ToString().Trim();
                        Plan.PlanNoofBottling = dr["PlanNoofBottling"].ToString().Trim();
                        Plan.Status = dr["Status"].ToString().Trim();
                        Plan.NumberOfCases = int.Parse(dr["NumberOfCases"].ToString().Trim());
                        Plan.TotalUnitQuantity = int.Parse(dr["TotalUnitQuantity"].ToString().Trim());
                        Plan.ProducedQunatityInCaseExport = Plan.QunatityInCaseExport;
                        Plan.ProducedNumberOfCases = int.Parse(dr["ProducedNumberOfCases"].ToString().Trim());
                        Plan.ProducedQunatityInCaseExport = int.Parse(dr["ProducedQunatityInCaseExport"].ToString().Trim());
                        Plan.ProducedBulkLiter = float.Parse(dr["ProducedBulkLitre"].ToString().Trim());
                        Plan.ProducedAlcoholicLiter = int.Parse(dr["ProducedAlcoholicLitre"].ToString().Trim());
                        Plan.ProducedTotalUnitQuantity = int.Parse(dr["ProducedTotalUnitQuantity"].ToString().Trim());
                        Plan.WastageInNumber = int.Parse(dr["WastageInNumber"].ToString().Trim());
                        Plan.WastageBL = int.Parse(dr["WastageBL"].ToString().Trim());
                        Plan.IsProductionFinal = short.Parse(dr["IsProductionFinal"].ToString().Trim());
                        BPList.Add(Plan);
                    }
                }
                catch (Exception exp) { }
            }
            return View(BPList);
        }
        [HttpGet]
        public ActionResult FreezePlan()
        {
            List<BottelingPlan> BPList = new List<BottelingPlan>();
            List<SelectListItem> BrandList = new List<SelectListItem>();
            string UserId = (Session["tbl_Session"] as DataTable).Rows[0]["UserId"].ToString().Trim();
            CMODataEntryBLL.bindDropDownHnGrid_db2("proc_ddlDetail", BrandList, "BR", UserId, "Z");
            ViewBag.Brand = BrandList;
            DataSet ds = new CommonDA().GetProducePlanDetail(DateTime.Now, DateTime.Now, -1, -1, "Z", "Z", "Z", -1);
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                try
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        BottelingPlan Plan = new BottelingPlan();
                        Plan.PlanId = int.Parse(dr["PlanId"].ToString().Trim());
                        Plan.EncPlanId = new Crypto().Encrypt(dr["PlanId"].ToString().Trim());
                        Plan.DateOfPlan1 = (dr["DateOfPlan"].ToString().Trim());
                        Plan.LiquorType = dr["LiquorType"].ToString().Trim();
                        Plan.LicenceType = dr["LiquorType"].ToString().Trim();
                        Plan.Brand = dr["BrandName"].ToString().Trim();
                        Plan.Capacity = dr["Capacity"].ToString().Trim();
                        Plan.QunatityInCaseExport = int.Parse(dr["QuantityInCase"].ToString().Trim());
                        Plan.Quantity = dr["TotalUnitQuantity"].ToString().Trim();
                        Plan.PlanNoofBottling = dr["PlanNoofBottling"].ToString().Trim();
                        Plan.Status = dr["Status"].ToString().Trim();
                        Plan.NumberOfCases = int.Parse(dr["NumberOfCases"].ToString().Trim());
                        Plan.ProducedQunatityInCaseExport = Plan.QunatityInCaseExport;
                        Plan.ProducedNumberOfCases = int.Parse(dr["ProducedNumberOfCases"].ToString().Trim());
                        Plan.ProducedQunatityInCaseExport = int.Parse(dr["ProducedQunatityInCaseExport"].ToString().Trim());
                        Plan.ProducedBulkLiter = float.Parse(dr["ProducedBulkLitre"].ToString().Trim());
                        Plan.ProducedAlcoholicLiter = int.Parse(dr["ProducedAlcoholicLitre"].ToString().Trim());
                        Plan.ProducedTotalUnitQuantity = int.Parse(dr["ProducedTotalUnitQuantity"].ToString().Trim());
                        Plan.WastageInNumber = int.Parse(dr["WastageInNumber"].ToString().Trim());
                        Plan.WastageBL = int.Parse(dr["WastageBL"].ToString().Trim());
                        Plan.IsProductionFinal = short.Parse(dr["IsProductionFinal"].ToString().Trim());
                        Plan.TotalUnitQuantity = int.Parse(dr["TotalUnitQuantity"].ToString().Trim());
                       
                        BPList.Add(Plan);
                    }
                }
                catch (Exception exp) { }
            }
            return View(BPList);
        }
        [HttpGet]
        public  string   GetBrandDetailsForDDl(string BrandId)
        {
            string str = "";
            DataSet ds =  new CommonDA().GetBrandDetail(int.Parse(BrandId), "", "", "", -1, -1, -1,"Z");
            if (ds!=null && ds.Tables[0]!=null && ds.Tables[0].Rows.Count>0)
            {
                try
                {
                    str = ds.Tables[0].Rows[0]["LiquorType"].ToString().Trim()+","+ ds.Tables[0].Rows[0]["LicenceType"].ToString().Trim() + "," + ds.Tables[0].Rows[0]["LicenceNo"].ToString().Trim() + "," + ds.Tables[0].Rows[0]["QuantityInCase"].ToString().Trim() + "," + ds.Tables[0].Rows[0]["QuantityInBottleML"].ToString().Trim() + "," + ds.Tables[0].Rows[0]["AlcoholType"].ToString().Trim()+","+ ds.Tables[0].Rows[0]["stateId"].ToString().Trim();
                } 
                catch{
                    str = "";
                }
                 }
            return str;
        }
        public ActionResult ProductionEntry(string A)
        {
            BottelingPlan Plan = new BottelingPlan();
            List<SelectListItem> BrandList = new List<SelectListItem>();
            A = new Crypto().Decrypt(A);
            int Planid = int.Parse(A);
            string UserId = (Session["tbl_Session"] as DataTable).Rows[0]["UserId"].ToString().Trim();
            CMODataEntryBLL.bindDropDownHnGrid_db2("proc_ddlDetail", BrandList, "BR", UserId, "Z");
            DataSet ds = new CommonDA().GetProducePlanDetail(Convert.ToDateTime(String.Format("{0:dd/MM/yyyy}", "01/01/1900"), CultureInfo.CurrentCulture), DateTime.Now, -1, -1, "", "", "", Planid);
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                Plan.PlanId = int.Parse(ds.Tables[0].Rows[0]["PlanId"].ToString().Trim());
                Plan.Type = 4;
                //Plan.DateOfPlan = Convert.ToDateTime(ds.Tables[0].Rows[0]["DateofPlan1"].ToString().Trim());
                Plan.DateOfPlan1 = ds.Tables[0].Rows[0]["DateofPlan"].ToString().Trim();
                Plan.BatchNo = ds.Tables[0].Rows[0]["BatchNo"].ToString().Trim();
                Plan.BrandId = int.Parse(ds.Tables[0].Rows[0]["BrandId"].ToString().Trim());
                Plan.MappedOrNot = short.Parse(ds.Tables[0].Rows[0]["MappedOrNot"].ToString().Trim());
                Plan.LiquorType = ds.Tables[0].Rows[0]["LiquorType"].ToString().Trim();
                Plan.LicenceType = ds.Tables[0].Rows[0]["LicenceType"].ToString().Trim();
                Plan.LicenseNo = ds.Tables[0].Rows[0]["LicenceNo"].ToString().Trim();
                Plan.IsStateSame = bool.Parse(ds.Tables[0].Rows[0]["IsStateSame"].ToString().Trim());
                Plan.NumberOfCases = int.Parse(ds.Tables[0].Rows[0]["NumberOfCases"].ToString().Trim());
                Plan.QunatityInCaseExport = int.Parse(ds.Tables[0].Rows[0]["QunatityInCaseExport"].ToString().Trim());
                Plan.PlanNoofBottling = ds.Tables[0].Rows[0]["PlanNoofBottling"].ToString().Trim();
                Plan.QuantityInBottleML = ds.Tables[0].Rows[0]["QuantityInBottleML"].ToString().Trim();
                Plan.Strength = ds.Tables[0].Rows[0]["Strength"].ToString().Trim();
                Plan.BulkLiter = float.Parse(ds.Tables[0].Rows[0]["BulkLitre"].ToString().Trim());
                Plan.AlcoholicLiter = float.Parse(ds.Tables[0].Rows[0]["AlcoholicLiter"].ToString().Trim());
                Plan.TotalUnitQuantity = int.Parse(ds.Tables[0].Rows[0]["TotalUnitQuantity"].ToString().Trim());
                Plan.ProducedQunatityInCaseExport = int.Parse(ds.Tables[0].Rows[0]["ProducedQunatityInCaseExport"].ToString().Trim());
                if (Plan.ProducedQunatityInCaseExport==0)
                {
                    Plan.ProducedQunatityInCaseExport = Plan.QunatityInCaseExport;
                }
                Plan.ProducedNumberOfCases = int.Parse(ds.Tables[0].Rows[0]["ProducedNumberOfCases"].ToString().Trim());
                
                Plan.ProducedBulkLiter = float.Parse(ds.Tables[0].Rows[0]["ProducedBulkLitre"].ToString().Trim());
                Plan.ProducedAlcoholicLiter = int.Parse(ds.Tables[0].Rows[0]["ProducedAlcoholicLitre"].ToString().Trim());
                Plan.ProducedTotalUnitQuantity = int.Parse(ds.Tables[0].Rows[0]["ProducedTotalUnitQuantity"].ToString().Trim());
                Plan.WastageInNumber = int.Parse(ds.Tables[0].Rows[0]["WastageInNumber"].ToString().Trim());
                Plan.WastageBL = int.Parse(ds.Tables[0].Rows[0]["WastageBL"].ToString().Trim());
                Plan.IsProductionFinal = short.Parse(ds.Tables[0].Rows[0]["IsProductionFinal"].ToString().Trim());
                Plan.TotalRevenue = ds.Tables[0].Rows[0]["TotalRevenue"].ToString().Trim();
            }
            ViewBag.Brand = BrandList;
            return View(Plan);
        }
        public string FinalizePlan(string PlanId)
        {
            string str = "";
            try
            {
                PlanId = new Crypto().Decrypt(PlanId);
                BottelingPlan Plan = new BottelingPlan();
                Plan.Type = 3;
                Plan.PlanId = int.Parse(PlanId);
                Plan.AlcoholicLiter = 0;
                Plan.BatchNo = "";
                Plan.Brand = "";
                Plan.BrandId = 1;
                Plan.BreweryId = -1;
                Plan.BulkLiter = 0;
                Plan.Capacity = "";
                Plan.DateOfPlan = DateTime.Now;
                Plan.DateOfPlan1 = "";
                
                Plan.IsPlanFinal = 1;
                Plan.IsProductionFinal = 0;
                Plan.IsStateSame = false;
                Plan.LicenceType = "";
                Plan.LiquorType = "";
                Plan.MappedOrNot = 0;
                Plan.NumberOfCases = 0;
                Plan.PlanNoofBottling = "";
                Plan.ProducedAlcoholicLiter = 0;
                Plan.ProducedBulkLiter = 0;
                Plan.ProducedNumberOfCases = 0;
                Plan.ProducedQunatityInCaseExport = 0;
                Plan.ProducedTotalUnitQuantity = 0;
                Plan.ProductionforStateCode = 0;
                Plan.Quantity = "";
                Plan.QunatityInCaseExport = 0;
                Plan.TotalUnitQuantity = 0;
                Plan.WastageBL = 0;
                Plan.WastageInNumber = 0;
                str = new CommonDA().InsertUpdatePlan(Plan);
            }
            catch (Exception x)
            {
                str = x.Message.ToString();
            }
            return str;
        }

        [HttpPost]
        public ActionResult ProductionEntry(BottelingPlan BP)
        {
            string UserId = (Session["tbl_Session"] as DataTable).Rows[0]["UserId"].ToString().Trim();
           
            BP.Type = 1;
            string str = new CommonDA().InsertUpdateProductionPlan(BP);
            ViewBag.Msg = str;
            BottelingPlan Plan = new BottelingPlan();
            List<SelectListItem> BrandList = new List<SelectListItem>();
            int Planid = BP.PlanId;
            CMODataEntryBLL.bindDropDownHnGrid_db2("proc_ddlDetail", BrandList, "BR", UserId, "Z");
            DataSet ds = new CommonDA().GetProducePlanDetail(Convert.ToDateTime(String.Format("{0:dd/MM/yyyy}", "01/01/1900"), CultureInfo.CurrentCulture), DateTime.Now, -1, -1, "", "", "", Planid);
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                Plan.PlanId = int.Parse(ds.Tables[0].Rows[0]["PlanId"].ToString().Trim());
                Plan.Type = 4;
                //Plan.DateOfPlan = Convert.ToDateTime(ds.Tables[0].Rows[0]["DateofPlan1"].ToString().Trim());
                Plan.DateOfPlan1 = ds.Tables[0].Rows[0]["DateofPlan"].ToString().Trim();
                Plan.BatchNo = ds.Tables[0].Rows[0]["BatchNo"].ToString().Trim();
                Plan.BrandId = int.Parse(ds.Tables[0].Rows[0]["BrandId"].ToString().Trim());
                Plan.MappedOrNot = short.Parse(ds.Tables[0].Rows[0]["MappedOrNot"].ToString().Trim());
                Plan.LiquorType = ds.Tables[0].Rows[0]["LiquorType"].ToString().Trim();
                Plan.LicenceType = ds.Tables[0].Rows[0]["LicenceType"].ToString().Trim();
                Plan.LicenseNo = ds.Tables[0].Rows[0]["LicenceNo"].ToString().Trim();
                Plan.IsStateSame = bool.Parse(ds.Tables[0].Rows[0]["IsStateSame"].ToString().Trim());
                Plan.NumberOfCases = int.Parse(ds.Tables[0].Rows[0]["NumberOfCases"].ToString().Trim());
                Plan.QunatityInCaseExport = int.Parse(ds.Tables[0].Rows[0]["QunatityInCaseExport"].ToString().Trim());
                Plan.PlanNoofBottling = ds.Tables[0].Rows[0]["PlanNoofBottling"].ToString().Trim();
                Plan.QuantityInBottleML = ds.Tables[0].Rows[0]["QuantityInBottleML"].ToString().Trim();
                Plan.Strength = ds.Tables[0].Rows[0]["Strength"].ToString().Trim();
                Plan.BulkLiter = float.Parse(ds.Tables[0].Rows[0]["BulkLitre"].ToString().Trim());
                Plan.AlcoholicLiter = float.Parse(ds.Tables[0].Rows[0]["AlcoholicLiter"].ToString().Trim());
                Plan.TotalUnitQuantity = int.Parse(ds.Tables[0].Rows[0]["TotalUnitQuantity"].ToString().Trim());
                Plan.ProducedQunatityInCaseExport = int.Parse(ds.Tables[0].Rows[0]["ProducedQunatityInCaseExport"].ToString().Trim());
                if (Plan.ProducedQunatityInCaseExport == 0)
                {
                    Plan.ProducedQunatityInCaseExport = Plan.QunatityInCaseExport;
                }
                Plan.ProducedNumberOfCases = int.Parse(ds.Tables[0].Rows[0]["ProducedNumberOfCases"].ToString().Trim());

                Plan.ProducedBulkLiter = float.Parse(ds.Tables[0].Rows[0]["ProducedBulkLitre"].ToString().Trim());
                Plan.ProducedAlcoholicLiter = int.Parse(ds.Tables[0].Rows[0]["ProducedAlcoholicLitre"].ToString().Trim());
                Plan.ProducedTotalUnitQuantity = int.Parse(ds.Tables[0].Rows[0]["ProducedTotalUnitQuantity"].ToString().Trim());
                Plan.WastageInNumber = int.Parse(ds.Tables[0].Rows[0]["WastageInNumber"].ToString().Trim());
                Plan.WastageBL = int.Parse(ds.Tables[0].Rows[0]["WastageBL"].ToString().Trim());
                Plan.IsProductionFinal = short.Parse(ds.Tables[0].Rows[0]["IsProductionFinal"].ToString().Trim());
                Plan.TotalRevenue = ds.Tables[0].Rows[0]["TotalRevenue"].ToString().Trim();
            }
            ViewBag.Brand = BrandList;
            return View(Plan);
            
        }
        public string FreezePlanSuccess(string PlanId)
        {
            string str = "";
            try
            {
                PlanId = new Crypto().Decrypt(PlanId);
                BottelingPlan Plan = new BottelingPlan();
                Plan.Type = 2;
                Plan.PlanId = int.Parse(PlanId);
                
                Plan.ProducedNumberOfCases=0;
                Plan.ProducedQunatityInCaseExport=0;
                Plan.ProducedBulkLiter=0;
                Plan.ProducedAlcoholicLiter=0;
                Plan.ProducedTotalUnitQuantity=0;
                Plan.WastageInNumber=0;
                Plan.WastageBL=0;
                Plan.IsProductionFinal=1;
                str = new CommonDA().InsertUpdateProductionPlan(Plan);
            }
            catch (Exception x)
            {
                str = x.Message.ToString();
            }
            return str;
        }
        [HttpGet]
        public ActionResult GenerateQRCode()
        {
            List<BottelingPlan> BPList = new List<BottelingPlan>();
            List<SelectListItem> BrandList = new List<SelectListItem>();
            string UserId = (Session["tbl_Session"] as DataTable).Rows[0]["UserId"].ToString().Trim();
            CMODataEntryBLL.bindDropDownHnGrid_db2("proc_ddlDetail", BrandList, "BR", UserId, "Z");
            ViewBag.Brand = BrandList;
            DataSet ds = new CommonDA().GetQRCodeDetails(DateTime.Now, DateTime.Now, -1, -1, "Z", "Z", "Z", -1);
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                try
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        BottelingPlan Plan = new BottelingPlan();
                        Plan.PlanId = int.Parse(dr["PlanId"].ToString().Trim());
                        Plan.EncPlanId = new Crypto().Encrypt(dr["PlanId"].ToString().Trim());
                        Plan.DateOfPlan1 = (dr["DateOfPlan"].ToString().Trim());
                        Plan.LiquorType = dr["LiquorType"].ToString().Trim();
                        Plan.LicenceType = dr["LiquorType"].ToString().Trim();
                        Plan.Brand = dr["BrandName"].ToString().Trim();
                        Plan.Capacity = dr["Capacity"].ToString().Trim();
                        Plan.QunatityInCaseExport = int.Parse(dr["QuantityInCase"].ToString().Trim());
                        Plan.Quantity = dr["TotalUnitQuantity"].ToString().Trim();
                        Plan.PlanNoofBottling = dr["PlanNoofBottling"].ToString().Trim();
                        Plan.Status = dr["QRStatus"].ToString().Trim();
                        Plan.NumberOfCases = int.Parse(dr["NumberOfCases"].ToString().Trim());
                        Plan.TotalUnitQuantity = int.Parse(dr["TotalUnitQuantity"].ToString().Trim());
                        Plan.ProducedQunatityInCaseExport = Plan.QunatityInCaseExport;
                        Plan.ProducedNumberOfCases = int.Parse(dr["ProducedNumberOfCases"].ToString().Trim());
                        Plan.ProducedQunatityInCaseExport = int.Parse(dr["ProducedQunatityInCaseExport"].ToString().Trim());
                        Plan.ProducedBulkLiter = float.Parse(dr["ProducedBulkLitre"].ToString().Trim());
                        Plan.ProducedAlcoholicLiter = int.Parse(dr["ProducedAlcoholicLitre"].ToString().Trim());
                        Plan.ProducedTotalUnitQuantity = int.Parse(dr["ProducedTotalUnitQuantity"].ToString().Trim());
                        Plan.WastageInNumber = int.Parse(dr["WastageInNumber"].ToString().Trim());
                        Plan.WastageBL = int.Parse(dr["WastageBL"].ToString().Trim());
                        Plan.IsProductionFinal = short.Parse(dr["IsProductionFinal"].ToString().Trim());
                        BPList.Add(Plan);
                    }
                }
                catch (Exception exp) { }
            }
            return View(BPList);
        }

        public string GenreateQR(string PlanId)
        {
            string str = "";
            try
            {
                PlanId = new Crypto().Decrypt(PlanId);
                string UserId = (Session["tbl_Session"] as DataTable).Rows[0]["UserId"].ToString().Trim();
               str = new CommonDA().GenerateQRCode(int.Parse(PlanId), UserId,"");
            }
            catch (Exception x)
            {
                str = x.Message.ToString();
            }
            return str;
        }
       
        [HttpPost]
        public FileResult Export(string PlanId )
        {
            PlanId = new Crypto().Decrypt(PlanId);
             DataSet ds = new DataSet();
            ds = new CommonDA().GetQRCOde(int.Parse(PlanId));

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(ds.Tables[0]);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "QRCodeList.xlsx");
                }
            }
        }
        [HttpGet]
        public ActionResult Searchplan()
        {
            List<BottelingPlan> BPList = new List<BottelingPlan>();
            List<SelectListItem> BrandList = new List<SelectListItem>();
            string UserId = (Session["tbl_Session"] as DataTable).Rows[0]["UserId"].ToString().Trim();
            CMODataEntryBLL.bindDropDownHnGrid_db2("proc_ddlDetail", BrandList, "BR", UserId, "Z");
            ViewBag.Brand = BrandList;
            DataSet ds = new CommonDA().GetQRCodeDetails(DateTime.Now, DateTime.Now, -1, -1, "Z", "Z", "Z", -1);
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                try
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        BottelingPlan Plan = new BottelingPlan();
                        Plan.PlanId = int.Parse(dr["PlanId"].ToString().Trim());
                        Plan.EncPlanId = new Crypto().Encrypt(dr["PlanId"].ToString().Trim());
                        Plan.DateOfPlan1 = (dr["DateOfPlan"].ToString().Trim());
                        Plan.LiquorType = dr["LiquorType"].ToString().Trim();
                        Plan.LicenceType = dr["LiquorType"].ToString().Trim();
                        Plan.Brand = dr["BrandName"].ToString().Trim();
                        Plan.Capacity = dr["Capacity"].ToString().Trim();
                        Plan.QunatityInCaseExport = int.Parse(dr["QuantityInCase"].ToString().Trim());
                        Plan.Quantity = dr["TotalUnitQuantity"].ToString().Trim();
                        Plan.PlanNoofBottling = dr["PlanNoofBottling"].ToString().Trim();
                        Plan.Status = dr["Status"].ToString().Trim();
                        Plan.NumberOfCases = int.Parse(dr["NumberOfCases"].ToString().Trim());
                        BPList.Add(Plan);
                    }
                }
                catch (Exception exp) { }
            }
            return View(BPList);
        }
        [HttpGet]
        public ActionResult SearchProduction()
        {
            List<BottelingPlan> BPList = new List<BottelingPlan>();
            List<SelectListItem> BrandList = new List<SelectListItem>();
            string UserId = (Session["tbl_Session"] as DataTable).Rows[0]["UserId"].ToString().Trim();
            CMODataEntryBLL.bindDropDownHnGrid_db2("proc_ddlDetail", BrandList, "BR", UserId, "Z");
            ViewBag.Brand = BrandList;
            DataSet ds = new CommonDA().GetFinalizedPlan(DateTime.Now, DateTime.Now, -1, -1, "P", "Z", "Z", -1);
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                try
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        BottelingPlan Plan = new BottelingPlan();
                        Plan.PlanId = int.Parse(dr["PlanId"].ToString().Trim());
                        Plan.EncPlanId = new Crypto().Encrypt(dr["PlanId"].ToString().Trim());
                        Plan.DateOfPlan1 = (dr["DateOfPlan"].ToString().Trim());
                        Plan.LiquorType = dr["LiquorType"].ToString().Trim();
                        Plan.LicenceType = dr["LiquorType"].ToString().Trim();
                        Plan.Brand = dr["BrandName"].ToString().Trim();
                        Plan.Capacity = dr["Capacity"].ToString().Trim();
                        Plan.QunatityInCaseExport = int.Parse(dr["QuantityInCase"].ToString().Trim());
                        Plan.Quantity = dr["TotalUnitQuantity"].ToString().Trim();
                        Plan.PlanNoofBottling = dr["PlanNoofBottling"].ToString().Trim();
                        Plan.Status = dr["Status"].ToString().Trim();
                        Plan.NumberOfCases = int.Parse(dr["NumberOfCases"].ToString().Trim());
                        Plan.ProducedQunatityInCaseExport = Plan.QunatityInCaseExport;
                        Plan.ProducedNumberOfCases = int.Parse(dr["ProducedNumberOfCases"].ToString().Trim());
                        Plan.ProducedQunatityInCaseExport = int.Parse(dr["ProducedQunatityInCaseExport"].ToString().Trim());
                        Plan.ProducedBulkLiter = float.Parse(dr["ProducedBulkLitre"].ToString().Trim());
                        Plan.ProducedAlcoholicLiter = int.Parse(dr["ProducedAlcoholicLitre"].ToString().Trim());
                        Plan.ProducedTotalUnitQuantity = int.Parse(dr["ProducedTotalUnitQuantity"].ToString().Trim());
                        Plan.WastageInNumber = int.Parse(dr["WastageInNumber"].ToString().Trim());
                        Plan.WastageBL = int.Parse(dr["WastageBL"].ToString().Trim());
                        Plan.IsProductionFinal = short.Parse(dr["IsProductionFinal"].ToString().Trim());
                        Plan.TotalUnitQuantity = int.Parse(dr["TotalUnitQuantity"].ToString().Trim());
                        BPList.Add(Plan);
                    }
                }
                catch (Exception exp) { }
            }
            return View(BPList);
        }
        [HttpGet]
        public ActionResult GetFinalizedPlan()
        {
            List<BottelingPlan> BPList = new List<BottelingPlan>();
            List<SelectListItem> BrandList = new List<SelectListItem>();
            string UserId = (Session["tbl_Session"] as DataTable).Rows[0]["UserId"].ToString().Trim();
            CMODataEntryBLL.bindDropDownHnGrid_db2("proc_ddlDetail", BrandList, "BR", UserId, "Z");
            ViewBag.Brand = BrandList;
            DataSet ds = new CommonDA().GetFinalizedPlan(DateTime.Now, DateTime.Now, -1, -1, "P", "Z", "Z", -1);
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                try
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        BottelingPlan Plan = new BottelingPlan();
                        Plan.PlanId = int.Parse(dr["PlanId"].ToString().Trim());
                        Plan.EncPlanId = new Crypto().Encrypt(dr["PlanId"].ToString().Trim());
                        Plan.DateOfPlan1 = (dr["DateOfPlan"].ToString().Trim());
                        Plan.LiquorType = dr["LiquorType"].ToString().Trim();
                        Plan.LicenceType = dr["LiquorType"].ToString().Trim();
                        Plan.Brand = dr["BrandName"].ToString().Trim();
                        Plan.Capacity = dr["Capacity"].ToString().Trim();
                        Plan.QunatityInCaseExport = int.Parse(dr["QuantityInCase"].ToString().Trim());
                        Plan.Quantity = dr["TotalUnitQuantity"].ToString().Trim();
                        Plan.PlanNoofBottling = dr["PlanNoofBottling"].ToString().Trim();
                        Plan.Status = dr["Status"].ToString().Trim();
                        Plan.NumberOfCases = int.Parse(dr["NumberOfCases"].ToString().Trim());
                        Plan.ProducedQunatityInCaseExport = Plan.QunatityInCaseExport;
                        Plan.ProducedNumberOfCases = int.Parse(dr["ProducedNumberOfCases"].ToString().Trim());
                        Plan.ProducedQunatityInCaseExport = int.Parse(dr["ProducedQunatityInCaseExport"].ToString().Trim());
                        Plan.ProducedBulkLiter = float.Parse(dr["ProducedBulkLitre"].ToString().Trim());
                        Plan.ProducedAlcoholicLiter = int.Parse(dr["ProducedAlcoholicLitre"].ToString().Trim());
                        Plan.ProducedTotalUnitQuantity = int.Parse(dr["ProducedTotalUnitQuantity"].ToString().Trim());
                        Plan.WastageInNumber = int.Parse(dr["WastageInNumber"].ToString().Trim());
                        Plan.WastageBL = int.Parse(dr["WastageBL"].ToString().Trim());
                        Plan.IsProductionFinal = short.Parse(dr["IsProductionFinal"].ToString().Trim());
                        Plan.TotalUnitQuantity = int.Parse(dr["TotalUnitQuantity"].ToString().Trim());
                        BPList.Add(Plan);
                    }
                }
                catch (Exception exp) { }
            }
            return View(BPList);
        }

        [ActionName("GetFinalizedPlan")]
        [HttpPost]
        public ActionResult UploadExcel()
        {
            if (Request.Files["FileUpload1"].ContentLength > 0)
            {

            }
           return View();
        }
        public string FinalBrand(string BrandId)
        {
            string str = "";
            try
            {
                str = new CommonDA().UpdateBrand("",int.Parse(BrandId),1);
            }
            catch (Exception x)
            {
                str = x.Message.ToString();
            }
            return str;
        }
        public string DeleteBrand(string BrandId)
        {
            string str = "";
            try
            {
                str = new CommonDA().UpdateBrand("", int.Parse(BrandId), 2);
            }
            catch (Exception x)
            {
                str = x.Message.ToString();
            }
            return str;
        }
        public ActionResult Track_staus()
        {
            return View();
        }
        public ActionResult EditBrand(string Code)
        {
            BrandMaster Brand = new BrandMaster();
            List<SelectListItem> breweryList = new List<SelectListItem>();
            List<SelectListItem> StateList = new List<SelectListItem>();
            string UserId = (Session["tbl_Session"] as DataTable).Rows[0]["UserId"].ToString().Trim();
            CMODataEntryBLL.bindDropDownHnGrid("proc_ddlDetail", breweryList, "BRE", UserId, "Z");
            CMODataEntryBLL.bindDropDownHnGrid("proc_ddlDetail", StateList, "STATE", "", "S");
            //BrandMaster Brand = new BrandMaster();
            ViewBag.Brewery = breweryList;
            ViewBag.StateList = StateList;
            //Brand.BrandId = -1;
            //Brand.SPType = 1;
            //Brand.LiquorType = "BE";
            //Brand.LicenceType = "FL3";
            //Brand.QuantityInBottleML = 650;
            //Brand.StateId = -1;

            DataSet ds = new CommonDA().GetBrandDetail(int.Parse(new Crypto().Decrypt(Code.Trim())), "", "", "", -1, -1, -1,"Z");
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                Brand.AdditionalDuty = float.Parse(ds.Tables[0].Rows[0]["AdditionalDuty"].ToString().Trim());
                Brand.AlcoholType = (ds.Tables[0].Rows[0]["AlcoholType"].ToString().Trim());
                Brand.BrandId = int.Parse(ds.Tables[0].Rows[0]["BrandId"].ToString().Trim());
                // Brand.brandID_incrpt = float.Parse( ds.Tables[0].Rows[0]["AdditionalDuty"].ToString().Trim());
                Brand.BrandName = (ds.Tables[0].Rows[0]["BrandName"].ToString().Trim());
                Brand.BrandRegistrationNumber = (ds.Tables[0].Rows[0]["BrandRegistrationNumber"].ToString().Trim());
                Brand.BreweryId = short.Parse(ds.Tables[0].Rows[0]["BreweryId"].ToString().Trim());
                //Brand.dbName = float.Parse( ds.Tables[0].Rows[0]["AdditionalDuty"].ToString().Trim());
                Brand.ExciseDuty = float.Parse(ds.Tables[0].Rows[0]["ExciseDuty"].ToString().Trim());
                Brand.LicenceNo = ds.Tables[0].Rows[0]["LicenceNo"].ToString().Trim();
                Brand.LicenceType = ds.Tables[0].Rows[0]["LicenceType"].ToString().Trim();
                Brand.LiquorType = ds.Tables[0].Rows[0]["LiquorType"].ToString().Trim();
                Brand.MRP = float.Parse(ds.Tables[0].Rows[0]["MRP"].ToString().Trim());
                Brand.PackagingType = (ds.Tables[0].Rows[0]["PackagingType"].ToString().Trim());
                Brand.QuantityInBottleML = int.Parse(ds.Tables[0].Rows[0]["QuantityInBottleML"].ToString().Trim());
                Brand.QuantityInCase = int.Parse(ds.Tables[0].Rows[0]["QuantityInCase"].ToString().Trim());
                Brand.Remark = (ds.Tables[0].Rows[0]["Remark"].ToString().Trim());
                Brand.SPType = 2;// int.Parse(ds.Tables[0].Rows[0]["SPType"].ToString().Trim());
                Brand.StateId = int.Parse(ds.Tables[0].Rows[0]["StateId"].ToString().Trim());
                Brand.Strength = float.Parse(ds.Tables[0].Rows[0]["Strength"].ToString().Trim());
                Brand.XFactoryPrice = float.Parse(ds.Tables[0].Rows[0]["XFactoryPrice"].ToString().Trim());
            }
            return View(Brand);
        }



       
        public ActionResult UploadCSVFile()
        {
            return RedirectToAction("GetFinalizedPlan");
        }
        [ActionName("UploadCSVFile")]
        [HttpPost]
        public ActionResult UploadCSVFile1()
        {
            if (Request.Files["impCSVUpload"].ContentLength > 0)
            {
                string extension = System.IO.Path.GetExtension(Request.Files["impCSVUpload"].FileName).ToLower();

                string path1 = string.Format("{0}/{1}", Server.MapPath("~/Content/Uploads"), Request.Files["impCSVUpload"].FileName);
                if (!Directory.Exists(path1))
                {
                    Directory.CreateDirectory(Server.MapPath("~/Content/Uploads"));
                }
                if (extension == ".csv")
                {
                    Request.Files["impCSVUpload"].SaveAs(path1);
                    var csv = new List<string[]>();
                    var lines = System.IO.File.ReadAllLines(path1);
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    int count = 0;
                    foreach (string line in lines)
                    {
                        if (count == 0)
                        {
                            //sb.Append("'");
                            sb.Append(line);
                            //sb.Append("'");
                            count++;
                        }
                        else
                        {
                            sb.Append(",");
                            //sb.Append("'");
                            sb.Append(line);
                            //sb.Append("'");
                        }
                    }

                    var uploadCSV = new CommonDA();
                    uploadCSV.UploadCSV(sb.ToString());
                    ViewBag.Error = "File Upload Successfully";

                }
                else
                {
                    ViewBag.Error = "Please Upload Files in .csv format";

                }

            }

            return RedirectToAction("GetFinalizedPlan");
        }
    }

}
