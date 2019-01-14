using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using System.Web;
using UPExciseLTE.Models;

namespace UPExciseLTE.DAL
{
    public class CommonDA
    {
        #region Default
        SqlDataAdapter adap;
        SqlCommand cmd;
        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
        SqlConnection con = new SqlConnection(CommonConfig.Conn());

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
        internal DataSet ValidateCSV(int uploadValue, int planId, int brandId, DataTable dtCaseCode)
        {
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("db_Name", UserSession.PushName));
                parameters.Add(new SqlParameter("dbBarCode", dtCaseCode));
                parameters.Add(new SqlParameter("UploadValue", uploadValue));
                parameters.Add(new SqlParameter("PlanId", planId));
                parameters.Add(new SqlParameter("BrandId", brandId));
                parameters.Add(new SqlParameter("UserId", UserSession.LoggedInUserId));
                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, "PROC_ValidateCSV", parameters.ToArray());
            }
            catch (Exception exp)
            {
                ds = null;
            }
            return ds;
        }

        public string filter_bad_chars_rep(string s)
        {
            string[] sL_Char = {
            "onfocus",
            "\"\"",
            "=",
            "onmouseover",
            "prompt",
            "%27",
            "alert#",
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
        #region MenuMaster
        public List<MenuMst> GetMenuMaster(int MenuId, int ParentId, string Enable, string ShowToAll, string ShowInMenu)
        {
            List<MenuMst> lstMenuMst = new List<MenuMst>();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("MenuId", MenuId));
                parameters.Add(new SqlParameter("ParentId", ParentId));
                parameters.Add(new SqlParameter("Enable", Enable));
                parameters.Add(new SqlParameter("ShowToAll", ShowToAll));
                parameters.Add(new SqlParameter("ShowInMenu", ShowInMenu));
                SqlDataReader reader = SqlHelper.ExecuteReader(CommonConfig.Conn(), CommandType.StoredProcedure, "PROC_GetMenuMst", parameters.ToArray());
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        lstMenuMst.Add(new MenuMst
                        {
                            MenuId = int.Parse(reader["MenuId"].ToString()),
                            Text = reader["Text"].ToString(),
                            Description = reader["Description"].ToString(),
                            ParentId = int.Parse(reader["ParentId"].ToString()),
                            NavUrl = reader["NavUrl"].ToString(),
                            Enable = reader["Enable"].ToString(),
                            ShowToPost = reader["ShowToPost"].ToString(),
                            ShowToAll = reader["ShowToAll"].ToString(),
                            ShowInMenu = bool.Parse(reader["ShowInMenu"].ToString()),
                            CreatedBy = reader["CreatedBy"].ToString(),
                            CreatedOn1 = reader["CreatedOn"].ToString(),
                            OrderBy = int.Parse(reader["OrderBy"].ToString()),
                            Icon = reader["Icon"].ToString(),
                            yojana_code = short.Parse(reader["yojana_code"].ToString()),
                            IsPrint = bool.Parse(reader["IsPrint"].ToString())
                        });
                    }
                }
            }
            catch (Exception exp)
            {
                lstMenuMst = null;
            }
            return lstMenuMst;
        }
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
                cmd.Parameters.Add(new SqlParameter("BrandName", filter_bad_chars_rep(brand.BrandName.Trim())));
                cmd.Parameters.Add(new SqlParameter("BrandRegistrationNumber", brand.BrandRegistrationNumber));
                cmd.Parameters.Add(new SqlParameter("Strength", brand.Strength));
                cmd.Parameters.Add(new SqlParameter("AlcoholType", filter_bad_chars_rep(brand.AlcoholType.Trim())));
                cmd.Parameters.Add(new SqlParameter("LiquorType", filter_bad_chars_rep(brand.LiquorType.Trim())));
                cmd.Parameters.Add(new SqlParameter("LicenceType", filter_bad_chars_rep(brand.LicenceType.Trim())));
                cmd.Parameters.Add(new SqlParameter("LicenceNo", filter_bad_chars_rep(brand.LicenceNo.Trim())));
                cmd.Parameters.Add(new SqlParameter("XFactoryPrice", brand.XFactoryPrice));
                cmd.Parameters.Add(new SqlParameter("QuantityInCase", brand.QuantityInCase));
                cmd.Parameters.Add(new SqlParameter("QuantityInBottleML", brand.QuantityInBottleML));
                cmd.Parameters.Add(new SqlParameter("PackagingType", filter_bad_chars_rep(brand.PackagingType.Trim())));
                cmd.Parameters.Add(new SqlParameter("Remark", filter_bad_chars_rep(brand.Remark.Trim())));
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
        public string UpdateBrand(int BrandId, int TypeId, string Reason)
        {
            string str = "";
            con.Open();
            SqlTransaction tran = con.BeginTransaction();
            try
            {
                cmd = new SqlCommand("Proc_Update_Brand", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = tran;
                cmd.Parameters.Add(new SqlParameter("dbName", UserSession.PushName));
                cmd.Parameters.Add(new SqlParameter("BrandId", BrandId));
                cmd.Parameters.Add(new SqlParameter("TypeId", TypeId));
                cmd.Parameters.Add(new SqlParameter("Reason", filter_bad_chars_rep(Reason.Trim())));
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
                parameters.Add(new SqlParameter("DbName", UserSession.PushName));
                parameters.Add(new SqlParameter("BrandId", BrandId));
                parameters.Add(new SqlParameter("LiquorType", filter_bad_chars_rep(LiquorType.Trim())));
                parameters.Add(new SqlParameter("LicenceType", filter_bad_chars_rep(LicenceType.Trim())));
                parameters.Add(new SqlParameter("LicenseNo", filter_bad_chars_rep(LicenseNo.Trim())));
                parameters.Add(new SqlParameter("BreweryId", BreweryId));
                parameters.Add(new SqlParameter("DistrictCode", DistrictCode));
                parameters.Add(new SqlParameter("StateId", StateId));
                parameters.Add(new SqlParameter("Status", filter_bad_chars_rep(Status.Trim())));
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
                parameters.Add(new SqlParameter("LiquorType", filter_bad_chars_rep(LiquorType.Trim())));
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
                cmd.Parameters.Add(new SqlParameter("BBTId", BP.BBTId));
                cmd.Parameters.Add(new SqlParameter("BottlingLineId", BP.BottlingLineId));
                cmd.Parameters.Add(new SqlParameter("DateOfPlan", BP.DateOfPlan));
                cmd.Parameters.Add(new SqlParameter("BatchNo", filter_bad_chars_rep(BP.BatchNo.Trim())));
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
                cmd.Parameters.Add(new SqlParameter("AfterBBTBal", BP.AfterBBTBal));
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
                parameters.Add(new SqlParameter("dbName", UserSession.PushName));
                parameters.Add(new SqlParameter("FromDate", FromDate));
                parameters.Add(new SqlParameter("ToDate", ToDate));
                parameters.Add(new SqlParameter("BreweryId", BreweryId));
                parameters.Add(new SqlParameter("BrandId", BrandId));
                parameters.Add(new SqlParameter("Status", filter_bad_chars_rep(Status.Trim())));
                parameters.Add(new SqlParameter("Mapped", filter_bad_chars_rep(Mapped.Trim())));
                parameters.Add(new SqlParameter("BatchNo", filter_bad_chars_rep(BatchNo.Trim())));
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
                parameters.Add(new SqlParameter("dbName", UserSession.PushName));
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
                cmd.Parameters.Add(new SqlParameter("dbName", UserSession.PushName));
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
                parameters.Add(new SqlParameter("SpType", 1));
                parameters.Add(new SqlParameter("UserLevelId", Convert.ToInt32(UserSession.LoggedInUserLevelId)));
                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, "Proc_GetGatePassDetails", parameters.ToArray());
            }
            catch (Exception)
            {
                ds = null;
            }
            return ds;
        }



        #endregion
        public string UploadCSV(long gatePassId, DataTable dbBarCode, int UploadValue, int BrandId, string BatchNo, int PlanId, short BottlingLineId)
        {
            con.Open();
            string result = "";
            try
            {
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Proc_InsertUploadCSV";
                cmd.CommandTimeout = 3000;
                cmd.Parameters.Add(new SqlParameter("dbName", UserSession.PushName));
                cmd.Parameters.Add(new SqlParameter("GatePassId", gatePassId));
                cmd.Parameters.Add(new SqlParameter("dbBarCode", dbBarCode));
                cmd.Parameters.Add(new SqlParameter("UploadValue", UploadValue));
                cmd.Parameters.Add(new SqlParameter("BrandId", BrandId));
                cmd.Parameters.Add(new SqlParameter("BatchNo", BatchNo));
                cmd.Parameters.Add(new SqlParameter("PlanId", PlanId));
                cmd.Parameters.Add(new SqlParameter("BottlingLineId", BottlingLineId));
                cmd.Parameters.Add(new SqlParameter("user_id", UserSession.LoggedInUserId));
                cmd.Parameters.Add(new SqlParameter("user_ip", IpAddress));
                cmd.Parameters.Add(new SqlParameter("mac", MacAddress));
                cmd.Parameters.Add(new SqlParameter("Msg", ""));
                cmd.Parameters["Msg"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters["Msg"].Size = 256;
                cmd.ExecuteNonQuery();
                result = cmd.Parameters["Msg"].Value.ToString().Trim();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            return result;
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
                cmd.Parameters.Add(new SqlParameter("UnitTankName", filter_bad_chars_rep(UT.UnitTankName.Trim())));
                cmd.Parameters.Add(new SqlParameter("UnitTankCapacity", UT.UnitTankCapacity));
                cmd.Parameters.Add(new SqlParameter("UnitTankBulkLitre", UT.UnitTankBulkLitre));
                cmd.Parameters.Add(new SqlParameter("UnitTankStrength", UT.UnitTankStrength));
                cmd.Parameters.Add(new SqlParameter("Status", filter_bad_chars_rep(UT.Status.Trim())));
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
        public string InsertUpdateBottlingLine(BottlingLine RM)
        {
            string result = "";
            con.Open();
            SqlTransaction tran = con.BeginTransaction();
            try
            {
                cmd = new SqlCommand("Proc_InsertUpdateBottlingLine", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = tran;
                cmd.Parameters.Add(new SqlParameter("dbName", UserSession.PushName));
                cmd.Parameters.Add(new SqlParameter("UnitId", RM.UnitId));
                cmd.Parameters.Add(new SqlParameter("BBTId", RM.BBTId));
                cmd.Parameters.Add(new SqlParameter("BottlingLineId", RM.BottlingLineId));
                cmd.Parameters.Add(new SqlParameter("BottlingLineName", filter_bad_chars_rep(RM.BottlingLineName.Trim())));
                cmd.Parameters.Add(new SqlParameter("BottlingLineStatus", filter_bad_chars_rep(RM.BottlingLineStatus.Trim())));
                cmd.Parameters.Add(new SqlParameter("CapacityNoOfCasePerHour", (RM.CapacityNoOfCasePerHour)));
                cmd.Parameters.Add(new SqlParameter("LineType", (RM.LineType.Trim())));
                cmd.Parameters.Add(new SqlParameter("user_id", UserSession.LoggedInUserId));
                cmd.Parameters.Add(new SqlParameter("user_ip", IpAddress));
                cmd.Parameters.Add(new SqlParameter("mac", MacAddress));
                cmd.Parameters.Add(new SqlParameter("Type", RM.Type));
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

        public DataSet GetUnitTank(short BreweryId, short UnitTankId, string status)
        {
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("db_Name", UserSession.PushName));
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
        public DataSet GetBottlingLine(short BreweryId, int BBTId, int LineId, string status)
        {
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("db_Name", UserSession.PushName));
                parameters.Add(new SqlParameter("UnitId", BreweryId));
                parameters.Add(new SqlParameter("BBTId", BBTId));
                parameters.Add(new SqlParameter("LineId", LineId));
                parameters.Add(new SqlParameter("status", status));
                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, "proc_GetBottlingLine", parameters.ToArray());
            }
            catch (Exception)
            {
                ds = null;
            }
            return ds;
        }
        public string InsertUpdateBBT(BBTMaster bbtFormation)
        {

            con.Open();
            string str = "";
            SqlTransaction tran = con.BeginTransaction();
            try
            {
                cmd = new SqlCommand("Proc_InsertUpdateBBT", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = tran;
                cmd.Parameters.Add(new SqlParameter("dbName", UserSession.PushName));
                cmd.Parameters.Add(new SqlParameter("BBTId", bbtFormation.BBTId));
                cmd.Parameters.Add(new SqlParameter("UnitId", bbtFormation.UnitId));
                cmd.Parameters.Add(new SqlParameter("BBTName", filter_bad_chars_rep(bbtFormation.BBTName.Trim())));
                cmd.Parameters.Add(new SqlParameter("BBTCapacity", bbtFormation.BBTCapacity));
                cmd.Parameters.Add(new SqlParameter("BBTBulkLitre", bbtFormation.BBTBulkLitre));
                cmd.Parameters.Add(new SqlParameter("Status", filter_bad_chars_rep(bbtFormation.Status.Trim())));
                cmd.Parameters.Add(new SqlParameter("mac", MacAddress));
                cmd.Parameters.Add(new SqlParameter("user_id", UserSession.LoggedInUserId.ToString()));
                cmd.Parameters.Add(new SqlParameter("user_ip", IpAddress));
                cmd.Parameters.Add(new SqlParameter("Msg", ""));
                cmd.Parameters.Add(new SqlParameter("sp_Type", bbtFormation.SP_Type));
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
        public DataSet GetBBTMaster(int bbtId, string status)
        {
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("dbName", UserSession.PushName));
                parameters.Add(new SqlParameter("BBTId", bbtId));
                parameters.Add(new SqlParameter("UserId", UserSession.LoggedInUserId));
                parameters.Add(new SqlParameter("Status", status));
                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, "PROC_GetBBTMaster", parameters.ToArray());
            }
            catch (Exception)
            {
                ds = null;
            }
            return ds;
        }
        public string InsertUTTransferToBBT(UTTransferToBBT UTBL)
        {
            string result = "";
            con.Open();
            SqlTransaction tran = con.BeginTransaction();
            try
            {
                cmd = new SqlCommand("PROC_InsertUTTransferToBBT", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = tran;
                cmd.Parameters.Add(new SqlParameter("dbName", UserSession.PushName));
                cmd.Parameters.Add(new SqlParameter("IssuedFromUTId", UTBL.IssuedFromUTId));
                cmd.Parameters.Add(new SqlParameter("BBTId", UTBL.BBTID));
                cmd.Parameters.Add(new SqlParameter("TransactionType", filter_bad_chars_rep(UTBL.TransactionType.Trim())));
                cmd.Parameters.Add(new SqlParameter("IssueBL", UTBL.IssueBL));
                cmd.Parameters.Add(new SqlParameter("Wastage", UTBL.Wastage));
                cmd.Parameters.Add(new SqlParameter("NetTransfer", UTBL.NetTransfer));
                cmd.Parameters.Add(new SqlParameter("TransferDate", UTBL.TransferDate));
                cmd.Parameters.Add(new SqlParameter("Remark", filter_bad_chars_rep(UTBL.Remark.Trim())));
                cmd.Parameters.Add(new SqlParameter("BrandID", UTBL.BrandID));
                cmd.Parameters.Add(new SqlParameter("macId", MacAddress));
                cmd.Parameters.Add(new SqlParameter("user_id", UserSession.LoggedInUserId));
                cmd.Parameters.Add(new SqlParameter("user_ip", IpAddress));
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
        public DataSet GetUTTransferToBBT(DateTime FromDate, DateTime ToDate, int UnitTankId, string Status, short BreweryId, int BBTId)
        {
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("dbName", UserSession.PushName));
                parameters.Add(new SqlParameter("FromDate", FromDate));
                parameters.Add(new SqlParameter("ToDate", ToDate));
                parameters.Add(new SqlParameter("UnitTankId", UnitTankId));
                parameters.Add(new SqlParameter("BBTId", BBTId));
                parameters.Add(new SqlParameter("BreweryId", BreweryId));
                parameters.Add(new SqlParameter("Status", filter_bad_chars_rep(Status.Trim())));
                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, "PROC_GetUTTransferToBBT", parameters.ToArray());
            }
            catch (Exception)
            {
                ds = null;
            }
            return ds;
        }
        public string InsertUpdateGatePass(GatePass gatePass, List<DistrictWholeSaleToRetailorModel> districtWholeSaleToRetailorModels)
        {
            DataTable dt = new DataTable();

            if (districtWholeSaleToRetailorModels != null)
            {
                if (districtWholeSaleToRetailorModels.Count > 0)
                {
                    dt.Columns.Add("Brand", typeof(string));
                    dt.Columns.Add("BatchNo", typeof(string));
                    dt.Columns.Add("Size", typeof(int));
                    dt.Columns.Add("AvailableBottle", typeof(int));
                    dt.Columns.Add("AvailableBox", typeof(int));
                    dt.Columns.Add("DispatchBox", typeof(string));
                    dt.Columns.Add("DispatchBottle", typeof(int));
                    dt.Columns.Add("Duty", typeof(decimal));
                    dt.Columns.Add("AddDuty", typeof(decimal));
                    dt.Columns.Add("CalculatedDuty", typeof(decimal));
                    dt.Columns.Add("CalculatedAdditionalDuty", typeof(decimal));

                    for (int i = 0; i < districtWholeSaleToRetailorModels.Count; i++)
                    {
                        dt.Rows.Add();
                        dt.Rows[i]["Brand"] = districtWholeSaleToRetailorModels[i].Brand;
                        dt.Rows[i]["AddDuty"] = Convert.ToDecimal(districtWholeSaleToRetailorModels[i].AddDuty);
                        dt.Rows[i]["AvailableBottle"] = Convert.ToInt32(districtWholeSaleToRetailorModels[i].AvailableBottle);
                        dt.Rows[i]["AvailableBox"] = Convert.ToInt32(districtWholeSaleToRetailorModels[i].AvailableBox);
                        dt.Rows[i]["CalculatedAdditionalDuty"] = Convert.ToDecimal(districtWholeSaleToRetailorModels[i].CalculatedAdditionalDuty);
                        dt.Rows[i]["CalculatedDuty"] = Convert.ToDecimal(districtWholeSaleToRetailorModels[i].CalculatedDuty);
                        dt.Rows[i]["DispatchBottle"] = Convert.ToInt32(districtWholeSaleToRetailorModels[i].DispatchBottle);
                        dt.Rows[i]["Size"] = Convert.ToInt32(districtWholeSaleToRetailorModels[i].Size);
                        dt.Rows[i]["Duty"] = Convert.ToDecimal(districtWholeSaleToRetailorModels[i].Duty);
                        dt.Rows[i]["BatchNo"] = districtWholeSaleToRetailorModels[i].BatchNo;
                        dt.Rows[i]["DispatchBox"] = Convert.ToInt32(districtWholeSaleToRetailorModels[i].DispatchBox);
                    }
                }
            }


            con.Open();
            string str = "";
            SqlTransaction tran = con.BeginTransaction();
            try
            {
                cmd = new SqlCommand("Proc_InsertUpdateGatePass", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = tran;
                cmd.Parameters.Add(new SqlParameter("GatePassId", gatePass.GatePassId));
                cmd.Parameters.Add(new SqlParameter("FromDate", gatePass.FromDate));
                cmd.Parameters.Add(new SqlParameter("ToDate", gatePass.ToDate));
                cmd.Parameters.Add(new SqlParameter("To", gatePass.SelectedToRdo));
                cmd.Parameters.Add(new SqlParameter("From", gatePass.SelectedFromRdo));
                cmd.Parameters.Add(new SqlParameter("BrandId", gatePass.BrandId));
                cmd.Parameters.Add(new SqlParameter("ShopName", gatePass.ShopName));
                cmd.Parameters.Add(new SqlParameter("ShopId", gatePass.ShopId));
                cmd.Parameters.Add(new SqlParameter("GatePassNo", gatePass.GatePassNo));
                cmd.Parameters.Add(new SqlParameter("LicenseeNo", gatePass.LicenseeLicenseNo));
                cmd.Parameters.Add(new SqlParameter("VehicleNo", gatePass.VehicleNo));
                cmd.Parameters.Add(new SqlParameter("DriverName", gatePass.VehicleDriverName));
                cmd.Parameters.Add(new SqlParameter("LicenseeName", gatePass.LicenseeName));
                cmd.Parameters.Add(new SqlParameter("LicenseeAddress", gatePass.LicenseeAddress));
                cmd.Parameters.Add(new SqlParameter("AgencyNameAndAddress", gatePass.AgencyNameAndAddress));
                cmd.Parameters.Add(new SqlParameter("Address", gatePass.Address));
                cmd.Parameters.Add(new SqlParameter("GrossWeight", gatePass.GrossWeight));
                cmd.Parameters.Add(new SqlParameter("TareWeight", gatePass.TareWeight));
                cmd.Parameters.Add(new SqlParameter("MDistrictId1", gatePass.MDistrictId1));
                cmd.Parameters.Add(new SqlParameter("MDistrictId2", gatePass.MDistrictId2));
                cmd.Parameters.Add(new SqlParameter("MDistrictId3", gatePass.MDistrictId3));
                cmd.Parameters.Add(new SqlParameter("GatePassSource", gatePass.PassTypeInformation));
                cmd.Parameters.Add(new SqlParameter("GatePassSourceId", UserSession.LoggedInUserLevelId.ToString()));
                cmd.Parameters.Add(new SqlParameter("mac", MacAddress));
                cmd.Parameters.Add(new SqlParameter("user_id", UserSession.LoggedInUserId.ToString()));
                cmd.Parameters.Add(new SqlParameter("user_ip", IpAddress));
                cmd.Parameters.Add(new SqlParameter("Msg", ""));
                cmd.Parameters.Add(new SqlParameter("sp_Type", gatePass.SP_Type));
                if (dt.Rows.Count > 0)
                    cmd.Parameters.Add(new SqlParameter("tbl_GatePassBrandMapping", dt));
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


        public string InsertUpdateGatePass(BrewerytToManufacturerGatePass gatePass = null, List<DistrictWholeSaleToRetailorModel> districtWholeSaleToRetailorModels = null)
        {
            DataTable dt = new DataTable();

            if (districtWholeSaleToRetailorModels != null)
            {
                if (districtWholeSaleToRetailorModels.Count > 0)
                {
                    dt.Columns.Add("Brand", typeof(string));
                    dt.Columns.Add("BatchNo", typeof(string));
                    dt.Columns.Add("Size", typeof(int));
                    dt.Columns.Add("AvailableBottle", typeof(int));
                    dt.Columns.Add("AvailableBox", typeof(int));
                    dt.Columns.Add("DispatchBox", typeof(string));
                    dt.Columns.Add("DispatchBottle", typeof(int));
                    dt.Columns.Add("Duty", typeof(decimal));
                    dt.Columns.Add("AddDuty", typeof(decimal));
                    dt.Columns.Add("CalculatedDuty", typeof(decimal));
                    dt.Columns.Add("CalculatedAdditionalDuty", typeof(decimal));

                    for (int i = 0; i < districtWholeSaleToRetailorModels.Count; i++)
                    {
                        dt.Rows.Add();
                        dt.Rows[i]["Brand"] = districtWholeSaleToRetailorModels[i].Brand;
                        dt.Rows[i]["AddDuty"] = Convert.ToDecimal(districtWholeSaleToRetailorModels[i].AddDuty);
                        dt.Rows[i]["AvailableBottle"] = Convert.ToInt32(districtWholeSaleToRetailorModels[i].AvailableBottle);
                        dt.Rows[i]["AvailableBox"] = Convert.ToInt32(districtWholeSaleToRetailorModels[i].AvailableBox);
                        dt.Rows[i]["CalculatedAdditionalDuty"] = Convert.ToDecimal(districtWholeSaleToRetailorModels[i].CalculatedAdditionalDuty);
                        dt.Rows[i]["CalculatedDuty"] = Convert.ToDecimal(districtWholeSaleToRetailorModels[i].CalculatedDuty);
                        dt.Rows[i]["DispatchBottle"] = Convert.ToInt32(districtWholeSaleToRetailorModels[i].DispatchBottle);
                        dt.Rows[i]["Size"] = Convert.ToInt32(districtWholeSaleToRetailorModels[i].Size);
                        dt.Rows[i]["Duty"] = Convert.ToDecimal(districtWholeSaleToRetailorModels[i].Duty);
                        dt.Rows[i]["BatchNo"] = districtWholeSaleToRetailorModels[i].BatchNo;
                        dt.Rows[i]["DispatchBox"] = Convert.ToInt32(districtWholeSaleToRetailorModels[i].DispatchBox);
                    }
                }
            }
            con.Open();
            string str = "";
            SqlTransaction tran = con.BeginTransaction();
            try
            {
                cmd = new SqlCommand("Proc_InsertUpdateGatePass", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = tran;
                if (gatePass.GatePassId > 0 && dt.Rows.Count > 0)
                {
                    cmd.Parameters.Add(new SqlParameter("GatePassId", gatePass.GatePassId));
                    cmd.Parameters.Add(new SqlParameter("tbl_GatePassBrandMapping", dt));
                }
                if (gatePass.GatePassId > 0 && gatePass.BrandId > 0)
                {
                    cmd.Parameters.Add(new SqlParameter("GatePassId", gatePass.GatePassId));
                    cmd.Parameters.Add(new SqlParameter("BrandId", gatePass.BrandId));
                    cmd.Parameters.Add(new SqlParameter("UploadValue", gatePass.UploadValue));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("BrandId", null));
                    cmd.Parameters.Add(new SqlParameter("FromDate", gatePass.FromDate));
                    cmd.Parameters.Add(new SqlParameter("ToDate", gatePass.ToDate));
                    cmd.Parameters.Add(new SqlParameter("To", gatePass.To));
                    cmd.Parameters.Add(new SqlParameter("From", gatePass.From));
                    cmd.Parameters.Add(new SqlParameter("ShopName", gatePass.ShopName));
                    cmd.Parameters.Add(new SqlParameter("ShopId", gatePass.ShopId));
                    cmd.Parameters.Add(new SqlParameter("GatePassNo", gatePass.GatePassNo));
                    cmd.Parameters.Add(new SqlParameter("LicenseeNo", gatePass.ShopName));
                    cmd.Parameters.Add(new SqlParameter("VehicleNo", gatePass.VehicleNo));
                    cmd.Parameters.Add(new SqlParameter("DriverName", gatePass.VehicleDriverName));
                    cmd.Parameters.Add(new SqlParameter("LicenseeName", gatePass.LicenseeName));
                    cmd.Parameters.Add(new SqlParameter("LicenseeAddress", gatePass.LicenseeAddress));
                    cmd.Parameters.Add(new SqlParameter("AgencyNameAndAddress", gatePass.AgencyNameAndAddress));
                    cmd.Parameters.Add(new SqlParameter("Address", gatePass.Address));
                    cmd.Parameters.Add(new SqlParameter("GrossWeight", gatePass.GrossWeight));
                    cmd.Parameters.Add(new SqlParameter("TareWeight", gatePass.TareWeight));
                    cmd.Parameters.Add(new SqlParameter("MDistrictId1", gatePass.MDistrictId1));
                    cmd.Parameters.Add(new SqlParameter("MDistrictId2", gatePass.MDistrictId2));
                    cmd.Parameters.Add(new SqlParameter("MDistrictId3", gatePass.MDistrictId3));
                    cmd.Parameters.Add(new SqlParameter("GatePassSource", gatePass.PassTypeInformation));
                    cmd.Parameters.Add(new SqlParameter("GatePassSourceId", UserSession.LoggedInUserLevelId.ToString()));
                    cmd.Parameters.Add(new SqlParameter("RouteDetails", gatePass.RouteDetails));


                    cmd.Parameters.Add(new SqlParameter("ToLicenceTypeId", gatePass.ToLicenseTypeId));
                    cmd.Parameters.Add(new SqlParameter("FromLicenceTypeId", gatePass.FromLicenseTypeId));
                    cmd.Parameters.Add(new SqlParameter("ConsinorLicenseId", 4)); //Convert.ToInt64( gatePass.ConsinorLicenseNo)));
                    cmd.Parameters.Add(new SqlParameter("ConsineeLicenseId", 5));  ///Convert.ToInt64(gatePass.ConsineeLicenseNo)));


                    //cmd.Parameters.Add(new SqlParameter("tbl_GatePassBrandMapping", dt));
                }
                cmd.Parameters.Add(new SqlParameter("mac", MacAddress));
                cmd.Parameters.Add(new SqlParameter("user_id", UserSession.LoggedInUserId.ToString()));
                cmd.Parameters.Add(new SqlParameter("user_ip", IpAddress));
                cmd.Parameters.Add(new SqlParameter("Msg", ""));
                cmd.Parameters.Add(new SqlParameter("sp_Type", gatePass.SP_Type));

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



        public DataSet GetGatePassDetails(int spType, long GatePassId = 0, long brandId = 0)
        {
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("SpType", spType));
                parameters.Add(new SqlParameter("UserLevelId", Convert.ToInt32(UserSession.LoggedInUserLevelId)));
                if (GatePassId > 0)
                    parameters.Add(new SqlParameter("GatePassId", GatePassId));
                if (brandId > 0)
                    parameters.Add(new SqlParameter("BrandId", brandId));
                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, "Proc_GetGatePassDetails", parameters.ToArray());
            }
            catch (Exception ex)
            {
                ds = null;
            }
            return ds;
        }
        public string InsertUpdateGatePassDetails(GatePassDetails GP)
        {
            string result = "";
            con.Open();
            SqlTransaction tran = con.BeginTransaction();
            try
            {
                cmd = new SqlCommand("PROC_InsertUpdateGatePassDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = tran;
                cmd.Parameters.Add(new SqlParameter("dbName", UserSession.PushName));
                cmd.Parameters.Add(new SqlParameter("GatePassId", GP.GatePassId));
                cmd.Parameters.Add(new SqlParameter("FromDate", GP.FromDate));
                cmd.Parameters.Add(new SqlParameter("ToDate", GP.ToDate));
                cmd.Parameters.Add(new SqlParameter("ToLicenseType", filter_bad_chars_rep(GP.ToLicenseType.Trim())));
                cmd.Parameters.Add(new SqlParameter("ToLicenceNo", filter_bad_chars_rep(GP.ToLicenceNo.Trim())));
                cmd.Parameters.Add(new SqlParameter("ToConsigeeName", filter_bad_chars_rep(GP.ToConsigeeName.Trim())));
                cmd.Parameters.Add(new SqlParameter("FromLicenseType", filter_bad_chars_rep(GP.FromLicenseType.Trim())));
                cmd.Parameters.Add(new SqlParameter("FromLicenceNo", filter_bad_chars_rep(GP.FromLicenceNo.Trim())));
                cmd.Parameters.Add(new SqlParameter("FromConsignorName", filter_bad_chars_rep(GP.FromConsignorName.Trim())));
                cmd.Parameters.Add(new SqlParameter("ShopName", filter_bad_chars_rep(GP.ShopName.Trim())));
                cmd.Parameters.Add(new SqlParameter("ShopId", GP.ShopId));
                cmd.Parameters.Add(new SqlParameter("GatePassNo", filter_bad_chars_rep(GP.GatePassNo.Trim())));
                cmd.Parameters.Add(new SqlParameter("LicenseeNo", filter_bad_chars_rep(GP.LicenseeNo.Trim())));
                cmd.Parameters.Add(new SqlParameter("VehicleNo", filter_bad_chars_rep(GP.VehicleNo.Trim())));
                cmd.Parameters.Add(new SqlParameter("DriverName", filter_bad_chars_rep(GP.DriverName.Trim())));
                cmd.Parameters.Add(new SqlParameter("LicenseeName", filter_bad_chars_rep(GP.LicenseeName.Trim())));
                cmd.Parameters.Add(new SqlParameter("LicenseeAddress", filter_bad_chars_rep(GP.LicenseeAddress.Trim())));
                cmd.Parameters.Add(new SqlParameter("AgencyNameAndAddress", filter_bad_chars_rep(GP.AgencyNameAndAddress.Trim())));
                cmd.Parameters.Add(new SqlParameter("ToAddress", filter_bad_chars_rep(GP.ConsigneeAddress.Trim())));
                cmd.Parameters.Add(new SqlParameter("FromAddress", filter_bad_chars_rep(GP.ConsignorAddress.Trim())));
                cmd.Parameters.Add(new SqlParameter("GrossWeight", GP.GrossWeight));
                cmd.Parameters.Add(new SqlParameter("TareWeight", GP.TareWeight));
                cmd.Parameters.Add(new SqlParameter("NetWeight", GP.NetWeight));
                cmd.Parameters.Add(new SqlParameter("district_code_census1", GP.district_code_census1));
                cmd.Parameters.Add(new SqlParameter("district_code_census2", GP.district_code_census2));
                cmd.Parameters.Add(new SqlParameter("district_code_census3", GP.district_code_census3));
                cmd.Parameters.Add(new SqlParameter("RouteDetails", filter_bad_chars_rep(GP.RouteDetails.Trim())));
                cmd.Parameters.Add(new SqlParameter("GatePassSourceId", GP.GatePassSourceId));
                cmd.Parameters.Add(new SqlParameter("Receiver", filter_bad_chars_rep(GP.Receiver.Trim())));
                cmd.Parameters.Add(new SqlParameter("UploadValue", GP.UploadValue));
                cmd.Parameters.Add(new SqlParameter("Status", filter_bad_chars_rep(GP.Status.Trim())));
                cmd.Parameters.Add(new SqlParameter("user_id", UserSession.LoggedInUserId));
                cmd.Parameters.Add(new SqlParameter("user_ip", IpAddress));
                cmd.Parameters.Add(new SqlParameter("macId", MacAddress));
                cmd.Parameters.Add(new SqlParameter("SP_Type", GP.SP_Type));
                cmd.Parameters.Add(new SqlParameter("GatePassTypeID", GP.GatePassType));
                cmd.Parameters.Add(new SqlParameter("CheckPostVia", GP.CheckPostVia));
                cmd.Parameters.Add(new SqlParameter("InBondValue", GP.InBondValue));
                cmd.Parameters.Add(new SqlParameter("ExportDuty", GP.ExportDuty));
                cmd.Parameters.Add(new SqlParameter("DispatchedBy", GP.DispatchType));
                cmd.Parameters.Add(new SqlParameter("ImportPermitNo", GP.ImportPermitNo));
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
        public DataSet GetGatePassDetailsG(long GatePassId, DateTime FromDate, DateTime Todate, int UploadValue, string Status, string IsReceive)
        {
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("dbName", UserSession.PushName));
                parameters.Add(new SqlParameter("GatePassId", GatePassId));
                parameters.Add(new SqlParameter("UserId", Convert.ToInt32(UserSession.LoggedInUserId)));
                parameters.Add(new SqlParameter("FromDate", FromDate));
                parameters.Add(new SqlParameter("ToDate", Todate));
                parameters.Add(new SqlParameter("GatePassSourceId", Convert.ToInt32(UserSession.LoggedInUserLevelId)));
                parameters.Add(new SqlParameter("UploadValue", UploadValue));
                parameters.Add(new SqlParameter("Status", filter_bad_chars_rep(Status.Trim())));
                parameters.Add(new SqlParameter("IsReceive", filter_bad_chars_rep(IsReceive.Trim())));
                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, "PROC_GetGatePassDetailsG", parameters.ToArray());
            }
            catch (Exception)
            {
                ds = null;
            }
            return ds;
        }
        public DataSet GetGatePassUploadBrandDetails(long GatePassId)
        {
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("dbName", UserSession.PushName));
                parameters.Add(new SqlParameter("GatePassId", GatePassId));
                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, "PROC_GetGatePassUploadBrandDetails", parameters.ToArray());
            }
            catch (Exception)
            {
                ds = null;
            }
            return ds;
        }
        public string FinalGatePass(long GatePassId, short SP_Type, int DamageBottles)
        {
            string result = "";
            con.Open();
            SqlTransaction tran = con.BeginTransaction();
            try
            {
                cmd = new SqlCommand("PROC_UpdateFinalGatePass", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = tran;
                cmd.Parameters.Add(new SqlParameter("dbName", UserSession.PushName));
                cmd.Parameters.Add(new SqlParameter("GatePassId", GatePassId));
                cmd.Parameters.Add(new SqlParameter("SP_Type", SP_Type));
                cmd.Parameters.Add(new SqlParameter("DamageBottles", DamageBottles));
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
        public DataSet GetUnitDetails(short BreweryId, string BreweryName, string BreweryLicenseNo, short DistrictCode, short TehsilCode,int ParentUnitId,int UserId)
        {
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("dbName", UserSession.PushName));
                parameters.Add(new SqlParameter("BreweryId", BreweryId));
                parameters.Add(new SqlParameter("BreweryName", BreweryName));
                parameters.Add(new SqlParameter("BreweryLicenseNo", BreweryLicenseNo));
                parameters.Add(new SqlParameter("DistrictCode", DistrictCode));
                parameters.Add(new SqlParameter("TehsilCode", TehsilCode));
                parameters.Add(new SqlParameter("UserId",UserId));
                parameters.Add(new SqlParameter("ParentUnitId", ParentUnitId));
                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, "PROC_GetBrewery", parameters.ToArray());
            }
            catch (Exception)
            {
                ds = null;
            }
            return ds;
        }
        internal string InsertUpdateFormFL21(FormFL21 FL)
        {
            string result = "";
            string result1 = "";
            con.Open();
            int check = 0, FL21Id = -1, Count = 0;

            SqlTransaction tran = con.BeginTransaction();
            try
            {
                cmd = new SqlCommand("PROC_InsertUpdateFormFL21", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = tran;
                cmd.Parameters.Add(new SqlParameter("dbName", UserSession.PushName));
                cmd.Parameters.Add(new SqlParameter("SPType", FL.SPType));
                cmd.Parameters.Add(new SqlParameter("FL21Id", FL.FL21ID));
                cmd.Parameters.Add(new SqlParameter("BrandId", FL.BrandId));
                cmd.Parameters.Add(new SqlParameter("BoxSize", FL.BoxSize));
                cmd.Parameters.Add(new SqlParameter("Quantity", FL.QuantityInBottleML));
                cmd.Parameters.Add(new SqlParameter("ConsignerName", FL.FromConsignorName));
                cmd.Parameters.Add(new SqlParameter("ConsignerLicenseNo", FL.FromLicenceNo));
                cmd.Parameters.Add(new SqlParameter("ConsignerAddress", FL.FromConsignorAddress));
                cmd.Parameters.Add(new SqlParameter("ConsigneeName", FL.ToConsigeeName));
                cmd.Parameters.Add(new SqlParameter("ConsigneeLicenseNo", FL.ToLicenceNo));
                cmd.Parameters.Add(new SqlParameter("ConsigneeAddress", FL.ToConsigeeAddress));
                cmd.Parameters.Add(new SqlParameter("TotalCase", FL.TotalCase));
                cmd.Parameters.Add(new SqlParameter("TotalBottle", FL.TotalBottle));
                cmd.Parameters.Add(new SqlParameter("TotalBL", FL.TotalBL));
                cmd.Parameters.Add(new SqlParameter("DutyCalculated", FL.DutyCalculated));
                cmd.Parameters.Add(new SqlParameter("PermitFees", FL.PermitFees));
                cmd.Parameters.Add(new SqlParameter("TotalFees", FL.TotalFees));
                cmd.Parameters.Add(new SqlParameter("RateofPermit", FL.RateofPermit));
                cmd.Parameters.Add(new SqlParameter("TransactionNo", FL.TransactionNo));
                cmd.Parameters.Add(new SqlParameter("RouteDetails", FL.RouteDetails));
                cmd.Parameters.Add(new SqlParameter("FL21Status", FL.FL21Status));
                cmd.Parameters.Add(new SqlParameter("TransactionDate", FL.TransactionDate));
                cmd.Parameters.Add(new SqlParameter("FromPermitDate", FL.FromPermitDate));
                cmd.Parameters.Add(new SqlParameter("ToPermitDate", FL.ToPermitDate));
                cmd.Parameters.Add(new SqlParameter("Bankname", FL.Bankname));
                cmd.Parameters.Add(new SqlParameter("Reason", FL.Reason));
                cmd.Parameters.Add(new SqlParameter("UserId", UserSession.LoggedInUserId));
                cmd.Parameters.Add(new SqlParameter("IPAddress", IpAddress));
                cmd.Parameters.Add(new SqlParameter("Mac", MacAddress));
                cmd.Parameters.Add(new SqlParameter("Msg", ""));
                cmd.Parameters["Msg"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters["FL21Id"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters["Msg"].Size = 256;
                cmd.Parameters["FL21Id"].Size = 256;
                cmd.ExecuteNonQuery();
                result = cmd.Parameters["Msg"].Value.ToString().Trim();
                FL21Id = int.Parse(cmd.Parameters["FL21Id"].Value.ToString().Trim());
                cmd.Dispose();
                if (result.Contains("Successfully"))
                {
                    foreach (FL21BrandMapp FL21BM in FL.lstFL21)
                    {
                        Count++;
                        cmd = new SqlCommand("PROC_InsertFL21BrandMapp", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Transaction = tran;
                        cmd.Parameters.Add(new SqlParameter("DBName", UserSession.PushName));
                        cmd.Parameters.Add(new SqlParameter("FL21Id", FL21Id));
                        cmd.Parameters.Add(new SqlParameter("BrandId", FL21BM.BrandId));
                        cmd.Parameters.Add(new SqlParameter("BoxSize", FL21BM.BoxSize));
                        cmd.Parameters.Add(new SqlParameter("Quantity", FL21BM.Quantity));
                        cmd.Parameters.Add(new SqlParameter("TotalCase", FL21BM.TotalCase));
                        cmd.Parameters.Add(new SqlParameter("TotalBottle", FL21BM.TotalBottle));
                        cmd.Parameters.Add(new SqlParameter("TotalBL", FL21BM.TotalBL));
                        cmd.Parameters.Add(new SqlParameter("DutyCalculated", FL21BM.DutyCalculated));
                        cmd.Parameters.Add(new SqlParameter("PermitFees", FL21BM.PermitFees));
                        cmd.Parameters.Add(new SqlParameter("TotalFees", FL21BM.TotalFees));
                        cmd.Parameters.Add(new SqlParameter("RateofPermit", FL21BM.RateofPermit));
                        cmd.Parameters.Add(new SqlParameter("IsFirst", Count == 1 ? true : false));
                        cmd.Parameters.Add(new SqlParameter("Msg", ""));
                        cmd.Parameters["Msg"].Direction = ParameterDirection.InputOutput;
                        cmd.Parameters["Msg"].Size = 256;
                        cmd.ExecuteNonQuery();
                        result1 = cmd.Parameters["Msg"].Value.ToString().Trim();
                        if (!result1.Contains("Successfully"))
                        {
                            result = result1;
                            check++;
                            break;
                        }
                        cmd.Dispose();
                    }
                }
                if (check == 0)
                {
                    tran.Commit();
                }
                else
                {
                    tran.Rollback();
                }
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
        public DataSet GetFormFL21(int FormFL21Id, string FormFLStatus)
        {
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("dbName", UserSession.PushName));
                parameters.Add(new SqlParameter("FormFL21Id", FormFL21Id));
                parameters.Add(new SqlParameter("FormFLStatus", FormFLStatus));
                parameters.Add(new SqlParameter("UserId", Convert.ToInt32(UserSession.LoggedInUserId)));
                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, "SP_GetFormFL21", parameters.ToArray());
            }
            catch (Exception)
            {
                ds = null;
            }
            return ds;
        }
        public string InsertUpdateChallan(Challan Ch)
        {
            string result = "";
            con.Open();
            SqlTransaction tran = con.BeginTransaction();
            try
            {
                cmd = new SqlCommand("PROC_InsertUpdateChallan", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = tran;
                cmd.Parameters.Add(new SqlParameter("dbName", UserSession.PushName));
                cmd.Parameters.Add(new SqlParameter("ChallanId", Ch.ChallanId));
                cmd.Parameters.Add(new SqlParameter("BankName", Ch.BankName));
                cmd.Parameters.Add(new SqlParameter("ChallanPhoto", Ch.ChallanPhoto));
                cmd.Parameters.Add(new SqlParameter("FL21Ids", Ch.FL21Ids));
                cmd.Parameters.Add(new SqlParameter("TotalFees", Ch.TotalFees));
                cmd.Parameters.Add(new SqlParameter("TransactionDate", Ch.TransactionDate));
                cmd.Parameters.Add(new SqlParameter("FileExt", Ch.FileExt));
                cmd.Parameters.Add(new SqlParameter("TransactionNo", Ch.TransactionNo));
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
        public DataSet GetChallan(int ChallanId)
        {
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("dbName", UserSession.PushName));
                parameters.Add(new SqlParameter("ChallanId", ChallanId));
                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, "PROC_GetChallan", parameters.ToArray());
            }
            catch (Exception)
            {
                ds = null;
            }
            return ds;
        }




        #region FL2D


        public DataSet GetFormFL33(int FormFL33Id, string FormFLStatus)
        {
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("dbName", UserSession.PushName));
                parameters.Add(new SqlParameter("FormFL33Id", FormFL33Id));
                parameters.Add(new SqlParameter("FormFLStatus", FormFLStatus));
                parameters.Add(new SqlParameter("UserId", Convert.ToInt32(UserSession.LoggedInUserId)));
                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, "FL2D_proc_GetFormFL33", parameters.ToArray());
            }
            catch (Exception)
            {
                ds = null;
            }
            return ds;
        }



        public string InsertUpdateFormFL33(FormFL33 FL2D)
        {
            string result = "";
            string result1 = "";
            con.Open();
            int check = 0, FL33Id = -1, Count = 0;

            SqlTransaction tran = con.BeginTransaction();
            try
            {
                cmd = new SqlCommand("FL2D_proc_InsertUpdateFormFL33", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = tran;
                cmd.Parameters.Add(new SqlParameter("dbName", UserSession.PushName));
                cmd.Parameters.Add(new SqlParameter("SPType", FL2D.SPType));
                cmd.Parameters.Add(new SqlParameter("FL33Id", FL2D.FL33ID));
                cmd.Parameters.Add(new SqlParameter("BrandId", FL2D.BrandId));
                cmd.Parameters.Add(new SqlParameter("BoxSize", FL2D.BoxSize));
                cmd.Parameters.Add(new SqlParameter("Quantity", FL2D.QuantityInBottleML));
                cmd.Parameters.Add(new SqlParameter("ConsignerName", FL2D.FromConsignorName));
                cmd.Parameters.Add(new SqlParameter("ConsignerLicenseNo", FL2D.FromLicenceNo));
                cmd.Parameters.Add(new SqlParameter("ConsignerAddress", FL2D.FromConsignorAddress));
                cmd.Parameters.Add(new SqlParameter("ConsigneeName", FL2D.ToConsigeeName));
                cmd.Parameters.Add(new SqlParameter("ConsigneeLicenseNo", FL2D.ToLicenceNo));
                cmd.Parameters.Add(new SqlParameter("ConsigneeAddress", FL2D.ToConsigeeAddress));
                cmd.Parameters.Add(new SqlParameter("TotalCase", FL2D.TotalCase));
                cmd.Parameters.Add(new SqlParameter("TotalBottle", FL2D.TotalBottle));
                cmd.Parameters.Add(new SqlParameter("TotalBL", FL2D.TotalBL));
                cmd.Parameters.Add(new SqlParameter("DutyCalculated", FL2D.DutyCalculated));
                cmd.Parameters.Add(new SqlParameter("PermitFees", FL2D.PermitFees));
                cmd.Parameters.Add(new SqlParameter("TotalFees", FL2D.TotalFees));
                cmd.Parameters.Add(new SqlParameter("RateofPermit", FL2D.RateofPermit));
                cmd.Parameters.Add(new SqlParameter("TransactionNo", FL2D.TransactionNo));
                cmd.Parameters.Add(new SqlParameter("RouteDetails", FL2D.RouteDetails));
                cmd.Parameters.Add(new SqlParameter("FL33Status", FL2D.FL33Status));
                cmd.Parameters.Add(new SqlParameter("TransactionDate", FL2D.TransactionDate));
                cmd.Parameters.Add(new SqlParameter("FromPermitDate", FL2D.FromPermitDate));
                cmd.Parameters.Add(new SqlParameter("ToPermitDate", FL2D.ToPermitDate));
                cmd.Parameters.Add(new SqlParameter("Bankname", FL2D.Bankname));
                cmd.Parameters.Add(new SqlParameter("Reason", FL2D.Reason));
                cmd.Parameters.Add(new SqlParameter("UserId", UserSession.LoggedInUserId));
                cmd.Parameters.Add(new SqlParameter("IPAddress", IpAddress));
                cmd.Parameters.Add(new SqlParameter("Mac", MacAddress));
                cmd.Parameters.Add(new SqlParameter("Msg", ""));
                cmd.Parameters["Msg"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters["FL33Id"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters["Msg"].Size = 256;
                cmd.Parameters["FL33Id"].Size = 256;
                cmd.ExecuteNonQuery();
                result = cmd.Parameters["Msg"].Value.ToString().Trim();
                FL33Id = int.Parse(cmd.Parameters["FL33Id"].Value.ToString().Trim());
                cmd.Dispose();
                if (result.Contains("Successfully"))
                {
                    foreach (FL33BrandMapp FL33BM in FL2D.lstFL33)
                    {
                        Count++;
                        cmd = new SqlCommand("FL2D_proc_InsertFL33BrandMapp", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Transaction = tran;
                        cmd.Parameters.Add(new SqlParameter("DBName", UserSession.PushName));
                        cmd.Parameters.Add(new SqlParameter("FL33Id", FL33Id));
                        cmd.Parameters.Add(new SqlParameter("BrandId", FL33BM.BrandId));
                        cmd.Parameters.Add(new SqlParameter("BoxSize", FL33BM.BoxSize));
                        cmd.Parameters.Add(new SqlParameter("Quantity", FL33BM.Quantity));
                        cmd.Parameters.Add(new SqlParameter("TotalCase", FL33BM.TotalCase));
                        cmd.Parameters.Add(new SqlParameter("TotalBottle", FL33BM.TotalBottle));
                        cmd.Parameters.Add(new SqlParameter("TotalBL", FL33BM.TotalBL));
                        cmd.Parameters.Add(new SqlParameter("DutyCalculated", FL33BM.DutyCalculated));
                        cmd.Parameters.Add(new SqlParameter("PermitFees", FL33BM.PermitFees));
                        cmd.Parameters.Add(new SqlParameter("TotalFees", FL33BM.TotalFees));
                        cmd.Parameters.Add(new SqlParameter("RateofPermit", FL33BM.RateofPermit));
                        cmd.Parameters.Add(new SqlParameter("IsFirst", Count == 1 ? true : false));
                        cmd.Parameters.Add(new SqlParameter("Msg", ""));
                        cmd.Parameters["Msg"].Direction = ParameterDirection.InputOutput;
                        cmd.Parameters["Msg"].Size = 256;
                        cmd.ExecuteNonQuery();
                        result1 = cmd.Parameters["Msg"].Value.ToString().Trim();
                        if (!result1.Contains("Successfully"))
                        {
                            result = result1;
                            check++;
                            break;
                        }
                        cmd.Dispose();
                    }
                }
                if (check == 0)
                {
                    tran.Commit();
                }
                else
                {
                    tran.Rollback();
                }
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

        public string InsertUpdateChallanFL2D(FL2DChallan ChFL2D)
        {
            string result = "";
            con.Open();
            SqlTransaction tran = con.BeginTransaction();
            try
            {
                cmd = new SqlCommand("FL2D_proc_InsertUpdateChallan", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = tran;
                cmd.Parameters.Add(new SqlParameter("dbName", UserSession.PushName));
                cmd.Parameters.Add(new SqlParameter("ChallanId", ChFL2D.ChallanId));
                cmd.Parameters.Add(new SqlParameter("BankName", ChFL2D.BankName));
                cmd.Parameters.Add(new SqlParameter("ChallanPhoto", ChFL2D.ChallanPhoto));
                cmd.Parameters.Add(new SqlParameter("FL21Ids", ChFL2D.FL33Ids));
                cmd.Parameters.Add(new SqlParameter("TotalFees", ChFL2D.TotalFees));
                cmd.Parameters.Add(new SqlParameter("TransactionDate", ChFL2D.TransactionDate));
                cmd.Parameters.Add(new SqlParameter("FileExt", ChFL2D.FileExt));
                cmd.Parameters.Add(new SqlParameter("TransactionNo", ChFL2D.TransactionNo));
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


        #endregion


        #region CL DAL
        public string InsertUpdateStorageVAT(StorageVATCL SV)
        {
            string result = "";
            con.Open();
            SqlTransaction tran = con.BeginTransaction();
            try
            {
                cmd = new SqlCommand("CL_InsertUpdateStorageVAT", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = tran;
                cmd.Parameters.Add(new SqlParameter("dbName", UserSession.PushName));
                cmd.Parameters.Add(new SqlParameter("StorageVATId", SV.StorageVATId));
                cmd.Parameters.Add(new SqlParameter("UnitId", SV.UnitId));
                cmd.Parameters.Add(new SqlParameter("StorageVATName", filter_bad_chars_rep(SV.StorageVATName.Trim())));
                cmd.Parameters.Add(new SqlParameter("StorageVATCapacity", SV.StorageVATCapacity));
                cmd.Parameters.Add(new SqlParameter("StorageVATBulkLitre", SV.StorageVATBulkLitre));
                cmd.Parameters.Add(new SqlParameter("StorageVATStrength", SV.StorageVATStrength));
                cmd.Parameters.Add(new SqlParameter("Status", filter_bad_chars_rep(SV.Status.Trim())));
                cmd.Parameters.Add(new SqlParameter("user_id", UserSession.LoggedInUserId));
                cmd.Parameters.Add(new SqlParameter("user_ip", IpAddress));
                cmd.Parameters.Add(new SqlParameter("mac", MacAddress));
                cmd.Parameters.Add(new SqlParameter("Type", SV.Type));
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

        public DataSet GetStorageVAT(short BreweryId, short StorageVATId, string status)
        {
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("db_Name", UserSession.PushName));
                parameters.Add(new SqlParameter("UnitID", BreweryId));
                parameters.Add(new SqlParameter("StorageVATId", StorageVATId));
                parameters.Add(new SqlParameter("status", status));
                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, "CL_PROC_GetStorageVAT", parameters.ToArray());
            }
            catch (Exception)
            {
                ds = null;
            }
            return ds;
        }
        public DataSet GetBlendingVAT(short Unitid, short BlendingVATId, string status)
        {
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("db_Name", UserSession.PushName));
                parameters.Add(new SqlParameter("Unitid", Unitid));
                parameters.Add(new SqlParameter("BlendingVATId", BlendingVATId));
                parameters.Add(new SqlParameter("status", status));
                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, "CL_PROC_GetBlendingVAT", parameters.ToArray());
            }
            catch (Exception)
            {
                ds = null;
            }
            return ds;
        }
        public string InsertUpdateBlendingVAT(BlendingVATCL SV)
        {
            string result = "";
            con.Open();
            SqlTransaction tran = con.BeginTransaction();
            try
            {
                cmd = new SqlCommand("CL_InsertUpdateBlendingVAT", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = tran;
                cmd.Parameters.Add(new SqlParameter("dbName", UserSession.PushName));
                cmd.Parameters.Add(new SqlParameter("BlendingVATId", SV.BlendingVATId));
                cmd.Parameters.Add(new SqlParameter("UnitId", SV.UnitId));
                cmd.Parameters.Add(new SqlParameter("BlendingVATName", filter_bad_chars_rep(SV.BlendingVATName.Trim())));
                cmd.Parameters.Add(new SqlParameter("BlendingVATCapacity", SV.BlendingVATCapacity));
                cmd.Parameters.Add(new SqlParameter("BlendingVATBulkLitre", SV.BlendingVATBulkLitre));
                cmd.Parameters.Add(new SqlParameter("BlendingVATStrength", SV.BlendingVATStrength));
                cmd.Parameters.Add(new SqlParameter("Status", filter_bad_chars_rep(SV.Status.Trim())));
                cmd.Parameters.Add(new SqlParameter("user_id", UserSession.LoggedInUserId));
                cmd.Parameters.Add(new SqlParameter("user_ip", IpAddress));
                cmd.Parameters.Add(new SqlParameter("mac", MacAddress));
                cmd.Parameters.Add(new SqlParameter("Type", SV.Type));
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
        public string InsertUpdateBottelingVAT(BottelingVATCL SV)
        {
            string result = "";
            con.Open();
            SqlTransaction tran = con.BeginTransaction();
            try
            {
                cmd = new SqlCommand("CL_InsertUpdateBottelingVAT", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = tran;
                cmd.Parameters.Add(new SqlParameter("dbName", UserSession.PushName));
                cmd.Parameters.Add(new SqlParameter("BottelingVATId", SV.BottelingVATId));
                cmd.Parameters.Add(new SqlParameter("UnitId", SV.UnitId));
                cmd.Parameters.Add(new SqlParameter("BottelingVATName", filter_bad_chars_rep(SV.BottelingVATName.Trim())));
                cmd.Parameters.Add(new SqlParameter("BottelingVATCapacity", SV.BottelingVATCapacity));
                cmd.Parameters.Add(new SqlParameter("BottelingVATBulkLitre", SV.BottelingVATBulkLitre));
                cmd.Parameters.Add(new SqlParameter("BottelingVATStrength", SV.BottelingVATStrength));
                cmd.Parameters.Add(new SqlParameter("Status", filter_bad_chars_rep(SV.Status.Trim())));
                cmd.Parameters.Add(new SqlParameter("user_id", UserSession.LoggedInUserId));
                cmd.Parameters.Add(new SqlParameter("user_ip", IpAddress));
                cmd.Parameters.Add(new SqlParameter("mac", MacAddress));
                cmd.Parameters.Add(new SqlParameter("Type", SV.Type));
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
        public DataSet GetBottelingVATDetails(short UnitId, short BottelingVATId, string status)
        {
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("db_Name", UserSession.PushName));
                parameters.Add(new SqlParameter("UnitId", UnitId));
                parameters.Add(new SqlParameter("BottelingVATId", BottelingVATId));
                parameters.Add(new SqlParameter("status", status));
                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, "CL_PROC_GetBottelingVAT", parameters.ToArray());
            }
            catch (Exception)
            {
                ds = null;
            }
            return ds;
        }
        public string InsertUpdateBottlingLineCL(BottlingLineCL RM)
        {
            string result = "";
            con.Open();
            SqlTransaction tran = con.BeginTransaction();
            try
            {
                cmd = new SqlCommand("CL_Proc_InsertUpdateBottlingLine", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = tran;
                cmd.Parameters.Add(new SqlParameter("dbName", UserSession.PushName));
                cmd.Parameters.Add(new SqlParameter("UnitId", RM.UnitId));
                cmd.Parameters.Add(new SqlParameter("BBTId", RM.BBTId));
                cmd.Parameters.Add(new SqlParameter("BottlingLineId", RM.BottlingLineId));
                cmd.Parameters.Add(new SqlParameter("BottlingLineName", filter_bad_chars_rep(RM.BottlingLineName.Trim())));
                cmd.Parameters.Add(new SqlParameter("BottlingLineStatus", filter_bad_chars_rep(RM.BottlingLineStatus.Trim())));
                cmd.Parameters.Add(new SqlParameter("CapacityNoOfCasePerHour", (RM.CapacityNoOfCasePerHour)));
                cmd.Parameters.Add(new SqlParameter("LineType", (RM.LineType.Trim())));
                cmd.Parameters.Add(new SqlParameter("user_id", UserSession.LoggedInUserId));
                cmd.Parameters.Add(new SqlParameter("user_ip", IpAddress));
                cmd.Parameters.Add(new SqlParameter("mac", MacAddress));
                cmd.Parameters.Add(new SqlParameter("Type", RM.Type));
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
        public string InsertTankTransferDetail(TankTransferDetail TTD)
        {
            string result = "";
            con.Open();
            SqlTransaction tran = con.BeginTransaction();
            try
            {
                cmd = new SqlCommand("CL_proc_InsertTankTransferDetail", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = tran;
                cmd.Parameters.Add(new SqlParameter("dbName", UserSession.PushName));
                cmd.Parameters.Add(new SqlParameter("TransferId", TTD.TransferId));
                cmd.Parameters.Add(new SqlParameter("IssuedFromSVId", TTD.IssuedFromSVId));
                cmd.Parameters.Add(new SqlParameter("IssuedFromBlendingId", TTD.IssuedFromBlendingId));
                cmd.Parameters.Add(new SqlParameter("BottlingVATId", TTD.BottlingVATId));
                cmd.Parameters.Add(new SqlParameter("SpiritTypeId", TTD.SpiritTypeId));
                cmd.Parameters.Add(new SqlParameter("BrandID", TTD.BrandID));
                cmd.Parameters.Add(new SqlParameter("TransactionType", TTD.TransactionType));
                cmd.Parameters.Add(new SqlParameter("SourceSV", TTD.SourceSV));
                cmd.Parameters.Add(new SqlParameter("PrevBalanceSV", TTD.PrevBalanceSV));
                cmd.Parameters.Add(new SqlParameter("PrevBalanceBlendingV", TTD.PrevBalanceBlendingV));
                cmd.Parameters.Add(new SqlParameter("PrevBalanceBottlingV", TTD.PrevBalanceBottlingV));
                cmd.Parameters.Add(new SqlParameter("IssueBL", TTD.IssueBL));
                cmd.Parameters.Add(new SqlParameter("CurrentBalanceSV", TTD.CurrentBalanceSV));
                cmd.Parameters.Add(new SqlParameter("CurrentBalanceBledingV", TTD.CurrentBalanceBledingV));
                cmd.Parameters.Add(new SqlParameter("CurrentBalanceBottlingV", TTD.CurrentBalanceBottlingV));
                cmd.Parameters.Add(new SqlParameter("PrevALSV", TTD.PrevALSV));
                cmd.Parameters.Add(new SqlParameter("PrevALBlendingV", TTD.PrevALBlendingV));
                cmd.Parameters.Add(new SqlParameter("PrevALBottlingV", TTD.PrevALBottlingV));
                cmd.Parameters.Add(new SqlParameter("IssueAL", TTD.IssueAL));
                cmd.Parameters.Add(new SqlParameter("CurrentALSV", TTD.CurrentALSV));
                cmd.Parameters.Add(new SqlParameter("CurrentALBledingV", TTD.CurrentALBledingV));
                cmd.Parameters.Add(new SqlParameter("CurrentALBottlingV", TTD.CurrentALBottlingV));
                cmd.Parameters.Add(new SqlParameter("dipForm", TTD.dipForm));
                cmd.Parameters.Add(new SqlParameter("dipTo", TTD.dipTo));
                cmd.Parameters.Add(new SqlParameter("Wastage", TTD.Wastage));
                cmd.Parameters.Add(new SqlParameter("TransferDate", TTD.TransferDate));
                cmd.Parameters.Add(new SqlParameter("IsFinal", TTD.IsFinal));
                cmd.Parameters.Add(new SqlParameter("Dip", TTD.Dip));
                cmd.Parameters.Add(new SqlParameter("Temperature", TTD.Temperature));
                cmd.Parameters.Add(new SqlParameter("Indication", TTD.Indication));
                cmd.Parameters.Add(new SqlParameter("Remark", filter_bad_chars_rep(TTD.Remark.Trim())));
                cmd.Parameters.Add(new SqlParameter("c_mac", MacAddress));
                cmd.Parameters.Add(new SqlParameter("c_user_id", UserSession.LoggedInUserId));
                cmd.Parameters.Add(new SqlParameter("c_user_ip", IpAddress));
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
        public DataSet GetSVTransferToBV(DateTime FromDate, DateTime ToDate, int UnitTankId, string Status, short BreweryId, int BBTId)
        {
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("dbName", UserSession.PushName));
                parameters.Add(new SqlParameter("FromDate", FromDate));
                parameters.Add(new SqlParameter("ToDate", ToDate));
                parameters.Add(new SqlParameter("UnitTankId", UnitTankId));
                parameters.Add(new SqlParameter("BBTId", BBTId));
                parameters.Add(new SqlParameter("BreweryId", BreweryId));
                parameters.Add(new SqlParameter("Status", filter_bad_chars_rep(Status.Trim())));
                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, "PROC_GetUTTransferToBBT", parameters.ToArray());
            }
            catch (Exception)
            {
                ds = null;
            }
            return ds;
        }
        public DataSet GetBottelingPlanDetailCL(DateTime FromDate, DateTime ToDate, short BreweryId, int BrandId, string Mapped, string BatchNo, int PlanId, string Status)
        {
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("dbName", UserSession.PushName));
                parameters.Add(new SqlParameter("FromDate", FromDate));
                parameters.Add(new SqlParameter("ToDate", ToDate));
                parameters.Add(new SqlParameter("BreweryId", BreweryId));
                parameters.Add(new SqlParameter("BrandId", BrandId));
                parameters.Add(new SqlParameter("Status", filter_bad_chars_rep(Status.Trim())));
                parameters.Add(new SqlParameter("Mapped", filter_bad_chars_rep(Mapped.Trim())));
                parameters.Add(new SqlParameter("BatchNo", filter_bad_chars_rep(BatchNo.Trim())));
                parameters.Add(new SqlParameter("PlanId", PlanId));
                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, "CL_proc_GetBottelingPlanDetail", parameters.ToArray());
            }
            catch (Exception)
            {
                ds = null;
            }
            return ds;
        }
        internal string InsertUpdatePlanCL(BottelingPlanCL BP)
        {
            string result = "";
            con.Open();
            SqlTransaction tran = con.BeginTransaction();
            try
            {
                cmd = new SqlCommand("CL_proc_InsertUpdatePlan", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = tran;
                cmd.Parameters.Add(new SqlParameter("dbName", UserSession.PushName));
                cmd.Parameters.Add(new SqlParameter("PlanId", BP.PlanId));
                cmd.Parameters.Add(new SqlParameter("BrandId", BP.BrandId));
                cmd.Parameters.Add(new SqlParameter("BottelingVATId", BP.BVId));
                cmd.Parameters.Add(new SqlParameter("BottlingLineId", BP.BottlingLineId));
                cmd.Parameters.Add(new SqlParameter("DateOfPlan", BP.DateOfPlan));
                cmd.Parameters.Add(new SqlParameter("BatchNo", filter_bad_chars_rep(BP.BatchNo.Trim())));
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
        internal string GenerateQRCodeCL(int PlanId, string UserId, string dbName)
        {
            string result = "";
            con.Open();
            SqlTransaction tran = con.BeginTransaction();
            try
            {
                cmd = new SqlCommand("CL_proc_GenerateQRCode", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = tran;
                cmd.Parameters.Add(new SqlParameter("dbName", UserSession.PushName));
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
        public DataSet GetQRCOdeCL(int PlanId)
        {
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("dbName", UserSession.PushName));
                parameters.Add(new SqlParameter("PlanId", PlanId));
                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, "CL_proc_GetQRCOde", parameters.ToArray());
            }
            catch (Exception)
            {
                ds = null;
            }
            return ds;
        }
        internal string InsertUpdateProductionPlanCL(BottelingPlanCL BP)
        {
            string result = "";
            con.Open();
            SqlTransaction tran = con.BeginTransaction();
            try
            {
                cmd = new SqlCommand("CL_proc_InsertUpdateProductionPlan", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = tran;
                cmd.Parameters.Add(new SqlParameter("dbName", UserSession.PushName));
                cmd.Parameters.Add(new SqlParameter("PlanId", BP.PlanId));
                cmd.Parameters.Add(new SqlParameter("ProducedNumberOfCases", BP.ProducedNumberOfCases));
                cmd.Parameters.Add(new SqlParameter("IsProductionFinal", BP.IsProductionFinal));
                cmd.Parameters.Add(new SqlParameter("Type", BP.Type));
                cmd.Parameters.Add(new SqlParameter("AfterBVBal", BP.AfterBVBal));
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
        internal DataSet ValidateCSVCL(int uploadValue, int planId, int brandId, DataTable dtCaseCode)
        {
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("db_Name", UserSession.PushName));
                parameters.Add(new SqlParameter("dbBarCode", dtCaseCode));
                parameters.Add(new SqlParameter("UploadValue", uploadValue));
                parameters.Add(new SqlParameter("PlanId", planId));
                parameters.Add(new SqlParameter("BrandId", brandId));
                parameters.Add(new SqlParameter("UserId", UserSession.LoggedInUserId));
                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, "CL_proc_ValidateCSV", parameters.ToArray());
            }
            catch (Exception exp)
            {
                ds = null;
            }
            return ds;
        }
        public DataSet GetBottlingLineCL(short BreweryId, int BBTId, int LineId, string status)
        {
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("db_Name", UserSession.PushName));
                parameters.Add(new SqlParameter("UnitId", BreweryId));
                parameters.Add(new SqlParameter("BBTId", BBTId));
                parameters.Add(new SqlParameter("LineId", LineId));
                parameters.Add(new SqlParameter("status", status));
                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, "CL_proc_GetBottlingLine", parameters.ToArray());
            }
            catch (Exception)
            {
                ds = null;
            }
            return ds;
        }
        #endregion CL DAL
        #region UnitMaster
        public string InsertUpdateUnitMaster(UnitMaster objUnitMaster)
        {
            string str = "";
            con.Open();
            SqlTransaction tran = con.BeginTransaction();
            try
            {
                cmd = new SqlCommand("PROC_InsertUpdateUnit", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = tran;
                cmd.Parameters.Add(new SqlParameter("UnitId", objUnitMaster.UnitId));
                cmd.Parameters.Add(new SqlParameter("StateId", objUnitMaster.StateCode));
                cmd.Parameters.Add(new SqlParameter("DistrictCode", objUnitMaster.DistrictCode));
                cmd.Parameters.Add(new SqlParameter("ProductionLiquorType", objUnitMaster.ProductionLiquorType));
                cmd.Parameters.Add(new SqlParameter("UnitType", objUnitMaster.UnitType));
                cmd.Parameters.Add(new SqlParameter("UnitLicenseType", objUnitMaster.LicenseType));
                cmd.Parameters.Add(new SqlParameter("UnitName", objUnitMaster.UnitName));
                cmd.Parameters.Add(new SqlParameter("UnitLicenseNo", objUnitMaster.LicenseNo));
                cmd.Parameters.Add(new SqlParameter("UserLevel", objUnitMaster.UserLevel));
                cmd.Parameters.Add(new SqlParameter("ValidityOfLicense", objUnitMaster.ValidityOfLicense1));
                cmd.Parameters.Add(new SqlParameter("UnitAddress", objUnitMaster.UnitAddress));
                cmd.Parameters.Add(new SqlParameter("OwnerName", objUnitMaster.OwnerName));
                cmd.Parameters.Add(new SqlParameter("UnitContactPerson", objUnitMaster.ContactPersonName));
                cmd.Parameters.Add(new SqlParameter("UnitContactPersonMobile", objUnitMaster.Mobile));
                cmd.Parameters.Add(new SqlParameter("UnitCapacity", objUnitMaster.UnitCapacity));
                cmd.Parameters.Add(new SqlParameter("UnitPhone", objUnitMaster.Mobile));
                cmd.Parameters.Add(new SqlParameter("UnitFax", objUnitMaster.UnitFax));
                cmd.Parameters.Add(new SqlParameter("UnitEmail", objUnitMaster.Email));
                cmd.Parameters.Add(new SqlParameter("ApproverUserID", UserSession.LoggedInUserId));
                cmd.Parameters.Add(new SqlParameter("Remark", objUnitMaster.Remark));
                cmd.Parameters.Add(new SqlParameter("UserId", UserSession.LoggedInUserId));
                cmd.Parameters.Add(new SqlParameter("ParentUnitId", objUnitMaster.ParentUnitId));
                cmd.Parameters.Add(new SqlParameter("DECApprovalStatus", objUnitMaster.DECApprovalStatus));
                cmd.Parameters.Add(new SqlParameter("Reason", objUnitMaster.Reason));
                cmd.Parameters.Add(new SqlParameter("LicensePhoto", objUnitMaster.LicensePhoto));
                cmd.Parameters.Add(new SqlParameter("dbName", UserSession.PushName));
                cmd.Parameters.Add(new SqlParameter("SP_Type", objUnitMaster.SPType));
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
        public DataSet GetUnitMasterDetail(int UnitId, string UnitName, string UnitLicenseNo, int StateId, int DistrictCode, int TehsilCode, int UserId, string DECApprovalStatus, int ParentUnitId, string UnitLicenseType)
        {
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("DbName", UserSession.PushName));
                parameters.Add(new SqlParameter("UnitId", UnitId));
                parameters.Add(new SqlParameter("UnitName", UnitName));
                parameters.Add(new SqlParameter("UnitLicenseNo", UnitLicenseNo));
                parameters.Add(new SqlParameter("StateId", StateId));
                parameters.Add(new SqlParameter("DistrictCode", DistrictCode));
                parameters.Add(new SqlParameter("TehsilCode", TehsilCode));
                parameters.Add(new SqlParameter("UserId", UserId));
                parameters.Add(new SqlParameter("DECApprovalStatus", DECApprovalStatus));
                parameters.Add(new SqlParameter("ParentUnitId", ParentUnitId));
                parameters.Add(new SqlParameter("UnitLicenseType", UnitLicenseType));

                ds = SqlHelper.ExecuteDataset(CommonConfig.Conn(), CommandType.StoredProcedure, "PROC_GetUnitMaster", parameters.ToArray());
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region BWFL
        internal string InsertUpdatePlanBWFL(BottelingPlan BP)
        {
            string result = "";
            con.Open();
            SqlTransaction tran = con.BeginTransaction();
            try
            {
                cmd = new SqlCommand("BWFL_Proc_InsertUpdatePlan", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = tran;
                cmd.Parameters.Add(new SqlParameter("dbName", UserSession.PushName));
                cmd.Parameters.Add(new SqlParameter("PlanId", BP.PlanId));
                cmd.Parameters.Add(new SqlParameter("BrandId", BP.BrandId));
                cmd.Parameters.Add(new SqlParameter("BBTId", BP.BBTId));
                cmd.Parameters.Add(new SqlParameter("BottlingLineId", BP.BottlingLineId));
                cmd.Parameters.Add(new SqlParameter("DateOfPlan", BP.DateOfPlan));
                cmd.Parameters.Add(new SqlParameter("BatchNo", filter_bad_chars_rep(BP.BatchNo.Trim())));
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
        #endregion
    }
}