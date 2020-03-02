using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp.Sandbox
{
    public partial class ProductSelection2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void AvailableProductsListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Loop through my ListView
            foreach(ListViewDataItem item in AvailableProductsListView.Items)
            {
                
            }
        }
    }
}