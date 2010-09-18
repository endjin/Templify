<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Northwind.Web.Views.Customers.Index" %>
<%@ Import Namespace="Northwind.Core" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <h2>Customers</h2>
    <p>All customers from Venezuela:</p>    
    <div>
        <asp:ListView ID="customerList" runat="server">
            <LayoutTemplate>
                <ul>
                    <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                </ul>
            </LayoutTemplate>
            <ItemTemplate>
                <li>
                    <%# ((Customer) Container.DataItem).ContactName %>
                    has placed
                    <%# ((Customer) Container.DataItem).Orders.Count %>
                    orders. </li>
            </ItemTemplate>
        </asp:ListView>
    </div>
</asp:Content>