using Newtonsoft.Json;

namespace HoGi.Commons.ToolsAndExtensions.Models.GEO
{
    public class Village : Place
    {
        public int Code { get; set; }

        public string Name { get; set; }

        public string SejamCode { get; set; }

        [JsonIgnore]
        public Region Region { get; set; }


        public override string GetFriendlyName() =>
            $"{Region.City.Province.Country.Name} - {Region.City.Province.Name} - {Region.City.Name} - {Region.Name} - {Name}";

        public override string GetFriendlyNameWithoutCountry()=>
            $"{Region.City.Province.Name} - {Region.City.Name} - {Region.Name} - {Name}";

        
    }

}
