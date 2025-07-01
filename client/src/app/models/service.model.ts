
import Generic from "./generic.model";

class Service extends Generic {
  /** Servicio de tipo telefonía. */
  static get TYPE_PHONE_LINE() { return 1 }
  /** Servicio de tipo televisión */
  static get TYPE_TV() { return 2 }
  /** Servicio de tipo internet. */
  static get TYPE_INTERNET() { return 3 }

  /**
   * @param id El identificador el servicio.
   * @param quantity La cantidad que ofrece el servicio: no. de líneas para telefonía,
   * no. de conexiones de TV para televisión y no. de megas para internet.
   * @param basePrice Precio base del servicio.
   * @param type Tipo de servicio (Service.TYPE_...).
   */
  constructor(
    id: number,
    private _quantity: number,
    private _basePrice: number,
    private _type: number
  ) { super(id) }

  /** Obtiene el servicio (solo el nombre según el tipo) */
  override toString() {
    switch (this._type) {
      case (Service.TYPE_INTERNET): return `Internet ${this._quantity} mega${this._quantity > 1 ? 's' : ''}`;
      case (Service.TYPE_PHONE_LINE): return `Telefonía ${this._quantity} línea${this._quantity > 1 ? 's' : ''}`;
      case (Service.TYPE_TV): return `Televisión ${this._quantity} canal${this._quantity > 1 ? 'es' : ''}`;
      default: return '?';
    }
  }

  /** Obtiene la cantidad que ofrece el servicio (según el tipo de servicio). */
  getQuantity() {
    return this._quantity;
  }

  /** Obtiene el tipo de servicio (Service.TYPE_...) */
  getType() {
    return this._type;
  }

  /** Asigna el precio base para el servicio. */
  setBasePrice(basePrice: number) {
    this._basePrice = basePrice;
  }

  setQuantity(quantity: number) {
    this._quantity = quantity;
  }

  /** Asigna el tipo para el servicio (Service.TYPE_...) */
  setType(type: number) {
    this._type = type;
  }

  /** Obtiene el precio base del servicio. */
  getBasePrice() {
    return this._basePrice;
  }
}

export default Service;
