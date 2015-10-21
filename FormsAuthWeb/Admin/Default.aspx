<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="FormsAuthWeb.Admin.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>管理员页面 - 访问权限控制</title>
</head>
<body>
    <%--可以在 web.config 设置禁止非管理员用户访问本页。--%>

    <h3>这是一个【管理员】用户才能查看的页面</h3>

    IsAuthenticated: <%= Request.IsAuthenticated %> <br />
    IsAdmin: <%= Context.User.IsInRole("Admin") %>
</body>
</html>
