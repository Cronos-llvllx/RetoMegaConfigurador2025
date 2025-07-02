import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-lista-paquetes',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './lista-paquetes.component.html',
  styleUrls: ['./lista-paquetes.component.scss'] 

})
export class ListaPaquetesComponent {
  paquetes = [
    { id: 1, nombre: 'Paquete Básico', descripcion: 'Internet 10 Mbps', precioMensual: 300 },
    { id: 2, nombre: 'Paquete Premium', descripcion: 'Internet 100 Mbps + TV', precioMensual: 700 },
    { id: 3, nombre: 'Paquete Total', descripcion: 'Internet + Telefonía + TV', precioMensual: 1000 }
  ];

  eliminarPaquete(id: number) {
    if (confirm('¿Estás seguro de eliminar el paquete?')) {
      this.paquetes = this.paquetes.filter(p => p.id !== id);
    }
  }
}
