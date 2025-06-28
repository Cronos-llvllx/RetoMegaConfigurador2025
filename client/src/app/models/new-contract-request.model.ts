import Subscriptor from './subscriptor.model'; // Importa la clase Subscriptor de tu archivo

/**
 * DTO para enviar una solicitud de alta de suscriptor y contrato al backend.
 * Refleja la estructura del DTO 'RegisterSubscriberWithContractRequest' en C#.
 */
export interface NewContractRequest {
  /** Los datos del nuevo suscriptor a crear. */
  subscriber: Subscriptor;

  /** Precio base del contrato antes de aplicar la promoción. */
  baseContractPrice: number;

  /** ID de la promoción de contratación seleccionada manualmente (opcional). */
  idManualPromotion?: number | null; // El '?' indica que es opcional

  /** IDs de los paquetes que se añadirán al contrato (opcional). */
  packageIds?: number[]; // Un array de números
}
