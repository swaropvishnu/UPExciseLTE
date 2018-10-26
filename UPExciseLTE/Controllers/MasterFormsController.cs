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

namespace UPExciseLTE.Controllers
{
    public class MasterFormsController : Controller
    {
        //
        // GET: /MasterForms/

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult BrandMaster()
        {
            List<SelectListItem> breweryList = new List<SelectListItem>();
            string UserId = (Session["tbl_Session"] as DataTable).Rows[0]["UserId"].ToString().Trim();
            CMODataEntryBLL.bindDropDownHnGrid("proc_ddlDetail", breweryList, "BRE", UserId, "Z");
            BrandMaster Brand = new BrandMaster();
            ViewBag.Brewery = breweryList;
            Brand.BrandId = -1;
            Brand.SPType = 1;
            Brand.LiquorType = "BE";
            return View(Brand);
        }
        public ActionResult BrandMaster(int BrandId)
        {
            List<SelectListItem> breweryList = new List<SelectListItem>();
            string UserId = (Session["tbl_Session"] as DataTable).Rows[0]["UserId"].ToString().Trim();
            CMODataEntryBLL.bindDropDownHnGrid("proc_ddlDetail", breweryList, "BRE", UserId, "Z");
            BrandMaster Brand = new BrandMaster();
            Brand.BrandId = -1;
            Brand.SPType = 1;
            ViewBag.Brewery = breweryList;
            return View(Brand);
        }
        
        [HttpPost]
        public ActionResult BrandMaster(BrandMaster B)
        {
            B.dbName = "be_unnao1";
            if (B.ExiciseTin==null)
            {
                B.ExiciseTin = "";
            }
            if (B.Remark==null)
            {
                B.Remark = "";
            }
            string str = new CommonDA().InsertUpdateBrand(B);
            /*Save Record In Database*/
            ViewData["Result"] = str;
            List<SelectListItem> breweryList = new List<SelectListItem>();
            string UserId = (Session["tbl_Session"] as DataTable).Rows[0]["UserId"].ToString().Trim();
            CMODataEntryBLL.bindDropDownHnGrid("proc_ddlDetail", breweryList, "BRE", UserId, "Z");
            BrandMaster Brand = new BrandMaster();
            Brand.BrandId = -1;
            Brand.SPType = 1;
            ViewBag.Brewery = breweryList;
            return View(Brand);
        }
        [HttpGet]
        public ActionResult GetBrandDetails()
        {
            List<BrandMaster> lstBrand = new List<BrandMaster>();
            List<SelectListItem> breweryList = new List<SelectListItem>();
            string UserId = (Session["tbl_Session"] as DataTable).Rows[0]["UserId"].ToString().Trim();
            CMODataEntryBLL.bindDropDownHnGrid("proc_ddlDetail", breweryList, "BRE", UserId, "Z");
            ViewBag.Brewery = breweryList;
            List<SelectListItem> DistrictList = new List<SelectListItem>();
            ViewBag.District = DistrictList;
            List<SelectListItem> TehsilList = new List<SelectListItem>();
            ViewBag.Tehsil = TehsilList;
            DataSet ds =  new CommonDA().GetBrandDetail(-1, "", "", "", -1, -1, -1);
            if (ds!=null && ds.Tables[0]!=null && ds.Tables[0].Rows.Count>0)
            {
                try
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        BrandMaster brand = new BrandMaster();
                        brand.AdditionalDuty = float.Parse(dr["AdditionalDuty"].ToString().Trim());
                        brand.BrandId = int.Parse(dr["BrandId"].ToString().Trim());
                        brand.BrandName = dr["BrandName"].ToString().Trim();
                        brand.BrandRegistrationNumber = dr["BrandRegistrationNumber"].ToString().Trim();
                        brand.BreweryId =short.Parse(dr["BreweryId"].ToString().Trim());
                        brand.ExciseDuty = float.Parse(dr["ExciseDuty"].ToString().Trim());
                        brand.ExiciseTin = dr["ExiciseTin"].ToString().Trim();
                        brand.ExportBoxSize = int.Parse(dr["ExportBoxSize"].ToString().Trim());
                        brand.LicenceNo = dr["LicenceNo"].ToString().Trim();
                        brand.LicenceType = dr["LicenceType"].ToString().Trim();
                        brand.LiquorType = dr["LiquorType"].ToString().Trim();
                        brand.MRP = float.Parse(dr["MRP"].ToString().Trim());
                        brand.QuantityInBottleML = int.Parse(dr["QuantityInBottleML"].ToString().Trim());
                        brand.QuantityInCase = int.Parse(dr["QuantityInCase"].ToString().Trim());
                        brand.Remark = dr["Remark"].ToString().Trim();
                        brand.Strength = float.Parse(dr["Strength"].ToString().Trim());
                        brand.XFactoryPrice = float.Parse(dr["XFactoryPrice"].ToString().Trim());
                        lstBrand.Add(brand);
                    }
                }
                catch (Exception exp) { } 
            }
            return View(lstBrand);
        }
         
        [HttpGet]
        public ActionResult BottelingPlan()
        {
            BottelingPlan Plan = new BottelingPlan();
            List<SelectListItem> BrandList = new List<SelectListItem>();
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
            ViewBag.Brand = BrandList;
            BP.dbName = "be_unnao1";
            string str = new CommonDA().InsertUpdatePlan(BP);
            ViewBag.Msg = str;
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
                Plan.Strength = ds.Tables[0].Rows[0]["Strength"].ToString().Trim();
                Plan.BulkLiter = float.Parse(ds.Tables[0].Rows[0]["BulkLitre"].ToString().Trim());
                Plan.AlcoholicLiter = float.Parse(ds.Tables[0].Rows[0]["AlcoholicLiter"].ToString().Trim());
                Plan.TotalUnitQuantity = int.Parse(ds.Tables[0].Rows[0]["TotalUnitQuantity"].ToString().Trim());
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
            BP.dbName = "be_unnao1";
            BP.Type = 2;
            BP.DateOfPlan = Convert.ToDateTime(String.Format("{0:dd/MM/yyyy}", BP.DateOfPlan1));
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
            DataSet ds =  new CommonDA().GetBrandDetail(int.Parse(BrandId), "", "", "", -1, -1, -1);
            if (ds!=null && ds.Tables[0]!=null && ds.Tables[0].Rows.Count>0)
            {
                try
                {
                    str = ds.Tables[0].Rows[0]["LiquorType"].ToString().Trim()+","+ ds.Tables[0].Rows[0]["LicenceType"].ToString().Trim() + "," + ds.Tables[0].Rows[0]["LicenceNo"].ToString().Trim() + "," + ds.Tables[0].Rows[0]["QuantityInCase"].ToString().Trim() + "," + ds.Tables[0].Rows[0]["QuantityInBottleML"].ToString().Trim() + "," + ds.Tables[0].Rows[0]["Strength"].ToString().Trim();
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
                Plan.ProducedQunatityInCaseExport= Plan.QunatityInCaseExport;
                Plan.ProducedNumberOfCases = int.Parse(ds.Tables[0].Rows[0]["ProducedNumberOfCases"].ToString().Trim());
                Plan.ProducedQunatityInCaseExport = int.Parse(ds.Tables[0].Rows[0]["ProducedQunatityInCaseExport"].ToString().Trim());
                Plan.ProducedBulkLiter = float.Parse(ds.Tables[0].Rows[0]["ProducedBulkLitre"].ToString().Trim());
                Plan.ProducedAlcoholicLiter = int.Parse(ds.Tables[0].Rows[0]["ProducedAlcoholicLitre"].ToString().Trim());
                Plan.ProducedTotalUnitQuantity = int.Parse(ds.Tables[0].Rows[0]["ProducedTotalUnitQuantity"].ToString().Trim());
                Plan.WastageInNumber = int.Parse(ds.Tables[0].Rows[0]["WastageInNumber"].ToString().Trim());
                Plan.WastageBL = int.Parse(ds.Tables[0].Rows[0]["WastageBL"].ToString().Trim());
                Plan.IsProductionFinal = short.Parse(ds.Tables[0].Rows[0]["IsProductionFinal"].ToString().Trim());
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
                Plan.dbName = "be_unnao1";
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
            BP.dbName = "be_unnao1";
            BP.Type = 1;
            string str = new CommonDA().InsertUpdateProductionPlan(BP);
            ViewBag.Msg = str;
            return View(BP);
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
                Plan.dbName= "be_unnao1";
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
        public ActionResult DownloadExcel(string PlanId)
        {
            try
            {
                PlanId = new Crypto().Decrypt(PlanId);
                DataSet ds = new DataSet();
                ds = new CommonDA().GetQRCOde(int.Parse(PlanId));
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(ds.Tables[0]);
                    wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    wb.Style.Font.Bold = true;

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename= EmployeeReport.xlsx");

                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }
            }
            catch { }
            return RedirectToAction("GenerateQRCode", "MasterFormsController");
        }
    }
}
