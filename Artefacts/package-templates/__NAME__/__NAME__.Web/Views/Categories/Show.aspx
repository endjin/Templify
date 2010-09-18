<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage<Category>" %>
<%@ Import Namespace="Northwind.Core" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <h2>Category Details</h2>

    <div>
        <p>
            ID:
            <%= ViewData.Model.Id %></p>
        <p>
            Name:
            <%= ViewData.Model.CategoryName%></p>
    </div>
</asp:Content>
