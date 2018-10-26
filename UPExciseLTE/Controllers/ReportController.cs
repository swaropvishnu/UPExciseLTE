using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UPExciseLTE.DAL;

namespace UPExciseLTE.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report

        [HttpGet]
        public ActionResult GetGatePassForDistrictWholesaleToRetailor()
        {
           return View();
        }


        [HttpGet]
        public ActionResult GetGatePassForDistrictWholesaleToRetailor1()
        {
            var applicationAllotmentShopList = new CommonDA().GetGatePassForDistrictWholesaleToRetailor();
            return Json(new { data = applicationAllotmentShopList }, JsonRequestBehavior.AllowGet);
        }

    }
}