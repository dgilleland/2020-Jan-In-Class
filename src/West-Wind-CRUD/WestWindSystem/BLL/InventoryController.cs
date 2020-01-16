using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WestWindSystem.DAL;
using WestWindSystem.Entities;

namespace WestWindSystem.BLL
{
    [DataObject] // Allows this class to be a "source" for databound controls
    public class InventoryController
    {
        #region Products CRUD
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Product> ListAllProducts()
        {
            using(var context = new WestWindContext())
            {
                return context.Products.ToList();
            }
        }
        #endregion
    }
}
