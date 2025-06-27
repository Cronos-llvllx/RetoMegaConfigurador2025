class Generic {
  constructor(private _id: number, private _creationDate?: Date) { }

  // *** GETTERS & SETTERS

  /** Obtiene la fecha de creación del objeto. */
  getCreationDate() {
    return this._creationDate;
  }

  /** Asigna una fecha de creación para el objeto. */
  setCreationDate(creationDate?: Date) {
    this._creationDate = creationDate;
  }

  /** Obtiene el identificador, siendo la llave primaria asignada por la base de datos. */
  getId() {
    return this._id;
  }

  /** Asigna un identificador, siendo la llave primaria asignada por la base de datos. */
  setId(id: number) {
    this._id = id;
  }
}

export default Generic;