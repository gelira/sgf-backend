using System;
using System.Collections.Generic;

namespace SGFBackend.Entities
{
    public partial class Categoria
    {
        public Categoria()
        {
            Exercicio = new HashSet<Exercicio>();
        }

        public int Idcategoria { get; set; }
        public string Nome { get; set; }

        public virtual ICollection<Exercicio> Exercicio { get; set; }
    }
}
