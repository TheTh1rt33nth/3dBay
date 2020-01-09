<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReviewCreation.aspx.cs" Inherits="_3dBay2.Pages.ReviewCreation" %>

<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Register TagPrefix="Bay" TagName="CategoryLinks" Src="~/Controls/CategoryList.ascx" %>
<!DOCTYPE html>
<% if ((Session == null || Session["UserID"] == null))
    {
        if (Session["OrderID"] == null)
        {
            Response.Redirect("/Home/Index");
            return;
        }
        else
        {
            Response.Redirect("/Home/LogNeeded");
            return;
        }

    }
    
   %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>3dBay</title>
    <link rel="stylesheet" href="/Content/Styles.css" />
</head>
<body>

    <div>
        <div id="header">
            <div class="title">3dBay - печать для вас!</div>
            <div class="headbuttons">
                <a href="/Home/Index">Главная</a>
                <% {
                        _3dBay2.Models.Repository.Repository repo = new _3dBay2.Models.Repository.Repository();
                        if (Session != null && Session["UserID"] != null)
                        {
                            Response.Write(String.Format(@"<a href='/Account/MyPage'>Моя страница</a>"));

                            if (repo.GetUserByID((int)Session["UserID"]).IfCustomer)
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
            <form method="post" enctype="multipart/form-data" action="/Reviews/ValidateReview">
                <table>
                    <tr>
                        <td>
                            <p>Отзыв на заказ номер : </p>
                        </td>
                        <td>
                            <%
                                String number = Session["OrderID"].ToString();
                                number.Trim(new char[] { '/' });
                                int id = Convert.ToInt32(number);
                                Response.Write(String.Format(@"<p><b>{0}</b></p>",number));
                                %>
                        </td>
                    </tr>
                     <tr>
                        <td>
                            <p>От</p>
                        </td>
                        <td>
                            <%
                                _3dBay2.Models.Repository.Repository repository = new _3dBay2.Models.Repository.Repository();
                                if(Session["IfMaker"].ToString()=="true")
                                {
                                    Response.Write(String.Format(@"<p><b>{0}</b></p>",repository.GetUserByID((int)Session["UserId"]).Name));
                                }
                                else
                                {
                                    Response.Write(String.Format(@"<p><b>{0}</b></p>",repository.GetUserByID((int)Session["UserId"]).Name));
                                }
                                %>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <p>Пользователю-</p>
                        </td>
                        <td>
                            <%
                                _3dBay2.Models.Order order = repository.GetOrderByID(id);
                                if(Session["IfMaker"].ToString()=="true")
                                {
                                    Response.Write(String.Format(@"<p><b>{0}</b></p>",repository.GetUserByID(order.MakerId).Name));
                                }
                                else
                                {
                                    Response.Write(String.Format(@"<p><b>{0}</b></p>",repository.GetUserByID(order.CustomerId).Name));
                                }
                                %>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <p>Ваши впечатления : </p>
                        </td>
                        <td>
                           <textarea name="Description" cols="30" rows="5" class="maxWidth"></textarea>

                        </td>
                    </tr>

                    <tr>
                        <td>
                            <p>Оставьте оценку :</p>
                        </td>
                        <td>
                            <input type="number" max=5" min="0" list="mark" name="marks" placeholder="Ваша оценка:">
                            <datalist id="mark">
                                <option value="5" label="5">
                                    <option value="4" label="4">
                                        <option value="3" label="3">
                                            <option value="2" label="2">
                                                <option value="1" label="1">
                            </datalist></td>
                    </tr>
                    <tr>
                        <td>
                            <input type="submit" value="Отправить" />
                        </td>
                        <td></td>
                    </tr>
                </table>
            </form>
        </div>
    </div>

</body>
</html>

