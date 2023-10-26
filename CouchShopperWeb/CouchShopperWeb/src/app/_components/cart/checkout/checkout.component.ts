import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CartProduct } from '../../../_models/_cart/response/cart-response';
import { OrderAddRequest } from '../../../_models/_order/request/order-request';
import { ShoppingCartService } from '../../../_services/shopping-cart.service';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.css']
})
export class CheckoutComponent {
  title!: string;
  steps: string[] = [
    'Cart',
    'Details',
    'Shipping',
    'Payment',
    'Review',
  ];
  products: CartProduct[];
  subtotal: number;
  currentStep!: number;

  constructor(private shoppingCartService: ShoppingCartService, private router: Router, private toastr: ToastrService) {
  }

  ngOnInit(): void {
    this.currentStep = this.shoppingCartService.getCurentStep();
    this.title = this.steps[this.currentStep - 1];
    this.getCartItemsDetails();
  }
  getCartItemsDetails() {
    this.shoppingCartService.getCartItemsDetails().subscribe(
      {
        next: (response => {
          this.products = response.cartItems.map(x => {
            return {
              productId: x.productId,
              productName: x.productName,
              imageBase64: x.imageBase64,
              price: x.price,
              quantity: x.quantity,
              sellerId: x.sellerId,
              selectedOptions: x.selectedOptions.map(o => {
                return {
                  optionName: o.optionName,
                  optionValue: o.optionValue
                }
              }),
              numberOfAvailableProducts: x.numberOfAvailableProducts
            }
          })
          this.subtotal = response.subtotal
        }),
        error: (
          error => {
            console.log(error.error.message)
          })
      })
  }


  goToStep(step: number): void {
    if (step >= 1 && step <= 5) {
      this.currentStep = step;
      this.shoppingCartService.setCurrentStep(this.currentStep);
    }
  }

  goToNextStep(): void {
    if (this.currentStep < 5) {
      this.currentStep++;
      this.title = this.steps[this.currentStep - 1];
      this.shoppingCartService.setCurrentStep(this.currentStep);
    }
  }

  goToPreviousStep(): void {
    if (this.currentStep > 1) {
      this.currentStep--;
      this.title = this.steps[this.currentStep - 1];
      this.shoppingCartService.setCurrentStep(this.currentStep);
    }
  }

  quantityChanged(event) {
    this.shoppingCartService.updateCartItemQuantity(event.product, event.newQuantity)
    this.getCartItemsDetails();
  }

  productRemoved(event) {
    this.shoppingCartService.removeFromCart(event)
    this.getCartItemsDetails();
  }

  submitForm(event) {
    const checkoutDetails = this.shoppingCartService.getCheckoutDetails();
    const paymentDetails = this.shoppingCartService.getPaymentDetails();
    const shippingDetails = this.shoppingCartService.getShippingDetails();
    console.log(this.products)
    const order: OrderAddRequest = {
      orderItems: this.products,
      buyerFullName: checkoutDetails.fullName,
      buyerUsername: '',
      shippingAddress: checkoutDetails.address,
      buyerPhone: checkoutDetails.phoneNumber,
      paymentMethodId: paymentDetails.id,
      price: this.subtotal,
      shippingType: shippingDetails.shippingMethodName,
      shippingPrice: shippingDetails.shippingPrice
    }
    this.shoppingCartService.completeOrder(order).subscribe({
      next: (response => {
        this.shoppingCartService.clearCart()
        this.router.navigate(["/"])
        this.toastr.success("Order succesfuly created")
      }),
      error: (
        error => {
          console.log(error.error.message)
        })
    })
  }
}
