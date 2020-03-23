using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WestWindSystem.BLL;
using WestWindSystem.DataModels.OrderProcessing;

namespace WebApp.Sandbox
{
    public partial class OrderShipping : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ShipmentsListView_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if(e.CommandName == "Ship")
            {
                #region Extract data from the form
                // Gather information from the form to send to the BLL for shipping
                // - ShipOrder(int orderId, ShippingDirections directions, List<ProductShipment> items)
                int orderId = 0;
                // safe-cast the control object to a Label
                Label orderIdLabel = e.Item.FindControl("OrderIdLabel") as Label;
                if (orderIdLabel != null)
                    orderId = int.Parse(orderIdLabel.Text);

                ShippingDirections shipInfo = new ShippingDirections(); // blank obj
                DropDownList shipViaDropDown = e.Item.FindControl("ShipperDropDown") as DropDownList;
                if (shipViaDropDown != null)
                    shipInfo.ShipperId = int.Parse(shipViaDropDown.SelectedValue);

                TextBox tracking = e.Item.FindControl("TrackingCode") as TextBox;
                if (tracking != null)
                    shipInfo.TrackingCode = tracking.Text;

                decimal price;
                TextBox freight = e.Item.FindControl("FreightCharge") as TextBox;
                if (freight != null && decimal.TryParse(freight.Text, out price))
                    shipInfo.FreightCharge = price;

                List<ProductShipment> goods = new List<ProductShipment>();
                GridView gv = e.Item.FindControl("ProductsGridView") as GridView;
                if(gv != null)
                {
                    // Extract the data from each row in the GridView
                    foreach(GridViewRow row in gv.Rows)
                    {
                        // get product id and ship qty
                        HiddenField prodId = row.FindControl("ProductId") as HiddenField;
                        TextBox qty = row.FindControl("ShipQuantity") as TextBox;
                        short quantity;
                        if(prodId != null && qty != null && short.TryParse(qty.Text, out quantity))
                        {
                            ProductShipment item = new ProductShipment
                            {
                                ProductId = int.Parse(prodId.Value),
                                Quantity = quantity
                            };
                            goods.Add(item);
                        }
                    }
                }
                #endregion

                #region Try to process the order by passing it to the BLL
                // Use my MessageUserControl to try sending the data to the BLL for processing
                MessageUserControl.TryRun(() => // This TryRun() method will catch any exceptions
                {
                    // An anonymous method
                    var controller = new OrderProcessingController();
                    controller.ShipOrder(orderId, shipInfo, goods);
                }, "Order shipment recorded", "The products identified as shipped are recorded in the database");
                #endregion
            }
        }
    }
}