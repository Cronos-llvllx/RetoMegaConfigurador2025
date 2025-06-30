namespace megaapi.data.objects;

static class OperadorObj<T, R>
where T : class
{
  /// <summary>
  /// Compara el valor de una propiedad en un objeto con el valor deseado.
  /// </summary>
  /// <param name="objeto">El objeto donde se encuentra la propiedad</param>
  /// <param name="nombrePropiedad">El nombre de la propiedad en el objeto. Si la propiedad
  /// no se encuentra, se lanzará un ArgumentNullException.</param>
  /// <param name="valorAComparar">El valor a comparar</param>
  /// <returns>Verdadero si la propiedad es de tipo <typeparamref name="R"/> y es igual
  /// a <paramref name="valorAComparar"/></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static bool Comparar(T objeto, string nombrePropiedad, R valorAComparar)
  {
    var prop = typeof(T).GetProperty(nombrePropiedad)
      ?? throw new ArgumentNullException($"No existe la propiedad {nombrePropiedad} en {typeof(T).Name}");

    var propValue = prop.GetValue(objeto);

    return propValue is R foundValue
      && EqualityComparer<R>.Default.Equals(foundValue, valorAComparar);
  }
}