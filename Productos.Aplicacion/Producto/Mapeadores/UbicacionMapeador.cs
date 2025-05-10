

using AutoMapper;
using Productos.Aplicacion.Producto.Dto;
using Productos.Dominio.ObjetoValor;

namespace Productos.Aplicacion.Producto.Mapeadores
{
    public class UbicacionMapeador : Profile
    {
        public UbicacionMapeador() 
        { 
            CreateMap<Ubicacion, UbicacionProductoDto>().ReverseMap();
        }

    }
}
