using megaapi.models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace megaapi.interfaces
{
    /// <summary>
    /// Define las operaciones para el repositorio de contratos.
    /// </summary>
    public interface IContrato
    {
        Task<IEnumerable<Contrato>> ObtenerTodoAsync();
        Task<Contrato?> ObtenerPorIdAsync(int id);
        Task<Contrato> CrearAsync(Contrato contrato);
        Task<bool> ActualizarAsync(Contrato contrato);
        Task<bool> EliminarAsync(Contrato contrato);
    }
}
