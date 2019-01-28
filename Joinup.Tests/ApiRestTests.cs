using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Joinup.Common.Models;
using Joinup.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Joinup.Tests
{
    [TestClass]
    public class ApiRestTests
    {
        public const string url = "https://joinupapi.azurewebsites.net";
        public const string prefix = "/api";
        public const string plansController = "/Plans";
        public const string commentsController = "/Comments";
        public const string usersController = "/Users/GetUser";

        [TestMethod]
        public async Task DoLogin()
        {
            GetUserRequest getUserRequest = new GetUserRequest();
            getUserRequest.Email = "jasoljim92@gmail.com";

            var request = JsonConvert.SerializeObject( getUserRequest );
            var content = new StringContent( request, Encoding.UTF8, "application/json" );

            var client = new HttpClient();
            client.BaseAddress = new Uri( url );
            var requestUrl = $"{prefix}{usersController}";
            var response = await client.PostAsync( url, content );

            Assert.IsTrue( response.IsSuccessStatusCode );
        }
        [TestMethod]
        public async Task GetPlansAsync()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri( url );

            var requestUrl = $"{prefix}{plansController}";

            var response = await client.GetAsync( requestUrl );
            Assert.IsTrue( response.IsSuccessStatusCode );
        }
        [TestMethod]
        public async Task GetCommentsAsync()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri( url );

            var requestUrl = $"{prefix}{commentsController}";

            var response = await client.GetAsync( requestUrl );
            Assert.IsTrue( response.IsSuccessStatusCode );
        }
    }
}
