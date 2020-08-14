import { Component, OnInit, Input } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { GiftCardService } from 'src/app/shared/services/data-service/gift-card.service';
import * as moment from 'moment';
import { ToastrService } from 'ngx-toastr';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';

@Component({
  selector: 'app-add-activity',
  templateUrl: './add-activity.component.html',
  styleUrls: ['./add-activity.component.css']
})
export class AddActivityComponent implements OnInit {
  giftCardForm: FormGroup;
  @Input() giftCardNumber?: any;
  @Input() activeDate?: any;
  @Input() totalAmount?: any;
  @Input() giftCardId?: any;
  submitted: boolean;
  constructor(
    private activeModal: NgbActiveModal,
    private fb: FormBuilder,
    private giftCardService: GiftCardService,
    private toastr: ToastrService,
    private messageService: MessageServiceToastr
    ) { }

  ngOnInit(): void {
    this.submitted = false;
    this.giftCardForm = this.fb.group({
      amount: ['', Validators.required]
    });
  }

  closeModal() {
    this.activeModal.close();
  }

  get f() {
    return this.giftCardForm.controls;
  }

  addActivity() {
    this.submitted = true;
    if (this.giftCardForm.invalid) {
      this.messageService.showMessage({ severity: 'warning', title: 'Warning', body: 'Please Enter Mandatory fields' });
      return;
    }
    const activityObj = {
      giftCardHistoryId: 0,
      giftCardId: this.giftCardId,
      locationId: 1,
      transactionType: 1,
      transactionAmount: this.giftCardForm.value.amount,
      transactionDate: moment(new Date()),
      comments: 'string',
      isActive: true,
      isDeleted: false,
      createdBy: 0,
      createdDate: moment(new Date()),
      updatedBy: 0,
      updatedDate: moment(new Date())
    };
    const finalObj = {
      giftCardHistory: activityObj
    };
    this.giftCardService.addCardHistory(finalObj).subscribe( res => {
      if (res.status === 'Success') {
        this.messageService.showMessage({ severity: 'success', title: 'Success', body: 'Activity Added Successfully!!' });
        this.activeModal.close(true);
      } else {
        this.messageService.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
      }
    });
  }

}
