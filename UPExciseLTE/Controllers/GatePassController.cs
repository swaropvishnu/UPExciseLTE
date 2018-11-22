
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using UPExciseLTE.Models;
using System.Data;
using UPExciseLTE.DAL;
using UPExciseLTE.BLL;
using UPExciseLTE.Filters;
using System.Data.Entity.Infrastructure;
namespace UPExciseLTE.Controllers
{
    [SessionExpireFilter]
    //[CheckAuthorization]
    [HandleError(ExceptionType = typeof(DbUpdateException), View = "Error")]
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
            gatePass.ToDate= CommonBL.Setdate(gatePass.ValidTill);
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
        public JsonResult GetFilteredDistrict(long districtId1=0, long districtId2=0)
        {
            var districts = CommonBL.fillDistict("S", districtId1, districtId2);
            return Json(districts, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ReceivedGatePass()
        {
            return View();
        }

        public ActionResult GenerateGatePass()
        {
            var gatePassList = new CommonBL().GetGatePassDetailsForGatePassList();
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
            breweryToManufacturerGatePass = new CommonBL().GetBreweryToManufacturerPassDetailsDetails();
            breweryToManufacturerGatePass.PassTypeInformation = informationByLoggedInUserLevel.PassTypeInformation;
            breweryToManufacturerGatePass = GetBrewerytToManufacturerGatePass(breweryToManufacturerGatePass);
            breweryToManufacturerGatePass.DistrictWholeSaleToRetailorList = new CommonBL().GetBMGatePassDetails(breweryToManufacturerGatePass.GatePassId, Convert.ToInt64(breweryToManufacturerGatePass.BrandId));
            return View(breweryToManufacturerGatePass);
        }

        [HttpPost]
        public ActionResult BreweryToManufacturerWholesale(BrewerytToManufacturerGatePass breweryToManufacturerGatePass)
        {
            breweryToManufacturerGatePass.FromDate = CommonBL.Setdate(breweryToManufacturerGatePass.Date);
            breweryToManufacturerGatePass.ToDate = CommonBL.Setdate(breweryToManufacturerGatePass.ValidTill);
            var str = new CommonDA().InsertUpdateGatePass(breweryToManufacturerGatePass);
            if (!string.IsNullOrEmpty(str))
            {
                breweryToManufacturerGatePass.Message = Message.MsgSuccess(str);
            }
            breweryToManufacturerGatePass.DistrictWholeSaleToRetailorList = new CommonBL().GetBMGatePassDetails(breweryToManufacturerGatePass.GatePassId, Convert.ToInt64(breweryToManufacturerGatePass.BrandId));
            breweryToManufacturerGatePass = GetBrewerytToManufacturerGatePass(breweryToManufacturerGatePass);
            return RedirectToAction("BreweryToManufacturerWholesale");
        }



        [HttpPost]
        public ActionResult FinalizeBMWholesaleGatePass(List<DistrictWholeSaleToRetailorModel> districtWholeSaleToRetailorModels,long gatePassID)
        {
            var gatePass = new BrewerytToManufacturerGatePass();
            gatePass.GatePassId = gatePassID;
            gatePass.SP_Type = 3;
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
            return breweryToManufacturerGatePass;
        }


        [HttpPost]
        public ActionResult UploadCSVFile(long brandId = 0,long gatePassId=0)
        {
            if (Request.Files["impCSVUpload"].ContentLength > 0)
            {
                string extension = System.IO.Path.GetExtension(Request.Files["impCSVUpload"].FileName).ToLower();
                if (extension == ".csv")
                {
                    string UploadedValue = "2";
                    System.Web.HttpPostedFileBase file = Request.Files["impCSVUpload"];
                    DataTable dt=CSV.GetCSVToDt(file);
                    string str = new CommonDA().UploadCSV(gatePassId,dt,2);
                    TempData["Message"] = str;
                    var breweryToManufacturerGatePass = new BrewerytToManufacturerGatePass();
                    breweryToManufacturerGatePass.SP_Type = 2;
                    breweryToManufacturerGatePass.BrandId = brandId;
                    breweryToManufacturerGatePass.GatePassId = gatePassId;
                    breweryToManufacturerGatePass.UploadValue = UploadedValue;
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


        //#region ManufacturerWholesaleToDistrictWholesale
        //public ActionResult ManufacturerWholesaleToDistrictWholesale()
        //{
        //    List<SelectListItem> LicenceNos = new List<SelectListItem>();
        //    GatePass breweryToManufacturerWholesaleModel = new GatePass();
        //    breweryToManufacturerWholesaleModel.DistrictWholeSaleToRetailorList = new List<DistrictWholeSaleToRetailorModel>();
        //    ViewBag.Brands = CommonBL.fillBrand("S");
        //    ViewBag.LicenceNo = LicenceNos;
        //    return View();
        //}
        //#endregion


        //#region DistrictWholesaleToRetailor
        //public ActionResult DistrictWholesaleToRetailor()
        //{
        //    ViewBag.Brands = CommonBL.fillBrand("S");
        //    List<SelectListItem> LicenceNos = new List<SelectListItem>();
        //    GatePass breweryToManufacturerWholesaleModel = new GatePass();
        //    breweryToManufacturerWholesaleModel.DistrictWholeSaleToRetailorList = new List<DistrictWholeSaleToRetailorModel>();
        //    ViewBag.LicenceNo = LicenceNos;
        //    return View(breweryToManufacturerWholesaleModel);
        //}
        //#endregion

    }
}