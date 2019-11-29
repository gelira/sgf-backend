using System.ComponentModel.DataAnnotations;

namespace SGFBackend.Models
{
    public class ProfessorUpdate
    {
        [Required, StringLength(11)]
        public string Cref { get; set; }
    }
}