using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _3dBay2.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public bool IfMaker { get; set; }
        public bool IfCustomer { get; set; }
        public bool IfAdmin { get; set; }
        public string RealName { get; set; }
        public string Machine { get; set; }
        public string MetroSt { get; set; }
        public string Descr { get; set; }
        public int MakerRating { get; set; }
        public int CustomerRating { get; set; }
        public string Contacts { get; set; }

        public string Email { get; set; }
    }
}