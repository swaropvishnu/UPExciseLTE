using System.Collections.Generic;
using System.Web;
using System.Text;
using System.IO;

namespace UPExciseLTE.Models
{
    public class UploadCSV
    {
        public StringBuilder InsertUploadManufProdQuery = new StringBuilder();
        public StringBuilder ListQRCode = new StringBuilder();
        public int CaseCount = 0,QRCount=0;
        public void GetCSVDetails(HttpPostedFileBase file, string UploadedValue)
        {
            StreamReader csvreader = new StreamReader(file.InputStream);

            //string path1 = string.Format("{0}/{1}", System.Web.HttpContext.Current.Server.MapPath("~/Content/Uploads"), file.FileName);
            //    if (!Directory.Exists(path1))
            //    {
            //        Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath("~/Content/Uploads"));
            //    }
            if (file.ContentLength > 0)
            {
                string extension = System.IO.Path.GetExtension(file.FileName).ToLower();
                if (extension == ".csv")
                {
                    /*while (!csvreader.EndOfStream)
                    {
                        var line = csvreader.ReadLine();
                        var values = line.Split(';');
                    }*/
                   
                    StringBuilder sb = new StringBuilder();
                    StringBuilder BarcodeList = new StringBuilder();
                    List<string> CaseCodelst = new List<string>();
                    string Barcode = "";
                    string[] Split = new string[4];
                    int count = 0;
                    string line = "";
                    while (!csvreader.EndOfStream)
                    {
                        line = csvreader.ReadLine();
                        if (count >= 1)
                        {
                            sb.Append("INSERT INTO[");
                            sb.Append(UserSession.PushName);
                            if (UploadedValue=="1")
                            {
                                sb.Append("].[dbo].tbl_UploadManufProduction VALUES(");
                            }
                            else if (UploadedValue=="2")
                            {
                                sb.Append("].[dbo].tbl_UploadManufGatePass VALUES(");

                            }
                            sb.Append(line);
                            sb.Append(",");
                            sb.Append(UploadedValue);
                            sb.Append(") ");
                            Split = line.Split(',');
                            Barcode = Split[3];
                            bool alreadyExist = CaseCodelst.Contains(Barcode);
                            if (!alreadyExist)
                            {
                                CaseCodelst.Add(Barcode);
                                if (BarcodeList.Length > 0)
                                {
                                    BarcodeList.Append(",");
                                }
                                BarcodeList.Append(Barcode);
                            }
                        }
                        count++;
                    }
                    InsertUploadManufProdQuery = sb;
                    CaseCount = CaseCodelst.Count;
                    QRCount = count;
                    ListQRCode = BarcodeList;
                }
            }
        }
    }
}