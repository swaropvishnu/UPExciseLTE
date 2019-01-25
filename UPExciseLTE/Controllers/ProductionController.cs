using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UPExciseLTE.BLL;
using UPExciseLTE.DAL;
using UPExciseLTE.Filters;
using UPExciseLTE.Models;

namespace UPExciseLTE.Controllers
{
    [SessionExpireFilterAttribute]
    [ChkAuthorization]
    public class ProductionController : Controller
    {
        // GET: Production

        #region BBTFormation

        /*[HttpGet]
        public ActionResult BBTFormation()
        {
            var bbtFormation = new Models.BBTFormation();
            var MasterFormsController = new MasterFormsController();
            if (Request.QueryString["Code"] != null && Request.QueryString["Code"].Trim() != string.Empty)
            {
                bbtFormation = new CommonBL().GetBBTMasterList(int.Parse(new Crypto().Decrypt(Request.QueryString["Code"].Trim())), -1,"Z").FirstOrDefault();
                var str = MasterFormsController.GetBrandDetailsForDDl(bbtFormation.BrandID.ToString()).Split(',');
                bbtFormation.LiquorType = str[0];
                bbtFormation.LicenseType = str[1];
            }
            ViewBag.Brands = CommonBL.fillBrand("S");
            return View(bbtFormation);
        }

        [HttpPost]
        public ActionResult BBTFormation(BBTFormation bbtFormation)
        {
            ViewBag.Brands = CommonBL.fillBrand("S");
            bbtFormation.Status ="A";
            var str = new CommonDA().InsertUpdateBBT(bbtFormation);
            bbtFormation = new BBTFormation();
            if(!string.IsNullOrEmpty(str))
            {
                bbtFormation.Message= Message.MsgSuccess(str);
            }
            return View(bbtFormation);
        }
        [HttpGet]
        public ActionResult GetBBTDetails( int brandId=-1)
        {
            var bbtFormations = new List<BBTFormation>();
            ViewBag.Brands = CommonBL.fillBrand("S");
            bbtFormations = new CommonBL().GetBBTMasterList(-1,brandId,"Z");
            return View(bbtFormations);
        }
        [HttpGet]
        public ActionResult DeleteBBT(int bbtId)
        {
            var bbtFormation = new Models.BBTFormation();
            bbtFormation.BBTId = bbtId;
            bbtFormation.SP_Type = 3;
            var str = new CommonDA().InsertUpdateBBT(bbtFormation);
            bbtFormation = new BBTFormation();
            if (!string.IsNullOrEmpty(str))
            {
                bbtFormation.Message = new Message();
                bbtFormation.Message.MStatus = "info";
                bbtFormation.Message.TextMessage = str;
            }
            return PartialView("~/Views/Shared/_ErrorMessage.cshtml", bbtFormation.Message);
        }
        */
        #endregion




    }
}