import {
  Component,
  inject,
  signal,
  computed
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterModule } from '@angular/router';

import { ProductService } from '../services/product.service';
import { BasketService } from '../services/basket.service';
import { Product } from '../models/Product';
import { CurrencyService } from '../../core/services/currency.service';

@Component({
  selector: 'app-product-details',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './product-details.html',
  styleUrls: ['./product-details.scss']
})
export class ProductDetails {

  private route = inject(ActivatedRoute);
  private productService = inject(ProductService);
  private basketService = inject(BasketService);
  private currencyService = inject(CurrencyService);

  /** товар */
  product = signal<Product | null>(null);

  /** выбранная валюта */
  currency = this.currencyService.selectedCurrency;

  /** курс: 1 EUR = X UAH */
  rate = this.currencyService.rate;

  /** ✅ РЕАКТИВНЫЙ ПЕРЕСЧЁТ ЦЕНЫ */
  convertedPrice = computed(() => {
    const p = this.product();
    if (!p) return '';

    // price в UAH → делим на курс
    return (p.price / this.rate()).toFixed(2);
  });


  constructor() {
    this.route.paramMap.subscribe(params => {
      const id = params.get('id');
      if (!id) return;

      this.productService
        .getProductById(id)
        .subscribe(p => this.product.set(p));
    });
  }

  addToCart(p: Product) {
    this.basketService.addToCart(p).subscribe();
  }
}
