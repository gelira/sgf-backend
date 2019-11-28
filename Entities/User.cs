using System;
using System.Collections.Generic;

namespace SGFBackend.Entities
{
    public partial class User
    {
        public int Iduser { get; set; }
        public string Nome { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Role { get; set; }

        public virtual Aluno Aluno { get; set; }
        public virtual Professor Professor { get; set; }
    }
}
