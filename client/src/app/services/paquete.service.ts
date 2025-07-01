import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import Paquete from '../models/package.model';
import Package from '../models/package.model';
import { APIPackageRequest, APIPackageResponse } from '../models/api/api-package.interface';

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

  addNewPackage(pack: Package): Observable<Package> {
    const req: APIPackageRequest = {
      Nombre: pack.getName(),
      PrecioBase: pack.getBasePrice(),
      Servicios: pack.getServices()!.map(s => s.getId()),
      Tipo: pack.getType(),
    }

    return this.http.post<APIPackageResponse>(`${this.apiUrl}/Paquete/registrar`, req).pipe(
      map(res => {
        pack.setId(res.Idpaquete);
        return pack;
      })
    );
  }

  updatePackage(pack: Package): Observable<Package> {
    const req: APIPackageRequest = {
      Idpaquete: pack.getId(),
      Nombre: pack.getName(),
      PrecioBase: pack.getBasePrice(),
      Servicios: pack.getServices()!.map(s => s.getId()),
      Tipo: pack.getType(),
    }

    return this.http.put<APIPackageResponse>(`${this.apiUrl}/Paquete/actualizar/${req.Idpaquete}`, req).pipe(
      map(() => pack)
    );
  }

  removePackage(pack: Package): Observable<boolean> {
    return this.http.delete<boolean>(`${this.apiUrl}/Paquete/eliminar/${pack.getId()}`).pipe(
      map(res => res)
    );
  }
}
