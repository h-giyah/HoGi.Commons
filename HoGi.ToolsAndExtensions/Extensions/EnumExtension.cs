using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using HoGi.Commons.ToolsAndExtensions.Attributes;

namespace HoGi.Commons.ToolsAndExtensions.Extensions
{
    public static class EnumExtension
    {
        
        public static string GetDisplayName(this Enum enumValue)
        {
            
            return enumValue.GetType()
                .GetMember(enumValue.ToString())
                .First()
                .GetCustomAttribute<DisplayAttribute>()?
                .GetName()??" ";
        }

        public static Dictionary<string, int> GetEnumMembers(Type type)
        {
            var result = new Dictionary<string, int>();
            var list = Enum.GetValues(type);
            foreach (var item in list)
            {
                var cast = Enum.Parse(type, item.ToString() ?? string.Empty);
                var title = (Enum)cast;
                var value = (int)cast;
                result.Add(title.GetEnumDescription(), value);
            }

            return result;
        }
        
        public static string GetEnumDescription(this Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());

            if (fi.GetCustomAttributes(typeof(DescriptionAttribute), false) is DescriptionAttribute[] attributes && attributes.Any())
            {
                return attributes.First().Description;
            }
            return value.ToString();
        }
        public static string GetEnumUri(this Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());
            if (fi == null) return "";
            if (fi.GetCustomAttributes(typeof(EndPointUriAttribute), false) is EndPointUriAttribute[] attributes && attributes.Any())
            {
                return attributes.First().Uri;
            }
            return string.Empty;
        }
        public static Range GetEnumRange(this Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());
            if (fi == null) return new Range(0, 0);
            if (fi.GetCustomAttributes(typeof(RangeLimitAttribute), false) is RangeLimitAttribute[] attributes && attributes.Any())
            {
                var attribute = attributes.First();

                return new Range(attribute.Minimum, attribute.Maximum);
            }
            return new Range(0, 0);
        }
        public static TEnum ToEnum<TEnum>(this short enumValue)
        {
            if (Enum.IsDefined(typeof(TEnum), enumValue))
                return (TEnum)Enum.ToObject(typeof(TEnum), enumValue);

            throw new InvalidEnumArgumentException(enumValue.ToString(), enumValue, typeof(TEnum));
        }

    }
}