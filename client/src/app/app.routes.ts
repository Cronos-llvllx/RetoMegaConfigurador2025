import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'promociones',
    pathMatch: 'full'
  },
  {
    path: 'promociones',
    loadComponent: () =>
      import('./promociones/promociones.component').then(m => m.PromocionesComponent)
  },
  {
    path: 'deuda',
    pathMatch: 'full',
    loadComponent: () => import('./pages/debt-calculator/debt-calculator.component').then(c => c.DebtCalculator)
  }
];
