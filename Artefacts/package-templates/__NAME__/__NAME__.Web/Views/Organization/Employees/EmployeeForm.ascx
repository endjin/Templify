<%@ Control Language="C#" AutoEventWireup="true"
	Inherits="System.Web.Mvc.ViewUserControl<Northwind.Web.Controllers.Organization.EmployeesController.EmployeeFormViewModel>" %>
<%@ Import Namespace="Northwind.Web.Controllers" %>
<%@ Import Namespace="Northwind.Core" %>
<%@ Import Namespace="Northwind.Web.Controllers.Organization" %>

<% if (ViewContext.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] != null) { %>
    <p id="pageMessage"><%= ViewContext.TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()]%></p>
<% } %>

<%= Html.ValidationSummary() %>

<% using (Html.BeginForm()) { %>
    <%= Html.AntiForgeryToken() %>
    <%= Html.Hidden("Employee.Id", (ViewData.Model.Employee != null) ? ViewData.Model.Employee.Id : 0)%>

    <!--
        Be sure to use CSS driven forms instead of layout via tables; 
        see http://wufoo.com/gallery/ for a ton of examples and templates.
        You can also Google "tableless forms" or "CSS forms."
    -->
    <ul>
		<li>
			<label for="Employee_FirstName">FirstName:</label>
			<div>
				<%= Html.TextBox("Employee.FirstName", 
					(ViewData.Model.Employee != null) ? ViewData.Model.Employee.FirstName : "")%>
			</div>
			<%= Html.ValidationMessage("Employee.FirstName")%>
		</li>
		<li>
			<label for="Employee_LastName">LastName:</label>
			<div>
				<%= Html.TextBox("Employee.LastName", 
					(ViewData.Model.Employee != null) ? ViewData.Model.Employee.LastName : "")%>
			</div>
			<%= Html.ValidationMessage("Employee.LastName")%>
		</li>
		<li>
			<label for="Employee_PhoneExtension">PhoneExtension:</label>
			<div>
				<%= Html.TextBox("Employee.PhoneExtension", 
					(ViewData.Model.Employee != null) ? ViewData.Model.Employee.PhoneExtension.ToString() : "")%>
			</div>
			<%= Html.ValidationMessage("Employee.PhoneExtension")%>
		</li>
		<li>
			<label for="Employee_Territories">Territories:</label>
            <span id="Employee_Territories">
                <table>
                    <tr>
                    <% for (int i = 0; i < ViewData.Model.AvailableTerritories.Count(); i++) {
                        if (i > 0 && i % 4 == 0) {
                           %></tr><tr><%
                        }

                        Territory territory = ViewData.Model.AvailableTerritories[i];
                        %>
                        <td>
                            <!-- 
                            It's very important that the name of the checkbox has a "." in it so that 
                            the binder sees it as a property value.
                            -->
                            <input type="checkbox" name="Employee.Territories" value="<%= territory.Id %>" <%
                                if (ViewData.Model.Employee != null && ViewData.Model.Employee.Territories.Contains(territory)) { 
                                    %>checked="checked"<% 
                                } %>/> 
                            <%= territory.Description%>
                        </td>
                    <% } %>
                    </tr>
                </table>
            </span>
		</li>
	    <li>
            <%= Html.SubmitButton("btnSave", "Save Employee") %>
	        <%= Html.Button("btnCancel", "Cancel", HtmlButtonType.Button, 
				    "window.location.href = '" + Html.BuildUrlFromExpression<EmployeesController>(c => c.Index()) + "';") %>
        </li>
    </ul>
<% } %>
