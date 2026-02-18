export interface CreateOrderDto {
  userName: string;
  totalPrice: number;
  firstName: string;
  lastName: string;
  emailAddress: string;
  addressLine: string;
  country: string;
  state: string;
  zipCode: string;
  paymentMethod: number; // ← number !!!
  currency: string;
  cardName: string;
}

