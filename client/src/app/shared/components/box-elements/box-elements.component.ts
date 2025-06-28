import { Component, ElementRef, EventEmitter, Input, OnDestroy, OnInit, Output, ViewChild } from '@angular/core';
import { BoxElementsService } from './services/box-elements.service';
import { Subscription } from 'rxjs';
import { NgFor } from '@angular/common';

@Component({
  selector: 'app-box-elements',
  standalone: true,
  imports: [NgFor],
  templateUrl: './box-elements.component.html',
  styleUrl: './box-elements.component.scss'
})
export class BoxElementsComponent implements OnInit, OnDestroy {
  @ViewChild('elementsContainer') elementsContainer!: ElementRef<HTMLDivElement>;
  /** Evento que se dispara cuando un nuevo elemento se agrega. Se activará si el elemento
   * no estaba en la lista anteriormente. Manda el nuevo elemento. */
  @Output() onAdd = new EventEmitter<string>();
  /** Evento que se dispara cuando un elemento existente se elimina. Manda el elemento eliminado. */
  @Output() onRemove = new EventEmitter<string>();
  /** Elementos por defecto para cuando el componente cargue. Se usará una copia de este arreglo. */
  @Input() defaultValues?: string[];
  /** El identificador del componente. Es obligatorio y debes asegurarte que sea único en caso de
   * que uses varias instancias a la vez. */
  @Input({ required: true }) id!: string;

  private _elements: string[] = [];
  private _$subscription!: Subscription;

  constructor(private _bEService: BoxElementsService) { }

  ngOnInit(): void {
    if (this.defaultValues && this.defaultValues.length > 0)
      this._elements = this.defaultValues.map(dV => dV);

    this._$subscription = this._bEService.getEventEmiterAsObservable().subscribe({
      next: e => {
        if (e.eventType === 'add') {
          if (!this._elements.includes(e.element)) {
            this._elements.push(e.element);
            this._elements = [...this._elements];
            this.onAdd.emit(e.element);
          }
        } else {
          const eIdx = this._elements.findIndex(eE => eE === e.element);

          if (eIdx !== -1) {
            const elem = this._elements.splice(eIdx, 1)[0];
            this.onRemove.emit(elem);
          }
        }
      }
    });
  }

  ngOnDestroy(): void {
    this._$subscription.unsubscribe();
  }

  /** Cuando se hace clic sobre un elemento de la caja. */
  onElementRemoveBtnClick(e: MouseEvent) {
    const target = e.target as HTMLElement;
    const parent = target.parentElement;

    // Filtra los clics inoportunos. Solo detectará los que fueron hechos dentro del botón X.
    if (target.classList.contains('button-sim') || parent?.classList.contains('button-sim')) {
      const auxE = (target.classList.contains('button-sim')
        ? target : parent
      )?.parentElement?.parentElement;

      const eIdx = Array
        .from(this.elementsContainer.nativeElement.children)
        .findIndex(el => el === auxE);

      if (eIdx >= 0 && eIdx < this._elements.length) // Emite el evento al servicio.
        this._bEService.removeElement(this._elements[eIdx], this.id);
    }
  }

  /** Obtiene la lista de elementos. */
  getElements() {
    return this._elements;
  }
}
