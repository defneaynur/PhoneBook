using System;
using Xunit;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using PhoneBook;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace TestPhoneBook
{
    public class ContactsTest
    {
        [Fact]
        public void TestGet()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
             .SetBasePath(AppContext.BaseDirectory)
             .AddJsonFile("appsettings.json")
             .Build();
            PhoneBook.Controllers.ContactsController contacts = new PhoneBook.Controllers.ContactsController(configuration);
            var actionResult = contacts.Get();


            Assert.Equal(HttpStatusCode.OK, actionResult.Value);

        }
        [Fact]
        public void TestPost()
        {

            var c = new PhoneBook.Models.Contacts()
            {
                Name = "Asaf",
                Surname = "Ekrem",
                Firm = "Can Holding"
            };
            #region Birtakým denemeler
            //var client = new TestClientProvider().client;

            //var myContent = JsonConvert.SerializeObject(c);
            //var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            //var byteContent = new ByteArrayContent(buffer);
            //byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            // POST using the BSON formatter.


            //MongoClient dbClient = new MongoClient(configuration.GetConnectionString("MoonLightConn"));
            //// var result = client.PostAsync("api/Contacts", byteContent).Result.StatusCode;
            #endregion

            IConfigurationRoot configuration = new ConfigurationBuilder()
             .SetBasePath(AppContext.BaseDirectory)
             .AddJsonFile("appsettings.json")
             .Build();
            PhoneBook.Controllers.ContactsController contacts = new PhoneBook.Controllers.ContactsController(configuration);
            var actionResult = contacts.Post(c);

            
            Assert.Equal("Added Successfully", actionResult.Value);

        }

        [Fact]
        public void TestPut()
        {

            var c = new PhoneBook.Models.Contacts()
            {
                UUID=5,
                Name = "Kemalettin",
                Surname = "Tarif Gerekmez",
                Firm = "Kemo Tech"
            };
            

            IConfigurationRoot configuration = new ConfigurationBuilder()
             .SetBasePath(AppContext.BaseDirectory)
             .AddJsonFile("appsettings.json")
             .Build();
            PhoneBook.Controllers.ContactsController contacts = new PhoneBook.Controllers.ContactsController(configuration);
            var actionResult = contacts.Put(c);


            Assert.Equal("Updated Successfully", actionResult.Value);

        }

        [Fact]
        public void TestDelete()
        {

            var c = new PhoneBook.Models.Contacts()
            {
                UUID = 5               
            };


            IConfigurationRoot configuration = new ConfigurationBuilder()
             .SetBasePath(AppContext.BaseDirectory)
             .AddJsonFile("appsettings.json")
             .Build();
            PhoneBook.Controllers.ContactsController contacts = new PhoneBook.Controllers.ContactsController(configuration);
            var actionResult = contacts.Delete(c.UUID);


            Assert.Equal("Deleted Successfully", actionResult.Value);

        }
    }
}
