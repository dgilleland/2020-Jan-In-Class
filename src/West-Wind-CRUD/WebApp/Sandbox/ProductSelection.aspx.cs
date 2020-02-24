using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WestWindSystem.BLL;

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
            if (name != null)
                message += $" The product name is {name.Text}";
            MessageUserControl.ShowInfo(message);
        }
    }
}