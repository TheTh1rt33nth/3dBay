using System;
using System.Collections.Generic;
using System.Linq;
using GenericStl;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using _3dBay2.Models;
using _3dBay2.Models.Repository;
namespace _3dBay2.Controllers
{
    public class OrdersController : Controller
    {
        private static Repository repository = new Repository();
        [HttpPost]
        public ActionResult CreateOrder(HttpPostedFileBase upload)
        {
            ViewBag.Message = "Order creation failed. Problems occured:";
            string filePath;
            NameValueCollection coll;
            coll = Request.Form;
            Order order = new Order();
            if (upload != null)
            {
                bool fuckup = false;
                if (repository.IsOrderExist(coll["Name"]))
                {
                    fuckup = true;
                    ViewBag.Message += "/nOrder with such name already exists";
                }
                if (!fuckup)
                {
                    string fileName = System.IO.Path.GetFileName(upload.FileName);
                    filePath = Server.MapPath("~/Files/" + (fileName+ Session["UserId"]));
                    upload.SaveAs(filePath);
                    order.CustomerId = (int)Session["UserId"];
                    order.Description = coll["Description"];
                    order.Name = coll["Name"];
                    order.Price = coll["Price"];
                    order.Quantity = coll["Quantity"];
                    order.FileLink = filePath;
                    order.DateToMake = Convert.ToDateTime(coll["DateToMake"]);
                    repository.CreateOrder(order);
                    Order dummy = new Order();
                    dummy.Name = order.OrderId.ToString();
                    return RedirectToAction("OrderCreated", dummy);
                }
                else
                {
                    ViewBag.Message += "\nPlease, try again";
                    return View();
                }
            }
            else
            {
                return View();
            }
            
        }
        [HttpPost]
        public ActionResult AcceptOrder()
        {
            NameValueCollection coll;
            coll = Request.Form;
            Order order = new Order();
            order = repository.GetOrderByID(Convert.ToInt32(coll["OrderId"]));
            order.MakerId = (int)Session["UserId"];
            repository.AddOrderMaker(order);
            ViewBag.Message += "Мэйкер " + repository.GetUserByID((int)Session["UserId"]).Name + " успешно назначен на заказ " + order.OrderId;
            return View();
        }
            public ActionResult OrderCreated(Order order)
        {
            ViewBag.Message = "Заказ номер " + order.Name + " успешно создан!";
            return View();
        }
    }
}