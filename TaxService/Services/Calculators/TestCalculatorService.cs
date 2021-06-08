using System.Threading.Tasks;
using TaxService.Interfaces;
using TaxService.Models;

namespace TaxService.Services
{
    public class TestCalculatorService: ITaxCalculatorService
    {
        public Task<TaxRate> GetTaxRateByLocation(Location location)
        {
            throw new System.NotImplementedException();
        }

        public Task<SalesTax> CalculateSalesTaxByOrder(Order order)
        {
            throw new System.NotImplementedException();
        }
    }
}