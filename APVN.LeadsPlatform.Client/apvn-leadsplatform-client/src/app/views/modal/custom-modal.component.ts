import { Component, Output, EventEmitter, ViewChild } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';

@Component({
    selector: 'ng-modal-dialog',
    templateUrl: 'custom-modal.component.html'
})
export class CustomModalComponent {
    @ViewChild('primaryModal') public primaryModal: ModalDirective;
    @Output() onConfirm = new EventEmitter<object>();

    modalTitle = "Xác nhận";
    modalContent = "";
    modelActionType = 0;
    data: any;

    public showConfirm(modalTitle: string, modalContent: string, modelActionType: number, data: any) {
        this.modalTitle = modalTitle;
        this.modalContent = modalContent;
        this.modelActionType = modelActionType;
        this.data = data;
        this.primaryModal.show();
    }

    doConfirm(): void {
        this.onConfirm.emit(this.data);
        this.primaryModal.hide();
    }
    //   @ViewChild('myModal') public myModal: ModalDirective;
    //   @ViewChild('largeModal') public largeModal: ModalDirective;
    //   @ViewChild('smallModal') public smallModal: ModalDirective;
    //   @ViewChild('primaryModal') public primaryModal: ModalDirective;
    //   @ViewChild('successModal') public successModal: ModalDirective;
    //   @ViewChild('warningModal') public warningModal: ModalDirective;
    //   @ViewChild('dangerModal') public dangerModal: ModalDirective;
    //   @ViewChild('infoModal') public infoModal: ModalDirective;
}
