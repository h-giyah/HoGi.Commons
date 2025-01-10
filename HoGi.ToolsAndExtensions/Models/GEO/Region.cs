using System.Collections.Generic;
using Newtonsoft.Json;

namespace HoGi.ToolsAndExtensions.Models.GEO
{
    public class Region : Place
    {
        public int Code { get; set; }

        public string Name { get; set; }

        public string SejamCode { get; set; }

        public string DistrictName { get; set; }

        public string DistrictSejamCode { get; set; }


        public IList<Village> Villages { get; set; }

        [JsonIgnore]
        public City City { get; set; }


        public override string GetFriendlyName() =>
            $"{City.Province.Country.Name} - {City.Province.Name} - {City.Name} - {Name}";

        public override string GetFriendlyNameWithoutCountry() => $"{City.Province.Name} - {City.Name} - {Name}";
    }
}