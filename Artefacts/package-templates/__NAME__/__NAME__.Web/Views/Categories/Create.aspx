<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" 
    Inherits="System.Web.Mvc.ViewPage<Northwind.Core.Category>" %>
<%@ Import Namespace="Northwind.Core" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

<h2>Category Created</h2>

<p>You just created a new category with a name of <%= ViewData.Model.CategoryName %>!</p>
<p>No really, go check the DB...there it is!</p>
</asp:Content>
