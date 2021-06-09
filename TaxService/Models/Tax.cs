namespace TaxService.Models
{
    public class Tax
    {
        public override string ToString()
        {
            return $"TAX: ( Order Total Amount: {OrderTotalAmount} | " +
                   $"Shipping: {Shipping} | " +
                   $"Taxable Amount: {TaxableAmount} | " +
                   $"Amount To Collect: {AmountToCollect} | " +
                   $"Rate: {Rate} | " +
                   $"Has Nexus: {HasNexus} | " +
                   $"Freight Taxable: {FreightTaxable} | " +
                   $"Tax Source: {TaxSource} | " +
                   "Jurisdictions: {...} | Breakdown: {...} )";
        }

        public float OrderTotalAmount { get; set; }
        public float Shipping { get; set; }
        public float TaxableAmount { get; set; }
        public float AmountToCollect { get; set; }
        public float Rate { get; set; }
        public bool HasNexus { get; set; }
        public bool FreightTaxable { get; set; }
        public string TaxSource { get; set; }
        public Jurisdictions Jurisdictions { get; set; }
        public Breakdown Breakdown { get; set; }
    }
}