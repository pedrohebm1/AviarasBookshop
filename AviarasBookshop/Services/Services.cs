using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace AviarasBookshop.Services
{
    public class CountryService
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task<List<Country>> GetCountriesAsync()
        {
            var response = await client.GetStringAsync("https://restcountries.com/v3.1/all");
            var countriesJson = JArray.Parse(response);
            var countries = new List<Country>();

            foreach (var countryJson in countriesJson)
            {
                var country = new Country
                {
                    CommonName = countryJson["name"]?["common"]?.ToString(),
                    Nationality = countryJson["demonyms"]?["eng"]?["m"]?.ToString()
                };

                if (!string.IsNullOrEmpty(country.CommonName) && !string.IsNullOrEmpty(country.Nationality))
                {
                    countries.Add(country);
                }
            }

            return countries;
        }
    }

    public class Country
    {
        public string CommonName { get; set; } // Nome comum do país
        public string Nationality { get; set; } // Nacionalidade em inglês
    }

}
