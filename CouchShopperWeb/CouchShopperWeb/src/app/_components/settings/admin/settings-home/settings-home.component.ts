import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SpecialOffer, PublishUnpublishRequest } from '../../../../_models/_common/request/special-offer-request';
import { PhotoAddRequest } from '../../../../_models/_product/request/product-request';
import { CommonService } from '../../../../_services/common.service';
import { SettingsService } from '../../../../_services/settings.service';
import { FileUploadComponent } from '../../../file-upload/file-upload.component';

@Component({
  selector: 'app-settings-home',
  templateUrl: './settings-home.component.html',
  styleUrls: ['./settings-home.component.css']
})
export class SettingsHomeComponent implements OnInit {
  specialOffers: SpecialOffer[] = [];
  specialOfferForm !: FormGroup;
  showSpecialOfferForm: boolean = false;
  specialOfferFormSubmitted: boolean = false;
  @ViewChild('fileUploadComponent', { static: false }) fileUploadComponent!: FileUploadComponent;
  files: PhotoAddRequest[] = [];
  specialOfferCurrentPage = 1;
  specialOffersNumber = 0;
  itemsPerPage = 5;
  maxSizePagination = 5;

  constructor(private route: ActivatedRoute, private router: Router, private commonService: CommonService, private formBuilder: FormBuilder, private settings: SettingsService) {

    this.specialOfferForm = this.formBuilder.group({
      id: [],
      title: ['', Validators.required],
      description: ['', Validators.required],
      backgroundColor: ['#000000', Validators.required],
      textColor: ['#000000', Validators.required],
      imageBase64: ['']
    })
    this.getSpecialOffers()
  }

  ngOnInit(): void {

  }

  isSpecialOfferTab(): boolean {
    return this.route.snapshot.url[0].path === 'special-offer';
  }

  navigateToSpecialOfferTab(): void {
    this.router.navigate(['/special-offer']);
  }


  editSpecialOffer(id: string) {
    this.commonService.getSpecialOffer(id).subscribe(
      {
        next: (response: SpecialOffer) => {
          const editValue: SpecialOffer = response;
          this.specialOfferForm.patchValue(editValue);
          this.fileUploadComponent.fileToUpload.push(this.blobToFile(this.base64ToByteArray(response.imageBase64), response.photoId))
          this.fileUploadComponent.showPreview()
          this.fileUploadComponent.filesChanged.emit()
          this.showSpecialOfferForm = true;
        },
        error: (error) => {
          console.log(error.error.message);
        },
      }
    );
  }

  deleteSpecialOffer(id: string) {
    this.commonService.deleteSpecialOffer(id).subscribe(
      {
        next: (() => {
          this.getSpecialOffers()
        }),
        error: (
          error => {
            console.log(error.error.message)
          })
      }
    );
  }

  getSpecialOffers(): void {
    this.commonService.getSpecialOffers(this.specialOfferCurrentPage)
      .subscribe(
        {
          next: (response => {
            this.specialOffersNumber = response.totalEntities;
            this.specialOffers = response.specialOffers
          }),
          error: (
            error => {
              console.log(error.error.message)
            })
        }
      );
  }

  submitSpecialOffer() {
    console.log(this.specialOfferForm.valid)
    if (this.specialOfferForm.get("id").value && this.specialOfferForm.valid) {
      this.specialOfferFormSubmitted = false
      if (this.files) {
        this.specialOfferForm.get("imageBase64").setValue(this.files[0]?.content ?? '')
      }
      this.commonService.updateSpecialOffer(this.specialOfferForm.value).subscribe({
        next: (() => {
          this.showSpecialOfferForm = false;
          this.fileUploadComponent.uploadFiles()
          this.specialOfferForm.reset()
          this.getSpecialOffers()
        }),
        error: (
          error => {
            console.log(error.error.message)
          })
      })
    }
    else if (this.specialOfferForm.valid) {
      this.specialOfferFormSubmitted = false

      this.specialOfferForm.get("imageBase64").setValue(this.files[0]?.content ?? '')
      this.commonService.addSpecialOffer(this.specialOfferForm.value).subscribe({
        next: (() => {
          this.showSpecialOfferForm = false;
          this.fileUploadComponent.uploadFiles()
          this.specialOfferForm.reset()
          this.getSpecialOffers()
        }),
        error: (
          error => {
            console.log(error.error.message)
          })
      })
    }
    else {
      this.specialOfferFormSubmitted = true

    }
  }

  fileOnChange(files: File[]) {
    console.log(this.fileUploadComponent)
    this.files = this.fileUploadComponent.getFilesWithDefault()
  }

  publishSpecialOffers(offerId: string) {
    const request: PublishUnpublishRequest = { OfferId: offerId }
    this.commonService.publisUnpublishSpecialOffer(request).subscribe({
      next: (response => {
        this.getSpecialOffers()
      }),
      error: (
        error => {
          console.log(error.error.message)
        })
    })
  }

  base64ToByteArray(base64String: string) {
    const byteArray = new Uint8Array(atob(base64String).split('').map((char) => char.charCodeAt(0)));
    return new Blob([byteArray], { type: "image/jpeg" })
  }
  blobToFile(blob, fileName) {
    return new File([blob], fileName, { type: blob.type });
  }

  specialOfferPageChanged(event: any): void {
    this.specialOfferCurrentPage = event.page;
    this.getSpecialOffers();
  }


}
