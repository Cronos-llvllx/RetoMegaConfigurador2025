import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import Susscriptor from '../models/suscriptor.model';
import Promocion from '../models/promocion.model';
import { environment } from '../../environments/environment';

// DTOs del backend (necesitarás crear estos archivos)
import { NewContractRequest } from '../models/new-contract-request.model';
import { ContractInfo } from '../models/contract-info.model';

@Injectable({
  providedIn: 'root'
})
export class SuscriptoresService {

  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  /**
   * Obtiene la información del suscriptor por su ID.
   * @param id El ID del suscriptor a buscar.
   * @returns Un Observable con la información del suscriptor.
   */
  getSubscriberById(id: number): Observable<Subscriptor> {
    // Llama al endpoint GET /api/Subscribers/{id}
    return this.http.get<Subscriptor>(`${this.apiUrl}/Suscriptors/${id}`);
  }

  /**
   * Da de alta un suscriptor y un contrato en una sola llamada.
   * @param altaRequest DTO con los datos del suscriptor y el contrato.
   * @returns Un Observable con la respuesta del backend.
   */
  createSubscriberAndContract(altaRequest: NewContractRequest): Observable<any> {
    // Llama al endpoint POST /api/Subscribers/alta en tu controlador de C#.
    return this.http.post<any>(`${this.apiUrl}/Subscribers/alta`, altaRequest);
  }

  /**
   * Obtiene la lista de promociones por tipo desde el backend.
   * @param type El tipo de promoción a filtrar (ej. 1 para contratación).
   * @returns Un Observable con la lista de promociones.
   */
  getPromotionsByType(type: number): Observable<Promotion[]> {
    // Llama al endpoint que Marlene debe implementar en CPromocion.
    // Asumimos un endpoint GET /api/Promocion/byType/{type}
    return this.http.get<Promotion[]>(`${this.apiUrl}/Promocion/byType/${type}`);
  }
}
