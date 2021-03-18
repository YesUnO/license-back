using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace license_back.DB.Entity
{
    public class Licence
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Xml { get; set; }
        public int ProductCount { get; set; }
    }
}
