import Generic from "./generic.model";
import City from "./city.model"; // Se importa el modelo de Ciudad

/** Clase para colonias. */
class Colony extends Generic {
  /**
   * @param id El identificador de la colonia.
   * @param _name El nombre de la colonia.
   * @param _city La ciudad a la que pertenece la colonia. // Parámetro añadido
   */
  constructor(
    id: number,
    private _name: string,
    private _city: City // Propiedad añadida
  ) { super(id) }

  /** Obtiene el nombre de la colonia. */
  getNombre() {
    return this._name;
  }

  /** Obtiene la ciudad de la colonia. */ // Método añadido
  getCiudad() {
    return this._city;
  }

  /** Asigna un nombre a la colonia. */
  setNombre(name: string) {
    this._name = name;
  }

  /** Asigna la ciudad a la colonia. */ // Método añadido
  setCiudad(city: City) {
    this._city = city;
  }
}

export default Colony;
