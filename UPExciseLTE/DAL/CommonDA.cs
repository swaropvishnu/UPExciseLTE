using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UPExciseLTE.BLL;
using UPExciseLTE.Models;
using Microsoft.ApplicationBlocks.Data;
using System.Net.NetworkInformation;
using UPExciseLTE.Models;
using Dapper;

namespace UPExciseLTE.DAL
{
    public class CommonDA
    {
        SqlDataAdapter adap;
        SqlCommand cmd;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
        internal LoginModal GetUserDetail(LoginModal objUserData)
        {
            DataTable dt = new DataTable();
            LoginModal fm = new LoginModal();
            try
            {
                con.Open();
                cmd = new SqlCommand("proc_GetUserMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@UID", SqlDbType.Int).Value = objUserData.UserId);
                adap = new SqlDataAdapter(cmd);
                dt = new DataTable();
                adap.Fill(dt);
                cmd.Dispose();
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        fm.UserId = objUserData.UserId;
                        fm.UserName = dt.Rows[i]["Name"].ToString();
                        fm.UserMobile = dt.Rows[i]["MobileNo"].ToString();
                        fm.UserNameHindi = dt.Rows[i]["NameHindi"].ToString();
                        fm.UserEmail = dt.Rows[i]["EmailId"].ToString();
                        fm.UserAddress = dt.Rows[i]["Office_Add"].ToString();
                        fm.UserImage = (Byte[])dt.Rows[i]["PhotoContent"];
                    }


                }
                return fm;

            }
            catch
            {
                throw;
            }
            finally
            {

                con.Close();
                con.Dispose();
            }
        }
        public string filter_bad_chars_rep(string s)
        {
            string[] sL_Char = {
            "onfocus",          "\"\"",         "=",            "onmouseover",          "prompt",           "%27",          "alert#",
            "alert",
            "'or",
            "`or",
            "`or`",
            "'or'",
            "'='",
            "`=`",
            "'",
            "`",
            "9,9,9",
            "src",
            "onload",
            "select",
            "drop",
            "insert",
            "delete",
            "xp_",
            "having",
            "union",
            "group",
            "update",
            "script",
            "<script",
            "</script>",
            "language",
            "javascript",
            "vbscript",
            "http",
            "~",
            "$",
            "<",
            ">",
            "%",
            "\\",
            ";",
            "@",
            "_",
            "{",
            "}",
            "^",
            "?",
            "[",
            "]",
            "!",
            "#",
            "&",
            "*"
        };
            int er = 0;
            int sL_Char_Length = sL_Char.Length - 1;
            while (sL_Char_Length >= 0)
            {
                if (s.Contains(sL_Char[sL_Char_Length]))
                {
                    s.Replace(sL_Char[sL_Char_Length], "");
                    break; // TODO: might not be correct. Was : Exit While
                }
                sL_Char_Length -= 1;
            }
            return s;
        }
      
        internal String UpdateUserDetail(LoginModal objUserData)
        {
            string result = "";
            try
            {
                con.Open();
                cmd = new SqlCommand("proc_UpdateUserProfile", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@UID", SqlDbType.Int).Value = objUserData.UserId);
                cmd.Parameters.Add(new SqlParameter("@UserName", SqlDbType.VarChar).Value = objUserData.UserName);
                cmd.Parameters.Add(new SqlParameter("@UserHindiName", SqlDbType.NVarChar).Value = objUserData.UserNameHindi);
                cmd.Parameters.Add(new SqlParameter("@UserMobile", SqlDbType.VarChar).Value = objUserData.UserMobile);
                cmd.Parameters.Add(new SqlParameter("@UserEmail", SqlDbType.VarChar).Value = objUserData.UserEmail);
                cmd.Parameters.Add(new SqlParameter("@UserAddress", SqlDbType.VarChar).Value = objUserData.UserAddress);
                cmd.Parameters.Add(new SqlParameter("@UserProfileImage", SqlDbType.VarBinary).Value = objUserData.UserImage);
                cmd.ExecuteNonQuery();
                result = "Success";

            }
            catch
            {
                result = "Failed";
                throw;

            }
            finally
            {

                con.Close();
                con.Dispose();
            }

            return result;
        }
        public static string GetMACAddress()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String sMacAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            {
                if (sMacAddress == String.Empty)
                {
                    IPInterfaceProperties properties = adapter.GetIPProperties();
                    sMacAddress = adapter.GetPhysicalAddress().ToString();
                }
            }
            return sMacAddress;
        }
        public static string GetIpAddress()
        {
            string ipaddress;

            ipaddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (ipaddress == "" || ipaddress == null)

                ipaddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            return ipaddress;
        }
        string IpAddress = GetIpAddress();
        string MacAddress = GetMACAddress();
        #region Industrial
      
      
       
        public DataTable Getplot(int IndustrialAreaCode, long PlotCode, string PlotSerialNo, string PlotName, DateTime FromDate, DateTime ToDate, string IsPlot_Disputed, string IsPlot_Assigned)
        {
            DataTable dt = new DataTable();
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "[Proc_GetPlot]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@IndustrialEstateCode", IndustrialAreaCode));
                cmd.Parameters.Add(new SqlParameter("PlotCode", PlotCode));
                cmd.Parameters.Add(new SqlParameter("PlotSerialNo", PlotSerialNo));
                cmd.Parameters.Add(new SqlParameter("PlotName", PlotName));
                cmd.Parameters.Add(new SqlParameter("FromDate", FromDate));
                cmd.Parameters.Add(new SqlParameter("ToDate", ToDate));
                cmd.Parameters.Add(new SqlParameter("IsPlot_Disputed", IsPlot_Disputed));
                cmd.Parameters.Add(new SqlParameter("IsPlot_Assigned", IsPlot_Assigned));
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);
            }
            catch (Exception e)
            {
                dt = null;
            }
            finally
            {

                con.Close();
                con.Dispose();
            }
            return dt;
        }
        public DataTable GetShed(int IndustrialAreaCode, long @ShedCode, string @ShedSerialNo, string @ShedName, DateTime FromDate, DateTime ToDate, string @IsShed_Disputed, string @IsShed_Assigned)
        {
            DataTable dt = new DataTable();
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "[Proc_GetShed]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@IndustrialEstateCode", IndustrialAreaCode));
                cmd.Parameters.Add(new SqlParameter("@ShedCode", @ShedCode));
                cmd.Parameters.Add(new SqlParameter("@ShedSerialNo", @ShedSerialNo));
                cmd.Parameters.Add(new SqlParameter("@ShedName", @ShedName));
                cmd.Parameters.Add(new SqlParameter("FromDate", FromDate));
                cmd.Parameters.Add(new SqlParameter("ToDate", ToDate));
                cmd.Parameters.Add(new SqlParameter("IsShed_Disputed", IsShed_Disputed));
                cmd.Parameters.Add(new SqlParameter("IsShed_Assigned", IsShed_Assigned));
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);
            }
            catch (Exception e)
            {
                dt = null;
            }
            finally
            {

                con.Close();
                con.Dispose();
            }
            return dt;
        }
        public DataTable GetDashBord()
        {
            DataTable dt = new DataTable();
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "[Proc_GetDashBord]";
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);
            }
            catch (Exception e)
            {
                dt = null;
            }
            finally
            {

                con.Close();
                con.Dispose();
            }
            return dt;
        }
        public DataSet GetEstateeAllotee(long allotee_code)
        {
            DataSet ds = new DataSet();
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "Proc_GetEstateeAllotee";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@allotee_code", allotee_code));
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
            }
            catch (Exception e)
            {
                ds = null;
            }
            finally
            {

                con.Close();
                con.Dispose();
            }
            return ds;
        }

    

        public DataTable GetIndustrialEstateInfo(int @IndustrialEstateCode, string @IndustrialEstateName, int @DistrictID, int @TehSilID, int @BlocID, int VillageID, string @PinCode, string @PlotNo, string @ShadeNo, DateTime @FromDate, DateTime @ToDate, DateTime @FromEstablishment, DateTime @ToEstablishment, string IndustryType)
        {
            DataTable dt = new DataTable();
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "[Proc_GetIndustrialEstateInfo]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("IndustrialEstateCode", IndustrialEstateCode));
                cmd.Parameters.Add(new SqlParameter("@IndustrialEstateName", @IndustrialEstateName));
                cmd.Parameters.Add(new SqlParameter("@DistrictID", @DistrictID));
                cmd.Parameters.Add(new SqlParameter("@TehSilID", @TehSilID));
                cmd.Parameters.Add(new SqlParameter("@BlocID", @BlocID));
                cmd.Parameters.Add(new SqlParameter("@VillageID", @VillageID));
                cmd.Parameters.Add(new SqlParameter("@PinCode", @PinCode));
                cmd.Parameters.Add(new SqlParameter("@PlotNo", @PlotNo));
                cmd.Parameters.Add(new SqlParameter("@ShadeNo", @ShadeNo));
                cmd.Parameters.Add(new SqlParameter("@FromDate", @FromDate));
                cmd.Parameters.Add(new SqlParameter("@ToDate", @ToDate));
                cmd.Parameters.Add(new SqlParameter("@FromEstablishment", @FromEstablishment));
                cmd.Parameters.Add(new SqlParameter("@ToEstablishment", @ToEstablishment));
                cmd.Parameters.Add(new SqlParameter("@IndustrialType", IndustryType));
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);
            }
            catch (Exception e)
            {
                dt = null;
            }
            finally
            {

                con.Close();
                con.Dispose();
            }
            return dt;
        }
        public DataTable GetShedInfo(int @IndustrialEstateCode, Int64 @ShedCode, string @ShedSerialNo, string @ShedName, DateTime @FromDate, DateTime @ToDate, string @IsShed_Disputed, string @IsShed_Assigned)
        {
            DataTable dt = new DataTable();
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "[Proc_GetShed]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("IndustrialEstateCode", IndustrialEstateCode));
                cmd.Parameters.Add(new SqlParameter("@ShedCode", @ShedCode));
                cmd.Parameters.Add(new SqlParameter("@ShedSerialNo", @ShedSerialNo));
                cmd.Parameters.Add(new SqlParameter("@ShedName", @ShedName));
                cmd.Parameters.Add(new SqlParameter("@FromDate", @FromDate));
                cmd.Parameters.Add(new SqlParameter("@ToDate", @ToDate));
                cmd.Parameters.Add(new SqlParameter("@IsShed_Disputed", @IsShed_Disputed));
                cmd.Parameters.Add(new SqlParameter("@IsShed_Assigned", @IsShed_Assigned));
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);
            }
            catch (Exception e)
            {
                dt = null;
            }
            finally
            {

                con.Close();
                con.Dispose();
            }
            return dt;
        }
        public DataTable GetPlotInfo(int IndustrialEstateCode, Int64 @PlotCode, string @PlotSerialNo, string @PlotName, DateTime @FromDate, DateTime @ToDate, string @IsPlot_Disputed, string @IsPlot_Assigned)
        {
            DataTable dt = new DataTable();
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "[Proc_GetPlot]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("IndustrialEstateCode ", IndustrialEstateCode));
                cmd.Parameters.Add(new SqlParameter("@PlotCode", @PlotCode));
                cmd.Parameters.Add(new SqlParameter("@PlotSerialNo", @PlotSerialNo));
                cmd.Parameters.Add(new SqlParameter("@PlotName", @PlotName));
                cmd.Parameters.Add(new SqlParameter("@FromDate", @FromDate));
                cmd.Parameters.Add(new SqlParameter("@ToDate", @ToDate));
                cmd.Parameters.Add(new SqlParameter("@IsPlot_Disputed", @IsPlot_Disputed));
                cmd.Parameters.Add(new SqlParameter("@IsPlot_Assigned", @IsPlot_Assigned));
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);
            }
            catch (Exception e)
            {
                dt = null;
            }
            finally
            {

                con.Close();
                con.Dispose();
            }
            return dt;
        }
        public DataTable GetPlotforSal(int IndustrialEstateCode)
        {
            DataTable dt = new DataTable();
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "[Proc_GetPlotforSale]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("IndustrialEstateCode ", IndustrialEstateCode));
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);
            }
            catch (Exception e)
            {
                dt = null;
            }
            finally
            {

                con.Close();
                con.Dispose();
            }
            return dt;
        }
        #endregion
        #region Form
        
        #endregion
        #region Applicant
        public DataTable Getifsc(string @prefix)
        {
            DataTable ds = new DataTable();
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "Proc_Getifsc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@prefix", @prefix));
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
            }
            catch (Exception e)
            {
                ds = null;
            }
            finally
            {

                con.Close();
                con.Dispose();
            }
            return ds;
        }
        public DataTable Getsponsoring_office(int @district_code_census)
        {
            DataTable ds = new DataTable();
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "SP_Getsponsoring_office";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@district_code_census", @district_code_census));
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
            }
            catch (Exception e)
            {
                ds = null;
            }
            finally
            {

                con.Close();
                con.Dispose();
            }
            return ds;
        }
        public DataTable GetBankDetails(int @bank_code)
        {
            DataTable dt = new DataTable();
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "[Proc_GetBankDetails]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@bank_code ", @bank_code));
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);
            }
            catch (Exception e)
            {
                dt = null;
            }
            finally
            {

                con.Close();
                con.Dispose();
            }
            return dt;
        }
        public DataTable GetMYSYDashboard(int @UserLevel)
        {
            DataTable dt = new DataTable();
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "[Proc_GetMYSYDashboard]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@UserLevel ", @UserLevel));
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);
            }
            catch (Exception e)
            {
                dt = null;
            }
            finally
            {

                con.Close();
                con.Dispose();
            }
            return dt;
        }
        public DataSet GetScheme_Doc(string user_name, short scheme_code)
        {
            DataSet ds = new DataSet();
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "[Proc_GetScheme_Doc]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@scheme_code", scheme_code));
                cmd.Parameters.Add(new SqlParameter("@user_name", user_name));
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
            }
            catch (Exception e)
            {
                ds = null;
            }
            finally
            {

                con.Close();
                con.Dispose();
            }
            return ds;
        }

        public DataTable GetApplicant_Doc(string user_name, short scheme_code, short @doc_id)
        {
            DataTable ds = new DataTable();
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "[Proc_GetApplicant_Doc]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@scheme_code", scheme_code));
                cmd.Parameters.Add(new SqlParameter("@user_name", user_name));
                cmd.Parameters.Add(new SqlParameter("@doc_id", @doc_id));
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
            }
            catch (Exception e)
            {
                ds = null;
            }
            finally
            {

                con.Close();
                con.Dispose();
            }
            return ds;
        }
      
        public string InsertUpdateCMYSS_MachininaryDetails(DataTable  objdoc)
        {
            string message = "Save";
            string UserName = "";
            con.Open();
            new DataTable();
            SqlTransaction transaction = con.BeginTransaction(); 
            try
            {
                if (objdoc != null)
                {
                    if (objdoc.Rows.Count > 0)
                    {
                        
                            cmd = new SqlCommand();
                            cmd.Connection = con;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "[Proc_InsertRegistrationMachinedetails]";
                            cmd.CommandTimeout = 3600;
                            cmd.Parameters.Add(new SqlParameter("@registration_code", objdoc.Rows[0]["registration_code"].ToString().Trim()));
                            cmd.Parameters.Add(new SqlParameter("@tbl_registration_mach", objdoc));
                            cmd.Parameters.Add(new SqlParameter("user_Id", @UserSession.LoggedInUser.UserName));
                            cmd.Parameters.Add(new SqlParameter("user_ip", this.IpAddress));
                            cmd.Parameters.Add(new SqlParameter("user_mac", this.MacAddress));
                            cmd.Parameters.Add(new SqlParameter("Msg", ""));
                            cmd.Parameters["Msg"].Size = 256;
                            cmd.Parameters["Msg"].Direction = ParameterDirection.InputOutput;
                            cmd.Transaction = transaction;
                            cmd.ExecuteNonQuery();
                            message = cmd.Parameters["Msg"].Value.ToString();
                        }
                    }
                
                transaction.Commit();
            }
            catch (Exception ex1)
            {
                transaction.Rollback();
                message = ex1.Message;
            }
            finally
            {

                con.Close();
                con.Dispose();
            }
            return message;
        }
        public string InsertRegistrationFinancedetails(DataTable  objdoc)
        {
            string message = "Save";
            string UserName = "";
            con.Open();
            new DataTable();
            SqlTransaction transaction = con.BeginTransaction();  // this.cn.BeginTransaction();
            try
            {
                if (objdoc != null)
                {
                    if (objdoc.Rows.Count > 0)
                    {
                            cmd = new SqlCommand();
                            cmd.Connection = con;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "[Proc_InsertRegistrationFinancedetails]";
                            cmd.CommandTimeout = 3600;
                            cmd.Parameters.Add(new SqlParameter("@registration_code", objdoc.Rows[0]["registration_code"].ToString().Trim()));
                            cmd.Parameters.Add(new SqlParameter("@tbl_registration_fin", objdoc));
                            cmd.Parameters.Add(new SqlParameter("user_Id", @UserSession.LoggedInUser.UserName));
                            cmd.Parameters.Add(new SqlParameter("user_ip", this.IpAddress));
                            cmd.Parameters.Add(new SqlParameter("user_mac", this.MacAddress));
                            cmd.Parameters.Add(new SqlParameter("Msg", ""));
                            cmd.Parameters["Msg"].Size = 256;
                            cmd.Parameters["Msg"].Direction = ParameterDirection.InputOutput;
                            cmd.Transaction = transaction;
                            cmd.ExecuteNonQuery();
                            message = cmd.Parameters["Msg"].Value.ToString();
                    }
                }
                transaction.Commit();
            }
            catch (Exception ex1)
            {
                transaction.Rollback();
                message = ex1.Message;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            return message;
        }
        public string InsertUpdateCMYSS_Applicantdoc2(DataTable dt)
        {
            string message = "Save";
            string UserName = "";
            con.Open();
            new DataTable();
            SqlTransaction transaction = con.BeginTransaction();  // this.cn.BeginTransaction();
            try
            {
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            cmd = new SqlCommand();
                            cmd.Connection = con;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "[Proc_InsertRegistrationDoc]";
                            cmd.CommandTimeout = 3600;
                            cmd.Parameters.Add(new SqlParameter("registration_code", dt.Rows[j]["applicant_code"].ToString().Trim()));
                            cmd.Parameters.Add(new SqlParameter("@doc", dt.Rows[j]["doc"]));// 
                            cmd.Parameters.Add(new SqlParameter("@doc_type", dt.Rows[j]["doc_type"].ToString().Trim()));
                            cmd.Parameters.Add(new SqlParameter("@doc_content_type", dt.Rows[j]["doc_content_type"].ToString().Trim()));
                            cmd.Parameters.Add(new SqlParameter("IsFirst", j == 0 ? true : false));
                            cmd.Parameters.Add(new SqlParameter("@user_Id", @UserSession.LoggedInUser.UserName));
                            cmd.Parameters.Add(new SqlParameter("@user_ip", this.IpAddress));
                            cmd.Parameters.Add(new SqlParameter("@user_mac", this.MacAddress));
                            //cmd.Parameters.Add(new SqlParameter("@UMac", this.MacAddress));
                            // cmd.Parameters.Add(new SqlParameter("@UUserID", @UserSession.LoggedInUser.UserName));
                            // cmd.Parameters.Add(new SqlParameter("@UUserIP", this.IpAddress));
                            cmd.Parameters.Add(new SqlParameter("Msg", ""));
                            cmd.Parameters["Msg"].Size = 256;
                            cmd.Parameters["Msg"].Direction = ParameterDirection.InputOutput;
                            cmd.Transaction = transaction;
                            cmd.ExecuteNonQuery();
                            if (j == 0)
                            {
                                message = cmd.Parameters["Msg"].Value.ToString();
                            }

                        }
                    }
                }
                transaction.Commit();
            }
            catch (Exception ex1)
            {
                transaction.Rollback();
                message = ex1.Message;
            }
            finally
            {

                con.Close();
                con.Dispose();
            }
            return message;
        }


        public string InsertUpdateCMYSS_Applicantdocument(DataTable dt)
        {
            string message = "";
            string UserName = "";
            con.Open();
            new DataTable();
            SqlTransaction transaction = con.BeginTransaction();  // this.cn.BeginTransaction();
            try
            {
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {

                        cmd = new SqlCommand();
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "[Proc_InsertRegistrationDoc2]";
                        cmd.CommandTimeout = 3600;
                        cmd.Parameters.Add(new SqlParameter("registration_code", dt.Rows[0]["applicant_code"].ToString().Trim()));
                        cmd.Parameters.Add(new SqlParameter("@tbl_registration_doc2", dt));
                        cmd.Parameters.Add(new SqlParameter("@user_Id", @UserSession.LoggedInUser.UserName));
                        cmd.Parameters.Add(new SqlParameter("@user_ip", this.IpAddress));
                        cmd.Parameters.Add(new SqlParameter("@user_mac", this.MacAddress));
                        cmd.Parameters.Add(new SqlParameter("Msg", ""));
                        cmd.Parameters["Msg"].Size = 256;
                        cmd.Parameters["Msg"].Direction = ParameterDirection.InputOutput;
                        cmd.Transaction = transaction;
                        cmd.ExecuteNonQuery();
                        message = cmd.Parameters["Msg"].Value.ToString();
                    }
                    else
                    {
                        message = "Please Upload File";
                    }
                }
                else
                {
                    message = "Please Upload File";
                }

                transaction.Commit();
            }
            catch (Exception ex1)
            {
                transaction.Rollback();
                message = ex1.Message;
            }
            finally
            {

                con.Close();
                con.Dispose();
            }
            return message;
        }

        public string Insert_vhpp_artwork(DataTable dt)
        {
            string message = "Save";
            string UserName = "";
            con.Open();
            new DataTable();
            SqlTransaction transaction = con.BeginTransaction();  // this.cn.BeginTransaction();
            try
            {

                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[Proc_Insert_VHPY_artwork]";
                cmd.CommandTimeout = 3600;
                cmd.Parameters.Add(new SqlParameter("@registration_code", dt.Rows[0]["applicant_code"].ToString().Trim()));
                cmd.Parameters.Add(new SqlParameter("@tbl_vhpp_artwork", dt));
                cmd.Parameters.Add(new SqlParameter("Msg", ""));
                cmd.Parameters["Msg"].Size = 256;
                cmd.Parameters["Msg"].Direction = ParameterDirection.InputOutput;
                cmd.Transaction = transaction;
                cmd.ExecuteNonQuery();
                message = cmd.Parameters["Msg"].Value.ToString();

                transaction.Commit();
            }
            catch (Exception ex1)
            {
                transaction.Rollback();
                message = ex1.Message;
            }
            finally
            {

                con.Close();
                con.Dispose();
            }
            return message;
        }



        #endregion
        #region Master

        #endregion
        #region Gaurav
        public string InsertUpdateBrand(BrandMaster brand)
        { 
            string str="";
            con.Open();
            SqlTransaction tran = con.BeginTransaction();
            try
            {
                cmd = new SqlCommand("Proc_InsertUpdate_Brand", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = tran;
                cmd.Parameters.Add(new SqlParameter("dbName", brand.dbName));
                cmd.Parameters.Add(new SqlParameter("BrandId", brand.BrandId));
                cmd.Parameters.Add(new SqlParameter("BreweryId", brand.BreweryId));
                cmd.Parameters.Add(new SqlParameter("BrandName", brand.BrandName));
                cmd.Parameters.Add(new SqlParameter("BrandRegistrationNumber", brand.BrandRegistrationNumber));
                cmd.Parameters.Add(new SqlParameter("Strength", brand.Strength));
                cmd.Parameters.Add(new SqlParameter("AlcoholType", brand.AlcoholType));
                cmd.Parameters.Add(new SqlParameter("LiquorType", brand.LiquorType));
                cmd.Parameters.Add(new SqlParameter("LicenceType", brand.LicenceType));
                cmd.Parameters.Add(new SqlParameter("LicenceNo", brand.LicenceNo));
                cmd.Parameters.Add(new SqlParameter("ExiciseTin", brand.ExiciseTin));
                cmd.Parameters.Add(new SqlParameter("MRP", brand.MRP));
                cmd.Parameters.Add(new SqlParameter("XFactoryPrice", brand.XFactoryPrice));
                cmd.Parameters.Add(new SqlParameter("AdditionalDuty", brand.AdditionalDuty));
                cmd.Parameters.Add(new SqlParameter("QuantityInCase", brand.QuantityInCase));
                cmd.Parameters.Add(new SqlParameter("QuantityInBottleML", brand.QuantityInBottleML));
                cmd.Parameters.Add(new SqlParameter("PackagingType", brand.PackagingType));
                cmd.Parameters.Add(new SqlParameter("ExciseDuty", brand.ExciseDuty));
                cmd.Parameters.Add(new SqlParameter("ExportBoxSize", brand.ExportBoxSize));
                cmd.Parameters.Add(new SqlParameter("Remark", brand.Remark));
               
               
                cmd.Parameters.Add(new SqlParameter("StateId", brand.StateId));
                cmd.Parameters.Add(new SqlParameter("c_user_id", ""));
                cmd.Parameters.Add(new SqlParameter("c_user_ip", ""));
                cmd.Parameters.Add(new SqlParameter("c_time_stamp", ""));
                cmd.Parameters.Add(new SqlParameter("c_mac", ""));
                cmd.Parameters.Add(new SqlParameter("sp_type", brand.SPType));
                cmd.Parameters.Add(new SqlParameter("Msg", ""));
                cmd.Parameters["Msg"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters["Msg"].Size = 256;
                cmd.ExecuteNonQuery();
                str = cmd.Parameters["Msg"].Value.ToString().Trim();
                tran.Commit();
            }
            catch (Exception ex)
            {
                str = ex.ToString();
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            return str;
        }
        public string UpdateBrand(string dbName, int BrandId,int TypeId)
        {
            string str = "";
            con.Open();
            SqlTransaction tran = con.BeginTransaction();
            try
            {
                cmd = new SqlCommand("Proc_Update_Brand", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = tran;
                cmd.Parameters.Add(new SqlParameter("dbName", dbName));
                cmd.Parameters.Add(new SqlParameter("BrandId",BrandId));
                cmd.Parameters.Add(new SqlParameter("TypeId", TypeId));
                cmd.Parameters.Add(new SqlParameter("Msg", ""));
                cmd.Parameters["Msg"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters["Msg"].Size = 256;
                cmd.ExecuteNonQuery();
                str = cmd.Parameters["Msg"].Value.ToString().Trim();
                tran.Commit();
            }
            catch (Exception ex)
            {
                str = ex.ToString();
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            return str;
        }
        public DataSet GetBrandDetail(int BrandId, string LiquorType, string LicenceType, string LicenseNo, short BreweryId, short DistrictCode, int TehsilCode,string Status)
        {
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("BrandId", BrandId));
                parameters.Add(new SqlParameter("LiquorType", LiquorType));
                parameters.Add(new SqlParameter("LicenceType", LicenceType));
                parameters.Add(new SqlParameter("LicenseNo", LicenseNo));
                parameters.Add(new SqlParameter("BreweryId", BreweryId));
                parameters.Add(new SqlParameter("DistrictCode", DistrictCode));
                parameters.Add(new SqlParameter("TehsilCode", TehsilCode));
                parameters.Add(new SqlParameter("Status", Status));
                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, "PROC_GetBrand", parameters.ToArray());
            }
            catch (Exception exp)
            {
                ds = null;
            }
            return ds;
        }

        internal string InsertUpdatePlan(BottelingPlan BP)
        {
            string result = "";
            con.Open();
            SqlTransaction tran = con.BeginTransaction();
            try
            {
                cmd = new SqlCommand("Proc_InsertUpdatePlan", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = tran;
                cmd.Parameters.Add(new SqlParameter("dbName", BP.dbName));
                cmd.Parameters.Add(new SqlParameter("PlanId", BP.PlanId));
                cmd.Parameters.Add(new SqlParameter("IsStateSame", BP.IsStateSame));
                cmd.Parameters.Add(new SqlParameter("BrandId", BP.BrandId));
                cmd.Parameters.Add(new SqlParameter("NumberOfCases", BP.NumberOfCases));
                cmd.Parameters.Add(new SqlParameter("QunatityInCaseExport", BP.QunatityInCaseExport));
                cmd.Parameters.Add(new SqlParameter("BulkLitre", BP.BulkLiter));
                cmd.Parameters.Add(new SqlParameter("AlcoholicLitre", BP.AlcoholicLiter));
                cmd.Parameters.Add(new SqlParameter("TotalUnitQuantity", BP.TotalUnitQuantity));
                cmd.Parameters.Add(new SqlParameter("DateOfPlan", BP.DateOfPlan));
                cmd.Parameters.Add(new SqlParameter("BatchNo", BP.BatchNo));
                cmd.Parameters.Add(new SqlParameter("MappedOrNot", BP.MappedOrNot));
                cmd.Parameters.Add(new SqlParameter("IsPlanFinal", BP.IsPlanFinal));
                cmd.Parameters.Add(new SqlParameter("Type", BP.Type));
                cmd.Parameters.Add(new SqlParameter("Msg", ""));
                cmd.Parameters["Msg"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters["Msg"].Size = 256;
                cmd.ExecuteNonQuery();
                result = cmd.Parameters["Msg"].Value.ToString().Trim();
                tran.Commit();
            }
            catch (Exception exp)
            {
                tran.Rollback();
                result = exp.Message;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            return result;
        }

        internal string InsertUpdateProductionPlan(BottelingPlan BP)
        {
            string result = "";
            con.Open();
            SqlTransaction tran = con.BeginTransaction();
            try
            {
                cmd = new SqlCommand("PROC_InsertUpdateProductionPlan", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = tran;
                cmd.Parameters.Add(new SqlParameter("dbName", BP.dbName));
                cmd.Parameters.Add(new SqlParameter("PlanId", BP.PlanId));
                cmd.Parameters.Add(new SqlParameter("ProducedNumberOfCases", BP.ProducedNumberOfCases));
                cmd.Parameters.Add(new SqlParameter("ProducedQunatityInCaseExport", BP.ProducedQunatityInCaseExport));
                cmd.Parameters.Add(new SqlParameter("ProducedBulkLitre", BP.ProducedBulkLiter));
                cmd.Parameters.Add(new SqlParameter("ProducedAlcoholicLitre", BP.ProducedAlcoholicLiter));
                cmd.Parameters.Add(new SqlParameter("ProducedTotalUnitQuantity", BP.ProducedTotalUnitQuantity));
                cmd.Parameters.Add(new SqlParameter("WastageInNumber", BP.WastageInNumber));
                cmd.Parameters.Add(new SqlParameter("WastageBL", BP.WastageBL));
                cmd.Parameters.Add(new SqlParameter("IsProductionFinal", BP.IsProductionFinal));
                cmd.Parameters.Add(new SqlParameter("Type", BP.Type));
                cmd.Parameters.Add(new SqlParameter("Msg", ""));
                cmd.Parameters["Msg"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters["Msg"].Size = 256;
                cmd.ExecuteNonQuery();
                result = cmd.Parameters["Msg"].Value.ToString().Trim();
                tran.Commit();
            }
            catch (Exception exp)
            {
                tran.Rollback();
                result = exp.Message;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            return result;
        }
        public DataSet GetBottelingPlanDetail(DateTime FromDate, DateTime ToDate, short BreweryId, int BrandId, string Status, string Mapped, string Import, int PlanId)
        {
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("FromDate", FromDate));
                parameters.Add(new SqlParameter("ToDate", ToDate));
                parameters.Add(new SqlParameter("BreweryId", BreweryId));
                parameters.Add(new SqlParameter("BrandId", BrandId));
                parameters.Add(new SqlParameter("Status", Status));
                parameters.Add(new SqlParameter("Mapped", Mapped));
                parameters.Add(new SqlParameter("Import", Import));
                parameters.Add(new SqlParameter("PlanId", PlanId));
                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, "PROC_GetBottelingPlanDetail", parameters.ToArray());
            }
            catch (Exception exp)
            {
                ds = null;
            }
            return ds;
        }
        public DataSet GetProducePlanDetail(DateTime FromDate, DateTime ToDate, short BreweryId, int BrandId, string Status, string Mapped, string Import, int PlanId)
        {
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("FromDate", FromDate));
                parameters.Add(new SqlParameter("ToDate", ToDate));
                parameters.Add(new SqlParameter("BreweryId", BreweryId));
                parameters.Add(new SqlParameter("BrandId", BrandId));
                parameters.Add(new SqlParameter("Status", Status));
                parameters.Add(new SqlParameter("Mapped", Mapped));
                parameters.Add(new SqlParameter("Import", Import));
                parameters.Add(new SqlParameter("PlanId", PlanId));
                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, "PROC_GetProducePlanDetail", parameters.ToArray());
            }
            catch (Exception exp)
            {
                ds = null;
            }
            return ds;
        }
        public DataSet GetFinalizedPlan(DateTime FromDate, DateTime ToDate, short BreweryId, int BrandId, string Status, string Mapped, string Import, int PlanId)
        {
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("FromDate", FromDate));
                parameters.Add(new SqlParameter("ToDate", ToDate));
                parameters.Add(new SqlParameter("BreweryId", BreweryId));
                parameters.Add(new SqlParameter("BrandId", BrandId));
                parameters.Add(new SqlParameter("Status", Status));
                parameters.Add(new SqlParameter("Mapped", Mapped));
                parameters.Add(new SqlParameter("Import", Import));
                parameters.Add(new SqlParameter("PlanId", PlanId));
                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, "PROC_GetFinalizedPlan", parameters.ToArray());
            }
            catch (Exception exp)
            {
                ds = null;
            }
            return ds;
        }
        public DataSet GetQRCodeDetails(DateTime FromDate, DateTime ToDate, short BreweryId, int BrandId, string Status, string Mapped, string Import, int PlanId)
        {
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("FromDate", FromDate));
                parameters.Add(new SqlParameter("ToDate", ToDate));
                parameters.Add(new SqlParameter("BreweryId", BreweryId));
                parameters.Add(new SqlParameter("BrandId", BrandId));
                parameters.Add(new SqlParameter("Status", Status));
                parameters.Add(new SqlParameter("Mapped", Mapped));
                parameters.Add(new SqlParameter("Import", Import));
                parameters.Add(new SqlParameter("PlanId", PlanId));
                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, "PROC_GetQRCodeDetails", parameters.ToArray());
            }
            catch (Exception exp)
            {
                ds = null;
            }
            return ds;
        }
        public DataSet GetQRCOde(int PlanId)
        {
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("PlanId", PlanId));
                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, "PROC_GetQRCOde", parameters.ToArray());
            }
            catch (Exception exp)
            {
                ds = null;
            }
            return ds;
        }
        #endregion

        internal string GenerateQRCode(int PlanId,string UserId,string dbName)
        {
            string result = "";
            con.Open();
            SqlTransaction tran = con.BeginTransaction();
            try
            {
                cmd = new SqlCommand("PROC_GenerateQRCode", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = tran;
                cmd.Parameters.Add(new SqlParameter("dbName", dbName));
                cmd.Parameters.Add(new SqlParameter("PlanId", PlanId));
                cmd.Parameters.Add(new SqlParameter("c_user_id", UserId));
                cmd.Parameters.Add(new SqlParameter("c_user_ip", IpAddress));
                cmd.Parameters.Add(new SqlParameter("c_mac", MacAddress));
                cmd.Parameters.Add(new SqlParameter("Msg", ""));
                cmd.Parameters["Msg"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters["Msg"].Size = 256;
                cmd.ExecuteNonQuery();
                result = cmd.Parameters["Msg"].Value.ToString().Trim();
                tran.Commit();
            }
            catch (Exception exp)
            {
                tran.Rollback();
                result = exp.Message;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            return result;
        }

        #region DistrictWholesaleToRetailor

        public IEnumerable<DistrictWholeSaleToRetailorModel> GetGatePassForDistrictWholesaleToRetailor()
        {
            var para = new DynamicParameters();
            para.Add("@SpType", Convert.ToInt32(UserSession.LoggedInUserLevelId));
            try
            {
                return con.Query<DistrictWholeSaleToRetailorModel>("Proc_GetGatePassForDistrictWholesaleToRetailor", para, null, true, 0, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        #endregion


    }
}