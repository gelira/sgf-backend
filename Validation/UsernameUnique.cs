using System.ComponentModel.DataAnnotations;
using System.Linq;
using SGFBackend.Entities;

namespace SGFBackend.Validation
{
    public class UsernameUnique : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var username = value as string;
            if (username != null)
            {
                using (var context = new SgfContext())
                {
                    if (context.Users.Where(u => u.Username.Equals(username)).Count() == 0)
                    {
                        return ValidationResult.Success;
                    }
                }
            }
            return new ValidationResult("Username já utilizado");
        }
    }
}