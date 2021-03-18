using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace license_back.APIModels
{
    public class CreateUserRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
