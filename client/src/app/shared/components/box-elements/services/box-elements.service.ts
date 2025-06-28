import { Injectable } from '@angular/core';
import BoxElementsEventObject from '../interfaces/box-elements-event-object.interface';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BoxElementsService {

  private _$eventEmiter = new Subject<BoxElementsEventObject>();

  constructor() { }

  /** Agrega un elemento a la caja.
   * @param element El elemento que se desea agregar a la caja. Si la caja ya contiene el elemento,
   * la caja lo ignorará y no emitirá "onAdd".
   * @param componentId El identificador de la caja destino.
   */
  addElement(element: string, componentId: string) {
    this._$eventEmiter.next({ componentId, element, eventType: 'add' });
  }

  getEventEmiterAsObservable() {
    return this._$eventEmiter.asObservable();
  }

  /** Elimina un elemento de la caja.
   * @param element El elemento que se desea eliminar de la caja. Si la caja no encuentra el elemento,
   * la caja ignorará la petición y no emitirá "onRemove".
   * @param componentId El identificador de la caja donde se encuentra el elemento.
   */
  removeElement(element: string, componentId: string) {
    this._$eventEmiter.next({ componentId, element, eventType: 'remove' });
  }
}
