<%@ Page Title="Territories via WCF" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" 
    Inherits="System.Web.Mvc.ViewPage<IEnumerable<Northwind.Wcf.Dtos.TerritoryDto>>" %>
<%@ Import Namespace="Northwind.Wcf.Dtos" %>

<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <h2>Territories Pulled from WCF Service</h2>

    <p>
    The territories shown below are pulled via a WCF service, found within the Northwind.Wcf.Web project, hosted via IIS. 
    The WCF service is in a separate project because it requires the use of SharpArch.Wcf.NHibernate.WcfSessionStorage 
    instead of SharpArch.Web.NHibernate.WebSessionStorage.
    </p>

    <table>
        <thead>
            <tr>
                <th>Id</th>
                <th>Description</th>
                <th>Region</th>
                <th>Assigned Employee(s)</th>
            </tr>
        </thead>
		<%
		foreach (TerritoryDto territory in ViewData.Model) { %>
			<tr>
				<td><%= territory.Id %></td>
				<td><%= territory.Description %></td>
				<td><%= territory.RegionBelongingTo.Description %></td>
				<td>
				    <table>
				        <% foreach (EmployeeDto employee in territory.Employees) { %>
				        <tr>
				            <td><%= employee.FirstName %> <%= employee.LastName %></td>
				        </tr>
				        <% } %>
				    </table>
				</td>
			</tr>
		<%} %>
    </table>
</asp:Content>
