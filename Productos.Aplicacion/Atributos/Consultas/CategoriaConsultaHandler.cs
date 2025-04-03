
using AutoMapper;
using MediatR;
using Productos.Aplicacion.Atributos.Dto;
using Productos.Aplicacion.Comun;
using Productos.Dominio.Servicios.Atributo;
using System.Net;

namespace Productos.Aplicacion.Atributos.Consultas
{
    public class CategoriaConsultaHandler : IRequestHandler<CategoriaConsulta, ListaCategoriaOut>
    {
        private readonly ConsultarAtributos _servicioAtributos;
        private readonly IMapper _mapper;

        public CategoriaConsultaHandler(ConsultarAtributos servicioAtributos, IMapper mapper)
        {
            _servicioAtributos = servicioAtributos;
            _mapper = mapper;
        }

        public async Task<ListaCategoriaOut> Handle(CategoriaConsulta request, CancellationToken cancellationToken)
        {
            ListaCategoriaOut output = new()
            {
                Categorias = []
            };

            try
            {
                var categorias = await _servicioAtributos.DarCategorias() ?? [];

                if (categorias.Count == 0)
                {
                    output.Resultado = Resultado.SinRegistros;
                    output.Mensaje = "El atributo categorias no tiene valores";
                    output.Status = HttpStatusCode.NoContent;
                }
                else
                {
                    categorias.ForEach(categoria => output.Categorias.Add(_mapper.Map<CategoriaDto>(categoria)));
                    output.Resultado = Resultado.Exitoso;
                    output.Mensaje = "Consulta exitosa";
                    output.Status = HttpStatusCode.OK;
                }
            }
            catch (Exception ex)
            {
                output.Resultado = Resultado.Error;
                output.Mensaje = ex.Message;
                output.Status = HttpStatusCode.InternalServerError;
            }

            return output;
        }


    }
}
