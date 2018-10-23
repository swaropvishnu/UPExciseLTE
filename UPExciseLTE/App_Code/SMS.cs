using System;
using System.Collections.Generic;
using System.IO;
//using System.Linq;
using System.Net;
using System.Net.Security;
using System.Text;
using System.Web;


namespace UPExciseLTE
{
    public class SMS
    {

          String username = "ECISMS-UPCEO";
          String password = "Admin@123";
          String senderid = "ECISMS";
          String message = "";
          String mobileNo = "";
          String mobileNos = "";
        //  static String scheduledTime = "20110819 13:26:00";
         //static HttpWebRequest request;
         //static Stream dataStream;

        //public string SendSMS(string mobileno, string message_text)
    //    {

    //        string url = "";
    //        int results = 0;
    //        string webresponse = "";
    //        try
    //        {
    //            Class1 objClass1 = new Class1();

    //            string mobiles = HttpContext.Current.Server.UrlEncode(objClass1.Encrypt("91" + mobileno));
    //            string msg = HttpContext.Current.Server.UrlEncode(objClass1.Encrypt(ConversionClass.ConvertStringToHex(message_text)));


    //            //url = "https://secure.up.nic.in/upcmosms/Default.aspx?dest=" + mobileno + "&msg=" + message_text + "";
    //            String finalmessage = "";
    //            String sss = "";

    //            foreach (char c in message_text)
    //            {
    //                int j = (int)c;
    //                sss = "&#" + j + ";";
    //                finalmessage = finalmessage + sss;
    //                //Console.WriteLine("Message in method==" + finalmessage);
    //            }

    //            url = "http://msdgweb.mgov.gov.in/esms/sendsmsrequest?username=ECISMS-UPCEO&password=Admin@123&smsservicetype=singlemsg&content=" + msg + "&mobileno=" + mobileno + "&senderid=ECISMS";


    //            HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(url);
    //            //ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback( CertificateValidationCallBack);
    //            ServicePointManager.ServerCertificateValidationCallback +=
    //new RemoteCertificateValidationCallback((sender, certificate, chain, policyErrors) => { return true; });
    //            HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();
    //            StreamReader sr = new StreamReader(httpres.GetResponseStream());
    //            webresponse = sr.ReadToEnd();
    //            sr.Close();
    //            return webresponse;
    //        }
    //        catch (Exception ex)
    //        {
    //            return ex.ToString();
    //        }
    //    }

        // Method for sending single SMS.

        //public static void sendSingleSMS(String username, String password, String senderid,
        //    String mobileNo, String message)
        //{

        //    HttpWebRequest request;
        //    Stream dataStream;
        //    String smsservicetype = "singlemsg"; //For single message.
        //    String query = "username=" + HttpUtility.UrlEncode(username) +
        //        "&password=" + HttpUtility.UrlEncode(password) +
        //        "&smsservicetype=" + HttpUtility.UrlEncode(smsservicetype) +
        //        "&content=" + HttpUtility.UrlEncode(message) +
        //        "&mobileno=" + HttpUtility.UrlEncode(mobileNo) +
        //        "&senderid=" + HttpUtility.UrlEncode(senderid);

        //    byte[] byteArray = Encoding.ASCII.GetBytes(query);
        //    request.ContentType = "application/x-www-form-urlencoded";
        //    request.ContentLength = byteArray.Length;

        //    dataStream = request.GetRequestStream();
        //    dataStream.Write(byteArray, 0, byteArray.Length);
        //    dataStream.Close();
        //    WebResponse response = request.GetResponse();
        //    String Status = ((HttpWebResponse)response).StatusDescription;
        //    dataStream = response.GetResponseStream();
        //    StreamReader reader = new StreamReader(dataStream);
        //    string responseFromServer = reader.ReadToEnd();
        //    reader.Close();
        //    dataStream.Close();
        //    response.Close();
        //}
        // method for sending bulk SMS
        //public static void sendBulkSMS(String username, String password, String senderid, String mobileNos, String message)
        //{

        //    HttpWebRequest request;
        //    Stream dataStream;

        //    String smsservicetype = "bulkmsg"; // for bulk msg
        //    String query = "username=" + HttpUtility.UrlEncode(username) +
        //        "&password=" + HttpUtility.UrlEncode(password) +
        //        "&smsservicetype=" + HttpUtility.UrlEncode(smsservicetype) +
        //        "&content=" + HttpUtility.UrlEncode(message) +
        //        "&bulkmobno=" + HttpUtility.UrlEncode(mobileNos) +
        //        "&senderid=" + HttpUtility.UrlEncode(senderid);
        //    byte[] byteArray = Encoding.ASCII.GetBytes(query);
        //    request.ContentType = "application/x-www-form-urlencoded";
        //    request.ContentLength = byteArray.Length;
        //    dataStream = request.GetRequestStream();
        //    dataStream.Write(byteArray, 0, byteArray.Length);
        //    dataStream.Close();
        //    WebResponse response = request.GetResponse();
        //    String Status = ((HttpWebResponse)response).StatusDescription;
        //    dataStream = response.GetResponseStream();
        //    StreamReader reader = new StreamReader(dataStream);
        //    string responseFromServer = reader.ReadToEnd();
        //    reader.Close();
        //    dataStream.Close();
        //    response.Close();
        //}

        //   for sending unicode
        public string SendSMS(string mobileno, string message_text)
        {

            HttpWebRequest request;
            Stream dataStream;

            request = (HttpWebRequest)WebRequest.Create("http://msdgweb.mgov.gov.in/esms/sendsmsrequest");
            request.ProtocolVersion = HttpVersion.Version10;
            //((HttpWebRequest)request).UserAgent = ".NET Framework Example Client";
            ((HttpWebRequest)request).UserAgent = "Mozilla/4.0 (compatible; MSIE 5.0; Windows 98; DigExt)";
            request.Method = "POST";
            //Console.WriteLine("Before Calling Method");
            //sendSingleSMS(username, password, senderid, mobileNo, message);
            //sendUnicodeSMS(username, password, senderid, mobileNos, message);

            Console.WriteLine("Response after Calling Method====");

            try
            {

                String username = "ECISMS-UPCEO";
                String password = "Admin@123";
                String senderid = "ECISMS";

                String finalmessage = "";
                String sss = "";

                foreach (char c in message_text)
                {
                    int j = (int)c;
                    sss = "&#" + j + ";";
                    finalmessage = finalmessage + sss;
                    Console.WriteLine("Message in method==" + finalmessage);
                }
                Console.WriteLine("Before Calling Message" + finalmessage);
                message_text = finalmessage;
                String smsservicetype = "unicodemsg"; // for unicode msg
                String query = "username=" + HttpUtility.UrlEncode(username) +
                    "&password=" + HttpUtility.UrlEncode(password) +
                    "&smsservicetype=" + HttpUtility.UrlEncode(smsservicetype) +
                    "&content=" + HttpUtility.UrlEncode(message_text) +
                    "&bulkmobno=" + HttpUtility.UrlEncode(mobileno) +
                    "&senderid=" + HttpUtility.UrlEncode(senderid);

                Console.WriteLine("URL==" + query);
                byte[] byteArray = Encoding.ASCII.GetBytes(query);
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;
                dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse response = request.GetResponse();
                String Status = ((HttpWebResponse)response).StatusDescription;
                dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                string webresponse = responseFromServer;
                //Console.WriteLine("response==" + responseFromServer);
                reader.Close();
                dataStream.Close();
                response.Close();
                return webresponse;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }


        public class Class1
        {
            public string Encrypt(string StrEncode)
            {
                string encodedString = null;
                encodedString = (Convert.ToBase64String(System.Text.UnicodeEncoding.ASCII.GetBytes(StrEncode)));
                return (encodedString);
            }

            public string Decrypt(string StrDecode)
            {

                string decodedString = null;
                decodedString = System.Text.UnicodeEncoding.ASCII.GetString(Convert.FromBase64String(StrDecode));
                return decodedString;


            }
        }
        public static class ConversionClass
        {
            public static string ConvertStringToHex(string s)
            {
                string result = string.Empty;
                foreach (char c in s)
                {
                    int tmp = c;
                    result += String.Format("{0:x4}", (uint)System.Convert.ToUInt32(tmp.ToString()));
                }
                result = result.ToUpper();
                return result;
            }
        }
    }
}
