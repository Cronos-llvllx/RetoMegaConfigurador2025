import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import City from '../models/city.model';
import Colony from '../models/colony.model';
import { CityApiResponse, ColonyApiResponse } from '../models/api/location.interface';

@Injectable({
  providedIn: 'root'
})
export class LocationService {
  // El puerto debe coincidir con el de tu backend (ej. 5026)
  private apiUrl = 'http://localhost:5026/api';

  constructor(private http: HttpClient) { }

  /**
   * Obtiene todas las ciudades registradas en la base de datos.
   */
  getCities(): Observable<City[]> {
    return this.http.get<City[]>(`${this.apiUrl}/Ciudad`);
  }

  /**
   * Obtiene todas las colonias de una ciudad específica.
   * @param cityId ID de la ciudad
   */
  getColoniesByCity(cityId: number): Observable<Colony[]> {
    return this.http.get<Colony[]>(`${this.apiUrl}/Colonia/ciudad/${cityId}`);
  }

  /**
   * Obtiene todas las colonias (si se necesita para otros casos).
   */
  getAllColonies(): Observable<Colony[]> {
    return this.http.get<Colony[]>(`${this.apiUrl}/Colonia`);
  }

  /**
   * Obtiene una ciudad específica por su ID.
   * @param cityId ID de la ciudad
   */
  getCityById(cityId: number): Observable<City> {
    return this.http.get<City>(`${this.apiUrl}/Ciudad/${cityId}`);
  }

  /**
   * Obtiene una colonia específica por su ID.
   * @param colonyId ID de la colonia
   */
  getColonyById(colonyId: number): Observable<Colony> {
    return this.http.get<Colony>(`${this.apiUrl}/Colonia/${colonyId}`);
  }

  /**
   * Obtiene nombres de ciudades simplificados para uso en componentes.
   * Retorna un array de objetos simples {id, name}
   */
  getCitiesSimplified(): Observable<{id: number, name: string}[]> {
    return this.http.get<CityApiResponse[]>(`${this.apiUrl}/Ciudad`)
      .pipe(
        map(cities => cities.map(city => ({
          id: city.id,
          name: city.name
        })))
      );
  }

  /**
   * Obtiene nombres de colonias simplificados por ciudad para uso en componentes.
   * Retorna un array de objetos simples {id, name}
   * @param cityId ID de la ciudad
   */
  getColoniesByCitySimplified(cityId: number): Observable<{id: number, name: string}[]> {
    return this.http.get<ColonyApiResponse[]>(`${this.apiUrl}/Colonia/ciudad/${cityId}`)
      .pipe(
        map(colonies => colonies.map(colony => ({
          id: colony.id,
          name: colony.name
        })))
      );
  }
}
