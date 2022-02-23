using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
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
    public class ReportController : Controller
    {

        private readonly IConfiguration _configuration;
        DbContext db;
        public ReportController(IConfiguration configuration)
        {
            _configuration = configuration;
            db = new DbContext(_configuration);
        }

        [HttpGet("{Location}")]
        public JsonResult Get(string Location)
        {
            var contacts = db.DbClient().GetDatabase("MoonLight").GetCollection<Contacts>("Contacts");
            var contactInformation = db.DbClient().GetDatabase("MoonLight").GetCollection<ContactInformation>("ContactInformation");

            var phoneCount = (from c in contacts.AsQueryable()
                              join i in contactInformation.AsQueryable()
                              on c.UUID equals i.ContactUUID
                              where i.Location == Location
                              //into joinedContactInformation   
                              select i.PhoneNumber).Count();
            var contCount = (from c in contacts.AsQueryable()
                             join i in contactInformation.AsQueryable()
                             on c.UUID equals i.ContactUUID 
                             where i.Location == Location
                             //into joinedContactInformation   
                             select c.Name).Count();

            var reportResult = new Report
            {
                UUID = 1,
                Status = "Tamamlandı",
                SysTar = DateTime.Now.ToLongDateString(),
                Location = Location,
                LocationContactCount = contCount,
                LocationPhoneCount = phoneCount

            };

            //var reportResult = (from c in contacts.AsQueryable()
            //                    join i in contactInformation.AsQueryable()
            //                    on c.UUID equals i.ContactUUID 
            //                    where i.Location == "İstanbul"
            //                    select new Report
            //                    {
            //                        UUID = c.UUID,
            //                        Status = "Tamamlandı",
            //                        SysTar = DateTime.Now.ToLongDateString(),
            //                        Location = i.Location,
            //                        LocationContactCount = contCount,
            //                        LocationPhoneCount = phoneCount

            //                    }).AsQueryable();


            return new JsonResult(reportResult);


        }

    }
}
