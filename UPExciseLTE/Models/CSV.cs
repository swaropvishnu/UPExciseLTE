using System.Collections.Generic;
using System.Web;
using System.Text;
using System.IO;
using System.Data;
using System;
using System.Data.OleDb;
using UPExciseLTE.DAL;

namespace UPExciseLTE.Models
{
    public class CSV
    {
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
        public static string ValidateCSV(int UploadValue, int PlanId, int BrandId, HttpPostedFileBase file)
        {
            StringBuilder sb = new StringBuilder();
            DataSet ds = new DataSet();
            try
            {
                DataTable dt = GetCSVToDt(file);
                ds = new CommonDA().ValidateCSV(UploadValue, PlanId, BrandId, dt);
                if (ds != null)
                {
                    sb.Append("<div id = 'myModal' class='modal fade' role='dialog'><div class='modal-dialog'><div class='modal-content'><div class='portlet box green'><div class='portlet-title'><div class='caption'><div>Validation Summary</div></div><button type = 'button' class='close' data-dismiss='modal' style='margin-top:15px'>&times;</button></div><div class='portlet-body form' style='background-color:#f5f5f5'><div><div class='modal-body'><table class='table table-striped table-bordered table-hover'><tr style = 'background-color:#cdf7cd' ><td> Valid Case Code</td><td>");
                    sb.Append(ds.Tables[0].Rows.Count);
                    sb.Append("</td></tr><tr onclick = 'ShowPopup();' style= 'background-color: #ffd6d6;' ><td> Invalid Case Code</td><td>");
                    sb.Append(ds.Tables[1].Rows.Count);
                    sb.Append("</td></tr><tr style = 'background-color:#f3f3ba' ><td> Total Case Code</td><td>");
                    sb.Append(ds.Tables[0].Rows.Count + ds.Tables[1].Rows.Count);
                    sb.Append("</td></tr></table></div><center>");
                    if (ds.Tables.Count>2)
                    {
                        if (ds.Tables[2].Rows.Count > 0)
                        {
                            sb.Append("<b style='color:red'>");
                             sb.Append(ds.Tables[2].Rows[0][0].ToString());
                            sb.Append("</b></br><button type = 'button' class='btn danger' style='background-color:crimson; color:white' id='btnFinal' data-dismiss='modal'>Retry</button>");

                        }
                        else
                        {
                            sb.Append("<button type = 'button' class='btn default' style='background-color: #659be0;color:white;border-color:#8E44AD' id='btnUploadCSV' onclick='UploadCSV();' data-dismiss='modal'>Upload CSV</button><button type = 'button' class='btn danger' style='background-color:crimson; color:white' id='btnFinal' data-dismiss='modal'>Retry</button>");
                        }
                    }
                    else
                    {
                        sb.Append("<button type = 'button' class='btn default' style='background-color: #659be0;color:white;border-color:#8E44AD' id='btnUploadCSV' onclick='UploadCSV();' data-dismiss='modal'>Upload CSV</button><button type = 'button' class='btn danger' style='background-color:crimson; color:white' id='btnFinal' data-dismiss='modal'>Retry</button>");
                    }

                    sb.Append("</center></div></div></div></div></div></div>");
                    sb.Append("<div id = 'myModal2' class='modal fade' role='dialog'><div class='modal-dialog'><div class='modal-content'><div class='portlet box green'><div class='portlet-title'><div class='caption'><div>Invalid Case Code List</div></div><button type = 'button' class='close' data-dismiss='modal' style='margin-top:15px'>&times;</button></div><div class='portlet-body form'><div><div class='modal-body'><table class='table table-striped table-bordered table-hover'><tr><th>Case Code</th><th>Status</th></tr>");
                    foreach (DataRow dr in ds.Tables[1].Rows)
                    {
                        sb.Append("<tr style = 'background-color:#ffd6d6' ><td> ");
                        sb.Append(dr["CaseBarCode"]);
                        sb.Append("</td><td> Invalid </td></tr>");
                    }
                    sb.Append("</table></div><center><button type= 'button' class='btn danger' style='background-color:crimson; color:white' data-dismiss='modal'>Close</button></center></div></div></div></div></div></div>");
                    HttpContext.Current.Session["CaseCode"] = ds.Tables[0];
                }
            }
            catch (Exception)
            { sb = null; }
            return sb.ToString();
        }
        public static string ValidateCSVCL(int UploadValue, int PlanId, int BrandId, HttpPostedFileBase file)
        {
            StringBuilder sb = new StringBuilder();
            DataSet ds = new DataSet();
            try
            {
                DataTable dt = GetCSVToDt(file);
                ds = new CommonDA().ValidateCSVCL(UploadValue, PlanId, BrandId, dt);
                if (ds != null)
                {
                    sb.Append("<div id = 'myModal' class='modal fade' role='dialog'><div class='modal-dialog'><div class='modal-content'><div class='portlet box green'><div class='portlet-title'><div class='caption'><div>Validation Summary</div></div><button type = 'button' class='close' data-dismiss='modal' style='margin-top:15px'>&times;</button></div><div class='portlet-body form' style='background-color:#f5f5f5'><div><div class='modal-body'><table class='table table-striped table-bordered table-hover'><tr style = 'background-color:#cdf7cd' ><td> Valid Case Code</td><td>");
                    sb.Append(ds.Tables[0].Rows.Count);
                    sb.Append("</td></tr><tr onclick = 'ShowPopup();' style= 'background-color: #ffd6d6;' ><td> Invalid Case Code</td><td>");
                    sb.Append(ds.Tables[1].Rows.Count);
                    sb.Append("</td></tr><tr style = 'background-color:#f3f3ba' ><td> Total Case Code</td><td>");
                    sb.Append(ds.Tables[0].Rows.Count + ds.Tables[1].Rows.Count);
                    sb.Append("</td></tr></table></div><center><button type = 'button' class='btn default' style='background-color: #659be0;color:white;border-color:#8E44AD' id='btnUploadCSV' onclick='UploadCSV();' data-dismiss='modal'>Upload CSV</button><button type = 'button' class='btn danger' style='background-color:crimson; color:white' id='btnFinal' data-dismiss='modal'>Retry</button></center></div></div></div></div></div></div>");
                    sb.Append("<div id = 'myModal2' class='modal fade' role='dialog'><div class='modal-dialog'><div class='modal-content'><div class='portlet box green'><div class='portlet-title'><div class='caption'><div>Invalid Case Code List</div></div><button type = 'button' class='close' data-dismiss='modal' style='margin-top:15px'>&times;</button></div><div class='portlet-body form'><div><div class='modal-body'><table class='table table-striped table-bordered table-hover'><tr><th>Case Code</th><th>Status</th></tr>");
                    foreach (DataRow dr in ds.Tables[1].Rows)
                    {
                        sb.Append("<tr style = 'background-color:#ffd6d6' ><td> ");
                        sb.Append(dr["CaseBarCode"]);
                        sb.Append("</td><td> Invalid </td></tr>");
                    }
                    sb.Append("</table></div><center><button type= 'button' class='btn danger' style='background-color:crimson; color:white' data-dismiss='modal'>Close</button></center></div></div></div></div></div></div>");
                    HttpContext.Current.Session["CaseCode"] = ds.Tables[0];
                }
            }
            catch (Exception)
            { sb = null; }
            return sb.ToString();
        }
    }
}