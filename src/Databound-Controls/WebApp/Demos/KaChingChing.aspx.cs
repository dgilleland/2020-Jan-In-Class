using BackEnd.BLL;
using BackEnd.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp.Demos
{
    public partial class KaChingChing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SellerGridView.DataBind();
            }
        }

        private static Random _rnd = new Random();
        protected void NextSet_Click(object sender, EventArgs e)
        {
            var controller = new BlackMarketController();
            var data = controller.GetVehicles();
            SellerGridView.DataSource = data;
            SellerGridView.DataBind();
        }

        protected void SellerGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // 1) Find the row, based on the index of the command
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            var row = SellerGridView.Rows[rowIndex];
            string vin = SellerGridView.DataKeys[rowIndex].Value.ToString();
            var carToMove = ExtractVehicleData(row, vin);
            PopulateSellerGridView(rowIndex);
            PopulateBuyerGridView(carToMove);
        }

        private void PopulateSellerGridView(int skipRow)
        {
            var cars = new List<Vehicle>();
            for (int index = 0; index < SellerGridView.Rows.Count; index++)
            {
                if (index != skipRow)
                {
                    var row = SellerGridView.Rows[index];
                    string vin = SellerGridView.DataKeys[index].Value.ToString();
                    cars.Add(ExtractVehicleData(row, vin));
                }
            }
            SellerGridView.DataSource = cars;
            SellerGridView.DataBind();
        }

        private void PopulateBuyerGridView(Vehicle appendItem)
        {
            var cars = new List<Vehicle>();
            for (int index = 0; index < BuyerInventory.Rows.Count; index++)
            {
                var row = BuyerInventory.Rows[index];
                string vin = BuyerInventory.DataKeys[index].Value.ToString();
                cars.Add(ExtractVehicleData(row, vin));
            }
            cars.Add(appendItem);
            BuyerInventory.DataSource = cars;
            BuyerInventory.DataBind();
        }

        private Vehicle ExtractVehicleData(GridViewRow row, string vin)
        {
            // 2) Create an empty vehicle
            var car = new Vehicle();

            // 3) Get the DataKey value
            car.VIN = vin;

            // 4) Get remaining items by finding the controls in the TemplateFields
            Label labelControl;
            labelControl = row.FindControl("Maker") as Label;
            car.Manufacturer = labelControl.Text;
            labelControl = row.FindControl("Model") as Label;
            car.Model = labelControl.Text;
            labelControl = row.FindControl("CarType") as Label;
            car.Type = labelControl.Text;
            labelControl = row.FindControl("FuelType") as Label;
            car.Fuel = labelControl.Text;
            return car;
        }
    }
}