using System.Collections.Generic;

namespace WestWindSystem.DataModels
{
    public class CategorizedProducts // DTO (Data Transfer Object)
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Picture { get; set; }
        public IEnumerable<ProductInfo> Products { get; set; }
    }
}
