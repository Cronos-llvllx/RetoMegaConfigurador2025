export interface APIPackageRequest {
  Idpaquete?: number,
  Nombre: string,
  PrecioBase: number,
  Tipo: number,
  Servicios: number[]
}

export interface APIPackageResponse {
  Idpaquete: number,
  Nombre: string,
  PrecioBase: number,
  Tipo: number,
  Servicios: number[]
}