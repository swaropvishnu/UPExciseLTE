using UPExciseLTE.BLL;
using UPExciseLTE.Filters;
using UPExciseLTE.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace UPExciseLTE.Controllers
{
    
    //[CheckAuthorization]
    //[HandleError(ExceptionType = typeof(DbUpdateException), View = "Error")]
    public class LoginController : Controller
    {
        
        // GET: Login
        public ActionResult Login()
        {
            string salt = CreateSalt(5);
            Session["salt"] = salt.ToString();
            return View();
        }
        
        public ActionResult LogOut()
        {
            try
            {
                // First we clean the authentication ticket like always
                //required NameSpace: using System.Web.Security;
                FormsAuthentication.SignOut();
                // Second we clear the principal to ensure the user does not retain any authentication
                //required NameSpace: using System.Security.Principal;
                HttpContext.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);
                Session.Clear();
                System.Web.HttpContext.Current.Session.RemoveAll();
                // Last we redirect to a controller/action that requires authentication to ensure a redirect takes place
                // this clears the Request.IsAuthenticated flag since this triggers a new request
                Session.Abandon(); // it will clear the session at the end of request
                return RedirectToAction("Login", "Login");
            }
            catch
            {
                throw;
            }
            finally
            {
                Session.Abandon();
            }
        }
        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginModal Model)
        {

            if (Model.CaptchaText == HttpContext.Session["captchastring"].ToString())
            {
                // Ensure we have a valid viewModel to work with
                //if (!ModelState.IsValid)
                //    return View(Model);
                string salt = CreateSalt(5);
                string usrname = Model.UserName;
                string password = Model.Password;
                DataSet ds = new DataSet();
                ds = UserDtl.VerifyUser(usrname);
                if (ds != null)
                {
                    
                    if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows.Count == 1)
                    {
                        string psw = ds.Tables[0].Rows[0]["Password"].ToString();

                        string hashed_pwd = CalculateHash(psw.ToString().ToLower() + Session["salt"].ToString());
                        if (hashed_pwd.ToString().ToLower().Equals(Model.Password.ToLower()))
                        {
                            //if (ds.Tables[0].Rows[0]["UserLevel"].ToString().Trim() == "6" || ds.Tables[0].Rows[0]["UserLevel"].ToString().Trim() == "3" || ds.Tables[0].Rows[0]["UserLevel"].ToString().Trim() == "5")
                            //{
                                Session["tbl_Session"] = ds.Tables[0];
                                FormsAuthentication.SetAuthCookie(usrname, Model.RememberMe);
                                return RedirectToAction("Dashboard", "Dashboard");
                            //}
                            //else
                            //{
                            //    ViewBag.ErrMessage = "Invalid Username or Password.";
                            //    return RedirectToAction("Login", "Login");
                            //}

                        }
                        else
                        {
                            //Login Fail
                            ViewBag.ErrMessage = "Access Denied! Wrong Credential";
                            //return RedirectToAction("Login", "Login");
                            return View();
                            //return RedirectToAction("Login", "Login");
                        }
                    }
                    else
                    {
                        ViewBag.ErrMessage = "Invalid Username or Password.";
                        //return RedirectToAction("Login", "Login");
                        return View();
                    }
                }
                else
                {
                    ViewBag.ErrMessage = "Invalid Username or Password.";
                    //return RedirectToAction("Login", "Login");
                    return View();
                }
            }
            else
            {
                ViewBag.ErrMessage = "Error: captcha is not valid.";
                //return RedirectToAction("Login", "Login");
                return View();
            }
        }
        public CaptchaImageResult ShowCaptchaImage()
        {
            return new CaptchaImageResult();
        }
        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="returnUrl"></param>
        /// <param name="rememberMe"></param>
        /// <returns></returns>
        /// 
        #region --> Generate HASH Using SHA512

        #endregion
        private string CreateSalt(int size) //Generate the salt via Randon Number Genertor cryptography
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }
        private string CalculateHash(string input)
        {
            using (var algorithm = SHA256.Create()) //or MD5 SHA512 etc.
            {
                var hashedBytes = algorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
        ///////////////////////
        [SessionExpireFilterAttribute]
        public ActionResult FirstTimeLogin()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FirstTimeLogin(LoginModal IstTimeLogin)
        {
            DataSet ds = new DataSet();
            ds = UserDtl.VerifyUser(UserSession.LoggedInUserName.ToString());
            //btnlogin.Attributes.Add("onclick", "return HashPwdwithSalt('" + salt.ToString() + "');");
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows.Count == 1)
                {
                    string psw = ds.Tables[0].Rows[0]["Password"].ToString();
                    string lpsw = ds.Tables[0].Rows[0]["OldPassWord"].ToString();
                    string type_pwd_salt = FormsAuthentication.HashPasswordForStoringInConfigFile(IstTimeLogin.NewPassword.ToString(), "md5");
                    string hashed_pwd = IstTimeLogin.OldPassword;
                    string hashed_newpwd = type_pwd_salt;
                    if ((lpsw.ToLower().Equals(hashed_newpwd.ToLower())) == true)
                    {
                        ViewBag.ErrMessage = "Your new password should not match with last old password ?";
                        return RedirectToAction("Login", "FirstTimeLogin");
                    }
                    if ((psw.ToLower().Equals(hashed_newpwd.ToLower())) == false)
                    {
                        if (psw.ToLower().Equals(hashed_pwd.ToLower()))
                        {
                            string hashed_pwdNew = type_pwd_salt;
                            string res = UserDtl.Userpasswordchange(UserSession.LoggedInUserName, hashed_pwdNew.ToLower(), hashed_pwd.ToLower());
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('" + res + "'); window.location='/EPDS2017/logout.aspx';", true);
                            return RedirectToAction("Login", "Login");
                        }
                        else
                        {
                            ViewBag.ErrMessage = "Your old Password is not correct ?";
                            return RedirectToAction("Login", "FirstTimeLogin");
                        }
                    }
                    else
                    {
                        ViewBag.ErrMessage = "Your new Password mathced with old password Please change new password. ?";
                        return RedirectToAction("Login", "FirstTimeLogin");
                    }
                }
                else
                {
                    return RedirectToAction("Login", "FirstTimeLogin");
                }
            }
            else
            {
                ViewBag.ErrMessage = "Invalid Username or Password.";
                return RedirectToAction("Login", "Login");
            }
        }
        //POST: Logout
        [HttpPost]
        [Authorize]
        public ActionResult Logout()
        {
            try
            {
                // First we clean the authentication ticket like always
                //required NameSpace: using System.Web.Security;
                FormsAuthentication.SignOut();
                // Second we clear the principal to ensure the user does not retain any authentication
                //required NameSpace: using System.Security.Principal;
                HttpContext.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);
                Session.Clear();
                Session.Abandon();
                System.Web.HttpContext.Current.Session.RemoveAll();
                // Last we redirect to a controller/action that requires authentication to ensure a redirect takes place
                // this clears the Request.IsAuthenticated flag since this triggers a new request
                //return RedirectToLocal();
                return View();
            }
            catch
            {
                throw;
            }
        }
        [SessionExpireFilterAttribute]
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionExpireFilterAttribute]
        public ActionResult ChangePassword(LoginModal ChangePwd)
        {
            DataSet ds = new DataSet();
            ds = UserDtl.VerifyUser(UserSession.LoggedInUserName.ToString());
            //btnlogin.Attributes.Add("onclick", "return HashPwdwithSalt('" + salt.ToString() + "');");
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows.Count == 1)
                {
                    string psw = ds.Tables[0].Rows[0]["Password"].ToString();
                    string lpsw = ds.Tables[0].Rows[0]["OldPassWord"].ToString();
                    string pwd_salt = FormsAuthentication.HashPasswordForStoringInConfigFile(ChangePwd.OldPassword_CHG.ToString(), "sha256");
                    string type_pwd_salt = FormsAuthentication.HashPasswordForStoringInConfigFile(ChangePwd.NewPassword_CHG.ToString(), "sha256");

                    string hashed_pwd = pwd_salt;
                    string hashed_newpwd = type_pwd_salt;
                    if ((lpsw.ToLower().Equals(hashed_newpwd.ToLower())) == true)
                    {
                        ViewBag.ErrMessage = "Your new password should not match with last old password ?";
                        //return RedirectToAction("Index", "Default");
                    }
                    if ((psw.ToLower().Equals(hashed_newpwd.ToLower())) == false)
                    {
                        if (psw.ToLower().Equals(hashed_pwd.ToLower()))
                        {
                            string hashed_pwdNew = type_pwd_salt;
                            string res = UserDtl.Userpasswordchange(UserSession.LoggedInUserName, hashed_pwdNew.ToLower(), hashed_pwd.ToLower());
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('" + res + "'); window.location='/EPDS2017/logout.aspx';", true);
                            //return RedirectToAction("ChangePassword", "Login");
                            ViewBag.ErrMessage = "Your Password Changed Successfully...";
                        }
                        else
                        {
                            ViewBag.ErrMessage = "Your old Password is not correct ?";
                            //return RedirectToAction("Index", "Default");
                        }
                    }
                    else
                    {
                        ViewBag.ErrMessage = "Your new Password mathced with old password Please change new password. ?";
                        //return RedirectToAction("Index", "Default");
                    }
                }
                else
                {
                    //return RedirectToAction("Index", "Default");
                }
            }
            else
            {
                ViewBag.ErrMessage = "Invalid Username or Password.";
                //return RedirectToAction("FirstTimeLogin", "Login");
            }
            return View();
        }
        [SessionExpireFilterAttribute]
        public ActionResult ProfileUpdate()
        {
            LoginModal objUserData = new LoginModal();
            objUserData.UserId = UserSession.LoggedInUserId.ToString();
            objUserData = CommonBL.GetUserDetail(objUserData);
            ViewBag.Base64String = "data:image/png;base64," + Convert.ToBase64String(objUserData.UserImage, 0);
            return View(objUserData);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionExpireFilterAttribute]
        public ActionResult ProfileUpdate(LoginModal objUserData, HttpPostedFileBase pimg)
        {
            Byte[] img = null;
            if (pimg != null && pimg.ContentLength > 0)
            {   /*****IMG-DB-CODE******/
                int FileSize = pimg.ContentLength;
                img = new Byte[FileSize];
                pimg.InputStream.Read(img, 0, FileSize);
                objUserData.UserImage = img;
            }
            objUserData.UserId = UserSession.LoggedInUserId.ToString();

            string A = CommonBL.UpdateUserDetail(objUserData);
            ViewBag.Base64String = "data:image/png;base64," + Convert.ToBase64String(objUserData.UserImage, 0);
            if (A == "Success")
            {
                ViewBag.AlertUpdate = "Update SuccessFully..";
            }
            else
            {
                ViewBag.AlertUpdate = "Failed To Update Profile..";
            }
            return View(objUserData);
        }
        public ActionResult Error()
        {
            return View();
        }
        public ActionResult RegistrationLogin()
        {
            return View();
        }
        //public ActionResult Registration_Login()
        //{
        //    string salt = CreateSalt(5);
        //    Session["salt"] = salt.ToString();
        //    List<SelectListItem> Scheme = new List<SelectListItem>();
        //    CMODataEntryBLL.bindDropDownHnGrid("proc_Detail", Scheme, "y", "", "");
        //    CM.Scheme = Scheme;
        //    return View(CM);
        //}
        
        public JsonResult Applicant_Login(LoginModal Model)
        {
            string[] result = new string[2];
            try
            {
                if (HttpContext.Session["captchastring"] == null)
                {
                    result[0] = "Fail";
                    result[1] = "Sorry ,Please Refresh the Page";
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (Model.CaptchaText == HttpContext.Session["captchastring"].ToString())
                {
                    string salt = CreateSalt(5);
                    string usrname = Model.UserName;
                    string password = Model.Password;
                    DataSet ds = new DataSet();
                    ds = UserDtl.VerifyApplicant(usrname, Model.yojana_code);
                    if (ds != null)
                    {

                        if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows.Count == 1)
                        {
                            string psw = ds.Tables[0].Rows[0]["Password"].ToString();
                            string hashed_pwd = CalculateHash(psw.ToString().ToLower() + Session["salt"].ToString());
                            if (hashed_pwd.ToString().ToLower().Equals(Model.Password.ToLower()))
                            {
                                if (ds.Tables[0].Rows[0]["UserLevel"].ToString().Trim() == "30")
                                {
                                    Session["tbl_Session"] = ds.Tables[0];
                                    FormsAuthentication.SetAuthCookie(usrname, Model.RememberMe);
                                    result[0] = "Sucess";
                                    result[1] = "/User/" + ds.Tables[0].Rows[0]["page_name"].ToString();
                                    return Json(result, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    result[0] = "Fail";
                                    result[1] = "Invalid Username or Password.";
                                    ViewBag.ErrMessage = "Invalid Username or Password.";
                                    return Json(result, JsonRequestBehavior.AllowGet);
                                }
                            }
                            else
                            {
                                result[0] = "Fail";
                                result[1] = "Access Denied! Wrong Credential";
                                TempData["ErrorMSG"] = "Access Denied! Wrong Credential";
                                return Json(result, JsonRequestBehavior.AllowGet);
                                //return RedirectToAction("Login", "Login");
                            }
                        }
                        else
                        {
                            result[0] = "Fail";
                            result[1] = "Invalid Username or Password.";
                            ViewBag.ErrMessage = "Invalid Username or Password.";
                            return Json(result, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        result[0] = "Fail";
                        result[1] = "Invalid Username or Password.";
                        ViewBag.ErrMessage = "Invalid Username or Password.";
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    result[0] = "Fail";
                    result[1] = "Error: captcha is not valid.";
                    ViewBag.ErrMessage = "Error: captcha is not valid.";
                    return Json(result, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                result[0] = "Fail";
                result[1] = "Sorry ,Please Refresh the Page";
                ViewBag.ErrMessage = ex.Message;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
        //public JsonResult Getsponsoring_office(int @district_code_census)
        //{
        //    DataTable dt = new DAL.CommonDA().Getsponsoring_office(@district_code_census);
        //    List<sponsoring_office> SL = new List<sponsoring_office>();
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        sponsoring_office s1 = new sponsoring_office();
        //        s1.sponsoring_office_code = int.Parse(dt.Rows[i]["sponsoring_office_code"].ToString().Trim());
        //        s1.address = dt.Rows[i]["address"].ToString().Trim();
        //        SL.Add(s1);
        //    }
        //    return Json(SL, JsonRequestBehavior.AllowGet);
        //}

        class sponsoring_office
        {
            public int sponsoring_office_code { get; set; }
            public string address { get; set; }
        }
        public ActionResult Track_staus()
        {
            return View();
        }

    }
}