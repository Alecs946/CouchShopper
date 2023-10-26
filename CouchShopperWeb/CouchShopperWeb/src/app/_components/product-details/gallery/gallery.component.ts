import { Component, ElementRef, HostListener, Input, OnInit, ViewChild } from '@angular/core';
import { GalleryImage } from '../../../_models/_product/response/product-response';

@Component({
  selector: 'app-gallery',
  templateUrl: './gallery.component.html',
  styleUrls: ['./gallery.component.css']
})
export class GalleryComponent implements OnInit {
  @Input() images!: GalleryImage[];

  previewImage!: GalleryImage;
  selectedImage!: GalleryImage;
  lightboxOpen = false;

  ngOnInit(): void {
    this.previewImage = this.images?.find(element => element.defaultImage) ?? this.images[0];
  }
  @HostListener('window:keydown', ['$event'])
  handleKeyboardEvent(event: KeyboardEvent): void {
    if (event.key === 'ArrowLeft' && !this.isFirstImage()) {
      this.previousImage();
    } else if (event.key === 'ArrowRight' && !this.isLastImage()) {
      this.nextImage();
    } else if (event.key === 'Escape') {
      this.closeLightbox();
    }
  }
  @HostListener('wheel', ['$event'])
  handleMouseScroll(event: WheelEvent): void {
    if (this.lightboxOpen) {
      event.preventDefault();
      if (event.deltaY < 0 && !this.isFirstImage()) {
        this.previousImage();
      } else if (event.deltaY > 0 && !this.isLastImage()) {
        this.nextImage();
      }
    }
  }
  openLightbox(image: GalleryImage): void {
    this.selectedImage = image;
    this.lightboxOpen = true;
  }

  closeLightbox(): void {
    this.lightboxOpen = false;
  }

  previousImage(): void {
    const currentIndex = this.images.indexOf(this.selectedImage);
    if (currentIndex > 0) {
      this.selectedImage = this.images[currentIndex - 1];
      this.previewImage = this.images[currentIndex - 1];
    }
  }

  nextImage(): void {
    const currentIndex = this.images.indexOf(this.selectedImage);
    if (currentIndex < this.images.length - 1) {
      this.selectedImage = this.images[currentIndex + 1];
      this.previewImage = this.images[currentIndex + 1];
    }
  }

  isFirstImage(): boolean {
    return this.images.indexOf(this.selectedImage) === 0;
  }

  isLastImage(): boolean {
    return this.images.indexOf(this.selectedImage) === this.images.length - 1;
  }

  openPreview(image: GalleryImage) {
    this.previewImage = image;
    this.lightboxOpen = false;
  }

  closePreview(event: Event) {
    const target = event.target as HTMLElement;
    const isImage = target.tagName === 'IMG';
    const isButton = target.tagName === 'I';
    const isPrevIcon = target.classList.contains('prev-icon');
    const isNextIcon = target.classList.contains('next-icon');
    const isCloseIcon = target.classList.contains('close-icon');

    if (!isImage && !isButton && !isPrevIcon && !isNextIcon && !isCloseIcon) {
      this.lightboxOpen = false;
    }
  }

}
