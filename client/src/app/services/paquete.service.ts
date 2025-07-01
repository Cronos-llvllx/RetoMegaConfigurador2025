import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import Paquete from '../models/package.model';

@Injectable({
  providedIn: 'root'
})
export class PaqueteService {
  // Asegúrate de que el puerto coincida con tu backend (ej. 5026)
  private apiUrl = 'http://localhost:5026/api';

  constructor(private http: HttpClient) { }

  /**
   * Obtiene todos los paquetes disponibles desde el backend.
   */
  getAllPackages(): Observable<Paquete[]> {
    return this.http.get<Paquete[]>(`${this.apiUrl}/Paquete`);
  }

}
