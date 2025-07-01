import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { trigger, state, style, transition, animate } from '@angular/animations';

// --- Servicios ---
import { SuscriptoresService } from '../../services/suscriptores.service';
import { PaqueteService } from '../../services/paquete.service';
import { ContratoPaqueteService } from '../../services/contrato-paquete.service';

// --- Modelos ---
import Contract from '../../models/contract.model';
import Subscriptor from '../../models/subscriptor.model';
import Colony from '../../models/colony.model';
import City from '../../models/city.model';
import Package from '../../models/package.model'; // Se usa Package como en tu modelo
import ContratoPaquete from '../../models/contratopaquete.model';

@Component({
  selector: 'app-subscribers-manager',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './subscribers-manager.component.html',
  styleUrls: ['./subscribers-manager.component.scss'],
})
export class SubscribersManagerComponent implements OnInit {
  // Propiedades para el estado de la UI
  public contractNumber: string = '';
  public isLoading: boolean = false;
  public errorMessage: string | null = null;

  // Propiedades para los datos
  public subscriberInfo: Contract | null = null;
  public startDate: string = '';
  public endDate: string = '';

  // Propiedades para la gestión de paquetes
  public activePackage: ContratoPaquete | null = null;
  public oldPackages: ContratoPaquete[] = [];
  public availablePackages: Package[] = [];
  public allPackages: Package[] = [];
  public selectedNewPackageId: number | null = null;

  constructor(
    private suscriptoresService: SuscriptoresService,
    private paqueteService: PaqueteService,
    private contratoPaqueteService: ContratoPaqueteService
  ) {}

  ngOnInit(): void {
    this.paqueteService.getAllPackages().subscribe((packagesData: any[]) => {
      // Se construyen los objetos Paquete correctamente desde la respuesta de la API
      this.allPackages = packagesData.map(p => new Package(p.idpaquete, p.nombre, p.tipo, new Date(), [], p.precioBase));
    });
  }

  searchContract(): void {
    if (!this.contractNumber) {
      this.errorMessage = 'Por favor, ingrese un número de contrato.';
      return;
    }

    this.isLoading = true;
    this.errorMessage = null;
    this.subscriberInfo = null;

    this.suscriptoresService.getContractInfo(this.contractNumber).subscribe({
      next: (data: any) => {
        const subData = data.suscriptor;
        const colData = subData.colonia;
        const cityData = colData.ciudad;

        const ciudad = new City(cityData.idciudad, cityData.nombre, []);
        const colonia = new Colony(colData.idcolonia, colData.nombre, ciudad);
        const subscriptor = new Subscriptor(subData.idsuscriptor, subData.nombre, subData.email, subData.telefono, subData.tipo, colonia);

        const contractPackages = (data.paquetes || []).map((cp: any) => {
            // Se pasan los 6 argumentos en el orden correcto que el constructor de 'Package' espera.
            const paquete = new Package(cp.paquete.idpaquete, cp.paquete.nombre, cp.paquete.tipo, new Date(), [], cp.paquete.precioBase);
            return new ContratoPaquete(null, paquete, new Date(cp.fechaAdicion), cp.fechaRetiro ? new Date(cp.fechaRetiro) : null);
        });

        const contract = new Contract(data.idcontrato, new Date(data.fechaContr), data.fechaFin ? new Date(data.fechaFin) : null, data.precioBase, subscriptor, [], contractPackages);

        this.subscriberInfo = contract;
        this.processPackages(contract.getPaquetes());
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Error al buscar el contrato:', err);
        this.errorMessage = `No se pudo encontrar el contrato #${this.contractNumber}.`;
        this.isLoading = false;
      }
    });
  }

  processPackages(contractPackages: ContratoPaquete[]): void {
    this.activePackage = contractPackages.find(p => !p.getFechaRetiro()) || null;
    this.oldPackages = contractPackages.filter(p => !!p.getFechaRetiro());
    this.updateAvailablePackages();
  }

  updateAvailablePackages(): void {
    let filteredPackages = this.allPackages;

    // Filtro paquetes basado en el tipo de suscriptor.
    if (this.subscriberInfo) {
      const subscriberType = this.subscriberInfo.getSubscriptor().getType();
      filteredPackages = this.allPackages.filter(p => p.getType() === subscriberType);
    }

    // Excluir paquete activo de los paquetes disponibles
    if (!this.activePackage) {
      this.availablePackages = filteredPackages;
    } else {
      this.availablePackages = filteredPackages.filter(p => p.getId() !== this.activePackage?.getPaquete().getId());
    }
  }

  addPackage(): void {
    if (!this.subscriberInfo || !this.selectedNewPackageId) {
      this.errorMessage = 'Por favor, seleccione un paquete para agregar.';
      return;
    }

    if (this.activePackage) {
      this.errorMessage = 'No se puede agregar un paquete cuando ya hay uno activo. Primero cancele el paquete actual.';
      return;
    }

    this.isLoading = true;
    this.errorMessage = null;

    const contractId = this.subscriberInfo.getId();
    const packageId = this.selectedNewPackageId;

    this.contratoPaqueteService.addPackageToContract(contractId, packageId).subscribe({
      next: (response) => {
        console.log('Paquete agregado exitosamente:', response);

        // Crear el nuevo paquete agregado
        const selectedPackage = this.allPackages.find(p => p.getId() === packageId);
        if (selectedPackage) {
          const newContractPackage = new ContratoPaquete(
            this.subscriberInfo,
            selectedPackage,
            new Date(response.FechaAdicion),
            null
          );

          // Actualizar el estado local
          this.activePackage = newContractPackage;
          this.updateAvailablePackages();
          this.selectedNewPackageId = null;
        }

        this.isLoading = false;
        alert('¡Paquete agregado exitosamente!');
      },
      error: (err) => {
        console.error('Error al agregar el paquete:', err);
        this.errorMessage = 'Error al agregar el paquete. Por favor, intente nuevamente.';
        this.isLoading = false;
      }
    });
  }

  cancelPackage(paquete: ContratoPaquete): void {
    if (!this.subscriberInfo) {
      this.errorMessage = 'No hay información del contrato disponible.';
      return;
    }

    const packageName = paquete.getPaquete().getName();
    const confirmCancel = confirm(`¿Está seguro de que desea cancelar el paquete "${packageName}"?`);

    if (!confirmCancel) {
      return;
    }

    this.isLoading = true;
    this.errorMessage = null;

    const contractId = this.subscriberInfo.getId();
    const packageId = paquete.getPaquete().getId();

    this.contratoPaqueteService.cancelPackageFromContract(contractId, packageId).subscribe({
      next: (response) => {
        console.log('Paquete cancelado exitosamente:', response);

        // Actualizar el estado local
        if (this.activePackage && this.activePackage.getPaquete().getId() === packageId) {
          // Marcar como retirado y mover a oldPackages
          const canceledPackage = new ContratoPaquete(
            this.activePackage.getContrato(),
            this.activePackage.getPaquete(),
            this.activePackage.getFechaAdicion(),
            new Date() // Fecha de retiro
          );

          this.oldPackages.unshift(canceledPackage);
          this.activePackage = null;
          this.updateAvailablePackages();
        }

        this.isLoading = false;
        alert(`Paquete "${packageName}" cancelado exitosamente.`);
      },
      error: (err) => {
        console.error('Error al cancelar el paquete:', err);
        this.errorMessage = 'Error al cancelar el paquete. Por favor, intente nuevamente.';
        this.isLoading = false;
      }
    });
  }

  /**
   * Método auxiliar para obtener el nombre del tipo de suscriptor para mostrar
   */
  getSubscriberTypeName(): string {
    if (!this.subscriberInfo) return '';

    const type = this.subscriberInfo.getSubscriptor().getType();
    return type === Subscriptor.TYPE_RESIDENTIAL ? 'Residencial' : 'Empresarial';
  }

  /**
   * Método auxiliar para obtener el nombre del tipo de paquete para mostrar
   */
  getPackageTypeName(packageType: number): string {
    return packageType === Package.TYPE_FOR_RESIDENTIAL ? 'Residencial' : 'Empresarial';
  }
}
