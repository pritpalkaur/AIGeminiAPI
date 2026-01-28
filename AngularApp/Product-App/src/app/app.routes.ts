import { Routes } from '@angular/router';
import { ProductComponent } from './product-component/product-component';

export const routes: Routes = [
  { path: '', component: ProductComponent },      // Home page
  { path: 'product', component: ProductComponent } // Optional extra route
];
