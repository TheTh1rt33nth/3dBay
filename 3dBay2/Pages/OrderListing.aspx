
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderListing.aspx.cs" Inherits="_3dBay2.Pages.OrderListing"
     %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="System.Web.Routing" %>
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
        <div class="ordercage">
            <div id="content">
        <%
            _3dBay2.Models.User cur;
            if (Session != null && Session["UserID"] != null)
            {
                cur = GetUser((int)Session["UserID"]);
            }
            else
            {
                cur = null;
            }
            foreach (_3dBay2.Models.Order order in GetOrders())
            {
                
                Response.Write(String.Format(@"<table><tr><td>
                        <div class='item'>
                            <h3>{0}</h3>
                            {1}</div>
                        </td>
                           <td><div class='item'>
                            <h3>Срок выполнения до: </h3>
                            {2}</div></td>
                            <td><div class='item'>
                            <h3>Готов заплатить: </h3>
                            {3}</div></td>",
                    "Заказ № "+order.OrderId.ToString()+" : "+ order.Name, order.Description, order.DateToMake, order.Price));
                if (cur != null)
                {
                    
                    if (cur.IfMaker&&order.MakerId!=cur.ID&&order.CustomerId!=cur.ID&&order.MakerId==0)
                    {
                        Response.Write(String.Format(@"<td>
                        <div class='cage'>
                        <form method='post' action='/Orders/AcceptOrder'>
                            <input type='hidden' name='OrderId' value="));
                        String id = order.OrderId.ToString();
                        Response.Write(id);
                        Response.Write(String.Format(@">
                            <input type='submit' value='Принять заказ'/>
                           </form>
                            </div>
                        </td></tr></table>"));
                    }
                    else
                    {
                        Response.Write(String.Format(@"<td></td></tr></table>"));
                    }
                }
                else
                {
                    Response.Write(String.Format(@"<td></td></tr></table>"));
                }
            } %>
    </div>
    <div class="pager">
       <%
            for (int i = 1; i <= MaxPage; i++)
            {
               
                string path = RouteTable.Routes.GetVirtualPath(null, "order",
                    new RouteValueDictionary() { { "page", i } }).VirtualPath;
                Response.Write(
                    String.Format("<a href='{0}' {1}>{2}</a>",
                        path, i == CurrentPage ? "class='selected'" : "", i));
            }
        %>
    </div>
        </div>
    </div>

</body>
</html>
   

