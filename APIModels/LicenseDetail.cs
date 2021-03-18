using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace license_back.APIModels
{
    public class LicenseDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Salt { get; set; }
        public string Hash { get; set; }
        public List<Product> Products { get; set; } 
    }
}
