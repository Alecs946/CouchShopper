import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Observable, Subscriber } from 'rxjs';
import { PhotoAddRequest } from '../../_models/_product/request/product-request';

@Component({
  selector: 'app-file-upload',
  templateUrl: './file-upload.component.html',
  styleUrls: ['./file-upload.component.css'],
})
export class FileUploadComponent {
  @Input() multipleUpload = false;
  @Input() autoUpload = false;
  @Input() uploadButtonText = "Choose File";
  @Input() showFilePreview = false;
  @Output() filesUploaded: EventEmitter<File[]> = new EventEmitter<File[]>();
  @Output() filesChanged: EventEmitter<File[]> = new EventEmitter<File[]>();
  fileToUpload: File[] = [];
  showProgressBar = false;
  uploadProgress = 0;
  @Input() acceptedFormats: string[] = [];
  filePreviews: { [key: string]: string | ArrayBuffer } = {};
  @Input() defaultPictureEnabled = false;
  defaultPictureIndex: number | null = null;

  @Input() externalUpload = false;

  onFileChange(event: any): void {

    const selectedFiles: File[] = Array.from(event.target.files);

    if (!this.multipleUpload) {
      this.fileToUpload = selectedFiles.slice(0, 1);
    } else {
      this.fileToUpload = this.fileToUpload.concat(selectedFiles);
    }

    this.fileToUpload = this.fileToUpload.filter((file) => this.isFileFormatAccepted(file));

    if (this.autoUpload) {
      this.uploadFiles();
    } else {
      this.showPreview();
    }
    this.filesChanged.emit(this.fileToUpload)
  }

  isFileFormatAccepted(file: File): boolean {
    if (this.acceptedFormats.length === 0) {
      return true;
    }

    const fileExtension = file.name.split('.').pop()?.toLowerCase();
    return !!fileExtension && this.acceptedFormats.includes(fileExtension);
  }

  uploadFiles(): void {
    if (this.fileToUpload.length === 0) {
      return;
    }
    this.showProgressBar = true;
    const uploadInterval = setInterval(() => {
      this.uploadProgress += 10;
      if (this.uploadProgress >= 100) {
        clearInterval(uploadInterval);
        this.showProgressBar = false;
        this.uploadProgress = 0;
        this.filesUploaded.emit(this.fileToUpload);
        this.fileToUpload = [];
        this.filePreviews = {};
      }
    }, 200);
  }

  showPreview(): void {
    this.filePreviews = {};
    for (const file of this.fileToUpload) {
      if (this.isImageFile(file)) {
        const reader = new FileReader();
        reader.onload = (e: any) => {
          this.filePreviews[file.name] = e.target.result;
        };
        reader.readAsDataURL(file);
      }
    }
  }

  isImageFile(file: File): boolean {
    return file.type.startsWith('image');
  }

  get acceptFormatsString(): string {
    return this.acceptedFormats.map(format => '.' + format).join(',');
  }

  uploadFilesExternally(): void {
    if (this.fileToUpload.length === 0) {
      return;
    }

    this.uploadFiles();
  }

  removeFile(file: File): void {
    const index = this.fileToUpload.indexOf(file);
    if (index !== -1) {
      this.fileToUpload.splice(index, 1);
    }
    delete this.filePreviews[file.name];
    this.defaultPictureIndex = null;
  }

  setAsDefaultPicture(file: File): void {
    const index = this.fileToUpload?.indexOf(file);

    if (index !== undefined && index !== null) {
      if (this.defaultPictureIndex === index) {
        this.defaultPictureIndex = null;
      } else {
        this.defaultPictureIndex = index;
        this.filesChanged.emit(this.fileToUpload)
      }
    }
  }

  isDefaultPicture(file: File): boolean {
    const index = this.fileToUpload?.indexOf(file);
    return index === this.defaultPictureIndex;
  }

  getFilesWithDefault(): PhotoAddRequest[] {
    var filesWithDefault: PhotoAddRequest[] = [];
    this.fileToUpload.forEach(file => {
      this.convertToBase64(file).subscribe((f) => {
        filesWithDefault.push({
          content: f.split(",")[1],
          isDefault: this.isDefaultPicture(file)
        });
      })
    });
    return filesWithDefault;
  }

  convertToBase64(file: File) {
    const filesWithDefault: { file: string, isDefault: boolean }[] = [];
    const observable = new Observable((subscriber: Subscriber<any>) => {
      this.readFile(file, subscriber);
    });
    return observable;
  }

  readFile(file: File, subscriber: Subscriber<any>) {
    const filereader = new FileReader();
    filereader.readAsDataURL(file);

    filereader.onload = () => {
      subscriber.next(filereader.result);
      subscriber.complete();
    };
    filereader.onerror = (error) => {
      subscriber.error(error);
      subscriber.complete();
    };
  };





}
