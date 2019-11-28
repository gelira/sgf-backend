using System.ComponentModel.DataAnnotations;
using SGFBackend.Validation;

namespace SGFBackend.Models
{
    public class ExercicioCreate
    {
        [Required, StringLength(100)]
        public string Nome { get; set; }

        [Required, CategoriaExists]
        public int Idcategoria { get; set; }
    }
}