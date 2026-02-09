import { Component, OnInit } from '@angular/core';
import { OrderService } from '../../services/order.service';
import { Order } from '../../models/order';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { FormsModule } from '@angular/forms';


@Component({
  selector: 'app-order-list',
  templateUrl: './order-list.html',
    imports: [CommonModule, 
      RouterModule,
      MatTableModule,
      FormsModule,
      MatButtonModule,
      MatIconModule,
      MatCardModule,
      MatFormFieldModule,
      MatInputModule,
      MatDatepickerModule,
      MatNativeDateModule
],
})
export class OrderList implements OnInit {
displayedColumns: string[] = ['id', 'status', 'totalAmount', 'createdAt', 'view'];
  orders: Order[] = [];
newOrder: Order = {
  id: 0,
  userId: '',
  createdAt: new Date().toISOString(),
  status: 'Pending',
  totalAmount: 0,
  items: [
   // { productId: 0, quantity: 1, unitPrice: 0 }
  ]
};

  constructor(private orderService: OrderService) {}

  ngOnInit(): void {
    this.loadOrders();
  }

  loadOrders(): void {
    this.orderService.getMyOrders().subscribe({
      next: (data) => this.orders = data,
      error: (err) => console.error('Error loading orders', err)
    });
  }

createOrder() {
  this.orderService.createOrder(this.newOrder).subscribe({
    next: () => {
      alert('Order created successfully');
      this.loadOrders();
    },
    error: err => console.error('Error creating order', err)
  });
}
 editOrder(order: Order) {
    this.newOrder = { ...order };
  }

  }
