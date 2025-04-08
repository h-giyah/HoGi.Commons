using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HoGi.Commons.ToolsAndExtensions.Extensions
{
   public static class Useful
    {
        public static IEnumerable<T> OrEmptyIfNull<T>(this IEnumerable<T> source) =>
            source ?? Enumerable.Empty<T>();

        public static void CopyPropertiesTo<T, TU>(this T source, TU dest)
        {
            var sourceProps = typeof(T).GetProperties()
                                       .Where(x => x.CanRead &&
                                                   x.Name != "Id" &&
                                                   x.GetCustomAttributes(typeof(BindNeverAttribute),false).Length==0).ToList();

            var destProps = typeof(TU).GetProperties()
                .Where(x => x.CanWrite)
                .ToList();

            foreach (var sourceProp in sourceProps)
            {
                if (destProps.Any(x => x.Name == sourceProp.Name))
                {
                    var p = destProps.First(x => x.Name == sourceProp.Name);
                    if (p.CanWrite)
                    { // check if the property can be set or no.
                        p.SetValue(dest, sourceProp.GetValue(source, null), null);
                    }
                }

            }

        }


        public static void CopyPropertiesToWithID<T, TU>(this T source, TU dest)
        {
            var sourceProps = typeof(T).GetProperties()
                                       .Where(x => x.CanRead &&
                                                   x.GetCustomAttributes(typeof(BindNeverAttribute),false).Length==0).ToList();

            var destProps = typeof(TU).GetProperties()
                .Where(x => x.CanWrite)
                .ToList();

            foreach (var sourceProp in sourceProps)
            {
                if (destProps.Any(x => x.Name == sourceProp.Name))
                {
                    var p = destProps.First(x => x.Name == sourceProp.Name);
                    if (p.CanWrite)
                    { // check if the property can be set or no.
                        p.SetValue(dest, sourceProp.GetValue(source, null), null);
                    }
                }

            }

        }
        public static string GeneratePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            var res = new StringBuilder();
            var rnd = new Random();
            while (0 < length--) res.Append(valid[rnd.Next(valid.Length)]);
            return res.ToString();
        } 
        public static string GenerateRandomNumberString(int length)
        {
            const string valid = "1234567890";
            var res = new StringBuilder();
            var rnd = new Random();
            while (0 < length--) res.Append(valid[rnd.Next(valid.Length)]);
            return res.ToString();
        }
        public static string GetFileExtension(this string base64String)
        {

            var data = base64String.Substring(0, 5);

            switch (data.ToUpper())
            {
                case "IVBOR":
                    return "png";
                case "/9J/4":
                    return "jpg";
                case "JVBER":
                    return "pdf";
                default:
                    return string.Empty;
            }
        }
        public static string GetImageType(byte[] buffer)
        {
            var headerCode = GetHeaderInfo(buffer).ToUpper();

            if (headerCode.StartsWith("FFD8FFE0"))
                return "JPG";
            if (headerCode.StartsWith("49492A"))
                return "TIFF";
            if (headerCode.StartsWith("424D"))
                return "BMP";
            if (headerCode.StartsWith("474946"))
                return "GIF";
            if (headerCode.StartsWith("89504E470D0A1A0A"))
                return "PNG";
            if (headerCode.StartsWith("JVBER"))
                return "PDF"; //UnKnown
            return string.Empty;
        }

        public static string GetHeaderInfo(byte[] buffer)
        {
            var sb = new StringBuilder();

            foreach (var b in buffer)
                sb.Append(b.ToString("X2"));
            return sb.ToString();
        }

        public static void ForAll<T>(this IList<T> items, Action<T> action)
        {
            foreach (var item in items)
            {
                action.Invoke(item);
            }
        }
    }
}
