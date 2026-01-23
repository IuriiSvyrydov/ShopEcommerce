import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-reset-password',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule],
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.scss']
})
export class ResetPasswordComponent {
  private route = inject(ActivatedRoute);
  private auth = inject(AuthService);
  private router = inject(Router);

  userId = signal<string>('');
  token = signal<string>('');
  newPassword = signal<string>('');

  constructor() {
    // Получаем query params из URL
    this.route.queryParams.subscribe(params => {
      const userId = params['userId'];
      const token = params['token'];

      this.userId.set(Array.isArray(userId) ? userId[0] : userId || '');
      this.token.set(Array.isArray(token) ? token[0] : token || '');
    });
  }

  submit() {
    // Проверка наличия userId и token
    if (!this.userId() || !this.token()) {
      alert('Invalid or expired reset link');
      this.router.navigate(['/auth/forgot-password']);
      return;
    }

    // Проверка введённого нового пароля
    if (!this.newPassword()) {
      alert('Please enter a new password');
      return;
    }

    // Вызываем метод resetPassword из AuthService
    this.auth.resetPassword(this.userId(), this.token(), this.newPassword())
      .subscribe({
        next: () => {
          alert('Password reset successfully');
          this.router.navigate(['/auth/login']);
        },
        error: err => {
          alert(err.error?.message || 'Failed to reset password');
        }
      });
  }
}
