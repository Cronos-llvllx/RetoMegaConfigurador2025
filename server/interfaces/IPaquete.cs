using megaapi.models;

namespace megaapi.interfaces;

public interface IPaquete
{
  Task<IEnumerable<Paquete>> ObtenerTodoAsync();
  // Otros métodos.
  Task<Paquete?> ObtenerPorIdAsync(int id);
  Task<Paquete> ActualizarAsync(Paquete paquete);
  Task<bool> CrearAsync(Paquete paquete);
  Task<bool> EliminarAsync(Paquete paquete);
}