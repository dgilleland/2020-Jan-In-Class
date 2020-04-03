using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp
{
    public partial class _Default : Page // example of inheritance
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ;
            //var sameObject = this.FindControl("ActiveCourses") as ListView;
            //ActiveCourses.DataSource = null;
            // Inheritance is the idea that one data type can gain all the behaviour/data from another data type
            // Composition is the idea that a data type can have other data types (objects) as properties/fields
            // Principal: Favor composition over inheritance - 
        }
        //protected void Page_Render(object sender, EventArgs e)
        //{
        //    ;
        //}
    }
}