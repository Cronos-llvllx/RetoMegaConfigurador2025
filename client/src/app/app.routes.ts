import { Routes } from '@angular/router';
import { NavbarComponent } from './shared/navbar/navbar.component'; // Importa el NavbarComponent
import { DebtCalculator } from './pages/debt-calculator/debt-calculator.component';
//import { SubscribersManager } from './pages/subscribers-manager/subscribers-manager.component';

export const routes: Routes = [
  // Ruta por defecto (redirecciona a la calculadora de deuda)
  { path: '', redirectTo: 'calculo-deuda', pathMatch: 'full' },

  // Este es el layout principal que contendrá el navbar
  // Usamos una ruta vacía para que este layout sea el "root" para las páginas con navbar
  {
    path: '',
    // No especificamos un componente aquí directamente,
    // ya que el AppComponent (nuestro layout principal) ya tiene el navbar y el router-outlet
    children: [
      {
        path: 'calculo-deuda',
        title: 'Calculadora de Deudas', // Puedes añadir un título para la pestaña del navegador
        loadComponent: () => import('./pages/debt-calculator/debt-calculator.component').then(c => c.DebtCalculator)
      },
      {
        path: 'administrador-promociones',
        title: 'Administrador de Promociones',
        // Asume que este componente existe, si no, puedes usar SubscribersManagerComponent temporalmente
        loadComponent: () => import('./pages/debt-calculator/debt-calculator.component').then(c => c.DebtCalculator) // Placeholder
      },
      {
        path: 'administrador-paquetes',
        title: 'Administrador de Paquetes',
        loadComponent: () => import('./pages/debt-calculator/debt-calculator.component').then(c => c.DebtCalculator) // Placeholder
      },
      // {
      //   path: 'gestor-suscriptores',
      //   title: 'Gestor de Suscriptores',
      //   loadComponent: () => import('./pages/subscribers-manager.component').then(c => c.SubscribersManagerComponent)
      // }
      // Puedes añadir más rutas de páginas aquí
    ]
  },
  // Opcional: Ruta para manejar cualquier URL no encontrada
  { path: '**', redirectTo: 'calculo-deuda' }
];
