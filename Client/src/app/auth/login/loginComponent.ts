import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';

interface LoginModel {
  email: string;
  password: string;
}

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './loginComponent.html',
  styleUrls: ['./loginComponent.scss'],
})
export class LoginComponent {
  private auth = inject(AuthService);
  private router = inject(Router);

  // Модель для формы
  model: LoginModel = {email: '', password: ''}

  loading: boolean = false;
  error: string | null = null;

  submit() {
    this.error = null;
    this.loading = true;

    this.auth.login(this.model).subscribe({
      next: () => {
        this.loading = false;
        this.router.navigate(['/']);
      },
      error: err => {
        this.loading = false;
        this.error = err.error?.message || 'Login failed';
      }
    });
  }
}
