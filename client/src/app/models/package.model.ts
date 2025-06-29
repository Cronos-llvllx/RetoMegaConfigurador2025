
import Generic from "./generic.model";
import PackagePromotion from "./packagepromotion.model";
import Service from "./service.model";

class Package extends Generic {
  /** Define que el paquete es para contratos empresariales. */
  static get TYPE_FOR_ENTERPRISE() { return 2 }

  /** Define que el paquete es para contratos residenciales. */
  static get TYPE_FOR_RESIDENTIAL() { return 1 }

  /**
   * @param id El identificador del paquete.
   * @param _name El nombre del paquete.
   * @param _type El tipo del paquete (Package.TYPE_...).
   * @param _services Lista de servicios que contiene el paquete.
   * @param _promotions Lista de promociones aplicables a este paquete.
   * @param _basePrice El precio base del paquete. Si no se define, se calculará el precio
   * base a partir de la suma del precio base de los servicios.
   */
  constructor(
    id: number,
    private _name: string,
    private _type: number,
    private _services: Service[],
    private _promotions: PackagePromotion[],
    private _basePrice?: number
  ) { super(id) }

  /** Obtiene el precio base del paquete. Si el precio base del paquete está indefinido,
   * regresará la suma de los precios base de cada servicio del paquete. */
  getBasePrice() {
    if (this._basePrice === undefined) {
      let auxSum = 0;

      this._services.forEach(s => auxSum += s.getBasePrice())

      return auxSum;
    }

    return this._basePrice;
  }

  /** Obtiene el nombre del paquete. */
  getName() {
    return this._name;
  }

  /** Obtiene las promociones aplicables al paquete */
  getPromotions() {
    return this._promotions;
  }

  /** Obtiene los servicios que contiene el paquete. */
  getServices() {
    return this._services;
  }

  /** Obtiene el tipo del paquete (define si es para contratos residenciales o empresariales). */
  getType() {
    return this._type;
  }

  /** Asigna un precio base al paquete. */
  setBasePrice(basePrice?: number) {
    this._basePrice = basePrice;
  }

  /** Asigna un nombre al paquete. */
  setName(name: string) {
    this._name = name;
  }

  /** Asigna una lista de promociones aplicables al paquete. */
  setPromotions(promotions: PackagePromotion[]) {
    this._promotions = promotions;
  }

  /** Asigna los servicios para el paquete. */
  setServices(services: Service[]) {
    this._services = services;
  }

  /** Asigna un tipo para el paquete, que define si es para contratos residenciales o empresariales. */
  setType(type: number) {
    this._type = type;
  }
}

export default Package;
