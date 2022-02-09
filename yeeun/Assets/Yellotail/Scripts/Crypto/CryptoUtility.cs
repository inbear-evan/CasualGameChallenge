using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace Yellotail.Crypto
{
    public class CryptoUtility
    {
        private static string key = ":{j%6j?E:t#}G10mM%9hp5S=%}2,Y26C";

        private static RijndaelManaged provider = null;

        static CryptoUtility()
        {
            provider = new RijndaelManaged();
            provider.Key = Encoding.UTF8.GetBytes(key);
            provider.Mode = CipherMode.ECB;
        }

        public static string Encrypt(string plainText)
        {
            var sourceBytes = Encoding.UTF8.GetBytes(plainText);

            var encryptor = provider.CreateEncryptor();
            var outputBytes = encryptor.TransformFinalBlock(sourceBytes, 0, sourceBytes.Length);
            return Convert.ToBase64String(outputBytes);
        }

        public static string Decrypt(string cipherText)
        {
            var sourceBytes = Convert.FromBase64String(cipherText);

            var decryptor = provider.CreateDecryptor();
            var outputBytes = decryptor.TransformFinalBlock(sourceBytes, 0, sourceBytes.Length);
            return Encoding.UTF8.GetString(outputBytes);
        }

        public static string GetMd5HashFromFile(string filepath)
        {
            using (var fileStream = File.OpenRead(filepath))
            {
                using (var provider = new MD5CryptoServiceProvider())
                {
                    var array = provider.ComputeHash(fileStream);
                    return null;
                    //return array.ToHexString();
                }
            }
        }
        public static string GetSha1HashFromFile(string filepath)
        {
            using (var fileStream = File.OpenRead(filepath))
            {
                using (var provider = new SHA1CryptoServiceProvider())
                {
                    var array = provider.ComputeHash(fileStream);
                    return null;
                    //return array.ToHexString();
                }
            }
        }
        public static string GetSha256HashFromFile(string filepath)
        {
            using (var fileStream = File.OpenRead(filepath))
            {
                using (var provider = new SHA256CryptoServiceProvider())
                {
                    var array = provider.ComputeHash(fileStream);
                    return null;
                    //return array.ToHexString();
                }
            }
        }
        public static void GetRandomBytes(byte[] array)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(array);
            }
        }
    }   
}
