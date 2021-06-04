using System.Threading.Tasks;
using TaxService.Interfaces;
using TaxService.Models;

namespace TaxService.Services
{
    public class TaxService : ITaxService
    {
        private readonly ITaxCalculator _taxCalculator;

        public TaxService(ITaxCalculator taxCalculator)
        {
            _taxCalculator = taxCalculator;
        }

        public Task<TaxRate> GetTaxRateByLocation(Location location)
        {
            return _taxCalculator.GetTaxRateByLocation(location);
        }

        public Task<Tax> CalculateSalesTaxByOrder(Order order)
        {
            return _taxCalculator.CalculateSalesTaxByOrder(order);
        }
    }
}