using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WestWindSystem.DataModels
{
    public class ProductSummary
    {
        public string ProductName { get; set; }
        public string Supplier { get; set; }
        public string Category { get; set; }
        public decimal SellingPrice { get; set; }
        public string QuantityPerUnit { get; set; }
    }
}
