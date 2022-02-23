using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PhoneBook.Controllers;
using PhoneBook.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Xunit;

namespace TestPhoneBook
{
    public class ContactInformationTest
    {
        ContactInformationController contactInfo;
        public ContactInformationTest()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
              .SetBasePath(AppContext.BaseDirectory)
              .AddJsonFile("appsettings.json")
              .Build();
            contactInfo = new ContactInformationController(configuration);
        }
        [Fact]
        public void TestGet()
        {
            var actionResult = contactInfo.Get();

            Assert.IsType<JsonResult>(actionResult);
        }
        [Fact]
        public void TestPost()
        {

            var info = new ContactInformation()
            {
                ContactUUID = 4,
                PhoneNumber = "05553456785",
                // Email = "",
                Location = "Ankara"
            };

            var actionResult = contactInfo.Post(info);

            Assert.Equal("Added Successfully", actionResult.Value);
        }

        [Fact]
        public void TestPut()
        {

            var info = new ContactInformation()
            {
                InfoId=1,
                ContactUUID = 4,
                PhoneNumber = "05553456785",
               // Email = "",
                Location = "Ankara"
            };

            var actionResult = contactInfo.Put(info);

            Assert.Equal("Updated Successfully", actionResult.Value);
        }

        [Fact]
        public void TestDelete()
        {
            var info = new ContactInformation()
            {
                InfoId = 4
            };

            var actionResult = contactInfo.Delete(info.InfoId);

            Assert.Equal("Deleted Successfully", actionResult.Value);
        }
    }
}
