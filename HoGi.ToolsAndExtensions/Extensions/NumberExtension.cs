namespace HoGi.Commons.ToolsAndExtensions.Extensions
{
    public static class NumberExtension
    {
        public static double ToDouble(this string value) {
            if (double.TryParse(value, out var result))
                return result;
            return -1;
        }

        public static int ToInt(this string value)
        {
            if (int.TryParse(value, out var result))
                return result;
            return -1;
        }

        public static long ToLong(this string value)
        {
            if (long.TryParse(value, out var result))
                return result;
            return -1;
        }

        
    }
}
