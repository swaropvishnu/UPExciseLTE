﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using UPExciseLTE.DAL;
using UPExciseLTE.Models;
using ClosedXML.Excel;
using System.IO;
using UPExciseLTE.BLL;
using UPExciseLTE.Filters;
using System.Data.Entity.Infrastructure;
using System.Web;
using System.Text;
using ZXing;
using System.Drawing;
using System.Drawing.Imaging;

namespace UPExciseLTE.Controllers
{
    [SessionExpireFilter]
    //[CheckAuthorization]
    [HandleError(ExceptionType = typeof(DbUpdateException), View = "Error")]
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
            if (Request.QueryString["Code"] != null && Request.QueryString["Code"].Trim() != string.Empty)
            {
                Brand = new CommonBL().GetBrand(int.Parse(new Crypto().Decrypt(Request.QueryString["Code"].Trim())), "", "", "", -1, -1, -1, "Z");
            }
            List<SelectListItem> lstLicense= new List<SelectListItem>();
            SelectListItem SLI = new SelectListItem();
            if (UserSession.PushName.Trim()== "be_unnao1")
            {
                SLI.Text = "FL3A";
                SLI.Value = "FL3A";
            }
            else if (UserSession.PushName.Trim() == "BWFL")
            {
                SLI.Text = "BWFL-2B";
                SLI.Value = "BWFL-2B";
            }
            else 
            {
                SLI.Text = "FL3A";
                SLI.Value = "FL3A";
            }
            lstLicense.Add(SLI);
            ViewBag.LicenseType= lstLicense;
            DataSet ds = new CommonDA().GetDutyCalculation(DateTime.Now.Year.ToString(), "BE");
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                ViewBag.MConsidrationFees = ds.Tables[0].Rows[0]["ConsidrationFees"].ToString().Trim();
                ViewBag.SConsidrationFees = ds.Tables[0].Rows[1]["ConsidrationFees"].ToString().Trim();
                ViewBag.MWholeSaleMargin = ds.Tables[0].Rows[0]["WholeSaleMargin"].ToString().Trim();
                ViewBag.SWholeSaleMargin = ds.Tables[0].Rows[1]["WholeSaleMargin"].ToString().Trim();
                ViewBag.MRetailerMargin = ds.Tables[0].Rows[0]["RetailerMargin"].ToString().Trim();
                ViewBag.SRetailerMargin = ds.Tables[0].Rows[1]["RetailerMargin"].ToString().Trim();
            }
            string UserId = UserSession.LoggedInUserId.ToString().Trim();
            ViewBag.Brewery = CommonBL.fillBrewery();
            ViewBag.StateList = CommonBL.fillState("S");
            ViewBag.Msg = TempData["Message"];
            return View(Brand);
        }
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
        public ActionResult FinalizeBrand()
        {
            ViewBag.Brewery = CommonBL.fillBrewery();
            ViewBag.District = CommonBL.fillState("S");
            List<BrandMaster> lstBrand = new CommonBL().GetBrandList(-1, "", "", "", -1, -1, -1, "P");
            return View(lstBrand);
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
           
            if (UserSession.LoggedInUserId == 10)
            {
                ViewBag.BBT = CommonBL.fillBBT("Z");
                ViewBag.BottlingLine = CommonBL.BottlingLine("Z","3");
                BP.BBTBulkLiter = 9999999;
            }
            else
            {
                ViewBag.BBT = CommonBL.fillBBT("S");
                ViewBag.BottlingLine = CommonBL.EmptyDDl("S");
            }
            ViewBag.Msg = TempData["Message"];
            if (Request.QueryString["A"] != null && Request.QueryString["A"].ToString().Trim() != string.Empty)
            {
                BP=(new CommonBL().GetBottelingPlan(CommonBL.Setdate("01/01/1900"), DateTime.Now, short.Parse(CommonBL.fillBrewery()[0].Value), -1, "", "", int.Parse(new Crypto().Decrypt(Request.QueryString["A"].Trim())), "PB"));
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
            DataSet ds = new CommonDA().GetBrandDetail(int.Parse(BrandId), "", "", "",  short.Parse(CommonBL.fillBrewery()[0].Value), -1, -1, "A");
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
            return View(new CommonBL().GetBottelingPlanList(CommonBL.Setdate("01/01/1900"), DateTime.Now,short.Parse(CommonBL.fillBrewery()[0].Value), -1, "Z", "", -1, "PB"));
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
        [HttpPost]
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
            return View(new CommonBL().GetBottelingPlan(CommonBL.Setdate("01/01/1900"), DateTime.Now, short.Parse(CommonBL.fillBrewery()[0].Value), -1, "Z", "", Planid, "FB"));
        }
        [HttpPost]
        public ActionResult ProductionEntry(BottelingPlan BP)
        {
            string UserId = (Session["tbl_Session"] as DataTable).Rows[0]["UserId"].ToString().Trim();
            BP.Type = 1;
            string str = new CommonDA().InsertUpdateProductionPlan(BP);
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

            ViewBag.Brand = CommonBL.fillBrand("A");
            return View(new CommonBL().GetBottelingPlanList(CommonBL.Setdate("01/01/1900"), DateTime.Now, short.Parse(CommonBL.fillBrewery()[0].Value), -1, "Z", "", -1, "Z"));
        }
        public ActionResult Track_staus()
        {
            return View();
        }

        [HttpPost]
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

                RM = new CommonBL().GetBottlingLine( short.Parse(CommonBL.fillBrewery()[0].Value), -1, int.Parse(new Crypto().Decrypt(Request.QueryString["A"].Trim())), "Z");
            }
            ViewBag.Msg = TempData["Msg"];
            ViewBag.Brewery = CommonBL.fillBrewery();
            ViewBag.UnitTank = new CommonBL().GetBottlingLineDetails(short.Parse(CommonBL.fillBrewery()[0].Value.Trim()), -1, -1, "Z");
            return View(RM);
        }
        [HttpPost]
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
            return Ut.UnitTankCapacity.ToString() + "," + Ut.UnitTankStrength.ToString() + "," + Ut.UnitTankBulkLiter.ToString();
        }
        [HttpPost]
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
            List<UTTransferToBBT> lstUtBl = new CommonBL().GetUTTransferToBBTList(CommonBL.Setdate("01/01/1900"), DateTime.Now, -1, "R",  short.Parse(CommonBL.fillBrewery()[0].Value), -1);
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
            return bbtFormation.BBTBulkLiter.ToString();
        }
       */
        public string GetBBTForDDl(string BBTId)
        {

            BBTMaster bbtFormation = new CommonBL().GetBBTMasterList(int.Parse(BBTId), "A")[0];
            return bbtFormation.BBTBulkLiter.ToString() + "," + bbtFormation.BBTCapacity.ToString();
        }
        [HttpPost]
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
            bbtFormations = new CommonBL().GetBBTMasterList(-1, "Z");
            return View(bbtFormations);
        }
        [HttpGet]
        public ActionResult DeleteBBT(int bbtId)
        {
            /*var bbtFormation = new Models.BBTFormation();
            bbtFormation.BBTId = bbtId;
            bbtFormation.SP_Type = 3;
            var str = new CommonDA().InsertUpdateBBT(bbtFormation);
            bbtFormation = new BBTFormation();
            if (!string.IsNullOrEmpty(str))
            {
                bbtFormation.Message = new Message();
                bbtFormation.Message.MStatus = "info";
                bbtFormation.Message.TextMessage = str;
            }*/
            return PartialView("~/Views/Shared/_ErrorMessage.cshtml", "");
        }
        [HttpPost]
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
            GP = new CommonBL().GetGatePassDetailsG(-1, CommonBL.Setdate("01/01/1900"), CommonBL.Setdate("31/12/3999"), 2, "P","P");

            //ViewBag.FromLicenceNo = CommonBL.fillLiceseeLicenseNos("S");
            //ViewBag.ToLicenceNo = CommonBL.fillLiceseeLicenseNos("S");
            List <SelectListItem> lstBR = CommonBL.fillBreweryLiceName();
            if (GP.FromConsignorName.Trim()==string.Empty)
            {
                GP.FromLicenceNo = lstBR[0].Value;
                GP.FromConsignorName = lstBR[0].Text;
                GP.ToLicenceNo = lstBR[0].Value;
                GP.ToConsigeeName = lstBR[0].Text;
            }
           
            ViewBag.Districts = CommonBL.fillDistict("S");
            ViewBag.FromLicenseTypes = CommonBL.fillLicenseTypes("S", "L1F");
            ViewBag.ToLicenseTypes = CommonBL.fillLicenseTypes("S", "L1T");
            return View(GP);
        }
        [HttpPost]
        public ActionResult GatePass(GatePassDetails GP)
        {
            GP.GatePassSourceId = long.Parse(UserSession.LoggedInUserLevelId);
            GP.UploadValue =2;
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
            GP = new CommonBL().GetGatePassDetailsG(-1, CommonBL.Setdate("01/01/1900"), CommonBL.Setdate("31/12/3999"), 2, "P","P");
            return View(GP);
        }
        [HttpPost]
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
            return new CommonDA().FinalGatePass(GatePass,1,0);
        }
        public string UploadVerifedCSV(string GatePassId, string BrandId, string BatchNo, string UploadValue,string PlanId)
        {
            long GatePass = long.Parse(GatePassId);
            int Brand = int.Parse(BrandId);
            int Plan = int.Parse(PlanId);
            return new CommonDA().UploadCSV(GatePass, (Session["CaseCode"] as DataTable), int.Parse(UploadValue), Brand, BatchNo, Plan);
        }
        public ActionResult GetPassDetails()
        {
            List<GatePassDetails> lstGPD = new List<GatePassDetails>();
            lstGPD = new CommonBL().GetGatePassDetailsList(-1, CommonBL.Setdate("01/01/1900"), CommonBL.Setdate("31/12/4000"), 2, "Z","P");
            return View(lstGPD);
        }
        public ActionResult ReceiveGatePassWH()
        {
            List<GatePassDetails> lstGPD = new List<GatePassDetails>();
            lstGPD = new CommonBL().GetGatePassDetailsList(-1, CommonBL.Setdate("01/01/1900"), CommonBL.Setdate("31/12/4000"), 2, "A","P");
            return View(lstGPD);
        }
        public string ReceiveGatePass(string GatePassId,string DamageBottles)
        {
            string str = new CommonDA().FinalGatePass(long.Parse(GatePassId.Trim()),2,int.Parse(DamageBottles.Trim()));
            return str;
        }
        /*Copy From Vijay For Show Gate Pass*/
        public ActionResult GatePassPreview()
        {
            GatePassDetails GP = new GatePassDetails();
            long GatePassId = long.Parse(new Crypto().Decrypt(Request.QueryString["GatePass"].Trim()));
            GP = new CommonBL().GetGatePassDetailsG(GatePassId, CommonBL.Setdate("01/01/1900"), CommonBL.Setdate("31/12/4000"), 2, "Z", "Z");
            string qrcode = "EXCISE";
            ViewBag.QRCodeImage = GenerateQRCode(qrcode);
            ViewBag.PassType = "B-12";
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
        /***********************************/
    }
}
