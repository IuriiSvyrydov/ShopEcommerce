import { Injectable, inject, signal, effect, computed } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, Observable, of } from 'rxjs';

export type CurrencyCode = 'UAH' | 'EUR' | 'USD';

@Injectable({ providedIn: 'root' })
export class CurrencyService {
  private http = inject(HttpClient);

  // ================= Signals =================
  /** Сигнал выбранной валюты */
  private _selectedCurrency = signal<CurrencyCode>('UAH');
  selectedCurrency = this._selectedCurrency.asReadonly();

  /** Курс выбранной валюты к UAH */
  private _rate = signal<number>(1);
  rate = this._rate.asReadonly();

  /** Список доступных валют */
  currencies: CurrencyCode[] = ['UAH', 'EUR', 'USD'];

  /** Базовая цена в UAH (для конвертации) */
  private _basePriceUAH = signal<number>(0);

  /** Конвертированная цена для отображения */
  convertedPrice = computed(() => {
    return +(this._basePriceUAH() * this._rate()).toFixed(2);
  });

  // ================= API =================
  /** Получить курс выбранной валюты к UAH */
  private getRateFromApi(target: CurrencyCode): Observable<number> {
    if (target === 'UAH') return of(1);

    return this.http
      .get<{ rate: number }>(`/Currency/rate?baseCurrency=UAH&targetCurrency=${target}`)
      .pipe(map(res => res.rate));
  }

  // ================= Public methods =================
  /** Установить выбранную валюту */
  setCurrency(code: CurrencyCode) {
    this._selectedCurrency.set(code);
  }

  /** Установить базовую цену в UAH (для конвертации) */
  setBasePriceUAH(price: number) {
    this._basePriceUAH.set(price);
  }

  /** Получить курс для конкретной валюты (можно использовать в async pipe) */
  getRate(code: CurrencyCode): Observable<number> {
    return this.getRateFromApi(code);
  }

  constructor() {
    // ================= Effect для конвертации =================
    effect(() => {
      const currency = this._selectedCurrency();

      // Если выбрана UAH — курс = 1
      if (currency === 'UAH') {
        this._rate.set(1);
        return;
      }

      // Получаем курс с backend
      this.getRateFromApi(currency).subscribe({
        next: r => this._rate.set(r),
        error: () => this._rate.set(1), // fallback
      });
    });
  }
}
