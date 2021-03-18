using license_back.APIModels;
using license_back.DB.Entity;
using license_back.Services.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace license_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LicenceController : ControllerBase
    {
        private ILicenceService _licenseService;
        public LicenceController(ILicenceService licenseService)
        {
            _licenseService = licenseService;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> SaveLicense(CreateLicenceRequest model)
        {
            _licenseService.SaveLicense(model);
            return Ok();
        }

        [Authorize]
        [HttpGet]
        public IAsyncEnumerable<License> GetLicenses()
        {
            var licences =  _licenseService.GetAllLicences();
            return licences;
        }

        [Authorize]
        [Route("detail/{id}")]
        [HttpGet]
        public async Task<ActionResult> GetLicenseDetail(int id)
        {
            var licence = _licenseService.GetLicenseDetail(id);
            return Ok(licence);
        }
    }
}
