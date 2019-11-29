using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGFBackend.Entities;
using SGFBackend.Helpers;
using SGFBackend.Models;

namespace SGFBackend.Controllers
{
    [ApiController]
    [Route("exercicios-aluno")]
    public class ExerciciosAlunoController : ControllerBase
    {
        private SgfContext _context;
        private IMapper _mapper;
        private int userId;

        public ExerciciosAlunoController(SgfContext context, IMapper mapper, IHttpContextAccessor http)
        {
            _context = context;
            _mapper = mapper;
            userId = (new IdUserExtractor(http)).IdUser;
        }

        [HttpPost]
        [Authorize(Policy = "Professores")]
        public IActionResult AddExercicio([FromBody] ExercicioAlunoCreate e)
        {
            var ea = _mapper.Map<ExerciciosAluno>(e);
            _context.ExerciciosAluno.Add(ea);
            _context.SaveChanges();
            return Ok(new { message = "Exercício adicionado" });
        }

        [HttpGet("{id}")]
        public IActionResult ListExerciciosAluno(int id)
        {
            var exercicios = _context.ExerciciosAluno.Where(ea => ea.Idaluno == id).ToList();
            var retorno = _mapper.Map<List<ExercicioAlunoGet>>(exercicios);
            return Ok(retorno);
        }
    }
}