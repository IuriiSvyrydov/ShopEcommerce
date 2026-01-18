import { Component, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { BasketService } from '../services/basket.service';
import { Basket, BasketItem } from '../models/Basket';

@Component({
  selector: 'app-basket',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.scss']
})
export class BasketComponent implements OnInit {
  private basketService = inject(BasketService);

  basket = this.basketService.basket;  // Используем сигнал из сервиса

  get cartCount() {
    return this.basketService.basketCount();
  }

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
  checkout(){


      alert('Корзина пуста');
      return;


    // TODO: подключить OrderService для создания заказа


  }

}
