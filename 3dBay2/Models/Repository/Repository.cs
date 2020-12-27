using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;

namespace _3dBay2.Models.Repository
{
    public class Repository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<User> Users
        {
            get { return context.Users; }
        }

        public IEnumerable<Order> Orders
        {
            get { return context.Orders; }
        }
        public IEnumerable<Order> OrdersOfMaker(int id)
        {
            return context.Orders.Where(p => p.MakerId == id);
        }
        public IEnumerable<Order> OrdersOfCustomer(int id)
        {
            return context.Orders.Where(p => p.CustomerId == id);
        }

        public IEnumerable<Review> Reviews
        {
            get { return context.Reviews; }
        }
        public IEnumerable<Review> ReviewsOfMaker(int id)
        {
            return context.Reviews.Where(p => p.MakerId == id);
        }
        public IEnumerable<Review> ReviewsOfCustomer(int id)
        {
            return context.Reviews.Where(p => p.CustomerId == id);
        }
        public void UpdateUserRaing(User user)
        {
            User target = GetUserByID(user.ID);
            target.CustomerRating = user.CustomerRating;
            target.MakerRating = user.MakerRating;
            context.SaveChanges();
        }
        public void CreateUser(User newUser)
        {
            context.Users.Add(newUser);
        
            context.SaveChanges();
        }
        public void CreateReview(Review newReview)
        {
            if (IsReviewExist(newReview.OrderId))
            {
                Review target = GetReviewByID(newReview.OrderId);
                target.CustomerRating = newReview.CustomerRating;
                target.MakerRating = newReview.MakerRating;
                target.Description = newReview.Description;
                context.SaveChanges();
            }
            else
            {
                context.Reviews.Add(newReview);
                context.SaveChanges();
            }
        }
        public void CreateOrder(Order newOrder)
        {
            context.Orders.Add(newOrder);
            context.SaveChanges();
        }
        public void EditPassword(User user)
        {
            User target = GetUserByEmail(user.Email);
            target.Password = user.Password;
            context.SaveChanges();
        }
        public void AddOrderMaker(Order order)
        {
            Order target = GetOrderByID(order.OrderId);
            target.MakerId = order.MakerId;
            context.SaveChanges();
        }
        public Order GetOrderByID(int id) => context.Orders.FirstOrDefault(p => p.OrderId == id);
        public Review GetReviewByID(int id) => context.Reviews.FirstOrDefault(p => p.OrderId == id);
        public Order GetOrderByName(string name) => context.Orders.FirstOrDefault(p => p.Name == name);
        public User GetUserByEmail(string email) => context.Users.FirstOrDefault(p => p.Email == email);
        public User GetUserByName(string name) => context.Users.FirstOrDefault(p => p.Name == name);
        public User GetUserByID(int id) => context.Users.FirstOrDefault(p => p.ID == id);
        public bool IsUserExist(string email) => context.Users.FirstOrDefault(p => (p.Email == email)||(p.Name==email)) != null;
        public bool IsOrderExist(string name) => context.Users.FirstOrDefault(p => p.Name == name) != null;
        public bool IsReviewExist(int id) => context.Reviews.FirstOrDefault(p => p.OrderId == id) != null;
        public void DeleteUser(int id)
        {
            var user = context.Users.FirstOrDefault(u => u.ID == id);
            if (user != null)
                context.Users.Remove(user);
            context.SaveChanges();
        }

    }
}