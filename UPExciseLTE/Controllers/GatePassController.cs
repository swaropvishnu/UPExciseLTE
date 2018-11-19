
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UPExciseLTE.Models;
using System.Data;
using UPExciseLTE.DAL;
using UPExciseLTE.BLL;
using System.IO;

namespace UPExciseLTE.Controllers
{
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
            breweryToManufacturerGatePass= new CommonBL().GetBreweryToManufacturerPassDetailsDetails();
            breweryToManufacturerGatePass = GetBrewerytToManufacturerGatePass(breweryToManufacturerGatePass);
            breweryToManufacturerGatePass.DistrictWholeSaleToRetailorList = new CommonBL().GetGatePassDetails();
            return View(breweryToManufacturerGatePass);
        }

        [HttpPost]
        public ActionResult BreweryToManufacturerWholesale(BrewerytToManufacturerGatePass breweryToManufacturerGatePass)
        {
            //var str = new CommonDA().InsertUpdateGatePass(breweryToManufacturerGatePass);
            //gatePass = new GatePass();
            //gatePass.PassTypeInformation = passTypeInformation;
            //if (!string.IsNullOrEmpty(str))
            //{
            //    gatePass.Message = new Message();
            //    gatePass.Message = Message.MsgSuccess(str);
            //}
            breweryToManufacturerGatePass.DistrictWholeSaleToRetailorList = new CommonBL().GetGatePassDetails();
            return View(breweryToManufacturerGatePass);
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
                    UploadCSV ob = new UploadCSV();
                    string UploadedValue = "2";
                    System.Web.HttpPostedFileBase file = Request.Files["impCSVUpload"];
                    ob.GetCSVDetails(file, UploadedValue);
                    string str = new CommonDA().UploadCSV(ob.InsertUploadManufProdQuery, UploadedValue, UserSession.LoggedInUserId.ToString(), ob.CaseCount, ob.QRCount, ob.ListQRCode);
                    TempData["Message"] = str;
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