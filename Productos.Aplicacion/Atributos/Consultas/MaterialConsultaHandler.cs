
using AutoMapper;
using MediatR;
using Productos.Aplicacion.Atributos.Dto;
using Productos.Aplicacion.Comun;
using Productos.Dominio.Servicios.Atributo;
using System.Net;

namespace Productos.Aplicacion.Atributos.Consultas
{
    public class MaterialConsultaHandler : IRequestHandler<MaterialConsulta, ListaMaterialOut>
    {
        private readonly ConsultarAtributos _servicioAtributos;
        private readonly IMapper _mapper;

        public MaterialConsultaHandler(ConsultarAtributos servicioAtributos, IMapper mapper)
        {
            _servicioAtributos = servicioAtributos;
            _mapper = mapper;
        }

        public async Task<ListaMaterialOut> Handle(MaterialConsulta request, CancellationToken cancellationToken)
        {
            ListaMaterialOut output = new()
            {
                Materiales = []
            };

            try
            {
                var materiales = await _servicioAtributos.DarMateriales() ?? [];

                if (materiales.Count == 0)
                {
                    output.Resultado = Resultado.SinRegistros;
                    output.Mensaje = "El atributo materiales no tiene valores";
                    output.Status = HttpStatusCode.NoContent;
                }
                else
                {
                    materiales.ForEach(marca => output.Materiales.Add(_mapper.Map<MaterialDto>(marca)));
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
