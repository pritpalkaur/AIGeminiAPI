import { Component } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { catchError } from 'rxjs/operators';
import { of } from 'rxjs';

import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

import { Product } from '../models/product';
import { ProductService } from '../services/product.service';

@Component({
  selector: 'app-product',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
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
  displayedColumns: string[] = ['id', 'name', 'price','stock', 'actions'];
  products: Product[] = [];
  newProduct: Product = { id: 0, name: '', price: 0, stock: 0 };

  constructor(private productService: ProductService) {}

  ngOnInit() {
    this.loadProducts();
  }

  loadProducts() {
    console.log('Loading products...');
    this.productService.getProducts().subscribe({
      next: (data) => this.products = data,
      error: (err) => console.error('Error loading products', err)
    });
  }

  // addProduct() {
  //   this.productService.addProduct(this.newProduct).subscribe({
  //     next: () => {
  //       this.loadProducts();
  //       this.newProduct = { id: 0, name: '', price: 0 };
  //     },
  //     error: (err) => console.error('Error adding product', err)
  //   });
  // }
  addProduct() {
  this.productService.addProduct(this.newProduct)
    .pipe(
      catchError(err => {
        console.error('Error adding product:', err);
        return of(null); // return fallback so app doesn't crash
      })
    )
    .subscribe(result => {
      if (result) {
        this.loadProducts();
        this.newProduct = { id: 0, name: '', price: 0, stock: 0 };
      }
    });
}


  deleteProduct(id: number) {
    this.productService.deleteProduct(id).subscribe({
      next: () => this.loadProducts(),
      error: (err) => console.error('Error deleting product', err)
    });
  }

  editProduct(product: Product) {
    this.newProduct = { ...product };
  }

  updateProduct() {
    this.productService.updateProduct(this.newProduct).subscribe({
      next: () => {
        this.loadProducts();
        this.newProduct = { id: 0, name: '', price: 0, stock: 0 };
      },
      error: (err) => console.error('Error updating product', err)
    });
  }
}


// import { Component } from '@angular/core';
// import { HttpClient, HttpClientModule } from '@angular/common/http';
// import { CommonModule } from '@angular/common';
// import { FormsModule } from '@angular/forms';

// import { MatTableModule } from '@angular/material/table';
// import { MatButtonModule } from '@angular/material/button';
// import { MatIconModule } from '@angular/material/icon';
// import { MatFormFieldModule } from '@angular/material/form-field';
// import { MatInputModule } from '@angular/material/input';

// import { Product } from '../models/product';

// @Component({
//   selector: 'app-product',
//   standalone: true,
//   imports: [
//     CommonModule,
//     FormsModule,
//     HttpClientModule,
//     MatTableModule,
//     MatButtonModule,
//     MatIconModule,
//     MatFormFieldModule,
//     MatInputModule
//   ],
//   templateUrl: './product-component.html',
//   styleUrl: './product-component.css',
// })
// export class ProductComponent {
//   displayedColumns: string[] = ['id', 'name', 'price', 'actions'];
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

