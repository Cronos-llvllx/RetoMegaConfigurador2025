import Promotion from "./promotion.model";

/** Clase Promoción para paquetes de servicios. */
class PackagePromotion extends Promotion {
  /** Define el alcance de la promoción para todos los suscriptores. */
  static get SCOPE_ALL_SUBSCRIPTOR() { return 2 }

  /** Define el alcance de la promoción solo para nuevos suscriptores. */
  static get SCOPE_ONLY_NEW_SUBSCRIPTORS() { return 1 }

  /**
   * @param id El identifiador de la promoción.
   * @param name El nombre de la promoción.
   * @param pricePorcent El precio (o porcentaje) de la promoción.
   * @param expiration Vigencia de la promoción.
   * @param _scope Alcance de la promoción (solo nuevos suscriptores o para todos los suscriptores).
   * @param _term La duración (en meses) de la promoción.
   */
  constructor(
    id: number,
    name: string,
    pricePorcent: number,
    expiration?: Date,
    private _scope?: number,
    private _term?: number,
  ) { super(id, name, pricePorcent, expiration) }

  /** Obtiene el alcance de la promoción. */
  getScope() {
    return this._scope;
  }

  /** Obtiene la duración (en meses) de la promoción. */
  getTerm() {
    return this._term;
  }

  /** Asgina el alcance para la promoción. */
  setScope(scope?: number) {
    this._scope = scope;
  }

  /** Asgina una duración (en meses) para la promoción */
  setTerm(term?: number) {
    this._term = term;
  }
}

export default PackagePromotion;