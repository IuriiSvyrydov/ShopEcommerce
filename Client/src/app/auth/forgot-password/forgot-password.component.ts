import {Component, inject, signal} from '@angular/core';
import {RouterModule} from '@angular/router';
import {CommonModule} from '@angular/common';
import {FormsModule} from '@angular/forms';
import {AuthService} from '../../core/services/auth.service';

@Component({
  selector: 'app-forgot-password',
  imports: [CommonModule,FormsModule, RouterModule],
  standalone: true,
  templateUrl: './forgot-password.Component.html',
  styleUrl: './forgot-password.Component.scss',
})
export class ForgotPasswordComponent {
  email =signal ('');
  message =signal ('');
  loading =signal (false);

  private auth = inject(AuthService);

  submit(){
    if (!this.email()) return;
    this.loading.set(true);
    this.message.set('');
    this.auth
      .forgotPassword(this.email(),`${window.origin}/auth/reset-password`)
      .subscribe({
        next: res=>{
          this.message.set(res.message|| 'Check your email for the reset password link')
          this.loading.set(false);
        },
        error: err=>{
          this.message.set(err.error?.message|| 'Failed to send reset password email')
          this.loading.set(false);
        },
      
      });
  }

}
