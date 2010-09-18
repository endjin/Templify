<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Northwind.Web.Views.Categories.Index" %>
<%@ Import Namespace="Northwind.Core" %>
<%@ Import Namespace="Northwind.Web.Controllers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <h2>Categories</h2>

    <form id="form1" runat="server">
    <div>
        <asp:ListView ID="categoryList" runat="server">
            <LayoutTemplate>
                <ul>
                    <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                </ul>
            </LayoutTemplate>
            <ItemTemplate>
                <li>
                    <%# Html.ActionLink<CategoriesController>(c => c.Show(((Category)Container.DataItem).Id), ((Category)Container.DataItem).CategoryName) %>
                </li>
            </ItemTemplate>
        </asp:ListView>
    </div>
    </form>
</asp:Content>
