export interface CardPaymentRequest {
  username: string;
  cardName: number;
  cardNumber: string;
  nameOnCard: string;
  expiryDate: string;
  isPrimary: boolean;
}
export interface PaymentDeleteRequest {
  username: string;
  cardId: string;
}
export interface PaypalPaymentMethod {
  username: string
  email: string
  password: string
  isPrimary: boolean
}
