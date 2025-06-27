import { NgIf } from '@angular/common';
import { AfterViewInit, Component, ElementRef, Input, OnDestroy, Output, ViewChild } from '@angular/core';
import ButtonPlaceholderObject from './models/buttonplaceholderobject.interface';
import ButtonCustomWidthObject from './models/buttoncustomwidthobject.iterface';
import { Subject } from 'rxjs';
import { EventEmitter } from '@angular/core';

@Component({
  selector: 'app-button',
  standalone: true,
  imports: [NgIf],
  templateUrl: './button.component.html',
  styleUrl: './button.component.scss'
})
export class ButtonComponent implements OnDestroy, AfterViewInit {
  /** Referencia al componente. */
  @ViewChild('buttonComponent') private buttonComponent!: ElementRef<HTMLButtonElement>;
  /** Una bandera para habilitar o deshabilitar el componente */
  @Input() disabled?: boolean;
  /** Asinga un identificador para el componente. */
  @Input() id?: string;
  /** Un ícono para el botón. Debe ser la dirección relativa en este proyecto o un archivo
   * en la web. */
  @Input() icon?: string;
  /** Una función que devuelve un Subject y se disparará cuando se haga clic sobre el componente.
   * El componente entrará en estado de espera hasta que el subject sea completado (Subject.complete()). */
  @Output() onAction = new Subject<Subject<any>>();
  /** Una función que se disparará cuando se haga clic sobre el componente. El evento regresará el id
   * del botón si está definido. Si 'onAction' está definido, esta función será ignorada. */
  @Output() onClick = new EventEmitter<string | undefined>();
  @Input() onReject?: (err: Error | string) => void
  /** Una función que se disparará cuando 'onAction' se resuelva. Obtendrá cual sea el resultado
   * que 'onAction' haya devuelto. No se usará esta función si 'onClick' está definida. */
  @Input() onResolve?: (data: any) => void
  /** El objeto de marcadores que puede tener el botón en diferentes casos. */
  @Input() placeholder?: ButtonPlaceholderObject;
  /** Asigna manualmente si el componente está en estado de espera. */
  @Input() waiting: boolean = false;
  /** Longitud personalizada del componente (dinámico, con límite de 100%, por defecto). */
  @Input() width?: ButtonCustomWidthObject;

  private onActionSubscription$?: Subject<any>;
  private onActionResult: any;

  // *** ANGULAR ***
  ngAfterViewInit(): void {
    if (this.width)
      this.buttonComponent.nativeElement.style[this.width.as] = this.width.value
  }

  /** Cierra la subscripción si es que está abierta. */
  ngOnDestroy(): void {
    if (this.onActionSubscription$?.closed === false)
      this.onActionSubscription$.unsubscribe();
  }

  // *** EVENTOS ***
  onButtonClick(e: MouseEvent) {
    if (this.waiting || this.disabled) {
      e.preventDefault();
      return;
    }

    if (this.onAction.observed) { // Verifica si onAction está siendo observado.
      this.waiting = true; // El componente se pone en espera.

      // Cierra la suscripción si es que hay una abierta.
      if (this.onActionSubscription$?.closed === false)
        this.onActionSubscription$.unsubscribe();

      // Crea una nueva suscripción.
      this.onActionSubscription$ = new Subject<any>();
      // Envía el observable al componente padre.
      this.onAction.next(this.onActionSubscription$);
      // Crea una suscripción.
      this.onActionSubscription$.subscribe({
        next: (data: any) => this.onActionResult = data,
        complete: () => {
          this.waiting = false;
          this.onResolve?.(this.onActionResult);
        },
        error: err => {
          this.waiting = false;
          console.error(err);
          this.onReject?.(err);
        },
      });
    } else {
      this.onClick.emit(this.id);
    }
  }

  // *** GET / SET ***
  /** Obtiene la clase del componente. */
  getClass() {
    let result = this.waiting
      ? 'waiting'
      : this.disabled
        ? 'disabled'
        : '';

    return result;
  }

  /** Obtiene el marcador actual del componente. */
  getPlaceholder() {
    return this.waiting
      ? this.placeholder?.waiting || this.placeholder?.default || ''
      : this.disabled
        ? this.placeholder?.disabled || this.placeholder?.default || ''
        : this.placeholder?.default || '';
  }
}
