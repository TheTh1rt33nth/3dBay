using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3dBay2.Models;
namespace _3dBay2.Interfaces
{
    interface IUsers
    {
        IEnumerable<User> Users { get; set; }
        User GetByEmail(string emal);
    }
}
