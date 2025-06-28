import Generic from "./generic.model";

class Colony extends Generic {
  constructor(
    id: number,
    private _name: string
  ) { super(id) }

  /** Obtiene el nombre de la colonia. */
  getName() {
    return this._name
  }

  /** Asigna un nombre a la colonia. */
  setName(name: string) {
    this._name = name;
  }
}

export default Colony;