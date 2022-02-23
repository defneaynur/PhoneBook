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
using PhoneBook.Controllers;
using System.Threading.Tasks;
using PhoneBook.Models;

namespace TestPhoneBook
{
    public class ContactsTest
    {
        ContactsController contacts;
        public ContactsTest()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
              .SetBasePath(AppContext.BaseDirectory)
              .AddJsonFile("appsettings.json")
              .Build();
            contacts = new ContactsController(configuration);
        }

        [Fact]
        public void TestGet()
        {
            var actionResult = contacts.Get();

            Assert.IsType<JsonResult>(actionResult);
        }

        [Fact]
        public void TestPost()
        {
            var c = new Contacts()
            {
                Name = "Asaf",
                Surname = "Ekrem",
                Firm = "Can Holding"
            };

            var actionResult = contacts.Post(c);

            Assert.Equal("Added Successfully", actionResult.Value);
        }

        [Fact]
        public void TestPut()
        {
            var c = new Contacts()
            {
                UUID = 5,
                Name = "Kemalettin",
                Surname = "Tarif Gerekmez",
                Firm = "Kemo Tech"
            };

            var actionResult = contacts.Put(c);

            Assert.Equal("Updated Successfully", actionResult.Value);
        }

        [Fact]
        public void TestDelete()
        {
            var c = new Contacts()
            {
                UUID = 5
            };

            var actionResult = contacts.Delete(c.UUID);

            Assert.Equal("Deleted Successfully", actionResult.Value);
        }
    }
}
