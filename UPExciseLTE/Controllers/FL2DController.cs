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
    [SessionExpireFilterAttribute]
    [NoCache]
    [ChkAuthorization]
    [HandleError(View = "Error")]
    //[HandleError(ExceptionType = typeof(DbUpdateException), View = "Error")]
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
            DataSet ds = new CommonDA().GetUnitDetails(-1, "", "", -1, -1,-1, UserSession.LoggedInUserId);
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
                FL2D.UnderBondYesNo = FLBM.UnderBondYesNo;
                FL2D.BondExecutedYesNo = FLBM.BondExecutedYesNo;
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
        public string FL33BrandMapp(string BrandId, string TotalCase, string RateofPermit,string WhetherUnderBond, string WhetherBondExecuted)
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
            FL33.UnderBondYesNo = WhetherUnderBond;
            FL33.BondExecutedYesNo = WhetherBondExecuted;
            lstFL33BrandMapp.Add(FL33);
            Session["lstFL33BrandMapp"] = lstFL33BrandMapp;
            StringBuilder sb = new StringBuilder();

            sb.Append("<div class='row'><table class='table table-striped table-bordered table-hover'><tr><th>Srno</th><th>Brand Name</th><th>Box Size</th><th>Quantity (In Bottle, In ml.)</th><th>Total Case</th><th>Total Bottle</th><th>Total BL</th><th>Import Pass fee (per Ltr.)</th><th>Duty Calculated</th><th>Whether Under Bond</th><th>Whether Bond Executed</th></tr>");
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
                sb.Append("<td>"); sb.Append(FL33BM.UnderBondYesNo); sb.Append("</td>");
                sb.Append("<td>"); sb.Append(FL33BM.BondExecutedYesNo); sb.Append("</td>");
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
        [HttpGet]
        public ActionResult GatePassForFL2D()
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
        public ActionResult GatePassForFL2D(GatePassDetails GP)
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
        #region FL36GatePass
        [HttpGet]
        public ActionResult FL36GatePass()
        {
            try
            {
                FL36GatePassDetails GP = new FL36GatePassDetails();
                DataSet ds = new CommonDA().GetUnitDetails(-1, "", "", -1, -1, -1, UserSession.LoggedInUserId);
                GP = new CommonBL().FL36GetGatePassDetails(-1, CommonBL.Setdate("01/01/1900"), CommonBL.Setdate("31/12/3999"), 8, "P", "P", ds.Tables[0].Rows[0]["UnitLicenseno"].ToString().Trim(), "", ds.Tables[0].Rows[0]["UnitLicenseType"].ToString().Trim(), "");
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
            catch (Exception ex)
            {
                return Content(ex.Message);
            }            
        }
        [HttpPost]
        public ActionResult FL36GatePass(FL36GatePassDetails GP)
        {
            try
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
                string str = new CommonDA().FL36InsertUpdateGatePassDetails(GP);
                TempData["Message"] = str;
                return RedirectToAction("GatePass");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }      
        }
        #endregion
        #region WithoutBondChallan
        public ActionResult WithoutBondChallan()
        {
            return View();
        }
        #endregion
        #region BlankChallan
        public ActionResult BlankChallan()
        {
            return View();
        }

        #endregion
    }
}