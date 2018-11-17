
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UPExciseLTE.Models;
using System.Data;
using UPExciseLTE.DAL;
using UPExciseLTE.BLL;

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


        //#region BreweryToManufacturerWholesale
        //public ActionResult BreweryToManufacturerWholesale()
        //{
        //    List<SelectListItem> LicenceNos = new List<SelectListItem>();
        //    ViewBag.LicenceNo = LicenceNos;
        //    ViewBag.Brands = CommonBL.fillBrand("S");
        //    return View();
        //}
        //#endregion


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