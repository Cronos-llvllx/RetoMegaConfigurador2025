using megaapi.models;

namespace megaapi.interfaces;

public interface IPaquete
{
    Task<IEnumerable<Paquete>> ObtenerTodoAsync();
    // Otros métodos.
}