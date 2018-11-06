
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
        // GET: GatePass

        #region BreweryToManufacturerWholesale
        public ActionResult BreweryToManufacturerWholesale()
        {
            List<SelectListItem> LicenceNos = new List<SelectListItem>();
            ViewBag.LicenceNo = LicenceNos;
            ViewBag.Brands = CommonBL.fillBrand("S");
            return View();
        }
        #endregion


        #region ManufacturerWholesaleToDistrictWholesale
        public ActionResult ManufacturerWholesaleToDistrictWholesale()
        {
            List<SelectListItem> LicenceNos = new List<SelectListItem>();
            BreweryToManufacturerWholesaleModel breweryToManufacturerWholesaleModel = new BreweryToManufacturerWholesaleModel();
            breweryToManufacturerWholesaleModel.DistrictWholeSaleToRetailorList = new List<DistrictWholeSaleToRetailorModel>();
            ViewBag.Brands = CommonBL.fillBrand("S");
            ViewBag.LicenceNo = LicenceNos;
            return View();
        }
        #endregion


        #region DistrictWholesaleToRetailor
        public ActionResult DistrictWholesaleToRetailor()
        {
            ViewBag.Brands = CommonBL.fillBrand("S");
            List<SelectListItem> LicenceNos = new List<SelectListItem>();
            BreweryToManufacturerWholesaleModel breweryToManufacturerWholesaleModel = new BreweryToManufacturerWholesaleModel();
            breweryToManufacturerWholesaleModel.DistrictWholeSaleToRetailorList = new List<DistrictWholeSaleToRetailorModel>();
            ViewBag.LicenceNo = LicenceNos;
            return View(breweryToManufacturerWholesaleModel);
        }
        #endregion



        #region ReceivedGatePass

        public ActionResult ReceivedGatePass()
        {
            return View();
        }

        #endregion

        #region GenerateGatePass
        public ActionResult GenerateGatePass()
        {
            return View();
        }
        public ActionResult UploadGatePass()
        {
            return View();
        }

        #endregion

        //#region BrandList

        //public List<SelectListItem> GetBrandList()
        //{
        //    return CommonBL.fillBrand("S");
        //}

        //#endregion

    }
}