using System;
using System.Collections.Generic;
using System.Text;

namespace A35Mge.Infrastructure
{
    public static class ToolsHelper
    {
        /// <summary>
        /// 进行MD5加密
        /// </summary>
        /// <param name="md5"></param>
        /// <returns></returns>
        public static string Get32Md5(this string md5)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider md = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] value, hash;
            value = System.Text.Encoding.UTF8.GetBytes(md5);
            hash = md.ComputeHash(value);
            md.Clear();
            string temp = "";
            for (int i = 0, len = hash.Length; i < len; i++)
            {
                temp += hash[i].ToString("X").PadLeft(2, '0');
            }
            return temp;

        }
    }
}
