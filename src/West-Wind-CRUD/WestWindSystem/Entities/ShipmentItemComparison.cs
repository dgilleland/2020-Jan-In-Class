using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WestWindSystem.Entities
{
    internal class ShipmentItemComparison
    {
        internal int ProductID;

        public int ShipQuantity { get; internal set; }
        public int ExpectedQuantity { get; internal set; }
    }
}
