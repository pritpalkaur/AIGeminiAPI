import { Routes } from '@angular/router';
import { ProductComponent } from './product-component/product-component';
import { OrderList } from './components/order-list/order-list';
import { OrderDetails } from './components/order-details/order-details';
import { SecureTest } from './secure-test/secure-test';
import { Login } from './login/login';
import { authGuard } from './auth-guard';
import { LayoutComponent } from './layout/layout';


export const routes: Routes = [

  { path: 'login', component: Login },

  {
    path: '',
    component: LayoutComponent,
    canActivate: [authGuard],
    children: [
      { path: 'product', component: ProductComponent },
      { path: 'orders', component: OrderList },
      { path: 'orders/:id', component: OrderDetails },
      { path: 'secure-test', component: SecureTest },
      { path: '', redirectTo: 'product', pathMatch: 'full' }
    ]
  },

  { path: '**', redirectTo: 'login' }
];

// export const routes: Routes = [
//   { path: 'login', component: Login },
//   { path: 'product', component: ProductComponent, canActivate: [authGuard]},
//   { path: 'secure-test', component: SecureTest, canActivate: [authGuard] },
//   // ✔ Correct default route
//   { path: '', redirectTo: 'login', pathMatch: 'full' },
//   // ✔ Optional: catch-all for unknown routes
//   { path: '**', redirectTo: 'login' }
// ];


