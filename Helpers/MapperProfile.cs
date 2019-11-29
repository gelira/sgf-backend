using AutoMapper;
using SGFBackend.Entities;
using SGFBackend.Models;

namespace SGFBackend.Helpers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Categoria, CategoriaGet>();
            CreateMap<CategoriaCreate, Categoria>();
            CreateMap<Exercicio, ExercicioGet>();
            CreateMap<ExercicioCreate, Exercicio>();
            CreateMap<UserCreate, User>();
        }
    }
}