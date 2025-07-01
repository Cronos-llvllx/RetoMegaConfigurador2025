/**
 * Interface para la respuesta de la API de Ciudad
 */
export interface CityApiResponse {
  id: number;
  name: string;
  colonies?: ColonyApiResponse[];
}

/**
 * Interface para la respuesta de la API de Colonia
 */
export interface ColonyApiResponse {
  id: number;
  name: string;
  cityId: number;
  city?: CityApiResponse;
}

/**
 * Interface para request de crear/actualizar Ciudad
 */
export interface CityRequest {
  name: string;
}

/**
 * Interface para request de crear/actualizar Colonia
 */
export interface ColonyRequest {
  name: string;
  cityId: number;
}
