using System;
using System.Threading.Tasks;
using TaxService.Factories;
using TaxService.Models;
using TaxService.Models.Enum;

namespace TaxService.Client
{
    internal static class Program
    {
        private static async Task Main()
        {
            var taxCalculatorServiceInstance = TaxCalculatorFactory.Create(CalculatorType.TaxJar);
            var taxCalculatorService = new Services.TaxService(taxCalculatorServiceInstance);

            var location = new Location
            {
                Street = "312 Hurricane Lane",
                City = "Williston",
                State = "VT",
                Zip = "05495-2086",
                Country = "US"
            };
            var taxRate = await taxCalculatorService.GetTaxRateByLocation(location);
            Console.WriteLine(taxRate.Rate.ToString());

            var order = new Order
            {
                FromCountry = "US",
                FromZip = "92093",
                FromState = "CA",
                FromCity = "La Jolla",
                FromStreet = "9500 Gilman Drive",

                ToCountry = "US",
                ToZip = "90002",
                ToState = "CA",
                ToCity = "Los Angeles",
                ToStreet = "1335 E 103rd St",

                Amount = 15,
                Shipping = (float) 1.5
            };
            var salesTax = await taxCalculatorService.CalculateSalesTaxByOrder(order);
            Console.WriteLine(salesTax.Tax.ToString());
        }
    }
}