import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

/**
 * Componente para administrar promociones.
 * Permite crear, editar y eliminar promociones desde un formulario.
 */

@Component({
  standalone: true,
  selector: 'app-promociones',
  imports: [CommonModule, FormsModule],
  templateUrl: './promociones.component.html',
  styleUrls: ['./promociones.component.scss']
})
export class PromocionesComponent {

/**
 * Lista de promociones creadas.
 * Cada promoción es un objeto con campos como nombre, tipo, colonia, etc.
 */

  promociones: any[] = [];

  /**
   * Objeto temporal que representa la promoción que se está creando o editando.
   */

  nuevaPromo: any = {
    nombre: '',
    tipo: '',
    colonia: '',
    ciudad: '',
    alcance: '',
    descuento: '',
    duracion: '',
    vigencia: ''
  };

 /**
   * Índice de la promoción que está siendo editada.
   * Si es `null`, significa que se está creando una nueva promoción.
   */
  
  editIndex: number | null = null;

 /**
   * Registra una nueva promoción o actualiza una existente si `editIndex` no es null.
   * Luego reinicia el formulario.
   */

  registrarPromo() {
    if (this.editIndex !== null) {
      this.promociones[this.editIndex] = { ...this.nuevaPromo };
      this.editIndex = null;
    } else {
      this.promociones.push({ ...this.nuevaPromo });
    }
    /**
     * Reinicia los campos del formulario y limpia el índice de edición.
     */
    this.nuevaPromo = {
      nombre: '',
      tipo: '',
      colonia: '',
      ciudad: '',
      alcance: '',
      descuento: '',
      duracion: '',
      vigencia: ''
    };
  }

/**
  * Carga los datos de una promoción existente en el formulario para ser editada.
  * @param index Índice de la promoción seleccionada.
  */
  editarPromo(index: number) {
    this.nuevaPromo = { ...this.promociones[index] };
    this.editIndex = index;
  }

  /**
   * Elimina una promoción de la lista según su índice.
   * @param index Índice de la promoción a eliminar.
   */
  eliminarPromo(index: number) {
    this.promociones.splice(index, 1);
  }
}
