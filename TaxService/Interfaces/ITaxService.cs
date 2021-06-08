using System.Threading.Tasks;
using TaxService.Models;

namespace TaxService.Interfaces
{
    public interface ITaxService
    {
        Task<TaxRate> GetTaxRateByLocation(Location location);
        Task<SalesTax> CalculateSalesTaxByOrder(Order order);
    }
}