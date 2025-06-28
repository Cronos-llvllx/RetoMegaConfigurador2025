<<<<<<< HEAD
export interface Promotion {
        IdPromotion: number; // Clave primaria para la entidad "promoción"
        Scope: number;  // Alcance de la promoción (1: Nuevos suscriptores, 2: todos los suscriptores)
        Duration: number;  //  a partir de la aplicación. No aplica para el precio de contratación.
        Name: string;  // Nombre de la promoción
        PriceOrPorcentage: number;   // Precio o porcentaje de la promoción. Cuando es un número entre cero y uno, se considera como un porcentaje (multiplicado por el precio o conjunto de precios para obtener el descuento). Si no, se considera como el precio base (reemplaza al precio o precios en conjunto).
        Type: number;  // Tipo de promoción (1: Promocion en precio de contratacion, 2: Promocion para servicios(pago mensual))
        Validity: Date;  // Fecha que la promoción ya no estara disponible al publico
}
=======
import Generic from "./generic.model";

/** Clase Promoción (plantilla). No crees instancias de esta clase: 
 * Para las promociones de paquetes, utiliza PackagePromotion.
 * Para las promociones de contratación, utiliza ContractPromotion.*/
class Promotion extends Generic {

  /**
   * @param id El identificador de la promoción.
   * @param _name El nombre de la promoción.
   * @param _pricePorcent El precio (o porcentaje) de la promoción.
   * @param _expiration La expiración de la promoción.
   */
  constructor(
    id: number,
    private _name: string,
    private _pricePorcent: number,
    private _expiration: Date
  ) { super(id) }

  /** Obtiene la vigencia de la promoción (la fecha en la que la promoción ya no estará
   * disponible para el público). */
  getExpiration() {
    return this._expiration
  }

  /** Obtiene el nombre de la promoción. */
  getName() {
    return this._name;
  }

  /** Obtiene el precio o porcentaje de la promoción. Si es un número entre 0
   * y 1, es un porcentaje. Si no, es un precio que reemplaza a los precios base
   * de los servicios o los costos base de los contratos.*/
  getPricePorcent() {
    return this._pricePorcent
  }

  /** Asigna una vigencia para la promoción (cuando dejará de estar disponible para el
   * público). */
  setExpiration(expiration: Date) {
    this._expiration = expiration;
  }

  /** Asigna un nombre a la promoción. */
  setName(name: string) {
    this._name = name;
  }

  /** Asigna el precio o porcentaje de la promoción (0 >= pricePorcent <= 1: porcentaje,
   * pricePorcent > 1: precio de reemplazo). */
  setPricePorcent(pricePorcent: number) {
    this._pricePorcent = pricePorcent;
  }
}

export default Promotion;
>>>>>>> origin/aldo_kalid
