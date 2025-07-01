import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ServicioService } from '../../services/servicio.service';
import Service from '../../models/service.model';
import { HttpErrorResponse, HttpStatusCode } from '@angular/common/http';
import { PaqueteService } from '../../services/paquete.service';
import Package from '../../models/package.model';

@Component({
  standalone: true,
  selector: 'app-paquetes',
  templateUrl: './paquetes.component.html',
  styleUrls: ['./paquetes.component.scss'],
  imports: [CommonModule, FormsModule]
})
export class PaquetesComponent implements OnInit {

  // Arreglo que almacena todos los paquetes registrados
  paquetes: Package[] = [];

  // Modelo que representa el paquete que se está creando o editando
  nuevoPaquete = {
    nombre: '',
    alcance: '',
    precio: 0,
    servicios: [] as string[]
  };

  // Índice del paquete que se está editando, o null si no hay edición activa
  editIndex: number | null = null;

  // Lista de servicios disponibles para seleccionar
  serviciosDisponibles: Service[] = [];
  // Variable auxiliar que controla el valor seleccionado en el combobox
  servicioSeleccionado: string = '';

  constructor(private $ser: ServicioService, private $paquetes: PaqueteService) { }

  ngOnInit(): void {
    // Obtiene los servicios registrados.
    this.$ser.getAllServices().subscribe({
      next: (res) => {
        this.serviciosDisponibles = res;

        // Obtiene los paquetes registrados.
        this.$paquetes.getAllPackages().subscribe({
          next: (packages: any[]) => {
            console.log(packages);
            this.paquetes = packages.map(p => {
              return new Package(
                p.idpaquete,
                p.nombre,
                p.tipo,
                new Date(),
                (p.servicios as any[]).map(s => this.serviciosDisponibles.find(sD => sD.getId() == s.idservicio) as Service),
                undefined, // promotions
                p.precioBase // precio base
              );
            });
          }
        })
      },
      error: (err: HttpErrorResponse) => {
        console.error(err);
      }
    });
  }

  // Registra un nuevo paquete o actualiza uno existente
  registrarPaquete() {
    // paquete vacío.
    if (this.nuevoPaquete.alcance === ''
      || this.nuevoPaquete.nombre === ''
      || this.nuevoPaquete.precio === 0
      || this.nuevoPaquete.precio === null
      || this.nuevoPaquete.servicios.length === 0
    ) {
      alert("Hay campos vacíos")
      return;
    }

    // Crea un nuevo objeto Package.
    const auxPackage = new Package(
      0,
      this.nuevoPaquete.nombre,
      this.nuevoPaquete.alcance === "Empresarial" ? Package.TYPE_FOR_ENTERPRISE : Package.TYPE_FOR_RESIDENTIAL,
      new Date(Date.now()),
      this.nuevoPaquete.servicios.map(s => this.serviciosDisponibles.find(sD => s == sD.toString())) as Service[],
      undefined, // promotions
      this.nuevoPaquete.precio // precio base
    );

    if (this.editIndex !== null) {
      // Asigna el id.
      const packageId = this.paquetes[this.editIndex].getId();
      
      if (!packageId || packageId === 0) {
        alert('Error: ID del paquete no válido');
        return;
      }
      
      auxPackage.setId(packageId);
      const index = this.editIndex;

      this.$paquetes.updatePackage(auxPackage).subscribe({
        next: () => {
          // Si hay edición en curso, actualiza el paquete existente
          this.paquetes[index] = auxPackage;
          this.editIndex = null; // Finaliza la edición.
          this.nuevoPaquete = {
            nombre: '',
            alcance: '',
            precio: 0,
            servicios: [] as string[]
          };

          this.servicioSeleccionado = ''; // Reset select

          alert("Paquete actualizado");
        }, error: (err: HttpErrorResponse) => {
          console.log(err);
          alert("Ocurrió un error: " + err.message);
        }
      })
    } else {
      // Si no hay edición, agrega un nuevo paquete a la db..
      this.$paquetes.addNewPackage(auxPackage).subscribe({
        next: pack => {
          // Resetea el formulario a su estado inicial
          this.nuevoPaquete = {
            nombre: '',
            alcance: '',
            precio: 0,
            servicios: [] as string[]
          };

          this.servicioSeleccionado = ''; // Reset select

          this.paquetes.push(pack);
          alert("El paquete fue agregado");
        }, error: (err: HttpErrorResponse) => {
          console.error(err);
          alert(err.message);
        }
      });
    }
  }

  // Carga los datos de un paquete para ser editado
  editarPaquete(index: number) {
    this.nuevoPaquete = {
      alcance: this.paquetes[index].getType() == Package.TYPE_FOR_ENTERPRISE ? "Empresarial" : "Residencial",
      nombre: this.paquetes[index].getName(),
      precio: this.paquetes[index].getBasePrice(),
      servicios: this.paquetes[index].getServices()!.map(s => s.toString())
    };
    this.editIndex = index;
  }

  // Elimina un paquete del arreglo
  eliminarPaquete(index: number) {
    if (confirm(`¿Estás seguro de que deseas eliminar "${this.paquetes[index].getName()}"?`)) {
      this.$paquetes.removePackage(this.paquetes[index]).subscribe({
        next: () => {
          alert("El paquete fue eliminado");
          this.paquetes.splice(index, 1); // Elimina el paquete por índice
        }, error: (err: HttpErrorResponse) => {
          console.error(err);

          if (err.status === HttpStatusCode.Unauthorized) {
            alert("No se puede eliminar este paquete porque ya está relacionado con contratos.")
          } else if (err.status === HttpStatusCode.NotFound) {
            alert("No se encontró el paquete a eliminar: " + this.paquetes[index].getId())
          } else {
            alert("Ocurrió un error desconocido: " + err.message);
          }
        }
      })
    }
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
    if (servicio && !this.nuevoPaquete.servicios.find(s => s.split(' ')[0] == servicio.split(' ')[0])) {
      this.nuevoPaquete.servicios.push(servicio);
    }
    this.servicioSeleccionado = ''; // Reset select
  }

  // Elimina un servicio específico del arreglo de servicios del paquete actual
  eliminarServicio(index: number) {
    this.nuevoPaquete.servicios.splice(index, 1);
  }

  obtenerServiciosDePaquete(paq: Package): string {
    return paq.getServices()?.map(s => s.toString()).join(', ') ?? '';
  }

  obtenerAlcanceDePaquete(paq: Package): string {
    return paq.getType() === Package.TYPE_FOR_ENTERPRISE
      ? 'Empresarial'
      : 'Residencial';
  }

}
