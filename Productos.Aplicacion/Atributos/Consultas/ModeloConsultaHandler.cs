
using AutoMapper;
using MediatR;
using Productos.Aplicacion.Atributos.Dto;
using Productos.Aplicacion.Comun;
using Productos.Dominio.Servicios.Atributo;
using System.Net;

namespace Productos.Aplicacion.Atributos.Consultas
{
    public class ModeloConsultaHandler : IRequestHandler<ModeloConsulta, ListaModelosOut>
    {
        private readonly ConsultarAtributos _servicioAtributos;
        private readonly IMapper _mapper;

        public ModeloConsultaHandler(ConsultarAtributos servicioAtributos, IMapper mapper)
        {
            _servicioAtributos = servicioAtributos;
            _mapper = mapper;
        }

        public async Task<ListaModelosOut> Handle(ModeloConsulta request, CancellationToken cancellationToken)
        {
            ListaModelosOut output = new()
            {
                Modelos = []
            };

            try
            {
                var modelos = await _servicioAtributos.DarModelos() ?? [];

                if (modelos.Count == 0)
                {
                    output.Resultado = Resultado.SinRegistros;
                    output.Mensaje = "El atributo modelos no tiene valores";
                    output.Status = HttpStatusCode.NoContent;
                }
                else
                {
                    modelos.ForEach(modelo => output.Modelos.Add(_mapper.Map<ModeloDto>(modelo)));
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
