<%@ Page Title="Employee Details" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" 
	Inherits="System.Web.Mvc.ViewPage<Northwind.Core.Organization.Employee>" %>
<%@ Import Namespace="Northwind.Core" %>
<%@ Import Namespace="Northwind.Web.Controllers.Organization" %>

<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

    <h1>Employee Details</h1>

    <ul>
		<li>
			<label for="Employee_FirstName">FirstName:</label>
            <span id="Employee_FirstName"><%= Server.HtmlEncode(ViewData.Model.FirstName) %></span>
		</li>
		<li>
			<label for="Employee_LastName">LastName:</label>
            <span id="Employee_LastName"><%= Server.HtmlEncode(ViewData.Model.LastName) %></span>
		</li>
		<li>
			<label for="Employee_PhoneExtension">PhoneExtension:</label>
            <span id="Employee_PhoneExtension"><%= Server.HtmlEncode(ViewData.Model.PhoneExtension.ToString()) %></span>
		</li>
		<li>
			<label for="Employee_Territories">Territories:</label>
            <span id="Employee_Territories">
                <ul>
                    <%
                        foreach (Territory territory in ViewData.Model.Territories) { %>
                            <li><%= territory.Description %></li>
                        <% }
                    %>
                </ul>
            </span>
		</li>
	    <li class="buttons">
            <%= Html.Button("btnBack", "Back", HtmlButtonType.Button, 
                "window.location.href = '" + Html.BuildUrlFromExpression<EmployeesController>(c => c.Index()) + "';") %>
        </li>
	</ul>

</asp:Content>
