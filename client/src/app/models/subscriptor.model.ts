import Generic from "./generic.model";

/** Clase para suscriptores. */
class Subscriptor extends Generic {
  /** Define a los suscriptores empresariales. */
  public static get TYPE_ENTERPRISE() { return 2 }

  /** Define a los suscriptores residenciales. */
  public static get TYPE_RESIDENTIAL() { return 1 }

  /**
   * @param id El identificador del suscriptor.
   * @param _name El nombre del suscriptor.
   * @param _email El email del suscriptor.
   * @param _phone El teléfono del suscriptor.
   * @param _type El tipo de suscriptor (Subscriptor.TYPE_...).
   */
  constructor(
    id: number,
    private _name: string,
    private _email: string,
    private _phone: string,
    private _type: number
  ) { super(id) }

  /** Obtiene el correo electrónico del suscriptor. */
  getEmail() {
    return this._email;
  }

  /** Obtiene el nombre del suscriptor. */
  getName() {
    return this._name;
  }

  /** Obtiene el teléfono del suscriptor. */
  getPhone() {
    return this._phone;
  }

  /** Obtiene el tipo de suscriptor. */
  getType() {
    return this._type;
  }

  /** Asigna un correo electrónico al suscriptor. */
  setEmail(email: string) {
    this._email = email;
  }

  /** Asigna un nombre al suscriptor. */
  setName(name: string) {
    this._name = name;
  }

  /** Asigna un teléfono al suscriptor. */
  setPhone(phone: string) {
    this._phone = phone;
  }

  /** Asigna un tipo al suscriptor (Globales 'Subscriptor.TYPE_...') */
  setType(type: number) {
    this._type = type;
  }
}

export default Subscriptor;