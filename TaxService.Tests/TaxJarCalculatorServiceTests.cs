using System;
using System.Threading.Tasks;
using TaxService.Config;
using TaxService.Models;
using TaxService.Services.Calculators;
using Xunit;

namespace TaxService.Tests
{
    //TODO: This tests must be improved (this is just a sample)
    public class TaxJarCalculatorServiceTests
    {
        // GetTaxRateByLocation
        [Fact]
        public async void GetTaxRateByLocation_Null_Location_Should_Fail()
        {
            var taxJarCalculatorService = new TaxJarCalculatorService(TaxJarApiConfig.ApiKey, TaxJarApiConfig.ApiUrl);
            
            Task TaxRate() => taxJarCalculatorService.GetTaxRateByLocation(null);
            
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(TaxRate);
            
            Assert.Equal("location", exception.ParamName);
        }

        [Fact]
        public async void GetTaxRateByLocation_NullOrWhiteSpace_Zip_Should_Fail()
        {
            var taxJarCalculatorService = new TaxJarCalculatorService(TaxJarApiConfig.ApiKey, TaxJarApiConfig.ApiUrl);
            
            var location = new Location
            {
                Street = "312 Hurricane Lane",
                City = "Williston",
                State = "VT",
                Zip = null,
                Country = "US"
            };
            
            Task TaxRate() => taxJarCalculatorService.GetTaxRateByLocation(location);
            
            var exception = await Assert.ThrowsAsync<ArgumentException>(TaxRate);
            
            Assert.Equal("Zip parameter is required.", exception.Message);
        }

        // CalculateSalesTaxByOrder
        [Fact]
        public async void CalculateSalesTaxByOrder_Null_Order_Should_Fail()
        {
            var taxJarCalculatorService = new TaxJarCalculatorService(TaxJarApiConfig.ApiKey, TaxJarApiConfig.ApiUrl);
            
            Task SalesTax() => taxJarCalculatorService.CalculateSalesTaxByOrder(null);
            
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(SalesTax);
            
            Assert.Equal("order", exception.ParamName);
        }
        
        [Fact]
        public async void CalculateSalesTaxByOrder_NullOrWhiteSpace_ToCountry_Should_Fail()
        {
            var taxJarCalculatorService = new TaxJarCalculatorService(TaxJarApiConfig.ApiKey, TaxJarApiConfig.ApiUrl);

            var order = new Order
            {
                FromCountry = "US",
                FromZip = "92093",
                FromState = "CA",
                FromCity = "La Jolla",
                FromStreet = "9500 Gilman Drive",

                ToCountry = null,
                ToZip = "90002",
                ToState = "CA",
                ToCity = "Los Angeles",
                ToStreet = "1335 E 103rd St",

                Amount = 15,
                Shipping = (float) 1.5
            };
            
            Task SalesTax() => taxJarCalculatorService.CalculateSalesTaxByOrder(order);
            
            var exception = await Assert.ThrowsAsync<ArgumentException>(SalesTax);
            
            Assert.Equal("ToCountry parameter is required.", exception.Message);
        }
        
        [Fact]
        public async void CalculateSalesTaxByOrder_LessOrEqualThanZero_Shipping_Should_Fail()
        {
            var taxJarCalculatorService = new TaxJarCalculatorService(TaxJarApiConfig.ApiKey, TaxJarApiConfig.ApiUrl);

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
                Shipping = 0
            };
            
            Task SalesTax() => taxJarCalculatorService.CalculateSalesTaxByOrder(order);
            
            var exception = await Assert.ThrowsAsync<ArgumentException>(SalesTax);
            
            Assert.Equal("Shipping parameter is required.", exception.Message);

        }

        [Fact]
        public async void CalculateSalesTaxByOrder_NaN_Shipping_Should_Fail()
        {
            var taxJarCalculatorService = new TaxJarCalculatorService(TaxJarApiConfig.ApiKey, TaxJarApiConfig.ApiUrl);

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
                Shipping = float.NaN
            };
            
            Task SalesTax() => taxJarCalculatorService.CalculateSalesTaxByOrder(order);
            
            var exception = await Assert.ThrowsAsync<ArgumentException>(SalesTax);
            
            Assert.Equal("Shipping parameter is not valid.", exception.Message);

        }
    }
}