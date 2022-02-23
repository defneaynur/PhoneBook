using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Newtonsoft.Json;
using PhoneBook.Models;
using PhoneBook.Models.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactWithContactInformationController : Controller
    {
        private readonly IConfiguration _configuration;
        DbContext db;
        public ContactWithContactInformationController(IConfiguration configuration)
        {
            _configuration = configuration;
            db = new DbContext(_configuration);
        }

        /// <summary>
        /// Kişi Bilgilerinin detaylarını getirir
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult Get()
        {
            var contacts = db.DbClient().GetDatabase("MoonLight").GetCollection<Contacts>("Contacts");
            var contactInformation = db.DbClient().GetDatabase("MoonLight").GetCollection<ContactInformation>("ContactInformation");

            var result = (from c in contacts.AsQueryable()
                          join i in contactInformation.AsQueryable()
                          on c.UUID equals i.ContactUUID
                          into joinedContactInformation
                          select new ContactWithContactInformation
                          {
                              UUID = c.UUID,
                              Name = c.Name,
                              Surname = c.Surname,
                              Firm = c.Firm,
                              ContInfo = joinedContactInformation
                          }).AsQueryable();
 

            return new JsonResult(result);
        }
    }
}
