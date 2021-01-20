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
  symbol: string;
  amountValidation: boolean;
  constructor(
    private activeModal: NgbActiveModal,
    private fb: FormBuilder,
    private giftCardService: GiftCardService,
    private toastr: ToastrService,
    private messageService: MessageServiceToastr
    ) { }

  ngOnInit(): void {
    this.symbol = 'plus';
    this.amountValidation = false;
    this.submitted = false;
    this.giftCardForm = this.fb.group({
      amount: ['', Validators.required],
      type: ['']
    });
  }

  closeModal() {
    this.activeModal.close();
  }

  get f() {
    return this.giftCardForm.controls;
  }

  settingType(event) {
    const type = event.target.value;
    if (type === 'plus') {
      this.symbol = 'plus';
    } else {
      this.symbol = 'minus';
    }
  }

  addActivity() {
    console.log(this.giftCardForm);
    this.submitted = true;
    this.amountValidation = false;
    if (this.giftCardForm.invalid) {
      this.messageService.showMessage({ severity: 'warning', title: 'Warning', body: 'Please Enter Mandatory fields' });
      return;
    }
    if (this.symbol === 'minus') {
      if (+this.totalAmount < Number(this.giftCardForm.value.amount)) {
        this.amountValidation = true;
        this.messageService.showMessage({ severity: 'warning', title: 'Warning', body: 'Insufficient Balance' });
        return;
      }
    }
    const activityObj = {
      giftCardHistoryId: 0,
      giftCardId: this.giftCardId,
      locationId: +localStorage.getItem('empLocationId'),
      transactionType: 1,
      transactionAmount: this.symbol === 'plus' ? this.giftCardForm.value.amount : -(this.giftCardForm.value.amount),
      transactionDate: moment(new Date()),
      comments: '',
      isActive: true,
      isDeleted: false,
      createdBy: +localStorage.getItem('empId'),
      createdDate: moment(new Date()),
      updatedBy: +localStorage.getItem('empId'),
      updatedDate: moment(new Date())
    };
    console.log(activityObj);
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

  updateBalance() {
    this.giftCardService.updateBalance(this.giftCardId).subscribe( res => {});
  }

}
