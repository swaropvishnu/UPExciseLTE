using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using System.Web.Mvc;
using UPExciseLTE.BLL;
using UPExciseLTE.DAL;
using UPExciseLTE.Filters;
using UPExciseLTE.Models;
using ZXing;

namespace UPExciseLTE.Controllers
{  
    
    [SessionExpireFilterAttribute]
    [NoCache]
    [ChkAuthorization]
    public class MasterFormsController : Controller
    {
        public ActionResult MenuMaster()
        {
            List<MenuMst> lstMst = new CommonDA().GetMenuMaster(-1, -1, "Z", "Z", "Z");
            return View(lstMst);
        }
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public ActionResult BrandMaster()
        {
            BrandMaster Brand = new BrandMaster();
            List<SelectListItem> lstLicense = new List<SelectListItem>();
            List<SelectListItem> lstLiquor = new List<SelectListItem>();
            SelectListItem SLI = new SelectListItem();
            SelectListItem LT = new SelectListItem();
            //if (UserSession.PushName.Trim()== "be_unnao1")
            if (UserSession.LoggedInUserLevelId.Trim() == "15") //ex BreweryUnnao1
            {
                SLI = new SelectListItem();
                SLI.Text = "FL3";
                SLI.Value = "FL3";
                lstLicense.Add(SLI);

                SLI = new SelectListItem();
                SLI.Text = "FL3A";
                SLI.Value = "FL3A";
                lstLicense.Add(SLI);

                LT = new SelectListItem();
                LT.Text = "Beer";
                LT.Value = "BE";
                lstLiquor.Add(LT);
            }
            else if (UserSession.LoggedInUserLevelId.Trim() == "50") // ex BWFLLucknow1
            {
                SLI = new SelectListItem();
                SLI.Text = "BWFL-2B";
                SLI.Value = "BWFL-2B";
                lstLicense.Add(SLI);

                LT = new SelectListItem();
                LT.Text = "Beer";
                LT.Value = "BE";
                lstLiquor.Add(LT);

            }
            else if (UserSession.LoggedInUserLevelId.Trim() == "25") // ex PD2_Unnao1
            {
                SLI = new SelectListItem();
                SLI.Text = "PD-2";
                SLI.Value = "PD-2";
                lstLicense.Add(SLI);

                LT.Text = "Country Liquor";
                LT.Value = "CL";
                lstLiquor.Add(LT);

            }
            else if (UserSession.LoggedInUserLevelId.Trim() == "55") // ex BWFL2D
            {
                SLI = new SelectListItem();
                SLI.Text = "PD-2";
                SLI.Value = "PD-2";
                lstLicense.Add(SLI);

                LT.Text = "Foreign Liquor";
                LT.Value = "FL";
                lstLiquor.Add(LT);

            }
            else
            {
                SLI = new SelectListItem();
                SLI.Text = "FL3";
                SLI.Value = "FL3";
                lstLicense.Add(SLI);

                SLI = new SelectListItem();
                SLI.Text = "FL3A";
                SLI.Value = "FL3A";
                lstLicense.Add(SLI);

                LT = new SelectListItem();
                LT.Text = "Beer";
                LT.Value = "BE";
                lstLiquor.Add(LT);

            }
            if (Request.QueryString["Code"] != null && Request.QueryString["Code"].Trim() != string.Empty)
            {
                Brand = new CommonBL().GetBrand(int.Parse(new Crypto().Decrypt(Request.QueryString["Code"].Trim())), "", "", "", -1, -1, -1, "Z");
                if (Brand.LicenceType != string.Empty)
                {
                    if (Brand.LicenceType == "FL3A" || Brand.LicenceType == "FL3")
                    {
                        lstLicense.Clear();

                        SLI = new SelectListItem();
                        SLI.Text = "FL3";
                        SLI.Value = "FL3";
                        lstLicense.Add(SLI);

                        SLI = new SelectListItem();
                        SLI.Text = "FL3A";
                        SLI.Value = "FL3A";
                        lstLicense.Add(SLI);

                    }
                    else if (Brand.LicenceType == "BWFL-2B")
                    {
                        lstLicense.Clear();

                        SLI = new SelectListItem();
                        SLI.Text = "BWFL-2B";
                        SLI.Value = "BWFL-2B";
                        lstLicense.Add(SLI);
                    }
                    else if (Brand.LicenceType == "PD-2")
                    {
                        lstLicense.Clear();

                        SLI = new SelectListItem();
                        SLI.Text = "PD-2";
                        SLI.Value = "PD-2";
                        lstLicense.Add(SLI);
                    }
                }
                if (Brand.LiquorType != string.Empty)
                {
                    if (Brand.LiquorType == "BE")
                    {
                        lstLiquor.Clear();
                        LT = new SelectListItem();
                        LT.Text = "Beer";
                        LT.Value = "BE";
                        lstLiquor.Add(LT);
                    }
                    else if (Brand.LiquorType == "CL")
                    {
                        lstLiquor.Clear();
                        LT = new SelectListItem();
                        LT.Text = "Country Liquor";
                        LT.Value = "CL";
                        lstLiquor.Add(LT);
                    }
                    else if (Brand.LiquorType == "FL")
                    {
                        lstLiquor.Clear();
                        LT = new SelectListItem();
                        LT.Text = "Foreign Liquor";
                        LT.Value = "FL";
                        lstLiquor.Add(LT);
                    }

                }
            }

            ViewBag.LiquorType = lstLiquor;
            ViewBag.LicenseType = lstLicense;
            if (UserSession.LoggedInUserLevelId.Trim() != "25")
            {
                DataSet ds = new CommonDA().GetDutyCalculation(DateTime.Now.Year.ToString(), "BE", 0);
                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    ViewBag.MConsidrationFees = ds.Tables[0].Rows[0]["ConsidrationFees"].ToString().Trim();
                    ViewBag.SConsidrationFees = ds.Tables[0].Rows[1]["ConsidrationFees"].ToString().Trim();
                    ViewBag.MWholeSaleMargin = ds.Tables[0].Rows[0]["WholeSaleMargin"].ToString().Trim();
                    ViewBag.SWholeSaleMargin = ds.Tables[0].Rows[1]["WholeSaleMargin"].ToString().Trim();
                    ViewBag.MRetailerMargin = ds.Tables[0].Rows[0]["RetailerMargin"].ToString().Trim();
                    ViewBag.SRetailerMargin = ds.Tables[0].Rows[1]["RetailerMargin"].ToString().Trim();
                }
            }
            string UserId = UserSession.LoggedInUserId.ToString().Trim();
            ViewBag.Brewery = CommonBL.fillBrewery();
            ViewBag.StateList = CommonBL.fillState("S");
            ViewBag.Msg = TempData["Message"];
            return View(Brand);
        }
        public JsonResult GetDutyCalculation(string AlcoholStrength)
        {
            string[] str = new string[11];
            DataSet ds = new CommonDA().GetDutyCalculation(DateTime.Now.Year.ToString(), "CL", decimal.Parse(AlcoholStrength.Trim()));
            if (ds != null)
            {
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    // CalculationId,FiscalYear,LiquorType,Capacity,StrengthType,Strength,XFactoryPrice,ConsidrationFees,WholeSaleMargin,WHPrice,RetailerMargin,MaxRetPrice,MRP,AdditionalConsiderationFees,TotalConsiderationFee,ActualMaximumWholeSalePrice
                    DataView dv = ds.Tables[0].DefaultView;
                    dv.RowFilter = "Strength ='" + AlcoholStrength + "'";
                    DataTable dt = dv.ToTable();
                    str[0] = dt.Rows[0]["Strength"].ToString().Trim();
                    str[1] = dt.Rows[0]["XFactoryPrice"].ToString().Trim();
                    str[2] = dt.Rows[0]["ConsidrationFees"].ToString().Trim();
                    str[3] = dt.Rows[0]["WholeSaleMargin"].ToString().Trim();
                    str[4] = dt.Rows[0]["WHPrice"].ToString().Trim();
                    str[5] = dt.Rows[0]["RetailerMargin"].ToString().Trim();
                    str[6] = dt.Rows[0]["MaxRetPrice"].ToString().Trim();
                    str[7] = dt.Rows[0]["MRP"].ToString().Trim();
                    str[8] = dt.Rows[0]["AdditionalConsiderationFees"].ToString().Trim();
                    str[9] = dt.Rows[0]["TotalConsiderationFee"].ToString().Trim();
                    str[10] = dt.Rows[0]["ActualMaximumWholeSalePrice"].ToString().Trim();

                }
            }
            return Json(str, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BrandMaster(BrandMaster B)
        {

            if (B.Remark == null)
            {
                //TempData["Message"] = "Please Submit Remark";
                //return RedirectToAction("BrandMaster",new {BrandMaster=B });
                B.Remark = "";
            }
            string str = new CommonDA().InsertUpdateBrand(B);
            TempData["Message"] = str;
            if (B.SPType == 1)
            {
                return RedirectToAction("BrandMaster");
            }
            else
            {
                return RedirectToAction("BrandMaster", new { Code = B.brandID_incrpt });
            }
        }
        [HttpGet]
        public ActionResult GetBrandDetails()
        {
            ViewBag.Brewery = CommonBL.fillBrewery();
            List<BrandMaster> lstBrand = new CommonBL().GetBrandList(-1, "", "", "", short.Parse(CommonBL.fillBrewery()[0].Value), -1, -1, "Z");

            ViewBag.StateList = CommonBL.fillState("A");
            return View(lstBrand);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetBrandDetails(FormCollection frm)
        {
            int StateId = -1;// int.Parse(frm["ddlState"].Trim());
            string LicenseType = "";// frm["ddlLicenseType"].Trim();
            string LicenseNo = "";// frm["txtLicenseNo"].Trim();
            List<BrandMaster> lstBrand = new CommonBL().GetBrandList(-1, "", LicenseType, LicenseNo, short.Parse(CommonBL.fillBrewery()[0].Value), -1, StateId, "Z");
            ViewBag.Brewery = CommonBL.fillBrewery();
            ViewBag.StateList = CommonBL.fillState("A");
            return View(lstBrand);
        }
        [HttpGet]
        public ActionResult FinalizeBrand(string LicenseType)
        {
            List<SelectListItem> lstLicenseType = new List<SelectListItem>();
            SelectListItem LT = new SelectListItem();
            LT = new SelectListItem();
            LT.Text = "FL3";
            LT.Value = "FL3";
            lstLicenseType.Add(LT);
            ViewBag.LicenseType = lstLicenseType;

            ViewBag.Brewery = CommonBL.fillBrewery();
            ViewBag.District = CommonBL.fillState("S");
            if (LicenseType == null)
            {
                List<BrandMaster> lstBrand = new CommonBL().GetBrandList(-1, "", "", "", -1, -1, -1, "P");
                return View(lstBrand);
            }
            else
            {
                List<BrandMaster> lstBrand = new CommonBL().GetBrandList(-1, "", LicenseType, "", -1, -1, -1, "P");
                return View(lstBrand);
            }
        }
        public string FinalBrand(string BrandId, string Status, string Reason)
        {
            string str = "";
            int TypeId = -1;
            try
            {
                if (Status == "A")
                { TypeId = 1; }
                else
                { TypeId = 3; }
                str = new CommonDA().UpdateBrand(int.Parse(BrandId), TypeId, Reason);
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
                str = new CommonDA().UpdateBrand(int.Parse(BrandId), 2, "Deleted Brand");
            }
            catch (Exception x)
            {
                str = x.Message.ToString();
            }
            return str;
        }
        [HttpGet]
        public ActionResult BottelingPlan()
        {
            BottelingPlan BP = new BottelingPlan();
            ViewBag.Msg = TempData["Message"];
            ViewBag.Brand = CommonBL.fillBrand("S");
            List<SelectListItem> lstBBT = CommonBL.fillBBT("S");
            ViewBag.BBT = lstBBT;
            BBTMaster bbtFormation = new CommonBL().GetBBTMasterList(int.Parse(lstBBT[0].Value), "A")[0];

            ViewBag.BottlingLine = CommonBL.BottlingLine("Z", lstBBT[0].Value);
            BP.BBTBulkLitre = decimal.Parse(bbtFormation.BBTBulkLitre.ToString());

            ViewBag.Msg = TempData["Message"];
            if (Request.QueryString["A"] != null && Request.QueryString["A"].ToString().Trim() != string.Empty)
            {
                BP = (new CommonBL().GetBottelingPlan(CommonBL.Setdate("01/01/1900"), DateTime.Now, short.Parse(CommonBL.fillBrewery()[0].Value), -1, "", "", int.Parse(new Crypto().Decrypt(Request.QueryString["A"].Trim())), "PB"));
            }
            return View(BP);
        }
        public ActionResult GetBottlingTankForddl(string ddlBBT)
        {
            return Json(CommonBL.BottlingLine("S", ddlBBT), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public string GetBrandDetailsForDDl(string BrandId)
        {
            string str = "";
            DataSet ds = new CommonDA().GetBrandDetail(int.Parse(BrandId), "", "", "", short.Parse(CommonBL.fillBrewery()[0].Value), -1, -1, "A");
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                try
                {
                    str = ds.Tables[0].Rows[0]["LiquorType"].ToString().Trim() + "," + ds.Tables[0].Rows[0]["LicenceType"].ToString().Trim() + "," + ds.Tables[0].Rows[0]["LicenceNo"].ToString().Trim() + "," + ds.Tables[0].Rows[0]["QuantityInCase"].ToString().Trim() + "," + ds.Tables[0].Rows[0]["QuantityInBottleML"].ToString().Trim() + "," + ds.Tables[0].Rows[0]["AlcoholType"].ToString().Trim() + "," + ds.Tables[0].Rows[0]["StateName"].ToString().Trim();
                }
                catch
                {
                    str = "";
                }
            }
            return str;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BottelingPlan(BottelingPlan BP)
        {
            try
            {
                BP.DateOfPlan = CommonBL.Setdate(BP.DateOfPlan1);
            }
            catch
            {
                ViewBag.Msg = "Invalid Date. Please use dd/MM/yyyy format.";
                return View(BP);
            }

            string str = new CommonDA().InsertUpdatePlan(BP);
            TempData["Message"] = str;
            if (BP.Type == 1)
            {
                return RedirectToAction("BottelingPlan");
            }
            else
            {
                return RedirectToAction("BottelingPlan", new { A = BP.EncPlanId });
            }
        }
        [HttpGet]
        public ActionResult EditFinalBottelingPlan()
        {
            return View(new CommonBL().GetBottelingPlanList(CommonBL.Setdate("01/01/1900"), DateTime.Now, short.Parse(CommonBL.fillBrewery()[0].Value), -1, "Z", "", -1, "PB"));
        }
        public string FinalizePlan(string PlanId)
        {
            string str = "";
            try
            {
                BottelingPlan Plan = new BottelingPlan();
                PlanId = new Crypto().Decrypt(PlanId);
                Plan.Type = 3;
                Plan.PlanId = int.Parse(PlanId);
                Plan.IsPlanFinal = 1;
                str = new CommonDA().InsertUpdatePlan(Plan);
            }
            catch (Exception x)
            {

                str = x.Message.ToString();
            }
            return str;
        }
        [HttpGet]
        public ActionResult Searchplan()
        {
            List<SelectListItem> BrandList = new List<SelectListItem>();
            ViewBag.Brand = CommonBL.fillBrand("A");
            return View(new CommonBL().GetBottelingPlanList(DateTime.Now, DateTime.Now, short.Parse(CommonBL.fillBrewery()[0].Value), -1, "Z", "", -1, "Z"));
        }
        public ActionResult SearchplanFilter(string FromDate, string ToDate)
        {
            try
            {
                return View(new CommonBL().GetBottelingPlanList(CommonBL.Setdate(FromDate), CommonBL.Setdate(ToDate), short.Parse(CommonBL.fillBrewery()[0].Value), -1, "Z", "", -1, "Z"));
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        [HttpGet]
        public ActionResult GenerateQRCode()
        {
            ViewBag.Brand = CommonBL.fillBrand("A");
            return View(new CommonBL().GetBottelingPlanList(CommonBL.Setdate("01/01/1900"), DateTime.Now, short.Parse(CommonBL.fillBrewery()[0].Value), -1, "Z", "", -1, "Z"));
        }
        public string GenreateQR(string PlanId)
        {
            string str = "";
            try
            {
                PlanId = new Crypto().Decrypt(PlanId);
                string UserId = (Session["tbl_Session"] as DataTable).Rows[0]["UserId"].ToString().Trim();
                str = new CommonDA().GenerateQRCode(int.Parse(PlanId), UserId, "");
            }
            catch (Exception x)
            {
                str = x.Message.ToString();
            }
            return str;
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public FileResult Export(string PlanId)
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
        public ActionResult ProductionPlanList()
        {
            List<BottelingPlan> BPList = new List<BottelingPlan>();
            ViewBag.Brand = CommonBL.fillBrand("A");
            return View(new CommonBL().GetBottelingPlanList(CommonBL.Setdate("01/01/1900"), DateTime.Now, short.Parse(CommonBL.fillBrewery()[0].Value), -1, "Z", "", -1, "FB"));
        }
        public ActionResult ProductionEntry(string A)
        {
            ViewBag.Msg = "";
            A = new Crypto().Decrypt(A);
            int Planid = int.Parse(A);
            ViewBag.Msg = TempData["Message"];
            return View(new CommonBL().GetBottelingPlan(CommonBL.Setdate("01/01/1900"), DateTime.Now, short.Parse(CommonBL.fillBrewery()[0].Value), -1, "Z", "", Planid, "FB"));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductionEntry(BottelingPlan BP)
        {
            string UserId = (Session["tbl_Session"] as DataTable).Rows[0]["UserId"].ToString().Trim();
            BP.Type = 1;
            string str = new CommonDA().InsertUpdateProductionPlan(BP);
            TempData["Message"] = str;
            return RedirectToAction("ProductionEntry", new { A = BP.EncPlanId });
        }
        [HttpGet]
        public ActionResult FreezePlan()
        {
            return View(new CommonBL().GetBottelingPlanList(CommonBL.Setdate("01/01/1900"), DateTime.Now, short.Parse(CommonBL.fillBrewery()[0].Value), -1, "Z", "", -1, "FB"));
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
                Plan.IsProductionFinal = 1;
                str = new CommonDA().InsertUpdateProductionPlan(Plan);
            }
            catch (Exception x)
            {
                str = x.Message.ToString();
            }
            return str;
        }
        [HttpGet]
        public ActionResult UploadCSV()
        {
            List<BottelingPlan> lstBotteling = new CommonBL().GetBottelingPlanList(CommonBL.Setdate("01/01/1900"), DateTime.Now, -1, -1, "Z", "", -1, "FP");
            if (TempData["Message"] != null)
            {
                var str = TempData["Message"].ToString();
                if (!string.IsNullOrEmpty(str))
                {
                    ViewBag.Msg = TempData["Msg"];
                    //lstBotteling[0].Msg = Message.MsgSuccess(str);
                }
            }
            return View(lstBotteling);
        }
        [HttpGet]
        public ActionResult SearchProduction()
        {
            try
            {
                ViewBag.Brand = CommonBL.fillBrand("A");
                return View(new CommonBL().GetBottelingPlanList(CommonBL.Setdate("01/01/1900"), DateTime.Now, short.Parse(CommonBL.fillBrewery()[0].Value), -1, "Z", "", -1, "Z"));
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        public ActionResult SearchProducationFilter(string FromDate, string ToDate)
        {
            try
            {
                ViewBag.Brand = CommonBL.fillBrand("A");
                return View(new CommonBL().GetBottelingPlanList(CommonBL.Setdate(FromDate), CommonBL.Setdate(ToDate), short.Parse(CommonBL.fillBrewery()[0].Value), -1, "Z", "", -1, "Z"));
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        public ActionResult Track_staus()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult UploadCSVFile()
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    string str = "";
                    if (Request.Files[0] != null)
                    {

                        HttpFileCollectionBase files = Request.Files;
                        HttpPostedFileBase file = files[0];
                        str = CSV.ValidateCSV(1, int.Parse(new Crypto().Decrypt(files.Keys[0])), -1, file);
                    }
                    return Json(str);
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
        /*
         [HttpGet]
         public ActionResult FreezeBrand()
         {
             ViewBag.Brewery = CommonBL.fillBrewery();
             ViewBag.District = CommonBL.fillState("S");
             List<BrandMaster> lstBrand = new CommonBL().GetBrandList(-1, "", "", "", -1, -1, -1, "P");
             return View(lstBrand);
         }*/
        //New Work
        [HttpGet]
        public ActionResult UnitTank()
        {
            UnitTank UT = new UnitTank();
            if (Request.QueryString["A"] != null && Request.QueryString["A"].Trim() != string.Empty)
            {

                UT = new CommonBL().GetUnitTank(-1, short.Parse(new Crypto().Decrypt(Request.QueryString["A"].Trim())), "Z");
            }
            ViewBag.Msg = TempData["Msg"];
            ViewBag.Brewery = CommonBL.fillBrewery();
            ViewBag.UnitTank = new CommonBL().GetUnitTankDetails(short.Parse(CommonBL.fillBrewery()[0].Value.Trim()), -1, "Z");
            return View(UT);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UnitTank(UnitTank UT)
        {
            string str = new CommonDA().InsertUpdateUnitTank(UT);
            TempData["Msg"] = str;
            return RedirectToAction("UnitTank");
        }
        public string UpdateUnitTank(string UTId, string Status)
        {
            UnitTank UT = new UnitTank();
            UT.UnitTankId = int.Parse(new Crypto().Decrypt(UTId));
            UT.Status = Status;
            UT.Type = 3;
            string str = new CommonDA().InsertUpdateUnitTank(UT);
            return str;
        }
        [HttpGet]
        public ActionResult BottlingLine()
        {
            BottlingLine RM = new BottlingLine();
            ViewBag.BBT = CommonBL.fillBBT("S");
            if (Request.QueryString["A"] != null && Request.QueryString["A"].Trim() != string.Empty)
            {

                RM = new CommonBL().GetBottlingLine(short.Parse(CommonBL.fillBrewery()[0].Value), -1, int.Parse(new Crypto().Decrypt(Request.QueryString["A"].Trim())), "Z");
            }
            ViewBag.Msg = TempData["Msg"];
            ViewBag.Brewery = CommonBL.fillBrewery();
            ViewBag.UnitTank = new CommonBL().GetBottlingLineDetails(short.Parse(CommonBL.fillBrewery()[0].Value.Trim()), -1, -1, "Z");
            return View(RM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BottlingLine(BottlingLine RM)
        {
            string str = new CommonDA().InsertUpdateBottlingLine(RM);
            TempData["Msg"] = str;
            return RedirectToAction("BottlingLine");
        }
        public string UpdateBottlingLine(string UTId, string Status)
        {
            BottlingLine BL = new BottlingLine();
            BL.BottlingLineId = int.Parse(new Crypto().Decrypt(UTId));
            BL.BottlingLineStatus = Status;
            BL.Type = 3;
            string str = new CommonDA().InsertUpdateBottlingLine(BL);
            return str;
        }
        [HttpGet]
        public ActionResult ReceiveUnitTank()
        {
            UTTransferToBBT UTBL = new UTTransferToBBT();
            ViewBag.UnitTank = CommonBL.fillUnitTank("S");
            ViewBag.Brand = CommonBL.fillBrand("A");
            if (TempData["Message"] != null)
            {
                var str = TempData["Message"].ToString();
                ViewBag.Msg = TempData["Message"].ToString();
                if (!string.IsNullOrEmpty(str))
                {
                    UTBL.Msg = Message.MsgSuccess(str);
                }
            }
            return View(UTBL);
        }
        public string GetUnitTankForDDl(string UnitTankId)
        {
            UnitTank Ut = new CommonBL().GetUnitTank(short.Parse(CommonBL.fillBrewery()[0].Value), short.Parse(UnitTankId.Trim()), "A");
            return Ut.UnitTankCapacity.ToString() + "," + Ut.UnitTankStrength.ToString() + "," + Ut.UnitTankBulkLitre.ToString() + "," + Ut.BrandId.ToString() + "," + Ut.Brand.Trim();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReceiveUnitTank(UTTransferToBBT UTBL)
        {
            UTBL.TransferDate = CommonBL.Setdate(UTBL.TransferDate1);
            string str = new CommonDA().InsertUTTransferToBBT(UTBL);
            TempData["Message"] = str;
            return RedirectToAction("ReceiveUnitTank");
        }
        [HttpGet]
        public ActionResult UnitTankRecevDetails()
        {
            ViewBag.UnitTank = CommonBL.fillUnitTank("A");
            List<UTTransferToBBT> lstUtBl = new CommonBL().GetUTTransferToBBTList(CommonBL.Setdate("01/01/1900"), DateTime.Now, -1, "R", short.Parse(CommonBL.fillBrewery()[0].Value), -1);
            return View(lstUtBl);
        }
        [HttpGet]
        public ActionResult UTTransferToBBT()
        {
            UTTransferToBBT UTTBBT = new Models.UTTransferToBBT();
            ViewBag.UnitTank = CommonBL.fillUnitTank("S");
            ViewBag.BBT = CommonBL.fillBBT("S");
            if (TempData["Message"] != null)
            {
                var str = TempData["Message"].ToString();
                ViewBag.Msg = TempData["Message"].ToString();
                if (!string.IsNullOrEmpty(str))
                {
                    UTTBBT.Msg = Message.MsgSuccess(str);
                }
            }
            return View(UTTBBT);
        }
        /*public string GetBBTDetailsForDDl(string BBTID)
        {
            BBTFormation bbtFormation = new CommonBL().GetBBTMasterList(int.Parse(BBTID), -1, "A")[0];
            return bbtFormation.BBTBulkLitre.ToString();
        }
       */
        public string GetBBTForDDl(string BBTId)
        {

            BBTMaster bbtFormation = new CommonBL().GetBBTMasterList(int.Parse(BBTId), "A")[0];
            return bbtFormation.BBTBulkLitre.ToString() + "," + bbtFormation.BBTCapacity.ToString() + "," + bbtFormation.BrandName + "," + bbtFormation.BrandId;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UTTransferToBBT(UTTransferToBBT UTTBBT)
        {
            UTTBBT.TransferDate = CommonBL.Setdate(UTTBBT.TransferDate1);
            UTTBBT.TransactionType = "T";
            string str = new CommonDA().InsertUTTransferToBBT(UTTBBT);
            TempData["Message"] = str;
            return RedirectToAction("UTTransferToBBT");
        }
        [HttpGet]       
        public ActionResult UTTransferToBBTDetails()
        {
            ViewBag.UnitTank = CommonBL.fillUnitTank("A");
            ViewBag.BBT = CommonBL.fillBBT("A");
            List<UTTransferToBBT> lstUtBl = new CommonBL().GetUTTransferToBBTList(CommonBL.Setdate("01/01/1900"), DateTime.Now, -1, "T", short.Parse(CommonBL.fillBrewery()[0].Value), -1);
            return View(lstUtBl);
        }
        [HttpGet]
        public ActionResult BBTMaster()
        {
            BBTMaster BBT = new BBTMaster();
            ViewBag.Brewery = CommonBL.fillBrewery();
            if (Request.QueryString["Code"] != null && Request.QueryString["Code"].Trim() != string.Empty)
            {
                BBT = new CommonBL().GetBBTMasterList(int.Parse(new Crypto().Decrypt(Request.QueryString["Code"].Trim())), "Z")[0];
            }
            if (TempData["Message"] != null)
            {
                var str = TempData["Message"].ToString();
                if (!string.IsNullOrEmpty(str))
                {
                    ViewBag.Msg = TempData["Message"].ToString();
                    BBT.Message = Message.MsgSuccess(str);
                }
            }
            return View(BBT);
        }
        [HttpGet]
        public ActionResult GetBBTDetails()
        {

            List<BBTMaster> bbtFormations = new List<BBTMaster>();
            if (TempData["Msg"] != null)
            {
                var str = TempData["Msg"].ToString();
                if (!string.IsNullOrEmpty(str))
                {
                    ViewBag.Msg = TempData["Msg"].ToString();
                }
            }
            bbtFormations = new CommonBL().GetBBTMasterList(-1, "Z");
            return View(bbtFormations);
        }

        public string DeleteBBT(string bbtId, string status)
        {
            BBTMaster bbt = new BBTMaster();
            bbt.BBTId = int.Parse(bbtId);
            bbt.Status = status;
            bbt.SP_Type = 4;
            string str = new CommonDA().InsertUpdateBBT(bbt);

            return str;
            /*var bbtFormation = new Models.BBTFormation();
            bbtFormation.BBTId = bbtId;
            bbtFormation.SP_Type = 3;
            
            bbtFormation = new BBTFormation();
            if (!string.IsNullOrEmpty(str))
            {
                bbtFormation.Message = new Message();
                bbtFormation.Message.MStatus = "info";
                bbtFormation.Message.TextMessage = str;
            }*/
            //return PartialView("~/Views/Shared/_ErrorMessage.cshtml", "");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BBTMaster(BBTMaster BBT)
        {
            var str = new CommonDA().InsertUpdateBBT(BBT);
            TempData["Message"] = str;
            return RedirectToAction("BBTMaster");
        }
        [HttpGet]
        public ActionResult GatePass()
        {
            GatePassDetails GP = new GatePassDetails();
            DataSet ds = new CommonDA().GetUnitDetails(-1, "", "", -1, -1, -1, UserSession.LoggedInUserId);
            GP = new CommonBL().GetGatePassDetailsG(-1, CommonBL.Setdate("01/01/1900"), CommonBL.Setdate("31/12/3999"), 2, "P", "P", ds.Tables[0].Rows[0]["UnitLicenseno"].ToString().Trim(), "", ds.Tables[0].Rows[0]["UnitLicenseType"].ToString().Trim(), "");
            ViewBag.Msg = TempData["Message"];


            List<SelectListItem> ToLicenseTypes = new List<SelectListItem>();
            List<SelectListItem> FL1Licence = new List<SelectListItem>();
            SelectListItem SLI = new SelectListItem();

            SLI = new SelectListItem();
            SLI.Text = "FL-1";
            SLI.Value = "FL-1";
            ToLicenseTypes.Add(SLI);
            SLI = new SelectListItem();
            SLI.Text = "FL-1A";
            SLI.Value = "FL-1A";
            ToLicenseTypes.Add(SLI);
            SLI = new SelectListItem();
            SLI.Text = "Export Outside UP";
            SLI.Value = "Export Outside UP";
            ToLicenseTypes.Add(SLI);
            SLI = new SelectListItem();
            SLI.Text = "Export Outside INDIA";
            SLI.Value = "Export Outside INDIA";
            ToLicenseTypes.Add(SLI);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                FL1Licence = CommonBL.fillFL1Licence(int.Parse(ds.Tables[0].Rows[0]["UnitId"].ToString().Trim()));
                if (GP.FromLicenseType.Trim() == string.Empty)
                {
                    if (ds.Tables[0].Rows[0]["UnitLicenseType"].ToString().Trim() == "FL-3")
                    {
                        GP.FromLicenseType = "FL-3";
                        GP.ToLicenseType = "FL-1";
                    }
                    else if (ds.Tables[0].Rows[0]["UnitLicenseType"].ToString().Trim() == "FL-3A")
                    {
                        GP.FromLicenseType = "FL-3A";
                        GP.ToLicenseType = "FL-1A";
                    }
                    GP.FromLicenceNo = ds.Tables[0].Rows[0]["UnitLicenseno"].ToString().Trim();
                    GP.FromConsignorName = ds.Tables[0].Rows[0]["UnitName"].ToString().Trim();
                    GP.ConsignorAddress = ds.Tables[0].Rows[0]["UnitAddress"].ToString().Trim();
                }

            }
            if (!string.IsNullOrEmpty(GP.ToLicenseType.Trim()) &&
                ToLicenseTypes.Find(x => x.Text.Trim() == GP.ToLicenseType.Trim().Trim()) != null)
            {
                ToLicenseTypes.Find(x => x.Value.Trim() == GP.ToLicenseType.Trim()).Selected = true;
            }
            if (!string.IsNullOrEmpty(GP.ToLicenceNo.Trim()) &&
                FL1Licence.Find(x => x.Text.Trim() == GP.ToLicenceNo.Trim()) != null)
            {
                FL1Licence.Find(x => x.Text.Trim() == GP.ToLicenceNo.Trim()).Selected = true;
            }
            ViewBag.FL1Licence = FL1Licence;
            ViewBag.Districts = CommonBL.fillDistict("N");
            ViewBag.ToLicenseTypes = ToLicenseTypes;
            return View(GP);
        }
        public string GetUnitDetailsForddl(string UnitId)
        {
            string str = "";
            DataSet ds = new CommonDA().GetUnitDetails(short.Parse(UnitId), "", "", -1, -1, -1, -1);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                str = ds.Tables[0].Rows[0]["UnitLicenseno"].ToString().Trim() + "," + ds.Tables[0].Rows[0]["UnitName"].ToString().Trim() + "," + ds.Tables[0].Rows[0]["UnitAddress"].ToString().Trim();
            }
            return str;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GatePass(GatePassDetails GP)
        {
            if (GP.Receiver == null)
            {
                GP.Receiver = "";
            }
            if (GP.ImportPermitNo == null)
            {
                GP.ImportPermitNo = "";
            }
            GP.GatepassLicenseNo = "B-12";
            GP.GatePassSourceId = long.Parse(UserSession.LoggedInUserLevelId);
            GP.UploadValue = 2;
            GP.FromDate = CommonBL.Setdate(GP.FromDate1.Trim());
            GP.ToDate = CommonBL.Setdate(GP.ToDate1.Trim());
            string str = new CommonDA().InsertUpdateGatePassDetails(GP);
            TempData["Message"] = str;
            return RedirectToAction("GatePass");
        }
        [HttpGet]
        public ActionResult UploadGatePassCSV(string A, string B)
        {
            GatePassDetails GP = new GatePassDetails();
            ViewBag.Brand = CommonBL.fillBrand("A");
            GP = new CommonBL().GetGatePassDetailsG(-1, CommonBL.Setdate("01/01/1900"), CommonBL.Setdate("31/12/3999"), 2, "P", "P", "", "", "", "");
            return View(GP);
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult UploadGatePassCSV()
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    string str = "";
                    if (Request.Files[0] != null)
                    {

                        HttpFileCollectionBase files = Request.Files;
                        HttpPostedFileBase file = files[0];
                        str = CSV.ValidateCSV(2, -1, int.Parse(files.Keys[0].Replace("file", "")), file);
                    }
                    return Json(str);
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
        public string GetGatePassUploadBrandDetails(string Gatepass)
        {
            long GatePass = long.Parse(Gatepass);
            return new CommonBL().GetGatePassUploadBrandDetailsTable(GatePass);
        }
        public string FinalGatePass(string Gatepass)
        {
            long GatePass = long.Parse(Gatepass);
            return new CommonDA().FinalGatePass(GatePass, 1, 0);
        }
        public string UploadVerifedCSV(string GatePassId, string BrandId, string BatchNo, string UploadValue, string PlanId, string BLID)
        {
            long GatePass = long.Parse(GatePassId);
            int Brand = int.Parse(BrandId);
            int Plan = int.Parse(PlanId);
            short BottlingLineId = short.Parse(BLID);
            return new CommonDA().UploadCSV(GatePass, (Session["CaseCode"] as DataTable), int.Parse(UploadValue), Brand, BatchNo, Plan, BottlingLineId);
        }
        public ActionResult GetPassDetails()
        {
            List<GatePassDetails> lstGPD = new List<GatePassDetails>();
            // lstGPD = new CommonBL().GetGatePassDetailsList(-1, CommonBL.Setdate("01/01/1900"), CommonBL.Setdate("31/12/4000"), 2, "Z","P","","","","");
            DataSet ds = new CommonDA().GetUnitDetails(-1, "", "", -1, -1, -1, UserSession.LoggedInUserId);
            lstGPD = new CommonBL().GetGatePassDetailsList(-1, CommonBL.Setdate("01/01/1900"), CommonBL.Setdate("31/12/3999"), 2, "Z", "P", ds.Tables[0].Rows[0]["UnitLicenseno"].ToString().Trim(), "", ds.Tables[0].Rows[0]["UnitLicenseType"].ToString().Trim(), "");
            return View(lstGPD);
        }
        public ActionResult ReceiveGatePassWH()
        {
            DataSet ds = new CommonDA().GetUnitDetails(-1, "", "", -1, -1, -1, UserSession.LoggedInUserId);
            List<GatePassDetails> lstGPD = new List<GatePassDetails>();
            lstGPD = new CommonBL().GetGatePassDetailsList(-1, CommonBL.Setdate("01/01/1900"), CommonBL.Setdate("31/12/4000"), 2, "A", "P", "", ds.Tables[0].Rows[0]["UnitLicenseno"].ToString().Trim(), "", ds.Tables[0].Rows[0]["UnitLicenseType"].ToString().Trim());
            return View(lstGPD);
        }
        public string ReceiveGatePass(string GatePassId, string DamageBottles)
        {
            string str = new CommonDA().FinalGatePass(long.Parse(GatePassId.Trim()), 2, int.Parse(DamageBottles.Trim()));
            return str;
        }
        /*Copy From Vijay For Show Gate Pass*/
        public ActionResult GatePassPreview()
        {
            GatePassDetails GP = new GatePassDetails();
            long GatePassId = long.Parse(new Crypto().Decrypt(Request.QueryString["GatePass"].Trim()));
            GP = new CommonBL().GetGatePassDetailsG(GatePassId, CommonBL.Setdate("01/01/1900"), CommonBL.Setdate("31/12/4000"), 2, "Z", "Z", "", "", "", "");
            string qrcode = GP.GatePassNo;
            ViewBag.QRCodeImage = GenerateQRCode(qrcode);

            ViewBag.GetGatePassBrandDetailsList = new CommonBL().GetGatePassBrandDetailsList(GatePassId);
            return View(GP);
        }
        private string GenerateQRCode(string qrcodeText)
        {
            string folderPath = "~/Img/";
            string imagePath = "~/Img/QrCode.jpg";
            // If the directory doesn't exist then create it.
            if (!Directory.Exists(Server.MapPath(folderPath)))
            {
                Directory.CreateDirectory(folderPath);
            }

            var barcodeWriter = new BarcodeWriter();
            barcodeWriter.Format = BarcodeFormat.QR_CODE;
            var result = barcodeWriter.Write(qrcodeText);

            string barcodePath = Server.MapPath(imagePath);
            var barcodeBitmap = new Bitmap(result);
            using (MemoryStream memory = new MemoryStream())
            {
                using (FileStream fs = new FileStream(barcodePath, FileMode.Create, FileAccess.ReadWrite))
                {
                    barcodeBitmap.Save(memory, ImageFormat.Jpeg);
                    byte[] bytes = memory.ToArray();
                    fs.Write(bytes, 0, bytes.Length);
                }
            }
            return imagePath;
        }
        [HttpGet]
        public ActionResult FindPreviousQRCode()
        {
            ViewBag.Brand = CommonBL.fillBrand("A");
            return View(new CommonBL().GetBottelingPlanList(CommonBL.Setdate(DateTime.Today.AddDays(-5).ToString("dd/MM/yyyy")), DateTime.Now, short.Parse(CommonBL.fillBrewery()[0].Value), -1, "Z", "", -1, "Z"));
        }

        /***********************************/

    }
}
