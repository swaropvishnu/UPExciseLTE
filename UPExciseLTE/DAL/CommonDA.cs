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
 
namespace UPExciseLTE.DAL
{
    public class CommonDA
    {
        #region Default
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
                //cmd.Parameters.Add(new SqlParameter("@UID", SqlDbType.Int).Value =Convert.ToInt32(objUserData.UserId));
                cmd.Parameters.Add(new SqlParameter("UID", Convert.ToInt32(objUserData.UserId)));



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
                        //fm.UserNameHindi = dt.Rows[i]["NameHindi"].ToString();
                        fm.UserEmail = dt.Rows[i]["EmailId"].ToString();
                        //fm.UserAddress = dt.Rows[i]["Office_Add"].ToString();
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
        #endregion
        internal String UpdateUserDetail(LoginModal objUserData)
        {
            string result = "";
            try
            {
                con.Open();
                cmd = new SqlCommand("proc_UpdateUserProfile", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@UID", SqlDbType.Int).Value = objUserData.UserId);
                cmd.Parameters.Add(new SqlParameter("@UserName", SqlDbType.VarChar).Value = filter_bad_chars_rep(objUserData.UserName));
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
        #region Gaurav
        public string InsertUpdateBrand(BrandMaster brand)
        {
            string str = "";
            con.Open();
            SqlTransaction tran = con.BeginTransaction();
            try
            {
                cmd = new SqlCommand("Proc_InsertUpdate_Brand", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = tran;
                cmd.Parameters.Add(new SqlParameter("dbName", UserSession.PushName));
                cmd.Parameters.Add(new SqlParameter("BrandId", brand.BrandId));
                cmd.Parameters.Add(new SqlParameter("BreweryId", brand.BreweryId));
                cmd.Parameters.Add(new SqlParameter("BrandName", brand.BrandName));
                cmd.Parameters.Add(new SqlParameter("BrandRegistrationNumber", brand.BrandRegistrationNumber));
                cmd.Parameters.Add(new SqlParameter("Strength", brand.Strength));
                cmd.Parameters.Add(new SqlParameter("AlcoholType", brand.AlcoholType));
                cmd.Parameters.Add(new SqlParameter("LiquorType", brand.LiquorType));
                cmd.Parameters.Add(new SqlParameter("LicenceType", brand.LicenceType));
                cmd.Parameters.Add(new SqlParameter("LicenceNo", brand.LicenceNo));
                cmd.Parameters.Add(new SqlParameter("MRP", brand.MRP));
                cmd.Parameters.Add(new SqlParameter("XFactoryPrice", brand.XFactoryPrice));
                cmd.Parameters.Add(new SqlParameter("AdditionalDuty", brand.AdditionalDuty));
                cmd.Parameters.Add(new SqlParameter("QuantityInCase", brand.QuantityInCase));
                cmd.Parameters.Add(new SqlParameter("QuantityInBottleML", brand.QuantityInBottleML));
                cmd.Parameters.Add(new SqlParameter("PackagingType", brand.PackagingType));
                cmd.Parameters.Add(new SqlParameter("ExciseDuty", brand.ExciseDuty));
                cmd.Parameters.Add(new SqlParameter("Remark", brand.Remark));
                cmd.Parameters.Add(new SqlParameter("StateId", brand.StateId));
                cmd.Parameters.Add(new SqlParameter("c_user_id", UserSession.LoggedInUserId));
                cmd.Parameters.Add(new SqlParameter("c_user_ip", IpAddress));
                cmd.Parameters.Add(new SqlParameter("c_mac", MacAddress));
                cmd.Parameters.Add(new SqlParameter("sp_type", brand.SPType));
                cmd.Parameters.Add(new SqlParameter("Msg", ""));
                cmd.Parameters["Msg"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters["Msg"].Size = 32676;
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
        public string UpdateBrand(string dbName, int BrandId, int TypeId)
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
                cmd.Parameters.Add(new SqlParameter("BrandId", BrandId));
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
        public DataSet GetBrandDetail(int BrandId, string LiquorType, string LicenceType, string LicenseNo, short BreweryId, short DistrictCode, int StateId, string Status)
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
                parameters.Add(new SqlParameter("StateId", StateId));
                parameters.Add(new SqlParameter("Status", Status));
                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, "PROC_GetBrand", parameters.ToArray());
            }
            catch
            {
                ds = null;
            }
            return ds;
        }
        public DataSet GetDutyCalculation(string Year, string LiquorType)
        {
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("YEAR", Year));
                parameters.Add(new SqlParameter("LiquorType", LiquorType));
                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, "PROC_GetDutyCalculation", parameters.ToArray());
            }
            catch (Exception)
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
                cmd.Parameters.Add(new SqlParameter("dbName", UserSession.PushName));
                cmd.Parameters.Add(new SqlParameter("PlanId", BP.PlanId));
                cmd.Parameters.Add(new SqlParameter("BrandId", BP.BrandId));
                cmd.Parameters.Add(new SqlParameter("DateOfPlan", BP.DateOfPlan));
                cmd.Parameters.Add(new SqlParameter("BatchNo", BP.BatchNo));
                cmd.Parameters.Add(new SqlParameter("NumberOfCases", BP.NumberOfCases));
                cmd.Parameters.Add(new SqlParameter("MappedOrNot", BP.MappedOrNot));
                cmd.Parameters.Add(new SqlParameter("IsPlanFinal", BP.IsPlanFinal));
                cmd.Parameters.Add(new SqlParameter("Type", BP.Type));
                cmd.Parameters.Add(new SqlParameter("c_user_id", UserSession.LoggedInUserId));
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
                cmd.Parameters.Add(new SqlParameter("dbName", UserSession.PushName));
                cmd.Parameters.Add(new SqlParameter("PlanId", BP.PlanId));
                cmd.Parameters.Add(new SqlParameter("ProducedNumberOfCases", BP.ProducedNumberOfCases));
                cmd.Parameters.Add(new SqlParameter("IsProductionFinal", BP.IsProductionFinal));
                cmd.Parameters.Add(new SqlParameter("Type", BP.Type));
                cmd.Parameters.Add(new SqlParameter("c_user_id_production", UserSession.LoggedInUserId));
                cmd.Parameters.Add(new SqlParameter("c_user_ip_production", IpAddress));
                cmd.Parameters.Add(new SqlParameter("c_mac_production", MacAddress));
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
        public DataSet GetBottelingPlanDetail(DateTime FromDate, DateTime ToDate, short BreweryId, int BrandId, string Mapped, string BatchNo, int PlanId, string Status)
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
                parameters.Add(new SqlParameter("BatchNo", BatchNo));
                parameters.Add(new SqlParameter("PlanId", PlanId));
                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, "PROC_GetBottelingPlanDetail", parameters.ToArray());
            }
            catch (Exception)
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
            catch (Exception)
            {
                ds = null;
            }
            return ds;
        }
        #endregion

        internal string GenerateQRCode(int PlanId, string UserId, string dbName)
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

        public DataSet GetGatePassReport()
        {
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("SpType", Convert.ToInt32(UserSession.LoggedInUserLevelId)));
                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, "Proc_GetGatePassForDistrictWholesaleToRetailor", parameters.ToArray());
            }
            catch (Exception)
            {
                ds = null;
            }
            return ds;
        }

        #endregion
        public void UploadCSV(string objdoc)
        {
            con.Open();

            try
            {
                //if (objdoc != null)
                //{
                //    if (objdoc.Rows.Count > 0)
                //    {
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Proc_Tbl_UploadProductionCSV";
                cmd.CommandTimeout = 3000;
                cmd.Parameters.Add(new SqlParameter("@tags", objdoc));
                cmd.Parameters.Add(new SqlParameter("@SpType", 1));
                //cmd.Parameters.Add(new SqlParameter("user_Id", @UserSession.LoggedInUser.UserName));
                //cmd.Parameters.Add(new SqlParameter("user_ip", this.IpAddress));
                //cmd.Parameters.Add(new SqlParameter("user_mac", this.MacAddress));
                //cmd.Parameters.Add(new SqlParameter("Msg", ""));

                cmd.ExecuteNonQuery();

                //    }
                //}




            }
            catch (Exception)
            {

            }
            finally
            {
                con.Close();
                con.Dispose();
            }

        }
        public string InsertUpdateUnitTank(UnitTank UT)
        {
            string result = "";
            con.Open();
            SqlTransaction tran = con.BeginTransaction();
            try
            {
                cmd = new SqlCommand("Proc_InsertUpdateUnitTank", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = tran;
                cmd.Parameters.Add(new SqlParameter("dbName", UserSession.PushName));
                cmd.Parameters.Add(new SqlParameter("UnitTankId", UT.UnitTankId));
                cmd.Parameters.Add(new SqlParameter("BreweryId", UT.BreweryId));
                cmd.Parameters.Add(new SqlParameter("UnitTankName", UT.UnitTankName));
                cmd.Parameters.Add(new SqlParameter("UnitTankCapacity", UT.UnitTankCapacity));
                cmd.Parameters.Add(new SqlParameter("UnitTankBulkLiter", UT.UnitTankBulkLiter));
                cmd.Parameters.Add(new SqlParameter("UnitTankStrength", UT.UnitTankStrength));
                cmd.Parameters.Add(new SqlParameter("Status", UT.Status));
                cmd.Parameters.Add(new SqlParameter("user_id", UserSession.LoggedInUserId));
                cmd.Parameters.Add(new SqlParameter("user_ip", IpAddress));
                cmd.Parameters.Add(new SqlParameter("mac", MacAddress));
                cmd.Parameters.Add(new SqlParameter("Type", UT.Type));
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
        public DataSet GetUnitTank(short BreweryId,short UnitTankId,string status)
        {
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("db_Name",UserSession.PushName));
                parameters.Add(new SqlParameter("BreweryId", BreweryId));
                parameters.Add(new SqlParameter("UnitTankId", UnitTankId));
                parameters.Add(new SqlParameter("status", status));
                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, "PROC_GetUnitTank", parameters.ToArray());
            }
            catch (Exception)
            {
                ds = null;
            }
            return ds;
        }
    }
}