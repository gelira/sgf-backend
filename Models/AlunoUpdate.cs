using System.ComponentModel.DataAnnotations;

namespace SGFBackend.Models
{
    public class AlunoUpdate
    {
        [Required]
        public int Idade { get; set; }

        [Required]
        public float Altura { get; set; }

        [Required]
        public string Doenca { get; set; }
    }
}