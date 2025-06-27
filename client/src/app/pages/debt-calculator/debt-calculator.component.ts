import { Component } from '@angular/core';
import { Subject } from 'rxjs';
import { ButtonComponent } from "../../shared/components/button/button.component";
import { InputbarComponent } from "../../shared/components/input-bar/inputbar.component";
import InputbarFilterObject from '../../shared/components/input-bar/models/inputbarfilterobject.interface';
import Subscriptor from '../../models/subscriptor.model';
import { NgIf } from '@angular/common';
import { DebtTableComponent } from "../../shared/components/debt-table/debt-table.component";
import InputbarValidatorObject from '../../shared/components/input-bar/models/inputbarvalidatorobject.interface';

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
  public subscriptor?: Subscriptor;
  public showTable: boolean = false;

  validators(): InputbarValidatorObject[] {
    return [
      {
        validate: (input?: string) => input === '1',
        rejectPlaceholder: 'Debes ingresar 1'
      }
    ]
  }

  /** @deprecated solo para pruebas */
  auxChange() { }

  // *** EVENTOS ***
  onCalculateBtnAction(s: Subject<any>) {
    this.disableUI = true;
    this.showTable = false;

    setTimeout(() => {
      s.complete();
      this.disableUI = false;
      this.showTable = true;
    }, 2550);
  }

  onContractNumberChange = (input?: string) => {
    this.contractNumber = input ? Number(input) : 0;
  }

  onSearchClientBtnAction(s: Subject<any>) {
    this.disableUI = true;
    this.subscriptor = undefined;
    setTimeout(() => {
      s.complete();
      this.disableUI = false;
      this.subscriptor = new Subscriptor(this.contractNumber, "Juan Escutia", "ejemplo@ejem.com", "331122334455", Subscriptor.TYPE_RESIDENTIAL)
    }, 3000);
  }

  /** Obtiene los filtros para la entrada del número de contrato. */
  getContractNumberFilters(): InputbarFilterObject[] {
    return [{ filter: /[^\d]/g }]
  }
}
