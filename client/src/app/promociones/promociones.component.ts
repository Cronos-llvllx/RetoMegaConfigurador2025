import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PromocionesService, Promocion, CrearPromocion } from '../services/promociones.service';
import { PaqueteService } from '../services/paquete.service';
import Package from '../models/package.model';
import { HttpErrorResponse } from '@angular/common/http';

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
export class PromocionesComponent implements OnInit {

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
    paquetes: [] as string[], // Paquetes seleccionados para la promoción
    alcance: '',
    descuento: '',
    duracion: '',
    vigencia: ''
  };

  // Lista de paquetes disponibles cargada desde el backend
  paquetesDisponibles: Package[] = [];
  
  // Paquete seleccionado en el dropdown
  paqueteSeleccionado: string = '';

 /**
   * Índice de la promoción que está siendo editada.
   * Si es `null`, significa que se está creando una nueva promoción.
   */
  
  editIndex: number | null = null;

  // Constructor con inyección de servicios
  constructor(
    private promocionesService: PromocionesService,
    private paqueteService: PaqueteService
  ) {}

  /**
   * Método que se ejecuta al inicializar el componente.
   * Carga los paquetes disponibles desde el backend.
   */
  ngOnInit(): void {
    this.cargarPaquetes();
  }

  /**
   * Carga la lista de paquetes disponibles desde el backend.
   */
  cargarPaquetes(): void {
    this.paqueteService.getAllPackages().subscribe({
      next: (paquetes: any[]) => {
        this.paquetesDisponibles = paquetes.map(p => {
          return new Package(
            p.idpaquete,
            p.nombre,
            p.tipo,
            new Date(),
            [], // servicios (no necesarios para el dropdown)
            undefined, // promotions
            p.precioBase
          );
        });
      },
      error: (err: HttpErrorResponse) => {
        console.error('Error al cargar paquetes:', err);
        alert('Error al cargar los paquetes disponibles');
      }
    });
  }

  /**
   * Agrega un paquete seleccionado a la lista de paquetes de la promoción.
   */
  agregarPaquete(): void {
    const paquete = this.paqueteSeleccionado;
    if (paquete && !this.nuevaPromo.paquetes.find((p: string) => p.split(' ')[0] === paquete.split(' ')[0])) {
      this.nuevaPromo.paquetes.push(paquete);
    }
    this.paqueteSeleccionado = ''; // Reset select
  }

  /**
   * Elimina un paquete específico del arreglo de paquetes de la promoción actual.
   */
  eliminarPaquete(index: number): void {
    this.nuevaPromo.paquetes.splice(index, 1);
  }

 /**
   * Registra una nueva promoción o actualiza una existente si `editIndex` no es null.
   * Luego reinicia el formulario.
   */
  registrarPromo() {
    // Validaciones básicas
    if (!this.nuevaPromo.nombre || !this.nuevaPromo.tipo || !this.nuevaPromo.descuento || !this.nuevaPromo.vigencia) {
      alert('Por favor completa todos los campos obligatorios');
      return;
    }

    // Convertir datos del formulario al formato del DTO
    const promocionData = {
      nombre: this.nuevaPromo.nombre,
      tipo: this.nuevaPromo.tipo === 'Contratacion' ? 1 : 2, // 1 = Contratación, 2 = Mensualidad
      precioPorcen: parseFloat(this.nuevaPromo.descuento),
      vigencia: this.nuevaPromo.vigencia,
      duracion: this.nuevaPromo.duracion ? parseInt(this.nuevaPromo.duracion) : 0,
      alcance: this.nuevaPromo.alcance === 'Nuevos suscriptores' ? 1 : 2,
      ciudaddes: [], // Por ahora vacío, se puede implementar después
      colonias: [], // Por ahora vacío, se puede implementar después
      paquetes: this.nuevaPromo.paquetes.map((p: string) => parseInt(p.split(' ')[0])) // Extraer IDs de paquetes
    };

    if (this.editIndex !== null) {
      // Actualizar promoción existente
      const promocionId = this.promociones[this.editIndex].idpromocion;
      this.promocionesService.actualizar(promocionId, promocionData).subscribe({
        next: (promocion) => {
          this.promociones[this.editIndex!] = promocion;
          this.editIndex = null;
          this.resetFormulario();
          alert('Promoción actualizada exitosamente');
        },
        error: (err: HttpErrorResponse) => {
          console.error('Error al actualizar promoción:', err);
          alert('Error al actualizar la promoción: ' + (err.error?.message || err.message));
        }
      });
    } else {
      // Crear nueva promoción
      this.promocionesService.crear(promocionData).subscribe({
        next: (promocion) => {
          this.promociones.push(promocion);
          this.resetFormulario();
          alert('Promoción creada exitosamente');
        },
        error: (err: HttpErrorResponse) => {
          console.error('Error al crear promoción:', err);
          alert('Error al crear la promoción: ' + (err.error?.message || err.message));
        }
      });
    }
  }

  /**
   * Reinicia el formulario a su estado inicial
   */
  private resetFormulario() {
    this.nuevaPromo = {
      nombre: '',
      tipo: '',
      colonias: [] as string[],
      ciudades: [] as string[],
      paquetes: [] as string[],
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






