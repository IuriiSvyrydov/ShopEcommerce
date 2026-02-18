import { Component, inject } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { PaymentService } from '../services/payment.service';

@Component({
  selector: 'app-payment',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './payment.component.html',
  styleUrl: './payment.component.scss',
})
export class PaymentComponent {

  private route = inject(ActivatedRoute);
  public paymentService = inject(PaymentService);

  orderId!: string;
  amount = 0;
  currency = 'UAH';

  constructor() {
    this.route.paramMap.subscribe(params => {
      const id = params.get('id');
      if (id) {
        this.orderId = id;
        this.paymentService.loadPaymentStatus(id);
      }
    });
  }

  pay() {
    this.paymentService.processPayment(this.orderId, this.amount, this.currency)
      .subscribe({
        next: () => {
          this.paymentService.loadPaymentStatus(this.orderId);
        }
      });
  }
}
