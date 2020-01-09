<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="KaChingChing.aspx.cs" Inherits="WebApp.Demos.KaChingChing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="page-header">Ka-Ching-Ching <small>The Black Market version of Kijiji</small></h1>

    <div class="row">
        <div class="col-md-6">
            <h2>
                Seller
                <small style="vertical-align:middle;">
                    <asp:LinkButton ID="NextSet" runat="server"
                        OnClick="NextSet_Click"
                        CssClass="btn btn-sm btn-default">Next Set</asp:LinkButton>
                </small>
            </h2>
            <asp:GridView ID="SellerGridView" runat="server"
                CssClass="table table-hover table-condensed"
                DataKeyNames="VIN"
                AutoGenerateColumns="false"
                ItemType="BackEnd.DataModels.Vehicle"
                OnRowCommand="SellerGridView_RowCommand">
                <EmptyDataTemplate>
                    <i>No goods - find the
                        <asp:LinkButton ID="NextSet" runat="server"
                        OnClick="NextSet_Click"
                        CssClass="btn btn-primary">Next Set</asp:LinkButton></i>
                </EmptyDataTemplate>
                <Columns>
                    <asp:BoundField HeaderText="VIN" DataField="VIN" />
                    <asp:TemplateField HeaderText="Maker">
                        <ItemTemplate><asp:Label ID="Maker" runat="server" Text="<%# Item.Manufacturer %>" /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Model">
                        <ItemTemplate><asp:Label ID="Model" runat="server" Text="<%# Item.Model %>" /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Type">
                        <ItemTemplate><asp:Label ID="CarType" runat="server" Text="<%# Item.Type %>" /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Fuel">
                        <ItemTemplate><asp:Label ID="FuelType" runat="server" Text="<%# Item.Fuel %>" /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:ButtonField CommandName="Select" Text="Add" />
                </Columns>
            </asp:GridView>
        </div>
        <div class="col-md-6">
            <h2><s>Fence</s> Buyer</h2>
            <asp:GridView ID="BuyerInventory" runat="server"
                CssClass="table table-condensed"
                DataKeyNames="VIN"
                AutoGenerateColumns="false"
                ItemType="BackEnd.DataModels.Vehicle">
                <EmptyDataTemplate>
                    <i>Empty</i>
                </EmptyDataTemplate>
                <Columns>
                    <asp:BoundField HeaderText="VIN" DataField="VIN" />
                    <asp:TemplateField HeaderText="Maker">
                        <ItemTemplate><asp:Label ID="Maker" runat="server" Text="<%# Item.Manufacturer %>" /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Model">
                        <ItemTemplate><asp:Label ID="Model" runat="server" Text="<%# Item.Model %>" /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Type">
                        <ItemTemplate><asp:Label ID="CarType" runat="server" Text="<%# Item.Type %>" /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Fuel">
                        <ItemTemplate><asp:Label ID="FuelType" runat="server" Text="<%# Item.Fuel %>" /></ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
