import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProductOption } from '../../../_models/_product/request/product-request';
import { ProductResponse } from '../../../_models/_product/response/product-response';
import { GalleryImage, ProductDescription, ProductPanel } from '../../../_models/_product/response/product-response';
import { SettingsService } from '../../../_services/settings.service';

@Component({
  selector: 'app-stttings-product-overview',
  templateUrl: './stttings-product-overview.component.html',
  styleUrls: ['./stttings-product-overview.component.css']
})
export class StttingsProductOverviewComponent implements OnInit {
  constructor(private route: ActivatedRoute, public settings: SettingsService) {
    this.route.params.subscribe(params => {
      this.id = params['id'];
      this.getProduct()
    })
  }
  product!: ProductResponse;
  photos!: GalleryImage[];
  productOptions!: ProductOption[]
  productPanel: ProductPanel
  productDescription: ProductDescription;
  id!: string
  ngOnInit(): void {
  }
  getProduct(): void {
    this.settings.getProductPreview(this.id)
      .subscribe(
        {
          next: (response => {
            this.product = response
            this.photos = response.photos.map((x) => {
              return {
                defaultImage: x.isDefault,
                largeUrl: x.content ? 'data:image/png;base64,' + x.content : "./assets/product.png",
                thumbnailUrl: x.content ? 'data:image/png;base64,' + x.content : "./assets/product.png",
              }
            })
            this.productOptions = response.options.map((m => {
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
            this.productDescription = {
              brand: response.brand,
              model: response.model,
              description: response.productDescription
            }
          }),
          error: (
            error => {
              console.log(error.error.message)
            })
        }
      );
  }

}
