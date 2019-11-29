using System.ComponentModel.DataAnnotations;
using SGFBackend.Validation;

namespace SGFBackend.Models
{
    public class UserCreate
    {
        [Required, StringLength(100)]
        public string Nome { get; set; }

        [Required, StringLength(100), UsernameUnique]
        public string Username { get; set; }

        [Required, StringLength(100, MinimumLength = 5)]
        public string Password { get; set; }
    }
}