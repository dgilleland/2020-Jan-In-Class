<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageProducts.aspx.cs" Inherits="WebApp.Inventory.ManageProducts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <h1 class="page-header">Manage Products</h1>
        </div>
        <div class="col-md-12">
            <asp:GridView ID="ProductInventoryGridView" runat="server"
                DataSourceID="ProductInventoryDataSource"
                AutoGenerateColumns="False"
                CssClass="table table-hover table-condensed"
                >
                <Columns>
                    <asp:BoundField DataField="ProductID" HeaderText="ID"></asp:BoundField>
                    <asp:BoundField DataField="ProductName" HeaderText="Name"></asp:BoundField>
                    <asp:BoundField DataField="SupplierID" HeaderText="Supplier"></asp:BoundField>
                    <asp:BoundField DataField="CategoryID" HeaderText="Category"></asp:BoundField>
                    <asp:BoundField DataField="QuantityPerUnit" HeaderText="Qty/Unit"></asp:BoundField>
                    <asp:BoundField DataField="MinimumOrderQuantity" HeaderText="Min Order"></asp:BoundField>
                    <asp:BoundField DataField="UnitPrice" HeaderText="Unit Price" DataFormatString="{0:C}"></asp:BoundField>
                    <asp:BoundField DataField="UnitsOnOrder" HeaderText="Qty On Order"></asp:BoundField>
                    <asp:CheckBoxField DataField="Discontinued" HeaderText="Discontinued"></asp:CheckBoxField>
                </Columns>
            </asp:GridView>
            <asp:ObjectDataSource ID="ProductInventoryDataSource" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ListAllProducts" TypeName="WestWindSystem.BLL.InventoryController"></asp:ObjectDataSource>
        </div>
    </div>
</asp:Content>
