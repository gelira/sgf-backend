using System;
using System.Collections.Generic;

namespace SGFBackend.Entities
{
    public partial class Professor
    {
        public int Idprofessor { get; set; }
        public string Cref { get; set; }

        public virtual User IdprofessorNavigation { get; set; }
    }
}
