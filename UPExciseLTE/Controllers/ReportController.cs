using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UPExciseLTE.DAL;
using UPExciseLTE.App_Code;
using UPExciseLTE.Models;
using UPExciseLTE.BLL;

namespace UPExciseLTE.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report

        [HttpGet]
        public ActionResult GetGatePassForDistrictWholesaleToRetailor()
        {
            var loggedInUserInformation = GetInformationByLoggedInUserLeve();
            ViewBag.PassTypeInformation = loggedInUserInformation.PassTypeInformation;
            return View();
        }


        [HttpGet]
        public ActionResult GetGatePassForDistrictWholesaleToRetailor1()
        {
            var applicationAllotmentShopList = new CommonBL().GetGatePassReport();
            return Json(new { data = applicationAllotmentShopList }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GatePassReport()
        {
            var loggedInUserInformation = GetInformationByLoggedInUserLeve();
            ViewBag.Reciever = loggedInUserInformation.Receiver;
            ViewBag.PassType = loggedInUserInformation.PassType;
            return View();
        }


        public ActionResult BarCodeProducution()
        {
            return View();
        }

        private DistrictWholeSaleToRetailorModel GetInformationByLoggedInUserLeve()
        {
            var loggedInUserInformation = new DistrictWholeSaleToRetailorModel();
            if (Convert.ToInt32(UserSession.LoggedInUserLevelId) == 45)
            {
                loggedInUserInformation.PassType = "FL-B11";
            }
            else if (Convert.ToInt32(UserSession.LoggedInUserLevelId) == 35)
            {
                loggedInUserInformation.PassType = "FL-36";
            }
            else if (Convert.ToInt32(UserSession.LoggedInUserLevelId) == 15)
            {
                loggedInUserInformation.PassType = "B-12";
            }
            else
                loggedInUserInformation.PassType = "FL-B11";


            if (Convert.ToInt32(UserSession.LoggedInUserLevelId) == 45)
            {
                loggedInUserInformation.Receiver = "Lucknow Warehouse";
                loggedInUserInformation.PassTypeInformation = "Manufacturer Wholesale to Disctrict Wholesale";
            }
            else if (Convert.ToInt32(UserSession.LoggedInUserLevelId) == 35)
            {
                loggedInUserInformation.Receiver = "Sahu & Sons Kanpur Road";
                loggedInUserInformation.PassTypeInformation = "District Wholesale to Retailor";
            }
            else if (Convert.ToInt32(UserSession.LoggedInUserLevelId) == 15)
            {
                loggedInUserInformation.Receiver = "M/s Mohan Goldwater Breweries Ltd. Warehouse";
                loggedInUserInformation.PassTypeInformation = "Brewery To Manufacturer Wholesale";

            }
            else
                loggedInUserInformation.Receiver = "FL-B11";

            return loggedInUserInformation;

        }






    }
}