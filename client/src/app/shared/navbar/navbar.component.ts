import { Component } from '@angular/core';
import { CommonModule } from '@angular/common'; // Para directivas como ngIf, ngFor
import { RouterModule } from '@angular/router'; // Para las directivas routerLink y routerLinkActive

@Component({
  selector: 'app-navbar',
  standalone: true, // Se declara como un componente standalone
  imports: [CommonModule, RouterModule], // Importa los módulos necesarios
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss'
})
export class NavbarComponent {

}
