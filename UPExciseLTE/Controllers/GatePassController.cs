using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using UPExciseLTE.BLL;
using UPExciseLTE.DAL;
using UPExciseLTE.Filters;
using UPExciseLTE.Models;

namespace UPExciseLTE.Controllers
{
    [SessionExpireFilterAttribute]
    [NoCache]
    [ChkAuthorization]
    public class GatePassController : Controller
    {

        #region GatePass

        [HttpGet]
        public ActionResult GatePass()
        {
            var gatePass = new Models.GatePass();
            var ReportController = new ReportController();
            var informationByLoggedInUserLevel = new InformationByLoggedInUserLevel();
            informationByLoggedInUserLevel = ReportController.GetInformationByLoggedInUserLeve();
            gatePass.PassTypeInformation = informationByLoggedInUserLevel.PassTypeInformation;
            ViewBag.Brands = CommonBL.fillBrand("S");
            gatePass.DistrictWholeSaleToRetailorList = new CommonBL().GetGatePassDetails();
            ViewBag.LicenseeLiceseeNos = CommonBL.fillLiceseeLicenseNos("S");
            ViewBag.Districts = CommonBL.fillDistict("S");
            ViewBag.Shops = CommonBL.fillShops("S");
            return View(gatePass);
        }

        [HttpPost]
        public ActionResult GatePass(GatePass gatePass, List<DistrictWholeSaleToRetailorModel> districtWholeSaleToRetailorModels)
        {
            ViewBag.Brands = CommonBL.fillBrand("S");
            gatePass.FromDate = CommonBL.Setdate(gatePass.Date);
            gatePass.ToDate = CommonBL.Setdate(gatePass.ValidTill);
            var passTypeInformation = gatePass.PassTypeInformation;
            var str = new CommonDA().InsertUpdateGatePass(gatePass, districtWholeSaleToRetailorModels);
            gatePass = new GatePass();
            gatePass.PassTypeInformation = passTypeInformation;
            if (!string.IsNullOrEmpty(str))
            {
                gatePass.Message = new Message();
                gatePass.Message = Message.MsgSuccess(str);
            }
            gatePass.DistrictWholeSaleToRetailorList = new CommonBL().GetGatePassDetails();
            ViewBag.LicenseeLiceseeNos = CommonBL.fillLiceseeLicenseNos("S");
            ViewBag.Districts = CommonBL.fillDistict("S");
            ViewBag.Shops = CommonBL.fillShops("S");
            return PartialView("~/Views/Shared/_ErrorMessage.cshtml", gatePass.Message);
        }

        [HttpPost]
        public JsonResult GetFilteredDistrict(long districtId1 = 0, long districtId2 = 0)
        {
            var districts = CommonBL.fillDistict("S","-1", districtId1, districtId2);
            return Json(districts, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ReceivedGatePass()
        {
            return View();
        }

        public ActionResult GenerateGatePass()
        {
            var spType = -1;
            if (Convert.ToInt32(UserSession.LoggedInUserLevelId) == 15)
            {
                spType = 3;
            }
            else if (Convert.ToInt32(UserSession.LoggedInUserLevelId) == 45)
            {
                spType = 11;
            }
            else if (Convert.ToInt32(UserSession.LoggedInUserLevelId) == 35)
            {
                spType = 12;
            }
            var gatePassList = new CommonBL().GetGatePassDetailsForGatePassList(spType);
            return View(gatePassList);
        }
        public ActionResult UploadGatePass()
        {
            var message = new Message();
            return View(message);
        }

        #endregion


        #region BreweryToManufacturerWholesale

        [HttpGet]
        public ActionResult BreweryToManufacturerWholesale()
        {
            BrewerytToManufacturerGatePass breweryToManufacturerGatePass = new BrewerytToManufacturerGatePass();
            var ReportController = new ReportController();
            var informationByLoggedInUserLevel = new InformationByLoggedInUserLevel();
            informationByLoggedInUserLevel = ReportController.GetInformationByLoggedInUserLeve();
            var spType = 6;
            breweryToManufacturerGatePass = new CommonBL().GetBreweryToManufacturerPassDetailsDetails(spType);
            breweryToManufacturerGatePass.PassTypeInformation = informationByLoggedInUserLevel.PassTypeInformation;
            breweryToManufacturerGatePass = GetBrewerytToManufacturerGatePass(breweryToManufacturerGatePass);
            spType = 2;
            breweryToManufacturerGatePass.DistrictWholeSaleToRetailorList = new CommonBL().GetBMGatePassDetails(breweryToManufacturerGatePass.GatePassId, Convert.ToInt64(breweryToManufacturerGatePass.BrandId), spType);
            return View(breweryToManufacturerGatePass);
        }

        [HttpPost]
        public ActionResult BreweryToManufacturerWholesale(BrewerytToManufacturerGatePass breweryToManufacturerGatePass)
        {
            breweryToManufacturerGatePass.FromDate = CommonBL.Setdate(breweryToManufacturerGatePass.Date);
            breweryToManufacturerGatePass.ToDate = CommonBL.Setdate(breweryToManufacturerGatePass.ValidTill);
            if (breweryToManufacturerGatePass.PassTypeInformation == "Manufacturer Wholesale to Disctrict Wholesale")
                breweryToManufacturerGatePass.SP_Type = 4;
            else if (breweryToManufacturerGatePass.PassTypeInformation == "Brewery To Manufacturer Wholesale")
                breweryToManufacturerGatePass.SP_Type = 1;
            else if (breweryToManufacturerGatePass.PassTypeInformation == "District Wholesale to Retailor")
                breweryToManufacturerGatePass.SP_Type = 7;
            var str = new CommonDA().InsertUpdateGatePass(breweryToManufacturerGatePass);
            if (!string.IsNullOrEmpty(str))
            {
                breweryToManufacturerGatePass.Message = Message.MsgSuccess(str);
            }
            var returnView = "";
            //breweryToManufacturerGatePass.DistrictWholeSaleToRetailorList = new CommonBL().GetBMGatePassDetails(breweryToManufacturerGatePass.GatePassId, Convert.ToInt64(breweryToManufacturerGatePass.BrandId));
            breweryToManufacturerGatePass = GetBrewerytToManufacturerGatePass(breweryToManufacturerGatePass);
            if (breweryToManufacturerGatePass.PassTypeInformation == "Manufacturer Wholesale to Disctrict Wholesale")
                returnView = "ManufacturerWholesaleToDistrictWholesale";
            else if (breweryToManufacturerGatePass.PassTypeInformation == "Brewery To Manufacturer Wholesale")
                returnView = "BreweryToManufacturerWholesale";
            else if (breweryToManufacturerGatePass.PassTypeInformation == "District Wholesale to Retailor")
                returnView = "DistrictWholesaleToRetailor";
            return RedirectToAction(returnView);
        }



        [HttpPost]
        public ActionResult FinalizeBMWholesaleGatePass(List<DistrictWholeSaleToRetailorModel> districtWholeSaleToRetailorModels, long gatePassID)
        {
            var gatePass = new BrewerytToManufacturerGatePass();
            gatePass.GatePassId = gatePassID;

            if (Convert.ToInt32(UserSession.LoggedInUserLevelId) == 15)
            {
                gatePass.SP_Type = 3;
            }
            else if (Convert.ToInt32(UserSession.LoggedInUserLevelId) == 45)
            {
                gatePass.SP_Type = 6;
            }
            else if (Convert.ToInt32(UserSession.LoggedInUserLevelId) == 35)
            {
                gatePass.SP_Type = 9;
            }
            var str = new CommonDA().InsertUpdateGatePass(gatePass, districtWholeSaleToRetailorModels);
            if (!string.IsNullOrEmpty(str))
            {
                gatePass.Message = new Message();
                gatePass.Message = Message.MsgSuccess(str);
            }
            return PartialView("~/Views/Shared/_ErrorMessage.cshtml", gatePass.Message);
        }

        private BrewerytToManufacturerGatePass GetBrewerytToManufacturerGatePass(BrewerytToManufacturerGatePass breweryToManufacturerGatePass)
        {
            breweryToManufacturerGatePass.ConsinorLicenseeNos = CommonBL.fillLiceseeLicenseNos("S");
            breweryToManufacturerGatePass.ConsineeLicenseeNos = CommonBL.fillLiceseeLicenseNos("S");
            breweryToManufacturerGatePass.Districts = CommonBL.fillDistict("S");
            breweryToManufacturerGatePass.Brands = CommonBL.fillBrand("S");
            breweryToManufacturerGatePass.LicenseTypes = CommonBL.fillLicenseTypes("S", "");
            breweryToManufacturerGatePass.Shops = CommonBL.fillShops("S");
            return breweryToManufacturerGatePass;
        }


        [HttpPost]
        public ActionResult UploadCSVFile(long brandId = 0, long gatePassId = 0)
        {
            int UploadedValue = -1;
            var breweryToManufacturerGatePass = new BrewerytToManufacturerGatePass();
            if (Request.Files["impCSVUpload"].ContentLength > 0)
            {
                string extension = System.IO.Path.GetExtension(Request.Files["impCSVUpload"].FileName).ToLower();
                if (Convert.ToInt32(UserSession.LoggedInUserLevelId) == 15)
                {
                    UploadedValue = 2;
                    breweryToManufacturerGatePass.SP_Type = 2;
                    breweryToManufacturerGatePass.BrandId = brandId;
                    breweryToManufacturerGatePass.GatePassId = gatePassId;
                    breweryToManufacturerGatePass.UploadValue = UploadedValue;
                }
                else if (Convert.ToInt32(UserSession.LoggedInUserLevelId) == 45)
                {
                    UploadedValue = 3;
                    breweryToManufacturerGatePass.SP_Type = 5;
                    breweryToManufacturerGatePass.BrandId = brandId;
                    breweryToManufacturerGatePass.GatePassId = gatePassId;
                    breweryToManufacturerGatePass.UploadValue = UploadedValue;
                }
                else if (Convert.ToInt32(UserSession.LoggedInUserLevelId) == 35)
                {
                    UploadedValue = 5;
                    breweryToManufacturerGatePass.SP_Type = 8;
                    breweryToManufacturerGatePass.BrandId = brandId;
                    breweryToManufacturerGatePass.GatePassId = gatePassId;
                    breweryToManufacturerGatePass.UploadValue = UploadedValue;
                }

                if (extension == ".csv")
                {


                    System.Web.HttpPostedFileBase file = Request.Files["impCSVUpload"];
                    DataTable dt = CSV.GetCSVToDt(file);
                    string str = new CommonDA().UploadCSV(gatePassId, dt, UploadedValue, -1, "",1,-1);

                    TempData["Message"] = str;
                    var msg = new CommonDA().InsertUpdateGatePass(breweryToManufacturerGatePass);
                    if (!string.IsNullOrEmpty(msg))
                    {
                        breweryToManufacturerGatePass.Message = Message.MsgSuccess(msg);
                    }
                }
                else
                {
                    TempData["Message"] = "Please Upload Files in .csv format";
                }

            }

            return RedirectToAction("UploadCSV");
        }

        #endregion


        #region ManufacturerWholesaleToDistrictWholesale
        public ActionResult ManufacturerWholesaleToDistrictWholesale()
        {
            BrewerytToManufacturerGatePass breweryToManufacturerGatePass = new BrewerytToManufacturerGatePass();
            var ReportController = new ReportController();
            var informationByLoggedInUserLevel = new InformationByLoggedInUserLevel();
            var spType = 7;
            informationByLoggedInUserLevel = ReportController.GetInformationByLoggedInUserLeve();
            breweryToManufacturerGatePass = new CommonBL().GetBreweryToManufacturerPassDetailsDetails(spType);
            breweryToManufacturerGatePass.PassTypeInformation = informationByLoggedInUserLevel.PassTypeInformation;
            breweryToManufacturerGatePass = GetBrewerytToManufacturerGatePass(breweryToManufacturerGatePass);
            spType = 9;
            breweryToManufacturerGatePass.DistrictWholeSaleToRetailorList = new CommonBL().GetBMGatePassDetails(breweryToManufacturerGatePass.GatePassId, Convert.ToInt64(breweryToManufacturerGatePass.BrandId), spType);
            return View(breweryToManufacturerGatePass);
        }
        #endregion


        #region DistrictWholesaleToRetailor
        public ActionResult DistrictWholesaleToRetailor()
        {
            BrewerytToManufacturerGatePass breweryToManufacturerGatePass = new BrewerytToManufacturerGatePass();
            var ReportController = new ReportController();
            var informationByLoggedInUserLevel = new InformationByLoggedInUserLevel();
            informationByLoggedInUserLevel = ReportController.GetInformationByLoggedInUserLeve();
            var spType = 8;
            breweryToManufacturerGatePass = new CommonBL().GetBreweryToManufacturerPassDetailsDetails(spType);
            breweryToManufacturerGatePass.PassTypeInformation = informationByLoggedInUserLevel.PassTypeInformation;
            breweryToManufacturerGatePass = GetBrewerytToManufacturerGatePass(breweryToManufacturerGatePass);
            spType = 10;
            breweryToManufacturerGatePass.DistrictWholeSaleToRetailorList = new CommonBL().GetBMGatePassDetails(breweryToManufacturerGatePass.GatePassId, Convert.ToInt64(breweryToManufacturerGatePass.BrandId), spType);
            return View(breweryToManufacturerGatePass);
        }
        #endregion

    }
}