namespace WestWindSystem.DataModels.OrderProcessing
{
    public class ProductSummary
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public short Quantity { get; set; }
        public string QtyPerUnit { get; set; }
        public int OutstandingQty { get; set; }
    }
}
