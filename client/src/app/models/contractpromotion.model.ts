import Promotion from "./promotion.model";

/** Clase Promoción para contratos */
class ContractPromotion extends Promotion {
  /**
   * @param id El identifiador de la promoción.
   * @param name El nombre de la promoción.
   * @param pricePorcent El precio (o porcentaje) de la promoción.
   * @param expiration Vigencia de la promoción.
   */
  constructor(
    id: number,
    name: string,
    pricePorcent: number,
    expiration: Date
  ) { super(id, name, pricePorcent, expiration) }
}

export default ContractPromotion;