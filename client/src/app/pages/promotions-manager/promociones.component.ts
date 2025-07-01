import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { LocationService } from '../../services/location.service';
import { PromocionesService, Promocion, CrearPromocion } from '../../services/promociones.service';

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

  constructor(
    private locationService: LocationService,
    private promocionesService: PromocionesService
  ) {}

/**
 * Lista de promociones cargadas desde el backend.
 */
  promociones: Promocion[] = [];

  /**
   * Objeto temporal que representa la promoción que se está creando o editando.
   * Adaptado para trabajar con IDs en lugar de strings.
   */
  nuevaPromo: any = {
    nombre: '',
    tipo: 1, // 1 = Contratación, 2 = Mensualidades
    coloniasIds: [] as number[], // Array de IDs de colonias
    ciudadesIds: [] as number[], // Array de IDs de ciudades
    paquetesIds: [] as number[], // Array de IDs de paquetes
    alcance: 1, // Byte value
    precioPorcen: 0, // Precio o porcentaje
    duracion: 0, // Duración en meses
    vigencia: '' // Fecha en formato string
  };

 /**
   * Índice de la promoción que está siendo editada.
   * Si es `null`, significa que se está creando una nueva promoción.
   */

  editIndex: number | null = null;

  // Variables para los selectores
  ciudadSeleccionada: string = '';
  coloniaSeleccionada: string = '';
  ciudadesDisponibles: {id: number, name: string}[] = [];
  coloniasDisponibles: {id: number, name: string}[] = [];
  todasLasColonias: {id: number, name: string, ciudadId: number, ciudadNombre: string}[] = [];
  
  // Modo de aplicación: 'ciudades' o 'colonias'
  tipoAplicacion: 'ciudades' | 'colonias' | '' = '';

  ngOnInit() {
    this.loadCities();
    this.cargarPromociones();
  }

  /**
   * Carga todas las promociones desde el backend
   */
  cargarPromociones() {
    this.promocionesService.obtenerTodas().subscribe({
      next: (promociones) => {
        this.promociones = promociones;
        console.log('Promociones cargadas:', promociones);
      },
      error: (error) => {
        console.error('Error al cargar promociones:', error);
      }
    });
  }

  /**
   * Limpia el formulario y reinicia todos los campos
   */
  limpiarFormulario() {
    this.nuevaPromo = {
      nombre: '',
      tipo: 1,
      coloniasIds: [] as number[],
      ciudadesIds: [] as number[],
      paquetesIds: [] as number[],
      alcance: 1,
      precioPorcen: 0,
      duracion: 0,
      vigencia: ''
    };
    this.editIndex = null;
    this.ciudadSeleccionada = '';
    this.coloniaSeleccionada = '';
    this.tipoAplicacion = '';
    this.coloniasDisponibles = [];
    this.todasLasColonias = [];
    
    // Resetear el texto del botón
    const botonRegistrar = document.querySelector('button[onclick="registrarPromo()"]') as HTMLButtonElement;
    if (botonRegistrar) {
      botonRegistrar.textContent = 'Registrar';
    }
  }

  /**
   * Carga las ciudades desde el backend
   */
  loadCities() {
    this.locationService.getCitiesSimplified().subscribe({
      next: (cities) => {
        this.ciudadesDisponibles = cities;
      },
      error: (error) => {
        console.error('Error al cargar ciudades:', error);
        // Fallback con datos ficticios
        this.ciudadesDisponibles = [
          {id: 1, name: 'Ciudad de México'},
          {id: 2, name: 'Guadalajara'},
          {id: 3, name: 'Monterrey'}
        ];
      }
    });
  }

 /**
   * Registra o actualiza una promoción en el backend
   */
  registrarPromo() {
    // Preparar datos para enviar al backend
    const promocionData: CrearPromocion = {
      nombre: this.nuevaPromo.nombre,
      tipo: parseInt(this.nuevaPromo.tipo),
      alcance: parseInt(this.nuevaPromo.tipo) === 2 ? parseInt(this.nuevaPromo.alcance) : undefined,
      precioPorcen: parseFloat(this.nuevaPromo.precioPorcen),
      duracion: parseInt(this.nuevaPromo.duracion),
      vigencia: this.nuevaPromo.vigencia,
      ciudaddes: this.nuevaPromo.ciudadesIds, // Nota: doble 'd' como en el backend
      colonias: this.nuevaPromo.coloniasIds,
      paquetes: this.nuevaPromo.paquetesIds
    };

    if (this.editIndex !== null) {
      // Actualizar promoción existente
      const promocionId = this.promociones[this.editIndex].idpromocion;
      this.promocionesService.actualizar(promocionId, promocionData).subscribe({
        next: (promocionActualizada) => {
          console.log('Promoción actualizada exitosamente:', promocionActualizada);
          this.cargarPromociones(); // Recargar la lista
          this.limpiarFormulario();
          alert('Promoción actualizada exitosamente');
        },
        error: (error) => {
          console.error('Error al actualizar promoción:', error);
          alert('Error al actualizar la promoción: ' + (error.error?.message || error.message));
        }
      });
    } else {
      // Crear nueva promoción
      this.promocionesService.crear(promocionData).subscribe({
        next: (promocionCreada) => {
          console.log('Promoción creada exitosamente:', promocionCreada);
          this.cargarPromociones(); // Recargar la lista
          this.limpiarFormulario();
          alert('Promoción creada exitosamente');
        },
        error: (error) => {
          console.error('Error al crear promoción:', error);
          alert('Error al crear la promoción: ' + (error.error?.message || error.message));
        }
      });
    }
  }

/**
  * Carga los datos de una promoción existente en el formulario para ser editada.
  * @param index Índice de la promoción seleccionada.
  */
  editarPromo(index: number) {
    const promocion = this.promociones[index];
    
    // Cargar datos básicos
    this.nuevaPromo = {
      nombre: promocion.nombre,
      tipo: promocion.tipo,
      alcance: promocion.alcance || 1,
      precioPorcen: promocion.precioPorcen,
      duracion: promocion.duracion || 0,
      vigencia: this.formatDateForInput(promocion.vigencia),
      ciudadesIds: promocion.ciudades?.map(c => c.idciudad) || [],
      coloniasIds: promocion.colonias?.map(c => c.idcolonia) || [],
      paquetesIds: promocion.paquetes?.map(p => p.idpaquete) || []
    };
    
    // Configurar el tipo de aplicación basado en los datos existentes
    if (this.nuevaPromo.ciudadesIds.length > 0) {
      this.tipoAplicacion = 'ciudades';
    } else if (this.nuevaPromo.coloniasIds.length > 0) {
      this.tipoAplicacion = 'colonias';
      this.cargarTodasLasColonias();
    }
    
    this.editIndex = index;
    
    // Cambiar el texto del botón
    const botonRegistrar = document.querySelector('button[onclick="registrarPromo()"]') as HTMLButtonElement;
    if (botonRegistrar) {
      botonRegistrar.textContent = 'Actualizar';
    }
  }

  /**
   * Elimina una promoción del backend y actualiza la lista
   * @param index Índice de la promoción a eliminar.
   */
  eliminarPromo(index: number) {
    const promocion = this.promociones[index];
    
    if (confirm(`¿Estás seguro de que deseas eliminar la promoción "${promocion.nombre}"?`)) {
      this.promocionesService.eliminar(promocion.idpromocion).subscribe({
        next: () => {
          console.log('Promoción eliminada exitosamente');
          this.cargarPromociones(); // Recargar la lista
          alert('Promoción eliminada exitosamente');
          
          // Si estábamos editando esta promoción, limpiar el formulario
          if (this.editIndex === index) {
            this.limpiarFormulario();
          }
        },
        error: (error) => {
          console.error('Error al eliminar promoción:', error);
          const errorMessage = error.error?.message || error.error || error.message || 'Error desconocido';
          alert('Error al eliminar la promoción: ' + errorMessage);
        }
      });
    }
  }

  /**
   * Formatea una fecha para que sea compatible con el input de tipo date
   * @param date Fecha a formatear
   * @returns String en formato YYYY-MM-DD
   */
  private formatDateForInput(date: Date): string {
    if (!date) return '';
    const d = new Date(date);
    const year = d.getFullYear();
    const month = String(d.getMonth() + 1).padStart(2, '0');
    const day = String(d.getDate()).padStart(2, '0');
    return `${year}-${month}-${day}`;
  }

  /**
   * Selecciona el tipo de promoción
   */
  seleccionarTipo(valor: string) {
    const numeroTipo = parseInt(valor);
    // Si ya está seleccionado, desmarcarlo
    if (this.nuevaPromo.tipo === numeroTipo) {
      this.nuevaPromo.tipo = 1; // Valor por defecto
    } else {
      this.nuevaPromo.tipo = numeroTipo;
    }
  }

  /**
   * Selecciona el alcance de la promoción
   */
  seleccionarAlcance(valor: string) {
    // Convertir texto a número para el backend
    let numeroAlcance: number;
    if (valor === 'Nuevos suscriptores') {
      numeroAlcance = 1;
    } else if (valor === 'Actuales y nuevos suscriptores') {
      numeroAlcance = 2;
    } else {
      numeroAlcance = 1; // Por defecto
    }
    
    // Si ya está seleccionado, desmarcarlo
    if (this.nuevaPromo.alcance === numeroAlcance) {
      this.nuevaPromo.alcance = 1; // Valor por defecto
    } else {
      this.nuevaPromo.alcance = numeroAlcance;
    }
  }

    // Funciones para colonia (trabajando con IDs)
  agregarColoniaById(coloniaId: number) {
    // Verifica que el ID no esté ya en la lista
    if (!this.nuevaPromo.coloniasIds.includes(coloniaId)) {
      this.nuevaPromo.coloniasIds.push(coloniaId);
    }
  }
  
  // Elimina una colonia de la lista según su índice.
  eliminarColonia(index: number) {
    this.nuevaPromo.coloniasIds.splice(index, 1);
  }

  // Funciones para ciudad (trabajando con IDs)
  agregarCiudadById(ciudadId: number) {
    // Verifica que el ID no esté ya en la lista
    if (!this.nuevaPromo.ciudadesIds.includes(ciudadId)) {
      this.nuevaPromo.ciudadesIds.push(ciudadId);
    }
  }
  
  // Elimina una ciudad de la lista según su índice.
  eliminarCiudad(index: number) {
    this.nuevaPromo.ciudadesIds.splice(index, 1);
  }

  // Obtener nombre de ciudad por ID (para mostrar en la interfaz)
  getNombreCiudad(ciudadId: number): string {
    const ciudad = this.ciudadesDisponibles.find(c => c.id === ciudadId);
    return ciudad ? ciudad.name : `Ciudad ${ciudadId}`;
  }

  // Obtener nombre de colonia por ID (para mostrar en la interfaz)
  getNombreColonia(coloniaId: number): string {
    const colonia = this.coloniasDisponibles.find(c => c.id === coloniaId);
    return colonia ? colonia.name : `Colonia ${coloniaId}`;
  }

  /**
   * Obtiene los nombres de las colonias de una promoción como string
   */
  getColoniasNombres(promocion: Promocion): string {
    if (!promocion.colonias || promocion.colonias.length === 0) {
      return 'N/A';
    }
    return promocion.colonias.map(c => c.colonia?.nombre || `Colonia ${c.idcolonia}`).join(', ');
  }

  /**
   * Obtiene los nombres de las ciudades de una promoción como string
   */
  getCiudadesNombres(promocion: Promocion): string {
    if (!promocion.ciudades || promocion.ciudades.length === 0) {
      return 'N/A';
    }
    return promocion.ciudades.map(c => c.ciudad?.nombre || `Ciudad ${c.idciudad}`).join(', ');
  }

  /**
   * Obtiene el texto descriptivo del tipo de promoción
   */
  getTipoTexto(tipo: number): string {
    return tipo === 1 ? 'Contratación' : 'Mensualidad';
  }

  /**
   * Obtiene el texto del alcance o N/A si no está definido
   */
  getAlcanceTexto(alcance?: number): string {
    return alcance ? alcance.toString() : 'N/A';
  }

  // Función que se ejecuta al cambiar la ciudad seleccionada
  onCiudadChange() {
    if (this.ciudadSeleccionada) {
      // Buscar el ID de la ciudad seleccionada
      const ciudadSeleccionadaObj = this.ciudadesDisponibles.find(c => c.name === this.ciudadSeleccionada);
      
      if (ciudadSeleccionadaObj) {
        // Cargar colonias desde el backend
        this.locationService.getColoniesByCitySimplified(ciudadSeleccionadaObj.id).subscribe({
          next: (colonies) => {
            this.coloniasDisponibles = colonies;
          },
          error: (error) => {
            console.error('Error al cargar colonias:', error);
            // Fallback con datos ficticios
            this.coloniasDisponibles = [
              {id: 1, name: `${this.ciudadSeleccionada} - Colonia 1`},
              {id: 2, name: `${this.ciudadSeleccionada} - Colonia 2`},
              {id: 3, name: `${this.ciudadSeleccionada} - Colonia 3`}
            ];
          }
        });
      }
      
      // Agregar la ciudad seleccionada a la lista de promoción
      if (ciudadSeleccionadaObj) {
        this.agregarCiudadById(ciudadSeleccionadaObj.id);
      }
      this.ciudadSeleccionada = '';
    } else {
      this.coloniasDisponibles = [];
    }
    // Limpiar colonias cuando se cambia de ciudad
    this.coloniaSeleccionada = '';
  }

  // Función que se ejecuta al cambiar la(s) colonia(s) seleccionada(s)
  onColoniaChange() {
    if (this.coloniaSeleccionada) {
      const coloniaSeleccionadaObj = this.coloniasDisponibles.find(c => c.name === this.coloniaSeleccionada);
      if (coloniaSeleccionadaObj) {
        this.agregarColoniaById(coloniaSeleccionadaObj.id);
      }
      this.coloniaSeleccionada = '';
    }
  }

  /**
   * Cambia el tipo de aplicación (ciudades completas vs colonias específicas)
   */
  onTipoAplicacionChange() {
    // Limpiar selecciones anteriores al cambiar modo
    this.nuevaPromo.ciudadesIds = [];
    this.nuevaPromo.coloniasIds = [];
    this.ciudadSeleccionada = '';
    this.coloniaSeleccionada = '';
    this.coloniasDisponibles = [];
    
    if (this.tipoAplicacion === 'colonias') {
      // Cargar todas las colonias de todas las ciudades
      this.cargarTodasLasColonias();
    }
  }

  /**
   * Carga todas las colonias de todas las ciudades para el modo colonias específicas
   */
  cargarTodasLasColonias() {
    this.todasLasColonias = [];
    
    this.ciudadesDisponibles.forEach(ciudad => {
      this.locationService.getColoniesByCitySimplified(ciudad.id).subscribe({
        next: (colonias) => {
          colonias.forEach(colonia => {
            this.todasLasColonias.push({
              id: colonia.id,
              name: colonia.name,
              ciudadId: ciudad.id,
              ciudadNombre: ciudad.name
            });
          });
        },
        error: (error) => {
          console.error(`Error al cargar colonias de ${ciudad.name}:`, error);
          // Fallback con datos ficticios
          for (let i = 1; i <= 3; i++) {
            this.todasLasColonias.push({
              id: (ciudad.id * 10) + i,
              name: `${ciudad.name} - Colonia ${i}`,
              ciudadId: ciudad.id,
              ciudadNombre: ciudad.name
            });
          }
        }
      });
    });
  }

  /**
   * Función para agregar ciudad completa (incluye todas sus colonias automáticamente)
   */
  onCiudadCompleteChange() {
    if (this.ciudadSeleccionada) {
      const ciudadObj = this.ciudadesDisponibles.find(c => c.name === this.ciudadSeleccionada);
      if (ciudadObj) {
        this.agregarCiudadById(ciudadObj.id);
      }
      this.ciudadSeleccionada = '';
    }
  }

  /**
   * Función para agregar colonia específica
   */
  onColoniaEspecificaChange() {
    if (this.coloniaSeleccionada) {
      const coloniaObj = this.todasLasColonias.find(c => c.name === this.coloniaSeleccionada);
      if (coloniaObj) {
        this.agregarColoniaById(coloniaObj.id);
      }
      this.coloniaSeleccionada = '';
    }
  }

  /**
   * Obtiene el nombre de la colonia incluyendo la ciudad (para modo colonias específicas)
   */
  getNombreColoniaCompleto(coloniaId: number): string {
    const colonia = this.todasLasColonias.find(c => c.id === coloniaId);
    return colonia ? `${colonia.name} (${colonia.ciudadNombre})` : `Colonia ${coloniaId}`;
  }

}
