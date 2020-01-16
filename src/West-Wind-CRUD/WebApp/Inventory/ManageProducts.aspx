<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageProducts.aspx.cs" Inherits="WebApp.Inventory.ManageProducts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <h1 class="page-header">Manage Products</h1>
        </div>
        <div class="col-md-12">
            <asp:Label ID="MessageLabel" runat="server"></asp:Label>
            <asp:GridView ID="ProductInventoryGridView" runat="server"
                DataSourceID="ProductInventoryDataSource"
                AutoGenerateColumns="False"
                CssClass="table table-hover table-condensed"
                DataKeyNames="ProductID"
                >
                <Columns>
                    <asp:CommandField ShowEditButton="True" ShowDeleteButton="True"></asp:CommandField>
                    <asp:BoundField DataField="ProductName" HeaderText="Name"></asp:BoundField>
                    
                    <asp:TemplateField HeaderText="Supplier">
                        <ItemTemplate>
                            <asp:DropDownList ID="SupplierDropDown" runat="server"
                                DataSourceID="SupplierDataSource"
                                DataTextField="CompanyName"
                                DataValueField="SupplierID"
                                SelectedValue='<%# Eval("SupplierID") %>'>
                            </asp:DropDownList>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="SupplierDropDown" runat="server"
                                DataSourceID="SupplierDataSource"
                                DataTextField="CompanyName"
                                DataValueField="SupplierID"
                                SelectedValue='<%# Bind("SupplierID") %>'>
                            </asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="CategoryID" HeaderText="Category"></asp:BoundField>
                    <asp:BoundField DataField="QuantityPerUnit" HeaderText="Qty/Unit"></asp:BoundField>
                    <asp:BoundField DataField="MinimumOrderQuantity" HeaderText="Min Order"></asp:BoundField>
                    <asp:BoundField DataField="UnitPrice" HeaderText="Unit Price" DataFormatString="{0:C}"></asp:BoundField>
                    <asp:BoundField DataField="UnitsOnOrder" HeaderText="Qty On Order"></asp:BoundField>
                    <asp:CheckBoxField DataField="Discontinued" HeaderText="Discontinued"></asp:CheckBoxField>
                </Columns>
            </asp:GridView>
            <asp:ObjectDataSource ID="ProductInventoryDataSource" runat="server"
                OldValuesParameterFormatString="original_{0}"
                OnDeleted="ProductInventoryDataSource_Deleted"
                OnDeleting="ProductInventoryDataSource_Deleting"
                SelectMethod="ListAllProducts"
                TypeName="WestWindSystem.BLL.InventoryController"
                DataObjectTypeName="WestWindSystem.Entities.Product"
                DeleteMethod="DeleteProduct"
                UpdateMethod="UpdateProduct"
                ></asp:ObjectDataSource>
                                        <asp:ObjectDataSource runat="server" ID="SupplierDataSource" OldValuesParameterFormatString="original_{0}" SelectMethod="ListAllSuppliers" TypeName="WestWindSystem.BLL.InventoryController"></asp:ObjectDataSource>
        </div>
    </div>
</asp:Content>
