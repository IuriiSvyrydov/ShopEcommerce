import { Component } from '@angular/core';
import { CommonModule, CurrencyPipe } from '@angular/common';
import {Product} from '../store/models/Product';
import {Type} from '../store/models/Type';
import {RouterModule} from '@angular/router';


@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, CurrencyPipe,RouterModule],
  templateUrl: './home.component.html',
  styleUrls: ['home.component.scss']
})
export class HomeComponent {

  // Example featured products
  featuredProducts: Product[] = [
    {
      id: '1',
      name: 'Wireless Headphones',
      summary: 'High-quality sound',
      description: 'Noise-cancelling, Bluetooth 5.0',
      imageFile: 'assets/products/headphones.jpg',
      brand: { id: 'b1', name: 'Sony' },
      type: { id: 't1', name: 'Audio' },
      price: 59.99,
      createDate: new Date().toISOString()
    },
    {
      id: '2',
      name: 'Smart Watch',
      summary: 'Track your fitness',
      description: 'Heart rate monitor, GPS',
      imageFile: 'assets/products/watch.jpg',
      brand: { id: 'b2', name: 'Samsung' },
      type: { id: 't2', name: 'Wearable' },
      price: 99.99,
      createDate: new Date().toISOString()
    }
  ];

  // Example categories (types)
  categories: Type[] = [
    { id: 't1', name: 'Audio' },
    { id: 't2', name: 'Wearable' },
    { id: 't3', name: 'Home' }
  ];

  toggleAccordion(event: Event) {
    const item = (event.target as HTMLElement).closest('.accordion-item');
    if (item) item.classList.toggle('active');
  }

  addToCart(product: Product) {
    console.log('Add to cart', product);
    // TODO: Call basket service
  }
}
