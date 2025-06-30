import { Component, Input } from '@angular/core';
import { ButtonComponent } from "../button/button.component";
import DebtCalc from '../../../models/debt-calc.model';
import { NgFor, NgIf } from '@angular/common';
import Global from '../../objects/global.object';

@Component({
  selector: 'app-debt-table',
  standalone: true,
  imports: [ButtonComponent, NgFor, NgIf],
  templateUrl: './debt-table.component.html',
  styleUrl: './debt-table.component.scss'
})
export class DebtTableComponent {
  /** El cálculo de la deuda */
  @Input({required: true}) public debt!: DebtCalc;
  public currentExpand?: HTMLDivElement;
  /** Acceso a global desde variable local. */
  public global = Global;

  // *** EVENTOS ***
  onDropDownAnimationEnd(e: AnimationEvent) {
    const target = (e.target as HTMLElement) as HTMLDivElement;

    if (target.classList.contains('show')) {
      this.currentExpand = target;
      target.classList.remove('show');
      target.classList.add('idle');
    } else {
      target.classList.remove('hide');
    }
  }

  onExpandBtnClick(btnId?: string) {
    const id = `d${btnId?.split('-')[1]}`;

    this.currentExpand?.classList.remove('idle');
    this.currentExpand?.classList.add('hide');

    if (this.currentExpand?.id !== id) {
      const auxExpand = document.getElementById(id) as HTMLTableRowElement;

      auxExpand.classList.add('show');
    } else this.currentExpand = undefined;
  }
}
