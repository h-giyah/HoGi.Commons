using System.Collections.Generic;
using Newtonsoft.Json;

namespace HoGi.ToolsAndExtensions.Models.GEO
{
    public class Province : Place
    {


        public int Code { get; set; }

        public string Name { get; set; }

        public string CallingCode { get; set; }

        public Coordinate Coordinate { get; set; }

        public string SejamCode { get; set; }


        public IList<City> Cities { get; set; }

        [JsonIgnore]
        public Country Country { get; set; }

        public override string GetFriendlyName() =>
            $"{Country.Name} - {Name}";

        public override string GetFriendlyNameWithoutCountry()=> $"{Name}";
    }
}