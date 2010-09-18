<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" 
    Inherits="System.Web.Mvc.ViewPage<Northwind.ApplicationServices.DashboardService.DashboardSummaryDto>" %>
<%@ Import Namespace="Northwind.Core" %>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <h2>Suppliers Carrying Most/Fewest Products</h2>

    <!-- 
        Quick!  What coding smell is below???  It's duplicated code!  At the very least, the two tables 
        should be moved into a reusable view control...but I don't want to further complicate this example
        of using an "application service" by throwing a view control into the mix.  But note that it's a smell!
    -->

    <h3>Suppliers carrying the most produts:</h3>
    
    <table>
        <thead>
            <tr>
                <th>Supplier Name</th>
                <th>Product Count</th>
            </tr>
        </thead>
        <tbody>
            <% foreach (Supplier supplier in ViewData.Model.SuppliersCarryingMostProducts) { %>
                <tr>
                    <td><%= supplier.CompanyName %></td>
                    <td><%= supplier.Products.Count %></td>
                </tr>
            <% }%>
        </tbody>
    </table>

    <h3>Suppliers carrying the most produts:</h3>
    
    <table>
        <thead>
            <tr>
                <th>Supplier Name</th>
                <th>Product Count</th>
            </tr>
        </thead>
        <tbody>
            <% foreach (Supplier supplier in ViewData.Model.SuppliersCarryingFewestProducts) { %>
                <tr>
                    <td><%= supplier.CompanyName %></td>
                    <td><%= supplier.Products.Count %></td>
                </tr>
            <% }%>
        </tbody>
    </table>
</asp:Content>