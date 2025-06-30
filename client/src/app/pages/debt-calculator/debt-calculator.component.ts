import { Component } from '@angular/core';
import { Subject } from 'rxjs';
import { ButtonComponent } from "../../shared/components/button/button.component";
import { InputbarComponent } from "../../shared/components/input-bar/inputbar.component";
import InputbarFilterObject from '../../shared/components/input-bar/models/inputbarfilterobject.interface';
import { NgIf } from '@angular/common';
import { DebtTableComponent } from "../../shared/components/debt-table/debt-table.component";
import { APIDebtCalulatorService } from '../../services/api-debt-calulator.service';
import Contract from '../../models/contract.model';
import { HttpErrorResponse, HttpResponse, HttpStatusCode } from '@angular/common/http';
import DebtCalc from '../../models/debt-calc.model';
import { APIDebtCalculatorContractRequest } from '../../models/api/debt-calculator.interface';
import Global from '../../shared/objects/global.object';

@Component({
  selector: 'app-debt-calculator',
  standalone: true,
  imports: [ButtonComponent, InputbarComponent, NgIf, DebtTableComponent],
  templateUrl: './debt-calculator.component.html',
  styleUrl: './debt-calculator.component.scss'
})
export class DebtCalculator {
  public disableUI: boolean = false;
  public contractNumber: number = 0;
  public contract?: Contract;
  public fromDate?: Date;
  public toDate?: Date;
  public debt?: DebtCalc;

  constructor(private $api: APIDebtCalulatorService) { }

  // *** GETTERS ***
  /** Obtiene los filtros para la entrada del número de contrato. */
  getContractNumberFilters(): InputbarFilterObject[] {
    return [{ filter: /[^\d]/g }]
  }

  /** Obtiene el estado de un contrato. */
  getContractStatus() {
    return this.contract?.getEndDate()
      ? "INACTIVO" : "ACTIVO";
  }

  /** Obtiene el mensaje de error de 'Calcular deuda'. */
  getErrorMessage() {
    if (this.areDatesConsistent())
      return ''
    else {
      const fromDateAsContract = new Date(
        this.fromDate!.getFullYear(),
        this.fromDate!.getMonth() + 1,
        this.contract!.getCreationDate()!.getDate() + 1
      );

      if (this.contract!.getCreationDate()!.getTime() > fromDateAsContract.getTime())
        return 'La fecha de inicio no puede ser menor que la fecha de contratación.';
      if (this.fromDate!.getTime() > this.toDate!.getTime())
        return 'La primera fecha no puede ser mayor a la segunda.';
      else if (Global.obtenerDiferenciaDeMeses(this.toDate!, this.fromDate!) < 1)
        return 'La diferencia entre las fechas debe ser uno o más meses.';
      else
        return 'La fecha final no puede ser mayor a la fecha de cancelación del contrato.';
    }
  }

  // *** VALIDADORES ***
  /** Verifica si el botón para solicitar el cálculo está habilitado. */
  isCalculateBtnEnabled() {
    return !this.disableUI
      && this.fromDate !== undefined
      && this.toDate !== undefined
      && this.areDatesConsistent();
  }

  /** Verifica si las fechas ingresadas son consistentes. */
  areDatesConsistent() {
    // Fechas vacías siempre serán consistentes.
    if (this.fromDate === undefined || this.toDate === undefined)
      return true;

    const fromDateAsContract = new Date(
      this.fromDate.getFullYear(),
      this.fromDate.getMonth() + 1,
      this.contract!.getCreationDate()!.getDate() + 1
    );

    const toDateAsContract = new Date(
      this.toDate.getFullYear(),
      this.toDate.getMonth() + 1,
      (this.contract!.getEndDate()?.getDate() ?? -1) + 1
    );

    // Fecha de inicio igual o mayor que la fecha de contratación.
    return this.contract!.getCreationDate()!.getTime() <= fromDateAsContract.getTime()
      // Fecha final igual o menor que la fecha de cancelación o fecha de cancelación en undefined.
      && (!this.contract!.getEndDate()
        // Fecha de inicio menor a la fecha final.
        || this.contract!.getEndDate()!.getTime() >= toDateAsContract.getTime()
        // Fecha de inicio menor a fecha final.
      ) && this.fromDate.getTime() < this.toDate.getTime()
      // Margen de un mes o más.
      && Global.obtenerDiferenciaDeMeses(this.toDate, this.fromDate) >= 1;
  }

  // *** EVENTOS ***
  onCalculateBtnAction(s: Subject<any>) {
    this.disableUI = true;
    this.debt = undefined;

    const fromDateAsContract = new Date(
      this.fromDate!.getFullYear(),
      this.fromDate!.getMonth() + 1,
      this.contract!.getCreationDate()!.getDate() + 1
    );

    const toDateAsContract = new Date(
      this.toDate!.getFullYear(),
      this.toDate!.getMonth() + 1,
      (this.contract!.getEndDate()?.getDate() ?? this.contract!.getCreationDate()!.getDate()) + 1
    );

    const body: APIDebtCalculatorContractRequest = {
      idcontrato: this.contract?.getId()!,
      FechaInicio: fromDateAsContract.toISOString().split('T')[0]!,
      FechaFin: toDateAsContract.toISOString().split('T')[0]!
    }

    this.$api.calculateDebt(body).subscribe({
      next: debtCalc => {
        this.debt = debtCalc;
        this.disableUI = false;

        s.complete();
      },
      error: (err: HttpErrorResponse) => {
        console.error(err);

        if (err.status == HttpStatusCode.NotFound) {
          alert(`No se encontró ningún contrato con el número ${this.contractNumber}`);
        } else if (err.status == HttpStatusCode.BadRequest) {
          alert(`Error en la petición`);
        } else {
          alert(`Ocurrió un error desconocido: ${err.message}`)
        }

        this.disableUI = false;

        s.complete();
      }
    });
  }

  onContractNumberChange = (input?: string) => {
    this.contractNumber = input ? Number(input) : 0;
  }

  /** Cuando la fecha 'desde' cambia. */
  onFromDateInputChange(input?: string) {
    this.fromDate = input
      ? new Date(input)
      : undefined;
  }

  /** Cuando se hace clic en el botón 'buscar'. */
  onSearchClientBtnAction(s: Subject<any>) {
    this.disableUI = true;
    this.debt = undefined;
    this.contract = undefined;
    this.fromDate = undefined;
    this.toDate = undefined;

    this.$api.getContract(this.contractNumber).subscribe({
      next: contract => {
        this.disableUI = false;
        this.contract = contract;
        s.complete();
      },
      error: (err: HttpErrorResponse) => {
        console.error(err);

        if (err.status == HttpStatusCode.NotFound) {
          alert(`No se encontró ningún contrato con el número ${this.contractNumber}`);
        } else {
          alert(`Ocurrió un error desconocido: ${err.message}`)
        }

        this.disableUI = false;
        s.complete();
      },
    });
  }

  /** Cuando la fecha 'hasta' cambia. */
  onToDateInputChange(input?: string) {
    this.toDate = input
      ? new Date(input)
      : undefined;
  }
}
