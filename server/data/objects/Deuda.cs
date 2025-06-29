using megaapi.models;

namespace megaapi.data.objects;

/// <summary>Clase estática para calcular la deuda de un contrato dentro de un periodo de tiempo</summary>
public static class Deuda
{
  /// <summary>
  /// Realiza el cálculo de la deuda de un contrato en un periodo determinado.
  /// </summary>
  /// <param name="desdeFecha">A partir de qué fecha se realizará el cálculo</param>
  /// <param name="hastaFecha">Hasta qué fecha se realizará el cálculo.</param>
  /// <param name="contrato">Instancia del contrato al que se le aplicará el cálculo.</param>
  /// <param name="contratoPaquetes">La lista de paquetes que incluye el contrato.</param>
  /// <param name="ciudadPromos">La lista de promociones aplicables a las ciudades.</param>
  /// <param name="coloniaPromos">La lista de promociones aplicables a las colonias.</param>
  /// <param name="paquetePromos">La lista de promociones que se aplican a paquetes específicos
  /// (debe estar filtrato para solo los paquetes que son parte del contrato).</param>
  /// <returns></returns>
  public static DeudaResDto Calcular(DateTime desdeFecha, DateTime hastaFecha, ContratoExt contrato, List<PromocionCiudad> ciudadPromos, List<PromocionColonia> coloniaPromos, List<PromocionPaquete> paquetePromos)
  {
    if (desdeFecha.Year > hastaFecha.Year
     || (desdeFecha.Year == hastaFecha.Year && desdeFecha.Month >= hastaFecha.Month))
      throw new InvalidDataException("La fecha origen no debe ser mayor o igual que la fecha destino.");
    else if (contrato.FechaContr > desdeFecha)
      throw new InvalidDataException("La fecha origen no debe ser mayor a la fecha de contratación.");

    DateTime fechaContratacion = contrato.FechaContr; // Fecha de contratación.
    int idCiudadSuscriptor = contrato.Suscriptor.Colonia.Idciudad; // Ciudad del suscriptor.
    int idColoniaSuscriptor = contrato.Suscriptor.Idcolonia; // Colonia del suscriptor.

    // ** Reducir arreglos de promociones.
    // Promociones que se aplican solo por ciudad.
    var promocionesPorCiudad = FiltrarPromocionesPorCiudad(
      fechaContratacion,
      idCiudadSuscriptor,
      ciudadPromos,
      paquetePromos
    );

    // Promociones que se aplican solo por colonia.
    var promocionesPorColonia = FiltrarPromocionesPorColonia(
      fechaContratacion,
      idColoniaSuscriptor,
      coloniaPromos,
      paquetePromos
    );

    // Promociones que se aplican por paquete (por colonia o por ciudad a la vez).
    var promocionesPorPaquete = FiltrarPromocionesPorPaquete(
      fechaContratacion,
      idCiudadSuscriptor,
      idColoniaSuscriptor,
      paquetePromos,
      ciudadPromos,
      coloniaPromos
    );

    // Generación del objeto respuesta.
    var deudaRes = GenerarObjetoDeudaResDto(
      contrato,
      desdeFecha,
      hastaFecha,
      promocionesPorCiudad,
      promocionesPorColonia,
      promocionesPorPaquete
    );

    // Inserta los periodos (con promociones) al objeto de respuesta.
    GenerarPeriodos(deudaRes, contrato, promocionesPorCiudad, promocionesPorColonia, promocionesPorPaquete);

    return deudaRes;
  }


  /// <summary>
  /// Calcula la diferencia, en meses, entre fechas. Siempre regresará positivo.
  /// </summary>
  /// <param name="fechaOrigen">Fecha de inicio</param>
  /// <param name="fechaDestino">Fecha fin.</param>
  /// <returns></returns>
  public static double DiferenciaDeFechas(DateTime fechaOrigen, DateTime fechaDestino)
  {
    // Calcula total de meses entre las fechas.
    int mesesTotales = (fechaDestino.Year - fechaOrigen.Year) * 12 + fechaDestino.Month - fechaOrigen.Month;
    // Intenta asimilar la fecha destino
    DateTime ancla = fechaOrigen.AddMonths(mesesTotales);

    double ajusteDeFecha;

    // La fecha destino es mayor o igual a la del intento se similitud.
    if (fechaDestino >= ancla) // Diferencia entre las dos fechas, dividido entre días en ese mes.
      ajusteDeFecha = (fechaDestino - ancla).TotalDays / DateTime.DaysInMonth(ancla.Year, ancla.Month);
    else // La fecha destino es menor al intento de similitud.
    {
      ancla = fechaOrigen.AddMonths(mesesTotales - 1);
      ajusteDeFecha = (fechaDestino - ancla).TotalDays / DateTime.DaysInMonth(ancla.Year, ancla.Month);
      mesesTotales--;
    }

    double diferencia = mesesTotales + ajusteDeFecha;

    return diferencia < 0 ? diferencia * -1 : diferencia;
  }

  /// <summary>
  /// Genera un objeto de respuesta. No genera los periodos (solo pone una lista vacía).
  /// </summary>
  /// <param name="contrato">El contrato a partir del cual se generará el objeto.</param>
  /// <param name="desdeFecha">La fecha orignen del cálculo de la deuda.</param>
  /// <param name="hastaFecha">La fecha destino del cálculo de la deuda.</param>
  /// <param name="promocionesPorCiudad">Lista de promociones aplicables solo para ciudades
  /// (no paquetes).</param>
  /// <param name="promocionesPorColonia">Lista de promociones aplicables solo para colonias
  /// (no paquetes)</param>
  /// <param name="promocionesPorPaquete">Lista de promociones aplicables para paquetes (puede
  /// aplicar, a la vez, ciudades o colonias).</param>
  /// <returns>Una instancia de DeudaResDto.</returns>
  public static DeudaResDto GenerarObjetoDeudaResDto(ContratoExt contrato, DateTime desdeFecha, DateTime hastaFecha, List<PromocionCiudad> promocionesPorCiudad, List<PromocionColonia> promocionesPorColonia, List<PromocionPaquete> promocionesPorPaquete)
  {
    var auxDto = new DeudaResDto
    {
      Idsuscriptor = contrato.Idsuscriptor,
      Idcontrato = contrato.Idcontrato,
      Deuda = new DeudaDto
      {
        Desde = desdeFecha.ToString("yyyy-MM-dd"),
        Hasta = hastaFecha.ToString("yyyy-MM-dd"),
        Paquetes = [.. contrato.Paquetes.Select(paq => new PaqueteDto
        {
          Idpaquete = paq.Idpaquete,
          FechaAdicion = paq.FechaAdicion.ToString("yyyy-MM-dd"),
          FechaRetiro = paq.FechaRetiro?.ToString("yyyy-MM-dd"),
          Nombre = paq.Nombre,
          PrecioBase = paq.PrecioBase,
          Servicios = [.. paq.Servicios.Select(s => new ServicioDto {
            Idservicio = s.Idservicio,
            Cantidad = s.Cantidad,
            Tipo = s.Tipo
          })]
        })],
        Periodos = [],
        Promociones = [
          .. promocionesPorCiudad.Select(pCiu => new PromocionDto {
            Idpromocion = pCiu.Idpromocion,
            Nombre = pCiu.Promocion.Nombre,
            PrecioPorcen = pCiu.Promocion.PrecioPorcen
          }),
          .. promocionesPorColonia.Select(pCol => new PromocionDto {
            Idpromocion = pCol.Idpromocion,
            Nombre = pCol.Promocion.Nombre,
            PrecioPorcen = pCol.Promocion.PrecioPorcen
          }),
          .. promocionesPorPaquete.Select(pPaq => new PromocionDto {
            Idpromocion = pPaq.Idpromocion,
            Nombre = pPaq.Promocion.Nombre,
            PrecioPorcen = pPaq.Promocion.PrecioPorcen
          })
        ]
      }
    };

    // Eliminando duplicados de promociones...
    List<PromocionDto> auxPromociones = [];

    auxDto.Deuda.Promociones.ToList().ForEach(p =>
    {
      if (!auxPromociones.Exists(aP => aP.Idpromocion == p.Idpromocion))
        auxPromociones.Add(p);
    });

    auxDto.Deuda.Promociones = auxPromociones;

    return auxDto;
  }

  /// <summary>
  /// Genera los periodos y los inserta al objeto de respuesta.
  /// </summary>
  /// <param name="deudaRes">El objeto de respuesta al que se le generarán los periodos.</param>
  /// <param name="contrato">El contrato relacionado al objeto de respuesta.</param>
  /// <param name="promocionesPorCiudad">La lista de promociones solo para ciudad.</param>
  /// <param name="promocionesPorColonia">La lista de promociones solo para colonia.</param>
  /// <param name="promocionesPorPaquete">La lista de promociones para paquetes específicos.</param>
  public static void GenerarPeriodos(DeudaResDto deudaRes, ContratoExt contrato, List<PromocionCiudad> promocionesPorCiudad, List<PromocionColonia> promocionesPorColonia, List<PromocionPaquete> promocionesPorPaquete)
  {
    if (deudaRes.Deuda.Periodos.Count > 0)
      deudaRes.Deuda.Periodos = [];

    // Inserción de periodos (iteraciones).
    DateTime fechaActual = DateTime.Parse(deudaRes.Deuda.Desde);
    DateTime fechaDestino = DateTime.Parse(deudaRes.Deuda.Hasta);
    // Diferencia entre periodos por ciclo.
    int numPeriodo = 1;

    while (fechaActual < fechaDestino)
    {
      // Crea las fechas de inicio y fin del periodo.
      DateTime desdePeriodo = fechaActual;
      DateTime hastaPeriodo = fechaActual.AddMonths(1);

      // Inicializa un objeto de PeriodoDto.
      PeriodoDto auxPeriodo = new()
      {
        NumPeriodo = numPeriodo,
        // Asigna la fecha de inicio del periodo
        Desde = desdePeriodo.AddDays(numPeriodo == 1 ? 0 : 1).ToString("yyyy-MM-dd"),
        // Asigna la fecha de fin del periodo.
        Hasta = hastaPeriodo.ToString("yyyy-MM-dd"),
        Paquetes = []
      };

      // Asigna los paquetes y promociones aplicables durante el periodo.
      contrato.Paquetes.ToList().ForEach(revisionPaqCon =>
      {
        bool dentroDePeriodo = revisionPaqCon.FechaAdicion <= desdePeriodo;
        bool noCancelado = revisionPaqCon.FechaRetiro == null
          || revisionPaqCon.FechaRetiro < hastaPeriodo;

        // El paquete entra en este periodo.
        if (dentroDePeriodo && noCancelado)
        {
          PaqueteReducidoDto auxPaqueteReducido = new()
          {
            Idpaquete = revisionPaqCon.Idpaquete,
            Promociones = []
          };

          // Aplicando promociones por ciudad.
          promocionesPorCiudad.ForEach(promoCiu =>
          {
            // Toma la diferencia entre la fecha de adición del paquete y desdePeriodo.
            bool revPaqAdiDiferenciaPeriodo = promoCiu.Promocion.Duracion == null
              || DiferenciaDeFechas(revisionPaqCon.FechaAdicion, desdePeriodo) < promoCiu.Promocion.Duracion;

            // Fecha de registro de promoción menor a la fecha del periodo.
            bool valid = promoCiu.Promocion.FechaRegistro < desdePeriodo
              // duración de la promoción nulo o mayor que el número de periodo actual.
              && revPaqAdiDiferenciaPeriodo;

            if (valid)
              auxPaqueteReducido.Promociones.Add(promoCiu.Idpromocion);
          });

          // Aplicando promociones por colonia.
          promocionesPorColonia.ForEach(promoCol =>
          {
            // Toma la diferencia entre la fecha de adición del paquete y desdePeriodo.
            bool revPaqAdiDiferenciaPeriodo = promoCol.Promocion.Duracion == null
              || DiferenciaDeFechas(revisionPaqCon.FechaAdicion, desdePeriodo) < promoCol.Promocion.Duracion;

            // Fecha de registro de promoción menor a la fecha del periodo.
            bool valid = promoCol.Promocion.FechaRegistro < desdePeriodo
              // duración de la promoción nulo o mayor que el número de periodo actual.
              && revPaqAdiDiferenciaPeriodo;

            if (valid)
            {
              auxPaqueteReducido.Promociones.Add(promoCol.Idpromocion);
            }
          });

          // Aplicando promociones por paquete específido.
          promocionesPorPaquete.ForEach(promoPaq =>
          {
            // Tipo de alcance.
            byte? alcance = promoPaq.Promocion.Alcance;
            // Toma similitudes entre ids de paquetes entre promo y paquete.
            bool revPaqIdpaqIgualpromIdpaq = promoPaq.Idpaquete == revisionPaqCon.Idpaquete;
            // Toma si la promoción aplica por vigencia.
            bool promoAplicaPorVigencia = promoPaq.Promocion.Vigencia >= revisionPaqCon.FechaAdicion;
            // Toma si la promoción aplica para nuevos paquetes (alcance).
            bool promoAplicaPorAlcance = alcance == 2
              || revisionPaqCon.FechaAdicion >= promoPaq.Promocion.FechaRegistro;
            // Toma la diferencia entre la fecha de adición del paquete y desdePeriodo.
            bool revPaqAdiDiferenciaPeriodo = promoPaq.Promocion.Duracion == null
              || DiferenciaDeFechas(revisionPaqCon.FechaAdicion, desdePeriodo) < promoPaq.Promocion.Duracion;

            // Los identificadores entre paquetes coinciden.
            bool valid = revPaqIdpaqIgualpromIdpaq
              // Aplica por vigencia
              && promoAplicaPorVigencia
              // Aplica solo para nuevas contrataciones o para todas.
              && promoAplicaPorAlcance
              // Diferencia de duración-periodo
              && revPaqAdiDiferenciaPeriodo;

            if (valid)
              auxPaqueteReducido.Promociones.Add(promoPaq.Idpromocion);
          });
          auxPeriodo.Paquetes.Add(auxPaqueteReducido);
        }
      });

      // Agrega el periodo al objeto de respuesta.
      deudaRes.Deuda.Periodos.Add(auxPeriodo);

      // Actualización de variables.
      fechaActual = hastaPeriodo;
      numPeriodo++;
    }
  }

  /// <summary>
  /// Filtra las promociones que no sean específicas para paquetes y que aplican para
  /// la fecha de contratación, alcance y ciudad del suscriptor, devolviendo solo las
  /// promociones aplicables para la ciudad del suscriptor.
  /// </summary>
  /// <param name="fechaContratacion">La fecha de contratación.</param>
  /// <param name="idCiudadSuscriptor">El id de la ciudad donde el suscriptor vive.</param>
  /// <param name="ciudadPromos">La lista de promociones de la ciudad que se filtrará.</param>
  /// <param name="paquetePromos">La lista de promociones por paquete a comparar.</param>
  public static List<PromocionCiudad> FiltrarPromocionesPorCiudad(DateTime fechaContratacion, int idCiudadSuscriptor, List<PromocionCiudad> ciudadPromos, List<PromocionPaquete> paquetePromos)
  {
    List<PromocionCiudad> promocionesPorCiudad = [];

    ciudadPromos.ForEach(promo =>
    {
      // Aplica para la ciudad del suscriptor.
      bool valid = ciudadPromos.Exists(ciuPromo => ciuPromo.Idpromocion == promo.Idpromocion
          && ciuPromo.Idciudad == idCiudadSuscriptor)
        // Alcance.
        && (promo.Promocion.Alcance == 2 || promo.Promocion.FechaRegistro < fechaContratacion)
        // Vigencia.
        && promo.Promocion.Vigencia > fechaContratacion
        // No sea para paquetes específicos.
        && !paquetePromos.Exists(paqPromo => paqPromo.Idpromocion == promo.Idpromocion);

      if (valid)
        promocionesPorCiudad.Add(promo);
    });

    return promocionesPorCiudad;
  }

  /// <summary>
  /// Filtra las promociones que no sean específicas para paquetes y que aplican para
  /// la fecha de contratación, alcance y colonia del suscriptor, devolviendo solo las
  /// promociones aplicables para la colonia del suscriptor.
  /// </summary>
  /// <param name="fechaContratacion">La fecha de contratación.</param>
  /// <param name="idColoniaSuscriptor">El id de la colonia donde el suscriptor vive.</param>
  /// <param name="coloniaPromos">La lista de promociones de la colonia que se filtrará.</param>
  /// <param name="paquetePromos">La lista de promociones por paquete a comparar.</param>
  public static List<PromocionColonia> FiltrarPromocionesPorColonia(DateTime fechaContratacion, int idColoniaSuscriptor, List<PromocionColonia> coloniaPromos, List<PromocionPaquete> paquetePromos)
  {
    List<PromocionColonia> promocionesPorColonia = [];

    coloniaPromos.ForEach(promo =>
    {
      // Aplica para la colonia del suscriptor.
      bool valid = coloniaPromos.Exists(colPromo => colPromo.Idpromocion == promo.Idpromocion
          && colPromo.Idcolonia == idColoniaSuscriptor)
        // Alcance.
        && (promo.Promocion.Alcance == 2 || promo.Promocion.FechaRegistro < fechaContratacion)
        // Vigencia.
        && promo.Promocion.Vigencia > fechaContratacion
        // No sea para paquetes específicos.
        && !paquetePromos.Exists(paqPromo => paqPromo.Idpromocion == promo.Idpromocion);

      if (valid)
        promocionesPorColonia.Add(promo);
    });

    return promocionesPorColonia;
  }

  /// <summary>
  /// Filtra las promociones aplicados por paquete y, si aplica, por ciudad o colonia (promociones
  /// compuestas).
  /// </summary>
  /// <param name="fechaContratacion">La fecha de contratación.</param>
  /// <param name="idCiudadSuscriptor">El id de la ciudad donde el suscriptor vive.</param>
  /// <param name="idColoniaSuscriptor">El id de la colonia donde el suscriptor vive.</param>
  /// <param name="paquetePromos">La lista de promociones que se filtrará. Esta lista ya debe
  /// estar filtrada para los paquetes que conforman el contrato del cliente.</param>
  /// <param name="coloniaPromos">La lista de promociones por colonia que se comparará.</param>
  /// <param name="ciudadPromos">La lista de promociones por ciudad que se comparará.</param>
  public static List<PromocionPaquete> FiltrarPromocionesPorPaquete(DateTime fechaContratacion, int idCiudadSuscriptor, int idColoniaSuscriptor, List<PromocionPaquete> paquetePromos, List<PromocionCiudad> ciudadPromos, List<PromocionColonia> coloniaPromos)
  {
    List<PromocionPaquete> promocionesPorPaquete = [];

    paquetePromos.ForEach(promo =>
    {
      // Alcance.
      bool valid = (promo.Promocion.Alcance == 2 || promo.Promocion.FechaRegistro < fechaContratacion)
        // Vigencia.
        && promo.Promocion.Vigencia > fechaContratacion
        // No existe la promoción por ciudades o existe una promoción específica.
        && (!ciudadPromos.Exists(ciuPromo => ciuPromo.Idpromocion == promo.Idpromocion)
          || ciudadPromos.Exists(ciuPromo => ciuPromo.Idpromocion == promo.Idpromocion && ciuPromo.Idciudad == idCiudadSuscriptor))
        // No existe la promoción por colonias o existe una promoción específica.
        && (!coloniaPromos.Exists(ciuPromo => ciuPromo.Idpromocion == promo.Idpromocion)
          || coloniaPromos.Exists(colPromo => colPromo.Idpromocion == promo.Idpromocion && colPromo.Idcolonia == idColoniaSuscriptor));

      if (valid)
        promocionesPorPaquete.Add(promo);
    });

    return promocionesPorPaquete;
  }
}

/// <summary>Dto utilizado para enviar como respuesta en los controladores.</summary>
public class DeudaResDto
{
  public int Idsuscriptor { get; set; }
  public int Idcontrato { get; set; }
  public DeudaDto Deuda { get; set; } = null!;
}

/// <summary>Dto que implementa DeudaResDto</summary>
public class DeudaDto
{
  public string Desde { get; set; } = null!;
  public string Hasta { get; set; } = null!;
  public ICollection<PeriodoDto> Periodos { get; set; } = null!;
  public ICollection<PaqueteDto> Paquetes { get; set; } = null!;
  public ICollection<PromocionDto> Promociones { get; set; } = null!;
}

public class PeriodoDto
{
  public int NumPeriodo { get; set; }
  public string Desde { get; set; } = null!;
  public string Hasta { get; set; } = null!;
  public ICollection<PaqueteReducidoDto> Paquetes { get; set; } = null!;
}

public class PaqueteReducidoDto
{
  public int Idpaquete { get; set; }
  public ICollection<int> Promociones { get; set; } = null!;
}

public class PaqueteDto
{
  public int Idpaquete { get; set; }
  public string Nombre { get; set; } = null!;
  public string FechaAdicion { get; set; } = null!;
  public string? FechaRetiro { get; set; }
  public decimal PrecioBase { get; set; }
  public ICollection<ServicioDto> Servicios { get; set; } = null!;
}

public class ServicioDto
{
  public int Idservicio { get; set; }
  public int? Cantidad { get; set; }
  public int Tipo { get; set; }
}

public class PromocionDto
{
  public int Idpromocion { get; set; }
  public string Nombre { get; set; } = null!;
  public decimal PrecioPorcen { get; set; }
}
