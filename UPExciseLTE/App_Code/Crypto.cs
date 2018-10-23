using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using Microsoft.VisualBasic;
using System.Security.Cryptography;

namespace UPExciseLTE
{
    public class Crypto
    {

        private string secKey = "RNf0UGY2017SpkD2co";
        public string Encrypt(string StrEncode)
        {

            string encodedString = null;

            encodedString = (Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(StrEncode)));

            return (encodedString);

        }
        public string Decrypt(string StrDecode)
        {

            string decodedString = null;

            decodedString = System.Text.ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(StrDecode));

            return decodedString;

        }
    }
}