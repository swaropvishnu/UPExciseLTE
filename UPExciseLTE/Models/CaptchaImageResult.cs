using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;


namespace UPExciseLTE.Models
{
    public class CaptchaImageResult:ActionResult
    {
        public string GetCaptchaString(int length)
        {
            int intZero = '1';
            int intNine = '9';
            int intA = 'A';
            int intZ = 'Z';
            int intCount = 0;
            int intRandomNumber = 0;
            string strCaptchaString="";

            Random random = new Random(System.DateTime.Now.Millisecond);

            while (intCount < length)
            {
                intRandomNumber = random.Next(intZero, intZ);
                if (((intRandomNumber >= intZero) && (intRandomNumber <= intNine) || (intRandomNumber >= intA) && (intRandomNumber <= intZ)))
                {
                    strCaptchaString = strCaptchaString + (char)intRandomNumber;
                    intCount = intCount + 1;
                }
            }
            return strCaptchaString;
        }


        public override void ExecuteResult(ControllerContext context)
        {
            //Bitmap bmp = new Bitmap(90, 30);
            //Graphics g = Graphics.FromImage(bmp);
            //g.Clear(Color.WhiteSmoke);
            //string randomString = GetCaptchaString(4);
            //context.HttpContext.Session["captchastring"] = randomString;
            //g.DrawString(randomString, new Font("Georgia", 19, FontStyle.Strikeout ), new SolidBrush(Color.BlueViolet), 2, 2);
            //HttpResponseBase response = context.HttpContext.Response;
            //response.ContentType = "image/jpeg";
            //bmp.Save(response.OutputStream,ImageFormat.Jpeg);
            //bmp.Dispose();
            
            //myRgbColor = Color.FromRgb(3, 2, 5);
            Bitmap bmp = new Bitmap(100, 30);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.FromArgb(255, 255, 255));
            //bmp.MakeTransparent(Color.blank);
            string randomString = GetCaptchaString(4);
            context.HttpContext.Session["captchastring"] = randomString;
            g.DrawString(randomString, new Font("Georgia", 18, FontStyle.Strikeout), new SolidBrush(Color.RoyalBlue), 2, 2);
            HttpResponseBase response = context.HttpContext.Response;
            response.ContentType = "image/jpeg";
            bmp.Save(response.OutputStream, ImageFormat.Jpeg);
            bmp.Dispose();
        }
    }
}