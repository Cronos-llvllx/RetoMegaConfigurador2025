namespace megaapi.interfaces;
using megaapi.models;

/// <summary>Interfaz para Suscriptor.</summary>
public interface ISuscriptor : IEntidad<Suscriptor, int> { }
/// <summary>
/// Interfaz genérica para las demás interfaces.
/// </summary>
/// <typeparam name="T">Tipo de dato abstracto (clase)</typeparam>
/// <typeparam name="R">Tipo de dato primitivo para acceder a tuplas por sus identificadores.</typeparam>
public interface IEntidad<T, R>
where T : class
{
  /// <summary>
  /// Obtiene todos los registros de la entidad <typeparamref name="T"/>.
  /// </summary>
  /// <returns></returns>
  Task<IEnumerable<T>> GetAllAsync();
  /// <summary>
  /// Obtiene un registro de la entidad <typeparamref name="T"/> con el id "<paramref name="id"/>"
  /// dado.
  /// </summary>
  /// <param name="id">El identificador de búsqueda.</param>
  /// <returns></returns>
  Task<T?> GetByIdAsync(R id);
  /// <summary>
  /// Crea un nuevo registro en la entidad <typeparamref name="T"/>.
  /// </summary>
  /// <param name="record">La entidad por crear.</param>
  /// <returns></returns>
  Task<T> CreateAsync(T record);
  /// <summary>
  /// Actualiza un registro de la entidad <typeparamref name="T"/>.
  /// </summary>
  /// <param name="record">La entidad por actualizar.</param>
  /// <returns></returns>
  Task<bool> UpdateAsync(T record);
  /// <summary>
  /// Elimina un registro de la entidad <typeparamref name="T"/>.
  /// </summary>
  /// <param name="record">El registro por eliminar.</param>
  /// <returns></returns>
  Task<bool> RemoveAsync(T record);
}