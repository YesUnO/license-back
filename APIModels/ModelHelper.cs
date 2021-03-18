using license_back.DB.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace license_back.APIModels
{
    public static class ModelHelper
    {
        public static License ToApiModel(Licence licence)
        {
            return new License
            {
                Id = licence.Id,
                Name = licence.Name,
                ProductCount = licence.ProductCount
            };
        }
    }
}
