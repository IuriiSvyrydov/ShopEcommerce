import { HttpClient } from "@angular/common/http";
import { Injectable, signal } from "@angular/core";
import { inject } from "@angular/core/primitives/di";
import { CreateOrderDto } from "../models/Orders/CreateOrderDto";
import { tap } from "rxjs";

@Injectable({providedIn: 'root'})
export class OrderService {
  private readonly apiUrl = '/Order';
  loading = signal(false);
  error = signal<string | null>(null);
  private http = inject(HttpClient);

  checkout(dto: CreateOrderDto) {
    this.loading.set(true);
    this.error.set(null);
    return this.http.post<string>(this.apiUrl, dto).pipe(
      tap({
        error: err => {
          this.error.set(err?.message ?? 'Order checkout failed');
          this.loading.set(false);
        },
        next: () =>
          this.loading.set(false)
      })
    );
  }

  createOrder(dto: CreateOrderDto) {
    this.loading.set(true);
    this.error.set(null);

    return this.http.post<{ orderId: string }>(this.apiUrl, dto).pipe(
      tap({
        next: () => this.loading.set(false),
        error: err => {
          this.error.set(err?.message ?? 'Order creation failed');
          this.loading.set(false);
        }
      })
    );
  }

}
