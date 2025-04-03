
using AutoMapper;
using MediatR;
using Productos.Aplicacion.Atributos.Dto;
using Productos.Aplicacion.Comun;
using Productos.Dominio.Servicios.Atributo;
using System.Net;

namespace Productos.Aplicacion.Atributos.Consultas
{
    public class MedidaConsultaHandler : IRequestHandler<MedidaConsulta, ListaMedidasOut>
    {
        private readonly ConsultarAtributos _servicioAtributos;
        private readonly IMapper _mapper;

        public MedidaConsultaHandler(ConsultarAtributos servicioAtributos, IMapper mapper)
        {
            _servicioAtributos = servicioAtributos;
            _mapper = mapper;
        }

        public async Task<ListaMedidasOut> Handle(MedidaConsulta request, CancellationToken cancellationToken)
        {
            ListaMedidasOut output = new()
            {
                Medidas = []
            };

            try
            {
                var medidas = await _servicioAtributos.DarMedidas() ?? [];

                if (medidas.Count == 0)
                {
                    output.Resultado = Resultado.SinRegistros;
                    output.Mensaje = "El atributo medidas no tiene valores";
                    output.Status = HttpStatusCode.NoContent;
                }
                else
                {
                    medidas.ForEach(medida => output.Medidas.Add(_mapper.Map<MedidaDto>(medida)));
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
