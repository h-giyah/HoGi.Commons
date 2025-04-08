namespace HoGi.Commons.ToolsAndExtensions.Models.GEO
{
    public abstract class Place
    {
        public string PlaceId { get; internal set; }
        /// <summary>
        /// تهران
        /// </summary>
        public static string Default => "108-8-6";
        public abstract string GetFriendlyName();
        public abstract string GetFriendlyNameWithoutCountry();

    }
}
