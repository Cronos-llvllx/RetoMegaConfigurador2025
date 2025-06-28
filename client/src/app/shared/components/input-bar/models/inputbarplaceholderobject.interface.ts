export default interface InputPlaceholderObject {
  /** El marcador por defecto. */
  default: string,
  /** El contenido de la cabecera cuando el componente no tiene el tamaño mínimo. */
  onMinLengthNotMet?: string,
  /** El contenido de la cabecera cuando el componente no tiene un valor válido (si se
   * tienen validadores con mensajes personalizados, este valor será omitido). */
  onNotValid?: string,
}