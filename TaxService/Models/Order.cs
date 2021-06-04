using System.Collections.Generic;

namespace TaxService.Models
{
    public class Order
    {
        public string FromCountry { get; set; }
        public string FromZip { get; set; }
        public string FromState { get; set; }
        public string FromCity { get; set; }
        public string FromStreet { get; set; }
        public string ToCountry { get; set; }
        public string ToZip { get; set; }
        public string ToState { get; set; }
        public string ToCity { get; set; }
        public string ToStreet { get; set; }
        public float Amount { get; set; }
        public float Shipping { get; set; }

        public ICollection<NexusAddress> NexusAddresses { get; set; }
        public ICollection<LineItem> LineItems { get; set; }

        public Order()
        {
            NexusAddresses = new List<NexusAddress>();
            LineItems = new List<LineItem>();
        }
    }
}