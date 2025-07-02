import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-editar-paquete',
  templateUrl: './editar-paquete.component.html',
  standalone: true,
  imports: [FormsModule]
})
export class EditarPaqueteComponent implements OnInit {
  paquete = {
    id: 0,
    nombre: '',
    descripcion: '',
    precioMensual: 0
  };

  constructor(private route: ActivatedRoute, private router: Router) {}

  ngOnInit() {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    // Aquí normalmente buscarías el paquete por id en backend
    // Por ahora cargamos datos ficticios para demostrar
    this.paquete = {
      id,
      nombre: 'Paquete demo ' + id,
      descripcion: 'Descripción demo',
      precioMensual: 500
    };
  }

  guardarPaquete() {
    alert('Paquete actualizado: ' + JSON.stringify(this.paquete));
    this.router.navigate(['/paquetes']);
  }
}
