namespace megaapi.interfaces;

/// <summary>
/// Interfaz genérica para las interfaces de relaciones (llaves compuestas del mismo tipo).
/// </summary>
/// <typeparam name="T">Tipo de dato abstracto (clase)</typeparam>
/// <typeparam name="R">Tipo de dato primitivo para acceder a tuplas por sus identificadores.</typeparam>
public interface IRelacion<T, R> : IEntidad<T, R[]>
where T : class
{
  /// <summary>
  /// Obtiene el conjunto de relaciones que coincidan con la referencia
  /// '<paramref name="nombreIdentificador"/>' (una de las partes de la llave compuesta)
  /// en la relación <typeparamref name="T"/>.
  /// </summary>
  /// <param name="id">El id de búsqueda en la relación <typeparamref name="T"/> .</param>
  /// <param name="nombreIdentificador">El nombre que recibe el atributo que guarda id que se desea
  /// buscar (el que forma parte de la llave compuesta).</param>
  Task<IEnumerable<T>> ObtenerPorReferencia(R id, string nombreIdentificador);
}