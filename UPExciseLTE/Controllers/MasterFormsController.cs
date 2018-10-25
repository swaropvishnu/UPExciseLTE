﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using UPExciseLTE.DAL;
using UPExciseLTE.Models;

namespace UPExciseLTE.Controllers
{
    public class MasterFormsController : Controller
    {
        //
        // GET: /MasterForms/

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult BrandMaster()
        {
            return View(LoadBrandMaster());
        }
        public ActionResult BrandMaster(int BrandId)
        {
            return View(LoadBrandMaster());
        }
        BrandMaster LoadBrandMaster()
        {
            List<SelectListItem> breweryList = new List<SelectListItem>();
            CMODataEntryBLL.bindDropDownHnGrid("proc_ddlDetail", breweryList, "BRE", "", "");
            BrandMaster Brand = new BrandMaster();
            ViewBag.Brewery = breweryList;
            return Brand;
        }
        [HttpPost]
        public ActionResult BrandMaster(BrandMaster B)
        {
            /*Save Record In Database*/
            ViewData["Result"] = "Successfully";
            return View(LoadBrandMaster());
        }
        [HttpGet]
        public ActionResult GetBrandDetails()
        {
            List<BrandMaster> lstBrand = new List<BrandMaster>();
            List<SelectListItem> breweryList = new List<SelectListItem>();
            CMODataEntryBLL.bindDropDownHnGrid("proc_ddlDetail", breweryList, "BRE", "", "");
            ViewBag.Brewery = breweryList;
            DataSet ds =  new CommonDA().GetBrandDetail(-1, "", "", "", -1, -1, -1);
            if (ds!=null && ds.Tables[0]!=null && ds.Tables[0].Rows.Count>0)
            {
                try
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        BrandMaster brand = new BrandMaster();
                        brand.AdditionalDuty = float.Parse(dr["AdditionalDuty"].ToString().Trim());
                        brand.BrandId = int.Parse(dr["BrandId"].ToString().Trim());
                        brand.BrandName = dr["BrandName"].ToString().Trim();
                        brand.BrandRegistrationNumber = dr["BrandRegistrationNumber"].ToString().Trim();
                        brand.BreweryId =short.Parse(dr["BreweryId"].ToString().Trim());
                        brand.ExciseDuty = float.Parse(dr["ExciseDuty"].ToString().Trim());
                        brand.ExiciseTin = dr["ExiciseTin"].ToString().Trim();
                        brand.ExportBoxSize = int.Parse(dr["ExportBoxSize"].ToString().Trim());
                        brand.LicenceNo = dr["LicenceNo"].ToString().Trim();
                        brand.LicenceType = dr["LicenceType"].ToString().Trim();
                        brand.LiquorType = dr["LiquorType"].ToString().Trim();
                        brand.MRP = float.Parse(dr["MRP"].ToString().Trim());
                        brand.QuantityInBottleML = int.Parse(dr["QuantityInBottleML"].ToString().Trim());
                        brand.QuantityInCase = int.Parse(dr["QuantityInCase"].ToString().Trim());
                        brand.Remark = dr["Remark"].ToString().Trim();
                        brand.Strength = float.Parse(dr["Strength"].ToString().Trim());
                        brand.XFactoryPrice = float.Parse(dr["XFactoryPrice"].ToString().Trim());
                        lstBrand.Add(brand);
                    }
                }
                catch (Exception exp) { } 
            }
            return View(lstBrand);
        }
        [HttpGet]
        public ActionResult BottelingPlan()
        {
            BottelingPlan Plan = new BottelingPlan();
            List<SelectListItem> BrandList = new List<SelectListItem>();
            Plan.PlanId = -1;
            Plan.Type = 1;
            CMODataEntryBLL.bindDropDownHnGrid_db2("proc_ddlDetail", BrandList, "BR", "", "");
            ViewBag.Brand = BrandList;
            return View(Plan);
        }
        [HttpPost]
        public ActionResult BottelingPlan(BottelingPlan BP)
        {
            BottelingPlan Plan = new BottelingPlan();
            List<SelectListItem> BrandList = new List<SelectListItem>();
            CMODataEntryBLL.bindDropDownHnGrid_db2("proc_ddlDetail", BrandList, "BR", "", "");
            ViewBag.Brand = BrandList;
            BP.dbName = "be_unnao1";
            string str = new CommonDA().InsertUpdatePlan(BP);
            ViewBag.Msg = str;
            return View(Plan);
        }
        [HttpGet]
        public ActionResult BottelingPlanList()
        {
            List<BottelingPlan> BPList = new List<BottelingPlan>();
            List<SelectListItem> BrandList = new List<SelectListItem>();
            CMODataEntryBLL.bindDropDownHnGrid_db2("proc_ddlDetail", BrandList, "BR", "", "");
            ViewBag.Brand = BrandList;
            DataSet ds = new CommonDA().GetBottelingPlanDetail(DateTime.Now,DateTime.Now,-1,-1,"Z","Z","Z");
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                try
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        BottelingPlan Plan = new BottelingPlan();
                        Plan.PlanId = int.Parse(dr["PlanId"].ToString().Trim());
                        Plan.DateOfPlan = DateTime.Parse(dr["DateOfPlan"].ToString().Trim());
                        Plan.LiquorType = dr["LiquorType"].ToString().Trim();
                        Plan.LicenceType = dr["LiquorType"].ToString().Trim();
                        Plan.Brand = dr["BrandName"].ToString().Trim();
                        Plan.Capacity = dr["Capacity"].ToString().Trim();
                        Plan.QunatityInCaseExport = int.Parse(dr["QuantityInCase"].ToString().Trim());
                        Plan.Quantity = dr["TotalUnitQuantity"].ToString().Trim();
                        Plan.PlanNoofBottling = dr["ExportBoxSize"].ToString().Trim();
                        Plan.NumberOfCases = int.Parse(dr["NumberOfCases"].ToString().Trim());
                        BPList.Add(Plan);
                    }
                }
                catch (Exception exp) { }
            }
            return View(BPList);
        }
        [HttpGet]
        public  string   GetBrandDetailsForDDl(string BrandId)
        {
            string str = "";
            DataSet ds =  new CommonDA().GetBrandDetail(int.Parse(BrandId), "", "", "", -1, -1, -1);
            if (ds!=null && ds.Tables[0]!=null && ds.Tables[0].Rows.Count>0)
            {
                try
                {
                    /*ViewData["LiquorType"] = ds.Tables[0].Rows[0]["LiquorType"].ToString().Trim();
                    ViewData["LicenseType"] = ds.Tables[0].Rows[0]["LicenceType"].ToString().Trim();
                    ViewData["LicenceNo"] = ds.Tables[0].Rows[0]["LicenceNo"].ToString().Trim();
                    ViewData["QuantityInCase"] = ds.Tables[0].Rows[0]["QuantityInCase"].ToString().Trim();
                    ViewData["QuantityInBottleML"] = ds.Tables[0].Rows[0]["QuantityInBottleML"].ToString().Trim();
                    ViewData["Strength"] = ds.Tables[0].Rows[0]["Strength"].ToString().Trim();
                    */


                    str = ds.Tables[0].Rows[0]["LiquorType"].ToString().Trim()+","+ ds.Tables[0].Rows[0]["LicenceType"].ToString().Trim() + "," + ds.Tables[0].Rows[0]["LicenceNo"].ToString().Trim() + "," + ds.Tables[0].Rows[0]["QuantityInCase"].ToString().Trim() + "," + ds.Tables[0].Rows[0]["QuantityInBottleML"].ToString().Trim() + "," + ds.Tables[0].Rows[0]["Strength"].ToString().Trim();
                } 
                catch{
                    str = "";
                }
                 }
            return str;
        }
        public ActionResult ProductionEntry()
        {
            BottelingPlan Plan = new BottelingPlan();
            List<SelectListItem> BrandList = new List<SelectListItem>();
            CMODataEntryBLL.bindDropDownHnGrid_db2("proc_ddlDetail", BrandList, "BR", "", "");
            ViewBag.Brand = BrandList;
            return View(Plan);
        }
    }
}
