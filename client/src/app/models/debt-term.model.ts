import Package from "./package.model";

class DebtTerm {
  constructor(
    private _periodIndex: number,
    private _fromDate: Date,
    private _toDate: Date,
    private _packages: Package[]
  ) { }

  /** Convierte ambas fechas del periodo en una cadena yyyy/MM/dd */
  toPeriodDatesString() {
    const date1 = this._fromDate.toISOString().split('T')[0].replace(/-/g, '/');
    const date2 = this._toDate.toISOString().split('T')[0].replace(/-/g, '/');

    return `${date1} - ${date2}`;
  }

  /** Obtiene el subtotal de precio del periodo (no aplica promociones) */
  getPeriodSubPrice(): string {
    let result = 0;

    this._packages.forEach(pack => result += pack.getBasePrice());

    const format = new Intl.NumberFormat('es-MX', {
      style: 'currency',
      currency: 'MXN'
    })

    return format.format(result);
  }

  /** Obtiene el total de precio del periodo (con promociones aplicadas) */
  getPeriodPrice(): string {
    let result = 0;

    this._packages.forEach(pack => result += pack.getBasePriceWithDiscounts());

    const format = new Intl.NumberFormat('es-MX', {
      style: 'currency',
      currency: 'MXN'
    })

    return format.format(result);
  }

  /** Obtiene la fecha de inicio del periodo. */
  getFromDate() {
    return this._fromDate;
  }

  /** Obtiene la lista de paquetes involucrados en el periodo. */
  getPackages() {
    return this._packages;
  }

  /** Obtiene el número o índice del periodo (base 1). */
  getPeriodIndex() {
    return this._periodIndex;
  }

  /** Obtiene la fecha final del periodo. */
  getToDate() {
    return this._toDate;
  }

  /** Asgina una fecha de inicio para el periodo. */
  setFromDate(fromDate: Date) {
    this._fromDate = fromDate;
  }

  /** Asigna una lista de paquetes involucrados para el periodo. */
  setPackages(packages: Package[]) {
    this._packages = packages;
  }

  /** Asigna un número o índice para el periodo (base 1). */
  setPeriodIndex(periodIndex: number) {
    this._periodIndex = periodIndex;
  }

  /** Asigna una fecha final para el periodo. */
  setToDate(toDate: Date) {
    this._toDate = toDate;
  }
}

export default DebtTerm;