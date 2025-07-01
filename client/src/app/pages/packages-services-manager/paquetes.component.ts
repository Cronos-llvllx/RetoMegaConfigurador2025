import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  standalone: true,
  selector: 'app-paquetes',
  templateUrl: './paquetes.component.html',
  styleUrls: ['./paquetes.component.scss'],
  imports: [CommonModule, FormsModule]
})
export class PaquetesComponent {

  // Arreglo que almacena todos los paquetes registrados
  paquetes: any[] = [];

  // Modelo que representa el paquete que se está creando o editando
  nuevoPaquete: any = {
    nombre: '',
    alcance: '', 
    precio: 0,
    servicios: [] as string[]
  };

  // Índice del paquete que se está editando, o null si no hay edición activa
  editIndex: number | null = null;

  // Lista de servicios disponibles para seleccionar
  serviciosDisponibles: string[] = ['Internet', 'TV', 'Telefonía'];
  // Variable auxiliar que controla el valor seleccionado en el combobox
  servicioSeleccionado: string = '';

  // Registra un nuevo paquete o actualiza uno existente
  registrarPaquete() {
    if (this.editIndex !== null) {
      // Si hay edición en curso, actualiza el paquete existente
      this.paquetes[this.editIndex] = { ...this.nuevoPaquete };
      this.editIndex = null; // Finaliza la edición
    } else {
      // Si no hay edición, agrega un nuevo paquete al arreglo
      this.paquetes.push({ ...this.nuevoPaquete });
    }

    // Resetea el formulario a su estado inicial
    this.nuevoPaquete = {
      nombre: '',
      alcance: '', 
      precio: 0,
      servicios: [] as string[]
    };

    this.servicioSeleccionado = ''; // Reset select
  }

   // Carga los datos de un paquete para ser editado
  editarPaquete(index: number) {
    this.nuevoPaquete = { ...this.paquetes[index] };
    this.editIndex = index;
  }

  // Elimina un paquete del arreglo
  eliminarPaquete(index: number) {
    this.paquetes.splice(index, 1); // Elimina el paquete por índice
  }

  // Maneja la selección del campo "Alcance" 
  seleccionarAlcance(valor: string) {
    // Si ya está seleccionado, desmarcarlo
    if (this.nuevoPaquete.alcance === valor) {
      this.nuevoPaquete.alcance = '';
    } else {
      this.nuevoPaquete.alcance = valor;
    }
  }

  // Agrega un servicio al paquete si no ha sido agregado ya
  agregarServicio() {
    const servicio = this.servicioSeleccionado;
    if (servicio && !this.nuevoPaquete.servicios.includes(servicio)) {
      this.nuevoPaquete.servicios.push(servicio);
    }
    this.servicioSeleccionado = ''; // Reset select
  }
  
  // Elimina un servicio específico del arreglo de servicios del paquete actual
  eliminarServicio(index: number) {
    this.nuevoPaquete.servicios.splice(index, 1);
  }

}
