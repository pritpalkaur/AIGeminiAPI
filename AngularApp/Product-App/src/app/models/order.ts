export interface Order {
  id: number;
  userId: string;
  createdAt: string;
  status: string;
  totalAmount: number;
  items: OrderItem[];
}
export interface OrderItem {
  id: number;
  orderId: number;
  productId: number;
  quantity: number;
  unitPrice: number;
}
