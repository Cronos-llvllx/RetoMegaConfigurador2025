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
    path: 'paquetes',
    loadComponent: () =>
      import('./paquetes/paquetes.component').then(m => m.PaquetesComponent)
  }
];
