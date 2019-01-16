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
            DataSet ds = new CommonDA().GetUnitDetails(-1, "", "", -1, -1,-1, UserSession.LoggedInUserId);
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
            string status = "F";
            if (Request.QueryString["V"]!=null)
            {
                if (Request.QueryString["V"].ToString().Trim()!=String.Empty)
                {
                    status = Request.QueryString["V"].ToString().Trim();
                }
            }
            return View(new CommonBL().GetFormFL21List(-1, status));
        }
        public string FinalFormFL21(string FL21ID, string status, string reason,string FromDate,string ToDate)
        {
            string str = "";
            try
            {
                
                FormFL21 FL21 = new FormFL21();
                FL21.FL21ID = int.Parse(FL21ID);
                FL21.SPType = 3;
                FL21.FL21Status = status;
                FL21.Reason = reason;
                if (status=="A")
                {
                    FL21.FromPermitDate = CommonBL.Setdate(FromDate);
                    FL21.ToPermitDate = CommonBL.Setdate(ToDate);
                }
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

            sb.Append("<div class='row'><table class='table table-striped table-bordered table-hover'><tr><th>Srno</th><th>Brand Name</th><th>Box Size</th><th>Capacity of Bottle/ Can (in ml.)</th><th>Total Case</th><th>Total Bottle</th><th>Total BL</th><th>Import Pass fee (per Ltr.)</th><th>Duty Calculated</th></tr>");
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
        public ActionResult FormFL21List(string hfFormId, HttpPostedFileBase fileChallan, string txtTotalFees,string txtBankName,string txtTransactionDate,string txtTransactionNo)
        {
            Challan Ch = new Challan();
            try
            {
               
                Ch.BankName = txtBankName.Trim();
                Ch.ChallanId = -1;
                Ch.FL21Ids = hfFormId.Trim();
                Ch.TransactionDate = CommonBL.Setdate(txtTransactionDate.Trim());
                Ch.TotalFees = decimal.Parse(txtTotalFees.Trim());
                Ch.TransactionNo = txtTransactionNo;
                Byte[] img = null;
                if (fileChallan != null && fileChallan.ContentLength > 0)
                {   /*****IMG-DB-CODE******/
                    int FileSize = fileChallan.ContentLength;
                    string[] extSplit = fileChallan.FileName.Split('.');
                    string ext = extSplit[extSplit.Length - 1];
                    img = new Byte[FileSize];
                    fileChallan.InputStream.Read(img, 0, FileSize);
                    Ch.ChallanPhoto = img;
                    Ch.FileExt = ext;
                }
            }
            catch { }
            string str = new CommonDA().InsertUpdateChallan(Ch); 
            return RedirectToAction("FormFL21List");
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
            else {
                sb.Append("<img src = '" + "data:image/");
                sb.Append(Ch.FileExt);
                sb.Append(";base64,");
                sb.Append(base64PDF);
                sb.Append("' style = 'width:75px ,height:65px' /> ");
            }
            return sb.ToString();
        }
        public ActionResult PreviewChallanForm()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GatePassForBWFL()
        {
            GatePassDetails GP = new GatePassDetails();
            GP = new CommonBL().GetGatePassDetailsG(-1, CommonBL.Setdate("01/01/1900"), CommonBL.Setdate("31/12/3999"), 2, "P", "P","","","","");
            ViewBag.Msg = TempData["Message"];
            DataSet ds = new CommonDA().GetUnitDetails(-1, "", "", -1, -1, -1, UserSession.LoggedInUserId);

            List<SelectListItem> FromLicenseTypes = new List<SelectListItem>();
            List<SelectListItem> ToLicenseTypes = new List<SelectListItem>();
            SelectListItem SLI = new SelectListItem();
            SLI.Text = "--Select--";
            SLI.Value = "-1";
            FromLicenseTypes.Add(SLI);
            SLI = new SelectListItem();
            SLI.Text = "FL-3";
            SLI.Value = "FL-3";
            FromLicenseTypes.Add(SLI);

            SLI = new SelectListItem();
            SLI.Text = "FL-3A";
            SLI.Value = "FL-3A";
            FromLicenseTypes.Add(SLI);

            SLI = new SelectListItem();
            SLI.Text = "--Select--";
            SLI.Value = "-1";
            ToLicenseTypes.Add(SLI);
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
                ViewBag.FL1Licence = CommonBL.fillFL1Licence(int.Parse(ds.Tables[0].Rows[0]["UnitId"].ToString().Trim()));

                if (GP.FromConsignorName.Trim() == string.Empty)
                {
                    GP.FromLicenceNo = ds.Tables[0].Rows[0]["UnitLicenseno"].ToString().Trim();
                    GP.FromConsignorName = ds.Tables[0].Rows[0]["UnitName"].ToString().Trim();
                    GP.ConsignorAddress = ds.Tables[0].Rows[0]["UnitAddress"].ToString().Trim();
                    //FromLicenseTypes.Find(x => x.Value == ds.Tables[0].Rows[0]["UnitLicenseType"].ToString().Trim()).Selected = true;
                    if (ds.Tables[0].Rows[0]["UnitLicenseType"].ToString().Trim() == "FL-3")
                    {
                        GP.FromLicenseType = "FL-3";
                        GP.ToLicenseType = "FL-1";
                        ToLicenseTypes.Find(x => x.Value == "FL-1").Selected = true;

                        SLI = new SelectListItem();
                        SLI.Text = "FL-3A";
                        SLI.Value = "FL-3A";
                        FromLicenseTypes.Remove(SLI);

                        SLI = new SelectListItem();
                        SLI.Text = "FL-1A";
                        SLI.Value = "FL-1A";
                        ToLicenseTypes.Remove(SLI);
                    }
                    else
                    {
                        GP.FromLicenseType = "FL-3A";
                        GP.ToLicenseType = "FL-1A";
                        ToLicenseTypes.Find(x => x.Value == "FL-1A").Selected = true;

                        SLI = new SelectListItem();
                        SLI.Text = "FL-3";
                        SLI.Value = "FL-3";
                        FromLicenseTypes.Remove(SLI);

                        SLI = new SelectListItem();
                        SLI.Text = "FL-1";
                        SLI.Value = "FL-1";
                        ToLicenseTypes.Remove(SLI);
                    }
                }
            }
            else
            {
                FromLicenseTypes.Find(x => x.Value == GP.FromLicenseType).Selected = true;
                if (GP.ToLicenseType.Trim() == "F.L. 1" || GP.ToLicenseType.Trim() == "Export Outside UP")
                {
                    ToLicenseTypes.Find(x => x.Value == GP.ToLicenseType).Selected = true;
                }
            }
            ViewBag.Districts = CommonBL.fillDistict("N");


            //ViewBag.FromLicenseTypes = CommonBL.fillLicenseTypes("S", "L1F");
            ViewBag.FromLicenseTypes = FromLicenseTypes;
            ViewBag.ToLicenseTypes = ToLicenseTypes;
            return View(GP);
        }
        [HttpPost]
        public ActionResult GatePassForBWFL(GatePassDetails GP)
        {
            if (GP.Receiver == null)
            {
                GP.Receiver = "";
            }
            GP.GatePassSourceId = long.Parse(UserSession.LoggedInUserLevelId);
            GP.UploadValue = 2;
            GP.FromDate = CommonBL.Setdate(GP.FromDate1.Trim());
            GP.ToDate = CommonBL.Setdate(GP.ToDate1.Trim());
            string str = new CommonDA().InsertUpdateGatePassDetails(GP);
            TempData["Message"] = str;
            return RedirectToAction("GatePass");
        }
        #region FL2BGatePass
        [HttpGet]
        public ActionResult FL2BGatePass()
        {
            FL2BGatePassDetails objGatePass = new FL2BGatePassDetails();
            DataSet ds = new CommonDA().FL2BGetUnitDetails(-1, "", "", -1, -1, -1, UserSession.LoggedInUserId);
            objGatePass = new CommonBL().FL2BGetGatePassDetails(-1, CommonBL.Setdate("01/01/1900"), CommonBL.Setdate("31/12/3999"), 9, "P", "P", ds.Tables[0].Rows[0]["UnitLicenseno"].ToString().Trim(), "", ds.Tables[0].Rows[0]["UnitLicenseType"].ToString().Trim(), "");
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
                FL1Licence = CommonBL.FL2BfillFL1Licence(int.Parse(ds.Tables[0].Rows[0]["UnitId"].ToString().Trim()));
                if (objGatePass.FromLicenseType.Trim() == string.Empty)
                {
                    if (ds.Tables[0].Rows[0]["UnitLicenseType"].ToString().Trim() == "FL-1")
                    {
                        objGatePass.FromLicenseType = "FL-1";
                    }
                    else if (ds.Tables[0].Rows[0]["UnitLicenseType"].ToString().Trim() == "FL-1A")
                    {
                        objGatePass.FromLicenseType = "FL-1A";
                    }
                    objGatePass.FromLicenceNo = ds.Tables[0].Rows[0]["UnitLicenseno"].ToString().Trim();
                    objGatePass.FromConsignorName = ds.Tables[0].Rows[0]["UnitName"].ToString().Trim();
                    objGatePass.ConsignorAddress = ds.Tables[0].Rows[0]["UnitAddress"].ToString().Trim();
                }
            }
            if (!string.IsNullOrEmpty(objGatePass.ToLicenseType.Trim()) &&
                ToLicenseTypes.Find(x => x.Text.Trim() == objGatePass.ToLicenseType.Trim().Trim()) != null)
            {
                ToLicenseTypes.Find(x => x.Value.Trim() == objGatePass.ToLicenseType.Trim()).Selected = true;
            }
            if (!string.IsNullOrEmpty(objGatePass.ToLicenceNo.Trim()) &&
                FL1Licence.Find(x => x.Text.Trim() == objGatePass.ToLicenceNo.Trim()) != null)
            {
                FL1Licence.Find(x => x.Text.Trim() == objGatePass.ToLicenceNo.Trim()).Selected = true;
            }
            ViewBag.FL1Licence = FL1Licence;
            ViewBag.Districts = CommonBL.fillDistict("N");
            ViewBag.ToLicenseTypes = ToLicenseTypes;
            return View(objGatePass);
        }
        [HttpPost]
        public ActionResult FL2BGatePass(FL2BGatePassDetails GP)
        {
            if (GP.Receiver == null)
            {
                GP.Receiver = "";
            }
            if (GP.ImportPermitNo == null)
            {
                GP.ImportPermitNo = "";
            }
            GP.GatepassLicenseNo = "FL-2B";
            GP.GatePassSourceId = long.Parse(UserSession.LoggedInUserLevelId);
            GP.UploadValue = 4;
            GP.FromDate = CommonBL.Setdate(GP.FromDate1.Trim());
            GP.ToDate = CommonBL.Setdate(GP.ToDate1.Trim());
            string str = new CommonDA().FL2BInsertUpdateGatePassDetails(GP);
            TempData["Message"] = str;
            return RedirectToAction("FL2BGatePass");
        }
        #endregion

    }
}