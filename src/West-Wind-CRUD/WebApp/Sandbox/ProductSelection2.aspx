<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductSelection2.aspx.cs" Inherits="WebApp.Sandbox.ProductSelection2" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <h1 class="page-header">Product Selection (ListView)</h1>
            <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
        </div>
    </div>
    <div class="row">
        <div class="col-md-6">
            <asp:ListView ID="AvailableProductsListView" runat="server"
                 OnSelectedIndexChanged="AvailableProductsListView_SelectedIndexChanged">
                <ItemTemplate>
                    <asp:LinkButton ID="AddProduct" runat="server"
                         CommandName="Select">Add</asp:LinkButton>
                </ItemTemplate>
            </asp:ListView>
        </div>
        <div class="col-md-6">
        </div>
    </div>
</asp:Content>
