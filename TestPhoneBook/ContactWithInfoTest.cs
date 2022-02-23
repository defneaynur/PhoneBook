using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using PhoneBook.Controllers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestPhoneBook
{
    public class ContactWithInfoTest
    {
        
        [Fact]
        public void TestGetAsync()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
           .SetBasePath(AppContext.BaseDirectory)
           .AddJsonFile("appsettings.json")
           .Build();
            PhoneBook.Controllers.ContactWithContactInformationController contactInfo = new PhoneBook.Controllers.ContactWithContactInformationController(configuration);

            var actionResult = contactInfo.Get();

            Assert.IsType<JsonResult>(actionResult);

        }
    }
}
