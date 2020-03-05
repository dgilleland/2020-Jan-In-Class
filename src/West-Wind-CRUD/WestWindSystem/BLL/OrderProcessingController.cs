using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WestWindSystem.DataModels.OrderProcessing;

namespace WestWindSystem.BLL
{
    [DataObject]
    public class OrderProcessingController
    {
        #region Query Methods
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<OutstandingOrder> LoadOrders(int supplierID)
        {
            // TODO: Implement LoadOrders
            throw new NotImplementedException();
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<ShipperSelection> ListShippers()
        {
            // TODO: Implement ListShippers
            throw new NotImplementedException();
        }

        public SupplierSummary GetSupplier(int supplierID)
        {
            // TODO: Implement GetSupplier
            throw new NotImplementedException();
        }
        #endregion

        #region Command Methods
        public void ShipOrder(int orderId, ShippingDirections directions, List<ProductShipment> items)
        {
            // TODO: ShipOrder - Validation of input:
            //  - OrderId must exist
            //  - Shipper must exist
            //  - Must have one or more items to ship
            //  - ProductIds must exist/valid
            //  - Quantities must be greater than 0 and less than the number/qty outstanding on the order
            //  - Freight charge is either null or a value greater than zero
            // TODO: ShipOrder - Add a new Shipment to the database
            // TODO: ShipOrder - Add new ManifestItem objects to the new shipment
        }
        #endregion
    }
}
