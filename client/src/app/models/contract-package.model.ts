export interface ContractPackage {
        IdContract: number; // Clave foránea para la entidad "contrato"
        IdPackage: number; // Clave foránea para la entidad "paquete"
        AddedTime: Date; // Fecha y hora en que se agregó el paquete al contrato
        RemovedTime: Date | null; // Fecha y hora en que se eliminó el paquete del contrato (si aplica)
}
