import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule, Router } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';
import { RegisterRequest } from '../../store/models/RegisterRequest';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent {
  private auth = inject(AuthService);
  private router = inject(Router);

  model: RegisterRequest = {
    firstName: '',
    lastName: '',
    email: '',
    password: '',
  };

  loading = false;
  error: string | null = null;
  submit() {
    this.error = null;
    this.loading = true;

    this.auth.register(this.model).subscribe({
      next: () => {
        this.loading = false;
        this.router.navigate(['/']);
      },
      error: err => {
        this.loading = false;

        if (err.error?.errors) {
          this.error = err.error.errors
            .map((e: any) => e.Description || e.description)
            .join(', ');
        } else if (err.error?.message) {
          this.error = err.error.message;
        } else {
          this.error = 'Registration failed';
        }

        console.error(err);
      }
    });
  }

}
