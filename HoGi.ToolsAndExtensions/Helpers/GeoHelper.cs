namespace HoGi.Commons.ToolsAndExtensions.Helpers
{
    public class GeoHelper
    {
        public static string PlaceIdToProvince(string placeId)
        {
            var province = placeId.Split('-');
            if (province.Length < 2)
            {
                return province[0];
            }
            return province[0] + '-' + province[1];
        }

        public static string FriendlyNameToPlace(string friendlyName)
        {
            var place = friendlyName.Split('-');
            return place.Length < 2 ? "ایران" : place[^1].Substring(1, place[^1].Length - 1);
        }

        public static bool IsValid(string placeId)
        {

            var place = placeId.Split('-');
            if (place.Length != 3)
            {
                return false;
            }

            if (!place[0].Equals("108"))
            {
                return false;
            }

            int.TryParse(place[1], out var placeResult);
            return placeResult >= 1 && placeResult <= 31;
        }    
        public static bool IsValidForProvince(string placeId)
        {

            var place = placeId.Split('-');
            if (place.Length != 2)
            {
                return false;
            }

            if (!place[0].Equals("108"))
            {
                return false;
            }

            int.TryParse(place[1], out var placeResult);
            return placeResult >= 1 && placeResult <= 31;
        }
    }
}
