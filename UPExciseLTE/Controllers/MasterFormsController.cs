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
            ViewBag.StateList = CommonBL.fillState("S");
            ViewBag.Msg = TempData["Message"]; 
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
            TempData["Message"] = str;
            if (B.SPType==1)
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
            List<BrandMaster> lstBrand = new CommonBL().GetBrandList(-1, "", "", "", -1, -1, -1, "Z");
            ViewBag.Brewery = CommonBL.fillBrewery();
            ViewBag.StateList = CommonBL.fillState("A");
            return View(lstBrand);
        }
        [HttpPost]
        public ActionResult GetBrandDetails(FormCollection frm)
        {
            int StateId = -1;// int.Parse(frm["ddlState"].Trim());
            string LicenseType = "";// frm["ddlLicenseType"].Trim();
            string LicenseNo = "";// frm["txtLicenseNo"].Trim();
            List<BrandMaster> lstBrand = new CommonBL().GetBrandList(-1,  "", LicenseType, LicenseNo, -1, -1, StateId, "Z");
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
        public string FinalBrand(string BrandId)
        {
            string str = "";
            try
            {
                str = new CommonDA().UpdateBrand("", int.Parse(BrandId), 1);
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
        [HttpGet]
        public ActionResult BottelingPlan()
        {
            ViewBag.Msg = TempData["Message"];
            ViewBag.Brand = CommonBL.fillBrand("S");
            
            if (Request.QueryString["A"] != null && Request.QueryString["A"].ToString().Trim() != string.Empty)
            {
                return View(new CommonBL().GetBottelingPlan(CommonBL.Setdate("01/01/1900"), DateTime.Now, -1, -1, "", "", int.Parse(new Crypto().Decrypt(Request.QueryString["A"].Trim())), "PB"));
            }
            else
            {
                return View(new BottelingPlan());
            }
            
        }
        [HttpGet]
        public string GetBrandDetailsForDDl(string BrandId)
        {
            string str = "";
            DataSet ds = new CommonDA().GetBrandDetail(int.Parse(BrandId), "", "", "", -1, -1, -1, "A");
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                try
                {
                    str = ds.Tables[0].Rows[0]["LiquorType"].ToString().Trim() + "," + ds.Tables[0].Rows[0]["LicenceType"].ToString().Trim() + "," + ds.Tables[0].Rows[0]["LicenceNo"].ToString().Trim() + "," + ds.Tables[0].Rows[0]["QuantityInCase"].ToString().Trim() + "," + ds.Tables[0].Rows[0]["QuantityInBottleML"].ToString().Trim() + "," + ds.Tables[0].Rows[0]["AlcoholType"].ToString().Trim() + "," + ds.Tables[0].Rows[0]["state_name_u"].ToString().Trim();
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
            catch {
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
            return View(new CommonBL().GetBottelingPlanList(CommonBL.Setdate("01/01/1900"), DateTime.Now, -1, -1, "Z", "", -1, "PB"));
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
            return View(new CommonBL().GetBottelingPlanList(DateTime.Now, DateTime.Now, -1, -1, "Z", "", -1, "Z"));
        }
        [HttpGet]
        public ActionResult GenerateQRCode()
        {
            ViewBag.Brand = CommonBL.fillBrand("A");
            return View(new CommonBL().GetBottelingPlanList(CommonBL.Setdate("01/01/1900"), DateTime.Now, -1, -1, "Z", "", -1, "Z"));
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
            return View(new CommonBL().GetBottelingPlanList(CommonBL.Setdate("01/01/1900"), DateTime.Now, -1, -1, "Z", "", -1, "FB"));
        }
        public ActionResult ProductionEntry(string A)
        {
            ViewBag.Msg = "";
            A = new Crypto().Decrypt(A);
            int Planid = int.Parse(A);         
            return View(new CommonBL().GetBottelingPlan(CommonBL.Setdate("01/01/1900"), DateTime.Now, -1, -1, "Z", "",Planid, "FB"));
        }
        [HttpPost]
        public ActionResult ProductionEntry(BottelingPlan BP)
        {
            string UserId = (Session["tbl_Session"] as DataTable).Rows[0]["UserId"].ToString().Trim();
            BP.Type = 1;
            string str = new CommonDA().InsertUpdateProductionPlan(BP);
            return RedirectToAction("ProductionEntry", new { A = BP.EncPlanId});
        }
        [HttpGet]
        public ActionResult FreezePlan()
        {
            return View(new CommonBL().GetBottelingPlanList(CommonBL.Setdate("01/01/1900"), DateTime.Now, -1, -1, "Z", "", -1, "FB"));
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
        public ActionResult UploadCSV()
        {
            return View(new CommonBL().GetBottelingPlanList(CommonBL.Setdate("01/01/1900"), DateTime.Now, -1, -1, "Z", "", -1, "FP"));
        }
        [HttpGet]
        public ActionResult SearchProduction()
        {
            
            ViewBag.Brand = CommonBL.fillBrand("A");
            return View(new CommonBL().GetBottelingPlanList(CommonBL.Setdate("01/01/1900"),DateTime.Now,-1,-1,"Z","",-1,"Z"));
        }
        public ActionResult Track_staus()
        {
            return View();
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
                            sb.Append("'");
                            sb.Append(line);
                            sb.Append("'");
                            count++;
                        }
                        else
                        {
                            sb.Append(",");
                            sb.Append("'");
                            sb.Append(line);
                            sb.Append("'");
                        }
                    }
                    ViewBag.Error = "File Upload Successfully";

                }
                else
                {
                    ViewBag.Error = "Please Upload Files in .csv format";

                }

            }

            return RedirectToAction("UploadCSV");
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
        public ActionResult UnitTank()
        {
            UnitTank UT = new UnitTank();
            return View(UT);
        }
    }

}
