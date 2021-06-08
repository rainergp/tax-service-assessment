using System.Threading.Tasks;
using TaxService.Models;

namespace TaxService.Interfaces
{
    public interface ITaxCalculatorService
    {
        Task<TaxRate> GetTaxRateByLocation(Location location);
        Task<SalesTax> CalculateSalesTaxByOrder(Order order);
    }
}