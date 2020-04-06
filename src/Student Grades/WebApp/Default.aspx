<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApp._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>GradeBook Demo</h1>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Courses &amp; Offerings</h2>
            <h3>Courses</h3>
            <asp:HiddenField ID="GraduationYear" runat="server"
                 Value="" />
            <asp:ListView ID="ActiveCourses" runat="server" ItemType="GradeBook.DataModels.ActiveCourse" DataSourceID="CourseListDataSource">
                <AlternatingItemTemplate>
                    <tr style="">
                        <td>
                            <asp:Label Text='<%# Eval("CourseId") %>' runat="server" ID="CourseIdLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("Name") %>' runat="server" ID="NameLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("Number") %>' runat="server" ID="NumberLabel" /></td>
                    </tr>
                </AlternatingItemTemplate>
                <EditItemTemplate>
                    <tr style="">
                        <td>
                            <asp:Button runat="server" CommandName="Update" Text="Update" ID="UpdateButton" />
                            <asp:Button runat="server" CommandName="Cancel" Text="Cancel" ID="CancelButton" />
                        </td>
                        <td>
                            <asp:TextBox Text='<%# Bind("CourseId") %>' runat="server" ID="CourseIdTextBox" /></td>
                        <td>
                            <asp:TextBox Text='<%# Bind("Name") %>' runat="server" ID="NameTextBox" /></td>
                        <td>
                            <asp:TextBox Text='<%# Bind("Number") %>' runat="server" ID="NumberTextBox" /></td>
                    </tr>
                </EditItemTemplate>
                <EmptyDataTemplate>
                    <table runat="server" style="">
                        <tr>
                            <td>No data was returned.</td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <InsertItemTemplate>
                    <tr style="">
                        <td>
                            <asp:Button runat="server" CommandName="Insert" Text="Insert" ID="InsertButton" />
                            <asp:Button runat="server" CommandName="Cancel" Text="Clear" ID="CancelButton" />
                        </td>
                        <td>
                            <asp:TextBox Text='<%# Bind("CourseId") %>' runat="server" ID="CourseIdTextBox" /></td>
                        <td>
                            <asp:TextBox Text='<%# Bind("Name") %>' runat="server" ID="NameTextBox" /></td>
                        <td>
                            <asp:TextBox Text='<%# Bind("Number") %>' runat="server" ID="NumberTextBox" /></td>
                    </tr>
                </InsertItemTemplate>
                <ItemTemplate>
                    <tr style="">
                        <td>
                            <asp:Label Text='<%# Eval("CourseId") %>' runat="server" ID="CourseIdLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("Name") %>' runat="server" ID="NameLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("Number") %>' runat="server" ID="NumberLabel" /></td>
                    </tr>
                </ItemTemplate>
                <LayoutTemplate>
                    <table runat="server">
                        <tr runat="server">
                            <td runat="server">
                                <table runat="server" id="itemPlaceholderContainer" style="" border="0">
                                    <tr runat="server" style="">
                                        <th runat="server">CourseId</th>
                                        <th runat="server">Name</th>
                                        <th runat="server">Number</th>
                                    </tr>
                                    <tr runat="server" id="itemPlaceholder"></tr>
                                </table>
                            </td>
                        </tr>
                        <tr runat="server">
                            <td runat="server" style=""></td>
                        </tr>
                    </table>
                </LayoutTemplate>
                <SelectedItemTemplate>
                    <tr style="">
                        <td>
                            <asp:Label Text='<%# Eval("CourseId") %>' runat="server" ID="CourseIdLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("Name") %>' runat="server" ID="NameLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("Number") %>' runat="server" ID="NumberLabel" /></td>
                    </tr>
                </SelectedItemTemplate>
            </asp:ListView>
            <asp:ObjectDataSource ID="CourseListDataSource" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ListActiveCourses" TypeName="GradeBook.BLL.CourseCatalogController"></asp:ObjectDataSource>
        </div>
        <div class="col-md-4">
            <h2>Student Registration</h2>
        </div>
        <div class="col-md-4">
            <h2>Instructor's GradeBook</h2>
        </div>
    </div>

</asp:Content>
