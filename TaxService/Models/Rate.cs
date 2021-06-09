namespace TaxService.Models
{
    public class Rate
    {
        public override string ToString()
        {
            return $"RATE: ( Zip: {Zip} | " +
                   $"Country: {Country} | " +
                   $"Country Rate: {CountryRate} | " +
                   $"State: {State} | " +
                   $"State Rate: {StateRate} | " +
                   $"County: {County} | " +
                   $"County Rate: {CountyRate} | " +
                   $"City: {City} | " +
                   $"City Rate: {CityRate} | " +
                   $"Combined DistrictRate: {CombinedDistrictRate} | " +
                   $"Combined Rate: {CombinedRate} | " +
                   $"Freight Taxable: {FreightTaxable} )";
        }

        public string Zip { get; set; }
        public string Country { get; set; }
        public float CountryRate { get; set; }
        public string State { get; set; }
        public float StateRate { get; set; }
        public string County { get; set; }
        public float CountyRate { get; set; }
        public string City { get; set; }
        public float CityRate { get; set; }
        public float CombinedDistrictRate { get; set; }
        public float CombinedRate { get; set; }
        public bool FreightTaxable { get; set; }
    }
}