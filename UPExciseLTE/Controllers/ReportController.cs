using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UPExciseLTE.DAL;
using UPExciseLTE.Models;
using UPExciseLTE.BLL;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using ZXing;
using UPExciseLTE.Filters;

namespace UPExciseLTE.Controllers
{
    [SessionExpireFilterAttribute]
    [NoCache]
    [ChkAuthorization]
    [HandleError(View = "Error")]
    public class ReportController : Controller
    {
        // GET: Report

        [HttpGet]
        public ActionResult GetGatePassForDistrictWholesaleToRetailor()
        {
            var loggedInUserInformation = GetInformationByLoggedInUserLeve();
            ViewBag.PassTypeInformation = loggedInUserInformation.PassTypeInformation;
            return View();
        }


        [HttpGet]
        public ActionResult GetGatePassForDistrictWholesaleToRetailor1()
        {
            var applicationAllotmentShopList = new CommonBL().GetGatePassReport();
            return Json(new { data = applicationAllotmentShopList }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GatePassReport()
        {
            int spType1 = 0;
            int spType2 = 0;
            if (Convert.ToInt32(UserSession.LoggedInUserLevelId) == 15)
            {
                spType1 = 5;
                spType2 = 4;
            }
            else if (Convert.ToInt32(UserSession.LoggedInUserLevelId) == 45)
            {
                spType1 = 14;
                spType2 = 13;
            }
            else if (Convert.ToInt32(UserSession.LoggedInUserLevelId) == 35)
            {
                spType1 = 16;
                spType2 = 15;
            }
            string qrcode = "EXCISE";
            ViewBag.QRCodeImage = GenerateQRCode(qrcode);

            var loggedInUserInformation = GetInformationByLoggedInUserLeve();
            ViewBag.Reciever = loggedInUserInformation.Receiver;
            ViewBag.PassType = loggedInUserInformation.PassType;
            var gatePass = new GatePass();

            if (Request.QueryString["GatePass"] != null && Request.QueryString["GatePass"].Trim() != string.Empty)
            {
                gatePass = new CommonBL().GenerateGatePass(long.Parse(new Crypto().Decrypt(Request.QueryString["GatePass"].Trim())), spType1, spType2);
            }
            return View(gatePass);
        }

        private string GenerateQRCode(string qrcodeText)
        {
            string folderPath = "~/Img/";
            string imagePath = "~/Img/QrCode.jpg";
            // If the directory doesn't exist then create it.
            if (!Directory.Exists(Server.MapPath(folderPath)))
            {
                Directory.CreateDirectory(folderPath);
            }

            var barcodeWriter = new BarcodeWriter();
            barcodeWriter.Format = BarcodeFormat.QR_CODE;
            var result = barcodeWriter.Write(qrcodeText);

            string barcodePath = Server.MapPath(imagePath);
            var barcodeBitmap = new Bitmap(result);
            using (MemoryStream memory = new MemoryStream())
            {
                using (FileStream fs = new FileStream(barcodePath, FileMode.Create, FileAccess.ReadWrite))
                {
                    barcodeBitmap.Save(memory, ImageFormat.Jpeg);
                    byte[] bytes = memory.ToArray();
                    fs.Write(bytes, 0, bytes.Length);
                }
            }
            return imagePath;
        }

        //private QRCodeModel ReadQRCode()
        //{
        //    QRCodeModel barcodeModel = new QRCodeModel();
        //    string barcodeText = "";
        //    string imagePath = "~/Images/QrCode.jpg";
        //    string barcodePath = Server.MapPath(imagePath);
        //    var barcodeReader = new BarcodeReader();

        //    var result = barcodeReader.Decode(new Bitmap(barcodePath));
        //    if (result != null)
        //    {
        //        barcodeText = result.Text;
        //    }
        //    return new QRCodeModel() { QRCodeText = barcodeText, QRCodeImagePath = imagePath };
        //}


        public ActionResult BarCodeProducution()
        {
            return View();
        }

        public InformationByLoggedInUserLevel GetInformationByLoggedInUserLeve()
        {
            var loggedInUserInformation = new InformationByLoggedInUserLevel();
            if (Convert.ToInt32(UserSession.LoggedInUserLevelId) == 45)
            {
                loggedInUserInformation.PassType = "FL-B11";
            }
            else if (Convert.ToInt32(UserSession.LoggedInUserLevelId) == 35)
            {
                loggedInUserInformation.PassType = "FL-36";
            }
            else if (Convert.ToInt32(UserSession.LoggedInUserLevelId) == 15)
            {
                loggedInUserInformation.PassType = "B-12";
            }
            else
                loggedInUserInformation.PassType = "FL-B11";


            if (Convert.ToInt32(UserSession.LoggedInUserLevelId) == 45)
            {
                loggedInUserInformation.Receiver = "Lucknow Warehouse";
                loggedInUserInformation.PassTypeInformation = "Manufacturer Wholesale to Disctrict Wholesale";
            }
            else if (Convert.ToInt32(UserSession.LoggedInUserLevelId) == 35)
            {
                loggedInUserInformation.Receiver = "Sahu & Sons Kanpur Road";
                loggedInUserInformation.PassTypeInformation = "District Wholesale to Retailor";
            }
            else if (Convert.ToInt32(UserSession.LoggedInUserLevelId) == 15)
            {
                loggedInUserInformation.Receiver = "M/s Mohan Goldwater Breweries Ltd. Warehouse";
                loggedInUserInformation.PassTypeInformation = "Brewery To Manufacturer Wholesale";

            }
            else
                loggedInUserInformation.Receiver = "FL-B11";

            return loggedInUserInformation;

        }






    }
}