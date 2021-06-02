import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-confirmation-dialog',
  templateUrl: './confirmation-dialog.component.html'
})
export class ConfirmationDialogComponent implements OnInit {
  @Input() title: string;
  @Input() message: string;
  @Input() btnOkText: string;
  @Input() btnNoText: string;
  @Input() btnCancelText: string;
  @Input() cancelBtn = false;

  constructor(private activeModal: NgbActiveModal) { }

  ngOnInit() {
  }
  public accept() {
    this.activeModal.close(true);
  }

  public dismiss() {
    this.activeModal.dismiss();
  }
  public cancel() {
     this.activeModal.dismiss(false);
  }


}

