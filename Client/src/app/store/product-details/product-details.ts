import { Component, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterModule } from '@angular/router';

import { ProductService } from '../services/product.service';
import { BasketService } from '../services/basket.service';
import { Product } from '../models/Product';

@Component({
  selector: 'app-product-details',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './product-details.html',
  styleUrls: ['./product-details.scss']
})
export class ProductDetails implements OnInit {
  private route = inject(ActivatedRoute);
  private productService = inject(ProductService);
  private basketService = inject(BasketService);

  product = signal<Product | null>(null);

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const id = params.get('id');
      if (id) {
        this.productService.getProductById(id).subscribe({
          next: res => this.product.set(res),
          error: err => console.error('Failed to load product details', err)
        });
      }
    });
  }

  addToCart(p: Product) {
    this.basketService.addToCart(p).subscribe({
      next: res => console.log('Basket updated', res),
      error: err => console.error('Error updating basket', err)
    });
  }
}
