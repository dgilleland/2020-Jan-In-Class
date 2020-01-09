using System.Linq;

namespace BackEnd.DataModels
{
    public class Vehicle
    {
        public string VIN { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public string Fuel { get; set; }
    }
}
