/** Interfaz de filtros para Inputbar */
export default interface InputbarFilterObject {
  /** Expresión relugar de comparación o una función que recibe la entrada actual. */
  filter: RegExp | ((input?: string) => string | undefined),
  /** Valor que se usará para reemplazar la cadena si el filtro no se cumple ('' por defecto). Solo
   * aplica cuando el filtro es una expresión regular. */
  replacer?: string
}