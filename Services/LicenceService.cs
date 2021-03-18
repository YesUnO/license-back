using license_back.DB.Entity;
using license_back.DB.Data;
using license_back.Services.ServiceInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using license_back.APIModels;
using System.Xml.Linq;

namespace license_back.Services
{
    public class LicenceService : ILicenceService
    {
        private LicenceContext _context;
        public LicenceService(LicenceContext licenseContext)
        {
            _context = licenseContext;
        }

        public async IAsyncEnumerable<License> GetAllLicences()
        {
            var licencesFromDb = await _context.License.ToListAsync();

            foreach (var licence in licencesFromDb)
            {
                yield return ModelHelper.ToApiModel(licence);
            }
        }

        public LicenseDetail GetLicenseDetail(int id)
        {
            var licence = _context.License.Find(id);
            return GetLicenceDetailFromXml(licence);

        }

        public void SaveLicense(CreateLicenceRequest model)
        {
            Licence licence = new Licence
            {
                Name = model.Name,
                Xml = model.Xml,
                ProductCount = GetProductCountFromXml(model.Xml)
            };

            _context.Add(licence);
            _context.SaveChanges();
        }

        private int GetProductCountFromXml(string xml)
        {
            XDocument doc = XDocument.Parse(xml);
            return doc.Descendants("Product").Count();
        }

        private LicenseDetail GetLicenceDetailFromXml(Licence licence)
        {
            XDocument doc = XDocument.Parse(licence.Xml);
            string hash = doc.Descendants("Hash").FirstOrDefault().Value;
            string salt = doc.Descendants("Salt").FirstOrDefault().Value;
            IEnumerable<Product> productList = ParseProducts(doc);
            return new LicenseDetail
            {
                Id = licence.Id,
                Name = licence.Name,
                Products = productList.ToList(),
                Hash = hash,
                Salt = salt
            };
        }

        private IEnumerable<Product> ParseProducts(XDocument doc)
        {
            string version = doc.Descendants("License").Select(x => (string)x.Attribute("version")).FirstOrDefault();

            switch (version)
            {
                case "1.0":
                    return ParseVersionA(doc);
                case "1.1":
                    return ParseVersionB(doc);
                case "2.0":
                    return ParseVersionC(doc);
                default:
                    return Enumerable.Empty<Product>();
            }
        }

        private IEnumerable<Product> ParseVersionA(XDocument doc)
        {
            return doc.Descendants("Product").Select(x => new Product { Id = (int)x.Attribute("id"), Name = (string)x.Attribute("name") });
        }

        private IEnumerable<Product> ParseVersionB(XDocument doc)
        {
            return doc.Descendants("Product").Select(x => new Product { Id = (int)x.Attribute("id"), Name = x.Value });
        }

        private IEnumerable<Product> ParseVersionC(XDocument doc)
        {
            return doc.Descendants("Product").Select(x => new Product { Id = int.Parse( x.Descendants("id").FirstOrDefault().Value), Name = x.Descendants("name").FirstOrDefault().Value });
        }
    }
}
