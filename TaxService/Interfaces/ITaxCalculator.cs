using System.Threading.Tasks;
using TaxService.Models;

namespace TaxService.Interfaces
{
    public interface ITaxCalculator
    {
        Task<TaxRate> GetTaxRateByLocation(Location location);
        Task<Tax> CalculateSalesTaxByOrder(Order order);
    }
}