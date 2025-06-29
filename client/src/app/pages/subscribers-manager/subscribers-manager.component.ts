import { SuscriptoresService } from './../../services/suscriptores.service';
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import  Subscriptor  from './../..//models/subscriptor.model';
import  Contract  from '../../models/contract.model';

@Component({
  selector: 'app-subscribers-manager',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './subscribers-manager.component.html',
  styleUrl: './subscribers-manager.component.scss'
})
export class SubscribersManagerComponent {

  public contractNumber: string = '';
  // --- 2. Propiedades mejoradas para la UI ---
  public subscriberInfo: Contract | null = null; // Usamos el tipo Contract y lo inicializamos en null
  public isLoading: boolean = false; // Para mostrar un spinner o mensaje de carga
  public errorMessage: string | null = null; // Para mostrar errores al usuario

  constructor(private suscriptoresService: SuscriptoresService) { } // El servicio se inyecta en minúscula por convención

  public searchContract(): void {
    if (!this.contractNumber) {
      this.errorMessage = 'Por favor, ingrese un número de contrato.';
      return;
    }

    // --- 3. Lógica de llamada real a la API ---
    this.isLoading = true; // Inicia la carga
    this.errorMessage = null; // Limpia errores previos
    this.subscriberInfo = null; // Limpia datos previos

    this.suscriptoresService.getContractInfo(this.contractNumber).subscribe({
      // Esto se ejecuta si la llamada a la API es exitosa
      next: (data) => {
        this.subscriberInfo = data; // Asigna los datos reales del backend
        this.isLoading = false; // Termina la carga
      },
      // Esto se ejecuta si la API devuelve un error
      error: (err) => {
        console.error('Error al buscar el contrato:', err);
        this.errorMessage = `No se pudo encontrar el contrato #${this.contractNumber}. Verifique el número e intente de nuevo.`;
        this.isLoading = false; // Termina la carga
      }
    });
  }

  public calculateDebt(): void {
    // Esta lógica la implementarás después
  }
}
