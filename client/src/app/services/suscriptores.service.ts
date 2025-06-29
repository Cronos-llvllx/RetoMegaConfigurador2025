import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';

// --- IMPORTACIONES CORREGIDAS ---
// Se importan como "named exports" usando llaves {}
import  Subscriptor  from '../models/subscriptor.model';
import  Promotion  from '../models/promotion.model'; // El modelo se llama Promotion (inglés)
import  Contract  from '../models/contract.model'; // Tu modelo se llama Contrato

// DTOs
import { NewContractRequest } from '../models/new-contract-request.model';
// ---------------------------------

@Injectable({
  providedIn: 'root'
})
export class SuscriptoresService {

  // La URL del backend. Se coloca aquí directamente porque el archivo environment.ts no existe.
  // Asegúrate de que el puerto (5026) coincida con el de tu backend.
  private apiUrl = 'http://localhost:5026/api';

  constructor(private http: HttpClient) { }

  /**
   * Obtiene la información completa de un contrato por su número.
   * @param contractNumber El número/ID del contrato.
   * @returns Un Observable con la información del contrato y su suscriptor.
   */
  getContractInfo(contractNumber: string): Observable<Contract> {
    // Llama al nuevo endpoint GET /api/CContrato/{id}
    return this.http.get<Contract>(`${this.apiUrl}/CContrato/${contractNumber}`);
  }

  /**
   * Obtiene la información del suscriptor por su ID.
   * @param id El ID del suscriptor a buscar.
   * @returns Un Observable con la información del suscriptor.
   */
  getSubscriberById(id: number): Observable<Subscriptor> {
    // Llama al endpoint GET /api/CSuscriptor/{id}
    // Nota: El controlador en tu backend se llama CSuscriptor
    return this.http.get<Subscriptor>(`${this.apiUrl}/CSuscriptor/${id}`);
  }

  /**
   * Da de alta un suscriptor y un contrato en una sola llamada.
   * @param altaRequest DTO con los datos del suscriptor y el contrato.
   * @returns Un Observable con la respuesta del backend.
   */
  createSubscriberAndContract(altaRequest: NewContractRequest): Observable<any> {
    // Llama al endpoint POST /api/CSuscriptor/alta en tu controlador de C#.
    return this.http.post<any>(`${this.apiUrl}/CSuscriptor/alta`, altaRequest);
  }

  /**
   * Obtiene la lista de promociones por tipo desde el backend.
   * @param type El tipo de promoción a filtrar (ej. 1 para contratación).
   * @returns Un Observable con la lista de promociones.
   */
  getPromotionsByType(type: number): Observable<Promotion[]> {
    // Asumimos un endpoint GET /api/Promocion/byType/{type}
    return this.http.get<Promotion[]>(`${this.apiUrl}/Promocion/byType/${type}`);
  }
}
