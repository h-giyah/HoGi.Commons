using HoGi.ToolsAndExtensions.Models.GEO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using HoGi.Shared.Exceptions;


namespace HoGi.ToolsAndExtensions.Tools
{
    public static class GeoFactory
    {
        private static readonly IList<Country> Countries;

        static GeoFactory()
        {
            //this is a new line

            var assembly = typeof(GeoFactory).GetTypeInfo().Assembly;
            using (var resource = assembly.GetManifestResourceStream($"GreenPhoenix.ToolsAndExtensions.Resources.geo.json"))
            using (var jsonStreamReader = new StreamReader(resource ?? throw new InvalidOperationException()))
            {
                Countries = JsonConvert.DeserializeObject<IList<Country>>(jsonStreamReader.ReadToEnd());
            }

            FillUniquePlaceId();

            void FillUniquePlaceId()
            {
                //walk-through of geo place
                foreach (var country in Countries)
                {
                    country.PlaceId = $"{country.Code}";
                    if (country.Provinces == null) continue;
                    foreach (var province in country.Provinces)
                    {
                        province.PlaceId = $"{country.PlaceId}-{province.Code}";
                        province.Country = country;
                        if (province.Code == 19)
                        {

                        }
                        if (province.Cities == null) continue;
                        foreach (var city in province.Cities)
                        {
                            city.PlaceId = $"{province.PlaceId}-{city.Code}";
                            city.Province = province;

                            if (city.Counties == null) continue;
                            foreach (var region in city.Counties)
                            {
                                region.PlaceId = $"{city.PlaceId}-{region.Code}";
                                region.City = city;

                                if (region.Villages == null) continue;
                                foreach (var village in region.Villages)
                                {
                                    village.PlaceId = $"{region.PlaceId}-{village.Code}";
                                    village.Region = region;
                                } //end village
                            } //end region foreach
                        } //end city foreach
                    } //end province foreach
                } //end country foreach
            }//end walk-through
        }


        public static Place GetPlace(string placeId)
        {
            if (string.IsNullOrEmpty(placeId)) return null;

            var places = placeId.Split('-').Select(int.Parse).ToList();
            switch (places.Count)
            {
                case 1:
                    {
                        return GetCountry(code: places[0]);

                    }
                case 2:
                    {
                        return GetProvince(countryCode: places[0], provinceCode: places[1]);

                    }
                case 3:
                    {
                        return GetCity(countryCode: places[0],
                                              provinceCode: places[1],
                                              cityCode: places[2]);

                    }
                case 4:
                    {
                        return GetRegion(countryCode: places[0],
                                              provinceCode: places[1],
                                              cityCode: places[2],
                                              regionCode: places[3]);

                    }
                case 5:
                    {
                        return GetVillage(countryCode: places[0],
                                                provinceCode: places[1],
                                                cityCode: places[2],
                                                regionCode: places[3],
                                                villageCode: places[4]);

                    }

                default:
                    return null;
            }
        }

        public static Place GetPlaceBySejamCodeChain(string sejamCodeChain)
        {
            if (string.IsNullOrEmpty(sejamCodeChain)) return null;

            var places = sejamCodeChain.Split('-').ToList();
            switch (places.Count)
            {
                case 1:
                    {
                        return GetCountryBySejamCode(countrySejamCode: places[0]);

                    }
                case 2:
                    {
                        return GetProvinceBySejamCode(countrySejamCode: places[0], provinceSejamCode: places[1]);

                    }
                case 3:
                    {
                        return GetCityBySejamCode(countrySejamCode: places[0],
                                              provinceSejamCode: places[1],
                                              citySejamCode: places[2]);

                    }
                case 4:
                    {
                        return GetRegionBySejamCode(countrySejamCode: places[0],
                                              provinceSejamCode: places[1],
                                              citySejamCode: places[2],
                                              regionSejamCode: places[3]);

                    }
                case 5:
                    {
                        return GetVillageBySejamCode(countrySejamCode: places[0],
                                                provinceSejamCode: places[1],
                                                citySejamCode: places[2],
                                                regionSejamCode: places[3],
                                                villageSejamCode: places[4]);

                    }

                default:
                    return null;
            }
        }

        public static IEnumerable<Place> GetChildren(string masterPlaceId)
        {
            if (string.IsNullOrEmpty(masterPlaceId))
                return GetCountries().Select(country => new Country
                {
                    CallingCode = country.CallingCode,
                    Code = country.Code,
                    Coordinate = country.Coordinate,
                    Currency = country.Currency,
                    FlagUri = country.FlagUri,
                    Language = country.Language,
                    Name = country.Name,
                    NativeName = country.NativeName,
                    PlaceId = country.PlaceId,
                    SejamCode = country.SejamCode,
                    ShortName = country.ShortName,
                    TopLevelDomain = country.TopLevelDomain
                }).ToList();

            var places = masterPlaceId.Split('-').Select(int.Parse).ToList();
            switch (places.Count)
            {
                case 1:
                    return GetProvinces(countryCode: places[0])
                        ?.Select(province => new Province
                        {
                            CallingCode = province.CallingCode,
                            SejamCode = province.SejamCode,
                            Coordinate = province.Coordinate,
                            Code = province.Code,
                            Name = province.Name,
                            PlaceId = province.PlaceId
                        }).ToList();
                case 2:
                    return GetCities(countryCode: places[0],
                        provinceCode: places[1])
                        ?.Select(city => new City
                        {
                            Code = city.Code,
                            Coordinate = city.Coordinate,
                            PlaceId = city.PlaceId,
                            Name = city.Name,
                            SejamCode = city.SejamCode,
                            ShortName = city.ShortName
                        }).ToList();

                case 3:
                    return GetRegions(countryCode: places[0],
                        provinceCode: places[1],
                        cityCode: places[2])?.
                                Select(region => new Region
                                {

                                    Code = region.Code,
                                    PlaceId = region.PlaceId,
                                    Name = region.Name,
                                    SejamCode = region.SejamCode,
                                    DistrictName = region.DistrictName,
                                    DistrictSejamCode = region.DistrictSejamCode
                                })
                                .ToList();
                case 4:
                    return GetVillages(countryCode: places[0],
                        provinceCode: places[1],
                        cityCode: places[2],
                        regionCode: places[3])?.Select(region => new Region
                        {
                            Code = region.Code,
                            PlaceId = region.PlaceId,
                            Name = region.Name,
                            SejamCode = region.SejamCode,
                        }).ToList();
                default:
                    return new List<Place>();
            }
        }


        public static IList<Country> GetCountries(string filter = "")
        {
            return Countries
                   .Where(c => string.IsNullOrEmpty(filter) || c.Name.Contains(filter))
                   .ToList();
        }

        public static Country GetCountry(int code)
        {
            var country = Countries.FirstOrDefault(c => c.Code == code);
            if (country == null)
            {
                throw new GeneralException($"کد کشور { code} مورد قبول نیست");
            }
            return country;
        }

        public static Country GetCountryBySejamCode(string countrySejamCode)
        {
            var country = Countries.FirstOrDefault(c => c.SejamCode == countrySejamCode);
            if (country == null)
            {
                throw new GeneralException($"کد سجام کشور { countrySejamCode} مورد قبول نیست");
            }
            return country;
        }

        public static IList<Province> GetProvinces(int countryCode, string filter = "")
        {
            return GetCountry(countryCode).Provinces
                .Where(c => string.IsNullOrEmpty(filter) || c.Name.Contains(filter))
                .ToList();
        }

        public static Province GetProvince(int countryCode, int provinceCode)
        {
            var province = GetCountry(countryCode)
                        .Provinces
                        .FirstOrDefault(c => c.Code == provinceCode);
            if (province == null)
            {
                throw new GeneralException($"کد استان { provinceCode }-{ countryCode} مورد قبول نیست");
            }
            return province;
        }

        public static Province GetProvinceBySejamCode(string countrySejamCode, string provinceSejamCode)
        {
            var province = GetCountryBySejamCode(countrySejamCode)
                        .Provinces
                        .FirstOrDefault(c => c.SejamCode == provinceSejamCode);
            if (province == null)
            {
                throw new GeneralException($"کد سجام استان { provinceSejamCode }-{ countrySejamCode} مورد قبول نیست");
            }
            return province;
        }

        public static Province GetProvinceByPlaceId(string placeId)
        {
            var place = GetPlace(placeId);
            switch (place)
            {
                case Village village:
                    return village.Region.City.Province;
                case Region region:
                    return region.City.Province;
                case City city:
                    return city.Province;
                case Province province:
                    return province;
                default:
                    return null;
            }
        }


        public static IList<Region> GetAllRegions(string provinceId)
        {
            var provinces = Countries.SelectMany(s => s.Provinces == null ? new List<Province>() : s.Provinces.Where(w => w.SejamCode == provinceId)).ToList();
            var cities = provinces.SelectMany(s => s.Cities == null ? new List<City>() : s.Cities).ToList();
            return cities.SelectMany(s => s.Counties == null ? new List<Region>() : s.Counties).ToList();

        }

        public static IList<City> GetCities(int countryCode, int provinceCode, string filter = "")
        {
            return GetProvince(countryCode, provinceCode)
                        .Cities
                        .Where(c => string.IsNullOrEmpty(filter) || c.Name.Contains(filter))
                        .ToList();
        }

        public static City GetCity(int countryCode, int provinceCode, int cityCode)
        {
            var city = GetProvince(countryCode, provinceCode)
                    .Cities
                    .FirstOrDefault(c => c.Code == cityCode);
            if (city == null)
            {
                throw new GeneralException($"کد شهر { cityCode }-{ provinceCode}-{countryCode} مورد قبول نیست");
            }
            return city;
        }

        public static City GetCityBySejamCode(string countrySejamCode, string provinceSejamCode, string citySejamCode)
        {
            var city = GetProvinceBySejamCode(countrySejamCode, provinceSejamCode)
                    .Cities
                    .FirstOrDefault(c => c.SejamCode == citySejamCode);
            if (city == null)
            {
                throw new GeneralException($"کد سجام شهر { citySejamCode }-{ provinceSejamCode}-{countrySejamCode} مورد قبول نیست");
            }
            return city;
        }

        public static City GetCityByPlaceId(string placeId)
        {
            var place = GetPlace(placeId);
            switch (place)
            {
                case Village village:
                    return village.Region.City;
                case Region region:
                    return region.City;
                case City city:
                    return city;
                default:
                    return null;
            }
        }

        public static IList<Region> GetRegions(int countryCode, int provinceCode, int cityCode, string filter = "")
        {
            return GetCity(countryCode, provinceCode, cityCode)
                      .Counties
                      .Where(c => string.IsNullOrEmpty(filter) || c.Name.Contains(filter))
                      .ToList();
        }

        public static Region GetRegion(int countryCode, int provinceCode, int cityCode, int regionCode)
        {
            var region = GetCity(countryCode, provinceCode, cityCode)
                    .Counties
                     .FirstOrDefault(c => c.Code == regionCode);
            if (region == null)
            {
                throw new GeneralException($"کد شهر {regionCode}-{ cityCode }-{ provinceCode}-{countryCode} مورد قبول نیست");
            }
            return region;
        }

        public static Region GetRegionBySejamCode(string countrySejamCode, string provinceSejamCode, string citySejamCode, string regionSejamCode)
        {
            var region = GetCityBySejamCode(countrySejamCode, provinceSejamCode, citySejamCode)
                    .Counties
                     .FirstOrDefault(c => c.SejamCode == regionSejamCode);
            if (region == null)
            {
                throw new GeneralException($"کد سجام شهر {regionSejamCode}-{ citySejamCode }-{ provinceSejamCode}-{countrySejamCode} مورد قبول نیست");
            }
            return region;
        }

        public static Region GetRegionByPlaceId(string placeId)
        {
            var place = GetPlace(placeId);
            switch (place)
            {
                case Village village:
                    return village.Region;
                case Region region:
                    return region;
                default:
                    return null;
            }
        }


        public static IList<Village> GetVillages(int countryCode, int provinceCode, int cityCode, int regionCode, string filter = "")
        {
            return GetRegion(countryCode, provinceCode, cityCode, regionCode)
                .Villages
                .Where(c => string.IsNullOrEmpty(filter) || c.Name.Contains(filter))
                .ToList();
        }

        public static Village GetVillage(int countryCode, int provinceCode, int cityCode, int regionCode, int villageCode)
        {
            var vilage = GetRegion(countryCode, provinceCode, cityCode, regionCode)
                        .Villages
                        .FirstOrDefault(c => c.Code == villageCode);
            if (vilage == null)
            {
                throw new GeneralException($"کد شهر {villageCode}-{regionCode}-{ cityCode }-{ provinceCode}-{countryCode} مورد قبول نیست");

            }
            return vilage;
        }

        public static Village GetVillageBySejamCode(string countrySejamCode, string provinceSejamCode, string citySejamCode, string regionSejamCode, string villageSejamCode)
        {
            var vilage = GetRegionBySejamCode(countrySejamCode, provinceSejamCode, citySejamCode, regionSejamCode)
                        .Villages
                        .FirstOrDefault(c => c.SejamCode == villageSejamCode);
            if (vilage == null)
            {
                throw new GeneralException($"کد سجام شهر {villageSejamCode}-{regionSejamCode}-{ citySejamCode }-{ provinceSejamCode}-{countrySejamCode} مورد قبول نیست");

            }
            return vilage;
        }

        public static Village GetVillageByPlaceId(string placeId)
        {
            var place = GetPlace(placeId);
            switch (place)
            {
                case Village village:
                    return village;
                default:
                    return null;
            }
        }
    }//end class
}//end namespace
