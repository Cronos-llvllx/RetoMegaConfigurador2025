export interface APIService {
  /** Id del servicio. */
  idservicio: number
  /** Cantidad del servicio (líneas telefónicas, canales de tv, megas de internet). */
  cantidad: number
  /** Precio base del servicio. */
  precioBase: number
  /** Tipo del servicio: Service.TYPE_... */
  tipo: number
  paquetes: null
}
