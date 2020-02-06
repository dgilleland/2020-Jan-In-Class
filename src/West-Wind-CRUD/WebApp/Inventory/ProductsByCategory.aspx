<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductsByCategory.aspx.cs" Inherits="WebApp.Inventory.ProductsByCategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <h1 class="page-header">Products by Category</h1>

            <asp:Repeater ID="CategoryRepeater" runat="server" DataSourceID="DataSource"
                ItemType="WestWindSystem.DataModels.CategorizedProducts">
                <ItemTemplate>
                    <img style="float:left; height:30px; margin-right:7px;"
                         src="data:image/png;base64,<%# Convert.ToBase64String(Item.Picture) %>" />
                    <h3><%# Item.Name %></h3>
                    <p><%# Item.Description %></p>
                    <asp:Repeater ID="ProductRepeater" runat="server"
                        DataSource="<%# Item.Products %>"
                        ItemType="WestWindSystem.DataModels.ProductInfo">
                        <HeaderTemplate><ul></HeaderTemplate>
                        <FooterTemplate></ul></FooterTemplate>
                        <ItemTemplate>
                            <li>
                                <i><%# Item.Price.ToString("C") %></i>
                                <b><%# Item.Name %></b> - <%# Item.QuantityPerUnit %>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ItemTemplate>
                <SeparatorTemplate><hr /></SeparatorTemplate>
            </asp:Repeater>

            <asp:ObjectDataSource ID="DataSource" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ListProductsByCategory" TypeName="WestWindSystem.BLL.InventoryController"></asp:ObjectDataSource>
        </div>
    </div>
</asp:Content>
