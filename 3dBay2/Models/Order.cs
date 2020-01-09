using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3dBay2.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int MakerId { get; set; }
        public string FileLink { get; set; }
        public string Quantity { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public DateTime DateToMake { get; set; }
        bool IfClosed { get; set; }
    }
}