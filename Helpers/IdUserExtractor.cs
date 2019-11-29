using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace SGFBackend.Helpers
{
    public class IdUserExtractor
    {
        private int idUser;
        public int IdUser { get => idUser; }

        public IdUserExtractor(IHttpContextAccessor http)
        {
            idUser = int.Parse(http.HttpContext.User.FindFirst(ClaimTypes.Name).Value);
        }
    }
}
