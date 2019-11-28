using System;
using System.Collections.Generic;

namespace SGFBackend.Entities
{
    public partial class Exercicio
    {
        public Exercicio()
        {
            ExerciciosAluno = new HashSet<ExerciciosAluno>();
        }

        public int Idexercicio { get; set; }
        public string Nome { get; set; }
        public int Idcategoria { get; set; }

        public virtual Categoria IdcategoriaNavigation { get; set; }
        public virtual ICollection<ExerciciosAluno> ExerciciosAluno { get; set; }
    }
}
