using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HoGi.Commons.ToolsAndExtensions.Extensions
{
    public static class StringExtension
    {


        private const char ARABI_Y1 = (char)1609;//'ى'
        private const char ARABI_Y2 = (char)1610;//'ي'
        private const char FARSI_Y = (char)1740;//'ی'
        private const char ARABI_K = (char)1603;//'ك'
        private const char FARSI_K = (char)1705;//'ک'

        private static string ReplaceYK(string obj, bool trim = true)
        {
            var retVal = string.IsNullOrEmpty(obj) ? obj : obj.Replace(ARABI_Y1, FARSI_Y).Replace(ARABI_Y2, FARSI_Y).Replace(ARABI_K, FARSI_K);
            if (trim && !string.IsNullOrEmpty(retVal))
            {
                retVal = retVal.Trim();
            }
            return retVal;
        }
        public static string Truncate(this string value, int maxLength)
        {
            return value?.Substring(0, Math.Min(value.Length, maxLength));
        }
        public static string ToStringNumber(this long? obj, string format)
        {
            return obj?.ToString(format);
        }
        public static string ToStringNumber(this decimal? obj, string format)
        {
            return obj?.ToString(format);
        }
        public static string ToJsonString(this object obj)
        {
            try
            {
                var serializerSettings = new JsonSerializerSettings();
                serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                serializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                return JsonConvert.SerializeObject(obj, Formatting.Indented, serializerSettings);
            }
            catch (Exception)
            {
                return "Bad Data,Could Not Be Serialized.";
            }
        }
        public static TModel ToModel<TModel>(this string jsonString)
        {
            if (string.IsNullOrEmpty(jsonString))
                throw new Exception("مقدار غیرمجاز است.");
            try
            {
                var serializerSettings = new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver(), // new CamelCasePropertyNamesContractResolver(),
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };
                return JsonConvert.DeserializeObject<TModel>(jsonString, serializerSettings);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public static string Safe(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;
            var regex = new Regex("[۰۱۲۳۴۵۶۷۸۹]");
            var dictNumbers = new Dictionary<string, string>{
                {"۰", "0" },
                {"۱", "1" },
                {"۲", "2" },
                {"۳", "3" },
                {"۴", "4" },
                {"۵", "5" },
                {"۶", "6" },
                {"۷", "7" },
                {"۸", "8" },
                {"۹", "9" },

            };
            str = regex.Replace(str, (m) => dictNumbers[m.Value]);
            str = str
                .Replace(char.ConvertFromUtf32(8206), "")
                .Replace(char.ConvertFromUtf32(8207), "");
            return ReplaceYK(str).Trim();
        }

        public static string RemoveSigns(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;

            var regex = new Regex("[!@#$%^&()_|:;'/.,}{=`-]");
            str = regex.Replace(str, (m) => "");
            return str.Trim();
        }

        public static bool IsNumber(this string data)
        {
            return !string.IsNullOrWhiteSpace(data) && data.All<char>(new Func<char, bool>(char.IsDigit));
        }
        public static string Sha256(this string data, string salt)
        {
            using var sha256 = SHA256.Create();
            var now = DateTime.Now;

            var saltedValue = Encoding.UTF8
                .GetBytes(data)
                .Concat(Encoding.UTF8.GetBytes(salt))
                .ToArray();

            // Send a sample text to hash.  
            var firstHashedBytes = sha256.ComputeHash(saltedValue);
            var secondHashedBytes = sha256.ComputeHash(firstHashedBytes);
            var thirdHashedBytes = sha256.ComputeHash(secondHashedBytes);
            //{now.Hour:00}:{now.Minute:00}:
            return $"{Convert.ToBase64String(Encoding.UTF8.GetBytes(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")))}:{BitConverter.ToString(thirdHashedBytes)}";
        }

        public static byte[] GetHashBytes(this string data, string salt)
        {
            using var sha256 = SHA256.Create();
            var now = DateTime.Now;

            var saltedValue = Encoding.UTF8
                .GetBytes(data)
                .Concat(Encoding.UTF8.GetBytes(salt))
                .ToArray();

            var hashedBytes = sha256.ComputeHash(saltedValue);

            return hashedBytes;
        }

        public static int[] GetIntListFromStringSeparatedWithComma(this string input)
        {
            return input.Split(',')
                .Where(x => !string.IsNullOrEmpty(x))
                .Select(s => Convert.ToInt32(s.Trim()))
                .ToArray();
        }
        public static List<string> SplitCsv(this string csvList, bool nullOrWhitespaceInputReturnsNull = false)
        {
            if (string.IsNullOrWhiteSpace(csvList))
                return nullOrWhitespaceInputReturnsNull ? null : new List<string>();

            return csvList
                .TrimEnd(',')
                .Split(',')
                .AsEnumerable<string>()
                .Select(s => s.Trim())
                .ToList();
        }

    }//end class
}