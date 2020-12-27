using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _3dBay2.Models.Repository;
using _3dBay2.Models;
using HtmlAgilityPack;

namespace _3dBay2.Pages
{
    public partial class News : System.Web.UI.Page
    {
        private Repository repository = new Repository();
        private int pageSize = 12;

        protected List<string> ParseNewsHeaders()
        {
            List<string> res = new List<string>();
            string url = "https://3dtoday.ru/";
            var web = new HtmlAgilityPack.HtmlWeb();
            HtmlDocument doc = web.Load(url);
            var HeaderlNodes = doc.DocumentNode.SelectNodes("(//h2[contains(@class,'post_list_item_title')]//a)");
            foreach (var n in HeaderlNodes)
            {
                string link = n.Attributes["href"].Value;
                res.Add(n.InnerText);
            }
            return res;
        }
        protected List<string> ParseNewsLinks()
        {
            List<string> res = new List<string>();
            string url = "https://3dtoday.ru/";
            var web = new HtmlAgilityPack.HtmlWeb();
            HtmlDocument doc = web.Load(url);
            var HeaderlNodes = doc.DocumentNode.SelectNodes("(//h2[contains(@class,'post_list_item_title')]//a)");
            foreach (var n in HeaderlNodes)
            {
                
                res.Add(n.Attributes["href"].Value);
            }
            return res;
        }
        protected int CurrentPage
        {
            get
            {
                int page;
                page = GetPageFromRequest();
                return page > MaxPage ? MaxPage : page;
            }
        }
        private int GetPageFromRequest()
        {
            int page;
            string reqValue = (string)RouteData.Values["page"] ??
                Request.QueryString["page"];
            return reqValue != null && int.TryParse(reqValue, out page) ? page : 1;
        }
        protected int MaxPage
        {
            get
            {
                int prodCount = FilterUsers().Count();
                return (int)Math.Ceiling((decimal)prodCount / pageSize);
            }
        }
        private IEnumerable<User> FilterUsers()
        {


            IEnumerable<User> users = repository.Users;
            string currentCategory = (string)RouteData.Values["category"] ??
                Request.QueryString["category"];
            if (currentCategory == "maker")
            {
                return currentCategory == null ? users :
                    users.Where(p => p.IfMaker == true);
            }
            else
            {
                return currentCategory == null ? users :
                    users.Where(p => p.IfCustomer == true);
            }



        }
        protected IEnumerable<User> GetUsers()
        {
            return FilterUsers()
                .OrderBy(g => g.ID)
                .Skip((CurrentPage - 1) * pageSize)
                .Take(pageSize);
        }
        private IEnumerable<Order> FilterOrders()
        {


            IEnumerable<Order> users = repository.Orders;
            string currentCategory = (string)RouteData.Values["category"] ??
                Request.QueryString["category"];
            /* if (currentCategory == "maker")
             {
                 return currentCategory == null ? users :
                     users.Where(p => p.IfMaker == true);
             }
             else
             {
                 return currentCategory == null ? users :
                     users.Where(p => p.IfCustomer == true);
             }
             */
            return null; 

        }
        protected IEnumerable<Order> GetOrders()
        {
            return repository.Orders.OrderBy(g => g.OrderId)
                .Skip((CurrentPage - 1) * pageSize)
                .Take(pageSize);
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}