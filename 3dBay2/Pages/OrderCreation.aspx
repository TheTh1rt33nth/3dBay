<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderCreation.aspx.cs" Inherits="_3dBay2.Pages.OrderCreation" %>
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
                             Response.Write(String.Format(@"<a style='background-color: salmon; color: floralwhite;' href='/Account/LogOut'>Выйти</a>"));
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
       <form method="post" enctype="multipart/form-data" action="/Orders/CreateOrder">
    <table>
        <tr><td><p>Название заказа : </p></td>
            <td><input type="text" name="Name" required /> </td></tr>
        <tr><td><p>Описание заказа : </p></td>
            <td><textarea name="Description" cols="40" rows="8" class="maxWidth" required placeholder="Опишите детали заказа"></textarea> </td></tr>
        <tr><td><p>Объем заказа :</p></td>
            <td><input type="text" name="Quantity" required /> </td></tr>
        <tr><td><p>Файл заказа :</p></td>
            <td><input type="file" name="upload" required placeholder="Если файлов больше одного, запакуйте в архив"/> </td></tr>
         <tr><td><p>Сроки заказа :</p></td>
            <td><input type="datetime-local" name="DateToMake" required placeholder="Опишите сроки выполнения"/></td></tr>
         <tr><td><p>Оплата :</p></td>
            <td><input type="text" name="Price" required placeholder="Опишите детали оплаты"/> </td></tr>
        <tr><td><input type="submit" value="Отправить" /> </td>
                <td></td></tr>
    </table>  
     </form>
        </div>
    </div>

</body>
</html>

