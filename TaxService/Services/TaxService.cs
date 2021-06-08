using System.Threading.Tasks;
using TaxService.Interfaces;
using TaxService.Models;

namespace TaxService.Services
{
    public class TaxService: ITaxService
    {
        private readonly  ITaxCalculatorService _taxCalculatorService;
        
        public TaxService(ITaxCalculatorService taxCalculatorService)
        {
            _taxCalculatorService = taxCalculatorService;
        }
        
        public Task<TaxRate> GetTaxRateByLocation(Location location)
        {
            return _taxCalculatorService.GetTaxRateByLocation(location);
        }
        
        public Task<SalesTax> CalculateSalesTaxByOrder(Order order)
        {
            return _taxCalculatorService.CalculateSalesTaxByOrder(order);
        }
    }
}