using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Newtonsoft.Json;
using PhoneBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ContactsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("MoonLightConn"));
            var dbList = dbClient.GetDatabase("MoonLight").GetCollection<Contacts>("Contacts").AsQueryable();

            return new JsonResult(dbList);
        }


        [HttpPost]
        public JsonResult Post(Contacts contacts)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("MoonLightConn"));
            int lastContactId = dbClient.GetDatabase("MoonLight").GetCollection<Contacts>("Contacts").AsQueryable().Count();
            contacts.UUID = lastContactId + 1;

            dbClient.GetDatabase("MoonLight").GetCollection<Contacts>("Contacts").InsertOne(contacts);


            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Contacts contacts)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("MoonLightConn"));
            var filter = Builders<Contacts>.Filter.Eq("UUID", contacts.UUID);

           
               var updateName = Builders<Contacts>.Update.Set("Name", contacts.Name);
            //var updateSurname = Builders<Contacts>.Update.Set("Surname", contacts.Surname);
            var jsonData = JsonConvert.SerializeObject(contacts);
            dbClient.GetDatabase("MoonLight").GetCollection<Contacts>("Contacts").UpdateMany(filter, jsonData);
            //dbClient.GetDatabase("MoonLight").GetCollection<Contacts>("Contacts").UpdateOne(filter, updateName);


            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("MoonLightConn"));
            var filter = Builders<Contacts>.Filter.Eq("UUID", id);

            dbClient.GetDatabase("MoonLight").GetCollection<Contacts>("Contacts").DeleteOne(filter);

            return new JsonResult("Deleted Successfully");
        }
    }
}
