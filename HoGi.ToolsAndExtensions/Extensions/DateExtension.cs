using System;
using System.Globalization;

namespace HoGi.Commons.ToolsAndExtensions.Extensions
{
    public static class DateExtension
    {
        public const int LegalAge = 18; 
        public static CultureInfo UsaCulture = new CultureInfo("en-US");
        public static readonly DateTime MinDate = new DateTime(1970, 1, 1);
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            var diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }
        
        public static string ToPersianDateTime(this DateTime dateValue)
        {
            var persianCalendar = new PersianCalendar();
            return
                $"{persianCalendar.GetYear(dateValue):0000}/{persianCalendar.GetMonth(dateValue):00}/{persianCalendar.GetDayOfMonth(dateValue):00} {dateValue.ToString("HH:mm:ss", UsaCulture)}";
        }
        public static string ToPersianDateTimeInRoute(this DateTime dateValue)
        {
            return dateValue.ToPersianDateTime().Replace("/", "-").Replace(" ", "T");
        }

        public static int ToPersianDate(this DateTime dateValue)
        {
            try
            {
                var persianCalendar = new PersianCalendar();
                return (persianCalendar.GetYear(dateValue) * 10000) + (persianCalendar.GetMonth(dateValue) * 100) + persianCalendar.GetDayOfMonth(dateValue);
            }
            catch
            {
                return 0;
            }
        }
        public static string ToPersianDateString(this DateTime dateValue)
        {
            var persianDate = dateValue.ToPersianDate();
            if (persianDate == 0)
                return "";
            return persianDate.ToString("####/##/##");

        }
        public static string ToPersianDateString(this DateTime? dateValue)
        {
            if (dateValue == null) return "";

            return dateValue.Value.ToPersianDate().ToString("####/##/##");
        }
        public static string ToPersianDateShortString(this DateTime dateValue)
        {
            var persianCalendar = new PersianCalendar();
            return $"{(persianCalendar.GetMonth(dateValue).ToString("00"))}/{persianCalendar.GetDayOfMonth(dateValue):00}";
        }
        public static string ToPersianDateShortString(this DateTime? dateValue)
        {
            if (dateValue == null) return "";

            var persianCalendar = new PersianCalendar();
            return $"{(persianCalendar.GetMonth(dateValue.Value).ToString("00"))}/{persianCalendar.GetDayOfMonth(dateValue.Value):00}";
        }
        public static string ToPersianDateTime(this DateTime? dateValue)
        {
            string returnValue = null;
            if (dateValue.HasValue)
            {
                returnValue = dateValue.Value.ToPersianDateTime();
            }
            return returnValue;
        }
        public static string ToPersianDate(this DateTime? dateValue)
        {
            string returnValue = null;
            if (dateValue.HasValue)
            {
                returnValue = dateValue.Value.ToPersianDateString();
            }
            return returnValue;
        }

        public static int GetPersianYear(this DateTime dateValue)
        {
            var persianCalendar = new PersianCalendar();
            return persianCalendar.GetYear(dateValue) * 10000;
        }


        public static int GetPersianYear(this DateTime? dateValue)
        {
            if (dateValue == null) return -1;
            var persianCalendar = new PersianCalendar();
            return persianCalendar.GetYear(dateValue.Value) * 10000;
        }
        public static int GetPersianMount(this DateTime dateValue)
        {
            var persianCalendar = new PersianCalendar();
            return persianCalendar.GetMonth(dateValue) * 100;
        }



        public static int GetPersianMount(this DateTime? dateValue)
        {
            if (dateValue == null) return -1;
            var persianCalendar = new PersianCalendar();
            return persianCalendar.GetMonth(dateValue.Value) * 100;
        }

        public static int GetPersianDay(this DateTime dateValue)
        {
            var persianCalendar = new PersianCalendar();
            return persianCalendar.GetDayOfMonth(dateValue);
        }
        public static int GetPersianDay(this DateTime? dateValue)
        {
            if (dateValue == null) return -1;
            var persianCalendar = new PersianCalendar();
            return persianCalendar.GetDayOfMonth(dateValue.Value);
        }
        public static DateTime ToDateTime(this string dateValue)
        {
            return int.Parse((dateValue ?? "0").Replace("/", "")).ToDateTime();
        }
        public static int GetDaysInMonth(this DateTime datetime)
        {
            var persianCalendar = new PersianCalendar();
            return persianCalendar.GetDaysInMonth(persianCalendar.GetYear(datetime), persianCalendar.GetMonth(datetime));

        }

        public static DateTime ToDateTime(this int dateValue)
        {
            if(dateValue==0)
                return MinDate;

            var persianCalendar = new PersianCalendar();
            var year = dateValue / 10000;
            var month = (dateValue % 10000) / 100;
            var day = (dateValue % 100);
            return persianCalendar.ToDateTime(year, month, day, 0, 0, 0, 0);
        }

        public static DateTime ToDateTimeBankPasargad(this string dateValue)
        {
            //1399 / 11 / 05 - 07:29:50.6117140
            var persianCalendar = new PersianCalendar();
            var datetime = dateValue.Split(' ');
            var date = datetime[0].Split('/');
            var year = date[0];
            var month = date[1];
            var day = date[2];
            var time = datetime[2].Split(':');
            var hour = time[0];
            var minute = time[1];
            var second = time[2].Split('.')[0];

            return persianCalendar.ToDateTime(int.Parse(year), int.Parse(month), int.Parse(day), int.Parse(hour), int.Parse(minute), int.Parse(second), 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <param name="toDate">default is DateTime.Now</param>
        /// <returns></returns>
        public static double ToAgeInYear(this DateTime date, DateTime? toDate = default)
        {
            toDate ??= DateTime.Now;
            var totalDays = (toDate.Value - date).TotalDays;
            if (totalDays < 365)
                return 0;
            return totalDays / 365;
        }
        public static double ToAgeInDay(this DateTime date, DateTime? toDate = default)
        {
            toDate ??= DateTime.Now;
            var totalDays = (toDate.Value - date).TotalDays;

            return totalDays;
        }
    }
}
