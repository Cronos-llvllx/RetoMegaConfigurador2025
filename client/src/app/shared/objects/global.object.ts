/** Clase para operaciones globales. */
class Global {
  /** Obtiene la diferencia (valor absoluto) de meses entre una fecha y otra. */
  static obtenerDiferenciaDeMeses(origen: Date, destino: Date) {
    let meses = Math.abs((origen.getFullYear() - destino.getFullYear()) * 12);
    meses -= Math.abs(origen.getMonth() - destino.getMonth());

    return Math.abs(meses);
  }

  /** Le da formato a un número como moneda. */
  static formatNumberToCoin(value: number): string {
    const format = new Intl.NumberFormat('es-MX', {
      style: 'currency',
      currency: 'MXN'
    })

    return format.format(value);
  }
}

export default Global;