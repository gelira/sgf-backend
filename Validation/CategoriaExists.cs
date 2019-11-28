using System.ComponentModel.DataAnnotations;
using System.Linq;
using SGFBackend.Entities;

namespace SGFBackend.Validation
{
     public class CategoriaExists : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            int id = System.Convert.ToInt32(value);
            using (var context = new SgfContext())
            {
                if (context.Categorias.Where(c => c.Idcategoria == id).Count() > 0)
                {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult("Categoria not found");
        }
    }
}
