namespace megaapi.interfaces;


/// <summary>
/// Interfaz genérica para las demás interfaces.
/// </summary>
/// <typeparam name="T">Tipo de dato abstracto (clase)</typeparam>
/// <typeparam name="R">Tipo de dato primitivo para acceder a tuplas por sus identificadores.</typeparam>
public interface IEntidad<T, R>
where T : class
{
  /// <summary>
  /// Obtiene todas las entidades <typeparamref name="T"/>.
  /// </summary>
  /// <returns></returns>
  Task<IEnumerable<T>> GetAllAsync();
  /// <summary>
  /// Obtiene una entidad <typeparamref name="T"/> con el id "<paramref name="id"/>" dado.
  /// </summary>
  /// <param name="id">El identificador de búsqueda.</param>
  /// <returns></returns>
  Task<T?> GetByIdAsync(R id);
  /// <summary>
  /// Crea una nueva entidad <typeparamref name="T"/>.
  /// </summary>
  /// <param name="entity">La entidad por crear.</param>
  /// <returns></returns>
  Task<T> CreateAsync(T entity);
  /// <summary>
  /// Actualiza una entidad <typeparamref name="T"/>.
  /// </summary>
  /// <param name="entity">La entidad por actualizar.</param>
  /// <returns></returns>
  Task<bool> UpdateAsync(T entity);
  /// <summary>
  /// Elimina una entidad <typeparamref name="T"/>.
  /// </summary>
  /// <param name="entity">La entidad por eliminar.</param>
  /// <returns></returns>
  Task<bool> RemoveAsync(T entity);
}