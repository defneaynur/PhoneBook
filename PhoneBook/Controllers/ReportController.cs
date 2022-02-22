using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using PhoneBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : Controller
    {

        private readonly IConfiguration _configuration;
        public ReportController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public JsonResult Get(string Location)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("MoonLightConn"));
            var contacts = dbClient.GetDatabase("MoonLight").GetCollection<Contacts>("Contacts");
            var contactInformation = dbClient.GetDatabase("MoonLight").GetCollection<ContactInformation>("ContactInformation");

            int phoneCount = (from c in contacts.AsQueryable()
                              join i in contactInformation.AsQueryable()
                              on c.UUID equals i.ContactUUID
                              where i.Location == "İstanbul"
                              //into joinedContactInformation   
                              select i.PhoneNumber).Count();
            int contCount = (from c in contacts.AsQueryable()
                             join i in contactInformation.AsQueryable()
                             on c.UUID equals i.ContactUUID 
                             where i.Location == "İstanbul"
                             //into joinedContactInformation   
                             select c.Name).Count();

            var reportResult = (from c in contacts.AsQueryable()
                                join i in contactInformation.AsQueryable()
                                on c.UUID equals i.ContactUUID
                                where i.Location == "İstanbul"
                                select new Report
                                {
                                    UUID = c.UUID,
                                    Status = "Tamamlandı",
                                    SysTar = DateTime.Now.ToLongDateString(),
                                    Location = i.Location,
                                    LocationContactCount = contCount,
                                    LocationPhoneCount = phoneCount

                                }).AsQueryable();


            return new JsonResult(reportResult);


        }

    }
}
