import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { APIDebtCalculatorContratoResponse as APIDebtCalculatorContratoResponse, APIDebtCalculatorContractRequest, APIDebtCalculatorDebtDeudaResponse } from '../models/api/debt-calculator.interface';
import env from '../environments/environment';
import Contract from '../models/contract.model';
import Subscriptor from '../models/subscriptor.model';
import DebtCalc from '../models/debt-calc.model';
import DebtTerm from '../models/debt-term.model';
import Package from '../models/package.model';
import Service from '../models/service.model';
import Promotion from '../models/promotion.model';
import PackagePromotion from '../models/packagepromotion.model';

/** Servicio de la API para calcular deuda de contratos. */
@Injectable({
  providedIn: 'root'
})
export class APIDebtCalulatorService {
  constructor(private _http: HttpClient) { }

  /** Obtiene el contrato con el identificador proporcionado. */
  getContract(id: number): Observable<Contract> {
    return this._http.get<APIDebtCalculatorContratoResponse>(`${env.url}contrato/${id}`).pipe(
      map(res => { // Filtra la información y la convierte en una instancia de Contract.

        const auxSubscriptor = new Subscriptor(
          res.idsuscriptor,
          res.suscriptor.nombre,
          res.suscriptor.email,
          res.suscriptor.telefono,
          res.suscriptor.tipo
        );

        return new Contract(
          res.idcontrato,
          new Date(res.fechaContr.split('T')[0]),
          res.precioBase,
          auxSubscriptor,
          [],
          []
        );
      })
    );
  }

  /** Obtiene el cálculo de la deuda para el contrato especificado. */
  calculateDebt(body: APIDebtCalculatorContractRequest): Observable<DebtCalc> {
    return this._http.post<APIDebtCalculatorDebtDeudaResponse>(`${env.url}deuda/calcular`, body).pipe(
      map(res => { // Filtra la información y la convierte en una instancia de DebtCalc.
        // Crea los paquetes.
        const packages: Package[] = res.deuda.paquetes.map(apiPack => new Package(
          apiPack.idpaquete,
          apiPack.nombre,
          0,
          new Date(apiPack.fechaAdicion),
          apiPack.servicios
            .map(apiServ => new Service(apiServ.idservicio, apiServ.cantidad, 0, apiServ.tipo)),
          undefined,
          apiPack.precioBase,
          new Date(apiPack.fechaRetiro)
        ));

        // Crea las promociones.
        const promotions = res.deuda.promociones.map(promotion => new PackagePromotion(
          promotion.idpromocion,
          promotion.nombre,
          promotion.precioPorcen,
          undefined
        ));

        const terms: DebtTerm[] = [];

        // Por cada periodo de la respuesta.
        res.deuda.periodos.forEach(apiTerm => {
          // Lo agrega a la lista de periodos para DebtCalc.
          terms.push(new DebtTerm(
            apiTerm.numPeriodo,
            new Date(apiTerm.desde),
            new Date(apiTerm.hasta),
            // Crea una nueva instancia de Package por periodo.
            apiTerm.paquetes.map(apiPack => {
              const auxPackage = packages.find(p => p.getId() === apiPack.idpaquete)!;
              let auxPromos = promotions
                .filter(promo => apiPack.promociones.find(aP => aP === promo.getId()));

              return new Package(
                auxPackage.getId(),
                auxPackage.getName(),
                auxPackage.getType(),
                auxPackage.getCreationDate()!,
                auxPackage.getServices(),
                // No se crean copias de promociones, todos los paquetes referanciarán las mismas
                // promociones si aplican.
                auxPromos.length > 0 ? auxPromos : undefined,
                auxPackage.getBasePrice(),
                auxPackage.getRemovalFromContract()
              );
            })
          ))
        }); console.log(res);

        return new DebtCalc(
          new Date(res.deuda.desde),
          new Date(res.deuda.hasta),
          terms
        );
      })
    );
  }
}
