import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import Paquete from '../models/package.model';
import Package from '../models/package.model';
import { APIPackageRequest, APIPackageResponse } from '../models/api/api-package.interface';
import env from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PaqueteService {
  // URL base del API usando environment
  private apiUrl = `${env.url}`;

  constructor(private http: HttpClient) { }

  /**
   * Obtiene todos los paquetes disponibles desde el backend (datos básicos).
   */
  getAllPackages(): Observable<Paquete[]> {
    return this.http.get<Paquete[]>(`${this.apiUrl}Paquete`);
  }

  /**
   * Obtiene todos los paquetes con información completa incluyendo servicios (para gestión).
   */
  getAllPackagesComplete(): Observable<any[]> {
    console.log('Llamando al endpoint completo:', `${this.apiUrl}Paquete/admin/completos`);
    return this.http.get<any[]>(`${this.apiUrl}Paquete/admin/completos`).pipe(
      map(paquetes => {
        console.log('Paquetes completos recibidos:', paquetes);
        return paquetes;
      })
    );
  }

  /**
   * Obtiene todos los paquetes en formato simplificado para selectores.
   */
  getPaquetesSimplified(): Observable<{id: number, name: string, tipo: number}[]> {
    console.log('Llamando al endpoint:', `${this.apiUrl}Paquete`);
    return this.http.get<any[]>(`${this.apiUrl}Paquete`).pipe(
      map(paquetes => {
        console.log('Paquetes recibidos del backend:', paquetes);
        return paquetes.map(p => ({
          id: p.idpaquete,
          name: p.nombre,
          tipo: p.tipo
        }));
      })
    );
  }

  addNewPackage(pack: Package): Observable<Package> {
    const req: APIPackageRequest = {
      Nombre: pack.getName(),
      PrecioBase: pack.getBasePrice(),
      Servicios: pack.getServices()!.map(s => s.getId()),
      Tipo: pack.getType(),
    }

    return this.http.post<APIPackageResponse>(`${this.apiUrl}Paquete/registrar`, req).pipe(
      map(res => {
        pack.setId(res.Idpaquete);
        return pack;
      })
    );
  }

  updatePackage(pack: Package): Observable<Package> {
    const packageId = pack.getId();
    
    if (!packageId || packageId === 0) {
      throw new Error('Package ID is invalid for update');
    }
    
    const req: APIPackageRequest = {
      Idpaquete: packageId,
      Nombre: pack.getName(),
      PrecioBase: pack.getBasePrice(),
      Servicios: pack.getServices()!.map(s => s.getId()),
      Tipo: pack.getType(),
    }

    return this.http.put<APIPackageResponse>(`${this.apiUrl}Paquete/actualizar/${req.Idpaquete}`, req).pipe(
      map(() => pack)
    );
  }

  removePackage(pack: Package): Observable<boolean> {
    return this.http.delete<boolean>(`${this.apiUrl}Paquete/eliminar/${pack.getId()}`).pipe(
      map(res => res)
    );
  }
}
