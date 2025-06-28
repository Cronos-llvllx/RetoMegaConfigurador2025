// import { Component, OnInit } from '@angular/core';
// import { CommonModule } from '@angular/common';
// import { FormsModule } from '@angular/forms';
// import { SuscriptoresService } from '../../services/suscriptores.service';
// import Subscriptor from '../../models/subscriptor.model';
// import Promotion from '../../models/promotion.model';
// import { NewContractRequest } from '../../models/new-contract-request.model';
// import { ContractInfo } from '../../models/contract-info.model';

// @Component({
//   selector: 'app-subscribers-manager',
//   standalone: true,
//   imports: [CommonModule, FormsModule], // Usamos FormsModule para el two-way binding
//   templateUrl: './subscribers-manager.component.html',
//   styleUrl: './subscribers-manager.component.scss'
// })
// export class SubscribersManagerComponent implements OnInit {

//   // --- Propiedades para la búsqueda ---
//   contractNumber: string = '';
//   // Usamos el DTO del backend para la respuesta
//   subscriberInfo: ContractInfo | null = null;

//   // --- Propiedades para el cálculo de deuda ---
//   startDate: string = '';
//   endDate: string = '';

//   // --- Propiedades para el formulario de alta ---
//   // Unimos los datos del suscriptor y el contrato en un solo objeto para el alta
//   newAltaRequest: NewContractRequest = {
//     subscriber: { name: '', email: '', phoneNumber: '', idNeighborhood: 0, type: 1 } as Subscriber,
//     baseContractPrice: 0,
//     idManualPromotion: null,
//     packageIds: []
//   };

//   availablePromotions: Promotion[] = []; // Almacena las promociones para el dropdown

//   constructor(private subscribersService: SuscriptoresService) { } // Inyección del servicio

//   ngOnInit(): void {
//     // Al iniciar el componente, carga las promociones de contratación disponibles.
//     this.loadContractPromotions();
//   }

//   /**
//    * Busca un suscriptor por número de contrato.
//    */
//   searchContract(): void {
//     if (!this.contractNumber) {
//       alert('Por favor, ingresa un número de contrato.');
//       return;
//     }

//     console.log('Buscando contrato:', this.contractNumber);
//     // Llama al servicio backend para obtener la información.
//     // Notar: La API necesita un endpoint para buscar por contrato, que aún no hemos implementado.
//     // Usaremos un ID de suscriptor para simular la búsqueda.
//     const subscriberId = parseInt(this.contractNumber, 10);

//     if (isNaN(subscriberId)) {
//         alert('Por favor, ingrese un número válido para buscar.');
//         return;
//     }

//     this.subscribersService.getSubscriberById(subscriberId).subscribe({
//       next: (data: Subscriber) => {
//         // Asume un DTO para mostrar la info del suscriptor
//         this.subscriberInfo = {
//           contractNumber: this.contractNumber,
//           name: data.name,
//           contractDate: 'N/A', // La API no devuelve la fecha de contrato
//           status: 'ACTIVO' // Se define manualmente por ahora
//         };
//         console.log('Suscriptor encontrado:', this.subscriberInfo);
//       },
//       error: (err) => {
//         console.error('Error al buscar suscriptor:', err);
//         this.subscriberInfo = null; // Limpia la información si no se encuentra
//         alert('No se encontró el suscriptor o hubo un error en la conexión.');
//       }
//     });
//   }

//   /**
//    * Crea un nuevo suscriptor y su contrato en una sola llamada a la API.
//    */
//   createSubscriberAndContract(): void {
//     const subscriber = this.newAltaRequest.subscriber;

//     // Validación básica
//     if (!subscriber.name || !subscriber.email || !subscriber.phoneNumber) {
//       alert('Por favor, completa todos los campos del suscriptor.');
//       return;
//     }

//     // Llama al servicio para enviar el DTO completo al backend
//     this.subscribersService.createContract(this.newAltaRequest).subscribe({
//       next: (response) => {
//         console.log('Suscriptor y Contrato creados exitosamente!', response);
//         alert('¡Suscriptor y contrato creados con éxito!');
//         // Limpiar el formulario después de la creación exitosa
//         this.newAltaRequest = { subscriber: { name: '', email: '', phoneNumber: '', idNeighborhood: 0, type: 1 } as Subscriber, baseContractPrice: 0, idManualPromotion: null, packageIds: [] };
//       },
//       error: (err) => {
//         console.error('Error al crear suscriptor y contrato:', err);
//         alert('Error al crear. Verifica los datos.');
//       }
//     });
//   }

//   /**
//    * Lógica para el botón de cálculo de deuda.
//    */
//   calculateDebt(): void {
//     if (!this.startDate || !this.endDate) {
//       alert('Por favor, selecciona un rango de fechas.');
//       return;
//     }
//     // TODO: Implementar la llamada al servicio de cálculo de deuda de Obed.
//     alert(`Lógica de cálculo de deuda pendiente para el contrato ${this.contractNumber} del ${this.startDate} al ${this.endDate}.`);
//   }

//   /**
//    * Carga las promociones de contratación (tipo 1) desde el backend.
//    */
//   loadContractPromotions(): void {
//     // Llama al servicio para obtener las promociones de tipo 1.
//     this.subscribersService.getPromotionsByType(1).subscribe({
//       next: (promos: Promotion[]) => {
//         this.availablePromotions = promos;
//         console.log('Promociones de contratación cargadas:', this.availablePromotions);
//       },
//       error: (err) => {
//         console.error('Error al cargar promociones:', err);
//         this.availablePromotions = [];
//         alert('Error al cargar las promociones. Verifica la conexión con el backend.');
//       }
//     });
//   }
// }
