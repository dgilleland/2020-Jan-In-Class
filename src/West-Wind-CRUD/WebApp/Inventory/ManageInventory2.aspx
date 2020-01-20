<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageInventory2.aspx.cs" Inherits="WebApp.Inventory.ManageInventory2" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <h1 class="page-header">Manage Products (w. ListView)</h1>
            
            <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
            
            <asp:ListView ID="ProductsListView" runat="server"
                DataSourceID="ProductDataSource"
                DataKeyNames="ProductID"
                ItemType="WestWindSystem.Entities.Product"
                InsertItemPosition="LastItem">
                <EditItemTemplate>
                    <tr style="">
                        <td>
                            <asp:Button runat="server" CommandName="Update" Text="Update" ID="UpdateButton" />
                            <asp:Button runat="server" CommandName="Cancel" Text="Cancel" ID="CancelButton" />
                        </td>
                        <td>
                            <asp:TextBox Text='<%# Bind("ProductID") %>' runat="server" ID="ProductIDTextBox" /></td>
                        <td>
                            <asp:TextBox Text='<%# Bind("ProductName") %>' runat="server" ID="ProductNameTextBox" /></td>
                        <td>
                            <asp:TextBox Text='<%# Bind("SupplierID") %>' runat="server" ID="SupplierIDTextBox" /></td>
                        <td>
                            <asp:TextBox Text='<%# Bind("CategoryID") %>' runat="server" ID="CategoryIDTextBox" /></td>
                        <td>
                            <asp:TextBox Text='<%# Bind("QuantityPerUnit") %>' runat="server" ID="QuantityPerUnitTextBox" /></td>
                        <td>
                            <asp:TextBox Text='<%# Bind("MinimumOrderQuantity") %>' runat="server" ID="MinimumOrderQuantityTextBox" /></td>
                        <td>
                            <asp:TextBox Text='<%# Bind("UnitPrice") %>' runat="server" ID="UnitPriceTextBox" /></td>
                        <td>
                            <asp:TextBox Text='<%# Bind("UnitsOnOrder") %>' runat="server" ID="UnitsOnOrderTextBox" /></td>
                        <td>
                            <asp:CheckBox Checked='<%# Bind("Discontinued") %>' runat="server" ID="DiscontinuedCheckBox" /></td>
                        <td>
                            <asp:TextBox Text='<%# Bind("Category") %>' runat="server" ID="CategoryTextBox" /></td>
                        <td>
                            <asp:TextBox Text='<%# Bind("Supplier") %>' runat="server" ID="SupplierTextBox" /></td>
                    </tr>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <tr style="">
                        <td>
                            <asp:Button runat="server" CommandName="Insert" Text="Insert" ID="InsertButton" />
                            <asp:Button runat="server" CommandName="Cancel" Text="Clear" ID="CancelButton" />
                        </td>
                        <td>
                            <asp:TextBox Text='<%# Bind("ProductName") %>' runat="server" ID="ProductNameTextBox" /></td>
                        <td>
                            <asp:TextBox Text='<%# Bind("QuantityPerUnit") %>' runat="server" ID="QuantityPerUnitTextBox" /></td>
                        <td>
                            <asp:TextBox Text='<%# Bind("MinimumOrderQuantity") %>' runat="server" ID="MinimumOrderQuantityTextBox" /></td>
                        <td>
                            <asp:TextBox Text='<%# Bind("UnitPrice") %>' runat="server" ID="UnitPriceTextBox" /></td>
                        <td>
                            <asp:TextBox Text='<%# Bind("UnitsOnOrder") %>' runat="server" ID="UnitsOnOrderTextBox" /></td>
                        <td>
                            </td>
                        <td>
                            <asp:DropDownList ID="SupplierDropDown" runat="server"
                                DataSourceID="SupplierDataSource"
                                DataTextField="CompanyName"
                                DataValueField="SupplierID"
                                SelectedValue="<%# BindItem.SupplierID %>"
                                AppendDataBoundItems="true">
                                <asp:ListItem Value="0">[select a supplier...]</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </InsertItemTemplate>
                <ItemTemplate>
                    <tr style="">
                        <td>
                            <asp:Button runat="server" CommandName="Delete" Text="Delete" ID="DeleteButton" />
                            <asp:Button runat="server" CommandName="Edit" Text="Edit" ID="EditButton" />
                        </td>
                        <td>
                            <asp:Label Text='<%# Eval("ProductName") %>' runat="server" ID="ProductNameLabel" />
                            <br />
                            <asp:CheckBox Checked='<%# Eval("Discontinued") %>' runat="server" ID="DiscontinuedCheckBox" Text="Discontinued" Enabled="false" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("QuantityPerUnit") %>' runat="server" ID="QuantityPerUnitLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("MinimumOrderQuantity") %>' runat="server" ID="MinimumOrderQuantityLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Item.UnitPrice.ToString("C") %>' runat="server" ID="UnitPriceLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("UnitsOnOrder") %>' runat="server" ID="UnitsOnOrderLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Item.Category.CategoryName %>' runat="server" ID="CategoryLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Item.Supplier.CompanyName %>' runat="server" ID="SupplierLabel" /></td>
                    </tr>
                </ItemTemplate>
                <LayoutTemplate>
                    <table runat="server">
                        <tr runat="server">
                            <td runat="server">
                                <table runat="server" id="itemPlaceholderContainer" class="table table-hover">
                                    <tr runat="server" style="">
                                        <th runat="server"></th>
                                        <th runat="server">Product</th>
                                        <th runat="server">Quantity / Unit</th>
                                        <th runat="server">Min. Order</th>
                                        <th runat="server">Unit Price</th>
                                        <th runat="server">On Order</th>
                                        <th runat="server">Category</th>
                                        <th runat="server">Supplier</th>
                                    </tr>
                                    <tr runat="server" id="itemPlaceholder"></tr>
                                </table>
                            </td>
                        </tr>
                        <tr runat="server">
                            <td runat="server" style="">
                                <asp:DataPager runat="server" ID="DataPager1">
                                    <Fields>
                                        <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False"></asp:NextPreviousPagerField>
                                        <asp:NumericPagerField></asp:NumericPagerField>
                                        <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False"></asp:NextPreviousPagerField>
                                    </Fields>
                                </asp:DataPager>
                            </td>
                        </tr>
                    </table>
                </LayoutTemplate>
            </asp:ListView>
            <asp:ObjectDataSource runat="server" ID="SupplierDataSource" OldValuesParameterFormatString="original_{0}" SelectMethod="ListAllSuppliers" TypeName="WestWindSystem.BLL.InventoryController"></asp:ObjectDataSource>
            <asp:ObjectDataSource runat="server" ID="ProductDataSource"
                DataObjectTypeName="WestWindSystem.Entities.Product"
                OnInserted="CheckForExceptions"
                OnUpdated="CheckForExceptions"
                OnDeleted="CheckForExceptions"
                DeleteMethod="DeleteProduct"
                OldValuesParameterFormatString="original_{0}"
                SelectMethod="ListAllProducts"
                TypeName="WestWindSystem.BLL.InventoryController"
                UpdateMethod="UpdateProduct"
                InsertMethod="InsertProduct"></asp:ObjectDataSource>
        </div>
    </div>
</asp:Content>
