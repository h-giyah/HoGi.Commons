
# HoGi.Commons.ToolsAndExtensions
A library in .NET Tools for internal use in HoGi packages.
it's include some extensions, attributes and helpers.

## Usage

> [!TIP]
> for persian bank using in FinTech Application Base on Sejam and Supporting RayanHamafza,Sahra and ... Id Codes
### Example Get Banks
>```BankProvider.Banks```
```sh
    [HttpGet]
        [ProducesResponseType(typeof(IList<Bank>), StatusCodes.Status200OK)]
        public IActionResult GetBanksForAddBankAccount()
        {
            return Ok(BankProvider.Banks);
        }
```
### Example Get Place (Geo info like city,state,country codes )
```sh
 GeoFactory.GetCountries(string filter = "")
 GeoFactory.GetProvinces(int countryCode, string filter = "")
 GeoFactory.GetCities(int countryCode, int provinceCode, string filter = "")
 GeoFactory.GetCityByPlaceId(string placeId)
 GeoFactory.GetCountryBySejamCode(string countrySejamCode)
 GeoFactory.GetProvinceBySejamCode(string countrySejamCode, string provinceSejamCode)
 GeoFactory.(string countrySejamCode, string provinceSejamCode, string citySejamCode)
 .
 .
 .
```

