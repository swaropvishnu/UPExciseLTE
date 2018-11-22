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
using System.Text;

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
            catch(Exception exp)
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
                cmd.Parameters.Add(new SqlParameter("BrandName", brand.BrandName));
                cmd.Parameters.Add(new SqlParameter("BrandRegistrationNumber", brand.BrandRegistrationNumber));
                cmd.Parameters.Add(new SqlParameter("Strength", brand.Strength));
                cmd.Parameters.Add(new SqlParameter("AlcoholType", brand.AlcoholType));
                cmd.Parameters.Add(new SqlParameter("LiquorType", brand.LiquorType));
                cmd.Parameters.Add(new SqlParameter("LicenceType", brand.LicenceType));
                cmd.Parameters.Add(new SqlParameter("LicenceNo", brand.LicenceNo));
                cmd.Parameters.Add(new SqlParameter("XFactoryPrice", brand.XFactoryPrice));
                cmd.Parameters.Add(new SqlParameter("QuantityInCase", brand.QuantityInCase));
                cmd.Parameters.Add(new SqlParameter("QuantityInBottleML", brand.QuantityInBottleML));
                cmd.Parameters.Add(new SqlParameter("PackagingType", brand.PackagingType));
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
        public string UpdateBrand(int BrandId, int TypeId,string Reason)
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
                cmd.Parameters.Add(new SqlParameter("Reason", Reason));
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
                cmd.Parameters.Add(new SqlParameter("BBTId", BP.BBTId));
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
        public string UploadCSV(long gatePassId,DataTable dbBarCode,int UploadValue)
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


        public DataSet GetUnitTank(short BreweryId, short UnitTankId, string status)
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

        public string InsertUpdateBBT(BBTFormation bbtFormation)
        {

            con.Open();
            string str = "";
            SqlTransaction tran = con.BeginTransaction();
            try
            {
                cmd = new SqlCommand("Proc_InsertUpdateBBT", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = tran;
                cmd.Parameters.Add(new SqlParameter("BBTId", bbtFormation.BBTId));
                cmd.Parameters.Add(new SqlParameter("BBTName", bbtFormation.BBTName));
                cmd.Parameters.Add(new SqlParameter("BBTCapacity", bbtFormation.BBTCapacity));
                cmd.Parameters.Add(new SqlParameter("BBTBulkLiter", bbtFormation.BBTBulkLiter));
                cmd.Parameters.Add(new SqlParameter("BrandId", bbtFormation.BrandID));
                cmd.Parameters.Add(new SqlParameter("Status", bbtFormation.Status));
                cmd.Parameters.Add(new SqlParameter("mac", MacAddress));
                cmd.Parameters.Add(new SqlParameter("user_id",UserSession.LoggedInUserId.ToString()));
                cmd.Parameters.Add(new SqlParameter("user_ip", IpAddress));
                cmd.Parameters.Add(new SqlParameter("Msg", ""));
                cmd.Parameters.Add(new SqlParameter("sp_Type", bbtFormation.SP_Type));
                cmd.Parameters["Msg"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters["Msg"].Size = 32676;
                cmd.ExecuteNonQuery();
                str = cmd.Parameters["Msg"].Value.ToString().Trim();
                tran.Commit();
            }
            catch (Exception ex){
                str = ex.ToString();
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            return str;
        }
        public DataSet GetBBTMaster(int bbtId,int brandId,string status)
        {
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("BBTId", bbtId));
                parameters.Add(new SqlParameter("BrandId", brandId));
                parameters.Add(new SqlParameter("BreweryId", 1));
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
                cmd.Parameters.Add(new SqlParameter("TransactionType", UTBL.TransactionType));
                cmd.Parameters.Add(new SqlParameter("IssueBL", UTBL.IssueBL));
                cmd.Parameters.Add(new SqlParameter("Wastage", UTBL.Wastage));
                cmd.Parameters.Add(new SqlParameter("TransferDate", UTBL.TransferDate));
                cmd.Parameters.Add(new SqlParameter("Remark", UTBL.Remark));
                cmd.Parameters.Add(new SqlParameter("macId", MacAddress));
                cmd.Parameters.Add(new SqlParameter("user_id", UserSession.LoggedInUserId));
                cmd.Parameters.Add(new SqlParameter("user_ip",IpAddress));
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
        public DataSet GetUTTransferToBBT(DateTime FromDate, DateTime ToDate, int UnitTankId,string Status,short BreweryId,int BBTId)
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
                parameters.Add(new SqlParameter("Status", Status));
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
                if(dt.Rows.Count>0)
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


        public string InsertUpdateGatePass(BrewerytToManufacturerGatePass gatePass=null,List<DistrictWholeSaleToRetailorModel> districtWholeSaleToRetailorModels=null)
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
                if(gatePass.GatePassId > 0 && dt.Rows.Count>0)
                {
                    cmd.Parameters.Add(new SqlParameter("GatePassId", gatePass.GatePassId));
                    cmd.Parameters.Add(new SqlParameter("tbl_GatePassBrandMapping", dt));
                }
                if (gatePass.GatePassId>0 && gatePass.BrandId > 0)
                {
                    cmd.Parameters.Add(new SqlParameter("GatePassId", gatePass.GatePassId));
                    cmd.Parameters.Add(new SqlParameter("BrandId", gatePass.BrandId));
                    cmd.Parameters.Add(new SqlParameter("UploadValue", gatePass.UploadValue));
                }
                else

                {
                    cmd.Parameters.Add(new SqlParameter("BrandId", null));
                    cmd.Parameters.Add(new SqlParameter("FromDate", gatePass.FromDate));
                    cmd.Parameters.Add(new SqlParameter("ToDate",gatePass.ToDate));
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



        public DataSet GetGatePassDetails(int spType,long GatePassId=0,long brandId=0)
        {
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("SpType", spType));
                parameters.Add(new SqlParameter("UserLevelId", Convert.ToInt32(UserSession.LoggedInUserLevelId)));
                if (GatePassId>0)
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

     


    }
}