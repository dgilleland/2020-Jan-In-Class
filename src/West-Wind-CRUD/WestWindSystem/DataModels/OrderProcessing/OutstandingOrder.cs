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
        public int DaysToDelivery
        { get { return (RequiredDate - OrderedDate).Days; } } // Calculated
        public List<ProductSummary> OutstandingItems { get; set; }
        public string Comments { get; set; }
        public string FullShippingAddress { get; set; }
    }
}
