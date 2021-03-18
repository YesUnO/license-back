using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace license_back.DB.Entity
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
