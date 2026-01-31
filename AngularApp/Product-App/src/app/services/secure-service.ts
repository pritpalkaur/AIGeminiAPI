import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root',
})
export class SecureTestService {
private apiUrl = 'https://localhost:59257/api/SecureTest/hello';

  constructor(private http: HttpClient) {}

  private getAuthHeaders(): HttpHeaders {
    const token = localStorage.getItem('token');

    return new HttpHeaders({
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    });
  }

  getHelloMessage(): Observable<any> {
    return this.http.get<any>(this.apiUrl, {
      headers: this.getAuthHeaders()
    });
  }
}

