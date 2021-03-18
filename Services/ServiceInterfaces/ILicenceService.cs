using license_back.APIModels;
using license_back.DB.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace license_back.Services.ServiceInterfaces
{
    public interface ILicenceService
    {
        IAsyncEnumerable<License> GetAllLicences();
        void SaveLicense(CreateLicenceRequest model);
        LicenseDetail GetLicenseDetail(int id);
    }
}
