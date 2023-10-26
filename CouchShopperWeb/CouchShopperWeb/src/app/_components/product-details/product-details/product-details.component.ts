import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../../_services/product.service';
import { ActivatedRoute } from '@angular/router';
import { SellerInfo } from '../../../_models/Account/response/seller-response';
import { Shipping, ProductDescription, ProductDetails } from '../../../_models/_product/response/product-response';


@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css']
})
export class ProductDetailsComponent implements OnInit {
  //product!: OrderInformation;
  productDetails!: ProductDetails;
  productDescription!: ProductDescription;
  shippingOptions!: Shipping[];
  sellerInfo: SellerInfo
  

  //pictures!: GalleryImage[];
  id!: string
  constructor(private route: ActivatedRoute, private productService: ProductService) {
    
}

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.id = params['id'];
    });
    this.productService.getProduct(this.id).subscribe({
      next: (response => {
        this.productDetails = {
          productName: response.name,
          price: response.price,
          salePrice: response.salePrice,
          rating: response.rating,
          numberOfReviews: response.numberOfReviews,
          numberOfAvailableProducts: response.quantity,
          pictures: (response.photos && response.photos.length > 0) ? response.photos.map((x) => {
            return {
              defaultImage: x.isDefault,
              largeUrl: x.content ? 'data:image/png;base64,' + x.content : "./assets/product.png",
              thumbnailUrl: x.content ? 'data:image/png;base64,' + x.content : "./assets/product.png",
            }
          }) :
            [{
              defaultImage: true,
              largeUrl: "./assets/product.png",
              thumbnailUrl: "./assets/product.png",
            }],
          productOptions: response.options.map((m => {
            return {
              optionName: m.productOptionName,
              optionValues: m.productOptionValues.map((o) => {
                return {
                  name: o,
                  value: null,
                  iconName: ''
                }
              })
            }
          }))
        }
        this.productDescription = {
          brand: response.brand,
          model: response.model,
          description: response.productDescription
        }
        this.productService.getSellerInfo(response.userId).subscribe(
          {
            next: (response => {
              this.sellerInfo = {
                sellerName: response.sellerName,
                imageBase64: response.imageBase64,
                memberSince: response.memberSince,
              }
            }),
            error: (
              error => {
                console.log(error.error.message)
              })
          })
        this.productService.getShippingOptions().subscribe(
          {
            next: (response => {
              this.shippingOptions = response?.map((x => {
                return {
                  shippingMethodName: x.shippingMethodName,
                  estimatedTime: x.minNumberOfDays + '-' + x.maxNumberOfDays,
                  shippingPrice: x.shippingPrice
                }
              }))??null
            }),
            error: (
              error => {
                console.log(error.error.message)
              })
          })
      }),
      error: (
        error => {
          console.log(error.error.message)
        })
    })



  }
}
