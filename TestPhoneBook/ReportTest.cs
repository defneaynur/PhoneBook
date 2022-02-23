using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PhoneBook.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace TestPhoneBook
{
    public class ReportTest
    {
        ReportController report;
        public ReportTest()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
              .SetBasePath(AppContext.BaseDirectory)
              .AddJsonFile("appsettings.json")
              .Build();
            report = new ReportController(configuration);
        }

        [Fact]
        public void TestGet()
        {
            var actionResult = report.Get("");

            Assert.IsType<JsonResult>(actionResult);
        }
    }
}
