import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

// Importación de tus modelos
import { SuscriptoresService } from '../../services/suscriptores.service';
import Contract from '../../models/contract.model';
import Subscriptor from '../../models/subscriptor.model';

@Component({
  selector: 'app-subscribers-manager',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './subscribers-manager.component.html',
  styleUrls: ['./subscribers-manager.component.scss']
})
export class SubscribersManagerComponent {

  public contractNumber: string = '';
  public subscriberInfo: Contract | null = null;
  public isLoading: boolean = false;
  public errorMessage: string | null = null;

  // --- 1. PROPIEDADES FECHAS PARA CALCULAR DEUDA ---
  public startDate: string = '';
  public endDate: string = '';

  constructor(private suscriptoresService: SuscriptoresService) {}

  public searchContract(): void {
    if (!this.contractNumber) {
      this.errorMessage = 'Por favor, ingrese un número de contrato.';
      return;
    }

    this.isLoading = true;
    this.errorMessage = null;
    this.subscriberInfo = null;

    // Se asume que el servicio ahora tiene el método getContractInfo
    this.suscriptoresService.getContractInfo(this.contractNumber).subscribe({
      next: (data: any) => {
        //Convertimos el JSON en instancias de Clases ---

        // creamos la instancia del suscriptor a partir del JSON anidado.
        const subData = data.suscriptor;
        const subscriptor = new Subscriptor(
          subData.idsuscriptor,
          subData.nombre,
          subData.email,
          subData.telefono,
          subData.tipo
        );

        // Luego, creamos la instancia del contrato, pasándole la instancia del suscriptor.
        // Nota: Los argumentos deben coincidir con el constructor de tu clase Contract.
        const contract = new Contract(
          data.idcontrato,
          new Date(data.fechaContr), // Convertimos el string de fecha a un objeto Date
          data.fechaFin ? new Date(data.fechaFin) : null, // Hacemos lo mismo para la fecha de fin si existe
          data.precioBase,
          subscriptor, // Pasamos la instancia de Subscriptor que creamos arriba
          [], // Asumimos listas vacías para promociones y paquetes por ahora
          []
        );

        this.subscriberInfo = contract; // Asignamos la INSTANCIA REAL
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Error al buscar el contrato:', err);
        this.errorMessage = `No se pudo encontrar el contrato #${this.contractNumber}.`;
        this.isLoading = false;
      }
    });
  }

  // --- 3. LÓGICA BÁSICA PARA CALCULATE DEBT ---
  public calculateDebt(): void {
    if (!this.startDate || !this.endDate) {
      alert('Por favor, seleccione una fecha de inicio y una de fin.');
      return;
    }

    // Usamos los métodos getter porque subscriberInfo es una instancia de la clase
    const contractId = this.subscriberInfo?.getId();
    console.log(`Cálculo de deuda solicitado para el contrato ${contractId} desde ${this.startDate} hasta ${this.endDate}`);
    alert(`Iniciando cálculo de deuda desde ${this.startDate} hasta ${this.endDate}.`);
  }
}
