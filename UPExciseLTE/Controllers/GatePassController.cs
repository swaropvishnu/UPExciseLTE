using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UPExciseLTE.Models;

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
            return View();
        }
        #endregion


        #region ManufacturerWholesaleToDistrictWholesale
        public ActionResult ManufacturerWholesaleToDistrictWholesale()
        {
            List<SelectListItem> LicenceNos = new List<SelectListItem>();
            BreweryToManufacturerWholesaleModel breweryToManufacturerWholesaleModel = new BreweryToManufacturerWholesaleModel();
            breweryToManufacturerWholesaleModel.DistrictWholeSaleToRetailorList = new List<DistrictWholeSaleToRetailorModel>();
   
            ViewBag.LicenceNo = LicenceNos;
            return View();
        }
        #endregion


        #region DistrictWholesaleToRetailor
        public ActionResult DistrictWholesaleToRetailor()
        {
            List<SelectListItem> LicenceNos = new List<SelectListItem>();
            BreweryToManufacturerWholesaleModel breweryToManufacturerWholesaleModel = new BreweryToManufacturerWholesaleModel();
            breweryToManufacturerWholesaleModel.DistrictWholeSaleToRetailorList = new List<DistrictWholeSaleToRetailorModel>();
            ViewBag.LicenceNo = LicenceNos;
            return View(breweryToManufacturerWholesaleModel);
        }
        #endregion


   




    }
}