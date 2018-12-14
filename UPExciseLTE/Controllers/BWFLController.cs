using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using UPExciseLTE.BLL;
using UPExciseLTE.DAL;
using UPExciseLTE.Filters;
using UPExciseLTE.Models;

namespace UPExciseLTE.Controllers
{
    [SessionExpireFilter]
    //[CheckAuthorization]
    [HandleError(ExceptionType = typeof(DbUpdateException), View = "Error")]
    public class BWFLController : Controller
    {
        // GET: BWFL
        [HttpGet]
        public ActionResult FormFL21()
        {
            FormFL21 FL21 = new FormFL21();
            Session["lstFL21BrandMapp"] = null;
            if (Request.QueryString["Code"] != null && Request.QueryString["Code"].Trim() != string.Empty)
            {
                FL21 = new CommonBL().GetFormFL21(int.Parse(new Crypto().Decrypt(Request.QueryString["Code"].Trim())), "Z");
                Session["lstFL21BrandMapp"] = FL21.lstFL21;
            }
            ViewBag.Brand = CommonBL.fillBrand("S");
            DataSet ds = new CommonDA().GetUnitDetails(-1, "", "", -1, -1);
            if (FL21.FromConsignorName.Trim() == string.Empty)
            {
                FL21.ToConsigeeName = ds.Tables[0].Rows[0]["UnitName"].ToString().Trim();
                FL21.ToLicenceNo = ds.Tables[0].Rows[0]["UnitLicenseno"].ToString().Trim();
                FL21.ToConsigeeAddress = ds.Tables[0].Rows[0]["UnitAddress"].ToString().Trim();
            }
            if (TempData["str"] != null)
            {
                ViewBag.Msg = TempData["str"];
            }
            return View(FL21);
        }
        [HttpPost]
        public ActionResult FormFL21(FormFL21 FL)
        {
            FL.lstFL21 = (Session["lstFL21BrandMapp"] as List<FL21BrandMapp>);
            foreach (FL21BrandMapp FLBM in FL.lstFL21)
            {
                FL.BoxSize += FLBM.BoxSize;
                FL.DutyCalculated += FLBM.DutyCalculated;
                FL.PermitFees= FLBM.PermitFees;
                FL.QuantityInBottleML+= FLBM.Quantity;
                FL.RateofPermit = FLBM.RateofPermit;
                FL.TotalBL+= FLBM.TotalBL;
                FL.TotalBottle+= FLBM.TotalBottle;
                FL.TotalCase+= FLBM.TotalCase;
                FL.TotalFees+= FLBM.TotalFees;
            }
            string str = new CommonDA().InsertUpdateFormFL21(FL);
            TempData["str"] = str;
            if (str.Contains("Successfully"))
            {
                Session["lstFL21BrandMapp"] = null;
            }
            return RedirectToAction("FormFL21");
        }
        [HttpGet]
        public ActionResult FinalizeFormFL21()
        {
            return View(new CommonBL().GetFormFL21List(-1, "P"));
        }
        public string FinalFormFL21(string FL21ID, string status, string reason)
        {
            string str = "";
            try
            {
                FormFL21 FL21 = new FormFL21();
                FL21.FL21ID = int.Parse(FL21ID);
                FL21.SPType = 3;
                FL21.FL21Status = status;
                
                str = new CommonDA().InsertUpdateFormFL21(FL21);
            }
            catch (Exception x)
            {
                str = x.Message.ToString();
            }
            return str;
        }
        [HttpGet]
        public ActionResult FormFL21List()
        {
            return View(new CommonBL().GetFormFL21List(-1, "Z"));
        }
        [HttpGet]
        public ActionResult PreviewFL22()
        {
            FormFL21 FL21 = new FormFL21();
            if (Request.QueryString["Code"] != null && Request.QueryString["Code"].Trim() != string.Empty)
            {
                FL21 = new CommonBL().GetFormFL21(int.Parse(new Crypto().Decrypt(Request.QueryString["Code"].Trim())), "Z");
            }
            return View(FL21);
        }
        public string FL21BrandMapp(string BrandId, string TotalCase, string RateofPermit)
        {
            List<FL21BrandMapp> lstFL21BrandMapp = (Session["lstFL21BrandMapp"] as List<FL21BrandMapp>);
            if (lstFL21BrandMapp==null)
            {
               lstFL21BrandMapp = new List<FL21BrandMapp>();
            }
            FL21BrandMapp FL21 = new FL21BrandMapp();
            
            FL21.BrandId = int.Parse(BrandId);
            FL21.TotalCase = int.Parse(TotalCase);
            FL21.RateofPermit = decimal.Parse(RateofPermit);
            BrandMaster BL = new CommonBL().GetBrand(int.Parse(BrandId), "", "", "", short.Parse(CommonBL.fillBrewery()[0].Value), -1, -1, "A");
            FL21.Brand = BL.BrandName;
            FL21.BoxSize = BL.QuantityInCase;
            FL21.TotalBottle = (BL.QuantityInCase * FL21.TotalCase);
            FL21.TotalBL = (FL21.TotalBottle * BL.QuantityInBottleML)/1000;
            FL21.DutyCalculated= FL21.TotalBL * FL21.RateofPermit;
            FL21.FL21BrandMappId = -1;
            FL21.PermitFees = 0;
            FL21.Quantity = BL.QuantityInBottleML;
            FL21.RateofPermit = FL21.RateofPermit;
            FL21.TotalFees = FL21.DutyCalculated;
            lstFL21BrandMapp.Add(FL21);
            Session["lstFL21BrandMapp"] = lstFL21BrandMapp;
            StringBuilder sb = new StringBuilder();

            sb.Append("<div class='row'><table class='table table-striped table-bordered table-hover'><tr><th>Srno</th><th>Brand Name</th><th>Box Size</th><th>Quantity (In Bottle, In ml.)</th><th>Total Case</th><th>Total Bottle</th><th>Total BL</th><th>Import Pass fee (per Ltr.)</th><th>Duty Calculated</th></tr>");
            int count = 0;
            foreach (FL21BrandMapp FL21BM in lstFL21BrandMapp)
            {
                count++;
                sb.Append("<tr>");
                sb.Append("<td>"); sb.Append(count); sb.Append("</td>");
                sb.Append("<td>"); sb.Append(FL21BM.Brand); sb.Append("</td>");
                sb.Append("<td>"); sb.Append(FL21BM.BoxSize); sb.Append("</td>");
                sb.Append("<td>"); sb.Append(FL21BM.Quantity); sb.Append("</td>");
                sb.Append("<td>"); sb.Append(FL21BM.TotalCase); sb.Append("</td>");
                sb.Append("<td>"); sb.Append(FL21BM.TotalBottle); sb.Append("</td>");
                sb.Append("<td>"); sb.Append(FL21BM.TotalBL); sb.Append("</td>");
                sb.Append("<td>"); sb.Append(FL21BM.RateofPermit); sb.Append("</td>");
                sb.Append("<td>"); sb.Append(FL21BM.DutyCalculated); sb.Append("</td>");
                sb.Append("</tr>");

            }
            sb.Append("</table></div>");
            return sb.ToString();
        }
        public ActionResult FormFL21List(string hfFormId, HttpPostedFileBase fileChallan, string txtTotalFees,string txtBankName,string txtTransactionDate)
        {
            Challan Ch = new Challan();
            Ch.BankName = txtBankName.Trim();
            Ch.ChallanId = -1;
            Ch.FL21Ids = hfFormId.Trim();
            Ch.TransactionDate = CommonBL.Setdate(txtTransactionDate.Trim());
            Ch.TotalFees = decimal.Parse(txtTotalFees.Trim());
            Byte[] img = null;
            if (fileChallan != null && fileChallan.ContentLength > 0)
            {   /*****IMG-DB-CODE******/
                int FileSize = fileChallan.ContentLength;
                img = new Byte[FileSize];
                fileChallan.InputStream.Read(img, 0, FileSize);
                Ch.ChallanPhoto = img;
            }
            string str = new CommonDA().InsertUpdateChallan(Ch); 
            return RedirectToAction("FormFL21List");
        }
    }
}