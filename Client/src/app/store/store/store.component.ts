import { Component, inject, OnInit, signal, computed } from '@angular/core';
import {CommonModule, NgOptimizedImage} from '@angular/common';
import {ActivatedRoute, RouterLink} from '@angular/router';
import { ProductService } from '../services/product.service';
import { Product } from '../models/Product';
import { Brand } from '../models/Brands';
import { Type } from '../models/Type';
import {BasketService} from '../services/basket.service';


@Component({
  selector: 'app-store',
  standalone: true,
  imports: [CommonModule,RouterLink],
  templateUrl: './store.component.html',
  styleUrls: ['./store.component.scss']
})
export class StoreComponent implements OnInit {
  private productService = inject(ProductService);
  private route = inject(ActivatedRoute);
  private basketService = inject(BasketService);

  // signals
  products = signal<Product[]>([]);
  brands = signal<Brand[]>([]);
  types = signal<Type[]>([]);
  totalCount = signal(0);
  currentPage = signal(1);
  pageSize = 10;
  searchTerm = signal('');
  selectedBrand = signal<string | null>(null);
  selectedType = signal<string | null>(null);
  sortOption = signal('default');

  ngOnInit(): void {
    this.loadBrands();
    this.loadTypes();
    this.loadProducts();

    this.route.queryParams.subscribe(params => {
      this.searchTerm.set(params['search'] || '');
      this.currentPage.set(1);
      this.loadProducts();
    });
  }

  loadProducts() {
    this.productService.getAllProducts(
      this.currentPage(),
      this.pageSize,
      this.selectedBrand(),
      this.selectedType(),
      this.sortOption(),
      this.searchTerm()
    ).subscribe(res => {
      this.products.set(res.data);
      this.totalCount.set(res.count);
    });
  }

  loadBrands() {
    this.productService.getAllBrands().subscribe(res => this.brands.set(res));
  }

  loadTypes() {
    this.productService.getAllTypes().subscribe(res => this.types.set(res));
  }

  applyFilters() {
    this.currentPage.set(1);
    this.loadProducts();
  }

  //Add to Cart functionality
  addToCart(product: Product) {
    const item = {
      productId: product.id,
      productName: product.name,
      price: product.price,
      quantity: 1,
      imageFile: product.imageFile
    };

    this.basketService.addToCart(product).subscribe({
      next: res => console.log('Basket updated', res),
      error: err => console.error('Error updating basket', err)
    });
  }


  resetFilters() {
    this.searchTerm.set('');
    this.selectedBrand.set(null);
    this.selectedType.set(null);
    this.sortOption.set('default');
    this.currentPage.set(1);
    this.loadProducts();
  }

  goToPage(page: number) {
    if (page >= 1 && page <= this.totalPages()) {
      this.currentPage.set(page);
      this.loadProducts();
    }
  }

  onSortChange(event: Event) {
    const select = event.target as HTMLSelectElement;
    this.sortOption.set(select.value);
    this.applyFilters();
  }

  // filtered & sorted products
  filteredProducts = computed(() => {
    let result = this.products();

    if (this.searchTerm()) {
      const term = this.searchTerm().toLowerCase();
      result = result.filter(p =>
        p.name.toLowerCase().includes(term) ||
        p.brand?.name?.toLowerCase().includes(term) ||
        p.type?.name?.toLowerCase().includes(term) ||
        (p.description && p.description.toLowerCase().includes(term))
      );
    }

    if (this.selectedBrand()) {
      result = result.filter(p => p.brand?.name?.toLowerCase() === this.selectedBrand()?.toLowerCase());
    }

    if (this.selectedType()) {
      result = result.filter(p => p.type?.name?.toLowerCase() === this.selectedType()?.toLowerCase());
    }

    if (this.sortOption() === 'priceAsc') {
      result = [...result].sort((a, b) => a.price - b.price);
    } else if (this.sortOption() === 'priceDesc') {
      result = [...result].sort((a, b) => b.price - a.price);
    }

    return result;
  });

  paginationProducts = computed(() => {
    const start = (this.currentPage() - 1) * this.pageSize;
    const end = start + this.pageSize;
    return this.filteredProducts().slice(start, end);
  });
  getImagePath(imageFile: string | undefined): string {
    if (!imageFile) return 'assets/images/default-image.png';
    // Если imageFile уже относительный путь внутри assets, возвращаем его
    if (imageFile.startsWith('images/')) return `assets/${imageFile}`;
    // Иначе добавляем к папке products
    return `assets/images/products/${imageFile}`;
  }


  totalPages = computed(() => Math.ceil(this.totalCount() / this.pageSize));
}
