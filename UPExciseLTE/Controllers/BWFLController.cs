using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UPExciseLTE.BLL;
using UPExciseLTE.DAL;
using UPExciseLTE.Filters;
using UPExciseLTE.Models;

namespace UPExciseLTE.Controllers
{
    [SessionExpireFilter]
    //[CheckAuthorization]
    [HandleError(ExceptionType = typeof(DbUpdateException), View = "Error")]
    public class BWFLController : Controller
    {
        // GET: BWFL
        [HttpGet]
        public ActionResult FormFL21()
        {
            FormFL21 FL21 = new FormFL21();
            if (Request.QueryString["Code"] != null && Request.QueryString["Code"].Trim() != string.Empty)
            {
                FL21 = new CommonBL().GetFormFL21(int.Parse(new Crypto().Decrypt(Request.QueryString["Code"].Trim())),"Z");
            }
            ViewBag.Brand = CommonBL.fillBrand("S");
            DataSet ds = new CommonDA().GetUnitDetails(-1, "", "", -1, -1);
            if (FL21.FromConsignorName.Trim()==string.Empty)
            {
                FL21.ToConsigeeName = ds.Tables[0].Rows[0]["UnitName"].ToString().Trim();
                FL21.ToLicenceNo = ds.Tables[0].Rows[0]["UnitLicenseno"].ToString().Trim();
                FL21.ToConsigeeAddress = ds.Tables[0].Rows[0]["UnitAddress"].ToString().Trim();
            }
            if (TempData["str"]!=null)
            {
                ViewBag.Msg = TempData["str"];
            }
            return View(FL21);
        }
        [HttpPost]
        public ActionResult FormFL21(FormFL21 FL)
        {
            string str = new CommonDA().InsertUpdateFormFL21(FL);
            TempData["str"] = str;
            return RedirectToAction("FormFL21");
        }
        [HttpGet]
        public ActionResult FinalizeFormFL21()
        {
            return View(new CommonBL().GetFormFL21List(-1, "P"));
        }
        public string FinalFormFL21(string FL21ID, string status, string reason)
        {
            string str = "";
            try
            {
                FormFL21 FL21 = new FormFL21();
                FL21.FL21ID = int.Parse(FL21ID);
                FL21.SPType = 3;
                FL21.FL21Status = status;
                str = new CommonDA().InsertUpdateFormFL21(FL21);
            }
            catch (Exception x)
            {
                str = x.Message.ToString();
            }
            return str;
        }
        [HttpGet]
        public ActionResult FormFL21List()
        {
            return View(new CommonBL().GetFormFL21List(-1, "Z"));
        }
        [HttpGet]
        public ActionResult PreviewFL22()
        {
            FormFL21 FL21 = new FormFL21();
            if (Request.QueryString["Code"] != null && Request.QueryString["Code"].Trim() != string.Empty)
            {
                FL21 = new CommonBL().GetFormFL21(int.Parse(new Crypto().Decrypt(Request.QueryString["Code"].Trim())), "Z");
            }
            return View(FL21);
        }
    }
}