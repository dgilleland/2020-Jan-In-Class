using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WestWindSystem.BLL;
using WestWindSystem.DataModels;

namespace WebApp.Sandbox
{
    public partial class ProductSelection : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack) // GET request for the page
            {
                var controller = new InventoryController();
                var data = controller.ListActiveProducts();
                SourceProductsGridView.DataSource = data;
                SourceProductsGridView.DataBind();
            }
        }

        protected void SourceProductsGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName == "Select")
            {
                //MessageUserControl.ShowInfo("A Select button was clicked");
            }
        }

        protected void SourceProductsGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            string message = $"The item at index {SourceProductsGridView.SelectedIndex} was selected.";
            GridViewRow row = SourceProductsGridView.Rows[SourceProductsGridView.SelectedIndex];
            // Get my Label control from the row using "safe casting"
            Label name = row.FindControl("ProductName") as Label;
            // TODO: Get the Price
            Label price = row.FindControl("Price") as Label;
            // TODO: Get the Quanitity/Unit
            Label uQty = row.FindControl("Qty") as Label;

            if (name != null && price != null && uQty != null)
                message += $" The product name is {name.Text} and sells at {price.Text} for {uQty.Text}";
            else // this should not really happen, if we've coded it correctly.....
                message += "<b>Error:</b> Problem parsing the contents of the row";
            MessageUserControl.ShowInfo(message);

            // Let's consider how we could use this information to produce a ProductInfo object.
            var info = new ProductInfo
            {
                Name = name.Text,
                QuantityPerUnit = uQty.Text,
                Price = decimal.Parse(price.Text, System.Globalization.NumberStyles.Currency)
            };
            ; // no-operation - just for a breakpoint

            var products = GetExistingProducts(DestinationGridView);
            products.Add(info);

            // Store in the destination Gridview
            DestinationGridView.DataSource = products;
            DestinationGridView.DataBind();

            // Remove the item from the source gridview
            var existing = GetExistingProducts(SourceProductsGridView);
            existing.RemoveAt(SourceProductsGridView.SelectedIndex);
            SourceProductsGridView.DataSource = existing;
            SourceProductsGridView.DataBind();
        }

        private List<ProductInfo> GetExistingProducts(GridView aProductGridView)
        {
            // Loop through all the rows and get each item from each row as a ProductInfo object
            List<ProductInfo> result = new List<ProductInfo>();

            foreach(GridViewRow row in aProductGridView.Rows)
            {
                result.Add(ExtractProduct(row));
            }

            return result;
        }

        private ProductInfo ExtractProduct(GridViewRow row)
        {
            Label name = row.FindControl("ProductName") as Label;
            // TODO: Get the Price
            Label price = row.FindControl("Price") as Label;
            // TODO: Get the Quanitity/Unit
            Label uQty = row.FindControl("Qty") as Label;
            var info = new ProductInfo
            {
                Name = name.Text,
                QuantityPerUnit = uQty.Text,
                Price = decimal.Parse(price.Text, System.Globalization.NumberStyles.Currency)
            };
            return info;
        }

        protected void DestinationGridView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}