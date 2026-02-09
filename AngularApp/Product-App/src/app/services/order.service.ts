import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Order } from '../models/order';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  private apiUrl = 'http://localhost/api/orders';

  constructor(private http: HttpClient) {}

  // getMyOrders(): Observable<Order[]> {
  //   return this.http.get<Order[]>(this.apiUrl);
  // }
  getMyOrders(): Observable<Order[]> {
  return this.http.get<Order[]>(this.apiUrl, {
    headers: this.getAuthHeaders()
  });
}


  // getOrderById(id: number): Observable<Order> {
  //   return this.http.get<Order>(`${this.apiUrl}/${id}`);
  // }
  getOrderById(id: number): Observable<Order> {
  return this.http.get<Order>(`${this.apiUrl}/${id}`, {
    headers: this.getAuthHeaders()
  });
}


createOrder(order: any): Observable<any> {
  return this.http.post(this.apiUrl, order, {
    headers: {
      Authorization: `Bearer ${localStorage.getItem('token')}`
    }
  });
}
  private getAuthHeaders() {
  return {
    Authorization: `Bearer ${localStorage.getItem('token')}`
  };
}

}