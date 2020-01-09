<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="_3dBay2.Pages.LoginPage" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Register TagPrefix="Bay" TagName="CategoryLinks" Src="~/Controls/CategoryList.ascx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>3dBay</title>
    <link rel="stylesheet" href="/Content/Styles.css" />
</head>
<body>
    
    <div>
        <div id="header">
            <div class="title">3dBay - печать для вас!</div>
            <div class="headbuttons"><a href="/Home/Index">Главная</a>
            <% {
                     _3dBay2.Models.Repository.Repository repo = new _3dBay2.Models.Repository.Repository();
                    if (Session != null && Session["UserID"] != null)
                    {
                        Response.Write(String.Format(@"<a href='/Account/MyPage'>Моя страница</a>"));
                        if(repo.GetUserByID((int)Session["UserID"]).IfCustomer)
                        {
                            Response.Write(String.Format(@"<a href='/Pages/OrderCreation.aspx' class='register'>Оформить заказ</a>"));
                        }
                    }
                    else
                    {
                        Response.Write(String.Format(@"<a href='/Pages/LoginPage.aspx'>Войти</a>"));
                        Response.Write(String.Format(@"<a href='/Pages/RegisterPage.aspx' class='register'>Зарегестрироваться</a>")); ;
                    }
                }%>
            
            </div>
        </div>
        <div id="categories">
                <Bay:CategoryLinks runat="server" />
            </div>
        <div class="cage">
            <form method="post" action="/Account/Login">
         <input type="hidden" name="Login" />
    <table>
        <tr><td><p>Введите электронную почту : </p></td>
            <td><input type="text" name="Email"/> </td></tr>
        <tr><td><p>Введите пароль :</p></td>
            <td><input type="password" name="Password" /> </td></tr>
        <tr><td><input type="submit" value="Отправить" /> </td>
                <td></td></tr>
    </table>  
     </form>
        </div>
    </div>

</body>
</html>

