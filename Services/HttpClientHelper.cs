using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace SGFBackend.Services
{
    public class HttpClientHelper
    {
        private HttpClient http;
        public HttpClient Client { get => http; }

        public HttpClientHelper()
        {
            http = new HttpClient();
            http.BaseAddress = new Uri("http://localhost:5000/");
            http.DefaultRequestHeaders.Accept.Clear();
            http.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}