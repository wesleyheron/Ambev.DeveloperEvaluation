import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { Sale } from '../models/sale.model';

@Injectable({
  providedIn: 'root'
})
export class SalesService {
  private http = inject(HttpClient);
  private baseUrl = 'http://localhost:8080/api/sales';

  getAll(): Observable<Sale[]> {
    return this.http.get<any>(this.baseUrl).pipe(
      map(response => response?.data?.data || [])
    );
  }

  getById(id: string): Observable<Sale> {
    return this.http.get<any>(`${this.baseUrl}/${id}`).pipe(
      map(response => response?.data?.data || {})
    );
  }

  create(sale: Sale): Observable<any> {
    return this.http.post(this.baseUrl, sale);
  }

  update(id: string, sale: Sale): Observable<any> {
    console.log(sale)
    return this.http.put(`${this.baseUrl}/${id}`, sale);
  }

  cancel(id: string): Observable<any> {
    return this.http.patch(`${this.baseUrl}/cancel/${id}`, {});
  }

  delete(id: string) {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }

  cancelItem(saleId: string, itemId: string): Observable<any> {
    return this.http.patch(`${this.baseUrl}/cancel-item/${saleId}/${itemId}`, {});
  }
}
