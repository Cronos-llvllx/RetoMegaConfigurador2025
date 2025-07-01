using megaapi.models;

namespace megaapi.interfaces;

/// <summary>Interfaz para Colonia.</summary>
public interface IColonia : IEntidad<Colonia, int> 
{
  /// <summary>Obtiene todas las colonias de una ciudad específica.</summary>
  /// <param name="ciudadId">ID de la ciudad.</param>
  /// <returns>Lista de colonias de la ciudad.</returns>
  Task<IEnumerable<Colonia>> ObtenerPorCiudadAsync(int ciudadId);
}
