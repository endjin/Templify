<%@ Page Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage<Northwind.Core.Customer>" %>
<%@ Import Namespace="Northwind.Core" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <div>
        Congratulations, you have a brand new customer, with an assigned ID of
        <%= ViewData.Model.Id %> who happens to work for <%= ViewData.Model.CompanyName%>.
    </div>
</body>
</html>
