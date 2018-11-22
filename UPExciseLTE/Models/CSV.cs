using System.Collections.Generic;
using System.Web;
using System.Text;
using System.IO;
using System.Data;
using System;
using System.Data.OleDb;

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
            catch(Exception) {
                dataTable = null;
            }
            return dataTable;
        }
    }
}