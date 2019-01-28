using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using Connect;
using UPExciseLTE.Models;

namespace UPExciseLTE
{
    public static class UserDtl
    {
        public static int GetFirstNonPunctuationCharIndex(string input, int startIndex)
        {
            char[] punctuation = new char[15] { '!', '#', '$', '%', '^', '^', '+', '=', '[', ']', '<', '>', '?', '~', '.' };
            //Move the startIndex forward one because we ignore the index user set
            startIndex = startIndex + 1 < input.Length ? startIndex + 1 : input.Length;

            for (int i = startIndex; i < input.Length; i++)
            {
                if (!punctuation.Contains(input[i]))
                {
                    return i;
                }
            }

            return -1;
        }
        //public static bool checkFileupload(MemoryStream stream, string ext)
        //{
        //    bool ret = false;
        //    string[] validFileTypes = { "pdf", "PDF", "gif", "GIF", "png", "PNG", "jpg", "JPG", "jpeg", "JPEG" };

        //    string f_extension = null;
        //    if (true == true)
        //    {
                
        //        string content = GetActualFileType(stream);
        //        bool isValidFile = false;
        //        bool isValidContent = false;
        //        for (int i = 0; i < validFileTypes.Length; i++)
        //        {
        //            if (ext == validFileTypes[i])
        //            {
        //                isValidFile = true;

        //            }
        //        }
        //        for (int i = 0; i < validFileTypes.Length; i++)
        //        {
        //            if (content == validFileTypes[i])
        //            {
        //                isValidContent = true;


        //            }
        //        }
             

        //        if (!isValidFile || !isValidContent)
        //        {
                  
        //            ret = false;
        //        }
        //        else
        //        {
        //            ret = true;
        //        }
        //    line: ;
        //        return ret;
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //}



        public static int GetFinalMapValid(int DistictId, string Module)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@pDistictId", DistictId));
            parameters.Add(new SqlParameter("@pModule", Module));
            int res = Convert.ToInt32(SqlHelper.ExecuteScalar(Connection.Conn (UserSession.dbAddress), CommandType.StoredProcedure, "Proc_GetFinalLockMap", parameters.ToArray()));
            return res;
        }

        public static int UpdateFinalMapValid(int Status, int DistictId, string Module)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@pStatus", Status));
            parameters.Add(new SqlParameter("@pDistictId", DistictId));
            parameters.Add(new SqlParameter("@pModule", Module));
            int res = Convert.ToInt32(SqlHelper.ExecuteNonQuery(Connection.Conn (UserSession.dbAddress), CommandType.StoredProcedure, "Proc_UpdateFinalLockMap", parameters.ToArray()));
            return res;
        }


        public static int GetMenuValid(int UserId, string PageName,string IP)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@pUserId", UserId));
            parameters.Add(new SqlParameter("@pPageName", PageName));         
            int res = Convert.ToInt32(SqlHelper.ExecuteScalar(Connection.Conn (UserSession.dbAddress), CommandType.StoredProcedure, "Proc_getMenuItemValid", parameters.ToArray()));
            return res;
        }

        public static DataTable DistictOfficeMap(int id, string OfficeName_H, string OfficeName_E, int department, int LevelId, int DivisionId, int distictId, int tehsilid, int thanaID, string officeTemplateid)
        {
            try
            {
                SqlParameter[] arParms = new SqlParameter[11];
                // @PersonID Input Parameter

                arParms[0] = new SqlParameter("@OfficesName_U", SqlDbType.NVarChar, 200);
                arParms[0].Value = OfficeName_H;

                arParms[1] = new SqlParameter("@OfficesName", SqlDbType.NVarChar, 200);
                arParms[1].Value = OfficeName_E;
                arParms[2] = new SqlParameter("@DepartmentCode", SqlDbType.Int);
                arParms[2].Value = department;
                arParms[3] = new SqlParameter("@LevelId", SqlDbType.Int);
                arParms[3].Value = LevelId;
                arParms[4] = new SqlParameter("@DistictId", SqlDbType.Int);
                arParms[4].Value = distictId;
                arParms[5] = new SqlParameter("@ID", SqlDbType.Int);
                arParms[5].Value = id;
                arParms[6] = new SqlParameter("@DivisionId", SqlDbType.Int);
                arParms[6].Value = DivisionId;
                arParms[7] = new SqlParameter("@CreatedBy", SqlDbType.BigInt);
                arParms[7].Value = DivisionId; // UserSession.LoggedInUserId;
                arParms[8] = new SqlParameter("@officeTemplateid", SqlDbType.VarChar);
                arParms[8].Value = officeTemplateid;
                arParms[9] = new SqlParameter("@TehsilId", SqlDbType.Int);
                arParms[9].Value = tehsilid;

                arParms[10] = new SqlParameter("@ThanaID", SqlDbType.Int);
                arParms[10].Value = thanaID;
                // Execute the stored procedure
                DataSet ds = new DataSet();
                ds = SqlHelper.ExecuteDataset(Connection.Conn (UserSession.dbAddress), CommandType.StoredProcedure, "proc_InsertDepartmentOffice", arParms);
                // create a string array of return values and assign values returned from stored procedure
                return ds.Tables[0];
            }
            catch
            {
                return null;
            }



        }
        public static DataSet GetUserId(int officerId, string UserLevel)
        {
            try
            {
                SqlParameter[] arParms = new SqlParameter[10];
                // @PersonID Input Parameter

                arParms[0] = new SqlParameter("@officerId", SqlDbType.Int);
                arParms[0].Value = officerId;

                arParms[1] = new SqlParameter("@LoginUserLevel", SqlDbType.NVarChar, 20);
                arParms[1].Value = UserLevel;

                // Execute the stored procedure
                DataSet ds = new DataSet();
                ds = SqlHelper.ExecuteDataset(Connection.Conn (UserSession.dbAddress), CommandType.StoredProcedure, "Proc_UserofficerEntry_GetbyofficerId", arParms);
                // create a string array of return values and assign values returned from stored procedure
                return ds;
            }
            catch
            {
                return null;
            }
        }


        public static DataSet GetMobileNo(string uname)
        {
            try
            {
                SqlParameter[] arParms = new SqlParameter[10];
                // @PersonID Input Parameter

                arParms[0] = new SqlParameter("@pUserName", SqlDbType.Text);
                arParms[0].Value = uname;
                
                // Execute the stored procedure
                DataSet ds = new DataSet();
                ds = SqlHelper.ExecuteDataset(Connection.Conn (UserSession.dbAddress), CommandType.StoredProcedure, "Proc_GetMobileNo", arParms);
                // create a string array of return values and assign values returned from stored procedure
                return ds;
            }
            catch(Exception)
            {
                return null;
            }
        }


        public static DataSet GetDistictOfficeMap(int DistictId, int DepartmentCode, int levelid, int DivisionId, int TehsilId, int ThanaId)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> parameters = new List<SqlParameter>();
           

            parameters.Add(new SqlParameter("@DistrictId", DistictId));
            parameters.Add(new SqlParameter("@levelid", levelid));
            parameters.Add(new SqlParameter("@DivisionId", DivisionId));
            parameters.Add(new SqlParameter("@TehsilId", TehsilId));
            parameters.Add(new SqlParameter("@ThanaId", TehsilId));
            parameters.Add(new SqlParameter("@DepartmentCode", DepartmentCode));
            ds = SqlHelper.ExecuteDataset(Connection.Conn (UserSession.dbAddress), CommandType.StoredProcedure, "proc_GetDepartmentOffice", parameters.ToArray());
            return ds;
        }
        public static DataSet GetIsForwardandMarkedBY(string ForwardId)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@pFoarwardId", ForwardId));
            ds = SqlHelper.ExecuteDataset(Connection.Conn (UserSession.dbAddress), CommandType.StoredProcedure, "Proc_GetIsForwardId", parameters.ToArray());
            return ds;
        }

        public static DataSet VerifyUser(string UserName)
        {
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@pUserName", UserName));
                ds = SqlHelper.ExecuteDataset(Connection.Conn( (UserSession. !=null)? UserSession.dbAddress:null), CommandType.StoredProcedure, "Proc_VerifyUser", parameters.ToArray());
            }
            catch (Exception exp) { ds = null; }
            return ds;
        }
        public static DataSet VerifyApplicant(string UserName,short yojana_code )
        {
            DataSet ds = new DataSet();
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@pUserName", UserName));
            parameters.Add(new SqlParameter("@Yojana_Code", yojana_code));
            ds = SqlHelper.ExecuteDataset(Connection.Conn (UserSession.dbAddress), CommandType.StoredProcedure, "Proc_VerifyApplicant", parameters.ToArray());
            return ds;
        }
        public static DataSet GetMenuData(int UserId)
        {
            try
            {
                DataSet ds = new DataSet();
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@pUserId", UserId));
               
                ds = SqlHelper.ExecuteDataset(Connection.Conn (UserSession.dbAddress), CommandType.StoredProcedure, "Proc_getMenuItem", parameters.ToArray());
                return ds;
            }


            catch
            {
                return null;
            }
        }

        public static IDataReader GetMenuData2(int UserId)
        {
            try
            {
                IDataReader ds;//= new IDataReader();
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@pUserId", UserId));

                ds = SqlHelper.ExecuteReader(Connection.Conn(UserSession.dbAddress.ToString()), CommandType.StoredProcedure, "Proc_getMenuItem", parameters.ToArray());
                return ds;
            }


            catch(Exception ex)
            {
                throw  ex;
            }
        }

        public static string CreateOperatorUser(string NameH, string NameE, string Email, string Phone, string UserName, string pass, string usertype, int UserLevel, int state, int DivisionId, int distict, int tahshil, int DepartmentId, int PostId, int thana, int block, int section, string rights, string address, string joiningdate, string id, string Officeid)
        {
            try
            {
                SqlParameter[] arParms = new SqlParameter[24];
                // @PersonID Input Parameter
                arParms[0] = new SqlParameter("@pNameH", SqlDbType.NVarChar, 200);
                arParms[0].Value = NameH;
                arParms[1] = new SqlParameter("@pNameE", SqlDbType.VarChar, 100);
                arParms[1].Value = NameE;
                arParms[2] = new SqlParameter("@pEmail", SqlDbType.NVarChar, 100);
                arParms[2].Value = Email;
                arParms[3] = new SqlParameter("@pPhone", SqlDbType.NVarChar, 40);
                arParms[3].Value = Phone;
                arParms[4] = new SqlParameter("@pUserName", SqlDbType.NVarChar, 40);
                arParms[4].Value = UserName;
                arParms[5] = new SqlParameter("@pPass", SqlDbType.NVarChar, 200);
                arParms[5].Value = pass;
                arParms[6] = new SqlParameter("@pUserType", SqlDbType.NVarChar, 200);
                arParms[6].Value = usertype;
                arParms[7] = new SqlParameter("@pUserLevel", SqlDbType.Int);
                arParms[7].Value = UserLevel;
                arParms[8] = new SqlParameter("@pStateId", SqlDbType.Int);
                arParms[8].Value = state;
                arParms[9] = new SqlParameter("@pDivisionId", SqlDbType.Int);
                arParms[9].Value = DivisionId;
                arParms[10] = new SqlParameter("@pDistictId", SqlDbType.Int);
                arParms[10].Value = distict;
                arParms[11] = new SqlParameter("@pTehshilId", SqlDbType.Int);
                arParms[11].Value = tahshil;
                arParms[12] = new SqlParameter("@pDepartmentId", SqlDbType.Int);
                arParms[12].Value = DepartmentId;
                arParms[13] = new SqlParameter("@pPostId", SqlDbType.Int);
                arParms[13].Value = PostId;
                arParms[14] = new SqlParameter("@pThana", SqlDbType.Int);
                arParms[14].Value = thana;
                arParms[15] = new SqlParameter("@pBlockId", SqlDbType.Int);
                arParms[15].Value = block;
                arParms[16] = new SqlParameter("@pRights", SqlDbType.VarChar, 500);
                arParms[16].Value = rights;
                arParms[17] = new SqlParameter("@psection", SqlDbType.Int);
                arParms[17].Value = section;
                arParms[18] = new SqlParameter("@paddress", SqlDbType.VarChar);
                arParms[18].Value = address;
                arParms[19] = new SqlParameter("@pjoiningdate", SqlDbType.VarChar);
                arParms[19].Value = joiningdate;
                arParms[20] = new SqlParameter("@pid", SqlDbType.Int);
                arParms[20].Value = id;
                arParms[21] = new SqlParameter("@pOfficeid", SqlDbType.Int);
                arParms[21].Value = Officeid;
                // @ProductName Output Parameter
                arParms[22] = new SqlParameter("@pOutPut", SqlDbType.NVarChar, 40);
                arParms[22].Direction = ParameterDirection.Output;
                // @UnitPrice Output Parameter
                arParms[23] = new SqlParameter("@pMessage", SqlDbType.NVarChar, 200);
                arParms[23].Direction = ParameterDirection.Output;
                // Execute the stored procedure
                SqlHelper.ExecuteNonQuery(Connection.Conn (UserSession.dbAddress), CommandType.StoredProcedure, "Proc_CreateOPUser", arParms);
                // create a string array of return values and assign values returned from stored procedure
                string[] arReturnParms = new string[2];
                arReturnParms[0] = arParms[22].Value.ToString();
                arReturnParms[1] = arParms[23].Value.ToString();
                return arParms[23].Value.ToString();
            }
            catch
            {
                return null;
            }

        }
       
        public static string Userpasswordchange(string UserName, string pass, string OldpPass)
        {
            try
            {
                SqlParameter[] arParms = new SqlParameter[5];
                // @PersonID Input Parameter

                arParms[0] = new SqlParameter("@pUserName", SqlDbType.NVarChar, 40);
                arParms[0].Value = UserName;
                arParms[1] = new SqlParameter("@pPass", SqlDbType.NVarChar, 200);
                arParms[1].Value = pass;

                arParms[2] = new SqlParameter("@OldpPass", SqlDbType.NVarChar, 200);
                arParms[2].Value = OldpPass;

                // @ProductName Output Parameter
                arParms[3] = new SqlParameter("@pOutPut", SqlDbType.NVarChar, 40);
                arParms[3].Direction = ParameterDirection.Output;
                // @UnitPrice Output Parameter
                arParms[4] = new SqlParameter("@pMessage", SqlDbType.NVarChar, 200);
                arParms[4].Direction = ParameterDirection.Output;
                // Execute the stored procedure
                SqlHelper.ExecuteNonQuery(Connection.Conn (UserSession.dbAddress), CommandType.StoredProcedure, "Proc_UserPasswordChange", arParms);
                // create a string array of return values and assign values returned from stored procedure
                string[] arReturnParms = new string[2];
                arReturnParms[0] = arParms[3].Value.ToString();
                arReturnParms[1] = arParms[4].Value.ToString();
                return arParms[4].Value.ToString();
            }
            catch
            {
                return null;
            }



        }


        public static string Userpasswordchange(string UserName, string pass)
        {
            try
            {
                SqlParameter[] arParms = new SqlParameter[5];
                // @PersonID Input Parameter

                arParms[0] = new SqlParameter("@pUserName", SqlDbType.NVarChar, 40);
                arParms[0].Value = UserName;
                arParms[1] = new SqlParameter("@pPass", SqlDbType.NVarChar, 200);
                arParms[1].Value = pass;

                //arParms[2] = new SqlParameter("@OldpPass", SqlDbType.NVarChar, 200);
                //arParms[2].Value = OldpPass;

                // @ProductName Output Parameter
                arParms[3] = new SqlParameter("@pOutPut", SqlDbType.NVarChar, 40);
                arParms[3].Direction = ParameterDirection.Output;
                // @UnitPrice Output Parameter
                arParms[4] = new SqlParameter("@pMessage", SqlDbType.NVarChar, 200);
                arParms[4].Direction = ParameterDirection.Output;
                // Execute the stored procedure
                SqlHelper.ExecuteNonQuery(Connection.Conn (UserSession.dbAddress), CommandType.StoredProcedure, "Proc_UserPasswordChangeOTP", arParms);
                // create a string array of return values and assign values returned from stored procedure
                string[] arReturnParms = new string[2];
                arReturnParms[0] = arParms[3].Value.ToString();
                arReturnParms[1] = arParms[4].Value.ToString();
                return arParms[4].Value.ToString();
            }
            catch (Exception )
            {
                return null;
            }

        }

        public static string Userpasswordchange2(string UserName, string pass)
        {
            try
            {
                SqlParameter[] arParms = new SqlParameter[5];
                // @PersonID Input Parameter

                arParms[0] = new SqlParameter("@pUserName", SqlDbType.NVarChar, 40);
                arParms[0].Value = UserName;
                arParms[1] = new SqlParameter("@pPass", SqlDbType.NVarChar, 200);
                arParms[1].Value = pass;

                //arParms[2] = new SqlParameter("@OldpPass", SqlDbType.NVarChar, 200);
                //arParms[2].Value = OldpPass;

                // @ProductName Output Parameter
                arParms[3] = new SqlParameter("@pOutPut", SqlDbType.NVarChar, 40);
                arParms[3].Direction = ParameterDirection.Output;
                // @UnitPrice Output Parameter
                arParms[4] = new SqlParameter("@pMessage", SqlDbType.NVarChar, 200);
                arParms[4].Direction = ParameterDirection.Output;
                // Execute the stored procedure
                SqlHelper.ExecuteNonQuery(Connection.Conn (UserSession.dbAddress), CommandType.StoredProcedure, "Proc_UserPasswordChangeOTP2", arParms);
                // create a string array of return values and assign values returned from stored procedure
                string[] arReturnParms = new string[2];
                arReturnParms[0] = arParms[3].Value.ToString();
                arReturnParms[1] = arParms[4].Value.ToString();
                return arParms[4].Value.ToString();
            }
            catch (Exception )
            {
                return null;
            }

        }
        

        public static string CreateUser(string Name, string Dob, string Email, string Phone, string UserName, string pass, string usertype, int UserLevel, int state, int DivisionId, int distict, int tahshil, int DepartmentId, int PostId, int thana, int block)
        {
            try
            {
                SqlParameter[] arParms = new SqlParameter[18];
                // @PersonID Input Parameter
                arParms[0] = new SqlParameter("@pName", SqlDbType.NVarChar, 40);
                arParms[0].Value = Name;
                arParms[1] = new SqlParameter("@pDob", SqlDbType.NVarChar, 40);
                arParms[1].Value = Dob;
                arParms[2] = new SqlParameter("@pEmail", SqlDbType.NVarChar, 100);
                arParms[2].Value = Email;
                arParms[3] = new SqlParameter("@pPhone", SqlDbType.NVarChar, 40);
                arParms[3].Value = Phone;
                arParms[4] = new SqlParameter("@pUserName", SqlDbType.NVarChar, 40);
                arParms[4].Value = UserName;
                arParms[5] = new SqlParameter("@pPass", SqlDbType.NVarChar, 200);
                arParms[5].Value = pass;
                arParms[6] = new SqlParameter("@pUserType", SqlDbType.NVarChar, 200);
                arParms[6].Value = usertype;
                arParms[7] = new SqlParameter("@pUserLevel", SqlDbType.Int);
                arParms[7].Value = UserLevel;
                arParms[8] = new SqlParameter("@pStateId", SqlDbType.Int);
                arParms[8].Value = state;
                arParms[9] = new SqlParameter("@pDivisionId", SqlDbType.Int);
                arParms[9].Value = DivisionId;
                arParms[10] = new SqlParameter("@pDistictId", SqlDbType.Int);
                arParms[10].Value = distict;
                arParms[11] = new SqlParameter("@pTehshilId", SqlDbType.Int);
                arParms[11].Value = tahshil;
                arParms[12] = new SqlParameter("@pDepartmentId", SqlDbType.Int);
                arParms[12].Value = DepartmentId;
                arParms[13] = new SqlParameter("@pPostId", SqlDbType.Int);
                arParms[13].Value = PostId;

                arParms[14] = new SqlParameter("@pThana", SqlDbType.Int);
                arParms[14].Value = thana;
                arParms[15] = new SqlParameter("@pBlockId", SqlDbType.Int);
                arParms[15].Value = block;
                // @ProductName Output Parameter
                arParms[16] = new SqlParameter("@pOutPut", SqlDbType.NVarChar, 40);
                arParms[16].Direction = ParameterDirection.Output;
                // @UnitPrice Output Parameter
                arParms[17] = new SqlParameter("@pMessage", SqlDbType.NVarChar, 200);
                arParms[17].Direction = ParameterDirection.Output;
                // Execute the stored procedure
                SqlHelper.ExecuteNonQuery(Connection.Conn (UserSession.dbAddress), CommandType.StoredProcedure, "Proc_CreateUser", arParms);
                // create a string array of return values and assign values returned from stored procedure
                string[] arReturnParms = new string[2];
                arReturnParms[0] = arParms[16].Value.ToString();
                arReturnParms[1] = arParms[17].Value.ToString();
                return arParms[17].Value.ToString();
            }
            catch
            {
                return null;
            }

        }

        public static string leveluser_bck(int officerId, string NameE, string NameU, string OfAdd, string JoiningDate, string Mobile_No, int UserLevel, int Department, int Post, int Div, int District, int Tehsil, int Thana, int Block, string EmailId)
        {

            try
            {
                SqlParameter[] arParms = new SqlParameter[16];
                // @PersonID Input Parameter
                arParms[0] = new SqlParameter("@Officername", SqlDbType.NVarChar, 200);
                arParms[0].Value = NameE;
                arParms[1] = new SqlParameter("@Officername_U", SqlDbType.NVarChar, 200);
                arParms[1].Value = NameU;
                arParms[2] = new SqlParameter("@Office_Add", SqlDbType.NVarChar, 100);
                arParms[2].Value = OfAdd;
                arParms[3] = new SqlParameter("@JoiningDate", SqlDbType.NVarChar, 40);
                arParms[3].Value = JoiningDate;
                arParms[4] = new SqlParameter("@Mobile_No", SqlDbType.NVarChar, 40);
                arParms[4].Value = Mobile_No;
                arParms[5] = new SqlParameter("@UserLevel", SqlDbType.Int);
                arParms[5].Value = UserLevel;
                arParms[6] = new SqlParameter("@Department ", SqlDbType.Int);
                arParms[6].Value = Department;
                arParms[8] = new SqlParameter("@Post", SqlDbType.Int);
                arParms[8].Value = Post;
                arParms[9] = new SqlParameter("@Div", SqlDbType.Int);
                arParms[9].Value = Div;
                arParms[10] = new SqlParameter("@District", SqlDbType.Int);
                arParms[10].Value = District;
                arParms[11] = new SqlParameter("@Tehsil", SqlDbType.Int);
                arParms[11].Value = Tehsil;
                arParms[12] = new SqlParameter("@Thana", SqlDbType.Int);
                arParms[12].Value = Thana;
                arParms[13] = new SqlParameter("@Block", SqlDbType.Int);
                arParms[13].Value = Block;
                arParms[14] = new SqlParameter("@officerId", SqlDbType.Int);
                arParms[14].Value = officerId;
                arParms[15] = new SqlParameter("@EmailId", SqlDbType.VarChar, 50);
                arParms[15].Value = EmailId;
                // @ProductName Output Parameter
                //arParms[14] = new SqlParameter("@pOutPut", SqlDbType.NVarChar, 40);
                //arParms[14].Direction = ParameterDirection.Output;
                //// @UnitPrice Output Parameter
                //arParms[15] = new SqlParameter("@pMessage", SqlDbType.NVarChar, 200);
                //arParms[15].Direction = ParameterDirection.Output;
                //// Execute the stored procedure
                int res = SqlHelper.ExecuteNonQuery(Connection.Conn (UserSession.dbAddress), CommandType.StoredProcedure, "Proc_UserofficerEntry_Save", arParms);
                //// create a string array of return values and assign values returned from stored procedure
                //string[] arReturnParms = new string[2];
                //arReturnParms[0] = arParms[14].Value.ToString();
                //arReturnParms[1] = arParms[15].Value.ToString();
                return res.ToString();
            }
            catch
            {
                return null;
            }


        }
        #region User Saved
        public static string leveluser(int officerId, string NameE, string NameH, string OfAdd, string JoiningDate, string Mobile_No, int UserLevel, int Department, int Post, int Div, int District, int Tehsil, int Thana, int Block, string EmailId, string userName, string usertype, string str, string paas, string section,string Designation,string ofcNo)
        {

            try
            {
                SqlParameter[] param = {
                      new SqlParameter("@officerId",officerId),
                      new SqlParameter("@Mobile_No",Mobile_No),
                      new SqlParameter("@Office_Add",OfAdd),
                      new SqlParameter("@Officername",NameE),                    
                      new SqlParameter("@OfficernameH",NameH),                    
                      new SqlParameter("@UserLevel",UserLevel),
                      new SqlParameter("@Department",Department),
                      new SqlParameter("@JoiningDate",JoiningDate),
                      new SqlParameter("@Post",Post),
                       new SqlParameter("@Div",Div),
                       new SqlParameter("@District",District),
                      new SqlParameter("@Tehsil",Tehsil),
                      new SqlParameter("@Thana",Thana),
                      new SqlParameter("@Block",Block),
                      new SqlParameter("@EmailId",EmailId),
                      new SqlParameter("@userName",userName),
                      new SqlParameter("@usertype",usertype),
                      new SqlParameter("@userRights",str),
                      new SqlParameter("@pass",paas),
                      new SqlParameter("@section",section),
                      new SqlParameter("@NodalDesignation",Designation),
                      new SqlParameter("@NodalMob",ofcNo)
                   };


                string res = SqlHelper.ExecuteScalar(Connection.Conn (UserSession.dbAddress), CommandType.StoredProcedure, "Proc_UserofficerEntry_Save", param).ToString();

                return res.ToString();
            }
            catch
            {
                return null;
            }


        }
        #endregion

        public static string leveluserWithPassword(int officerId, string NameE, string Mobile_No, int UserLevel, int Department, int Post, int Div, int District, int Tehsil, int Thana, int Block, string EmailId, string username, string password, int createdby)
        {

            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@officerId",officerId),
                    new SqlParameter("@Officername",NameE),
                    new SqlParameter("@Mobile_No",Mobile_No),
                    new SqlParameter("@UserLevel",UserLevel),
                    new SqlParameter("@Department",Department),
                    new SqlParameter("@Post",Post),
                    new SqlParameter("@Div",Div),
                    new SqlParameter("@District",District),
                    new SqlParameter("@Tehsil",Tehsil),
                    new SqlParameter("@Thana",Thana),
                    new SqlParameter("@Block",Block),
                    new SqlParameter("@EmailId",EmailId),
                    new SqlParameter("@UserName",username),
                    new SqlParameter("@Password",password),
                    new SqlParameter("@createdby",createdby)
                                       };


                string res = SqlHelper.ExecuteScalar(Connection.Conn (UserSession.dbAddress), CommandType.StoredProcedure, "Proc_UserofficerEntry_SaveAndPassword", param).ToString();

                return res.ToString();
            }
            catch
            {
                return null;
            }


        }

        public static DataSet bindCheckBoxList(string usertype, string userlevel)
        {


            try
            {
                SqlParameter[] param = {
                                           new SqlParameter("@Parm1",userlevel),
                                           new SqlParameter("@Parm2",usertype)                                      
                  
                   };


                DataSet res = SqlHelper.ExecuteDataset(Connection.Conn (UserSession.dbAddress), CommandType.StoredProcedure, "proc_GetUserRights", param.ToArray());

                return res;
            }
            catch
            {
                return null;
            }

        }

        public static DataTable VerifyDeleteOffice(long Id, string flag)
        {
            try
            {
                SqlParameter[] param = {
                                           new SqlParameter("@OfficeId",Id),

                                           new SqlParameter("@flag",flag)                                      
                  
                   };


                DataSet res = SqlHelper.ExecuteDataset(Connection.Conn (UserSession.dbAddress), CommandType.StoredProcedure, "proc_VerifyDeleteOffice", param.ToArray());

                return res.Tables[0];
            }
            catch
            {
                return null;
            }
        }

        public static DataSet GetDepartmentOfficeTemplates(short deptid,int levelid)
        {
            try
            {
                SqlParameter[] param = {
                                           new SqlParameter("@departmentCode",deptid),
                                               new SqlParameter("@levelid",levelid)
                   };


                DataSet res = SqlHelper.ExecuteDataset(Connection.Conn (UserSession.dbAddress), CommandType.StoredProcedure, "proc_GetDepartmentOfficeTemplates", param.ToArray());

                return res;
            }
            catch
            {
                return null;
            }
        }

        public static DataTable DistictOfficeMap(int id, string OfficeName_H, string OfficeName_E, int department, int PostId, int LevelId)
        {
            try
            {
                SqlParameter[] arParms = new SqlParameter[6];
                // @PersonID Input Parameter

                arParms[0] = new SqlParameter("@OfficesName_U", SqlDbType.NVarChar, 200);
                arParms[0].Value = OfficeName_H;

                arParms[1] = new SqlParameter("@OfficesName", SqlDbType.NVarChar, 200);
                arParms[1].Value = OfficeName_E;
                arParms[2] = new SqlParameter("@DepartmentCode", SqlDbType.Int);
                arParms[2].Value = department;
                arParms[3] = new SqlParameter("@ID", SqlDbType.Int);
                arParms[3].Value = id;
                arParms[4] = new SqlParameter("@PostId", SqlDbType.Int);
                arParms[4].Value = PostId;
                arParms[5] = new SqlParameter("@LevelId", SqlDbType.Int);
                arParms[5].Value = LevelId;
                
                // Execute the stored procedure
                DataSet ds = new DataSet();
                ds = SqlHelper.ExecuteDataset(Connection.Conn (UserSession.dbAddress), CommandType.StoredProcedure, "proc_InsertOfficeTemplate", arParms);
                // create a string array of return values and assign values returned from stored procedure
                return ds.Tables[0];
            }
            catch
            {
                return null;
            }
        }

        public static DataTable DeleteTemplate(long Id)
        {
            try
            {
                SqlParameter[] arParms = new SqlParameter[1];
                // @PersonID Input Parameter

                
                arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                arParms[0].Value = Id;
               

                // Execute the stored procedure
                DataSet ds = new DataSet();
                ds = SqlHelper.ExecuteDataset(Connection.Conn (UserSession.dbAddress), CommandType.StoredProcedure, "proc_DeleteOfficeTemplate", arParms);
                // create a string array of return values and assign values returned from stored procedure
                return ds.Tables[0];
            }
            catch
            {
                return null;
            }
        }

        public static int UserLastloginPassIncorrect(string UserName,string Attempt)
        {
            try
            {
                SqlParameter[] arParms = new SqlParameter[2];
                // @PersonID Input Parameter

                arParms[0] = new SqlParameter("@pUserName", SqlDbType.NVarChar, 40);
                arParms[0].Value = UserName;
                arParms[1] = new SqlParameter("@pAttempt", SqlDbType.Int);
                arParms[1].Value = Convert.ToInt32(Attempt);

                // Execute the stored procedure
                int res = SqlHelper.ExecuteNonQuery(Connection.Conn (UserSession.dbAddress), CommandType.StoredProcedure, "Proc_UserLastloginAttempt", arParms);
                // create a string array of return values and assign values returned from stored procedure
                return res;
            }
            catch
            {
                return 0;
            }



        }

        public static int UserLastloginchange(string UserName,string PageName, string IPAddress)
        {
            try
            {
                SqlParameter[] arParms = new SqlParameter[3];
                // @PersonID Input Parameter

                arParms[0] = new SqlParameter("@pUserName", SqlDbType.NVarChar, 40);
                arParms[0].Value = UserName;
                arParms[1] = new SqlParameter("@pPageName", SqlDbType.NVarChar, 40);
                arParms[1].Value = PageName;

                arParms[2] = new SqlParameter("@pIPAddress", SqlDbType.NVarChar, 40);
                arParms[2].Value = IPAddress;

                // Execute the stored procedure
                int res = SqlHelper.ExecuteNonQuery(Connection.Conn (UserSession.dbAddress), CommandType.StoredProcedure, "Proc_UserLastloginchange", arParms);
                // create a string array of return values and assign values returned from stored procedure
                return res;
            }
            catch
            {
                return 0;
            }



        }

        #region Added By Ramesh
        public static string CreatePostMaster(string postHindiName,  int postId, string type,int  dist_code,int Depart_Code, int Office_Code, int userid, string ip)
        {
            SqlConnection Sqlcon = null;
            SqlCommand Sqlcmd = null;
            try
            {
                string result = string.Empty;
                Sqlcon = new SqlConnection(Connection.Conn (UserSession.dbAddress));
                Sqlcon.Open();
                Sqlcmd = new SqlCommand("exec Proc_Save_DesigName_Details @postHindiName,@postId ,@type , @dist_code, @depart_code, @OfficeCode,@userid ,@IPAddress ", Sqlcon);
                Sqlcmd.Parameters.Add("@postHindiName", postHindiName);
                Sqlcmd.Parameters.Add("@postId", postId);
                Sqlcmd.Parameters.Add("@type", type);
                Sqlcmd.Parameters.Add("@dist_code", dist_code);
                Sqlcmd.Parameters.Add("@depart_code", Depart_Code);
                Sqlcmd.Parameters.Add("@OfficeCode", Office_Code);
                Sqlcmd.Parameters.Add("@userid", userid);
                Sqlcmd.Parameters.Add("@IPAddress", ip);
                result = (string)Sqlcmd.ExecuteScalar();
                Sqlcon.Close();
                return result;
            }
            catch
            {
                if (Sqlcon.State == ConnectionState.Open)
                    Sqlcon.Close();
                throw;
            }
            finally
            {

                if (Sqlcon.State == ConnectionState.Open)
                    Sqlcon.Close();
                Sqlcon = null;
                Sqlcmd = null;
            }

        }

        public static DataSet Get_PostMaster_Details(int dist_code ,int depart_code,int office_code)
        {

            SqlCommand Sqlcmd = null;
            SqlConnection Sqlcon = null;
            SqlDataAdapter Sqlda;
            DataSet dset = null;
            try
            {
                Sqlcon = new SqlConnection(Connection.Conn (UserSession.dbAddress));
                Sqlcon.Open();
                Sqlcmd = new SqlCommand("Proc_Get_DesigMaster_Details ", Sqlcon);
                Sqlcmd.Parameters.Add("@dist_code", dist_code);
                Sqlcmd.Parameters.Add("@depart_code", depart_code);
                Sqlcmd.Parameters.Add("@office_code", office_code);
                Sqlcmd.CommandType = CommandType.StoredProcedure;
                Sqlcmd.CommandTimeout = 600;

                Sqlda = new SqlDataAdapter(Sqlcmd);
                dset = new DataSet();
                Sqlda.Fill(dset);
                dset.RemotingFormat = SerializationFormat.Binary;
                Sqlcon.Close();
                return dset;

            }
            catch
            {
                throw;
            }
            finally
            {

                if (Sqlcon.State == ConnectionState.Open)
                    Sqlcon.Close();
                Sqlcon = null;
                Sqlcmd = null;
                Sqlda = null;
                dset = null;
            }
        }
        #endregion

        #region Added by Ramesh for DepartmentMaster
        public static DataSet GET_DEPARTMENT_LEVEL()
        {

            SqlCommand Sqlcmd = null;
            SqlConnection Sqlcon = null;
            SqlDataAdapter Sqlda;
            DataSet dset = null;
            try
            {
                Sqlcon = new SqlConnection(Connection.Conn (UserSession.dbAddress));
                Sqlcon.Open();
                Sqlcmd = new SqlCommand("PROC_GET_DEPARTMENT_LEVEL", Sqlcon);
                Sqlcmd.CommandType = CommandType.StoredProcedure;
                Sqlcmd.CommandTimeout = 600;

                Sqlda = new SqlDataAdapter(Sqlcmd);
                dset = new DataSet();
                Sqlda.Fill(dset);
                dset.RemotingFormat = SerializationFormat.Binary;
                Sqlcon.Close();
                return dset;

            }
            catch
            {
                throw;
            }
            finally
            {

                if (Sqlcon.State == ConnectionState.Open)
                    Sqlcon.Close();
                Sqlcon = null;
                Sqlcmd = null;
                Sqlda = null;
                dset = null;
            }
        }
        public static DataSet GET_DEPARTMENT_TYPE()
        {

            SqlCommand Sqlcmd = null;
            SqlConnection Sqlcon = null;
            SqlDataAdapter Sqlda;
            DataSet dset = null;
            try
            {
                Sqlcon = new SqlConnection(Connection.Conn (UserSession.dbAddress));
                Sqlcon.Open();
                Sqlcmd = new SqlCommand("PROC_GET_DEPARTMENT_DETAILS", Sqlcon);
                Sqlcmd.CommandType = CommandType.StoredProcedure;
                Sqlcmd.CommandTimeout = 600;

                Sqlda = new SqlDataAdapter(Sqlcmd);
                dset = new DataSet();
                Sqlda.Fill(dset);
                dset.RemotingFormat = SerializationFormat.Binary;
                Sqlcon.Close();
                return dset;

            }
            catch
            {
                throw;
            }
            finally
            {

                if (Sqlcon.State == ConnectionState.Open)
                    Sqlcon.Close();
                Sqlcon = null;
                Sqlcmd = null;
                Sqlda = null;
                dset = null;
            }
        }
        public static string CreateDepartmentMaster(string postHindiName, string postEnglishName, int departmenttype, int parentDepartment, int departId, int isShashan, int isMandal, int isDistrict, int isTehsil, int isThana, int isBlock)
        {
            SqlConnection Sqlcon = null;
            SqlCommand Sqlcmd = null;
            try
            {
                string result = string.Empty;
                Sqlcon = new SqlConnection(Connection.Conn (UserSession.dbAddress));
                Sqlcon.Open();
                Sqlcmd = new SqlCommand("exec Proc_Save_DepartmentMaster_Details @departHindiName,@departEnglishName,@departmenttype,@parentDepartment,@departId,@isShashan,@isMandal,@isDistrict,@isTehsil,@isThana,@isBlock", Sqlcon);
                Sqlcmd.Parameters.AddWithValue("@departHindiName", postHindiName);
                Sqlcmd.Parameters.AddWithValue("@departEnglishName", postEnglishName);
                Sqlcmd.Parameters.AddWithValue("@departmenttype", departmenttype);
                Sqlcmd.Parameters.AddWithValue("@parentDepartment", parentDepartment);
                Sqlcmd.Parameters.AddWithValue("@departId", departId);
                Sqlcmd.Parameters.AddWithValue("@isShashan", isShashan);
                Sqlcmd.Parameters.AddWithValue("@isMandal", isMandal);
                Sqlcmd.Parameters.AddWithValue("@isDistrict", isDistrict);
                Sqlcmd.Parameters.AddWithValue("@isTehsil", isTehsil);
                Sqlcmd.Parameters.AddWithValue("@isThana", isThana);
                Sqlcmd.Parameters.AddWithValue("@isBlock", isBlock);
                result = (string)Sqlcmd.ExecuteScalar();
                Sqlcon.Close();
                return result;
            }
            catch
            {
                if (Sqlcon.State == ConnectionState.Open)
                    Sqlcon.Close();
                throw;
            }
            finally
            {

                if (Sqlcon.State == ConnectionState.Open)
                    Sqlcon.Close();
                Sqlcon = null;
                Sqlcmd = null;
            }

        }

        public static DataSet GET_Parent_Depart_Details(int departCode)
        {

            SqlCommand Sqlcmd = null;
            SqlConnection Sqlcon = null;
            SqlDataAdapter Sqlda;
            DataSet dset = null;
            try
            {
                Sqlcon = new SqlConnection(Connection.Conn (UserSession.dbAddress));
                Sqlcon.Open();
                Sqlcmd = new SqlCommand("PROC_Parent_Depart_Details", Sqlcon);
                Sqlcmd.Parameters.AddWithValue("@departCode", SqlDbType.Int).Value = departCode;
                Sqlcmd.CommandType = CommandType.StoredProcedure;
                Sqlcmd.CommandTimeout = 600;

                Sqlda = new SqlDataAdapter(Sqlcmd);
                dset = new DataSet();
                Sqlda.Fill(dset);
                dset.RemotingFormat = SerializationFormat.Binary;
                Sqlcon.Close();
                return dset;

            }
            catch
            {
                throw;
            }
            finally
            {

                if (Sqlcon.State == ConnectionState.Open)
                    Sqlcon.Close();
                Sqlcon = null;
                Sqlcmd = null;
                Sqlda = null;
                dset = null;
            }
        }
        public static DataSet Get_Search_Department_Details(string searchType, string deptHindiName, string deptEnglishName, int departmentType, int parentdepartment)
        {

            SqlCommand Sqlcmd = null;
            SqlConnection Sqlcon = null;
            SqlDataAdapter Sqlda;
            DataSet dset = null;
            try
            {
                Sqlcon = new SqlConnection(Connection.Conn (UserSession.dbAddress));
                Sqlcon.Open();
                Sqlcmd = new SqlCommand("Proc_Get_Search_Department_Details", Sqlcon);
                Sqlcmd.Parameters.AddWithValue("@searchType", SqlDbType.Int).Value = searchType;
                Sqlcmd.Parameters.AddWithValue("@deptHindiName", SqlDbType.NVarChar).Value = deptHindiName;
                Sqlcmd.Parameters.AddWithValue("@deptEnglishName", SqlDbType.NVarChar).Value = deptEnglishName;
                Sqlcmd.Parameters.AddWithValue("@departmentType", SqlDbType.Int).Value = departmentType;
                Sqlcmd.Parameters.AddWithValue("@parentdepartment", SqlDbType.Int).Value = parentdepartment;
                Sqlcmd.CommandType = CommandType.StoredProcedure;
                Sqlcmd.CommandTimeout = 600;

                Sqlda = new SqlDataAdapter(Sqlcmd);
                dset = new DataSet();
                Sqlda.Fill(dset);
                dset.RemotingFormat = SerializationFormat.Binary;
                Sqlcon.Close();
                return dset;

            }
            catch
            {
                throw;
            }
            finally
            {

                if (Sqlcon.State == ConnectionState.Open)
                    Sqlcon.Close();
                Sqlcon = null;
                Sqlcmd = null;
                Sqlda = null;
                dset = null;
            }
        }
        #endregion

        public static string SaveDioFeedback(int UserId, string Comments, string ContentType, int size, Byte[] bytes)
        {
            string result = string.Empty;
            SqlCommand Sqlcmd = null;
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(Connection.Conn (UserSession.dbAddress));
                con.Open();
                Sqlcmd = new SqlCommand("exec Proc_SaveDioFeedback @userId,@comments,@contentType,@size,@Data", con);
                Sqlcmd.Parameters.AddWithValue("@userId", UserId);
                Sqlcmd.Parameters.AddWithValue("@comments", Comments);
                Sqlcmd.Parameters.AddWithValue("@contentType", ContentType);
                Sqlcmd.Parameters.AddWithValue("@size", size);
                Sqlcmd.Parameters.AddWithValue("@Data", bytes);
                result = (string)Sqlcmd.ExecuteScalar();
                con.Close();
                return result;
            }
            catch
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                throw;
            }
            finally
            {

                if (con.State == ConnectionState.Open)
                    con.Close();
                con = null;
                Sqlcmd = null;
            }

        }

        public static DataSet GeneralReportColumn()
        {
            try
            {

                // Execute the stored procedure
                DataSet ds = new DataSet();
                ds = SqlHelper.ExecuteDataset(Connection.Conn (UserSession.dbAddress), CommandType.StoredProcedure, "proc_GetGeneralReportColumn", null);
                // create a string array of return values and assign values returned from stored procedure
                return ds;
            }
            catch
            {
                return null;
            }
        }

        public static DataSet Get_Department()
        {
            try
            {

                // Execute the stored procedure
                DataSet ds = new DataSet();
                ds = SqlHelper.ExecuteDataset(Connection.Conn (UserSession.dbAddress), CommandType.StoredProcedure, "SP_Get_Department", null);
                // create a string array of return values and assign values returned from stored procedure
                return ds;
            }
            catch
            {
                return null;
            }

        }

        public static string CreateOperatorUser_csc(string NameH, string NameE, string Email, string Phone, string UserName, string pass, string usertype, int UserLevel, int state, int DivisionId, int distict, int tahshil, int DepartmentId, int PostId, int thana, int block, int section, string rights, string address, string joiningdate, int id)
        {
            try
            {
                SqlParameter[] arParms = new SqlParameter[23];
                // @PersonID Input Parameter
                arParms[0] = new SqlParameter("@pNameH", SqlDbType.NVarChar, 200);
                arParms[0].Value = NameH;
                arParms[1] = new SqlParameter("@pNameE", SqlDbType.VarChar, 100);
                arParms[1].Value = NameE;
                arParms[2] = new SqlParameter("@pEmail", SqlDbType.NVarChar, 100);
                arParms[2].Value = Email;
                arParms[3] = new SqlParameter("@pPhone", SqlDbType.NVarChar, 40);
                arParms[3].Value = Phone;
                arParms[4] = new SqlParameter("@pUserName", SqlDbType.NVarChar, 40);
                arParms[4].Value = UserName;
                arParms[5] = new SqlParameter("@pPass", SqlDbType.NVarChar, 200);
                arParms[5].Value = pass;
                arParms[6] = new SqlParameter("@pUserType", SqlDbType.NVarChar, 200);
                arParms[6].Value = usertype;
                arParms[7] = new SqlParameter("@pUserLevel", SqlDbType.Int);
                arParms[7].Value = UserLevel;
                arParms[8] = new SqlParameter("@pStateId", SqlDbType.Int);
                arParms[8].Value = state;
                arParms[9] = new SqlParameter("@pDivisionId", SqlDbType.Int);
                arParms[9].Value = DivisionId;
                arParms[10] = new SqlParameter("@pDistictId", SqlDbType.Int);
                arParms[10].Value = distict;
                arParms[11] = new SqlParameter("@pTehshilId", SqlDbType.Int);
                arParms[11].Value = tahshil;
                arParms[12] = new SqlParameter("@pDepartmentId", SqlDbType.Int);
                arParms[12].Value = DepartmentId;
                arParms[13] = new SqlParameter("@pPostId", SqlDbType.Int);
                arParms[13].Value = PostId;
                arParms[14] = new SqlParameter("@pThana", SqlDbType.Int);
                arParms[14].Value = thana;
                arParms[15] = new SqlParameter("@pBlockId", SqlDbType.Int);
                arParms[15].Value = block;
                arParms[16] = new SqlParameter("@pRights", SqlDbType.VarChar, 500);
                arParms[16].Value = rights;
                arParms[17] = new SqlParameter("@psection", SqlDbType.Int);
                arParms[17].Value = section;
                arParms[18] = new SqlParameter("@paddress", SqlDbType.VarChar);
                arParms[18].Value = address;
                arParms[19] = new SqlParameter("@pjoiningdate", SqlDbType.VarChar);
                arParms[19].Value = joiningdate;
                arParms[20] = new SqlParameter("@pid", SqlDbType.Int);
                arParms[20].Value = id;
                // @ProductName Output Parameter
                arParms[21] = new SqlParameter("@pOutPut", SqlDbType.NVarChar, 40);
                arParms[21].Direction = ParameterDirection.Output;
                // @UnitPrice Output Parameter
                arParms[22] = new SqlParameter("@pMessage", SqlDbType.NVarChar, 200);
                arParms[22].Direction = ParameterDirection.Output;
                // Execute the stored procedure
                SqlHelper.ExecuteNonQuery(Connection.Conn (UserSession.dbAddress), CommandType.StoredProcedure, "Proc_CreateOPUser_CSC", arParms);
                // create a string array of return values and assign values returned from stored procedure
                string[] arReturnParms = new string[2];
                arReturnParms[0] = arParms[21].Value.ToString();
                arReturnParms[1] = arParms[22].Value.ToString();
                return arParms[21].Value.ToString();
            }
            catch
            {
                return null;
            }

        }

        public static int filter_bad_chars(string s)
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
            int er = 0;
            int sL_Char_Length = sL_Char.Length - 1;
            while (sL_Char_Length >= 0)
            {
                if (s.Contains(sL_Char[sL_Char_Length]))
                {
                    er = 1;
                    break; // TODO: might not be correct. Was : Exit While
                }
                sL_Char_Length -= 1;
            }
            return er;
        }
        public static string filter_bad_chars_rep(string s)
        {
            string[] sL_Char = {
			"onfocus",	"\"\"",	"=","onmouseover","prompt","%27","alert#","alert","'or","`or","`or`",	"'or'",	"'='",	"`=`",	"'","`","9,9,9","src","onload",	"select","drop",
			"insert","delete","xp_","having","union","group","update","script",	"<script","</script>",
			"language",			"javascript",			"vbscript",			"http",			"~",			"$",			"<",			">",			"%",			"\\",
			";",			"@",			"_",			"{",			"}",			"^",			"?",			"[",			"]",			"!",			"#",			"&",
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

        

        internal static DataSet GetUserDetail(string DistCode, string DepCode,string OfficeCode, string Cond)
        {
            try
            {
                SqlParameter[] arParms = new SqlParameter[10];
                // @PersonID Input Parameter

                arParms[0] = new SqlParameter("@Parm1", SqlDbType.Int);
                arParms[0].Value = DistCode;

                arParms[1] = new SqlParameter("@Parm2", SqlDbType.NVarChar, 20);
                arParms[1].Value = DepCode;

                arParms[2] = new SqlParameter("@Parm3", SqlDbType.NVarChar, 20);
                arParms[2].Value = OfficeCode;

                arParms[3] = new SqlParameter("@Parm4", SqlDbType.NVarChar, 20);
                arParms[3].Value = Cond;

                // Execute the stored procedure
                DataSet ds = new DataSet();
                ds = SqlHelper.ExecuteDataset(Connection.Conn (UserSession.dbAddress), CommandType.StoredProcedure, "Proc_OfficeDDLDepartWise", arParms);
                // create a string array of return values and assign values returned from stored procedure
                return ds;
            }
            catch
            {
                return null;
            }
        }

        internal static DataSet Get_RandomCode_Details(int p1, int p2)
        {
            SqlCommand Sqlcmd = null;
            SqlConnection Sqlcon = null;
            SqlDataAdapter Sqlda;
            DataSet dset = null;
            try
            {
                Sqlcon = new SqlConnection(Connection.Conn (UserSession.dbAddress));
                Sqlcon.Open();
                Sqlcmd = new SqlCommand("proc_RandomGenCode ", Sqlcon);
                Sqlcmd.Parameters.AddWithValue("@dist", p1);
                Sqlcmd.Parameters.AddWithValue("@dep", p2);
                //Sqlcmd.Parameters.Add("@office_code", office_code);
                Sqlcmd.CommandType = CommandType.StoredProcedure;
                Sqlcmd.CommandTimeout = 600;

                Sqlda = new SqlDataAdapter(Sqlcmd);
                dset = new DataSet();
                Sqlda.Fill(dset);
                dset.RemotingFormat = SerializationFormat.Binary;
                Sqlcon.Close();
                return dset;

            }
            catch
            {
                throw;
            }
            finally
            {

                if (Sqlcon.State == ConnectionState.Open)
                    Sqlcon.Close();
                Sqlcon = null;
                Sqlcmd = null;
                Sqlda = null;
                dset = null;
            }
        }

        public static DataSet GetOfficeMobileNoDataEntryPassword(int DistrictCode, int cond)
        {
            try
            {
                SqlParameter[] arParms = new SqlParameter[10];
                // @PersonID Input Parameter

                arParms[0] = new SqlParameter("@DistCode", SqlDbType.Int );
                arParms[0].Value = DistrictCode;

                arParms[1] = new SqlParameter("@cond", SqlDbType.Int);
                arParms[1].Value = cond;

                // Execute the stored procedure
                DataSet ds = new DataSet();
                ds = SqlHelper.ExecuteDataset(Connection.Conn (UserSession.dbAddress), CommandType.StoredProcedure, "Proc_GetOfficeMobileNoDataEntryPassword", arParms);
                // create a string array of return values and assign values returned from stored procedure
                return ds;
            }
            catch (Exception )
            {
                return null;
            }
        }

        #region Nodal user Update
        public static string UpdateUserDetailsNodal(int officerId, string NameE, string OfAdd, string Mobile_No, int Department, int District, string EmailId, string userName, string section, string Designation, string ofcNo)
        {

            try
            {
                SqlParameter[] param = {
                      new SqlParameter("@officerId",officerId),
                      new SqlParameter("@Mobile_No",Mobile_No),
                      new SqlParameter("@Office_Add",OfAdd),
                      new SqlParameter("@Officername",NameE),                    
                      //new SqlParameter("@OfficernameH",NameH),                    
                      //new SqlParameter("@UserLevel",UserLevel),
                      new SqlParameter("@Department",Department),
                      //new SqlParameter("@JoiningDate",JoiningDate),
                      //new SqlParameter("@Post",Post),
                       //new SqlParameter("@Div",Div),
                      new SqlParameter("@District",District),
                      //new SqlParameter("@Tehsil",Tehsil),
                      //new SqlParameter("@Thana",Thana),
                      //new SqlParameter("@Block",Block),
                      new SqlParameter("@EmailId",EmailId),
                      new SqlParameter("@userName",userName),
                      //new SqlParameter("@usertype",usertype),
                      //new SqlParameter("@userRights",str),
                      //new SqlParameter("@pass",paas),
                      new SqlParameter("@section",section),
                      new SqlParameter("@NodalDesignation",Designation),
                      new SqlParameter("@NodalMob",ofcNo)
                   };

                string res = SqlHelper.ExecuteScalar(Connection.Conn (UserSession.dbAddress), CommandType.StoredProcedure, "Proc_ChangeNodalOffice", param).ToString();
                return res.ToString();
            }
            catch
            {
                return null;
            }
        }
        #endregion


        #region Fetch Nodal Officer Details
        public static string GetNodalOfficeDetails(int District, int Department, string userName)
        {
            try
            {
                SqlParameter[] param = {                      
                      new SqlParameter("@Dep",Department),                     
                      new SqlParameter("@DistCode",District),                     
                      new SqlParameter("@username",userName),
                   };

                string res = SqlHelper.ExecuteScalar(Connection.Conn (UserSession.dbAddress), CommandType.StoredProcedure, "Proc_FetchNodalOfficeDetails", param).ToString();
                return res.ToString();
            }
            catch
            {
                return null;
            }
        }
        #endregion



        internal static DataSet VerifyOfficePin(int p1, int p2)
        {
            try
            {
                SqlParameter[] arParms = new SqlParameter[10];
                // @PersonID Input Parameter

                arParms[0] = new SqlParameter("@DistCode", SqlDbType.Int);
                arParms[0].Value = p1;

                arParms[1] = new SqlParameter("@depcode", SqlDbType.Int);
                arParms[1].Value = p2;

               
                // Execute the stored procedure
                DataSet ds = new DataSet();
                ds = SqlHelper.ExecuteDataset(Connection.Conn (UserSession.dbAddress), CommandType.StoredProcedure, "proc_checkRandomPass", arParms);
                // create a string array of return values and assign values returned from stored procedure
                return ds;
            }
            catch (Exception )
            {
                return null;
            }
        }

        public static int GetDepartmentType(int DepaertID,int cond)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@DepartCode", DepaertID));
            parameters.Add(new SqlParameter("@cond", cond));
            int res = Convert.ToInt32(SqlHelper.ExecuteScalar(Connection.Conn (UserSession.dbAddress), CommandType.StoredProcedure, "Proc_getDepartmentTypeCenterState", parameters.ToArray()));
            return res;
        }

        public static DataTable GetStateTotal()
        {
            try
            {
                SqlParameter[] arParms = new SqlParameter[10];
                // @PersonID Input Parameter

                //arParms[0] = new SqlParameter("@officerId", SqlDbType.Int);
                //arParms[0].Value = officerId;

                //arParms[1] = new SqlParameter("@LoginUserLevel", SqlDbType.NVarChar, 20);
                //arParms[1].Value = UserLevel;

                // Execute the stored procedure
                DataSet ds = new DataSet();
                ds = SqlHelper.ExecuteDataset(Connection.Conn (UserSession.dbAddress), CommandType.StoredProcedure, "Proc_CompileTotalStateWiseData", arParms);
                // create a string array of return values and assign values returned from stored procedure
                return  ds.Tables[0];
            }
            catch
            {
                return null;
            }
        }

        public static DataSet GetComilpeDataEPDSsum()
        {
            try
            {
                SqlParameter[] arParms = new SqlParameter[10];
                // @PersonID Input Parameter

                //arParms[0] = new SqlParameter("@officerId", SqlDbType.Int);
                //arParms[0].Value = officerId;

                //arParms[1] = new SqlParameter("@LoginUserLevel", SqlDbType.NVarChar, 20);
                //arParms[1].Value = UserLevel;

                // Execute the stored procedure
                DataSet ds = new DataSet();
                ds = SqlHelper.ExecuteDataset(Connection.Conn (UserSession.dbAddress), CommandType.StoredProcedure, "Proc_ComilpeDataEPDSsum", arParms);
                // create a string array of return values and assign values returned from stored procedure
                return ds;
            }
            catch
            {
                return null;
            }
        }

        /*********** salman 25-11-2016 ************/

        public static int GetDepartmentTypeFromDep(int Districtcode, int DepaertID, int OfficeCode, int cond)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@parm1", Districtcode));
            parameters.Add(new SqlParameter("@parm2", DepaertID));
            parameters.Add(new SqlParameter("@parm3", OfficeCode));
            parameters.Add(new SqlParameter("@cond", cond));
            int res = Convert.ToInt32(SqlHelper.ExecuteScalar(Connection.Conn (UserSession.dbAddress), CommandType.StoredProcedure, "proc_GetOfficeDeparTypetWiseForPPReport", parameters.ToArray()));
            return res;
        }


        public static string GetOfficeFreezeStatus(string District, string Dep, int Dep_code, int cond, string isExempt)
        {

            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@DistCode",District),
                    new SqlParameter("@DepartCode",Dep),
                    new SqlParameter("@officeCode",Dep_code),
                    new SqlParameter("@cond",cond),
                    new SqlParameter("@exem",isExempt),                    
                                       };
                string res = SqlHelper.ExecuteScalar(Connection.Conn (UserSession.dbAddress), CommandType.StoredProcedure, "Proc_EmployeeReport", param).ToString();

                return res.ToString();
            }
            catch
            {
                return null;
            }
        }  

    }
}