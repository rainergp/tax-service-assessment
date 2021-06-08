using System.Threading.Tasks;
using TaxService.Factories;
using TaxService.Models;
using TaxService.Types;

namespace TaxService.ConsoleApp
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            var taxJarCalculatorService = TaxCalculatorFactory.CreateTaxCalculatorService(CalculatorType.TaxJar);
            var taxJarCalculator = new Services.TaxService(taxJarCalculatorService);

            var location = new Location
            {
                Street = "3428 Capri Rd",
                City = "Palm Beach Gardens",
                State = "FL",
                Zip = "3410",
                Country = "US"
            };
            // var taxRate = await taxJarCalculator.GetTaxRateByLocation(location);
            // Console.WriteLine(taxRate.Rate);

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
            // var salesTax = await taxJarCalculator.CalculateSalesTaxByOrder(order);
            // Console.WriteLine(salesTax.Tax);
        }
    }
}