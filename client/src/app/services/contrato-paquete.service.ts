import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface ContratoPaqueteRequest {
  Idcontrato: number;  
  Idpaquete: number;   
}

export interface ContratoPaqueteResponse {
  Idcontrato: number;     
  Idpaquete: number;      
  FechaAdicion: string;   
  FechaRetiro?: string | null;  
}

@Injectable({
  providedIn: 'root'
})
export class ContratoPaqueteService {
  private apiUrl = 'http://localhost:5026/api/ContratoPaquete';

  constructor(private http: HttpClient) { }

  /**
   * Agrega un paquete a un contrato
   */
  addPackageToContract(contractId: number, packageId: number): Observable<ContratoPaqueteResponse> {
    const request: ContratoPaqueteRequest = {
      Idcontrato: contractId,  
      Idpaquete: packageId     
    };
    
    return this.http.post<ContratoPaqueteResponse>(this.apiUrl, request);
  }

  /**
   * Cancela un paquete de un contrato
   */
  cancelPackageFromContract(contractId: number, packageId: number): Observable<any> {
    return this.http.put(`${this.apiUrl}/cancel/${contractId}/${packageId}`, {});
  }

  /**
   * Obtiene todos los paquetes de un contrato
   */
  getPackagesByContract(contractId: number): Observable<ContratoPaqueteResponse[]> {
    return this.http.get<ContratoPaqueteResponse[]>(`${this.apiUrl}/contract/${contractId}`);
  }
}
