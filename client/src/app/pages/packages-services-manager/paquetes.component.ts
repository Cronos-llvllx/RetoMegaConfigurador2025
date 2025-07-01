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
  paquetes: any[] = [];

  nuevoPaquete: any = {
    nombre: '',
    precio: 0,
    servicios: [] as string[]
  };

  editIndex: number | null = null;

  registrarPaquete() {
    if (this.editIndex !== null) {
      this.paquetes[this.editIndex] = { ...this.nuevoPaquete };
      this.editIndex = null;
    } else {
      this.paquetes.push({ ...this.nuevoPaquete });
    }

    this.nuevoPaquete = {
      nombre: '',
      precio: 0,
      servicios: []
    };
  }

  editarPaquete(index: number) {
    this.nuevoPaquete = { ...this.paquetes[index] };
    this.editIndex = index;
  }

  eliminarPaquete(index: number) {
    this.paquetes.splice(index, 1);
  }

  agregarServicio(servicio: string) {
    if (servicio.trim()) {
      this.nuevoPaquete.servicios.push(servicio.trim());
    }
  }

  eliminarServicio(index: number) {
    this.nuevoPaquete.servicios.splice(index, 1);
  }
}
