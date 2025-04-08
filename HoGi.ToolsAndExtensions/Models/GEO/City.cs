using System.Collections.Generic;
using Newtonsoft.Json;

namespace HoGi.Commons.ToolsAndExtensions.Models.GEO
{
    public class City : Place
    {
        public int Code { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }

        public Coordinate Coordinate { get; set; }

        public string SejamCode { get; set; }


        public IList<Region> Counties { get; set; }

        [JsonIgnore]
        public Province Province { get; set; }

        public override string GetFriendlyName() =>
            $"{Province.Country.Name} - {Province.Name} - {Name}";
        public override string GetFriendlyNameWithoutCountry() =>
            $"{Province.Name} - {Name}";


    }
}