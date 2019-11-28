using System;
using System.Collections.Generic;

namespace SGFBackend.Entities
{
    public partial class ExerciciosAluno
    {
        public int IdexerciciosAluno { get; set; }
        public int Idexercicio { get; set; }
        public int Idaluno { get; set; }
        public string Repeticoes { get; set; }

        public virtual Aluno IdalunoNavigation { get; set; }
        public virtual Exercicio IdexercicioNavigation { get; set; }
    }
}
