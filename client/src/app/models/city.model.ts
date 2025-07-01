
import Colony from "./colony.model";
import Generic from "./generic.model";

class City extends Generic {
  constructor(
    id: number,
    private _name: string,
    private _colonies: Colony[]
  ) { super(id) }

  /** Obtiene las colonias de la ciudad. */
  getColonies() {
    return this._colonies;
  }

  /** Obtiene el nombre de la ciudad. */
  getName() {
    return this._name;
  }

  /** Asgina colonias a la ciudad. */
  setColonies(colonies: Colony[]) {
    this._colonies = colonies;
  }

  /** Asgina un nombre a la ciudad. */
  setName(name: string) {
    this._name = name;
  }
}

export default City;
