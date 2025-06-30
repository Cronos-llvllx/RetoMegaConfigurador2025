// Interfaces de solicitud.
/** Interfaz para realizar una petición de cálculo de deuda a la API. */
export interface APIDebtCalculatorContractRequest {
  /** El identificador del contrato del que se desea realizar el cálculo. */
  idcontrato: Number,
  /** La fecha de inicio para el cálculo (aaaa-MM-dd). Debe ser consistente y tener un margen de uno
   * o más meses de diferencia con la fecha de término.*/
  FechaInicio: string,
  /** La fecha de término para el cálculo (aaaa-MM-dd). Debe ser consistente y tener un margen de uno
   * o más meses de diferencia con la fecha de inicio.*/
  FechaFin: string
}

// Interfaces de respuesta.
export interface APIDebtCalculatorContratoResponse {
  /** Número del contrato. */
  idcontrato: number
  /** Id del suscriptor. */
  idsuscriptor: number
  /** Fecha de contratación de los servicios. */
  fechaContr: string
  /** Fecha de finalización del contrato. */
  fechaFin: string | null
  /** Precio de contratación. */
  precioBase: number
  /** Referencia al suscriptor. */
  suscriptor: APIDebtCalculatorSuscriptor
}

export interface APIDebtCalculatorDebtDeudaResponse {
  /** El id del suscriptor. */
  idsuscriptor: number
  /** El id del contrato del suscriptor. */
  idcontrato: number
  /** El objeto que encapsula el cálculo de la deuda. */
  deuda: APIDebtCalculatorDebtDeuda
}

// Interfaces miseláneas.
// Contrato.
export interface APIDebtCalculatorSuscriptor {
  /** Id del suscriptor. */
  idsuscriptor: number
  /** Id de la colonia donde vive el suscriptor. */
  idcolonia: number
  /** Email de suscriptor. */
  email: string
  /** Nombre del suscriptor. */
  nombre: string
  /** Teléfono del suscriptor. */
  telefono: string
  /** Tipo de suscriptor (Subscriptor.TYPE...). */
  tipo: 1 | 2
  /** Referencia a la colonia. */
  colonia: APIDebtCalculatorColonia
}

export interface APIDebtCalculatorColonia {
  /** Id de la colonia. */
  idcolonia: number
  /** Id de la ciudad donde se ubica la colonia. */
  idciudad: number
  /** Nombre de la colonia. */
  nombre: string
  /** Referencia a la ciudad. */
  ciudad: APIDebtCalculatorCiudad
}

export interface APIDebtCalculatorCiudad {
  /** Id de la ciudad. */
  idciudad: number
  /** Nombre de la ciudad. */
  nombre: string
  colonias: null
}

// Calculadora.
export interface APIDebtCalculatorDebtDeuda {
  /** La fecha por la que empezó el cálculo. */
  desde: string
  /** La fecha por la que terminó el cálculo. */
  hasta: string
  /** Lista de periodos calculados. */
  periodos: APIDebtCalculatorPeriodo[]
  /** Lista de paquetes involucrados en el margen de fechas. */
  paquetes: APIDebtCalculatorPaquete[]
  /** Lista de promociones aplicadas en el margen de fechas. */
  promociones: APIDebtCalculatorPromocion[]
}

export interface APIDebtCalculatorPeriodo {
  /** Número o índice del periodo (base 1). */
  numPeriodo: number
  /** Fecha de inicio del periodo. */
  desde: string
  /** Fecha final del periodo. */
  hasta: string
  /** Lista de paquetes reducidos involucrados en el periodo. */
  paquetes: APIDebtCalculatorPaqueteReducido[]
}

export interface APIDebtCalculatorPaqueteReducido {
  /** Id del paquete. */
  idpaquete: number
  /** Lista de promociones aplicadas al paquete. */
  promociones: number[]
}

export interface APIDebtCalculatorPaquete {
  /** Id del paquete. */
  idpaquete: number
  /** Nombre del paquete. */
  nombre: string
  /** Fecha en la que el paquete fue agregado al contrato. */
  fechaAdicion: string
  /** Fecha en la que el paquete fue retirado del contrato. */
  fechaRetiro: any
  /** Percio base del paquete. */
  precioBase: number
  /** Servicios incluidos en el paquete. */
  servicios: APIDebtCalculatorServicio[]
}

export interface APIDebtCalculatorServicio {
  /** Id del servicio. */
  idservicio: number
  /** Cantidad del servicio (telefoía: líneas, tv: canales?, e internet: megas) */
  cantidad: number
  /** Tipo del servicio (Service.TYPE_...) */
  tipo: number
}

export interface APIDebtCalculatorPromocion {
  /** Id de la promoción. */
  idpromocion: number
  /** Nombre de la promoción. */
  nombre: string
  // Precio o porcentaje de la promoción.
  precioPorcen: number
}