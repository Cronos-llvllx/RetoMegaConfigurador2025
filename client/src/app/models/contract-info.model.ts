/**
 * DTO para recibir la información de un suscriptor y su contrato desde el backend.
 * Refleja la información que se muestra en la tarjeta de información.
 */
export interface ContractInfo {
  contractNumber: string; // Número del contrato
  name: string; // Nombre del suscriptor
  contractDate: string; // Fecha de contratación (la API la devuelve como string)
  status: string; // Estado del servicio (ACTIVO, etc.)
  // Puedes añadir más propiedades aquí si tu backend las devuelve (ej. email, teléfono, etc.)
}
