
=<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="News.aspx.cs" Inherits="_3dBay2.Pages.News"
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
        <div class="maincontent">
            <div id="content">
        <%      
                List<string> headers = ParseNewsHeaders();
                List<string> links = ParseNewsLinks();
             Response.Write(String.Format(@"
                         <div class='Disclaimer' style='margin-bottom:2em;text-align:center;'>
                         <h3>Новости печати и печатников с 3dtoday.com</h3></div>"));
                for (int i=0; i<headers.Count; i++)
                {

                    Response.Write(String.Format(@"
                         <div class='parsedItem' style='margin-bottom:2em;text-align:center;'>
                         <a href='{1}' style='background-color:#00ade2;padding-bottom:1em;text-align:center;'>{0}</a></div>",headers[i],links[i]));
                        
            }
            /* foreach (_3dBay2.Models.User user in GetUsers())
             {

                 Response.Write(String.Format(@"
                         <div class='item'>
                              <table><tr><td>
                             <h3>{0}</h3>
                             {1}
                             ",
                     user.Name, user.Descr));

                 if (Session != null && Session["UserID"] != null)
                 {
                     int id = user.ID;
                     Response.Write(String.Format(@"</td>
                         <td class='pagelink'>
                             <form method='post' action='/Account/DisplayPage'>
                             <input type='hidden' name='UserID' value="));
                     Response.Write(id.ToString());
                      Response.Write(String.Format(@"/>
                             <input style='padding: .5em .4em .5em .4em; color: floralwhite; background-color:#ffb841; border: none;' type='submit' value='Перейти на страницу' />
                             </form>
                             </td>"));
                 }
                 Response.Write(String.Format(@"
                         </td></table>
                         </div>"));
             }
             */

        %>
    </div>
    <div class="pager">
       <%
           /*
            for (int i = 1; i <= MaxPage; i++)
            {
                string path = RouteTable.Routes.GetVirtualPath(null, null,
                    new RouteValueDictionary() { { "page", i } }).VirtualPath;
                Response.Write(
                    String.Format("<a href='{0}' {1}>{2}</a>",
                        path, i == CurrentPage ? "class='selected'" : "", i));
            }
            */
        %>
    </div>
        </div>
    </div>

</body>
</html>
   

