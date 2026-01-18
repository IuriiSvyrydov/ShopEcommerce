import {Component, Inject, inject} from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import {Router, RouterLink, RouterModule} from '@angular/router';

import { BasketService } from '../../store/services/basket.service';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: 'navbar.html',
  styleUrls: ['./navbar.scss'],
})
export class NavbarComponent {
  private basketService = inject(BasketService);
  auth = inject(AuthService);
  isAuthReady = this.auth.isAuthReady;
  isLoggedIn = this.auth.isLoggedIn;
  user = this.auth.user;
   router = inject(Router);


  searchText = '';



  get cartCount() {
    return this.basketService.basketCount();
  }

  logout() {
    this.auth.logout();
  }

  onSearch() {
    const term = this.searchText.trim();
    this.router.navigate(['/store'], term ? { queryParams: { search: term } } : {});
  }
}

