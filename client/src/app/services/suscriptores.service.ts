import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import Contract from '../models/contract.model'; // Asegúrate de que la ruta sea correcta

@Injectable({
  providedIn: 'root'
})
export class SuscriptoresService {
  // El puerto debe coincidir con el de tu backend (ej. 5026)
  private apiUrl = 'http://localhost:5026/api';

  constructor(private http: HttpClient) { }

  /**
   * Obtiene la información completa de un contrato por su número.
   */
  getContractInfo(contractNumber: string): Observable<Contract> {
    // La URL apunta a 'Contrato'
    return this.http.get<Contract>(`${this.apiUrl}/Contrato/${contractNumber}`);
  }
}
