import { Injectable, inject, signal } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, of, tap } from 'rxjs';
import { AuthService } from '../../core/services/auth.service';
import { Basket } from '../models/Basket';
import { Product } from '../models/Product';

@Injectable({ providedIn: 'root' })
export class BasketService {
  private http = inject(HttpClient);
  private auth = inject(AuthService);

  private baseUrl = '/Basket';

  // Signals
  private basketSignal = signal<Basket>({
    userName: '',
    items: [],
    totalPrice: 0
  });
  public basket = this.basketSignal;          // публичная корзина
  public basketCount = signal<number>(0);     // публичный счётчик

  private recalc(basket: Basket) {
    this.basketSignal.set(basket);
    this.basketCount.set(basket.items.reduce((sum, i) => sum + i.quantity, 0));
  }

  private getHeaders() {
    const token = this.auth.getToken();
    return {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        ...(token ? { Authorization: `Bearer ${token}` } : {})
      })
    };
  }

  // Получить корзину с сервера
  getBasket() {
    if (!this.auth.getToken()) return of(this.basketSignal());

    return this.http.get<Basket>(this.baseUrl, this.getHeaders()).pipe(
      tap(b => this.recalc(b)),
      catchError(() => of(this.basketSignal()))
    );
  }

  // Добавить товар
  addToCart(p: Product) {
    const current = this.basketSignal();
    const items = [...current.items];

    const existing = items.find(i => i.productId === p.id);
    if (existing) existing.quantity++;
    else
      items.push({
        productId: p.id,
        productName: p.name,
        price: p.price,
        quantity: 1,
        imageFile: p.imageFile
      });

    const basket: Basket = {
      userName: this.auth.user()?.email || '',
      items,
      totalPrice: items.reduce((sum, i) => sum + i.price * i.quantity, 0)
    };

    this.recalc(basket);

    if (!this.auth.getToken()) return of(basket);

    return this.http.put<Basket>(this.baseUrl, basket, this.getHeaders()).pipe(
      tap(b => this.recalc(b)),
      catchError(() => of(basket))
    );
  }

  // Изменить количество товара
  changeQuantity(productId: string, delta: number) {
    const current = this.basketSignal();
    const items = current.items
      .map(i => i.productId === productId ? { ...i, quantity: i.quantity + delta } : i)
      .filter(i => i.quantity > 0);

    const basket: Basket = {
      ...current,
      items,
      totalPrice: items.reduce((sum, i) => sum + i.price * i.quantity, 0)
    };

    this.recalc(basket);

    if (!this.auth.getToken()) return of(basket);

    return this.http.put<Basket>(this.baseUrl, basket, this.getHeaders()).pipe(
      tap(b => this.recalc(b)),
      catchError(() => of(basket))
    );
  }


  removeItem(productId: string) {
    const current = this.basketSignal();
    const item = current.items.find(i => i.productId === productId);


    const delta = item ? -item.quantity : 0;

    return this.changeQuantity(productId, delta);
  }

}
