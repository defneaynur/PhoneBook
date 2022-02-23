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
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ContactsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Contacts tablosunu Listeler
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult Get()
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("MoonLightConn"));
            var dbList = dbClient.GetDatabase("MoonLight").GetCollection<Contacts>("Contacts").AsQueryable();

            return new JsonResult(dbList);
        }

        /// <summary>
        ///  Contacts tablosuna ekleme yapar
        /// </summary>
        /// <param name="contacts">Name, Surname, Firm</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Post(Contacts contacts)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("MoonLightConn"));
            int lastContactId = dbClient.GetDatabase("MoonLight").GetCollection<Contacts>("Contacts").AsQueryable().Count();
            contacts.UUID = lastContactId + 1;

            dbClient.GetDatabase("MoonLight").GetCollection<Contacts>("Contacts").InsertOne(contacts);

            return new JsonResult("Added Successfully");
        }

        /// <summary>
        ///  Contacts tablosunu gönderilen değerlere göre günceller
        /// </summary>
        /// <param name="contacts">Name, Surname, Firm</param>
        /// <returns></returns>
        [HttpPut]
        public JsonResult Put(Contacts contacts)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("MoonLightConn"));
            var filter = Builders<Contacts>.Filter.Eq("UUID", contacts.UUID);

            //var jsonData = JsonConvert.SerializeObject(contacts);
            
                var updateContact = Builders<Contacts>.Update.Set("Name", contacts.Name)
                                                           .Set("Surname",contacts.Surname)
                                                           .Set("Firm",contacts.Firm);
                dbClient.GetDatabase("MoonLight").GetCollection<Contacts>("Contacts").UpdateMany(filter, updateContact);
           
            return new JsonResult("Updated Successfully");
        }

        /// <summary>
        /// Contacts tablosundan idye göre silme işlemi gerçekleştirir
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("MoonLightConn"));
            var filter = Builders<Contacts>.Filter.Eq("UUID", id);
            var filterInfo = Builders<Contacts>.Filter.Eq("ContactUUID", id);

            dbClient.GetDatabase("MoonLight").GetCollection<Contacts>("Contacts").DeleteOne(filter);
            dbClient.GetDatabase("MoonLight").GetCollection<Contacts>("ContactInformation").DeleteOne(filterInfo);

            return new JsonResult("Deleted Successfully");
        }
    }
}
