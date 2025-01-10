using System.Collections.Generic;

namespace HoGi.ToolsAndExtensions.Models.GEO
{
    public class Country : Place
    {
        public int Code { get; set; }

        public string Name { get; set; }

        public string NativeName { get; set; }

        public string ShortName { get; set; }

        public string CallingCode { get; set; }

        public string Language { get; set; }

        public string TopLevelDomain { get; set; }

        public string FlagUri { get; set; }

        public Coordinate Coordinate { get; set; }

        public Currency Currency { get; set; }



        public string SejamCode { get; set; }

        public IList<Province> Provinces { get; set; }


        public override string GetFriendlyName() =>
            $"{Name}";

        public override string GetFriendlyNameWithoutCountry() => "";
        
        
    }
}