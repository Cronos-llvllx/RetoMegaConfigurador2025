import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  standalone: true,
  selector: 'app-promociones',
  imports: [CommonModule, FormsModule],
  templateUrl: './promociones.component.html',
  styleUrls: ['./promociones.component.scss']
})
export class PromocionesComponent {
  promociones: any[] = [];
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
  editIndex: number | null = null;

  registrarPromo() {
    if (this.editIndex !== null) {
      this.promociones[this.editIndex] = { ...this.nuevaPromo };
      this.editIndex = null;
    } else {
      this.promociones.push({ ...this.nuevaPromo });
    }
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

  editarPromo(index: number) {
    this.nuevaPromo = { ...this.promociones[index] };
    this.editIndex = index;
  }

  eliminarPromo(index: number) {
    this.promociones.splice(index, 1);
  }
}
