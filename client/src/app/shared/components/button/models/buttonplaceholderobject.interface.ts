export default interface ButtonPlaceholderObject {
  /** El marcador por defecto. */
  default: string
  /** El marcador para cuando el componente esté desactivado (se usará 'default' si está indefinido). */
  disabled?: string
  /** El marcador para cuando el componente está esperando (se usará 'default' si está indefinido) */
  waiting?: string
}