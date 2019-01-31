using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UPExciseLTE.BLL;
using UPExciseLTE.DAL;
using UPExciseLTE.Models;
using System.Data.Entity.Infrastructure;
using UPExciseLTE.Filters;

namespace UPExciseLTE.Controllers
{
    [SessionExpireFilterAttribute]
    //[NoCache]
    [ChkAuthorization]
    [HandleError(View = "Error")]
    public class RetailerController : Controller
    {
        public ActionResult ReceiveGatePassWH()
        {
            DataSet ds = new CommonDA().GetUnitDetails(-1, "", "", -1, -1, -1, UserSession.LoggedInUserId);
            List<GatePassDetails> lstGPD = new List<GatePassDetails>();
            lstGPD = new CommonBL().GetGatePassDetailsList(-1, CommonBL.Setdate("01/01/1900"), CommonBL.Setdate("31/12/4000"), 6, "A", "P", "", ds.Tables[0].Rows[0]["UnitLicenseno"].ToString().Trim(), "", ds.Tables[0].Rows[0]["UnitLicenseType"].ToString().Trim());
            return View(lstGPD);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public string ReceiveGatePass(string GatePassId)
        {
            DataTable dt = new DataTable();
            if (Session["CaseCode"] != null)
            {
                dt = Session["CaseCode"] as DataTable;
            }
            string str = new CommonDA().ReceiveGatePass(long.Parse(GatePassId.Trim()), 3, dt);
            return str;
        }
        [HttpGet]
        public ActionResult StockBalance()
        {
            try
            {
                DataSet dsStockBalance = new DataSet();
                dsStockBalance = new CommonDA().GetStockBalanceDetail(3);
                ViewData["StockBalance"] = dsStockBalance.Tables[0];
                return View(ViewData["StockBalance"]);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        #region CL
        [HttpGet]
        public ActionResult StockBalanceCL()
        {
            try
            {
                DataSet dsStockBalance = new DataSet();
                dsStockBalance = new CommonDA().GetStockBalanceDetailCL(3);
                ViewData["StockBalance"] = dsStockBalance.Tables[0];
                return View(ViewData["StockBalance"]);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        public ActionResult ReceiveGatePassWHCL()
        {
            DataSet ds = new CommonDA().GetUnitDetails(-1, "", "", -1, -1, -1, UserSession.LoggedInUserId);
            List<GatePassDetailsCL> lstGPD = new List<GatePassDetailsCL>();
            lstGPD = new CommonBL().GetGatePassDetailsListCL(-1, CommonBL.Setdate("01/01/1900"), CommonBL.Setdate("31/12/4000"), 6, "A", "P", "", ds.Tables[0].Rows[0]["UnitLicenseno"].ToString().Trim(), "", ds.Tables[0].Rows[0]["UnitLicenseType"].ToString().Trim());
            return View(lstGPD);
        }
        public string ReceiveGatePassCL(string GatePassId, string DamageBottles)
        {
            string str = new CommonDA().FinalGatePass(long.Parse(GatePassId.Trim()), 2, int.Parse(DamageBottles.Trim()));
            return str;
        }
        #endregion
    }
}