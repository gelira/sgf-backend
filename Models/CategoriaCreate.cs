using System.ComponentModel.DataAnnotations;

namespace SGFBackend.Models
{
    public class CategoriaCreate
    {
        [Required, StringLength(100)]
        public string Nome { get; set; }
    }
}
