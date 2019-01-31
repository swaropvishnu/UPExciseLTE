using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using UPExciseLTE.DAL;
using UPExciseLTE.Filters;

namespace UPExciseLTE.Controllers
{
    [SessionExpireFilterAttribute]
    //[NoCache]
    [ChkAuthorization]
    [HandleError(View = "Error")]
    public class DamageUploadCSVController : Controller
    {
        #region Default
        public static DataTable GetCSVToDt(HttpPostedFileBase file)
        {
            DataTable dataTable = new DataTable();
            try
            {
                StreamReader csvreader = new StreamReader(file.InputStream);
                dataTable.Clear();
                dataTable.Columns.Add("CaseCode");
                string line = "";
                while (!csvreader.EndOfStream)
                {
                    line = csvreader.ReadLine();
                    dataTable.Rows.Add(line);
                }
            }
            catch (Exception)
            {
                dataTable = null;
            }
            return dataTable;
        }
        private string ValidateCSV(int UploadValue, int GatePassId, int BrandId, HttpPostedFileBase file)
        {
            StringBuilder sb = new StringBuilder();
            DataSet ds = new DataSet();
            try
            {
                DataTable dt = GetCSVToDt(file);
                ds = new CommonDA().ValidateDamageCSV(UploadValue, GatePassId, BrandId, dt);
                if (ds != null)
                {
                    sb.Append("<div id = 'myModal' class='modal fade' role='dialog'><div class='modal-dialog'><div class='modal-content'><div class='portlet box green'><div class='portlet-title'><div class='caption'><div>Validation Summary</div></div><button type = 'button' class='close' data-dismiss='modal' style='margin-top:15px'>&times;</button></div><div class='portlet-body form' style='background-color:#f5f5f5'><div><div class='modal-body'><table class='table table-striped table-bordered table-hover'><tr style = 'background-color:#cdf7cd' ><td> Valid QR Code</td><td>");
                    sb.Append(ds.Tables[0].Rows.Count);
                    sb.Append("</td></tr><tr onclick = 'ShowPopupError();' style= 'background-color: #ffd6d6;' ><td> Invalid QR Code</td><td>");
                    sb.Append(ds.Tables[1].Rows.Count);
                    sb.Append("</td></tr><tr style = 'background-color:#f3f3ba' ><td> Total QR Code</td><td>");
                    sb.Append(ds.Tables[0].Rows.Count + ds.Tables[1].Rows.Count);
                    sb.Append("</td></tr></table></div><center>");
                    if (ds.Tables.Count > 2)
                    {
                        if (ds.Tables[2].Rows.Count > 0)
                        {
                            sb.Append("<b style='color:red'>");
                            sb.Append(ds.Tables[2].Rows[0][0].ToString());
                            sb.Append("</b></br><button type = 'button' class='btn danger' style='background-color:crimson; color:white' id='btnFinal' data-dismiss='modal'>Retry</button>");

                        }
                        else
                        {
                            sb.Append("<button type = 'button' class='btn default' style='background-color: #659be0;color:white;border-color:#8E44AD' id='btnUploadCSV' onclick='ReceiveGatePass();' data-dismiss='modal'>Upload CSV</button><button type = 'button' class='btn danger' style='background-color:crimson; color:white' id='btnFinal' data-dismiss='modal'>Retry</button>");
                        }
                    }
                    else
                    {
                        sb.Append("<button type = 'button' class='btn default' style='background-color: #659be0;color:white;border-color:#8E44AD' id='btnUploadCSV' onclick='ReceiveGatePass();' data-dismiss='modal'>Receive Gate Pass</button><button type = 'button' class='btn danger' style='background-color:crimson; color:white' id='btnFinal' data-dismiss='modal'>Retry</button>");
                    }

                    sb.Append("</center></div></div></div></div></div></div>");
                    sb.Append("<div id = 'myModal2' class='modal fade' role='dialog'><div class='modal-dialog'><div class='modal-content'><div class='portlet box green'><div class='portlet-title'><div class='caption'><div>Invalid QR Code List</div></div><button type = 'button' class='close' data-dismiss='modal' style='margin-top:15px'>&times;</button></div><div class='portlet-body form'><div><div class='modal-body'><table class='table table-striped table-bordered table-hover'><tr><th>QR Code</th><th>Status</th></tr>");
                    foreach (DataRow dr in ds.Tables[1].Rows)
                    {
                        sb.Append("<tr style = 'background-color:#ffd6d6' ><td> ");
                        sb.Append(dr["CaseBarCode"]);
                        sb.Append("</td><td> Invalid </td></tr>");
                    }
                    sb.Append("</table></div><center><button type= 'button' class='btn danger' style='background-color:crimson; color:white' data-dismiss='modal'>Close</button></center></div></div></div></div></div></div>");
                    Session["CaseCode"] = ds.Tables[0];
                }
            }
            catch (Exception)
            { sb = null; }
            return sb.ToString();
        }
        #endregion
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReceiveGatePassWH()
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
                        str = ValidateCSV(1,  int.Parse(files.Keys[0].Replace("file", "")),-1, file);
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
        public ActionResult ReceiveGatePassDistrictWH()
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
                        str = ValidateCSV(2, int.Parse(files.Keys[0].Replace("file", "")), -1, file);
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
        public ActionResult ReceiveGatePassRetailer()
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
                        str = ValidateCSV(3, int.Parse(files.Keys[0].Replace("file", "")), -1, file);
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
    }
}