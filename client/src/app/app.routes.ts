import { Routes } from '@angular/router';

import { ListaPaquetesComponent } from './modules/paquetes/pages/lista-paquetes/lista-paquetes.component';
import { CrearPaqueteComponent } from './modules/paquetes/pages/crear-paquete/crear-paquete.component';
import { EditarPaqueteComponent } from './modules/paquetes/pages/editar-paquete/editar-paquete.component';

export const routes: Routes = [
  { path: '', redirectTo: 'paquetes', pathMatch: 'full' },
  { path: 'paquetes', component: ListaPaquetesComponent },
  { path: 'paquetes/crear', component: CrearPaqueteComponent },
  { path: 'paquetes/editar/:id', component: EditarPaqueteComponent }
];
