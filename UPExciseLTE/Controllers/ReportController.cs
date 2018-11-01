using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UPExciseLTE.DAL;
using UPExciseLTE.App_Code;
using UPExciseLTE.Models;

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
            var applicationAllotmentShopList = "";//new CommonDA().GetGatePassForDistrictWholesaleToRetailor();
            return Json(new { data = applicationAllotmentShopList }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GatePassReport()
        {
            string passtype;
            string reciever;
            if (Convert.ToInt32(UserSession.LoggedInUserLevelId) == 45)
            {
                passtype = "FL-B11";
            }
            else if (Convert.ToInt32(UserSession.LoggedInUserLevelId) == 35)
            {
                passtype = "FL-36";
            }
            else if (Convert.ToInt32(UserSession.LoggedInUserLevelId) == 15)
            {
                passtype = "B-12";
            }
            else
                passtype = "FL-B11";


            if (Convert.ToInt32(UserSession.LoggedInUserLevelId) == 45)
            {
                reciever = "Lucknow Warehouse";
            }
            else if (Convert.ToInt32(UserSession.LoggedInUserLevelId) == 35)
            {
                reciever = "Sahu & Sons Kanpur Road";
            }
            else if (Convert.ToInt32(UserSession.LoggedInUserLevelId) == 15)
            {
                reciever = "M/s Mohan Goldwater Breweries Ltd. Warehouse";
            }
            else
                reciever = "FL-B11";


            ViewBag.Reciever = reciever;
            ViewBag.PassType = passtype;


            return View();
        }


    }
}