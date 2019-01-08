using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using UPExciseLTE.DAL;
using UPExciseLTE.BLL;
using UPExciseLTE.Models;

namespace UPExciseLTE.Controllers
{
    public class BreweryWareHouseController : Controller
    {
        //
        // GET: /BreweryWareHouse/
        [HttpGet]
        public ActionResult BreweryWareHouse()
        {
            try
            {
                
                List<SelectListItem> lstUnitLicenseType = new List<SelectListItem>();
                SelectListItem ULT = new SelectListItem();
                ULT = new SelectListItem();
                ULT.Text = "FL1/FL2A";
                ULT.Value = "FL1/FL2A";
                lstUnitLicenseType.Add(ULT);
                ViewBag.LicenseType = lstUnitLicenseType;

                List<SelectListItem> lstDistrict = new List<SelectListItem>();
                SelectListItem ld = new SelectListItem();
                ld = new SelectListItem();
                ld.Text = "--Select--";
                ld.Value = "0";
                lstDistrict.Add(ld);
                ViewBag.DistrictCode = lstDistrict;

                List<SelectListItem> lstUnitType = new List<SelectListItem>();
                SelectListItem UT = new SelectListItem();
                UT = new SelectListItem();
                UT.Text = "Brewery";
                UT.Value = "Brewery";
                lstUnitType.Add(UT);
                ViewBag.UnitTypeList = lstUnitType;

                List<SelectListItem> lstProductionLiquor = new List<SelectListItem>();
                SelectListItem PL = new SelectListItem();
                PL = new SelectListItem();
                PL.Text = "BEER";
                PL.Value = "BE";
                lstProductionLiquor.Add(PL);
                ViewBag.ProductionLiquorList = lstProductionLiquor;

                ViewBag.StateList = CommonBL.fillState("S");
                ViewBag.Msg = TempData["Message"];
                return View();
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }  
        }
        [HttpPost]
        public ActionResult BreweryWareHouse(UnitMaster ObjUnitMaster, HttpPostedFileBase imgLicenseUpload)
        {
            try
            {
                Byte[] img = null;
                if (imgLicenseUpload != null && imgLicenseUpload.ContentLength > 0)
                {   /*****IMG-DB-CODE******/
                    int FileSize = imgLicenseUpload.ContentLength;
                    string[] extSplit = imgLicenseUpload.FileName.Split('.');
                    string ext = extSplit[extSplit.Length - 1];
                    img = new Byte[FileSize];
                    imgLicenseUpload.InputStream.Read(img, 0, FileSize);
                    ObjUnitMaster.LicensePhoto = img;
                    ObjUnitMaster.FileExt = ext;
                }
                if (ObjUnitMaster.Remark != null && ObjUnitMaster.Remark != string.Empty)
                {
                    ObjUnitMaster.SPType = 4;
                    ObjUnitMaster.ValidityOfLicense1 = CommonBL.Setdate(ObjUnitMaster.ValidityOfLicense);
                    ObjUnitMaster.ParentUnitId =  Convert.ToInt32(CommonBL.fillBrewery()[0].Value);
                    string str = new CommonDA().InsertUpdateUnitMaster(ObjUnitMaster);
                    TempData["Message"] = str;
                    return RedirectToAction("BreweryWareHouse");
                }
                else
                {
                    ObjUnitMaster.Remark = "";
                    ObjUnitMaster.ValidityOfLicense1 = CommonBL.Setdate(ObjUnitMaster.ValidityOfLicense);
                    ObjUnitMaster.SPType = 4;
                    ObjUnitMaster.ParentUnitId = Convert.ToInt32(CommonBL.fillBrewery()[0].Value);
                    string str = new CommonDA().InsertUpdateUnitMaster(ObjUnitMaster);
                    TempData["Message"] = str;
                    return RedirectToAction("BreweryWareHouse");
                }                
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        public ActionResult GetDistrictAgainstState(long ddlState)
        {
            try
            {
                return Json(CommonBL.fillDistict("S", ddlState.ToString()), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
	}
}