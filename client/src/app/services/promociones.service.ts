import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import env from '../environments/environment';

/**
 * Interfaz que define la estructura de una promoción según el backend
 */
export interface Promocion {
  idpromocion: number;
  alcance?: number;
  nombre: string;
  duracion?: number;
  fechaRegistro: Date;
  precioPorcen: number;
  tipo: number;
  vigencia: Date;
  // Relaciones con IDs en lugar de strings
  ciudades: PromocionCiudad[];
  colonias: PromocionColonia[];
  paquetes: PromocionPaquete[];
}

/**
 * Interfaz para la relación promoción-ciudad
 */
export interface PromocionCiudad {
  idpromocion: number;
  idciudad: number;
  ciudad?: {
    idciudad: number;
    nombre: string;
  };
}

/**
 * Interfaz para la relación promoción-colonia
 */
export interface PromocionColonia {
  idpromocion: number;
  idcolonia: number;
  colonia?: {
    idcolonia: number;
    nombre: string;
  };
}

/**
 * Interfaz para la relación promoción-paquete
 */
export interface PromocionPaquete {
  idpromocion: number;
  idpaquete: number;
  paquete?: {
    idpaquete: number;
    nombre: string;
  };
}

/**
 * Interfaz para crear una nueva promoción (coincide con PromocionRegistroDto del backend)
 */
export interface CrearPromocion {
  alcance?: number;
  nombre: string;
  duracion: number;
  precioPorcen: number;
  tipo: number;
  vigencia: string; // String en formato fecha, como espera el backend
  // Arrays de IDs para las relaciones (nombres exactos del DTO)
  ciudaddes: number[]; // Nota: el backend usa "Ciudaddes" con doble 'd'
  colonias: number[];
  paquetes: number[];
}

/**
 * Servicio para manejar las operaciones CRUD de promociones
 * Se comunica con el backend a través de HTTP
 */
@Injectable({
  providedIn: 'root'
})
export class PromocionesService {
  
  // URL base del API del backend
  private readonly apiUrl = `${env.url}Promocion`; // Usa configuración centralizada

  constructor(private http: HttpClient) { }

  /**
   * Obtiene todas las promociones desde el backend
   * @returns Observable con la lista de promociones
   */
  obtenerTodas(): Observable<Promocion[]> {
    return this.http.get<Promocion[]>(this.apiUrl);
  }

  /**
   * Obtiene una promoción específica por su ID
   * @param id ID de la promoción a obtener
   * @returns Observable con la promoción encontrada
   */
  obtenerPorId(id: number): Observable<Promocion> {
    return this.http.get<Promocion>(`${this.apiUrl}/${id}`);
  }

  /**
   * Crea una nueva promoción en el backend
   * @param promocion Datos de la promoción a crear
   * @returns Observable con la promoción creada
   */
  crear(promocion: CrearPromocion): Observable<Promocion> {
    console.log('🚀 Datos enviados al backend:', JSON.stringify(promocion, null, 2));
    console.log('🌐 URL completa:', `${this.apiUrl}/registro`);
    return this.http.post<Promocion>(`${this.apiUrl}/registro`, promocion);
  }

  /**
   * Actualiza una promoción existente
   * @param id ID de la promoción a actualizar
   * @param promocion Datos actualizados de la promoción
   * @returns Observable con la promoción actualizada
   */
  actualizar(id: number, promocion: CrearPromocion): Observable<Promocion> {
    return this.http.put<Promocion>(`${this.apiUrl}/actualizar/${id}`, promocion);
  }

  /**
   * Elimina una promoción del backend
   * @param id ID de la promoción a eliminar
   * @returns Observable con el resultado de la operación
   */
  eliminar(id: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/eliminar/${id}`);
  }
}
