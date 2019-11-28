using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SGFBackend.Entities;
using SGFBackend.Models;

namespace SGFBackend.Controllers
{
    [ApiController]
    [Route("categorias")]
    public class CategoriaController : ControllerBase
    {
        private SgfContext _context;
        private IMapper _mapper;

        public CategoriaController(SgfContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCategorias()
        {
            var categorias = _context.Categorias.ToList();
            var retorno = _mapper.Map<List<CategoriaGet>>(categorias);
            return Ok(retorno);
        }

        [HttpGet("{id}")]
        public IActionResult GetCategoria(int id)
        {
            var categoria = _context.Categorias.SingleOrDefault(c => c.Idcategoria == id);
            if (categoria == null)
            {
                return NotFound(new { message = "Categoria not found" });
            }

            var retorno = _mapper.Map<CategoriaGet>(categoria);
            return Ok(retorno);
        }

        [HttpPost]
        public IActionResult CreateCategoria([FromBody] CategoriaCreate c)
        {
            var categoria = _mapper.Map<Categoria>(c);
            _context.Categorias.Add(categoria);
            _context.SaveChanges();

            return Ok(new { message = "Categoria criada" });
        }
    }
}