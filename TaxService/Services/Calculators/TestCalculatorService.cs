using System;
using System.Threading.Tasks;
using TaxService.Interfaces;
using TaxService.Models;

namespace TaxService.Services.Calculators
{
    public class TestCalculatorService: ITaxCalculatorService
    {
        public async Task<TaxRate> GetTaxRateByLocation(Location location)
        {
            return await Task.FromResult(new TaxRate
            {
                Rate = new Rate
                {
                    Zip = location.Zip,
                    Country = location.Country,
                    CountryRate = 0,
                    State = location.State,
                    StateRate = 0,
                    County = string.Empty,
                    CountyRate = 0,
                    City = location.City,
                    CityRate = 0,
                    CombinedDistrictRate = 0,
                    CombinedRate = 0,
                    FreightTaxable = false,
                }
            });
        }

        public async Task<SalesTax> CalculateSalesTaxByOrder(Order order)
        {
            return await Task.FromResult(new SalesTax
            {
                Tax = new Tax
                {
                    OrderTotalAmount = order.Amount,
                    Shipping = order.Shipping,
                    TaxableAmount = 0,
                    AmountToCollect = 0,
                    Rate = 0,
                    HasNexus = false,
                    FreightTaxable = false,
                    TaxSource = string.Empty,
                    Jurisdictions = null,
                    Breakdown = null,
                }
            });
        }
    }
}