using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace license_back.APIModels
{
    public class CreateLicenceRequest
    {
        public string Name { get; set; }
        public string Xml { get; set; }
    }
}
