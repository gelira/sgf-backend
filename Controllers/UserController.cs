using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SGFBackend.Entities;
using SGFBackend.Helpers;
using SGFBackend.Models;

namespace SGFBackend.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private SgfContext _context;
        private IMapper _mapper;
        private SecretKeyConfig _secret;

        public UserController(SgfContext context, IMapper mapper, IOptions<SecretKeyConfig> options)
        {
            _context = context;
            _mapper = mapper;
            _secret = options.Value;
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

        private bool CheckPassword(string plainPassword, string password, string salt)
        {
            var plainBytes = Encoding.UTF8.GetBytes(plainPassword);
            var passwordBytes = Convert.FromBase64String(password);
            using (var hasher = new HMACSHA256(Convert.FromBase64String(salt)))
            {
                var hash = hasher.ComputeHash(plainBytes);
                for (int i = 0; i < hash.Length; i ++)
                {
                    if (hash[i] != passwordBytes[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private string GerarToken(int id, string role)
        {
            var key = _secret.SecretKeyBytes;
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, id.ToString()),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key), 
                        SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLogin u)
        {
            User user = _context.Users.SingleOrDefault(us => us.Username.Equals(u.Username));
            if (user != null)
            {
                if (CheckPassword(u.Password, user.Password, user.Salt))
                {
                    var token = new AccessToken { Token = GerarToken(user.Iduser, user.Role) };
                    return Ok(token);
                }
            }
            return Unauthorized(new { message = "Username ou Password inválidos" });
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