import Subscriptor from "./subscriptor.model";
import Generic from "./generic.model";
import ContratoPaquete from "./contratopaquete.model"; // Se importa el nuevo modelo
import Promotion from "./promotion.model";

/** Clase para contratos. */
class Contract extends Generic {
  /**
   * @param id El número del contrato.
   * @param contrDate La fecha de contratación (creación del contrato).
   * @param _endDate La fecha en la que el contrato finalizó.
   * @param _basePrice El precio de contratación del servicio.
   * @param _subscriptor El suscriptor del contrato.
   * @param _promotions Lista de promociones aplicados al contrato (no a los paquetes).
   * @param _contractPackages Los paquetes que contiene el contrato.
   */
  constructor(
    id: number,
    contrDate: Date,
    private _endDate: Date | null,
    private _basePrice: number,
    private _subscriptor: Subscriptor,
    private _promotions: Promotion[],
    private _contractPackages: ContratoPaquete[] // Se añade la propiedad
  ) { super(id, contrDate) }

  // --- MÉTODO AÑADIDO ---
  // Permite acceder a la lista de paquetes desde el componente.
  getPaquetes(): ContratoPaquete[] {
    return this._contractPackages;
  }

  getEndDate(): Date | null {
    return this._endDate;
  }

  getBasePrice() {
    return this._basePrice;
  }

  getSubscriptor() {
    return this._subscriptor;
  }

  getPromotions() {
    return this._promotions;
  }
}

export default Contract;
