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
            int lastContactId = dbClient.GetDatabase("MoonLight").GetCollection<ContactInformation>("ContactInformation").AsQueryable().Count();
            ContactInformation.ContactUUID = lastContactId + 1;

            dbClient.GetDatabase("MoonLight").GetCollection<ContactInformation>("ContactInformation").InsertOne(ContactInformation);


            return new JsonResult("Added Successfully");
        }

        //[HttpPut]
        //public JsonResult Put(ContactInformation ContactInformation,string)
        //{
        //    MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("MoonLightConn"));
        //    var filter = Builders<ContactInformation>.Filter.Eq("UUID", ContactInformation.InfoId,"");
        //    var updateName = "";
        //    var updateSurname = "";
        //    var updateFirm = "";
        //    if (!string.IsNullOrEmpty(ContactInformation.Name))
        //    {
        //        updateName = Builders<ContactInformation>.Update.Set("Name", ContactInformation.Name).ToString();

        //    }
        //    if (!string.IsNullOrEmpty(ContactInformation.Surname))
        //    {
        //        updateSurname = Builders<ContactInformation>.Update.Set("Surname", ContactInformation.Surname).ToString();
        //    }
        //    if (!string.IsNullOrEmpty(ContactInformation.Firm))
        //    {
        //        updateFirm = Builders<ContactInformation>.Update.Set("Firm", ContactInformation.Firm).ToString();
        //    }
        //    dbClient.GetDatabase("MoonLight").GetCollection<ContactInformation>("ContactInformation").UpdateOne(filter, updateName);
        //    dbClient.GetDatabase("MoonLight").GetCollection<ContactInformation>("ContactInformation").UpdateOne(filter, updateSurname);
        //    dbClient.GetDatabase("MoonLight").GetCollection<ContactInformation>("ContactInformation").UpdateOne(filter, updateFirm);



        //    return new JsonResult("Updated Successfully");
        //}

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("MoonLightConn"));
            var filter = Builders<ContactInformation>.Filter.Eq("UUID", id);

            dbClient.GetDatabase("MoonLight").GetCollection<ContactInformation>("ContactInformation").DeleteOne(filter);

            return new JsonResult("Deleted Successfully");
        }
    }
}
