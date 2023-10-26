import { Component, EventEmitter, Input, Output, TemplateRef } from '@angular/core';

@Component({
  selector: 'app-modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.css']
})
export class ModalComponent {
  @Input() title!: string;
  @Input() contentTemplate!: TemplateRef<any>;
  @Input() hasCancelButton: boolean=true;
  @Input() hasConfirmButton: boolean=true;
  @Output() action: EventEmitter<any> = new EventEmitter<any>();

  onClose() {
    this.action.emit("close");
  }

  onConfirm() {
    this.action.emit("confirm");
  }
}
