﻿using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using Microsoft.VisualBasic;
using System.Security.Cryptography;
using System.Linq;
using System.IO;
using System.Text;

namespace UPExciseLTE
{
    public class Crypto
    {


        #region new encrypt dycrypt

        // This constant is used to determine the keysize of the encryption algorithm in bits.
        // We divide this by 8 within the code below to get the equivalent number of bytes.
        private const int Keysize = 256;

        // This constant determines the number of iterations for the password bytes generation function.
        private const int DerivationIterations = 1000;
        private string Encryptionkey = "RNf0UGY2017SpkD2co";
        public string Encrypt(string textData)
        {
            RijndaelManaged objrij = new RijndaelManaged();
            //set the mode for operation of the algorithm   
            objrij.Mode = CipherMode.CBC;
            //set the padding mode used in the algorithm.   
            objrij.Padding = PaddingMode.PKCS7;
            //set the size, in bits, for the secret key.   
            objrij.KeySize = 0x80;
            //set the block size in bits for the cryptographic operation.    
            objrij.BlockSize = 0x80;
            //set the symmetric key that is used for encryption & decryption.    
            byte[] passBytes = Encoding.UTF8.GetBytes(Encryptionkey);
            //set the initialization vector (IV) for the symmetric algorithm    
            byte[] EncryptionkeyBytes = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

            int len = passBytes.Length;
            if (len > EncryptionkeyBytes.Length)
            {
                len = EncryptionkeyBytes.Length;
            }
            Array.Copy(passBytes, EncryptionkeyBytes, len);

            objrij.Key = EncryptionkeyBytes;
            objrij.IV = EncryptionkeyBytes;

            //Creates symmetric AES object with the current key and initialization vector IV.    
            ICryptoTransform objtransform = objrij.CreateEncryptor();

            byte[] textDataByte = Encoding.UTF8.GetBytes(textData);


            //Final transform the test string.  
            return Convert.ToBase64String(objtransform.TransformFinalBlock(textDataByte, 0, textDataByte.Length)).ToString().Replace('+', '-').Replace('/', '_').Replace('=', ',');  //.Replace('/', '_').Replace('=', ',')


        }

        public string Decrypt(string EncryptedText)
        {
            RijndaelManaged objrij = new RijndaelManaged();
            objrij.Mode = CipherMode.CBC;
            objrij.Padding = PaddingMode.PKCS7;

            objrij.KeySize = 0x80;
            objrij.BlockSize = 0x80;
            byte[] encryptedTextByte = Convert.FromBase64String(EncryptedText.ToString().Replace('-', '+').Replace('_', '/').Replace(',', '='));   //.Replace('_', '/').Replace(',', '=')
            byte[] passBytes = Encoding.UTF8.GetBytes(Encryptionkey);
            byte[] EncryptionkeyBytes = new byte[0x10];
            int len = passBytes.Length;
            if (len > EncryptionkeyBytes.Length)
            {
                len = EncryptionkeyBytes.Length;
            }
            Array.Copy(passBytes, EncryptionkeyBytes, len);
            objrij.Key = EncryptionkeyBytes;
            objrij.IV = EncryptionkeyBytes;

            byte[] TextByte = objrij.CreateDecryptor().TransformFinalBlock(encryptedTextByte, 0, encryptedTextByte.Length);
            return Encoding.UTF8.GetString(TextByte);  //it will return readable string  

        }
        //public string Encrypt(string plainText)
        //{
        //    // Salt and IV is randomly generated each time, but is preprended to encrypted cipher text
        //    // so that the same Salt and IV values can be used when decrypting.  

        //    string passPhrase = "RNf0UGY2017SpkD2co";
        //    var saltStringBytes = Generate256BitsOfRandomEntropy();
        //    var ivStringBytes = Generate256BitsOfRandomEntropy();
        //    var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
        //    using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
        //    {
        //        var keyBytes = password.GetBytes(Keysize / 8);
        //        using (var symmetricKey = new RijndaelManaged())
        //        {
        //            symmetricKey.BlockSize = 256;
        //            symmetricKey.Mode = CipherMode.CBC;
        //            symmetricKey.Padding = PaddingMode.PKCS7;
        //            using (var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes))
        //            {
        //                using (var memoryStream = new MemoryStream())
        //                {
        //                    using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
        //                    {
        //                        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
        //                        cryptoStream.FlushFinalBlock();
        //                        // Create the final bytes as a concatenation of the random salt bytes, the random iv bytes and the cipher bytes.
        //                        var cipherTextBytes = saltStringBytes;
        //                        cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
        //                        cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();
        //                        memoryStream.Close();
        //                        cryptoStream.Close();
        //                        return Convert.ToBase64String(cipherTextBytes);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        //public string Decrypt(string cipherText)
        //{
        //    string passPhrase = "RNf0UGY2017SpkD2co";
        //    // Get the complete stream of bytes that represent:
        //    // [32 bytes of Salt] + [32 bytes of IV] + [n bytes of CipherText]
        //    var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cipherText);
        //    // Get the saltbytes by extracting the first 32 bytes from the supplied cipherText bytes.
        //    var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(Keysize / 8).ToArray();
        //    // Get the IV bytes by extracting the next 32 bytes from the supplied cipherText bytes.
        //    var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(Keysize / 8).Take(Keysize / 8).ToArray();
        //    // Get the actual cipher text bytes by removing the first 64 bytes from the cipherText string.
        //    var cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip((Keysize / 8) * 2).Take(cipherTextBytesWithSaltAndIv.Length - ((Keysize / 8) * 2)).ToArray();

        //    using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
        //    {
        //        var keyBytes = password.GetBytes(Keysize / 8);
        //        using (var symmetricKey = new RijndaelManaged())
        //        {
        //            symmetricKey.BlockSize = 256;
        //            symmetricKey.Mode = CipherMode.CBC;
        //            symmetricKey.Padding = PaddingMode.PKCS7;
        //            using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes))
        //            {
        //                using (var memoryStream = new MemoryStream(cipherTextBytes))
        //                {
        //                    using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
        //                    {
        //                        var plainTextBytes = new byte[cipherTextBytes.Length];
        //                        var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
        //                        memoryStream.Close();
        //                        cryptoStream.Close();
        //                        return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        private static byte[] Generate256BitsOfRandomEntropy()
        {
            var randomBytes = new byte[32]; // 32 Bytes will give us 256 bits.
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                // Fill the array with cryptographically secure random bytes.
                rngCsp.GetBytes(randomBytes);
            }
            return randomBytes;
        }

        #endregion

        private string secKey = "RNf0UGY2017SpkD2co";
        //public string Encrypt(string StrEncode)
        //{

        //    string encodedString = null;

        //    encodedString = (Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(StrEncode)));

        //    return (encodedString);

        //}
        //public string Decrypt(string StrDecode)
        //{

        //    string decodedString = null;

        //    decodedString = System.Text.ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(StrDecode));

        //    return decodedString;

        //}
    }
}