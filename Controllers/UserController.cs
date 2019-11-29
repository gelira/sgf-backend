using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SGFBackend.Entities;
using SGFBackend.Models;

namespace SGFBackend.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private SgfContext _context;
        private IMapper _mapper;

        public UserController(SgfContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private User CreateUser(User u)
        {
            var passwordBytes = Encoding.UTF8.GetBytes(u.Password);
            using (var hasher = new HMACSHA256())
            {
                var hash = hasher.ComputeHash(passwordBytes);
                u.Password = Convert.ToBase64String(hash);
                u.Salt = Convert.ToBase64String(hasher.Key);
            }
            _context.Users.Add(u);
            _context.SaveChanges();
            return u;
        }

        [HttpPost("aluno")]
        public IActionResult CreateAluno([FromBody] UserCreate u)
        {
            User user = _mapper.Map<User>(u);
            user.Role = "Aluno";
            user = CreateUser(user);

            Aluno a = new Aluno { IdalunoNavigation = user };
            _context.Alunos.Add(a);
            _context.SaveChanges();

            return Ok(new { message = "Aluno criado" });
        }

        [HttpPut("aluno/{id}")]
        public IActionResult UpdateAluno(int id, [FromBody] AlunoUpdate a)
        {
            Aluno aluno = _context.Alunos.SingleOrDefault(al => al.Idaluno == id);
            if (aluno == null)
            {
                return NotFound(new { message = "Aluno not found" });
            }

            aluno.Idade = a.Idade;
            aluno.Altura = a.Altura;
            aluno.Doenca = a.Doenca;
            _context.SaveChanges();
            
            return Ok(new { message = "Aluno atualizado" });
        }

        [HttpPost("professor")]
        public IActionResult CreateProfessor([FromBody] UserCreate u)
        {
            User user = _mapper.Map<User>(u);
            user.Role = "Professor";
            user = CreateUser(user);

            Professor p = new Professor { IdprofessorNavigation = user };
            _context.Professores.Add(p);
            _context.SaveChanges();

            return Ok(new { message = "Professor criado" });
        }

        [HttpPut("professor/{id}")]
        public IActionResult UpdateProfessor(int id, [FromBody] ProfessorUpdate p)
        {
            Professor prof = _context.Professores.SingleOrDefault(pr => pr.Idprofessor == id);
            if (prof == null)
            {
                return NotFound(new { message = "Professor not found" });
            }

            prof.Cref = p.Cref;
            _context.SaveChanges();
            
            return Ok(new { message = "Professor atualizado" });
        }
    }
}