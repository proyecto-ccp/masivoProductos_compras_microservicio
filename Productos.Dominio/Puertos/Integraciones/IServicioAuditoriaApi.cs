using Productos.Dominio.ObjetoValor;

namespace Productos.Dominio.Puertos.Integraciones
{
    public interface IServicioAuditoriaApi
    {
        Task RegistrarAuditoria(Auditoria auditoria);
    }
}
