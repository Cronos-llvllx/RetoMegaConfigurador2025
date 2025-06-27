import PackageAdition from "./packageadition.iterface";
import ContractPromotion from "./contractpromotion.model";
import Generic from "./generic.model";
import Subscriptor from "./subscriptor.model";

class Contract extends Generic {
  /**
   * @param id El número del contrato.
   * @param contrDate La fecha de contratación (creación del contrato).
   * @param _endDate La fecha en la que el contrato finalizó.
   * @param _basePrice El precio de contratación del servicio.
   * @param _subscriptor El suscriptor del contrato.
   * @param _promotions Lista de promociones aplicados al contrato (no a los paquetes).
   */
  constructor(
    id: number,
    contrDate: Date,
    private _endDate: Date,
    private _basePrice: number,
    private _subscriptor: Subscriptor,
    private _promotions: ContractPromotion[],
    private _contractPackages: PackageAdition[]
  ) { super(id, contrDate) }

  /** Obtiene la fecha de finalización del contrato. */
  getEndDate() {
    return this._endDate;
  }

  /** Asigna una fecha de finalización para el contrato. */
  setEndDate(endDate: Date) {
    this._endDate = endDate;
  }

  /** Obtiene los paquetes de adición del contrato. */
  getContractPackages() {
    return this._contractPackages;
  }

  /** Obtiene el precio base de contratación. */
  getBasePrice() {
    return this._basePrice;
  }

  /** Obtiene la lista de promociones aplicadas al precio base de contratación. */
  getPromotions() {
    return this._promotions;
  }

  /** Obtiene el subscriptor del contrato. */
  getSubscriptor() {
    return this._subscriptor
  }

  /** Asigna los paquetes de adición al contrato. */
  setContractPackages(packages: PackageAdition[]) {
    this._contractPackages = packages;
  }

  /** Asigna el precio de contratación. */
  setContrPrice(contrPrice: number) {
    this._basePrice = contrPrice;
  }

  /** Asigna una lista de promociones aplicables al precio base de contratación. */
  setPromotions(promotions: ContractPromotion[]) {
    this._promotions = promotions;
  }

  /** Asigna un subscriptor al contrato. */
  setSubscriptor(subscriptor: Subscriptor) {
    this._subscriptor = subscriptor;
  }
}

export default Contract;