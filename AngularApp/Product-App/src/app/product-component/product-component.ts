import { Component } from '@angular/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

import { Product } from '../models/product';

@Component({
  selector: 'app-product',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    HttpClientModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule
  ],
  templateUrl: './product-component.html',
  styleUrl: './product-component.css',
})
export class ProductComponent {
  displayedColumns: string[] = ['id', 'name', 'price', 'actions'];
  products: Product[] = [];
  newProduct: Product = { id: 0, name: '', price: 0 };

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.http.get<Product[]>('assets/products.json')
      .subscribe(data => this.products = data);
  }

  addProduct() {
    this.products.push({ ...this.newProduct });
    this.newProduct = { id: 0, name: '', price: 0 };
  }

  deleteProduct(id: number) {
    this.products = this.products.filter(p => p.id !== id);
  }

  editProduct(product: Product) {
    this.newProduct = { ...product };
    this.deleteProduct(product.id);
  }
}


// import { Component } from '@angular/core';
// import { HttpClient,HttpClientModule } from '@angular/common/http';
// import { Product } from '../models/product';
// import { FormsModule } from '@angular/forms';
// import { CommonModule } from '@angular/common';

// @Component({
//   selector: 'app-product',
//   standalone: true,
//   imports: [
//     HttpClientModule,
//     FormsModule,
//     CommonModule
//   ],   // <-- Add this
//   templateUrl: './product-component.html',
//   styleUrl: './product-component.css',
// })

// export class ProductComponent {

//   products: Product[] = [];
//   newProduct: Product = { id: 0, name: '', price: 0 };

//   constructor(private http: HttpClient) {}

//   ngOnInit() {
//     this.http.get<Product[]>('assets/products.json')
//       .subscribe(data => this.products = data);
//   }

//   addProduct() {
//     this.products.push({ ...this.newProduct });
//     this.newProduct = { id: 0, name: '', price: 0 };
//   }

//   deleteProduct(id: number) {
//     this.products = this.products.filter(p => p.id !== id);
//   }

//   editProduct(product: Product) {
//     this.newProduct = { ...product };
//     this.deleteProduct(product.id);
//   }
// } 

// // import { Component } from '@angular/core';

// // @Component({
// //   selector: 'app-product-component',
// //    standalone: true,
// //   imports: [],
// //   templateUrl: './product-component.html',
// //   styleUrl: './product-component.css',
// // })
// // export class ProductComponent {

// // }
