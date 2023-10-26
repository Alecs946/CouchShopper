import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Dropdown } from '../../../_models/_common/response/home-content-response';
import { PhotoAddRequest, ProductAddRequest, ProductUpdateRequest } from '../../../_models/_product/request/product-request';
import { ProductService } from '../../../_services/product.service';
import { FileUploadComponent } from '../../file-upload/file-upload.component';

@Component({
  selector: 'app-settings-new-product',
  templateUrl: './settings-new-product.component.html',
  styleUrls: ['./settings-new-product.component.css']
})
export class SettingsNewProductComponent implements OnInit {
  @Input() productId!: string
  productForm: FormGroup;
  productFormSubmitted = false;
  @ViewChild('fileUploadComponent', { static: false }) fileUploadComponent!: FileUploadComponent;
  files: PhotoAddRequest[] = [];
  categories$!: Observable<Dropdown[]>
  constructor(public fb: FormBuilder, public productService: ProductService, private router: Router) {
    this.getCategories();
    this.productForm = this.fb.group({
      brand: new FormControl('', Validators.required),
      model: new FormControl(''),
      name: new FormControl('', Validators.required),
      price: new FormControl(null, Validators.required),
      onSale: new FormControl(false),
      featureProduct: new FormControl(false),
      salePrice: new FormControl(null, [this.requiredValues('onSale')]),
      quantity: new FormControl(null, Validators.required),
      category: new FormControl(null, Validators.required),
      productDescription: new FormControl('', Validators.required),
      options: this.fb.array([], [Validators.required, this.requiredAtLeastOne(), this.atLeastOneOptionValidator()]),
    });
  }
  ngOnInit(): void {
    if (this.productId) {
      this.productService.getProduct(this.productId).subscribe({
        next: (response => {
          this.productForm.get("brand").setValue(response.brand);
          this.productForm.get("model").setValue(response.model);
          this.productForm.get("name").setValue(response.name);
          this.productForm.get("price").setValue(response.price);
          this.productForm.get("onSale").setValue(!!response.salePrice);
          this.productForm.get("salePrice").setValue(response.salePrice);
          this.productForm.get("quantity").setValue(response.quantity);
          this.productForm.get("productDescription").setValue(response.productDescription);
          this.productForm.get("featureProduct").setValue(response.featureProduct);
          this.productForm.get("category").setValue(response.category);
          for (var i = 0; i < response.photos.length; i++) {
            const item = response.photos[i];
            this.fileUploadComponent.fileToUpload.push(this.blobToFile(this.base64ToByteArray(item.content), item.id))
            if (item.isDefault) {
              this.fileUploadComponent.defaultPictureIndex=i;
            }
          }
          this.fileUploadComponent.showPreview()
          this.fileUploadComponent.filesChanged.emit()
          response.options.forEach((item) => {
            const array = this.fb.array([])
            item.productOptionValues.forEach((x) => {
              array.push(this.fb.control(x, Validators.required))
            })
            this.options.push(
              this.fb.group({
                option: this.fb.group({
                  productOptionName: this.fb.control(item.productOptionName, Validators.required),
                  productOptionValues: array
                })
              })
            )

          })
        }),
        error: (
          error => {
            console.log(error.error.message);
          })
      })
    }
  }

  get options() {
    return this.productForm.get('options') as FormArray;
  }

  addItems() {
    const newFormGroup = this.fb.group({
      option: this.fb.group({
        productOptionName: this.fb.control('', [Validators.required]),
        productOptionValues: this.fb.array([], Validators.required)
      })
    });
    this.options.push(newFormGroup);
  }


  deleteItem(i: number) {
    this.options.removeAt(i);
  }

  get productOptionValues() {
    return this.options.get('productOptionValues') as FormArray;
  }

  getControls(i: number) {
    return (this.options.at(i) as FormGroup).controls;
  }

  getProductOptionValues(i: number) {
    return (this.options.at(i).get('option.productOptionValues') as FormArray);
  }

  getProductOptionFormControl(itemIndex: number, valueIndex: number): FormControl {
    const productOptionValuesArray = this.getProductOptionValues(itemIndex);
    return productOptionValuesArray.at(valueIndex) as FormControl;
  }


  addOption(i: number) {
    this.getProductOptionValues(i).push(this.fb.control('', Validators.required));
  }

  deleteOption(itemIndex: number, optionIndex: number) {
    const productOptionValuesArray = this.getProductOptionValues(itemIndex);
    productOptionValuesArray.removeAt(optionIndex);
  }

  onSubmit() {

    if (this.productForm.valid) {
      this.productFormSubmitted=false
      if (this.productId) {
        const product: ProductUpdateRequest = {
          userId: '',
          id:'',
          name: this.productForm.value.name,
          brand: this.productForm.value.brand,
          model: this.productForm.value.model,
          price: this.productForm.value.price,
          salePrice: this.productForm.value.salePrice,
          quantity: this.productForm.value.quantity,
          options: this.productForm.value.options,
          category:this.productForm.value.category,
          productDescription: this.productForm.value.productDescription,
          featureProduct: this.productForm.value.featureProduct,
          photos: this.files.map((photo: any) => ({
            content: photo.content,
            isDefault: photo.isDefault
          }))
        };
        this.productService.updateProduct(product, this.productId).subscribe(
            {
              next: (response => {
                this.productForm.reset()
                this.router.navigate(['/products']);
              }),
              error: (
                error => {
                  console.log(error.error.message);
                })
            }
          );
      }
      else {
        const product: ProductAddRequest = {
          userId: '',
          name: this.productForm.value.name,
          brand: this.productForm.value.brand,
          model: this.productForm.value.model,
          price: this.productForm.value.price,
          salePrice: this.productForm.value.salePrice,
          quantity: this.productForm.value.quantity,
          options: this.productForm.value.options,
          category: this.productForm.value.category,
          productDescription: this.productForm.value.productDescription,
          featureProduct: this.productForm.value.featureProduct,
          photos: this.files.map((photo: any) => ({
            content: photo.content,
            isDefault: photo.isDefault
          }))
        };

        this.productService.addProduct(product)
          .subscribe(
            {
              next: (response => {
                this.productForm.reset()
                this.router.navigate(['/products']);
              }),
              error: (
                error => {
                  console.log(error.error.message);
                })
            }
          );
      }
    }
    else {
      this.productFormSubmitted=true
    }
  }

  fileOnChange(files: File[]) {
    this.files = this.fileUploadComponent.getFilesWithDefault()
  }

  base64ToByteArray(base64String: string) {
    const byteArray = new Uint8Array(atob(base64String).split('').map((char) => char.charCodeAt(0)));
    return new Blob([byteArray], { type: "image/jpeg" })
  }

  blobToFile(blob, fileName) {
    return new File([blob], fileName, { type: blob.type });
  }

  requiredValues(setValue: string): ValidatorFn {
    return (control: AbstractControl) => {
      if (control.parent?.get(setValue)?.value && !!control.parent?.get(setValue)?.value) {
        if (!control.value) {
          console.log(control.parent?.get(setValue)?.value)
          return { notSet: true }
        }
        return null
      }
        return null
    }
  }

  requiredAtLeastOne(): ValidatorFn {
    return (control: AbstractControl) => {
      if (control?.value.length == 0) {
        return { noValidOptions: true }
      }
      return null 
    }
  }

  atLeastOneOptionValidator() {
    return (formArray: FormArray) => {
      const isValid = formArray.controls.every(control => {
        const optionName = control.get('option.productOptionName').value;
        const values = control.get('option.productOptionValues').value;

        return (
          optionName && optionName.trim() !== '' &&
          values && values.length > 0 && values.every(value => value.trim() !== '')
        );
      });

      return isValid ? null : { atLeastOneOption: true };
    };
  }
  getCategories() {
    this.categories$ = this.productService.getCategories()
  }
}
