using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using PhoneBook;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace TestPhoneBook
{
    public class TestClientProvider
    {
        public HttpClient client { get; set; }
        public TestClientProvider()
        {
            var server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            client = server.CreateClient();
        }
    }
}
