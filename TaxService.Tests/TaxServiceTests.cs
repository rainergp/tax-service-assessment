using TaxService.Factories;
using TaxService.Models;
using TaxService.Models.Enum;
using Xunit;

namespace TaxService.Tests
{
    //TODO: This tests must be improved (this is just a sample)
    public class TaxServiceTests
    {
        private readonly Location _location = new Location
        {
            Street = "312 Hurricane Lane",
            City = "Williston",
            State = "VT",
            Zip = "05495-2086",
            Country = "US"
        };
        
        private readonly Order _order = new Order
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

        [Fact]
        public async void TaxService_Test_GetTaxRateByLocation_Should_Work()
        {
            var taxCalculatorServiceInstance = TaxCalculatorFactory.Create(CalculatorType.Test);
            var taxCalculatorService = new Services.TaxService(taxCalculatorServiceInstance);

            var taxRate = await taxCalculatorService.GetTaxRateByLocation(_location);

            Assert.Equal(_location.Zip, taxRate.Rate.Zip);
            Assert.Equal(_location.Country, taxRate.Rate.Country);
            Assert.Equal(0, taxRate.Rate.CountryRate);
            Assert.Equal(_location.State, taxRate.Rate.State);
            Assert.Equal(0, taxRate.Rate.StateRate);
            Assert.Equal(string.Empty, taxRate.Rate.County);
            Assert.Equal(0, taxRate.Rate.CountyRate);
            Assert.Equal(_location.City, taxRate.Rate.City);
            Assert.Equal(0, taxRate.Rate.CityRate);
            Assert.Equal(0, taxRate.Rate.CombinedDistrictRate);
            Assert.Equal(0, taxRate.Rate.CombinedRate);
            Assert.False(taxRate.Rate.FreightTaxable);
        }
        
        [Fact]
        public async void TaxService_Test_CalculateSalesTaxByOrder_Should_Work()
        {
            var taxCalculatorServiceInstance = TaxCalculatorFactory.Create(CalculatorType.Test);
            var taxCalculatorService = new Services.TaxService(taxCalculatorServiceInstance);

            var salesTax = await taxCalculatorService.CalculateSalesTaxByOrder(_order);

            Assert.Equal(_order.Amount, salesTax.Tax.OrderTotalAmount);
            Assert.Equal(_order.Shipping, salesTax.Tax.Shipping);
            Assert.Equal(0, salesTax.Tax.TaxableAmount);
            Assert.Equal(0, salesTax.Tax.AmountToCollect);
            Assert.Equal(0, salesTax.Tax.Rate);
            Assert.False(salesTax.Tax.HasNexus);
            Assert.False(salesTax.Tax.FreightTaxable);
            Assert.Equal(string.Empty, salesTax.Tax.TaxSource);
            Assert.Null(salesTax.Tax.Jurisdictions);
            Assert.Null(salesTax.Tax.Breakdown);
        }
        
        [Fact]
        public async void TaxService_TaxJar_GetTaxRateByLocation_Should_Work()
        {
            var taxCalculatorServiceInstance = TaxCalculatorFactory.Create(CalculatorType.TaxJar);
            var taxCalculatorService = new Services.TaxService(taxCalculatorServiceInstance);
        
            var taxRate = await taxCalculatorService.GetTaxRateByLocation(_location);

            var expectedRate = new Rate
            {
                Zip = "05495-2086",
                Country = "US",
                CountryRate = 0,
                State = "VT",
                StateRate = (float) 0.06,
                County = "CHITTENDEN",
                CountyRate = 0,
                City = "WILLISTON",
                CityRate = 0,
                CombinedDistrictRate = 0,
                CombinedRate = (float) 0.06,
                FreightTaxable = true
            };
        
            Assert.Equal(expectedRate.Zip, taxRate.Rate.Zip);
            Assert.Equal(expectedRate.Country, taxRate.Rate.Country);
            Assert.Equal(expectedRate.CountryRate, taxRate.Rate.CountryRate);
            Assert.Equal(expectedRate.State, taxRate.Rate.State);
            Assert.Equal(expectedRate.StateRate, taxRate.Rate.StateRate);
            Assert.Equal(expectedRate.County, taxRate.Rate.County);
            Assert.Equal(expectedRate.CountyRate, taxRate.Rate.CountyRate);
            Assert.Equal(expectedRate.City, taxRate.Rate.City);
            Assert.Equal(expectedRate.CityRate, taxRate.Rate.CityRate);
            Assert.Equal(expectedRate.CombinedDistrictRate, taxRate.Rate.CombinedDistrictRate);
            Assert.Equal(expectedRate.CombinedRate, taxRate.Rate.CombinedRate);
            Assert.Equal(expectedRate.FreightTaxable, taxRate.Rate.FreightTaxable);
        }
        
        [Fact]
        public async void TaxService_TaxJar_CalculateSalesTaxByOrder_Should_Work()
        {
            var taxCalculatorServiceInstance = TaxCalculatorFactory.Create(CalculatorType.TaxJar);
            var taxCalculatorService = new Services.TaxService(taxCalculatorServiceInstance);

            var salesTax = await taxCalculatorService.CalculateSalesTaxByOrder(_order);

            var expectedTax = new Tax
            {
                OrderTotalAmount = (float) 16.5,
                Shipping = (float) 1.5,
                TaxableAmount = 15,
                AmountToCollect = (float) 1.43,
                Rate = (float) 0.095,
                HasNexus = true,
                FreightTaxable = false,
                TaxSource = "destination",
                Jurisdictions = new Jurisdictions
                {
                    Country = "US",
                    State = "CA",
                    County = "LOS ANGELES COUNTY",
                    City = "LOS ANGELES",
                },
                Breakdown = null,
            };

            Assert.Equal(expectedTax.OrderTotalAmount, salesTax.Tax.OrderTotalAmount);
            Assert.Equal(expectedTax.Shipping, salesTax.Tax.Shipping);
            Assert.Equal(expectedTax.TaxableAmount, salesTax.Tax.TaxableAmount);
            Assert.Equal(expectedTax.AmountToCollect, salesTax.Tax.AmountToCollect);
            Assert.Equal(expectedTax.Rate, salesTax.Tax.Rate);
            Assert.Equal(expectedTax.HasNexus, salesTax.Tax.HasNexus);
            Assert.Equal(expectedTax.FreightTaxable, salesTax.Tax.FreightTaxable);
            Assert.Equal(expectedTax.TaxSource, salesTax.Tax.TaxSource);
            Assert.Equal(expectedTax.Jurisdictions.Country, salesTax.Tax.Jurisdictions.Country);
            Assert.Equal(expectedTax.Jurisdictions.State, salesTax.Tax.Jurisdictions.State);
            Assert.Equal(expectedTax.Jurisdictions.County, salesTax.Tax.Jurisdictions.County);
            Assert.Equal(expectedTax.Jurisdictions.City, salesTax.Tax.Jurisdictions.City);
            Assert.Equal(expectedTax.Breakdown, salesTax.Tax.Breakdown);
        }
    }
}