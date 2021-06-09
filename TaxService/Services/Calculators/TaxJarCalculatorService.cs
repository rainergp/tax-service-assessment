using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TaxService.Interfaces;
using TaxService.Models;

namespace TaxService.Services.Calculators
{
    public class TaxJarCalculatorService: ITaxCalculatorService
    {
        private readonly string _ratesUrl;
        private readonly string _taxesUrl;
        
        private readonly HttpClient _httpClient = new HttpClient();

        private readonly JsonSerializerSettings _jsonDeserializationSettings;

        public TaxJarCalculatorService(string apiKey, string apiUrl)
        {
            _ratesUrl = $"{apiUrl}/rates/";
            _taxesUrl = $"{apiUrl}/taxes";
            
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
            
            _jsonDeserializationSettings  = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                }
            };
        }
        public async Task<TaxRate> GetTaxRateByLocation(Location location)
        {
            ValidateLocation(location);

            var queryString = GetQueryStringFromLocation(location);
            var url = $"{_ratesUrl}{queryString}";

            try
            {
                var httpResponseMessage = await _httpClient.GetAsync(url);
                
                httpResponseMessage.EnsureSuccessStatusCode();
                
                var responseContent = await httpResponseMessage.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<TaxRate>(responseContent, _jsonDeserializationSettings);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error on GetTaxRateByLocation. {ex.Message}");
            }

        }

        public async Task<SalesTax> CalculateSalesTaxByOrder(Order order)
        {
            ValidateOrder(order);
            
            try
            {
                var stringPayload = JsonConvert.SerializeObject(order, _jsonDeserializationSettings);
                var stringContent = new StringContent(stringPayload, Encoding.Default, "application/json");

                var httpResponseMessage = await _httpClient.PostAsync(_taxesUrl, stringContent);

                httpResponseMessage.EnsureSuccessStatusCode();

                var responseContent = await httpResponseMessage.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<SalesTax>(responseContent, _jsonDeserializationSettings);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error on GetTaxRateByLocation. {ex.Message}");
            }
        }

        private static string GetQueryStringFromLocation(Location location)
        {
            var queryParams = System.Web.HttpUtility.ParseQueryString(string.Empty);

            if (!string.IsNullOrWhiteSpace(location.Country))
            {
                queryParams.Add("country", location.Country);
            }

            if (!string.IsNullOrWhiteSpace(location.State))
            {
                queryParams.Add("state", location.State);
            }

            if (!string.IsNullOrWhiteSpace(location.City))
            {
                queryParams.Add("city", location.City);
            }

            if (!string.IsNullOrWhiteSpace(location.Street))
            {
                queryParams.Add("street", location.Street);
            }

            return $"{location.Zip}?{queryParams}";
        }
        
        //TODO: ValidateLocation & ValidateOrder must be improved 
        private static void ValidateLocation(Location location)
        {
            if (location == null)
            {
                throw new ArgumentNullException(nameof(location));
            }

            if (string.IsNullOrWhiteSpace(location.Zip))
            {
                throw new ArgumentException("Zip parameter is required.");
            }
        }

        private static void ValidateOrder(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            if (string.IsNullOrWhiteSpace(order.ToCountry))
            {
                throw new ArgumentException("ToCountry parameter is required.");
            }

            if (order.Shipping <= 0)
            {
                throw new ArgumentException("Shipping parameter is required.");
            }

            if (double.IsNaN(order.Shipping))
            {
                throw new ArgumentException("Shipping parameter is not valid.");
            }
        }
    }
}