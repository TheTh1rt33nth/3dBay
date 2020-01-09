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
    public class ReviewsController : Controller
    {
        private static Repository repository = new Repository();
        public void RecalculateRating(int mark, User user, string type)
        {
            IEnumerable<Review> reviews = repository.Reviews;
            int n = 1;
            double rat = 0;
            foreach(Review r in reviews)
            {
                if (type=="Customer")
                {
                    if(r.CustomerId==user.ID)
                    {
                        n += 1;
                        rat += r.CustomerRating;
                    }
                }
                else
                {
                    if (r.MakerId == user.ID)
                    {
                        n += 1;
                        rat += r.MakerRating;
                    }
                }
                
            }
            rat += mark;
            rat = rat / n;
            int rating = Convert.ToInt32(rat);
            if (type == "Customer")
            {
                user.CustomerRating = rating; 
            }
            else
            {
                user.MakerRating = rating;
            }
            repository.UpdateUserRaing(user);
        }
        public ActionResult CreateReview()
        {
            NameValueCollection coll;
            coll = Request.Form;
            Session["OrderId"] = coll["OrderId"];
            Session["IfMaker"] = coll["IfMaker"];
            return Redirect("/Pages/ReviewCreation.aspx");
        }
        public ActionResult ValidateReview()
        {

            NameValueCollection coll;
            coll = Request.Form;
            Review review = new Review();
            string id = Session["OrderId"].ToString();
            id.Trim(new char[] { '/' });

            if (repository.IsReviewExist(Convert.ToInt32(id)))
            {
                review = repository.GetReviewByID(Convert.ToInt32(id));
            }
            Order order = repository.GetOrderByID(Convert.ToInt32(id));
            review.MakerId = order.MakerId;
            review.CustomerId = order.CustomerId;
            review.OrderId = order.OrderId;
            string mark = coll["marks"];
            mark.Trim(new char[] { '/' });
            int m = Convert.ToInt32(mark);
            if (m>5)
            {
                m = 5;
            }
            if(m<1)
            {
                m = 1;
            }
            string user = "";
            if (Session["IfMaker"].ToString()=="true")
            {
                review.MakerRating = m;
                RecalculateRating(m, repository.GetUserByID(review.CustomerId),"Maker");
                user = "Customer comment: ";
            }
            else
            {
                review.CustomerRating = m;
                user = "Maker comment: ";
                RecalculateRating(m, repository.GetUserByID(review.MakerId),"Customer");
            }
            
            review.Description += user + coll["Description"];
            repository.CreateReview(review);
            ViewBag.Review = review;
                return View();

        }
    }
}