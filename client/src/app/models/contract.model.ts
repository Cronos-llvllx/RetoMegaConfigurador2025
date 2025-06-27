export interface Contract {
        IdContract: number;  // Clave primaria para la entidad "contrato"
        StartTime: Date;  // Fecha y hora del contrato inicio
        EndTime: Date | null;  // Fecha y hora del contrato fin
        PriceContract: number;  // Precio del contrato (sin IVA)
}
