using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;
using _3dBay2.Models.Repository;

namespace _3dBay2.Controls
{
    public partial class CategoryList : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected IEnumerable<string> GetCategories()
        {
            return new Repository().Users.Where(p => p.IfMaker == true)
                .Select(p => p.MetroSt)
                .Distinct()
                .OrderBy(x => x);
        }

        protected string CreateHomeLinkHtml()
        {
            string path = RouteTable.Routes.GetVirtualPath(null, null).VirtualPath;
            path= RouteTable.Routes.GetVirtualPath(null, null,
                new RouteValueDictionary() { { "page", "1" } }).VirtualPath;
            return string.Format("<a href='{0}'>Все пользователи</a>", path);
        }
        protected string CreateOrdersLinkHtml()
        {
            string path = RouteTable.Routes.GetVirtualPath(null, null).VirtualPath;
            path = RouteTable.Routes.GetVirtualPath(null, null,
                new RouteValueDictionary() { { "page", "1" } }).VirtualPath;
            return string.Format("<a href='{0}'>Доска заказов</a>", "/Pages/OrderListing.aspx");
        }
        protected string CreateLinkHtml(string category, string name)
        {

            string selectedCategory = (string)Page.RouteData.Values["category"]
               ?? Request.QueryString["category"];

            string path = RouteTable.Routes.GetVirtualPath(null, null,
                new RouteValueDictionary() { { "category", category }, { "page", "1" } }).VirtualPath;
            return string.Format("<a href='{0}' {1}>{2}</a>",
               path, category == selectedCategory ? "class='selected'" : "", name);

        }
    }
}