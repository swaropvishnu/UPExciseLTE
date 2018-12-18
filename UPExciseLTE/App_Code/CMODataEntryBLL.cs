using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web;
using System.Web.Mvc;
using UPExciseLTE.Models;
//using System.Web.Mvc;

namespace UPExciseLTE
{
    public static class CMODataEntryBLL
    {
        //Get Menu Item
        public static DataTable GetDataMenu(string ProcName, string parm1, string parm2, string parm3)
        {

            DataSet ds = new DataSet();
            if (parm1.Length > 0 && parm1 != "")
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@Parm1", parm1));
                if (parm2.Length > 0 && parm2 != "")
                    parameters.Add(new SqlParameter("@Parm2", parm2));
                if (parm3.Length > 0 && parm3 != "")
                    parameters.Add(new SqlParameter("@Parm3", parm3));

                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, ProcName, parameters.ToArray());
            }
            else
            {
                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, ProcName);
            }
            return ds.Tables[0];
        }
        public static SqlDataReader GetData(string ProcName, string parm1, string parm2, string parm3)
        {

            SqlDataReader sdr ;
            if (parm1.Length > 0 && parm1 != "")
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@Parm1", parm1));
                if (parm2.Length > 0 && parm2 != "")
                    parameters.Add(new SqlParameter("@Parm2", parm2));
                if (parm3.Length > 0 && parm3 != "")
                    parameters.Add(new SqlParameter("@Parm3", parm3));                

                sdr = SqlHelper.ExecuteReader(CommonConfig.Conn(), CommandType.StoredProcedure, ProcName, parameters.ToArray());
            }
            else
            {
                sdr = SqlHelper.ExecuteReader(CommonConfig.Conn(), CommandType.StoredProcedure, ProcName);
            }
            return sdr;
        }

        //public static void bindDropDownHnGrid(string ProcName, List<SelectListItem> distNames, string parm1, string parm2, string parm3)
        //{
        //    DataSet ds = new DataSet();
        //    SqlDataReader sdr;
        //    if (parm1.Length > 0 && parm1 != "")
        //    {
        //        List<SqlParameter> parameters = new List<SqlParameter>();
        //        parameters.Add(new SqlParameter("@Parm1", parm1));
        //        if (parm2.Length > 0 && parm2 != "")
        //            parameters.Add(new SqlParameter("@Parm2", parm2));
        //        if (parm3.Length > 0 && parm3 != "")
        //            parameters.Add(new SqlParameter("@Parm3", parm3));
        //        sdr = SqlHelper.ExecuteReader(CommonConfig.Conn(), CommandType.StoredProcedure, ProcName, parameters.ToArray());
        //    }
        //    else
        //    {
        //        sdr = SqlHelper.ExecuteReader(CommonConfig.Conn(), CommandType.StoredProcedure, ProcName);
        //    }
        //    distNames.Insert(0, new SelectListItem { Text = "--चयन करे--", Value = "-1" });
        //    while (sdr.Read())
        //    {
        //        distNames.Add(new SelectListItem
        //        {

        //            Text = sdr["ValueText"].ToString(),
        //            Value = sdr["ValueId"].ToString()
        //        });
        //    }
        //    sdr.Close();
        //}
        public static DataSet GetData2(string ProcName, string parm1, string parm2, string parm3, string parm4)
        {
            DataSet ds = new DataSet();
            if (parm1.Length > 0 && parm1 != "")
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@Parm1", parm1));
                if (parm2.Length > 0 && parm2 != "")
                    parameters.Add(new SqlParameter("@Parm2", parm2));
                if (parm3.Length > 0 && parm3 != "")
                    parameters.Add(new SqlParameter("@Parm3", parm3));
                if (parm4.Length > 0 && parm4 != "")
                    parameters.Add(new SqlParameter("@Parm4", parm4));
                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, ProcName, parameters.ToArray());
            }
            else
            {
                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, ProcName);
            }
            return ds;
        }
        public static void bindDropDownEn(string ProcName, DropDownList ddl, string parm1, string parm2, string parm3)
        {

            DataSet ds = new DataSet();
            if (parm1.Length > 0 && parm1 != "")
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@Parm1", parm1));
                if (parm2.Length > 0 && parm2 != "")
                    parameters.Add(new SqlParameter("@Parm2", parm2));
                if (parm3.Length > 0 && parm3 != "")
                    parameters.Add(new SqlParameter("@Parm3", parm3));
                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, ProcName, parameters.ToArray());
            }
            else
            {
                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, ProcName);
            }
            ddl.Items.Clear();

            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {

                ddl.DataSource = ds;
                ddl.DataTextField = "ValueText";
                ddl.DataValueField = "ValueId";
                ddl.DataBind();
            }
            ddl.Items.Insert(0, new ListItem("--Select One--", "0", true));

        }


//        public static int bindDropDownWithRowsCountEn(string ProcName, DropDownList ddl, string parm1, string parm2, string parm3)
//        {

//            DataSet ds = new DataSet();
//            if (parm1.Length > 0 && parm1 != "")
//            {
//                List<SqlParameter> parameters = new List<SqlParameter>();
//                parameters.Add(new SqlParameter("@Parm1", parm1));
//                if (parm2.Length > 0 && parm2 != "")
//                    parameters.Add(new SqlParameter("@Parm2", parm2));
//                if (parm3.Length > 0 && parm3 != "")
//                    parameters.Add(new SqlParameter("@Parm3", parm3));
//                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, ProcName, parameters.ToArray());
//            }
//            else
//            {
//                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, ProcName);
//            }
//            ddl.Items.Clear();
//            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
//            {
                
//                ddl.DataSource = ds;
//                ddl.DataTextField = "ValueText";
//                ddl.DataValueField = "ValueId";
//                ddl.DataBind();
//            }
//            ddl.Items.Insert(0, new ListItem("Select One", "0", true));
//            return ds.Tables[0].Rows.Count;
//        }

        public static void bindCheckbox(string ProcName, CheckBoxList ddl, string parm1, string parm2, string parm3)
        {

            DataSet ds = new DataSet();
            if (parm1.Length > 0 && parm1 != "")
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@Parm1", parm1));
                if (parm2.Length > 0 && parm2 != "")
                    parameters.Add(new SqlParameter("@Parm2", parm2));
                if (parm3.Length > 0 && parm3 != "")
                    parameters.Add(new SqlParameter("@Parm3", parm3));
                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, ProcName, parameters.ToArray());
            }
            else
            {
                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, ProcName);
            }
            ddl.Items.Clear();
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {

                ddl.DataSource = ds;
                ddl.DataTextField = "ValueText";
                ddl.DataValueField = "ValueId";
                ddl.DataBind();
            }


        }
        


//        public static void bindListBoxHn(string ProcName, ListBox ddl, string parm1, string parm2, string parm3)
//        {

//            DataSet ds = new DataSet();
//            if (parm1.Length > 0 && parm1 != "")
//            {
//                List<SqlParameter> parameters = new List<SqlParameter>();
//                parameters.Add(new SqlParameter("@Parm1", parm1));
//                if (parm2.Length > 0 && parm2 != "")
//                    parameters.Add(new SqlParameter("@Parm2", parm2));
//                if (parm3.Length > 0 && parm3 != "")
//                    parameters.Add(new SqlParameter("@Parm3", parm3));
//                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, ProcName, parameters.ToArray());
//            }
//            else
//            {
//                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, ProcName);
//            }
//            ddl.Items.Clear();
//            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
//            {
               
//                ddl.DataSource = ds;
//                ddl.DataTextField = "ValueText";
//                ddl.DataValueField = "ValueId";
//                ddl.DataBind();
//            }
//            ddl.Items.Insert(0, new ListItem("-- समस्त --", "0", true));
//        }

        public static void bindRadioBoxHn(string ProcName, RadioButtonList ddl, string parm1, string parm2, string parm3)
        {

            DataSet ds = new DataSet();
            if (parm1.Length > 0 && parm1 != "")
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@Parm1", parm1));
                if (parm2.Length > 0 && parm2 != "")
                    parameters.Add(new SqlParameter("@Parm2", parm2));
                if (parm3.Length > 0 && parm3 != "")
                    parameters.Add(new SqlParameter("@Parm3", parm3));
                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, ProcName, parameters.ToArray());
            }
            else
            {
                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, ProcName);
            }
            ddl.Items.Clear();
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {

                ddl.DataSource = ds;
                ddl.DataTextField = "ValueText";
                ddl.DataValueField = "ValueId";
                ddl.DataBind();
            }

        }


//        public static void bindCheckBoxHn(string ProcName, CheckBoxList ddl, string parm1, string parm2, string parm3)
//        {

//            DataSet ds = new DataSet();
//            if (parm1.Length > 0 && parm1 != "")
//            {
//                List<SqlParameter> parameters = new List<SqlParameter>();
//                parameters.Add(new SqlParameter("@Parm1", parm1));
//                if (parm2.Length > 0 && parm2 != "")
//                    parameters.Add(new SqlParameter("@Parm2", parm2));
//                if (parm3.Length > 0 && parm3 != "")
//                    parameters.Add(new SqlParameter("@Parm3", parm3));
//                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, ProcName, parameters.ToArray());
//            }
//            else
//            {
//                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, ProcName);
//            }
//            ddl.Items.Clear();
//            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
//            {
               
//                ddl.DataSource = ds;
//                ddl.DataTextField = "ValueText";
//                ddl.DataValueField = "ValueId";
//                ddl.DataBind();
//            }
//            ddl.Items.Insert(0, new ListItem("-- समस्त --", "0", true));
//        }


//        public static Object Scaler(string ProcName, string parm1, string parm2, string parm3)
//        {
//            Object abc = new Object();

//            if (parm1.Length > 0 && parm1 != "")
//            {
//                List<SqlParameter> parameters = new List<SqlParameter>();
//                parameters.Add(new SqlParameter("@Parm1", parm1));
//                if (parm2.Length > 0 && parm2 != "")
//                    parameters.Add(new SqlParameter("@Parm2", parm2));
//                if (parm3.Length > 0 && parm3 != "")
//                    parameters.Add(new SqlParameter("@Parm3", parm3));
//                abc = SqlHelper.ExecuteScalar(CommonConfig.Conn(), CommandType.StoredProcedure, ProcName, parameters.ToArray());
//            }
//            else
//            {
//                abc = SqlHelper.ExecuteScalar(CommonConfig.Conn(), CommandType.StoredProcedure, ProcName);
//            }
//            return abc;

//        }

        public static void bindDropDownHn(string ProcName, DropDownList ddl, string parm1, string parm2, string parm3,string parm4)
        {

            DataSet ds = new DataSet();
            if (parm1.Length > 0 && parm1 != "")
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@Parm1", parm1));
                if (parm2.Length > 0 && parm2 != "")
                    parameters.Add(new SqlParameter("@Parm2", parm2));
                if (parm3.Length > 0 && parm3 != "")
                    parameters.Add(new SqlParameter("@Parm3", parm3));
                if (parm4.Length > 0 && parm3 != "")
                    parameters.Add(new SqlParameter("@Parm4", parm4));
                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, ProcName, parameters.ToArray());
            }
            else
            {
                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, ProcName);
            }
            ddl.Items.Clear();
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                ddl.DataSource = ds;
                ddl.DataTextField = "ValueText";
                ddl.DataValueField = "ValueId";
                ddl.DataBind();
            }
            ddl.Items.Insert(0, new ListItem("--चयन करे--", "0", true));

        }


        

        //public static void bindDropDownHn4(string ProcName, DropDownList ddl, string parm1, string parm2, string parm3, string parm4)
        //{

        //    DataSet ds = new DataSet();
        //    if (parm1.Length > 0 && parm1 != "")
        //    {
        //        List<SqlParameter> parameters = new List<SqlParameter>();
        //        parameters.Add(new SqlParameter("@Parm1", parm1));
        //        if (parm2.Length > 0 && parm2 != "")
        //            parameters.Add(new SqlParameter("@Parm2", parm2));
        //        if (parm3.Length > 0 && parm3 != "")
        //            parameters.Add(new SqlParameter("@Parm3", parm3));

        //        if (parm4.Length > 0 && parm4 != "")
        //            parameters.Add(new SqlParameter("@Parm4", parm4));

        //        ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, ProcName, parameters.ToArray());
        //    }
        //    else
        //    {
        //        ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, ProcName);
        //    }
        //    ddl.Items.Clear();
        //    if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        //    {

        //        ddl.DataSource = ds;
        //        ddl.DataTextField = "ValueText";
        //        ddl.DataValueField = "ValueId";
        //        ddl.DataBind();
        //    }
        //    ddl.Items.Insert(0, new ListItem("--कोई एक चयन करे--", "0", true));

        //}

//        public static void bindDropDownProfile(string ProcName, DropDownList ddl, string parm1, string parm2, string parm3)
//        {

//            DataSet ds = new DataSet();
//            //List<SqlParameter> parameters = new List<SqlParameter>();
//            //if (parm1.Length > 0 && parm1 != "")
//            //{

//            //    parameters.Add(new SqlParameter("@Parm1", parm1));
//            //    }
//            //    if (parm2.Length > 0 && parm2 != "")
//            //        parameters.Add(new SqlParameter("@Parm2", parm2));
//            //    if (parm3.Length > 0 && parm3 != "")
//            //        parameters.Add(new SqlParameter("@Parm3", parm3));
//            //    ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, ProcName, parameters.ToArray());

//            //else
//            //{
//            ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, ProcName);
//            //}

//            ddl.Items.Clear();
//            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
//            {

//                ddl.DataSource = ds;
//                ddl.DataTextField = "UN";
//                ddl.DataValueField = "UL";
//                ddl.DataBind();

//            }
//            ddl.Items.Insert(0, new ListItem("--कोई एक चयन करे--", "0", true));

//        }

        public static void bindDropDownHn5(string ProcName, DropDownList ddl, string parm1, string parm2, string parm3, string parm4, string parm5)
        {

            DataSet ds = new DataSet();
            if (parm1.Length > 0 && parm1 != "")
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@Parm1", parm1));
                if (parm2.Length > 0 && parm2 != "")
                    parameters.Add(new SqlParameter("@Parm2", parm2));
                if (parm3.Length > 0 && parm3 != "")
                    parameters.Add(new SqlParameter("@Parm3", parm3));


                if (parm4.Length > 0 && parm4 != "")
                    parameters.Add(new SqlParameter("@Parm4", parm4));


                if (parm5.Length > 0 && parm5 != "")
                    parameters.Add(new SqlParameter("@Parm5", parm5));

                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, ProcName, parameters.ToArray());
            }
            else
            {
                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, ProcName);
            }
            ddl.Items.Clear();
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {

                ddl.DataSource = ds;
                ddl.DataTextField = "ValueText";
                ddl.DataValueField = "ValueId";
                ddl.DataBind();
            }
            ddl.Items.Insert(0, new ListItem("--कोई एक चयन करे--", "0", true));

        }




//        public static void bindDropDownHnReport(string ProcName, DropDownList ddl, string parm1, string parm2, string parm3)
//        {

//            DataSet ds = new DataSet();
//            if (parm1.Length > 0 && parm1 != "")
//            {
//                List<SqlParameter> parameters = new List<SqlParameter>();
//                parameters.Add(new SqlParameter("@Parm1", parm1));
//                if (parm2.Length > 0 && parm2 != "")
//                    parameters.Add(new SqlParameter("@Parm2", parm2));
//                if (parm3.Length > 0 && parm3 != "")
//                    parameters.Add(new SqlParameter("@Parm3", parm3));
//                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, ProcName, parameters.ToArray());
//            }
//            else
//            {
//                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, ProcName);
//            }
//            ddl.Items.Clear();
//            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
//            {
               
//                ddl.DataSource = ds;
//                ddl.DataTextField = "ValueText";
//                ddl.DataValueField = "ValueId";
//                ddl.DataBind();
//            }
//            ddl.Items.Insert(0, new ListItem("समस्त", "0", true));

//        }

//        public static int bindDropDownWithRowsCountHn(string ProcName, DropDownList ddl, string parm1, string parm2, string parm3)
//        {

//            DataSet ds = new DataSet();
//            if (parm1.Length > 0 && parm1 != "")
//            {
//                List<SqlParameter> parameters = new List<SqlParameter>();
//                parameters.Add(new SqlParameter("@Parm1", parm1));
//                if (parm2.Length > 0 && parm2 != "")
//                    parameters.Add(new SqlParameter("@Parm2", parm2));
//                if (parm3.Length > 0 && parm3 != "")
//                    parameters.Add(new SqlParameter("@Parm3", parm3));
//                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, ProcName, parameters.ToArray());
//            }
//            else
//            {
//                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, ProcName);
//            }
//            ddl.Items.Clear();
//            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
//            {
              
//                ddl.DataSource = ds;
//                ddl.DataTextField = "ValueText";
//                ddl.DataValueField = "ValueId";
//                ddl.DataBind();
//            }
//            ddl.Items.Insert(0, new ListItem("--कोई एक चयन करे--", "0", true));
//            return ds.Tables[0].Rows.Count;
//        }


//        public static DataSet bindMemberParliyamentInfo(string ProcName, string parm1, string parm2, string parm3)
//        {

//            DataSet ds = new DataSet();
//            if (parm1.Length > 0 && parm1 != "")
//            {
//                List<SqlParameter> parameters = new List<SqlParameter>();
//                parameters.Add(new SqlParameter("@Parm1", parm1));
//                if (parm2.Length > 0 && parm2 != "")
//                    parameters.Add(new SqlParameter("@Parm2", parm2));
//                if (parm3.Length > 0 && parm3 != "")
//                    parameters.Add(new SqlParameter("@Parm3", parm3));
//                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, ProcName, parameters.ToArray());
//            }
//            else
//            {
//                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, ProcName);
//            }
//            return ds;
//        }

//        public static DataSet GetPlantCountInfo(string ProcName, string parm1, string parm2, string parm3)
//        {

//            DataSet ds = new DataSet();
//            if (parm1.Length > 0 && parm1 != "")
//            {
//                List<SqlParameter> parameters = new List<SqlParameter>();
//                parameters.Add(new SqlParameter("@Parm1", parm1));
//                if (parm2.Length > 0 && parm2 != "")
//                    parameters.Add(new SqlParameter("@Parm2", parm2));
//                if (parm3.Length > 0 && parm3 != "")
//                    parameters.Add(new SqlParameter("@Parm3", parm3));
//                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, ProcName, parameters.ToArray());
//            }
//            else
//            {
//                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, ProcName);
//            }
//            return ds;
//        }

//        public static void bindDropDownHnnf(string ProcName, DropDownList ddl, string parm1, string parm2, string parm3, string parm4)
//        {

//            DataSet ds = new DataSet();
//            if (parm1.Length > 0 && parm1 != "")
//            {
//                List<SqlParameter> parameters = new List<SqlParameter>();
//                parameters.Add(new SqlParameter("@Parm1", parm1));
//                if (parm2.Length > 0 && parm2 != "")
//                    parameters.Add(new SqlParameter("@Parm2", parm2));
//                if (parm3.Length > 0 && parm3 != "")
//                    parameters.Add(new SqlParameter("@Parm3", parm3));
//                if (parm4.Length > 0 && parm4 != "")
//                    parameters.Add(new SqlParameter("@Parm4", parm4));
//                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, ProcName, parameters.ToArray());
//            }
//            else
//            {
//                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, ProcName);
//            }
//            ddl.Items.Clear();
//            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
//            {
               
//                ddl.DataSource = ds;
//                ddl.DataTextField = "ValueText";
//                ddl.DataValueField = "ValueId";
//                ddl.DataBind();
//            }
//            ddl.Items.Insert(0, new ListItem("--कोई एक चयन करे--", "0", true));

//        }


//        public static void ReplaceValueAtGivenIndexInIDsSession(string sessionStr, int index)
//        {
//            char[] delimiters = new char[] { '#' };
//            string[] sessionsStr = HttpContext.Current.Session["Report"].ToString().Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
//            sessionsStr[index] = sessionStr;
//            string finalValue = "";
//            int i = 0;
//            foreach (string str in sessionsStr)
//            {
//                if (i != 0)
//                {
//                    finalValue = finalValue + "#" + str;
//                }
//                else
//                {
//                    finalValue = str;
//                }
//                i = i + 1;
//            }
//            HttpContext.Current.Session["Report"] = finalValue;
//        }

        internal static void InsertErrLog(string UrlPage, string ErrMsg)
        {
            try
            {
                // string Errid = objg.GetNewID_String("Err_Log", "ErrID", 8);
                string IP = HttpContext.Current.Request.UserHostAddress.ToString();
                string sql = "Insert into tbl_errorlog(IP,ErrPage,ErrLog) values(@IP,@ErrPage,@ErrLog)";
                SqlParameter[] sqlparam = {
                new SqlParameter("@IP", IP),
                new SqlParameter("@ErrPage", UrlPage),
                new SqlParameter("@ErrLog", ErrMsg)
            };
                SqlHelper.ExecuteNonQuery(CommonConfig.Conn(), CommandType.Text, sql, sqlparam);
            }
            catch (Exception exp) { }
        }

        //public static DataSet GetDashBoard(string ProcName, BO.UserBO objo, string p2, string p5, string p6, int flag)
        //{
        //    DataSet ds = new DataSet();
        //    if (objo.UserId.ToString().Length > 0 && objo.UserId.ToString() != "")
        //    {
        //        List<SqlParameter> parameters = new List<SqlParameter>();
        //        parameters.Add(new SqlParameter("@UserId", objo.UserId));
        //        if (p2.Length > 0 && p2 != "")
        //            parameters.Add(new SqlParameter("@EmpType", p2));
        //        if (p6.Length > 0 && p6 != "")
        //            parameters.Add(new SqlParameter("@Parm3", p6));
        //        ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, ProcName, parameters.ToArray());
        //    }
        //    else
        //    {
        //        ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, ProcName);
        //    }
        //    return ds;
        //}

//        public static DataSet GetDashBoardDistrictwise(string ProcName, BussinessObject.UserBO objo, string p2, string p5, string p6, int flag)
//        {
//            DataSet ds = new DataSet();
//            if (objo.UserId.ToString().Length > 0 && objo.UserId.ToString() != "")
//            {
//                List<SqlParameter> parameters = new List<SqlParameter>();
//                parameters.Add(new SqlParameter("@UserId", objo.UserId));
//                if (p2.Length > 0 && p2 != "")
//                    parameters.Add(new SqlParameter("@EmpType", p2));
//                if (p6.Length > 0 && p6 != "")
//                    parameters.Add(new SqlParameter("@Parm3", p6));
//                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, ProcName, parameters.ToArray());
//            }
//            else
//            {
//                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, ProcName);
//            }
//            return ds; 
//        }

//        internal static DataSet GetDashBoardDistrictWiseEmytype(string ProcName, BussinessObject.UserBO objo, string p2, string p3, string p4)
//        {
//            DataSet ds = new DataSet();
//            if (objo.UserId.ToString().Length > 0 && objo.UserId.ToString() != "")
//            {
//                List<SqlParameter> parameters = new List<SqlParameter>();
//                parameters.Add(new SqlParameter("@UserId", objo.UserId));
//                if (p2.Length > 0 && p2 != "")
//                    parameters.Add(new SqlParameter("@districtID", p2));
//                if (p3.Length > 0 && p3 != "")
//                    parameters.Add(new SqlParameter("@Emptype", p3));
//                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, ProcName, parameters.ToArray());
//            }
//            else
//            {
//                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, ProcName);
//            }
//            return ds;
//        }


        public static void bindDropDownofftype(string ProcName, DropDownList ddlofftype, string parm1, string parm2, string parm3, string parm4)
        {
            DataSet ds = new DataSet();
            if (parm1.Length > 0 && parm1 != "")
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@Parm1", parm1));
                if (parm2.Length > 0 && parm2 != "")
                    parameters.Add(new SqlParameter("@Parm2", parm2));
                if (parm3.Length > 0 && parm3 != "")
                    parameters.Add(new SqlParameter("@Parm3", parm3));
                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, ProcName, parameters.ToArray());
            }
            else
            {
                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, ProcName);
            }
            ddlofftype.Items.Clear();
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {

                ddlofftype.DataSource = ds;
                ddlofftype.DataTextField = "ValueText";
                ddlofftype.DataValueField = "ValueId";
                ddlofftype.DataBind();
            }
            ddlofftype.Items.Insert(0, new ListItem("-- समस्त --", "0", true));
        }

        public static void bindDropDownhead1desig(string ProcName, DropDownList ddlofftype, string parm1, string parm2, string parm3, string parm4)
        {
            DataSet ds = new DataSet();
            if (parm1.Length > 0 && parm1 != "")
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@Parm1", parm1));
                if (parm2.Length > 0 && parm2 != "")
                    parameters.Add(new SqlParameter("@Parm2", parm2));
                if (parm3.Length > 0 && parm3 != "")
                    parameters.Add(new SqlParameter("@Parm3", parm3));
                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, ProcName, parameters.ToArray());
            }
            else
            {
                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, ProcName);
            }
            ddlofftype.Items.Clear();
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {

                ddlofftype.DataSource = ds;
                ddlofftype.DataTextField = "ValueText";
                ddlofftype.DataValueField = "ValueId";
                ddlofftype.DataBind();
            }
            ddlofftype.Items.Insert(0, new ListItem("-- समस्त --", "0", true));
        }

        public static void bindDropDownhead2desig(string ProcName, DropDownList ddlofftype, string parm1, string parm2, string parm3, string parm4)
        {
            DataSet ds = new DataSet();
            if (parm1.Length > 0 && parm1 != "")
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@Parm1", parm1));
                if (parm2.Length > 0 && parm2 != "")
                    parameters.Add(new SqlParameter("@Parm2", parm2));
                if (parm3.Length > 0 && parm3 != "")
                    parameters.Add(new SqlParameter("@Parm3", parm3));
                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, ProcName, parameters.ToArray());
            }
            else
            {
                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, ProcName);
            }
            ddlofftype.Items.Clear();
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {

                ddlofftype.DataSource = ds;
                ddlofftype.DataTextField = "ValueText";
                ddlofftype.DataValueField = "ValueId";
                ddlofftype.DataBind();
            }
            ddlofftype.Items.Insert(0, new ListItem("-- समस्त --", "0", true));
        }

        internal static void bindDepartmentTxt(string ProcName, TextBox txtDepartID, string parm1, string parm2, string parm3, string parm4)
        {
            DataSet ds = new DataSet();
            if (parm1.Length > 0 && parm1 != "")
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@DistCode", parm1));
                if (parm2.Length > 0 && parm2 != "")
                    parameters.Add(new SqlParameter("@DepartCode", parm2));
                if (parm3.Length > 0 && parm3 != "")
                    parameters.Add(new SqlParameter("@Parm3", parm3));
                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, ProcName, parameters.ToArray());
            }
            else
            {
                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, ProcName);
            }
            txtDepartID.Text ="";
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                //ddlofftype.DataSource = ds;
                //ddlofftype.DataTextField = "ValueText";
                //ddlofftype.DataValueField = "ValueId";
                //ddlofftype.DataBind();
                txtDepartID.Text = ds.Tables[0].Rows[0]["ValueText"].ToString();
            }           
        }

        internal static void bindOfficeDropDown(string ProcName, DropDownList ddlOffice, string parm1, string parm2, string parm3)
        {
            DataSet ds = new DataSet();
            if (parm1.Length > 0 && parm1 != "")
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@Parm1", parm1));
                if (parm2.Length > 0 && parm2 != "")
                    parameters.Add(new SqlParameter("@Parm2", parm2));
                if (parm3.Length > 0 && parm3 != "")
                    parameters.Add(new SqlParameter("@Parm3", parm3));
                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, ProcName, parameters.ToArray());
            }
            else
            {
                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, ProcName);
            }
            ddlOffice.Items.Clear();
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {

                ddlOffice.DataSource = ds;
                ddlOffice.DataTextField = "ValueText";
                ddlOffice.DataValueField = "ValueId";
                ddlOffice.DataBind();
            }
            ddlOffice.Items.Insert(0, new ListItem("-- समस्त --", "0", true));
        }

        internal static void bindDropDownHn12(string ProcName, DropDownList ddl, string parm1, string parm2, string parm3)
        {

            DataSet ds = new DataSet();
            if (parm1.Length > 0 && parm1 != "")
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@Parm1", parm1));
                if (parm2.Length > 0 && parm2 != "")
                    parameters.Add(new SqlParameter("@Parm2", parm2));
                if (parm3.Length > 0 && parm3 != "")
                    parameters.Add(new SqlParameter("@Parm3", parm3));
                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, ProcName, parameters.ToArray());
            }
            else
            {
                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, ProcName);
            }
            ddl.Items.Clear();
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                ddl.DataSource = ds;
                ddl.DataTextField = "ValueText";
                ddl.DataValueField = "ValueId";
                ddl.DataBind();
            }
            ddl.Items.Insert(0, new ListItem("--कोई एक चयन करे--", "0", true));
        }


        internal static bool chkFreezeOffice(int parm1, int parm2, int parm3, string p3, string p4)
        {
            DataSet ds = new DataSet();
            if (parm1 > 0 )
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@Parm1", parm1));
                if (parm2 > 0 )
                    parameters.Add(new SqlParameter("@Parm2", parm2));
                if (parm3 > 0 )
                    parameters.Add(new SqlParameter("@Parm3", parm3));
                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.Text, "select 1 from dep where DistrictCode = "+parm1 +" and dep = "+parm2+" and dep_code = "+parm3+" and  feeded = 'Y' and feededstamp is not null");
            }
            else
            {
                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.Text, "select 1 from dep where DistrictCode = " + parm1 + " and dep = " + parm2 + " and dep_code = " + parm3 + " and  feeded = 'Y' and feededstamp is not null");
            }

            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
                return false;
        }


        internal static void bindSrNoTextBox(string ProcName, TextBox txtDepartID, string parm1, string parm2, string parm3, string parm4)
        {
            DataSet ds = new DataSet();
            if (parm1.Length > 0 && parm1 != "")
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@Parm1", parm1));
                if (parm2.Length > 0 && parm2 != "")
                    parameters.Add(new SqlParameter("@Parm2", parm2));
                if (parm3.Length > 0 && parm3 != "")
                    parameters.Add(new SqlParameter("@Parm3", parm3));
                if (parm4.Length > 0 && parm3 != "")
                    parameters.Add(new SqlParameter("@Parm4", parm4));
                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, ProcName, parameters.ToArray());
            }
            else
            {
                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, ProcName);
            }
            txtDepartID.Text = "";
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                //ddlofftype.DataSource = ds;
                //ddlofftype.DataTextField = "ValueText";
                //ddlofftype.DataValueField = "ValueId";
                //ddlofftype.DataBind();
                txtDepartID.Text = ds.Tables[0].Rows[0]["ValueText"].ToString();
            }
        }

        public static void bindDropDownHnGrid(string ProcName, List<SelectListItem> distNames, string parm1, string parm2, string parm3,long districtId1=0, long districtId2=0)
        {
            DataSet ds = new DataSet();
            SqlDataReader sdr;
            if (parm1.Length > 0 && parm1 != "")
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@Parm1", parm1));
                if (parm2.Length > 0 && parm2 != "")
                    parameters.Add(new SqlParameter("@Parm2", parm2));
                if (parm3.Length > 0 && parm3 != "")
                    parameters.Add(new SqlParameter("@Parm3", parm3)); 
                if(districtId1>0)
                    parameters.Add(new SqlParameter("@DistrictId1", districtId1));
                if (districtId2 >0)
                    parameters.Add(new SqlParameter("@DistrictId2", districtId2));
                    parameters.Add(new SqlParameter("dbName", UserSession.PushName));
                sdr = SqlHelper.ExecuteReader(CommonConfig.Conn(), CommandType.StoredProcedure, ProcName, parameters.ToArray());
            }
            else
            {
                sdr = SqlHelper.ExecuteReader(CommonConfig.Conn(), CommandType.StoredProcedure, ProcName);
            }
            if (parm3 != "Z")
            { 
                if (parm3 == "S")
                {
                    distNames.Insert(0, new SelectListItem { Text = "--Select--", Value = "-1" });
                }
                if (parm3 == "A")
                {
                    distNames.Insert(0, new SelectListItem { Text = "--All--", Value = "-1" });
                }
                if (parm3 == "N")
                {
                    distNames.Insert(0, new SelectListItem { Text = "N.A.", Value = "-1" });
                }
            }
            while (sdr.Read())
            {
                distNames.Add(new SelectListItem
                {

                    Text = sdr["ValueText"].ToString(),
                    Value = sdr["ValueId"].ToString()
                });
            }
            sdr.Close();
        }

    }
}
