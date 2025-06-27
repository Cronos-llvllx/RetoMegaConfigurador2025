import { Routes } from '@angular/router';

export const routes: Routes = [{
  path: '',
  loadComponent: () => import('./pages/debt-calculator/debt-calculator.component').then(c => c.DebtCalculator)
}];
