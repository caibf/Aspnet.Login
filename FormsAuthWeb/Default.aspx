<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="FormsAuthWeb.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>FormsAuthentication - 实现登录与注销</title>
    <link type="text/css" rel="Stylesheet" href="css/StyleSheet.css" />
</head>
<body>
    <fieldset>
        <legend>普通登录</legend>
        <form action="<%= Request.RawUrl %>" method="post">
            登录名：<input type="text" name="loginName" style="width: 200px" value="cbfjay" />
            <input type="submit" name="NormalLogin" value="登录" />
        </form>
    </fieldset>

    <fieldset>
        <legend>包含【用户信息】的自定义登录</legend>
        <form action="<%= Request.RawUrl %>" method="post">
            <table border="0">
                <tr><td>登录名：</td>
                    <td><input type="text" name="loginName" style="width: 200px" value="cbfjay" /></td></tr>
                <tr><td>UserId：</td>
                    <td><input type="text" name="UserId" style="width: 200px" value="78" /></td></tr>
                <tr><td>GroupId：</td>
                    <td><input type="text" name="GroupId" style="width: 200px" />
                    1表示管理员用户
                    </td></tr>
                <tr><td>用户全名：</td>
                    <td><input type="text" name="UserName" style="width: 200px" value="Xlive1991" /></td></tr>
            </table>    
            <input type="submit" name="CustomizeLogin" value="登录" />
        </form>
    </fieldset>

    <fieldset>
        <legend>用户状态</legend>
        <form action="<%= Request.RawUrl %>" method="post">
            <% if(Request.IsAuthenticated) { %>
            当前用户已登录，登录名：<%=Context.User.Identity.Name.HtmlEncode() %>
            <input type="submit" name="Logout" value="退出" />
            <% } else { %>
            <b>当前用户还未登录。</b>
            <% } %>
        </form>
    </fieldset>
    
	<fieldset><legend>查看其它页面</legend>
		<p><a href="MyInfo.aspx" target="_blank">MyInfo.aspx  （用于【已登录】用户浏览）</a>	</p>
		<p><a href="Admin/Default.aspx" target="_blank">Admin/Default.aspx  （用于【管理员】用户浏览）</a></p>			
	</fieldset>

    <p id="hideText"><i>不应该显示的文字</i></p>
    <%--<script type="text/javascript">
        document.getElementById("hideText").setAttribute("style", "display: none");
    </script>--%>
    <!-- 测试页面引用JS文件的场景 -->
    <!-- 使用ASP.NET Development Server，由于静态资源需要交给ASP.NET来响应，所以在不允许匿名用户访问的情况下会重定向到登陆页 -->
    <!-- 使用IIS，静态资源直接处理，不会发生授权检查失败 -->
    <script type="text/javascript" src="js/JScript.js"></script>
</body>
</html>
