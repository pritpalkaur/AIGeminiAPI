import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Product } from '../models/product';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private apiUrl = 'https://localhost:59257/api/Product';

  constructor(private http: HttpClient) {}

  private getAuthHeaders(): HttpHeaders {
    const token = localStorage.getItem('token');

    return new HttpHeaders({
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    });
  }

  getProducts(): Observable<Product[]> {
   // alert('In getProducts method of ProductService');
    return this.http.get<Product[]>(this.apiUrl, {
      headers: this.getAuthHeaders()
    });
  }

  addProduct(product: Product): Observable<Product> {
    return this.http.post<Product>(this.apiUrl, product, {
      headers: this.getAuthHeaders()
    });
  }

  updateProduct(product: Product): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${product.id}`, product, {
      headers: this.getAuthHeaders()
    });
  }

  deleteProduct(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`, {
      headers: this.getAuthHeaders()
    });
  }
}