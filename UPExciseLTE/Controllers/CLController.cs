using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UPExciseLTE.BLL;
using UPExciseLTE.DAL;
using UPExciseLTE.Models;

namespace UPExciseLTE.Controllers
{
   
    public class CLController : Controller
    {
        [HttpGet]
        public ActionResult StorageVAT()
        {
            StorageVAT UT = new StorageVAT();
            if (Request.QueryString["A"] != null && Request.QueryString["A"].Trim() != string.Empty)
            {
                UT = new CommonBL().GetStorageVAT(-1, short.Parse(new Crypto().Decrypt(Request.QueryString["A"].Trim())), "Z");
            }
            ViewBag.Msg = TempData["Msg"];
            ViewBag.Brewery = CommonBL.fillBrewery();
            ViewBag.UnitTank = new CommonBL().GetStorageVAT(short.Parse(CommonBL.fillBrewery()[0].Value.Trim()), -1, "Z");
            return View(UT);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UnitTank(UnitTank UT)
        {
            string str = new CommonDA().InsertUpdateUnitTank(UT);
            TempData["Msg"] = str;
            return RedirectToAction("StorageVAT");
        }
    }
}