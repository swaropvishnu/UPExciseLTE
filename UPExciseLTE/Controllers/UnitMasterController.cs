﻿using System;
using System.Web.Mvc;
using UPExciseLTE.Models;
using UPExciseLTE.DAL;
using System.Collections.Generic;
using UPExciseLTE.BLL;
using System.Web;


namespace UPExciseLTE.Controllers
{
    public class UnitMasterController : Controller
    {
        //
        // GET: /UnitMaster/
        [HttpGet]
        public ActionResult UnitMaster()
        {
            try
            {
                UnitMaster objUnitMaster = new UnitMaster();
                List<SelectListItem> lstUnitLicenseType = new List<SelectListItem>();
                SelectListItem ULT = new SelectListItem();
                ULT = new SelectListItem();
                ULT.Text = "FL3";
                ULT.Value = "FL3";
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

                if (Request.QueryString["Code"] != null && Request.QueryString["Code"].Trim() != string.Empty)
                {
                    objUnitMaster = new CommonBL().GetUnitMaster(int.Parse(Request.QueryString["Code"].Trim()),"","",-1,-1,-1,-1,"Z",-1,"Z");
                    ViewBag.StateList = CommonBL.fillState("S");
                }

                ViewBag.Msg = TempData["Message"];
                return View(objUnitMaster);

            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult UnitMaster(UnitMaster ObjUnitMaster, HttpPostedFileBase imgLicenseUpload)
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
                    string str = new CommonDA().InsertUpdateUnitMaster(ObjUnitMaster);
                    TempData["Message"] = str;
                    return RedirectToAction("UnitMaster");
                }
                else
                {
                    ObjUnitMaster.Remark = "";
                    string str = new CommonDA().InsertUpdateUnitMaster(ObjUnitMaster);
                    TempData["Message"] = str;
                    return RedirectToAction("UnitMaster");
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
        [HttpGet]
        public ActionResult GetUnitMasterDetails()
        {
            try
            {
                List<UnitMaster> lstUnitMaster = new CommonBL().GetUnitMasterList(-1, "", "",-1,-1,-1,-1, "Z",-1, "Z");                
                return View(lstUnitMaster);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        [HttpGet]
        public ActionResult FreezeUnitMaster()
        {
            try
            {
                List<UnitMaster> lstUnitMaster = new CommonBL().GetUnitMasterList(-1, "", "", -1, -1, -1, -1, "Z", -1, "Z");
                return View(lstUnitMaster);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        [HttpPost]
        public string FinalBrand(string UnitId, string Status, string Reason)
        {
            string str = "";
            int TypeId = -1;
            try
            {
                if (Status == "A")
                { 
                    TypeId = 1; 
                }
                else
                { 
                    TypeId = 3; 
                }
                str = new CommonDA().UpdateBrand(int.Parse(UnitId), TypeId, Reason);
            }
            catch (Exception x)
            {
                str = x.Message.ToString();
            }
            return str;
        }
    }
}