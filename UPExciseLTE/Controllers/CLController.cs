using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UPExciseLTE.BLL;
using UPExciseLTE.DAL;
using UPExciseLTE.Filters;
using UPExciseLTE.Models;
using ZXing;

namespace UPExciseLTE.Controllers
{
    [SessionExpireFilter]
    //[CheckAuthorization]
    [HandleError(ExceptionType = typeof(DbUpdateException), View = "Error")]
    public class CLController : Controller
    {
        [HttpGet]
        public ActionResult StorageVATCL()
        {
            StorageVATCL UT = new StorageVATCL();
            if (Request.QueryString["A"] != null && Request.QueryString["A"].Trim() != string.Empty)
            {
                UT = new CommonBL().GetStorageVAT(-1, short.Parse(new Crypto().Decrypt(Request.QueryString["A"].Trim())), "Z");
            }
            ViewBag.Msg = TempData["Msg"];
            ViewBag.Brewery = CommonBL.fillBrewery();
            ViewBag.StorageVATList = new CommonBL().GetStorageVATList(short.Parse(CommonBL.fillBrewery()[0].Value.Trim()), -1, "Z");
            return View(UT);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult StorageVATCL(StorageVATCL UT)
        {
            string str = new CommonDA().InsertUpdateStorageVAT(UT);
            TempData["Msg"] = str;
            return RedirectToAction("StorageVATCL");
        }
        public string UpdateStorageVAT(string UTId, string Status)
        {
            StorageVATCL UT = new StorageVATCL();
            UT.StorageVATId = int.Parse(new Crypto().Decrypt(UTId));
            UT.Status = Status;
            UT.Type = 3;
            string str = new CommonDA().InsertUpdateStorageVAT(UT);
            return str;
        }
        [HttpGet]
        public ActionResult BlendingVATCL()
        {
            BlendingVATCL UT = new BlendingVATCL();
            if (Request.QueryString["A"] != null && Request.QueryString["A"].Trim() != string.Empty)
            {
                UT = new CommonBL().GetBlendingVAT(-1, short.Parse(new Crypto().Decrypt(Request.QueryString["A"].Trim())), "Z");
            }
            ViewBag.Msg = TempData["Msg"];
            ViewBag.Brewery = CommonBL.fillBrewery();
            ViewBag.BlendingVATCL = new CommonBL().GetBlendingVATList(short.Parse(CommonBL.fillBrewery()[0].Value.Trim()), -1, "Z");
            return View(UT);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BlendingVATCL(BlendingVATCL UT)
        {
            string str = new CommonDA().InsertUpdateBlendingVAT(UT);
            TempData["Msg"] = str;
            return RedirectToAction("BlendingVATCL");
        }
        public string UpdateBlendingVAT(string UTId, string Status)
        {
            BlendingVATCL UT = new BlendingVATCL();
            UT.BlendingVATId = int.Parse(new Crypto().Decrypt(UTId));
            UT.Status = Status;
            UT.Type = 3;
            string str = new CommonDA().InsertUpdateBlendingVAT(UT);
            return str;
        }
        [HttpGet]
        public ActionResult BottelingVATCL()
        {
            BottelingVATCL UT = new BottelingVATCL();
            if (Request.QueryString["A"] != null && Request.QueryString["A"].Trim() != string.Empty)
            {
                UT = new CommonBL().GetBottelingVAT(-1, short.Parse(new Crypto().Decrypt(Request.QueryString["A"].Trim())), "Z");
            }
            ViewBag.Msg = TempData["Msg"];
            ViewBag.Brewery = CommonBL.fillBrewery();
            ViewBag.BottelingVATCL = new CommonBL().GetBottelingVATList(short.Parse(CommonBL.fillBrewery()[0].Value.Trim()), -1, "Z");
            return View(UT);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BottelingVATCL(BottelingVATCL UT)
        {
            string str = new CommonDA().InsertUpdateBottelingVAT(UT);
            TempData["Msg"] = str;
            return RedirectToAction("BottelingVATCL");
        }
        public string UpdateBottelingVAT(string UTId, string Status)
        {
            BottelingVATCL UT = new BottelingVATCL();
            UT.BottelingVATId = int.Parse(new Crypto().Decrypt(UTId));
            UT.Status = Status;
            UT.Type = 3;
            string str = new CommonDA().InsertUpdateBottelingVAT(UT);
            return str;
        }
        public ActionResult BottelingPlanCL()
        {
            BottelingPlanCL BP = new BottelingPlanCL();
            ViewBag.Msg = TempData["Message"];
            ViewBag.Brand = CommonBL.fillBrand("S");
            List<SelectListItem> lstBBT = CommonBL.fillBottelingVATCL("S");
            ViewBag.BBT = lstBBT;
            //BBTMaster bbtFormation = new CommonBL().GetBBTMasterList(int.Parse(lstBBT[0].Value), "A")[0];

            //ViewBag.BottlingLine = CommonBL.BottlingLine("Z", lstBBT[0].Value);
            //BP.BBTBulkLitre = float.Parse(bbtFormation.BBTBulkLitre.ToString());

            ViewBag.Msg = TempData["Message"];
            if (Request.QueryString["A"] != null && Request.QueryString["A"].ToString().Trim() != string.Empty)
            {
                BP = (new CommonBL().GetBottelingPlanCL(CommonBL.Setdate("01/01/1900"), DateTime.Now, short.Parse(CommonBL.fillBrewery()[0].Value), -1, "", "", int.Parse(new Crypto().Decrypt(Request.QueryString["A"].Trim())), "PB"));
            }
            return View(BP);
        }
        [HttpPost]
        public ActionResult BottelingPlanCL(BottelingPlanCL BP)
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

            string str = new CommonDA().InsertUpdatePlanCL(BP);
            TempData["Message"] = str;
            if (BP.Type == 1)
            {
                return RedirectToAction("BottelingPlanCL");
            }
            else
            {
                return RedirectToAction("BottelingPlanCL", new { A = BP.EncPlanId });
            }
        }
        public string GetBottlingTankForddl(string BTID)
        {
            BottelingVATCL BVT = new CommonBL().GetBottelingVAT(short.Parse(CommonBL.fillBrewery()[0].Value.Trim()), short.Parse(BTID), "A");
            return BVT.BottelingVATBulkLitre.ToString()+","+ BVT.BottelingVATCapacity.ToString() + ","+ BVT.SpiritType.ToString() + "," + BVT.BrandName.ToString();

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
        
        [HttpGet]
        public ActionResult EditFinalBottelingPlanCL()
        {
            return View(new CommonBL().GetBottelingPlanListCL(CommonBL.Setdate("01/01/1900"), DateTime.Now, short.Parse(CommonBL.fillBrewery()[0].Value), -1, "Z", "", -1, "PB"));
        }
        public string FinalizePlan(string PlanId)
        {
            string str = "";
            try
            {
                BottelingPlanCL Plan = new BottelingPlanCL();
                PlanId = new Crypto().Decrypt(PlanId);
                Plan.Type = 3;
                Plan.PlanId = int.Parse(PlanId);
                Plan.IsPlanFinal = 1;
                str = new CommonDA().InsertUpdatePlanCL(Plan);
            }
            catch (Exception x)
            {
                str = x.Message.ToString();
            }
            return str;
        }
        [HttpGet]
        public ActionResult SearchplanCL()
        {
            List<SelectListItem> BrandList = new List<SelectListItem>();
            ViewBag.Brand = CommonBL.fillBrand("A");
            return View(new CommonBL().GetBottelingPlanListCL(DateTime.Now, DateTime.Now, short.Parse(CommonBL.fillBrewery()[0].Value), -1, "Z", "", -1, "Z"));
        }
        [HttpGet]
        public ActionResult GenerateQRCodeCL()
        {
            ViewBag.Brand = CommonBL.fillBrand("A");
            return View(new CommonBL().GetBottelingPlanListCL(CommonBL.Setdate("01/01/1900"), DateTime.Now, short.Parse(CommonBL.fillBrewery()[0].Value), -1, "Z", "", -1, "Z"));
        }
        public string GenreateQR(string PlanId)
        {
            string str = "";
            try
            {
                PlanId = new Crypto().Decrypt(PlanId);
                string UserId = (Session["tbl_Session"] as DataTable).Rows[0]["UserId"].ToString().Trim();
                str = new CommonDA().GenerateQRCodeCL(int.Parse(PlanId), UserId, "");
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
            ds = new CommonDA().GetQRCOdeCL(int.Parse(PlanId));

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
        public ActionResult ProductionPlanListCL()
        {
            List<BottelingPlanCL> BPList = new List<BottelingPlanCL>();
            ViewBag.Brand = CommonBL.fillBrand("A");
            return View(new CommonBL().GetBottelingPlanListCL(CommonBL.Setdate("01/01/1900"), DateTime.Now, short.Parse(CommonBL.fillBrewery()[0].Value), -1, "Z", "", -1, "FB"));
        }
        public ActionResult ProductionEntryCL(string A)
        {
            ViewBag.Msg = "";
            A = new Crypto().Decrypt(A);
            int Planid = int.Parse(A);
            return View(new CommonBL().GetBottelingPlanCL(CommonBL.Setdate("01/01/1900"), DateTime.Now, short.Parse(CommonBL.fillBrewery()[0].Value), -1, "Z", "", Planid, "FB"));
        }
        [HttpPost]
        public ActionResult ProductionEntryCL(BottelingPlanCL BP)
        {
            string UserId = (Session["tbl_Session"] as DataTable).Rows[0]["UserId"].ToString().Trim();
            BP.Type = 1;
            string str = new CommonDA().InsertUpdateProductionPlanCL(BP);
            return RedirectToAction("ProductionEntryCL", new { A = BP.EncPlanId });
        }
        [HttpGet]
        public ActionResult FreezePlanCL()
        {
            return View(new CommonBL().GetBottelingPlanListCL(CommonBL.Setdate("01/01/1900"), DateTime.Now, short.Parse(CommonBL.fillBrewery()[0].Value), -1, "Z", "", -1, "FB"));
        }
        public string FreezePlanSuccess(string PlanId)
        {
            string str = "";
            try
            {
                PlanId = new Crypto().Decrypt(PlanId);
                BottelingPlanCL Plan = new BottelingPlanCL();
                Plan.Type = 2;
                Plan.PlanId = int.Parse(PlanId);
                Plan.IsProductionFinal = 1;
                str = new CommonDA().InsertUpdateProductionPlanCL(Plan);
            }
            catch (Exception x)
            {
                str = x.Message.ToString();
            }
            return str;
        }
        [HttpGet]
        public ActionResult UploadCSVCL()
        {
            List<BottelingPlanCL> lstBotteling = new CommonBL().GetBottelingPlanListCL(CommonBL.Setdate("01/01/1900"), DateTime.Now, -1, -1, "Z", "", -1, "FP");
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
        public ActionResult SearchProductionCL()
        {

            ViewBag.Brand = CommonBL.fillBrand("A");
            return View(new CommonBL().GetBottelingPlanListCL(CommonBL.Setdate("01/01/1900"), DateTime.Now, short.Parse(CommonBL.fillBrewery()[0].Value), -1, "Z", "", -1, "Z"));
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
                        str = CSV.ValidateCSVCL(1, int.Parse(new Crypto().Decrypt(files.Keys[0])), -1, file);
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
        //[HttpGet]
        //public ActionResult UnitTank()
        //{
        //    UnitTank UT = new UnitTank();
        //    if (Request.QueryString["A"] != null && Request.QueryString["A"].Trim() != string.Empty)
        //    {

        //        UT = new CommonBL().GetUnitTank(-1, short.Parse(new Crypto().Decrypt(Request.QueryString["A"].Trim())), "Z");
        //    }
        //    ViewBag.Msg = TempData["Msg"];
        //    ViewBag.Brewery = CommonBL.fillBrewery();
        //    ViewBag.UnitTank = new CommonBL().GetUnitTankDetails(short.Parse(CommonBL.fillBrewery()[0].Value.Trim()), -1, "Z");
        //    return View(UT);
        //}
        //[HttpPost]
        
        //public string UpdateUnitTank(string UTId, string Status)
        //{
        //    UnitTank UT = new UnitTank();
        //    UT.UnitTankId = int.Parse(new Crypto().Decrypt(UTId));
        //    UT.Status = Status;
        //    UT.Type = 3;
        //    string str = new CommonDA().InsertUpdateUnitTank(UT);
        //    return str;
        //}
        [HttpGet]
        public ActionResult BottlingLineCL()
        {
            BottlingLineCL RM = new BottlingLineCL();
            ViewBag.BBT = CommonBL.fillBottelingVATCL("S");
            if (Request.QueryString["A"] != null && Request.QueryString["A"].Trim() != string.Empty)
            {

                RM = new CommonBL().GetBottlingLineCL(short.Parse(CommonBL.fillBrewery()[0].Value), -1, int.Parse(new Crypto().Decrypt(Request.QueryString["A"].Trim())), "Z");
            }
            ViewBag.Msg = TempData["Msg"];
            ViewBag.Brewery = CommonBL.fillBrewery();
            ViewBag.UnitTank = new CommonBL().GetBottlingLineDetailsCL(short.Parse(CommonBL.fillBrewery()[0].Value.Trim()), -1, -1, "Z");
            return View(RM);
        }
        [HttpPost]
        public ActionResult BottlingLineCL(BottlingLineCL RM)
        {
            string str = new CommonDA().InsertUpdateBottlingLineCL(RM);
            TempData["Msg"] = str;
            return RedirectToAction("BottlingLineCL");
        }
        public string UpdateBottlingLineCL(string UTId, string Status)
        {
            BottlingLineCL BL = new BottlingLineCL();
            BL.BottlingLineId = int.Parse(new Crypto().Decrypt(UTId));
            BL.BottlingLineStatus = Status;
            BL.Type = 3;
            string str = new CommonDA().InsertUpdateBottlingLineCL(BL);
            return str;
        }
         
        public string GetStorageVATDetails(string SVID)
        {
            StorageVATCL Ut = new CommonBL().GetStorageVAT(short.Parse(CommonBL.fillBrewery()[0].Value), short.Parse(SVID.Trim()), "A");
            return Ut.StorageVATCapacity.ToString() + "," + Ut.SpiritType.ToString() + "," + Ut.StorageVATBulkLitre.ToString()+","+Ut.SpiritType+","+Ut.StorageVATAlcoholicLiter;
        }
        [HttpGet]
        public ActionResult ReceiveStorageVATCL()
        {
            TankTransferDetail SVBL = new TankTransferDetail();
            ViewBag.IssuedFromSVId = CommonBL.fillStorageVAT("S");
            ViewBag.SpiritType = CommonBL.fillSpiritType("S");
            if (TempData["Message"] != null)
            {
                ViewBag.Msg = TempData["Message"].ToString();    
            }
            return View(SVBL);
           
        }
        [HttpPost]
        public ActionResult ReceiveStorageVATCL(TankTransferDetail UTBL)
        {
            UTBL.TransactionType = "RS";
            string str = new CommonDA().InsertTankTransferDetail(UTBL);
            TempData["Message"] = str;
            return RedirectToAction("ReceiveStorageVATCL");
        }
        [HttpGet]
        public ActionResult StorageVATRecevDetails()
        {
            /*ViewBag.UnitTank = CommonBL.fillUnitTank("A");
            List<SVTransferToBV> lstUtBl = new CommonBL().GetSVTransferToBTList(CommonBL.Setdate("01/01/1900"), DateTime.Now, -1, "R", short.Parse(CommonBL.fillBrewery()[0].Value), -1);
            return View(lstUtBl);*/
            return View();
        }
        [HttpGet]
        public ActionResult StorageVATTransferToBVCL()
        {
            TankTransferDetail TTD = new Models.TankTransferDetail();
            ViewBag.IssuedFromSVId = CommonBL.fillStorageVAT("S");
            ViewBag.BlendingVAT = CommonBL.fillBlendingVAT("S");
            ViewBag.Brand = CommonBL.fillBrand("S");
            if (TempData["Message"] != null)
            {
                 ViewBag.Msg = TempData["Message"].ToString();
            }
            return View(TTD); 
        }
        
        public string GetBlendingVATForDDl(string BVId)
        {
            BlendingVATCL BVT = new CommonBL().GetBlendingVAT(short.Parse(CommonBL.fillBrewery()[0].Value), short.Parse(BVId), "A"); 
            return BVT.BlendingVATBulkLitre.ToString() + "," + BVT.BlendingVATCapacity.ToString()+","+BVT.SpiritType+","+BVT.BrandName+","+BVT.BlendingVATAlcoholicLiter;
        }
        [HttpPost]
        public ActionResult StorageVATTransferToBVCL(TankTransferDetail UTTBBT)
        {
            UTTBBT.TransactionType = "SB";
            string str = new CommonDA().InsertTankTransferDetail(UTTBBT);
            TempData["Message"] = str;
            return RedirectToAction("StorageVATTransferToBVCL");
        }
        [HttpGet]
        public ActionResult StorageVATTransferToBVDetails()
        {
            ViewBag.UnitTank = CommonBL.fillUnitTank("A");
            ViewBag.BBT = CommonBL.fillBBT("A");
            List<UTTransferToBBT> lstUtBl = new CommonBL().GetUTTransferToBBTList(CommonBL.Setdate("01/01/1900"), DateTime.Now, -1, "T", short.Parse(CommonBL.fillBrewery()[0].Value), -1);
            return View(lstUtBl);
        }
        
        [HttpGet]
        public ActionResult GetBottelingVATDetailsCL()
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
        [HttpGet]
        public ActionResult GatePassCL()
        {
            GatePassDetails GP = new GatePassDetails();
            GP = new CommonBL().GetGatePassDetailsG(-1, CommonBL.Setdate("01/01/1900"), CommonBL.Setdate("31/12/3999"), 2, "P", "P");

            //ViewBag.FromLicenceNo = CommonBL.fillLiceseeLicenseNos("S");
            //ViewBag.ToLicenceNo = CommonBL.fillLiceseeLicenseNos("S");
            //List <SelectListItem> lstBR = CommonBL.fillBreweryLiceName();
            ViewBag.Msg = TempData["Message"];
            DataSet ds = new CommonDA().GetUnitDetails(-1, "", "", -1, -1);
            if (GP.FromConsignorName.Trim() == string.Empty)
            {
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    GP.FromLicenceNo = ds.Tables[0].Rows[0]["UnitLicenseno"].ToString().Trim();
                    GP.FromConsignorName = ds.Tables[0].Rows[0]["UnitName"].ToString().Trim();
                    GP.ToLicenceNo = ds.Tables[0].Rows[0]["UnitLicenseno"].ToString().Trim();
                    GP.ToConsigeeName = ds.Tables[0].Rows[0]["UnitName"].ToString().Trim();
                    GP.Address = ds.Tables[0].Rows[0]["UnitAddress"].ToString().Trim();
                }
            }
            ViewBag.Districts = CommonBL.fillDistict("N");
            ViewBag.FromLicenseTypes = CommonBL.fillLicenseTypes("S", "L1F");
            ViewBag.ToLicenseTypes = CommonBL.fillLicenseTypes("S", "L1T");
            return View(GP);
        }
        [HttpPost]
        public ActionResult GatePassCL(GatePassDetails GP)
        {
            GP.GatePassSourceId = long.Parse(UserSession.LoggedInUserLevelId);
            GP.UploadValue = 2;
            GP.FromDate = CommonBL.Setdate(GP.FromDate1.Trim());
            GP.ToDate = CommonBL.Setdate(GP.ToDate1.Trim());
            string str = new CommonDA().InsertUpdateGatePassDetails(GP);
            TempData["Message"] = str;
            return RedirectToAction("GatePass");
        }
        [HttpGet]
        public ActionResult UploadGatePassCSVCL(string A, string B)
        {
            GatePassDetails GP = new GatePassDetails();
            ViewBag.Brand = CommonBL.fillBrand("A");
            GP = new CommonBL().GetGatePassDetailsG(-1, CommonBL.Setdate("01/01/1900"), CommonBL.Setdate("31/12/3999"), 2, "P", "P");
            return View(GP);
        }
        [HttpPost]
        public ActionResult UploadGatePassCSVCL()
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
        public ActionResult GetPassDetailsCL()
        {
            List<GatePassDetails> lstGPD = new List<GatePassDetails>();
            lstGPD = new CommonBL().GetGatePassDetailsList(-1, CommonBL.Setdate("01/01/1900"), CommonBL.Setdate("31/12/4000"), 2, "Z", "P");
            return View(lstGPD);
        }
        public ActionResult ReceiveGatePassWH()
        {
            List<GatePassDetails> lstGPD = new List<GatePassDetails>();
            lstGPD = new CommonBL().GetGatePassDetailsList(-1, CommonBL.Setdate("01/01/1900"), CommonBL.Setdate("31/12/4000"), 2, "A", "P");
            return View(lstGPD);
        }
        public string ReceiveGatePass(string GatePassId, string DamageBottles)
        {
            string str = new CommonDA().FinalGatePass(long.Parse(GatePassId.Trim()), 2, int.Parse(DamageBottles.Trim()));
            return str;
        }
        /*Copy From Vijay For Show Gate Pass*/
        public ActionResult GatePassPreviewCL()
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


        #region New for CL
        [HttpGet]
        public ActionResult BlendingVATtfBottelingVATCL()
        {
            TankTransferDetail TTD = new TankTransferDetail();
            ViewBag.BlendingVAT = CommonBL.fillBlendingVAT("S");
            ViewBag.BottelingVAT = CommonBL.fillBottelingVATCL("S");
            if (TempData["Message"] != null)
            {
                ViewBag.Msg = TempData["Message"].ToString();
            }
            return View(TTD);
        }
        [HttpPost]
        public ActionResult BlendingVATtfBottelingVATCL(TankTransferDetail TTD)
        {

            TTD.TransactionType = "BB";
            string str = new CommonDA().InsertTankTransferDetail(TTD);
            TempData["Message"] = str;
            return RedirectToAction("BlendingVATtfBottelingVATCL");
        }
        #endregion

    }
}