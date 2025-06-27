export interface Service {
  IdService: number; // Clave primaria para la entidad "servicio"
  Type: number; // Tipo de servicio (1.Telefonía, 2.TV, 3.Internet)
  Amount: number; // Monto del servicio num lineas de telefono, megas de internet, canales de TV
  BasePrice: number; // Precio base del servicio
}
