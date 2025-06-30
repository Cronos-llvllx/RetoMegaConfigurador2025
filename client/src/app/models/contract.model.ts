import Subscriptor from "./subscriptor.model";
import Generic from "./generic.model";
import ContratoPaquete from "./contratopaquete.model"; // Se importa el nuevo modelo
import Promotion from "./promotion.model";

/** Clase para contratos. */
class Contract extends Generic {
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
