using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UPExciseLTE.BLL;
using UPExciseLTE.DAL;
using UPExciseLTE.Models;
using ZXing;
using System.Data.Entity.Infrastructure;
using UPExciseLTE.Filters;
namespace UPExciseLTE.Controllers
{
    [SessionExpireFilterAttribute]
    [NoCache]
    [ChkAuthorization]
    [HandleError(View = "Error")]
    //[HandleError(ExceptionType = typeof(DbUpdateException), View = "Error")]
    public class UnitWareHouseController : Controller
    {
        [HttpGet]
        public ActionResult GatePass()
        {
            GatePassDetails GP = new GatePassDetails();
            DataSet ds = new CommonDA().GetUnitDetails(-1, "", "", -1, -1, -1, UserSession.LoggedInUserId);
            GP = new CommonBL().GetGatePassDetailsG(-1, CommonBL.Setdate("01/01/1900"), CommonBL.Setdate("31/12/3999"), 4, "P", "P", ds.Tables[0].Rows[0]["UnitLicenseno"].ToString().Trim(), "", ds.Tables[0].Rows[0]["UnitLicenseType"].ToString().Trim(), "");
            ViewBag.Msg = TempData["Message"];


            List<SelectListItem> ToLicenseTypes = new List<SelectListItem>();
            List<SelectListItem> FL1Licence = new List<SelectListItem>();
            SelectListItem SLI = new SelectListItem();

            SLI = new SelectListItem();
            SLI.Text = "FL-2B";
            SLI.Value = "FL-2B";
            ToLicenseTypes.Add(SLI);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                FL1Licence = CommonBL.fillFL1Licence(int.Parse(ds.Tables[0].Rows[0]["UnitId"].ToString().Trim()));
                if (GP.FromLicenseType.Trim() == string.Empty)
                {
                    if (ds.Tables[0].Rows[0]["UnitLicenseType"].ToString().Trim() == "FL-1")
                    {
                        GP.FromLicenseType = "FL-1";
                    }
                    else if (ds.Tables[0].Rows[0]["UnitLicenseType"].ToString().Trim() == "FL-1A")
                    {
                        GP.FromLicenseType = "FL-1A";
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
        
        [HttpPost]
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
            GP.GatepassLicenseNo = "FL-36";
            GP.GatePassSourceId = long.Parse(UserSession.LoggedInUserLevelId);
            GP.UploadValue = 4;
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
            ViewBag.Brand = CommonBL.fillBrandForCSV("S");
            GP = new CommonBL().GetGatePassDetailsG(-1, CommonBL.Setdate("01/01/1900"), CommonBL.Setdate("31/12/3999"), 4, "P", "P", "", "", "", "");
            return View(GP);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
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
                        str = CSV.ValidateCSV(4, -1, int.Parse(files.Keys[0].Replace("file", "")), file);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public string FinalGatePass(string Gatepass)
        {
            long GatePass = long.Parse(Gatepass);
            return new CommonDA().FinalGatePass(GatePass, 1, 0);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
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
            lstGPD = new CommonBL().GetGatePassDetailsList(-1, CommonBL.Setdate("01/01/1900"), CommonBL.Setdate("31/12/3999"), 4, "Z", "P", ds.Tables[0].Rows[0]["UnitLicenseno"].ToString().Trim(), "", ds.Tables[0].Rows[0]["UnitLicenseType"].ToString().Trim(), "");
            return View(lstGPD);
        }
        /*Copy From Vijay For Show Gate Pass*/
        public ActionResult GatePassPreview()
        {
            GatePassDetails GP = new GatePassDetails();
            long GatePassId = long.Parse(new Crypto().Decrypt(Request.QueryString["GatePass"].Trim()));
            GP = new CommonBL().GetGatePassDetailsG(GatePassId, CommonBL.Setdate("01/01/1900"), CommonBL.Setdate("31/12/4000"), 4, "Z", "Z", "", "", "", "");
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
        public ActionResult StockBalance()
        {
            try
            {
                DataSet dsStockBalance = new DataSet();
                dsStockBalance = new CommonDA().GetStockBalanceDetail(1);
                ViewData["StockBalance"] = dsStockBalance.Tables[0];
                return View(ViewData["StockBalance"]);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }      
        
    }
}