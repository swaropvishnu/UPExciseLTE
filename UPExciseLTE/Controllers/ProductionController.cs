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
    public class ProductionController : Controller
    {
        // GET: Production
        [HttpGet]
        public ActionResult BBTFormation()
        {
            ViewBag.Brands = CommonBL.fillBrand("S");
            return View();
        }

        [HttpPost]
        public ActionResult BBTFormation(BBTFormation bbtFormation)
        {
            ViewBag.Brands = CommonBL.fillBrand("S");
            bbtFormation.Status ="A";
            var str = new CommonDA().InsertUpdateBBT(bbtFormation);
            bbtFormation = new Models.BBTFormation();
            if(!string.IsNullOrEmpty(str))
            {
                bbtFormation.Message = new Message();
                bbtFormation.Message.MStatus = "success";
                bbtFormation.Message.TextMessage = str;
            }
            return View(bbtFormation);
        }

        [HttpGet]
        public ActionResult EditBBTFormation(int bbtId)
        {
            ViewBag.Brands = CommonBL.fillBrand("S");
            return View();
        }



        [HttpPut]
        public ActionResult EditBBTFormation(BBTFormation bbtFormation)
        {
            ViewBag.Brands = CommonBL.fillBrand("S");
            bbtFormation.Status = "A";
            var str = new CommonDA().InsertUpdateBBT(bbtFormation);
            bbtFormation = new Models.BBTFormation();
            if (!string.IsNullOrEmpty(str))
            {
                bbtFormation.Message = new Message();
                bbtFormation.Message.MStatus = "success";
                bbtFormation.Message.TextMessage = str;
            }
            return View(bbtFormation);
        }



    }
}