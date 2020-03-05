using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WestWindSystem.DataModels.OrderProcessing;

namespace WestWindSystem.BLL
{
    public class OrderProcessingController
    {
        #region Query Methods
        public List<OutstandingOrder> LoadOrders(int supplierID)
        {
            // TODO: Implement LoadOrders
            throw new NotImplementedException();
        }

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
        #endregion
    }
}
