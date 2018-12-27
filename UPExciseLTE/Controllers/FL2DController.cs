﻿using System;
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
    public class FL2DController : Controller
    {
        // GET: FL2D

        [HttpGet]
        public ActionResult FormFL33()
        {
            FormFL33 FL33 = new FormFL33();
            Session["lstFL33BrandMapp"] = null;
            if (Request.QueryString["Code"] != null && Request.QueryString["Code"].Trim() != string.Empty)
            {
                FL33 = new CommonBL().GetFormFL33(int.Parse(new Crypto().Decrypt(Request.QueryString["Code"].Trim())), "Z");
                Session["lstFL33BrandMapp"] = FL33.lstFL33;
            }
            ViewBag.Brand = CommonBL.fillBrand("S");
            DataSet ds = new CommonDA().GetUnitDetails(-1, "", "", -1, -1);
            if (FL33.FromConsignorName.Trim() == string.Empty)
            {
                FL33.ToConsigeeName = ds.Tables[0].Rows[0]["UnitName"].ToString().Trim();
                FL33.ToLicenceNo = ds.Tables[0].Rows[0]["UnitLicenseno"].ToString().Trim();
                FL33.ToConsigeeAddress = ds.Tables[0].Rows[0]["UnitAddress"].ToString().Trim();
            }
            if (TempData["str"] != null)
            {
                ViewBag.Msg = TempData["str"];
            }
            return View(FL33);
        }
        [HttpPost]
        public ActionResult FormFL33(FormFL33 FL2D)
        {
            FL2D.lstFL33 = (Session["lstFL33BrandMapp"] as List<FL33BrandMapp>);
            foreach (FL33BrandMapp FLBM in FL2D.lstFL33)
            {
                FL2D.BoxSize += FLBM.BoxSize;
                FL2D.DutyCalculated += FLBM.DutyCalculated;
                FL2D.PermitFees = FLBM.PermitFees;
                FL2D.QuantityInBottleML += FLBM.Quantity;
                FL2D.RateofPermit = FLBM.RateofPermit;
                FL2D.TotalBL += FLBM.TotalBL;
                FL2D.TotalBottle += FLBM.TotalBottle;
                FL2D.TotalCase += FLBM.TotalCase;
                FL2D.TotalFees += FLBM.TotalFees;
            }
            string str = new CommonDA().InsertUpdateFormFL33(FL2D);
            TempData["str"] = str;
            if (str.Contains("Successfully"))
            {
                Session["lstFL33BrandMapp"] = null;
            }
            return RedirectToAction("FormFL33");
        }
        [HttpGet]
        public ActionResult FinalizeFormFL33()
        {
            string status = "F";
            if (Request.QueryString["V"] != null)
            {
                if (Request.QueryString["V"].ToString().Trim() != String.Empty)
                {
                    status = Request.QueryString["V"].ToString().Trim();
                }
            }
            return View(new CommonBL().GetFormFL33List(-1, status));
        }
        public string FinalFormFL33(string FL33ID, string status, string reason, string FromDate, string ToDate)
        {
            string str = "";
            try
            {

                FormFL33 FL33 = new FormFL33();
                FL33.FL33ID = int.Parse(FL33ID);
                FL33.SPType = 3;
                FL33.FL33Status = status;
                FL33.Reason = reason;
                if (status == "A")
                {
                    FL33.FromPermitDate = CommonBL.Setdate(FromDate);
                    FL33.ToPermitDate = CommonBL.Setdate(ToDate);
                }
                str = new CommonDA().InsertUpdateFormFL33(FL33);
            }
            catch (Exception x)
            {
                str = x.Message.ToString();
            }
            return str;
        }
        [HttpGet]
        public ActionResult FormFL33List()
        {
            return View(new CommonBL().GetFormFL33List(-1, "Z"));
        }
        [HttpGet]
        public ActionResult PreviewFL34()
        {
            FormFL33 FL33 = new FormFL33();
            if (Request.QueryString["Code"] != null && Request.QueryString["Code"].Trim() != string.Empty)
            {
                FL33 = new CommonBL().GetFormFL33(int.Parse(new Crypto().Decrypt(Request.QueryString["Code"].Trim())), "Z");
            }
            return View(FL33);
        }
        public string FL33BrandMapp(string BrandId, string TotalCase, string RateofPermit)
        {
            List<FL33BrandMapp> lstFL33BrandMapp = (Session["lstFL33BrandMapp"] as List<FL33BrandMapp>);
            if (lstFL33BrandMapp == null)
            {
                lstFL33BrandMapp = new List<FL33BrandMapp>();
            }
            FL33BrandMapp FL33 = new FL33BrandMapp();

            FL33.BrandId = int.Parse(BrandId);
            FL33.TotalCase = int.Parse(TotalCase);
            FL33.RateofPermit = decimal.Parse(RateofPermit);
            BrandMaster BL = new CommonBL().GetBrand(int.Parse(BrandId), "", "", "", short.Parse(CommonBL.fillBrewery()[0].Value), -1, -1, "A");
            FL33.Brand = BL.BrandName;
            FL33.BoxSize = BL.QuantityInCase;
            FL33.TotalBottle = (BL.QuantityInCase * FL33.TotalCase);
            FL33.TotalBL = (FL33.TotalBottle * BL.QuantityInBottleML) / 1000;
            FL33.DutyCalculated = FL33.TotalBL * FL33.RateofPermit;
            FL33.FL33BrandMappId = -1;
            FL33.PermitFees = 0;
            FL33.Quantity = BL.QuantityInBottleML;
            FL33.RateofPermit = FL33.RateofPermit;
            FL33.TotalFees = FL33.DutyCalculated;
            lstFL33BrandMapp.Add(FL33);
            Session["lstFL33BrandMapp"] = lstFL33BrandMapp;
            StringBuilder sb = new StringBuilder();

            sb.Append("<div class='row'><table class='table table-striped table-bordered table-hover'><tr><th>Srno</th><th>Brand Name</th><th>Box Size</th><th>Quantity (In Bottle, In ml.)</th><th>Total Case</th><th>Total Bottle</th><th>Total BL</th><th>Import Pass fee (per Ltr.)</th><th>Duty Calculated</th></tr>");
            int count = 0;
            foreach (FL33BrandMapp FL33BM in lstFL33BrandMapp)
            {
                count++;
                sb.Append("<tr>");
                sb.Append("<td>"); sb.Append(count); sb.Append("</td>");
                sb.Append("<td>"); sb.Append(FL33BM.Brand); sb.Append("</td>");
                sb.Append("<td>"); sb.Append(FL33BM.BoxSize); sb.Append("</td>");
                sb.Append("<td>"); sb.Append(FL33BM.Quantity); sb.Append("</td>");
                sb.Append("<td>"); sb.Append(FL33BM.TotalCase); sb.Append("</td>");
                sb.Append("<td>"); sb.Append(FL33BM.TotalBottle); sb.Append("</td>");
                sb.Append("<td>"); sb.Append(FL33BM.TotalBL); sb.Append("</td>");
                sb.Append("<td>"); sb.Append(FL33BM.RateofPermit); sb.Append("</td>");
                sb.Append("<td>"); sb.Append(FL33BM.DutyCalculated); sb.Append("</td>");
                sb.Append("</tr>");

            }
            sb.Append("</table></div>");
            return sb.ToString();
        }
        public ActionResult FormFL33List(string hfFormId, HttpPostedFileBase fileChallan, string txtTotalFees, string txtBankName, string txtTransactionDate, string txtTransactionNo)
        {
            FL2DChallan ChFL2D = new FL2DChallan();
            try
            {

                ChFL2D.BankName = txtBankName.Trim();
                ChFL2D.ChallanId = -1;
                ChFL2D.FL33Ids = hfFormId.Trim();
                ChFL2D.TransactionDate = CommonBL.Setdate(txtTransactionDate.Trim());
                ChFL2D.TotalFees = decimal.Parse(txtTotalFees.Trim());
                ChFL2D.TransactionNo = txtTransactionNo;
                Byte[] img = null;
                if (fileChallan != null && fileChallan.ContentLength > 0)
                {   /*****IMG-DB-CODE******/
                    int FileSize = fileChallan.ContentLength;
                    string[] extSplit = fileChallan.FileName.Split('.');
                    string ext = extSplit[extSplit.Length - 1];
                    img = new Byte[FileSize];
                    fileChallan.InputStream.Read(img, 0, FileSize);
                    ChFL2D.ChallanPhoto = img;
                    ChFL2D.FileExt = ext;
                }
            }
            catch { }
            string str = new CommonDA().InsertUpdateChallanFL2D(ChFL2D);
            return RedirectToAction("FormFL33List");
        }
        public string PreviewChallan(string ChallanId)
        {
            StringBuilder sb = new StringBuilder();
            Challan Ch = new CommonBL().GetChallan(int.Parse(ChallanId));
            string base64PDF = System.Convert.ToBase64String(Ch.ChallanPhoto, 0, Ch.ChallanPhoto.Length);
            if (Ch.FileExt.Trim().Contains("pdf"))
            {
                sb.Append("<embed src = 'data:application/pdf;base64, );");
                sb.Append(base64PDF);
                sb.Append(" type = 'application /pdf' width = '100%' height = '500' />");
                //sb.Append("window.open('data:application/pdf;base64,'");
                //sb.Append(base64PDF);
                //sb.Append(")");
            }
            else
            {
                sb.Append("<img src = '" + "data:image/");
                sb.Append(Ch.FileExt);
                sb.Append(";base64,");
                sb.Append(base64PDF);
                sb.Append("' style = 'width:75px ,height:65px' /> ");
            }
            return sb.ToString();
        }
        public ActionResult PreviewChallanFormFL2D()
        {
            return View();
        }
    }
}