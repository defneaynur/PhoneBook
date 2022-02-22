using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
    public class ContactInformationController : Controller
    {
        private readonly IConfiguration _configuration;
        public ContactInformationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("MoonLightConn"));
            var dbList = dbClient.GetDatabase("MoonLight").GetCollection<ContactInformation>("ContactInformation").AsQueryable();

            return new JsonResult(dbList);
        }


        [HttpPost]
        public JsonResult Post(ContactInformation ContactInformation)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("MoonLightConn"));
            int lastInfoId = dbClient.GetDatabase("MoonLight").GetCollection<ContactInformation>("ContactInformation").AsQueryable().Count();
            ContactInformation.InfoId = lastInfoId + 1;

            dbClient.GetDatabase("MoonLight").GetCollection<ContactInformation>("ContactInformation").InsertOne(ContactInformation);


            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(ContactInformation ContactInfo)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("MoonLightConn"));
            var filter = Builders<ContactInformation>.Filter.Eq("InfoId", ContactInfo.InfoId);

            //var jsonData = JsonConvert.SerializeObject(contacts);

            var updateContactInfo = Builders<ContactInformation>.Update.Set("PhoneNumber", ContactInfo.PhoneNumber)
                                                                        .Set("Email", ContactInfo.Email)
                                                                        .Set("Location", ContactInfo.Location);
            dbClient.GetDatabase("MoonLight").GetCollection<ContactInformation>("ContactInformation").UpdateOne(filter, updateContactInfo);

            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("MoonLightConn"));
            var filter = Builders<ContactInformation>.Filter.Eq("InfoId", id);

            dbClient.GetDatabase("MoonLight").GetCollection<ContactInformation>("ContactInformation").DeleteOne(filter);

            return new JsonResult("Deleted Successfully");
        }
    }
}
