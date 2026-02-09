import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { OrderService } from '../../services/order.service';
import { Order } from '../../models/order';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatTableModule } from '@angular/material/table';
import { MatDividerModule } from '@angular/material/divider';

import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';


@Component({
  selector: 'app-order-details',
  templateUrl: './order-details.html',
  imports: [CommonModule,  MatCardModule,
  MatTableModule,
  MatDividerModule,
  MatButtonModule,
  MatIconModule,
  MatFormFieldModule,
  MatInputModule
]
})
export class OrderDetails implements OnInit {
displayedColumns: string[] = ['productId', 'quantity', 'unitPrice'];
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

  order?: Order;

  constructor(
    private route: ActivatedRoute,
    private orderService: OrderService
  ) {}

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.loadOrder(id);
  }

  loadOrder(id: number): void {
    this.orderService.getOrderById(id).subscribe({
      next: (data) => this.order = data,
      error: (err) => console.error('Error loading order', err)
    });
  }
}