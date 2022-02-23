﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Xunit;

namespace TestPhoneBook
{
    public class ContactInformationTest
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

            Assert.IsType<JsonResult>(actionResult);
        }
        [Fact]
        public void TestPost()
        {

            var info = new PhoneBook.Models.ContactInformation()
            {
                ContactUUID = 4,
                PhoneNumber = "05553456785",
                // Email = "",
                Location = "Ankara"
            };
           

            IConfigurationRoot configuration = new ConfigurationBuilder()
             .SetBasePath(AppContext.BaseDirectory)
             .AddJsonFile("appsettings.json")
             .Build();
            PhoneBook.Controllers.ContactInformationController information = new PhoneBook.Controllers.ContactInformationController(configuration);
            var actionResult = information.Post(info);


            Assert.Equal("Added Successfully", actionResult.Value);

        }

        [Fact]
        public void TestPut()
        {

            var info = new PhoneBook.Models.ContactInformation()
            {
                InfoId=1,
                ContactUUID = 4,
                PhoneNumber = "05553456785",
               // Email = "",
                Location = "Ankara"
            };


            IConfigurationRoot configuration = new ConfigurationBuilder()
             .SetBasePath(AppContext.BaseDirectory)
             .AddJsonFile("appsettings.json")
             .Build();
            PhoneBook.Controllers.ContactInformationController information = new PhoneBook.Controllers.ContactInformationController(configuration);
            var actionResult = information.Put(info);


            Assert.Equal("Updated Successfully", actionResult.Value);

        }

        [Fact]
        public void TestDelete()
        {

            var info = new PhoneBook.Models.ContactInformation()
            {
                InfoId = 4
            };


            IConfigurationRoot configuration = new ConfigurationBuilder()
             .SetBasePath(AppContext.BaseDirectory)
             .AddJsonFile("appsettings.json")
             .Build();
            PhoneBook.Controllers.ContactInformationController information = new PhoneBook.Controllers.ContactInformationController(configuration);
            var actionResult = information.Delete(info.InfoId);


            Assert.Equal("Deleted Successfully", actionResult.Value);

        }
    }
}
