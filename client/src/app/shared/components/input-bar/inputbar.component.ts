import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import InputPlaceholderObject from './models/inputbarplaceholderobject.interface';
import InputbarFilterObject from './models/inputbarfilterobject.interface';
import InputbarValidatorObject from './models/inputbarvalidatorobject.interface';

@Component({
  standalone: true,
  selector: 'app-inputbar',
  imports: [],
  templateUrl: './inputbar.component.html',
  styleUrl: './inputbar.component.scss'
})
export class InputbarComponent implements OnInit {
  /** Alineación del componente (cuando se define width en pixeles o menor al 100%). Puede fallar si el
   * componente está dentro de etiquetas flex. */
  @Input() alignment: 'left' | 'center' = 'left';
  /** El valor del componente al crearse (vacío por defecto). */
  @Input() defaultValue: string = ''
  /** Si 'true', el componente no podrá cambiar ('false' por defecto). */
  @Input() disabled: boolean = false;
  /** Filtros para aplicar a la entrada. Las entradas vacías siempre pasarán los filtros. Si
   * un filtro no tiene definida una expresión regular, 'replacer' se asignara o ejecutará
   * siempre.*/
  @Input() filters?: InputbarFilterObject[]
  /** El tamaño máximo de la entrada. Debe ser un valor mayor que cero y mayor que minLenght. */
  @Input() maxLength: number = 0;
  /** El tamaño mínimo de la entrada. Debe ser un valor mayor que cero. */
  @Input() minLength: number = 0;
  /** Cuando el componente deja de ser enfocado, se activa este disparador con el valor actual
   * del componente. Si el valor que regresa el disparador es diferente al valor actual del
   * componente, entonces se reemplazará con el valor del disparador, eliminará los mensajes de
   * error y activará 'onChange'. Este nuevo valor no pasará por los validadores ni los filtros.  */
  @Input() onBlur?: (input?: string) => string | undefined
  /** Un disparador que se activará cuando el componente cambia de valor. Si se definen filtros,
   * la entrada pasará por ellos antes de emitir el evento (no aplican cuando "type" es 'date' o
   * 'month', ). Si se definen validadores, la entrada pasará por ellos y no pasa algunon de ellos,
   * se emitirá 'undefined'. */
  @Output() onChange = new EventEmitter<string | undefined>();
  /** Contiene los valores para usar en el cabezal del componente */
  @Input() placeholder: InputPlaceholderObject = { default: 'Inputbar' }
  /** El tipo de entrada. Solo especifica el tipo de teclado (en dispositivos compatibles). */
  @Input() type: 'date' | 'email' | 'month' | 'number' | 'password' | 'tel' | 'text' = 'text'
  /** Un validador o arreglo de validadores para la entrada. Se llama(n) cada vez que el componente
   * cambia de valor. Los validadores no se tomarán en cuenta cuando la cadena es indefinida.*/
  @Input() validators?: InputbarValidatorObject[]
  /** Longitud personalizada del componente (100% por defecto). Se considera como la longitud máxima, por lo
   * que puede cambiar si la pantalla se redimensiona si se usa un porcentaje. No puede ser menor a 1. Puede
   * usar cualquier unidad admitida. Si solo es un número, se considerará en pixeles. Puede fallar si
   * el componente está dentro de etiquetas flex. */
  @Input() width?: string;

  // Se almacena en un objeto para que Angular no detecte cambios cuando se asignan
  // nuevos valores a sus atributos.
  private state = { value: this.defaultValue, placeholder: this.placeholder.default }
  private statusClass: '' | 'err ml' | 'err nv' = '';

  // *** ANGULAR ***
  /** Se utiliza para verificar que las propiedades sean consistentes. */
  ngOnInit(): void {
    // Necesario para que state tenga el marcador ingresado por el usuario.
    this.state.placeholder = this.placeholder.default;

    if (this.minLength > 0) {
      if (this.maxLength > 0 && this.minLength > this.maxLength) {
        throw new Error('Inputbar.minLength cannot be greater than Inputbar.maxLength');
      }
    }

    if (this.maxLength > 0) {
      if (this.minLength > 0 && this.maxLength < this.minLength) {
        throw new Error('Inputbar.maxLength cannot be lesser than Inputbar.minLength');
      }
    }

  }

  // *** EVENTOS ***
  /** Evento para cuando el componente deja de ser enfocado. */
  onInputBlur() {
    const auxVal = this.onBlur && this.onBlur(this.state.value);

    // Reemplaza el valor actual.
    if (auxVal !== this.state.value) {
      this.state.value = auxVal ?? '';
      this.onChange.emit(auxVal);
    }
  }

  /** Evento para cuando el componente cambia. */
  onInputChange(e: Event) {
    // Obtiene el elemento de entrada.
    const target = (e.target as HTMLInputElement);

    if (this.disabled) { // Evita que el componente y su valor cambien si está desactivado.
      e.preventDefault();
      target.value = this.state.value;
      return;
    }

    let auxInput = target.value || undefined;

    // Filtra la entrada actual.
    if (this.type !== 'date' && this.type !== 'month')
      auxInput = this.filterInput(target.value || undefined)

    // Revisa que cumpla con el tamaño máximo de entrada
    if (auxInput && !this.checkMaxLength(auxInput))
      auxInput = auxInput.substring(0, this.maxLength);

    target.value = auxInput ?? ''; // Actualiza el valor del componente

    if (!this.checkMinLength(auxInput)) {
      // La entrada no cumple con la longitud mínima.
      auxInput = undefined;
      this.statusClass = 'err ml';
    } else if (!this.checkValidators(auxInput)) {
      // La entrada no cumple con uno, varios o todos los validadores.
      auxInput = undefined;
      this.statusClass = 'err nv'
    } else {
      // La entrada cumple con todos los requisitos.
      this.statusClass = '';
    }

    this.state.value = auxInput ?? ''; // Actualiza el estado del valor actual.
    this.onChange.emit(auxInput); // Activa función de llamada de vuelta asignada.
  }

  // *** GENÉRICOS ***
  /** Revisa el tamaño máximo de entrada. Siempre se cumplirá si input es indefinido. */
  private checkMaxLength(input?: string): boolean {
    return this.maxLength <= 0 || input === undefined || input.length <= this.maxLength;
  }

  /** Revisa el tamaño mínimo de entrada. Siempre se cumplirá si input es indefinido. */
  private checkMinLength(input?: string): boolean {
    if (input && this.minLength > 0 && input.length < this.minLength) {
      this.state.placeholder = this.placeholder.onMinLengthNotMet
        ?? this.maxLength > 0
        ? `La entrada debe tener de ${this.minLength} a ${this.maxLength} caracteres`
        : `La entrada debe tener ${this.minLength} o más caracteres`;

      return false;
    }

    return true;
  }

  /** Revisa que los validadores se cumplan. Siempre se cumplirán si la entrada es indefinido. */
  private checkValidators(input?: string): boolean {
    if (input) {
      const rejectValtr = this.validators?.find(v => !v.validate(input));

      if (rejectValtr) {
        this.state.placeholder = rejectValtr.rejectPlaceholder
          || this.placeholder.onNotValid
          || this.placeholder.default;

        return false;
      }
    }

    this.state.placeholder = this.placeholder.default
    return true;
  }

  /** Filtra la entrada con los filtros recibidos (si existen). */
  private filterInput(input?: string): string | undefined {
    let auxInput = input;

    if (auxInput) {
      this.filters?.forEach(ibFilter => {
        if (typeof ibFilter.filter !== 'function')
          auxInput = auxInput?.replace(ibFilter.filter, ibFilter.replacer ?? '');
        else
          auxInput = ibFilter.filter(auxInput);
      });
    }

    return auxInput;
  }

  /** Obtiene el estilo personalizado para el componente. */
  getCustomStyle() {
    const style: any = {};

    if (this.width && (isNaN(Number(this.width)) || Number(this.width) >= 1)) {
      style["max-width"] = isNaN(Number(this.width)) ? this.width : `${this.width}px`;
      style["margin"] = `0 ${this.alignment === 'center' ? 'auto' : '0'}`;
    }

    return style;
  }

  // *** GET / SET ***
  /** Obtiene la marca actual del componente. */
  getCurrentPlaceholder(): string {
    return this.state.placeholder
  }

  /** Obtiene el estado actual de la etiqueta. */
  getStatusClass(): string {
    return this.statusClass
      ? ` ${this.statusClass}`
      : '';
  }

  /** Obtiene el tipo de la etiqueta input. */
  getType(): string {
    switch (this.type) {
      case 'number': return 'text';
      default: return this.type;
    }
  }
}
