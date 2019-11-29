using System.ComponentModel.DataAnnotations;
using SGFBackend.Validation;

namespace SGFBackend.Models
{
    public class ExercicioAlunoCreate
    {
        [Required, ExercicioExists]
        public int Idexercicio { get; set; }

        [Required, AlunoExists]
        public int Idaluno { get; set; }

        [Required, StringLength(200)]
        public string Repeticoes { get; set; }
    }
}