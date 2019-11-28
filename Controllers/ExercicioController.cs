using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SGFBackend.Entities;
using SGFBackend.Models;

namespace SGFBackend.Controllers
{
    [ApiController]
    [Route("exercicios")]
    public class ExercicioController : ControllerBase
    {
        private SgfContext _context;
        private IMapper _mapper;

        public ExercicioController(SgfContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetExercicios()
        {
            var exercicios = _context.Exercicios.ToList();
            var retorno = _mapper.Map<List<ExercicioGet>>(exercicios);
            return Ok(retorno);
        }

        [HttpGet("categoria/{id}")]
        public IActionResult GetExerciciosCategoria(int id)
        {
            var exercicios = _context.Exercicios.Where(e => e.Idcategoria == id).ToList();
            var retorno = _mapper.Map<List<ExercicioGet>>(exercicios);
            return Ok(retorno);
        }

        [HttpPost]
        public IActionResult CreateExercicio([FromBody] ExercicioCreate e)
        {
            var exercicio = _mapper.Map<Exercicio>(e);
            _context.Exercicios.Add(exercicio);
            _context.SaveChanges();

            return Ok(new { message = "Exercício criado" });
        }
    }
}