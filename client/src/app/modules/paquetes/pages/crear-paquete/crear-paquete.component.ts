import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule, Router } from '@angular/router';

@Component({
  selector: 'app-crear-paquete',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './crear-paquete.component.html',
})
export class CrearPaqueteComponent {
  paquete = {
    nombre: '',
    descripcion: '',
    precioMensual: 0
  };

  constructor(private router: Router) {}

  guardarPaquete() {
    alert('Paquete guardado: ' + JSON.stringify(this.paquete));
    this.router.navigate(['/paquetes']);
  }
}
