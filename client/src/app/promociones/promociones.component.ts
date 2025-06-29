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
    colonias: [] as string[],
    ciudades: [] as string[],
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
      colonias: [] as string[],
      ciudades: [] as string[],
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

  /**
   * Limpia el formulario y resetea el índice de edición.
   */
  seleccionarTipo(valor: string) {
    // Si ya está seleccionado, desmarcarlo
    if (this.nuevaPromo.tipo === valor) {
      this.nuevaPromo.tipo = '';
    } else {
      this.nuevaPromo.tipo = valor;
    }
  }
  
    /**
   * Limpia el formulario y resetea el índice de edición.
   */
    seleccionarAlcance(valor: string) {
      // Si ya está seleccionado, desmarcarlo
      if (this.nuevaPromo.alcance === valor) {
        this.nuevaPromo.alcance = '';
      } else {
        this.nuevaPromo.alcance = valor;
      }
    }
    
    // Funciones para colonia (box-elemts)
  agregarColonia(valor: string) {
    // Verifica si el valor no está vacío antes de agregarlo
    // y lo agrega a la lista de colonias.
    if (valor.trim()) this.nuevaPromo.colonias.push(valor.trim());
  }
  // Elimina una colonia de la lista según su índice.
  eliminarColonia(index: number) {
    this.nuevaPromo.colonias.splice(index, 1);
  }

  // Funciones para ciudad (box-elemts)
  agregarCiudad(valor: string) {
    // Verifica si el valor no está vacío antes de agregarlo
    if (valor.trim()) this.nuevaPromo.ciudades.push(valor.trim());
  }
  // Elimina una ciudad de la lista según su índice.
  eliminarCiudad(index: number) {
    this.nuevaPromo.ciudades.splice(index, 1);
  }

}






