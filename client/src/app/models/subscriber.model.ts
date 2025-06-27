export interface Subscriber {
  IdSubscriber: number; // Clave primaria para la entidad "suscriptor"
  Name: string; // Nombre del suscriptor
  Email: string; // Correo electrónico del suscriptor
  PhoneNumber: string;// Número de teléfono del suscriptor
  IdNeighborhood: number;// Clave foránea para la entidad "colonia"
  Type: number; // 1: Residencial, 2: Empresarial
}
