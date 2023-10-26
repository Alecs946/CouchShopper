import { Injectable } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ModalComponent } from '../_components/modal/modal.component';

@Injectable({
  providedIn: 'root'
})
export class ModalService {
  private modalRef!: BsModalRef;

  constructor(private modalService: BsModalService) { }

  public openModal(title: string, contentTemplate: any, hasCancelButton: boolean, hasConfirmButton: boolean,
    handleConfirm: () => void, handleClose: () => void) {
    const initialState = {
      title,
      contentTemplate,
      hasCancelButton,
      hasConfirmButton
    };
    this.modalRef = this.modalService.show(ModalComponent, { initialState });
    this.modalRef.content.action.subscribe((result: string) => {
      if (result === "confirm") {
        handleConfirm();
      } else if (result === "close") {
        handleClose();
      }
    });
  }
  public closeModal() {
    this.modalRef.hide();
  }
}
