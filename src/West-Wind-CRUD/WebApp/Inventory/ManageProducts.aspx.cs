using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp.Inventory
{
    public partial class ManageProducts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ProductInventoryDataSource_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if(e.Exception != null)
            {
                // Get to the root of the problem
                Exception inner = e.Exception; // Start at the top
                while (inner.InnerException != null)
                    inner = inner.InnerException; // Go a level deeper

                MessageLabel.Text = inner.Message;
                e.ExceptionHandled = true;
            }
        }

        protected void ProductInventoryDataSource_Deleting(object sender, ObjectDataSourceMethodEventArgs e)
        {

        }
    }
}