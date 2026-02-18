import { Component, inject, OnInit, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { BasketService } from '../services/basket.service';
import { BasketItem } from '../models/Basket';
import { CurrencyService } from '../services/currency.service';
import { OrderService } from '../services/order.service';
import { CreateOrderDto } from '../models/Orders/CreateOrderDto';
import { AuthService } from '../../core/services/auth.service';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-basket',
  standalone: true,
  imports: [CommonModule, RouterModule, ReactiveFormsModule],
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.scss']
})
export class BasketComponent implements OnInit {
  private basketService = inject(BasketService);
  private currencyService = inject(CurrencyService);
  private orderService = inject(OrderService);
  private authService = inject(AuthService);
  private router = inject(Router);
  private fb = inject(FormBuilder);

  basket = this.basketService.basket;
  currency = this.currencyService.selectedCurrency;
  rate = this.currencyService.rate;

  checkoutForm = this.fb.group({
    firstName: ['',Validators.required],
    lastName: ['',Validators.required],
    email: ['',Validators.required],
    address: ['',Validators.required],
    country: ['',Validators.required],
    state: ['',Validators.required],
    zipCode: ['',Validators.required],
    paymentMethod: [1, Validators.required],
  });

  /** Список доступных валют для селекта */
  currencies = this.currencyService.currencies;

  /** Пересчитанная итоговая сумма */
  totalPrice = computed(() => {
    const b = this.basket();
    if (!b) return 0;
    return (b.items.reduce((sum, item) => sum + item.price * item.quantity, 0) / this.rate()).toFixed(2);
  });

  /** Пересчитанная цена для конкретного товара */
  itemPrice = (item: BasketItem) => computed(() => (item.price / this.rate()).toFixed(2));


  ngOnInit(): void {
    this.basketService.getBasket().subscribe();
  }

  increment(item: BasketItem) {
    this.basketService.changeQuantity(item.productId, +1).subscribe();
  }

  decrement(item: BasketItem) {
    this.basketService.changeQuantity(item.productId, -1).subscribe();
  }

  removeItem(item: BasketItem) {
    this.basketService.removeItem(item.productId).subscribe();
  }

checkout() {
   if (!this.basket()?.items.length || this.checkoutForm.invalid) {
    this.checkoutForm.markAllAsTouched();
    return;
  }

  const f = this.checkoutForm.getRawValue();

  const orderPayload: CreateOrderDto = {
  userName: this.authService.user()?.email ?? 'guest',
  totalPrice: Number(this.totalPrice()),
  firstName: f.firstName!,
  lastName: f.lastName!,
  emailAddress: f.email!,
  addressLine: f.address!,
  country: f.country!,
  state: f.state!,
  zipCode: f.zipCode!,
  paymentMethod: Number(f.paymentMethod), // ← number
  currency: this.currency(),
    cardName: f.firstName + ' ' + f.lastName,

};
  this.orderService.createOrder(orderPayload).subscribe({
    next: res => {
      const orderId = res.orderId; // теперь корректно
      console.log('Order created with id:', orderId);
      this.router.navigate(['/payment', orderId]);
    },
    error: err => console.error('Failed to create order:', err)
  });
}
  /** Метод смены валюты */
  changeCurrency(code: string) {
    this.currencyService.setCurrency(code as any);
  }
}
