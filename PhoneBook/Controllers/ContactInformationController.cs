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

        /// <summary>
        /// ContactInformation tablosunu listeler
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult Get()
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("MoonLightConn"));
            var dbList = dbClient.GetDatabase("MoonLight").GetCollection<ContactInformation>("ContactInformation").AsQueryable();

            return new JsonResult(dbList);
        }


        /// <summary>
        /// ContactInformation tablosuna ekleme işlemi yapar
        /// </summary>
        /// <param name="ContactInformation">UUID, Phone, Email, Location</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Post(ContactInformation ContactInformation)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("MoonLightConn"));
            int lastInfoId = dbClient.GetDatabase("MoonLight").GetCollection<ContactInformation>("ContactInformation").AsQueryable().Count();
            ContactInformation.InfoId = lastInfoId + 1;

            dbClient.GetDatabase("MoonLight").GetCollection<ContactInformation>("ContactInformation").InsertOne(ContactInformation);


            return new JsonResult("Added Successfully");
        }

        /// <summary>
        /// ContactInformation tablosunu günceller
        /// </summary>
        /// <param name="ContactInfo">InfoId, UUID, Phone, Email, Location</param>
        /// <returns></returns>
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

        /// <summary>
        /// ContactInformation silme işlemi yapar
        /// </summary>
        /// <param name="id">InfoId</param>
        /// <returns></returns>
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
