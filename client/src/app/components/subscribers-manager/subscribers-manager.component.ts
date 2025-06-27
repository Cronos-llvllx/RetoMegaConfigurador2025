import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SubscribersService } from '../../services/subscribers.service';
import { Subscriber } from '../../models/subscriber.model';
import { Contract } from '../../models/contract.model';

@Component({
  selector: 'app-subscribers-manager',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './subscribers-manager.component.html',
  styleUrl: './subscribers-manager.component.scss'
})
export class SubscribersManagerComponent implements OnInit {

  // Propiedades para almacenar datos del formulario e información del suscriptor
  contractNumber: string = '';
  subscriberInfo: any; // Utilice un tipo más específico si crea uno
  startDate: string = '';
  endDate: string = '';

  // Inyecta tu servicio en el constructor
  constructor(private subscribersService: SubscribersService) { }

  ngOnInit(): void {
    // Puede agregar lógica de inicialización aquí si es necesario
  }

  // Método a llamar cuando se hace clic en el botón "Buscar"
  searchContract(): void {
    console.log('Searching for contract number:', this.contractNumber);
// Llama a tu servicio backend aquí.
// Por ahora, simulemos una llamada y actualicemos la interfaz de usuario.
// this.subscribersService.getSubscriberInfo(this.contractNumber).subscribe(data => {
// this.subscriberInfo = data;
// });

    // Simular la respuesta de la API con datos simulados de la imagen
    this.subscriberInfo = {
      contractNumber: this.contractNumber,
      name: 'Juan Escutia',
      contractDate: '2025-03-01',
      status: 'ACTIVO'
    };
  }

  // Método que se llamará cuando se haga clic en el botón "Calcular"
  calculateDebt(): void {
    console.log('Calculating debt from', this.startDate, 'to', this.endDate);
// Aquí llamarías al punto final de tu API de cálculo de deuda.
// this.subscribersService.calculateDebt(this.contractNumber, this.startDate, this.endDate).subscribe(debt => {
// console.log('Total debt:', debt);
// });
    alert(`Calculando deuda para el contrato ${this.contractNumber} del ${this.startDate} al ${this.endDate}.`);
  }

}
