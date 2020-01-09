using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using _3dBay2.Models;
using _3dBay2.Models.Repository;
namespace _3dBay2.Controllers
{
    public class AccountController : Controller
    {
        private static Repository repository = new Repository();
        private IEnumerable<User> users = repository.Users;
        public AccountController(IEnumerable<User> users)
        {
            this.users = users;
        }
        public AccountController()
        {
            
        }
        public string Encrypt (string p)
        {
            byte[] tmpSource;
            byte[] tmpHash;
            tmpSource = ASCIIEncoding.ASCII.GetBytes(p);
            tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);
            string finaliized = Convert.ToBase64String(tmpHash);
            return finaliized;
        }
        [HttpPost]
        public ActionResult Login()
        {
            User guest = new User();
            NameValueCollection coll;
            coll = Request.Form;
            string pwd = Encrypt(coll["Password"]);
            guest.Email = coll["Email"];
            guest.Password = pwd;
            if (ModelState.IsValid)
            {
                if (repository.IsUserExist(guest.Email))
                {
                    User target;
                    target = repository.GetUserByEmail(guest.Email);
                    if (target==null)
                    {
                        target= repository.GetUserByName(guest.Email);
                    }
                    
                    if (target.Password==guest.Password)
                    {
                        User dummy = new User();
                        dummy.Name = target.Name;
                        Session["UserId"] = target.ID;
                        return RedirectToAction("Complete", dummy);
                    }
                    else
                    {
                        return RedirectToAction("WrongPassword");
                    }
                    
                }
                
                return RedirectToAction("NotExist");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Register()
        {
            User guest = new User();
            NameValueCollection coll;
            coll = Request.Form;
            string pwd = Encrypt(coll["Password"]);
            bool fuckup = false;
            ViewBag.Message = "Registration failed. Problems occured:";
            bool ifmaker = false;
            bool ifcustomer = false;
            if (coll["IfMaker"] == "on")
                ifmaker = true;
            if (coll["IfCustomer"] == "on")
                ifcustomer = true;
            if (!Regex.IsMatch(coll["Email"],
              @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
              @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$",
              RegexOptions.IgnoreCase))
            {
                ViewBag.Message += "\nEmail is not valid";
                fuckup = true;
            }
            if (coll["Name"].Length<3 || coll["Name"].Length > 25)
            {
                ViewBag.Message += "\nUsername must be between 3 and 25 units long";
                fuckup = true;
            }
            if (coll["Password"].Length < 6)
            {
                ViewBag.Message += "\nPassword must contain 6 or more units";
                fuckup = true;
            }
            if (repository.IsUserExist(coll["Email"]))
            {
                ViewBag.Message += "\nUser with such email aready exists";
                fuckup = true;
            }
            if (repository.IsUserExist(coll["Name"]))
            {
                ViewBag.Message += "\nUser with such username aready exists";
                fuckup = true;
            }
            if (!ifmaker&&!ifcustomer)
            {
                ViewBag.Message += "\nYour role is not selected. At least one role needs to be applied";
                fuckup = true;
            }
            if (!fuckup)
            {
                guest.Email = coll["Email"];
                guest.Password = pwd;
                guest.Name = coll["Name"];
                guest.IfMaker = ifmaker;
                guest.IfCustomer = ifcustomer;
                repository.CreateUser(guest);
                User dummy = new User();
                dummy.Name = guest.Name;
                Session["UserId"] = guest.ID;
                return RedirectToAction("RegComplete",dummy);
            }
            else
            {
                ViewBag.Message += "\nPlease, try again";
                return View();
            }
            
        }
        [HttpPost]
        public ActionResult ChangePassword()
        {
            User guest = new User();
            NameValueCollection coll;
            coll = Request.Form;
            string pwd = Encrypt(coll["Password"]);
            bool fuckup = false;
            if (coll["Password"].Length < 6)
            {
                ViewBag.Message += "\nNew password must contain 6 or more units";
                fuckup = true;
            }
            if (!fuckup)
            {
                repository.EditPassword(guest);
                return RedirectToAction("PassChangeSuccess");
            }
            else
            {
                ViewBag.Message += "\nPlease, try again";
                return View();
            }
            
        }
        public ActionResult LogOut()
        {
            Session.Clear();
            return View();
        }
        public ActionResult MyPage()
        {

            int id = (int)Session["UserId"];
            User user = repository.GetUserByID(id);
            User dummy = new User();
            dummy.ID = user.ID;
            return RedirectToAction("DisplayPage",dummy);
        }
        public ActionResult DisplayPage(User dummy)
        {
            if (dummy.ID!=0)
            {
                User user = repository.GetUserByID(dummy.ID);
                ViewBag.User = user;
                return View();
            }
            else
            {
                User guest = new User();
                NameValueCollection coll;
                coll = Request.Form;
                string id = coll["UserID"];
                if (id==null)
                {
                    return RedirectToAction("Index", "Home");
                }
                id = id.TrimEnd(new char[] { '/' });
                User user = repository.GetUserByID(Convert.ToInt32(id));
                ViewBag.User = user;
                return View();
            }
           
        }
        public ActionResult RegComplete(User user)
        {
            
            ViewBag.Message = "You are now registered, " + user.Name + " ! You may log into your account on main page now";
            return View();
        }
        public ActionResult PassChangeSuccess()
        {
            ViewBag.Message = "Password successfully updated";
            return View();
        }
        public ActionResult Complete(User user)
        {
            ViewBag.Message = "Welcome home, " + user.Name+" !";
            return View();
        }
        public ActionResult NotExist()
        {
            return View();
        }
        public ActionResult WrongPassword()
        {
            return View();
        }
    }
}