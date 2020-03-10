using System;
using System.Collections.Generic;

namespace WestWindSystem.DataModels.OrderProcessing
{
    public class OutstandingOrder
    {
        public int OrderID { get; set; }
        public string ShipToName { get; set; }
        public DateTime OrderedDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public int DaysToDelivery // TODO: Fix bug in calculation for long timespans
        { get { return (RequiredDate - OrderedDate).Days; } } // Calculated
        public IEnumerable<ProductSummary> OutstandingItems { get; set; }
        public string Comments { get; set; }
        public string FullShippingAddress { get; set; }
    }
}
