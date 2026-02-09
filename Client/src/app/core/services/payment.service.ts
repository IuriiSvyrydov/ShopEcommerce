import { Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { PaymentDto } from '../../store/models/Payment/PaymentDto';

@Injectable({
  providedIn: 'root'
})
export class PaymentService {
  private readonly apiUrl = '/Payment';

  // STATE
  payment = signal<PaymentDto | null>(null);
  loading = signal(false);
  error = signal<string | null>(null);

  constructor(private http: HttpClient) {}

  loadPaymentStatus(orderId: string) {
    this.loading.set(true);
    this.error.set(null);

    this.http
      .get<PaymentDto>(`${this.apiUrl}/status/${orderId}`)
      .subscribe({
        next: payment => {
          this.payment.set(payment);
          this.loading.set(false);
        },
        error: err => {
          this.error.set(err.message ?? 'Payment load failed');
          this.loading.set(false);
        }
      });
  }

  checkout(orderId: string) {
    this.loading.set(true);

    return this.http.post<void>(`${this.apiUrl}/checkout`, { orderId });
  }

  clear() {
    this.payment.set(null);
    this.error.set(null);
  }
}
