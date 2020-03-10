<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrderShipping.aspx.cs" Inherits="WebApp.Sandbox.OrderShipping" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <h1 class="page-header">Order Processing</h1>
            <asp:Label ID="SupplierName" runat="server"/>
            <asp:Label ID="ContactName" runat="server" />
            <uc1:MessageUserControl runat="server" ID="MessageUserControl" />

            <asp:ListView ID="ShipmentsListView" runat="server"
                DataSourceID="OutstandingOrdersDataSource"
                ItemType="WestWindSystem.DataModels.OrderProcessing.OutstandingOrder">
                <LayoutTemplate>
                    <table runat="server" id="itemPlaceholderContainer"
                         class="table table-hover">
                        <tr runat="server">
                            <th runat="server">Ship To</th>
                            <th runat="server">Ordered On</th>
                            <th runat="server">Required By</th>
                            <th runat="server"><!-- Select/Expand --></th>
                        </tr>
                        <tr runat="server" id="itemPlaceholder"></tr>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            (<%# Item.OrderID %>)
                            <%# Item.ShipToName %>
                        </td>
                        <td>
                            <%# Item.OrderedDate.ToString("MMM dd, yyyy") %>
                        </td>
                        <td>
                            <%# Item.RequiredDate.ToString("MMM dd, yyyy") %>
                            - in <%# Item.DaysToDelivery %> days
                        </td>
                        <td>
                            <asp:LinkButton ID="EditOrder" runat="server"
                                CommandName="Edit" CssClass="btn btn-default">
                                Order Details
                            </asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>

            <asp:HiddenField ID="TempSupplier" runat="server" Value="3" />
            <asp:ObjectDataSource ID="OutstandingOrdersDataSource" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="LoadOrders" TypeName="WestWindSystem.BLL.OrderProcessingController">
                <SelectParameters>
                    <asp:ControlParameter ControlID="TempSupplier" PropertyName="Value" DefaultValue="0" Name="supplierID" Type="Int32"></asp:ControlParameter>
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>
    </div>
</asp:Content>
