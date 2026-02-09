import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatCardModule } from '@angular/material/card';
@Component({
  selector: 'app-login',
  standalone: true,
  //imports: [CommonModule, FormsModule],
  imports: [
  CommonModule,
  FormsModule,
  MatCardModule,
  MatFormFieldModule,
  MatInputModule,
  MatButtonModule
],
  templateUrl: './login.html'
})
export class Login {

  username = '';
  password = '';
  error = '';

  constructor(private http: HttpClient, private router: Router) {}

  login() {
    this.http.post<any>('http://localhost/api/Login/login', {
      username: this.username,
      password: this.password
    }).subscribe({
      next: (res) => {
        localStorage.setItem('token', res.token);
        this.router.navigate(['/secure-test']);
      },
      error: () => this.error = 'Invalid login'
    });
  }
}