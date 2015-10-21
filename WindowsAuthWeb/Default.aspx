<%@ Page Language="C#" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Windows Authentication - 在ASP.NET中访问Active Directory</title>
</head>
<body>
    <% if(Request.IsAuthenticated) { %>
    当前登录全名：<%=Context.User.Identity.Name.HtmlEncode() %>

        <% var user = UserHelper.GetCurrentUserInfo(Context); %>
	    <% if( user != null ) { %>
        用户短名：<%= user.GivenName.HtmlEncode()%> <br />
		用户全名：<%= user.FullName.HtmlEncode() %> <br />
		邮箱地址：<%= user.Email.HtmlEncode() %>
        <% } %>	
    <% } else { %>
    当前用户还未登录。
    <% } %>	
</body>
</html>
