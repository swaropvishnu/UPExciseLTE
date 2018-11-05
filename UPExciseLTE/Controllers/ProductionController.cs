using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UPExciseLTE.Controllers
{
    public class ProductionController : Controller
    {
        // GET: Production
        public ActionResult BBTFormation()
        {
            ViewBag.Brands = new List<SelectListItem>();
            ViewBag.LiquorTypes= new List<SelectListItem>();
            ViewBag.LicenseTypes= new List<SelectListItem>();
            return View();
        }
    }
}