using System;
using System.Collections.Generic;

namespace SGFBackend.Entities
{
    public partial class Aluno
    {
        public Aluno()
        {
            ExerciciosAluno = new HashSet<ExerciciosAluno>();
        }

        public int Idaluno { get; set; }
        public int? Idade { get; set; }
        public float? Altura { get; set; }
        public string Doenca { get; set; }

        public virtual User IdalunoNavigation { get; set; }
        public virtual ICollection<ExerciciosAluno> ExerciciosAluno { get; set; }
    }
}
