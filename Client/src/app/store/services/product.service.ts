import { inject, Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CatalogResponse } from '../models/CatalogResponse';
import { Brand } from '../models/Brands';
import { Type } from '../models/Type';
import { Product } from '../models/Product';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private http = inject(HttpClient);

  // Ocelot upstream
  private baseUrl = '/Catalog';

  getAllProducts(
    page: number,
    size: number,
    brand?: string | null,
    type?: string | null,
    sort?: string | null,
    search?: string | null
  ): Observable<CatalogResponse> {

    let params = new HttpParams()
      .set('pageIndex', page)
      .set('pageSize', size);

    if (brand) params = params.set('brand', brand);
    if (type) params = params.set('type', type);
    if (sort && sort !== 'default') params = params.set('sort', sort);
    if (search) params = params.set('search', search);

    // ⬇️ ВАЖЛИВО
    return this.http.get<CatalogResponse>(
      `${this.baseUrl}/products`,
      { params }
    );
  }

  getAllBrands(): Observable<Brand[]> {
    return this.http.get<Brand[]>(`${this.baseUrl}/brands`);
  }

  getAllTypes(): Observable<Type[]> {
    return this.http.get<Type[]>(`${this.baseUrl}/types`);
  }

  getProductById(id: string): Observable<Product> {
    return this.http.get<Product>(`${this.baseUrl}/products/${id}`);
  }

  getProductsByBrand(brandId: string): Observable<Product[]> {
    return this.http.get<Product[]>(
      `${this.baseUrl}/products/brand/${brandId}`
    );
  }

  getProductsByName(name: string): Observable<Product[]> {
    return this.http.get<Product[]>(
      `${this.baseUrl}/products/name/${name}`
    );
  }
}
