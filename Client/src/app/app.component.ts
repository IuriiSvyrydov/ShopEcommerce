import {Component, inject, OnInit} from '@angular/core';
import {AuthService} from './core/services/auth.service';
import {NavbarComponent} from './core/navbar/navbar';
import {Loading} from './core/loading/loading';
import {RouterOutlet} from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [NavbarComponent, Loading, RouterOutlet],
  template: `
    <app-navbar></app-navbar>
    <router-outlet></router-outlet>
    <app-loading></app-loading>
  `,
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  private authService = inject(AuthService);
  constructor() {
    this.authService.restoreUser();
  }

  ngOnInit(): void {
    this.authService.tryRefresh().subscribe({
      next: () => this.authService.authReady.set(true),
      error: () => this.authService.authReady.set(true)
    });
  }
}
