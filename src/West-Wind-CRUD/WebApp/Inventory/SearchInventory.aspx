<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SearchInventory.aspx.cs" Inherits="WebApp.Inventory.SearchInventory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <h1 class="page-header">Search Inventory</h1>
        </div>
    </div>
    <div class="row">
        <div class="col-md-4">
            <asp:DropDownList ID="SuppliersDropDown" runat="server"
                AppendDataBoundItems="true"
                DataSourceID="SuppliersDataSource"
                DataTextField="CompanyName"
                DataValueField="SupplierID">
                <asp:ListItem Value="0">[Select a supplier...]</asp:ListItem>
            </asp:DropDownList>
            <asp:LinkButton ID="SearchBySupplier" runat="server"
                CssClass="btn btn-default">Search by Supplier</asp:LinkButton>
            <asp:ObjectDataSource ID="SuppliersDataSource" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ListAllSuppliers" TypeName="WestWindSystem.BLL.InventoryController"></asp:ObjectDataSource>
        </div>
        <div class="col-md-4"></div>
        <div class="col-md-4"></div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <h2>Inventory</h2>
            <asp:GridView ID="ProductsGridView" runat="server" AutoGenerateColumns="False" DataSourceID="ProductsDataSource">
                <Columns>
                    <asp:BoundField DataField="ProductID" HeaderText="ProductID" SortExpression="ProductID"></asp:BoundField>
                    <asp:BoundField DataField="ProductName" HeaderText="ProductName" SortExpression="ProductName"></asp:BoundField>
                    <asp:BoundField DataField="SupplierID" HeaderText="SupplierID" SortExpression="SupplierID"></asp:BoundField>
                    <asp:BoundField DataField="CategoryID" HeaderText="CategoryID" SortExpression="CategoryID"></asp:BoundField>
                    <asp:BoundField DataField="QuantityPerUnit" HeaderText="QuantityPerUnit" SortExpression="QuantityPerUnit"></asp:BoundField>
                    <asp:BoundField DataField="MinimumOrderQuantity" HeaderText="MinimumOrderQuantity" SortExpression="MinimumOrderQuantity"></asp:BoundField>
                    <asp:BoundField DataField="UnitPrice" HeaderText="UnitPrice" SortExpression="UnitPrice"></asp:BoundField>
                    <asp:BoundField DataField="UnitsOnOrder" HeaderText="UnitsOnOrder" SortExpression="UnitsOnOrder"></asp:BoundField>
                    <asp:CheckBoxField DataField="Discontinued" HeaderText="Discontinued" SortExpression="Discontinued"></asp:CheckBoxField>
                </Columns>
            </asp:GridView>

            <asp:ObjectDataSource ID="ProductsDataSource" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ListProductsBySupplier" TypeName="WestWindSystem.BLL.InventoryController">
                <SelectParameters>
                    <asp:ControlParameter ControlID="SuppliersDropDown" PropertyName="SelectedValue" Name="supplierId" Type="Int32"></asp:ControlParameter>
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>
    </div>
</asp:Content>
