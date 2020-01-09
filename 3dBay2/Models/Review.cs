using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3dBay2.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public int CustomerId { get; set; }
        public int MakerId { get; set; }
        public int OrderId { get; set; }
        public string FileLink { get; set; }
        public string Description { get; set; }
        public int MakerRating { get; set; }
        public int CustomerRating { get; set; }
    }
}