export default interface ButtonCustomWidthObject {
  /** No puede ser menor a 1. Puede usar cualquier unidad admitida. Si solo es un número, se
   * considerará en pixeles. */
  value: string,
  /** El tipo de longitud que será considerado el valor. */
  as: 'maxWidth' | 'width'
}