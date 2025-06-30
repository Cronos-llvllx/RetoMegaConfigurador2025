import Contract from "./contract.model";
import Paquete from "./package.model";

/**
 * Clase para la relación entre Contrato y Paquete.
 * Representa un paquete que ha sido añadido a un contrato.
 */
class ContratoPaquete {
  constructor(
    private _contrato: Contract | null, // Puede ser nulo para evitar dependencias circulares
    private _paquete: Paquete,
    private _fechaAdicion: Date,
    private _fechaRetiro: Date | null
  ) {}

  public getContrato(): Contract | null {
    return this._contrato;
  }

  public getPaquete(): Paquete {
    return this._paquete;
  }

  public getFechaAdicion(): Date {
    return this._fechaAdicion;
  }

  public getFechaRetiro(): Date | null {
    return this._fechaRetiro;
  }
}

export default ContratoPaquete;
