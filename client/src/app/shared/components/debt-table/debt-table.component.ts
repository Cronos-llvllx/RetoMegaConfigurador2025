import { Component } from '@angular/core';
import { ButtonComponent } from "../button/button.component";

@Component({
  selector: 'app-debt-table',
  standalone: true,
  imports: [ButtonComponent],
  templateUrl: './debt-table.component.html',
  styleUrl: './debt-table.component.scss'
})
export class DebtTableComponent {
  public currentExpand?: HTMLTableRowElement;

  // *** EVENTOS ***
  onDropDownAnimationEnd(e: AnimationEvent) {
    const target = (e.target as HTMLElement).parentElement?.parentElement as HTMLTableRowElement;

    console.log(target);

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
