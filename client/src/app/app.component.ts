import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common'; // Necesario para directivas comunes
import { NavbarComponent } from './shared/navbar/navbar.component'; // Asegúrate de que la ruta sea correcta

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CommonModule, // Importa CommonModule
    RouterOutlet,
    NavbarComponent // Importa el NavbarComponent
  ],
  template: `
    <!-- Aquí se renderiza tu navbar, siempre visible -->
    <app-navbar></app-navbar>

    <!-- Aquí se renderizarán los componentes de las rutas hijas (las "páginas") -->
    <main class="router-content">
      <router-outlet></router-outlet>
    </main>
  `,
  styles: [
    // Opcional: algunos estilos básicos para el contenido principal
    `
      .router-content {
        padding: 20px; /* Un poco de espacio alrededor del contenido */
      }
    `
  ]
})
export class AppComponent {
  // Puedes dejar esto vacío si no necesitas lógica aquí
}
