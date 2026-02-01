import { Injectable, inject, signal, effect } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, Observable, of } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class CurrencyService {
  private http = inject(HttpClient);

  /** Сигнал выбранной валюты */
  private _selectedCurrency = signal<'UAH' | 'EUR' | 'USD'>('UAH');
  selectedCurrency = this._selectedCurrency.asReadonly();

  /** Курс выбранной валюты к UAH */
  rate = signal(1);

  /** Список доступных валют */
  currencies = ['UAH', 'EUR', 'USD'] as const;

  setCurrency(code: 'UAH' | 'EUR' | 'USD') {
    this._selectedCurrency.set(code);
  }

  /** Получить курс выбранной валюты к UAH */
  getRate(code: string): Observable<number> {
    if (code === 'UAH') return of(1);

    return this.http.get<{ rate: number }>(`/Currency/convert?base=UAH&target=${code}`)
      .pipe(map(res => res.rate));
  }

  constructor() {
    // Срабатывает при смене валюты
    effect(() => {
      const code = this._selectedCurrency();
      if (code === 'UAH') {
        this.rate.set(1);
        return;
      }

      this.getRate(code).subscribe(r => this.rate.set(r));
    });
  }
}
