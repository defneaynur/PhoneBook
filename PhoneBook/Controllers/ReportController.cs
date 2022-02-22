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
        //[HttpGet]
        //public JsonResult Get(string Location)
        //{
        //    MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("MoonLightConn"));
        //    //var dbList = dbClient.GetDatabase("MoonLight").GetCollection<Contacts>("Contacts").AsQueryable();
        //    var contacts = dbClient.GetDatabase("MoonLight").GetCollection<Contacts>("Contacts");
        //    var contactInformation = dbClient.GetDatabase("MoonLight").GetCollection<ContactInformation>("ContactInformation");

        //    //var result = from c in contacts.AsQueryable()
        //    //             join i in contactInformation.AsQueryable()
        //    //             on c.UUID equals i.ContactUUID
        //    //             into joinedReport                     
        //    //             select new Report
        //    //             {
        //    //                 Location =c.Id ,
        //    //                 LocationContactCount=c.,
        //    //                 inventory_docs = joinedInventory
        //    //             });
        //    //return new JsonResult(dbList);
            

        //}
       
    }
}
