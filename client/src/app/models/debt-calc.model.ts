import DebtTerm from "./debt-term.model";

/** Clase para el cálculo de una deuda. */
class DebtCalc {
  /**
   * @param _fromDate La fecha por la que se inició el cálculo.
   * @param _toDate La fecha por la que se terminó el cálculo.
   * @param _terms // La lista de periodos (mensuales) del cálculo.
   */
  constructor(
    private _fromDate: Date,
    private _toDate: Date,
    private _terms: DebtTerm[]
  ) { }

  /** Obtiene la fecha de inicio del cálculo. */
  getFromDate() {
    return this._fromDate;
  }
  
  /** Obtiene la lista de periodos (mensual) del cálculo. */
  getTerms() {
    return this._terms;
  }

  /** Obtiene la fecha final del cálculo. */
  getToDate() {
    return this._toDate;
  }

  /** Asgina una fecha de inicio para el cálculo. */
  setFromDate(fromDate: Date) {
    this._fromDate = fromDate;
  }

  /** Asigna una lista de periodos para el cálculo. */
  setTerms(periods: DebtTerm[]) {
    this._terms = periods;
  }

  /** Asigna una fecha final para el cálculo. */
  setToDate(toDate: Date) {
    this._toDate = toDate;
  }
}

export default DebtCalc;