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
    [HandleError(View = "Error")]
    public class FLController : Controller
    {
        [HttpGet]
        public ActionResult ReceiverMaster()
        {
            Reciver UT = new Reciver();
            ViewBag.Brewery = CommonBL.fillBrewery();
            UT.UnitId = short.Parse(CommonBL.fillBrewery()[0].Value.Trim());
            UT.ReciverID = -1;
            ViewBag.StorageVATList = new CommonBL().GetReciverListFL(short.Parse(CommonBL.fillBrewery()[0].Value.Trim()), -1, "Z");
            return View(UT);
        }
        public ActionResult GetReciver()
        {
            return Json(CommonBL.fillReceiver("S"), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReceiverMaster(StorageVATFL UT)
        {
            UT.BreweryId = short.Parse(CommonBL.fillBrewery()[0].Value);
            string str = new CommonDA().InsertUpdateStorageVATFL(UT);
            TempData["Msg"] = str;
            return RedirectToAction("StorageVATFL");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult InsertUpdateReciver(ReciverFL Objform)
        {
            try
            {
                string str = new DAL.CommonDA().InsertUpdateReciverFL(Objform);
                return Json(str, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult InsertUpdateTankTranfer(TankTransferDetailFL Objform)
        {
            try
            {

                string str = new DAL.CommonDA().InsertTankTransferDetailFL(Objform);
                return Json(str, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult StorageVATFL()
        {
            StorageVATFL UT = new StorageVATFL();
            if (Request.QueryString["A"] != null && Request.QueryString["A"].Trim() != string.Empty)
            {
                UT = new CommonBL().GetStorageVATFL(-1, short.Parse(new Crypto().Decrypt(Request.QueryString["A"].Trim())), "Z");
            }
            ViewBag.Msg = TempData["Msg"];
            ViewBag.Brewery = CommonBL.fillBrewery();
            ViewBag.StorageVATList = new CommonBL().GetStorageVATListFL(short.Parse(CommonBL.fillBrewery()[0].Value.Trim()), -1, "Z");
            return View(UT);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult StorageVATFL(StorageVATFL UT)
        {
            UT.BreweryId = short.Parse(CommonBL.fillBrewery()[0].Value);
            string str = new CommonDA().InsertUpdateStorageVATFL(UT);
            TempData["Msg"] = str;
            return RedirectToAction("StorageVATFL");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public string UpdateStorageVAT(string UTId, string Status)
        {
            StorageVATFL UT = new StorageVATFL();
            UT.StorageVATId = int.Parse(new Crypto().Decrypt(UTId));
            UT.Status = Status;
            UT.Type = 3;
            string str = new CommonDA().InsertUpdateStorageVATFL(UT);
            return str;
        }
        [HttpGet]
        public ActionResult BlendingVATFL()
        {
            BlendingVATFL UT = new BlendingVATFL();
            if (Request.QueryString["A"] != null && Request.QueryString["A"].Trim() != string.Empty)
            {
                UT = new CommonBL().GetBlendingVATFL(-1, short.Parse(new Crypto().Decrypt(Request.QueryString["A"].Trim())), "Z");
            }
            ViewBag.Msg = TempData["Msg"];
            ViewBag.Brewery = CommonBL.fillBrewery();
            ViewBag.BlendingVATFL = new CommonBL().GetBlendingVATListFL(short.Parse(CommonBL.fillBrewery()[0].Value.Trim()), -1, "Z");
            return View(UT);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BlendingVATFL(BlendingVATFL UT)
        {
            UT.BreweryId = short.Parse(CommonBL.fillBrewery()[0].Value);
            string str = new CommonDA().InsertUpdateBlendingVATFL(UT);
            TempData["Msg"] = str;
            return RedirectToAction("BlendingVATFL");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public string UpdateBlendingVAT(string UTId, string Status)
        {
            BlendingVATFL UT = new BlendingVATFL();
            UT.BlendingVATId = int.Parse(new Crypto().Decrypt(UTId));
            UT.Status = Status;
            UT.Type = 3;
            string str = new CommonDA().InsertUpdateBlendingVATFL(UT);
            return str;
        }
        [HttpGet]
        public ActionResult BottelingVATFL()
        {
            BottelingVATFL UT = new BottelingVATFL();
            if (Request.QueryString["A"] != null && Request.QueryString["A"].Trim() != string.Empty)
            {
                UT = new CommonBL().GetBottelingVATFL(-1, short.Parse(new Crypto().Decrypt(Request.QueryString["A"].Trim())), "Z");
            }
            ViewBag.Msg = TempData["Msg"];
            ViewBag.Brewery = CommonBL.fillBrewery();
            ViewBag.BottelingVATFL = new CommonBL().GetBottelingVATListFL(short.Parse(CommonBL.fillBrewery()[0].Value.Trim()), -1, "Z");
            return View(UT);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BottelingVATFL(BottelingVATFL UT)
        {
            UT.BreweryId = short.Parse(CommonBL.fillBrewery()[0].Value);
            string str = new CommonDA().InsertUpdateBottelingVATFL(UT);
            TempData["Msg"] = str;
            return RedirectToAction("BottelingVATFL");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public string UpdateBottelingVAT(string UTId, string Status)
        {
            BottelingVATFL UT = new BottelingVATFL();
            UT.BottelingVATId = int.Parse(new Crypto().Decrypt(UTId));
            UT.Status = Status;
            UT.Type = 3;
            string str = new CommonDA().InsertUpdateBottelingVATFL(UT);
            return str;
        }
        public ActionResult BottelingPlanFL()
        {
            BottelingPlanFL BP = new BottelingPlanFL();
            ViewBag.Msg = TempData["Message"];
            ViewBag.Brand = CommonBL.fillBrand("S");
            List<SelectListItem> lstBBT = CommonBL.fillBottelingVATFL("S");
            ViewBag.BBT = lstBBT; 
            ViewBag.Msg = TempData["Message"];
            if (Request.QueryString["A"] != null && Request.QueryString["A"].ToString().Trim() != string.Empty)
            {
                BP = (new CommonBL().GetBottelingPlanFL(CommonBL.Setdate("01/01/1900"), DateTime.Now, short.Parse(CommonBL.fillBrewery()[0].Value), -1, "", "", int.Parse(new Crypto().Decrypt(Request.QueryString["A"].Trim())), "PB"));
            }
            return View(BP);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult BottelingPlanFL(BottelingPlanFL BP)
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
             
            string str = new CommonDA().InsertUpdatePlanFL(BP);
            TempData["Message"] = str;
            if (BP.Type == 1)
            {
                return RedirectToAction("BottelingPlanFL");
            }
            else
            {
                return RedirectToAction("BottelingPlanFL", new { A = BP.EncPlanId });
            }
        }
        public string GetBBTForDDl(string BBTId)
        {

            BottelingVATFL bbtFormation = new CommonBL().GetBottelingVATFL(short.Parse(CommonBL.fillBrewery()[0].Value), short.Parse(BBTId), "A");
            return bbtFormation.BottelingVATBulkLitre.ToString() + "," + bbtFormation.BottelingVATCapacity.ToString() + "," + bbtFormation.BrandName + "," + bbtFormation.BrandId + "," + bbtFormation.BottelingVATStrength + "," + bbtFormation.BottelingVATAlcoholicLiter + "," + bbtFormation.BatchNo;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetBottlingTankForddl(string ddlBBT)
        {
            return Json(CommonBL.BottlingLineFL("S", ddlBBT), JsonRequestBehavior.AllowGet);

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
                    str = ds.Tables[0].Rows[0]["LiquorType"].ToString().Trim() + "," + ds.Tables[0].Rows[0]["LicenceType"].ToString().Trim() + "," + ds.Tables[0].Rows[0]["LicenceNo"].ToString().Trim() + "," + ds.Tables[0].Rows[0]["QuantityInCase"].ToString().Trim() + "," + ds.Tables[0].Rows[0]["QuantityInBottleML"].ToString().Trim() + "," + ds.Tables[0].Rows[0]["AlcoholType"].ToString().Trim() + "," + ds.Tables[0].Rows[0]["StateName"].ToString().Trim() + "," + ds.Tables[0].Rows[0]["BrandRegistrationNumber"].ToString().Trim();
                }
                catch
                {
                    str = "";
                }
            }
            return str;
        }

        [HttpGet]
        public ActionResult EditFinalBottelingPlanFL()
        {
            return View(new CommonBL().GetBottelingPlanListFL(CommonBL.Setdate("01/01/1900"), DateTime.Now, short.Parse(CommonBL.fillBrewery()[0].Value), -1, "Z", "", -1, "PB"));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public string FinalizePlan(string PlanId)
        {
            string str = "";
            try
            {
                BottelingPlanFL Plan = new BottelingPlanFL();
                PlanId = new Crypto().Decrypt(PlanId);
                Plan.Type = 3;
                Plan.PlanId = int.Parse(PlanId);
                Plan.IsPlanFinal = 1;
                str = new CommonDA().InsertUpdatePlanFL(Plan);
            }
            catch (Exception x)
            {
                str = x.Message.ToString();
            }
            return str;
        }
        [HttpGet]
        public ActionResult SearchplanFL()
        {
            List<SelectListItem> BrandList = new List<SelectListItem>();
            ViewBag.Brand = CommonBL.fillBrand("A");
            return View(new CommonBL().GetBottelingPlanListFL(DateTime.Now, DateTime.Now, short.Parse(CommonBL.fillBrewery()[0].Value), -1, "Z", "", -1, "Z"));
        }
        [HttpGet]
        public ActionResult GenerateQRCodeFL()
        {
            ViewBag.Brand = CommonBL.fillBrand("A");
            return View(new CommonBL().GetBottelingPlanListFL(CommonBL.Setdate("01/01/1900"), DateTime.Now, short.Parse(CommonBL.fillBrewery()[0].Value), -1, "Z", "", -1, "Z"));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public string GenreateQR(string PlanId)
        {
            string str = "";
            try
            {
                PlanId = new Crypto().Decrypt(PlanId);
                string UserId = (Session["tbl_Session"] as DataTable).Rows[0]["UserId"].ToString().Trim();
                str = new CommonDA().GenerateQRCodeFL(int.Parse(PlanId), UserId, "");
            }
            catch (Exception x)
            {
                str = x.Message.ToString();
            }
            return str;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public FileResult Export(string PlanId)
        {
            PlanId = new Crypto().Decrypt(PlanId);
            DataSet ds = new DataSet();
            ds = new CommonDA().GetQRCOdeFL(int.Parse(PlanId));

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
        public ActionResult ProductionPlanListFL()
        {
            List<BottelingPlanFL> BPList = new List<BottelingPlanFL>();
            ViewBag.Brand = CommonBL.fillBrand("A");
            return View(new CommonBL().GetBottelingPlanListFL(CommonBL.Setdate("01/01/1900"), DateTime.Now, short.Parse(CommonBL.fillBrewery()[0].Value), -1, "Z", "", -1, "FB"));
        }

        public ActionResult ProductionEntryFL(string A)
        {
            // TempData["Message"] = "";
            ViewBag.Msg = "";
            A = new Crypto().Decrypt(A);
            int Planid = int.Parse(A);
            return View(new CommonBL().GetBottelingPlanFL(CommonBL.Setdate("01/01/1900"), DateTime.Now, short.Parse(CommonBL.fillBrewery()[0].Value), -1, "Z", "", Planid, "FB"));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductionEntryFL(BottelingPlanFL BP)
        {
            string UserId = (Session["tbl_Session"] as DataTable).Rows[0]["UserId"].ToString().Trim();
            BP.Type = 1;
            string str = new CommonDA().InsertUpdateProductionPlanFL(BP);
            TempData["Message"] = str.Trim();
            TempData.Keep();
            //ViewBag.Msg = str.Trim();
            return RedirectToAction("ProductionEntryFL", new { A = BP.EncPlanId });
        }
        [HttpGet]
        public ActionResult FreezePlanFL()
        {
            return View(new CommonBL().GetBottelingPlanListFL(CommonBL.Setdate("01/01/1900"), DateTime.Now, short.Parse(CommonBL.fillBrewery()[0].Value), -1, "Z", "", -1, "FB"));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public string FreezePlanSuccess(string PlanId)
        {
            string str = "";
            try
            {
                PlanId = new Crypto().Decrypt(PlanId);
                BottelingPlanFL Plan = new BottelingPlanFL();
                Plan.Type = 2;
                Plan.PlanId = int.Parse(PlanId);
                Plan.IsProductionFinal = 1;
                str = new CommonDA().InsertUpdateProductionPlanFL(Plan);
            }
            catch (Exception x)
            {
                str = x.Message.ToString();
            }
            return str;
        }
        [HttpGet]
        public ActionResult UploadCSVFL()
        {
            List<BottelingPlanFL> lstBotteling = new CommonBL().GetBottelingPlanListFL(CommonBL.Setdate("01/01/1900"), DateTime.Now, -1, -1, "Z", "", -1, "FP");
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
        public ActionResult SearchProductionFL()
        {

            ViewBag.Brand = CommonBL.fillBrand("A");
            return View(new CommonBL().GetBottelingPlanListFL(CommonBL.Setdate("01/01/1900"), DateTime.Now, short.Parse(CommonBL.fillBrewery()[0].Value), -1, "Z", "", -1, "Z"));
        }
        public ActionResult Track_staus()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
                        str = CSV.ValidateCSVFL(1, int.Parse(new Crypto().Decrypt(files.Keys[0])), -1, file);
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
        [HttpGet]
        public ActionResult BottlingLineFL()
        {
            BottlingLineFL RM = new BottlingLineFL();
            ViewBag.BBT = CommonBL.fillBottelingVATFL("S");
            if (Request.QueryString["A"] != null && Request.QueryString["A"].Trim() != string.Empty)
            {

                RM = new CommonBL().GetBottlingLineFL(short.Parse(CommonBL.fillBrewery()[0].Value), -1, int.Parse(new Crypto().Decrypt(Request.QueryString["A"].Trim())), "Z");
            }
            ViewBag.Msg = TempData["Msg"];
            ViewBag.Brewery = CommonBL.fillBrewery();
            ViewBag.UnitTank = new CommonBL().GetBottlingLineDetailsFL(short.Parse(CommonBL.fillBrewery()[0].Value.Trim()), -1, -1, "Z");
            return View(RM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BottlingLineFL(BottlingLineFL RM)
        {
            RM.UnitId = short.Parse(CommonBL.fillBrewery()[0].Value);
            string str = new CommonDA().InsertUpdateBottlingLineFL(RM);
            TempData["Msg"] = str;
            return RedirectToAction("BottlingLineFL");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public string UpdateBottlingLineFL(string UTId, string Status)
        {
            BottlingLineFL BL = new BottlingLineFL();
            BL.BottlingLineId = int.Parse(new Crypto().Decrypt(UTId));
            BL.BottlingLineStatus = Status;
            BL.Type = 3;
            string str = new CommonDA().InsertUpdateBottlingLineFL(BL);
            return str;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult GetStorageVATDetails(string SVID)
        {
            string[] result = new string[5];
            DataTable dt = new CommonDA().GetStorageVAT(short.Parse(CommonBL.fillBrewery()[0].Value), short.Parse(SVID.Trim()), "A").Tables[0];
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    result[0] = dt.Rows[0]["StorageVATCapacity"].ToString().Trim();
                    result[1] = dt.Rows[0]["StorageVATBulkLiter"].ToString().Trim();
                    result[2] = dt.Rows[0]["StorageVATStrength"].ToString().Trim();
                    result[3] = dt.Rows[0]["StorageVATAlcoholicLiter"].ToString().Trim();
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
            //StorageVATCL Ut = new CommonBL().GetStorageVAT(short.Parse(CommonBL.fillBrewery()[0].Value), short.Parse(SVID.Trim()), "A");
            //return Ut.StorageVATCapacity.ToString() + "," + Ut.SpiritType.ToString() + "," + Ut.StorageVATBulkLitre.ToString()+","+Ut.SpiritType+","+Ut.StorageVATAlcoholicLiter;
        }
        [HttpGet]
        public ActionResult ReceiveStorageVATFL()
        {
            TankTransferDetail SVBL = new TankTransferDetail();
            ViewBag.IssuedFromSVId = CommonBL.fillStorageVAT("S");
            ViewBag.SpiritType = CommonBL.fillSpiritType("S");
            SVBL.SpiritTypeId = 1;
            if (TempData["Message"] != null)
            {
                ViewBag.Msg = TempData["Message"].ToString();
            }
            return View(SVBL);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReceiveStorageVATFL(TankTransferDetail UTBL)
        {
            UTBL.TransactionType = "RS";
            string str = new CommonDA().InsertTankTransferDetail(UTBL);
            TempData["Message"] = str;
            return RedirectToAction("ReceiveStorageVATFL");
        }
        [HttpGet]
        public ActionResult StorageVATRecevDetails()
        {
            /*ViewBag.UnitTank = CommonBL.fillUnitTank("A");
         List<SVTransferToBV> lstUtBl = new CommonBL().GetSVTransferToBTList(CommonBL.Setdate("01/01/1900"), DateTime.Now, -1, "R", short.Parse(CommonBL.fillBrewery()[0].Value), -1);
         return View(lstUtBl);*/
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult GetBottlingTankdetails(string BTID)
        {
            string[] result = new string[8];
            DataTable dt = new CommonDA().GetBottelingVATDetails(short.Parse(CommonBL.fillBrewery()[0].Value.Trim()), short.Parse(BTID), "A").Tables[0];
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    result[0] = dt.Rows[0]["BrandName"].ToString().Trim();
                    result[1] = dt.Rows[0]["BottelingVATCapacity"].ToString().Trim();
                    result[2] = dt.Rows[0]["BottelingVATStrength"].ToString().Trim();
                    result[3] = dt.Rows[0]["BottelingVATAlcoholicLiter"].ToString().Trim();
                    result[4] = dt.Rows[0]["BottelingVATBulkLiter"].ToString().Trim();
                    result[5] = dt.Rows[0]["SpiritType"].ToString().Trim();
                    result[6] = dt.Rows[0]["BrandId"].ToString().Trim();
                    result[7] = dt.Rows[0]["BatchNo"].ToString().Trim();
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ReceiveSplitTfReceiverVat()
        {
            TankTransferDetail SVBL = new TankTransferDetail();
            ViewBag.IssuedFromSVId = CommonBL.fillStorageVAT("S");
            ViewBag.SpiritType = CommonBL.fillSpiritType("S");
            SVBL.SpiritTypeId = 1;
            if (TempData["Message"] != null)
            {
                ViewBag.Msg = TempData["Message"].ToString();
            }
            return View(SVBL);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult GetBlendingVATDetails(string BVId)
        {
            //BlendingVATCL BVT = new CommonBL().GetBlendingVAT(short.Parse(CommonBL.fillBrewery()[0].Value), short.Parse(BVId), "A");
            //return BVT.BlendingVATBulkLitre.ToString() + "," + BVT.BlendingVATCapacity.ToString() + "," + BVT.SpiritType + "," + BVT.BrandName + "," + BVT.BlendingVATAlcoholicLiter;
            string[] result = new string[8];
            DataTable dt = new CommonDA().GetBlendingVAT(short.Parse(CommonBL.fillBrewery()[0].Value), short.Parse(BVId), "A").Tables[0];
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    result[0] = dt.Rows[0]["BrandName"].ToString().Trim();
                    result[1] = dt.Rows[0]["BlendingVATCapacity"].ToString().Trim();
                    result[2] = dt.Rows[0]["BlendingVATStrength"].ToString().Trim();
                    result[3] = dt.Rows[0]["BlendingVATAlcoholicLiter"].ToString().Trim();
                    result[4] = dt.Rows[0]["BlendingVATBulkLiter"].ToString().Trim();
                    result[5] = dt.Rows[0]["SpiritType"].ToString().Trim();
                    result[6] = dt.Rows[0]["BrandId"].ToString().Trim();
                    result[7] = dt.Rows[0]["BatchNo"].ToString().Trim();
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult StorageVATTransferToBVFL()
        {
            TankTransferDetail TTD = new Models.TankTransferDetail();
            ViewBag.IssuedFromSVId = CommonBL.fillStorageVAT("S");
            ViewBag.BlendingVAT = CommonBL.fillBlendingVAT("S");

            if (TempData["Message"] != null)
            {
                ViewBag.Msg = TempData["Message"].ToString();
            }
            return View(TTD);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public string GetBlendingVATForDDl(string BVId)
        {
            BlendingVATFL BVT = new CommonBL().GetBlendingVATFL(short.Parse(CommonBL.fillBrewery()[0].Value), short.Parse(BVId), "A");
            return BVT.BlendingVATBulkLitre.ToString() + "," + BVT.BlendingVATCapacity.ToString() + "," + BVT.SpiritType + "," + BVT.BrandName + "," + BVT.BlendingVATAlcoholicLiter + "," + BVT.BlendingVATName + "," + BVT.BlendingVATStrength + "," + BVT.BrandId + "," + BVT.BatchNo;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult StorageVATTransferToBVFL(TankTransferDetailFL UTTBBT)
        {
            UTTBBT.TransactionType = "SB";
            string str = new CommonDA().InsertTankTransferDetailFL(UTTBBT);
            TempData["Message"] = str;
            return RedirectToAction("StorageVATTransferToBVFL");
        }
        [HttpGet]
        public ActionResult StorageVATTransferToBVDetails()
        {
            ViewBag.UnitTank = CommonBL.fillUnitTank("A");
            ViewBag.BBT = CommonBL.fillBBT("A");
            List<UTTransferToBBTFL> lstUtBl = new CommonBL().GetUTTransferToBBTListFL(CommonBL.Setdate("01/01/1900"), DateTime.Now, -1, "T", short.Parse(CommonBL.fillBrewery()[0].Value), -1);
            return View(lstUtBl);
        }

        [HttpGet]
        public ActionResult GetBottelingVATDetailsFL()
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
        public ActionResult GatePassFL()
        {
            GatePassDetailsFL GP = new GatePassDetailsFL();
            DataSet ds = new CommonDA().GetUnitDetails(-1, "", "", -1, -1, -1, UserSession.LoggedInUserId);
            GP = new CommonBL().GetGatePassDetailsGFL(-1, CommonBL.Setdate("01/01/1900"), CommonBL.Setdate("31/12/3999"), 2, "P", "P", ds.Tables[0].Rows[0]["UnitLicenseno"].ToString().Trim(), "", ds.Tables[0].Rows[0]["UnitLicenseType"].ToString().Trim(), "");
            ViewBag.Msg = TempData["Message"];

            List<SelectListItem> ToLicenseTypes = new List<SelectListItem>();
            List<SelectListItem> FL1Licence = new List<SelectListItem>();
            SelectListItem SLI = new SelectListItem();

            SLI = new SelectListItem();
            SLI.Text = "FL-2";
            SLI.Value = "FL-2";
            ToLicenseTypes.Add(SLI);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                FL1Licence = CommonBL.fillFL1Licence(int.Parse(ds.Tables[0].Rows[0]["UnitId"].ToString().Trim()));
                if (GP.FromLicenseType.Trim() == string.Empty)
                {
                    if (ds.Tables[0].Rows[0]["UnitLicenseType"].ToString().Trim() == "PD-2")
                    {
                        GP.FromLicenseType = "PD-2";
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
        [ValidateAntiForgeryToken]
        public ActionResult GatePassFL(GatePassDetails GP)
        {
            if (GP.Receiver == null)
            {
                GP.Receiver = "";
            }
            if (GP.ImportPermitNo == null)
            {
                GP.ImportPermitNo = "";
            }
            GP.GatepassLicenseNo = "PD-25A";
            GP.GatePassSourceId = long.Parse(UserSession.LoggedInUserLevelId);
            GP.UploadValue = 2;
            GP.FromDate = CommonBL.Setdate(GP.FromDate1.Trim());
            GP.ToDate = CommonBL.Setdate(GP.ToDate1.Trim());
            string str = new CommonDA().InsertUpdateGatePassDetails(GP);
            TempData["Message"] = str;
            return RedirectToAction("GatePassFL");
        }
        [HttpGet]
        public ActionResult UploadGatePassCSVFL(string A, string B)
        {
            GatePassDetailsFL GP = new GatePassDetailsFL();
            ViewBag.Brand = CommonBL.fillBrand("A");
            GP = new CommonBL().GetGatePassDetailsGFL(-1, CommonBL.Setdate("01/01/1900"), CommonBL.Setdate("31/12/4000"), 2, "P", "P", "", "", "", "");
            return View(GP);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadGatePassCSVFL()
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
                        str = CSV.ValidateCSVFL(2, -1, int.Parse(files.Keys[0].Replace("file", "")), file);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
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
            return new CommonDA().FinalGatePassFL(GatePass, 1, 0);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public string UploadVerifedCSV(string GatePassId, string BrandId, string BatchNo, string UploadValue, string PlanId, string BLID)
        {
            long GatePass = long.Parse(GatePassId);
            int Brand = int.Parse(BrandId);
            int Plan = int.Parse(PlanId);
            short BottlingLineId = short.Parse(BLID);
            return new CommonDA().UploadCSVFL(GatePass, (Session["CaseCode"] as DataTable), int.Parse(UploadValue), Brand, BatchNo, Plan, BottlingLineId);
        }
        public ActionResult GetPassDetailsFL()
        {
            List<GatePassDetailsFL> lstGPD = new List<GatePassDetailsFL>();
            lstGPD = new CommonBL().GetGatePassDetailsListFL(-1, CommonBL.Setdate("01/01/1900"), CommonBL.Setdate("31/12/4000"), 2, "Z", "P", "", "", "", "");
            return View(lstGPD);
        }
        //public ActionResult ReceiveGatePassWH()
        //{
        //    List<GatePassDetailsFL> lstGPD = new List<GatePassDetailsFL>();
        //    lstGPD = new CommonBL().GetGatePassDetailsListFL(-1, CommonBL.Setdate("01/01/1900"), CommonBL.Setdate("31/12/4000"), 2, "A", "P");
        //    return View(lstGPD);
        //}
        //public string ReceiveGatePass(string GatePassId, string DamageBottles)
        //{
        //    string str = new CommonDA().FinalGatePass(long.Parse(GatePassId.Trim()), 2, int.Parse(DamageBottles.Trim()));
        //    return str;
        //}
        /*Copy From Vijay For Show Gate Pass*/
        public ActionResult GatePassPreviewFL()
        {
            GatePassDetailsFL GP = new GatePassDetailsFL();
            long GatePassId = long.Parse(new Crypto().Decrypt(Request.QueryString["GatePass"].Trim()));
            GP = new CommonBL().GetGatePassDetailsGFL(GatePassId, CommonBL.Setdate("01/01/1900"), CommonBL.Setdate("31/12/4000"), 2, "Z", "Z", "", "", "", "");
            string qrcode = "EXCISE";
            ViewBag.QRCodeImage = GenerateQRCode(qrcode);
            ViewBag.PassType = "PD-25A";
            ViewBag.GetGatePassBrandDetailsList = new CommonBL().GetGatePassBrandDetailsList(GatePassId);
            return View(GP);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        #region New for FL
        [HttpGet]
        public ActionResult BlendingVATtfBottelingVATFL()
        {
            TankTransferDetailFL TTD = new TankTransferDetailFL();
            ViewBag.BlendingVAT = CommonBL.fillBlendingVAT("S");
            ViewBag.BottelingVAT = CommonBL.fillBottelingVATFL("S");
            if (TempData["Message"] != null)
            {
                ViewBag.Msg = TempData["Message"].ToString();
            }
            return View(TTD);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BlendingVATtfBottelingVATFL(TankTransferDetailFL TTD)
        {

            TTD.TransactionType = "BB";
            string str = new CommonDA().InsertTankTransferDetailFL(TTD);
            TempData["Message"] = str;
            return RedirectToAction("BlendingVATtfBottelingVATFL");
        }
        [HttpGet]
        public ActionResult BlendingVATReductionDetails()
        {
            BlendingVATReductionFL BVR = new BlendingVATReductionFL();
            ViewBag.BlendingVAT = CommonBL.fillBlendingVAT("S");
            ViewBag.Brand = CommonBL.fillBrand("S");
            if (TempData["Message"] != null)
            {
                ViewBag.Msg = TempData["Message"].ToString();
            }
            return View(BVR);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BlendingVATReductionDetails(BlendingVATReductionFL BVR)
        {
            string str = "";
            BVR.ReductionDate = CommonBL.Setdate(BVR.ReductionDate1);
            if (BVR.Remarks == null)
            {
                BVR.Remarks = "NA";
            }
            if (BVR.BatchNo == null || BVR.BatchNo.Trim() == string.Empty)
            {
                str = "Please Enter Batch No";
            }
            if (BVR.BrandID == -1)
            {
                str = "Please Select Brand";
            }
            if (BVR.AfterRedAL <= 0)
            {
                str = "Blending VAT AL (After Reduction) must be greater than Zero";
            }
            if (BVR.AfterRedStrength <= 0)
            {
                str = "Blending VAT Strength (After Reduction) must be greater than Zero";
            }
            if (BVR.AfterRedBL <= 0)
            {
                str = "Blending VAT BL (After Reduction) must be greater than Zero";
            }
            if (BVR.AfterRedStrength > BVR.BeforeRedStrength)
            {
                str = "Blending VAT Strength (Before Reduction) must be greater than Blending VAT Strength (After Reduction)";
            }
            if (BVR.AfterRedBL < BVR.BeforeRedBL)
            {
                str = "Blending VAT BL (After Reduction) must be greater than Blending VAT BL (After Reduction)";
            }
            BVR.IsSophistication = false;
            if (str.Trim() == string.Empty)
            {
                str = new CommonDA().InsertBlendingVATReductionDetailsFL(BVR);
            }
            TempData["Message"] = str;
            return RedirectToAction("BlendingVATReductionDetails");
        }
        [HttpGet]
        public ActionResult BlendingVATSophisticationDetails()
        {
            BlendingVATReductionFL BVR = new BlendingVATReductionFL();
            ViewBag.BlendingVAT = CommonBL.fillBlendingVAT("S");
            ViewBag.Brand = CommonBL.fillBrand("S");
            if (TempData["Message"] != null)
            {
                ViewBag.Msg = TempData["Message"].ToString();
            }
            return View(BVR);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BlendingVATSophisticationDetails(BlendingVATReductionFL BVR)
        {
            BVR.ReductionDate = CommonBL.Setdate(BVR.ReductionDate1);
            BVR.IsSophistication = true;
            if (BVR.Remarks == null)
            {
                BVR.Remarks = "NA";
            }
            string str = new CommonDA().InsertBlendingVATReductionDetailsFL(BVR);
            TempData["Message"] = str;
            return RedirectToAction("BlendingVATReductionDetails");
        }
        #endregion
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult GetReceiverTankdetails(string Rid)
        {
            string[] result = new string[8];
            DataTable dt = new CommonDA().GetReciverFL(short.Parse(CommonBL.fillBrewery()[0].Value.Trim()), short.Parse(Rid), "A").Tables[0];
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    result[0] = dt.Rows[0]["Reciver_Capacity"].ToString().Trim();
                    result[1] = dt.Rows[0]["BL"].ToString().Trim();
                    result[2] = dt.Rows[0]["AL"].ToString().Trim();
                    result[3] = dt.Rows[0]["Strength"].ToString().Trim();
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);

        }
    }
}