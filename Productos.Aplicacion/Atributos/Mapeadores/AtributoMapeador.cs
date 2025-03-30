
using AutoMapper;
using Productos.Aplicacion.Atributos.Dto;
using Productos.Dominio.ObjetoValor;

namespace Productos.Aplicacion.Atributos.Mapeadores
{
    public class AtributoMapeador : Profile
    {
        public AtributoMapeador()
        {
            CreateMap<Modelo, ModeloDto>().ReverseMap();
            CreateMap<Material, MaterialDto>().ReverseMap(); 
            CreateMap<Marca, MarcaDto>().ReverseMap(); 
            CreateMap<Color, ColorDto>().ReverseMap(); 
            CreateMap<Categoria, CategoriaDto>().ReverseMap();
            CreateMap<Medida, MedidaDto>().ReverseMap();
        }
    }
}
