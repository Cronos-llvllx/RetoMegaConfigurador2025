import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { APIService } from '../models/api/api-service.interface';
import Service from '../models/service.model';

@Injectable({
  providedIn: 'root'
})
export class ServicioService {
  // Asegúrate de que el puerto coincida con tu backend (ej. 5026)
  private apiUrl = 'http://localhost:5026/api';

  constructor(private http: HttpClient) { }

  /**
   * Obtiene todos los paquetes disponibles desde el backend.
   */
  getAllServices(): Observable<Service[]> {
    return this.http.get<APIService[]>(`${this.apiUrl}/servicio`).pipe(
      map(res => {
        return res.map(s => new Service(
          s.idservicio,
          s.cantidad,
          s.precioBase,
          s.tipo
        ));
      })
    )
  }
}
