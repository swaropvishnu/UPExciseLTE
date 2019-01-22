using System;
using System.Collections.Generic;
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
    [HandleError(ExceptionType = typeof(DbUpdateException), View = "Error")]
    public class BottlingPlanController : Controller
    {
        [HttpGet]
        public ActionResult BottelingPlan()
        {
            BottelingPlan BP = new BottelingPlan();
            ViewBag.Msg = TempData["Message"];
            ViewBag.Brand = CommonBL.fillBrand("S");
            ViewBag.Msg = TempData["Message"];
            if (Request.QueryString["A"] != null && Request.QueryString["A"].ToString().Trim() != string.Empty)
            {
                BP = (new CommonBL().GetBottelingPlan(CommonBL.Setdate("01/01/1900"), DateTime.Now, short.Parse(CommonBL.fillBrewery()[0].Value), -1, "", "", int.Parse(new Crypto().Decrypt(Request.QueryString["A"].Trim())), "PB"));
            }
            return View(BP);
        }
        [HttpPost]
        public ActionResult BottelingPlan(BottelingPlan BP)
        {
            try
            {
                BP.DateOfPlan = CommonBL.Setdate(BP.DateOfPlan1);
            }
            catch
            {
                ViewBag.Msg = "Invalid Date. Please use dd/MM/yyyy format.";
                return View(BP);
            }

            string str = new CommonDA().InsertUpdatePlanBWFL(BP);
            TempData["Message"] = str;
            if (BP.Type == 1)
            {
                return RedirectToAction("BottelingPlan");
            }
            else
            {
                return RedirectToAction("BottelingPlan", new { A = BP.EncPlanId });
            }
        }
        [HttpGet]
        public ActionResult EditFinalBottelingPlan()
        {
            return View(new CommonBL().GetBottelingPlanList(CommonBL.Setdate("01/01/1900"), DateTime.Now, short.Parse(CommonBL.fillBrewery()[0].Value), -1, "Z", "", -1, "PB"));
        }
        public string FinalizePlan(string PlanId)
        {
            string str = "";
            try
            {
                BottelingPlan Plan = new BottelingPlan();
                PlanId = new Crypto().Decrypt(PlanId);
                Plan.Type = 3;
                Plan.PlanId = int.Parse(PlanId);
                Plan.IsPlanFinal = 1;
                str = new CommonDA().InsertUpdatePlan(Plan);
            }
            catch (Exception x)
            {
                str = x.Message.ToString();
            }
            return str;
        }
    }
}