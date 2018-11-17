using System.Collections.Generic;
using System.Web;
using System.Text;

namespace UPExciseLTE.Models
{
    public class UploadCSV
    {
        public StringBuilder InsertUploadManufProdQuery = new StringBuilder();
        public StringBuilder ListQRCode = new StringBuilder();
        public int CaseCount = 0,QRCount=0;
        public void GetCSVDetails(HttpPostedFileBase file, string UploadedValue)
        {
            if (file.ContentLength > 0)
            {
                string extension = System.IO.Path.GetExtension(file.FileName).ToLower();
                string path1 = string.Format("{0}/{1}", HttpContext.Current.Server.MapPath("~/Content/Uploads"), file.FileName);
                if (extension == ".csv")
                {
                    file.SaveAs(path1);
                    var csv = new List<string[]>();
                    var lines = System.IO.File.ReadAllLines(path1);
                    StringBuilder sb = new StringBuilder();
                    StringBuilder QRCodeList = new StringBuilder();
                    List<string> CaseCodelst = new List<string>();
                    string Barcode = "";
                    string[] Split = new string[4];
                    int count = 0;
                    foreach (string line in lines)
                    {
                        if (count >= 1)
                        {
                            sb.Append("INSERT INTO[");
                            sb.Append(UserSession.PushName);
                            sb.Append("].[dbo].tbl_UploadManufProduction VALUES(");
                            sb.Append(line);
                            sb.Append(",1) ");
                            Split = line.Split(',');
                            Barcode = Split[3];
                            bool alreadyExist = CaseCodelst.Contains(Barcode);
                            if (!alreadyExist)
                            {
                                CaseCodelst.Add(Barcode);
                            }
                            if (QRCodeList.Length > 0)
                            {
                                QRCodeList.Append(",");
                            }
                            QRCodeList.Append(Barcode);
                        }
                        count++;
                    }
                    InsertUploadManufProdQuery = sb;
                    CaseCount = CaseCodelst.Count;
                    QRCount = count;
                    ListQRCode = QRCodeList;
                }
            }
        }
    }
}