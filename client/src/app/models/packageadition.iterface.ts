import Package from "./package.model";

/** Clase de paquete por contrato. */
interface PackageAdition {
  /** El paquete ligado a esta interfaz. */
  package: Package,
  /** Fecha de adición del paquete al contrato. */
  aditionDate: Date
}

export default PackageAdition;