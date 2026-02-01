import { Component, inject, OnInit, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { BasketService } from '../services/basket.service';
import { BasketItem } from '../models/Basket';
import { CurrencyService } from '../../core/services/currency.service';

@Component({
  selector: 'app-basket',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.scss']
})
export class BasketComponent implements OnInit {
  private basketService = inject(BasketService);
  private currencyService = inject(CurrencyService);

  basket = this.basketService.basket;
  currency = this.currencyService.selectedCurrency;
  rate = this.currencyService.rate;

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
    if (!this.basket()?.items.length) {
      alert('Корзина пуста');
      return;
    }
    alert('TODO: подключить OrderService для создания заказа');
  }

  /** Метод смены валюты */
  changeCurrency(code: string) {
    this.currencyService.setCurrency(code as any);
  }
}
