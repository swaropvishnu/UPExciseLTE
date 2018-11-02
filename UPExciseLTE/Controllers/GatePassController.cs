
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UPExciseLTE.Models;
using System.Data;
using UPExciseLTE.DAL;

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
            ViewBag.Brands = GetBrandList();
            return View();
        }
        #endregion


        #region ManufacturerWholesaleToDistrictWholesale
        public ActionResult ManufacturerWholesaleToDistrictWholesale()
        {
            List<SelectListItem> LicenceNos = new List<SelectListItem>();
            BreweryToManufacturerWholesaleModel breweryToManufacturerWholesaleModel = new BreweryToManufacturerWholesaleModel();
            breweryToManufacturerWholesaleModel.DistrictWholeSaleToRetailorList = new List<DistrictWholeSaleToRetailorModel>();
            ViewBag.Brands = GetBrandList();
            ViewBag.LicenceNo = LicenceNos;
            return View();
        }
        #endregion


        #region DistrictWholesaleToRetailor
        public ActionResult DistrictWholesaleToRetailor()
        {
            ViewBag.Brands = GetBrandList();
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

        #region BrandList

        public List<SelectListItem> GetBrandList()
        {
            List<SelectListItem> BrandList = new List<SelectListItem>();
            string UserId = (Session["tbl_Session"] as DataTable).Rows[0]["UserId"].ToString().Trim();
           // CMODataEntryBLL.bindDropDownHnGrid_db2("proc_ddlDetail", BrandList, "BR", UserId, "Z");
            return BrandList;
        }

        #endregion

    }
}