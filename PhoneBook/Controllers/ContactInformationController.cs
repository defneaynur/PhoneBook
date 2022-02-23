using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
    public class ContactInformationController : Controller
    {
        private readonly IConfiguration _configuration;
        DbContext db;
        public ContactInformationController(IConfiguration configuration)
        {
            _configuration = configuration;
            db = new DbContext(_configuration);
        }

        /// <summary>
        /// ContactInformation tablosunu listeler
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult Get()
        {
            var dbList = db.DbClient().GetDatabase("MoonLight").GetCollection<ContactInformation>("ContactInformation").AsQueryable();

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
            int lastInfoId = db.DbClient().GetDatabase("MoonLight").GetCollection<ContactInformation>("ContactInformation").AsQueryable().Count();
            ContactInformation.InfoId = lastInfoId + 1;

            db.DbClient().GetDatabase("MoonLight").GetCollection<ContactInformation>("ContactInformation").InsertOne(ContactInformation);

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
            var filter = Builders<ContactInformation>.Filter.Eq("InfoId", ContactInfo.InfoId);

            //var jsonData = JsonConvert.SerializeObject(contacts);

            var updateContactInfo = Builders<ContactInformation>.Update.Set("PhoneNumber", ContactInfo.PhoneNumber)
                                                                        .Set("Email", ContactInfo.Email)
                                                                        .Set("Location", ContactInfo.Location);
            db.DbClient().GetDatabase("MoonLight").GetCollection<ContactInformation>("ContactInformation").UpdateOne(filter, updateContactInfo);

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
            var filter = Builders<ContactInformation>.Filter.Eq("ContactUUID", id);

            db.DbClient().GetDatabase("MoonLight").GetCollection<ContactInformation>("ContactInformation").DeleteOne(filter);

            return new JsonResult("Deleted Successfully");
        }
    }
}
