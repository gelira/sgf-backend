using System.Net.Http;
using System.Net.Http.Headers;

namespace SGFBackend.Services
{
    public class TokenService
    {
        private string token = null;
        private static TokenService instance = null;

        public string Token 
        {
            get => token;
            set => token = value; 
        }

        private TokenService() { }

        public static TokenService GetInstance()
        {
            if (instance == null)
            {
                instance = new TokenService();
            }
            return instance;
        }

        public HttpClient AddToken(HttpClient client)
        {
            if (token != null)
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", Token);
            }
            return client;
        }
    }
}