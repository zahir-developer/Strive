import { Component, OnInit, Input } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { GiftCardService } from 'src/app/shared/services/data-service/gift-card.service';
import * as moment from 'moment';

@Component({
  selector: 'app-add-activity',
  templateUrl: './add-activity.component.html',
  styleUrls: ['./add-activity.component.css']
})
export class AddActivityComponent implements OnInit {
  giftCardForm: FormGroup;
  @Input() giftCardId?: any;
  constructor(private activeModal: NgbActiveModal, private fb: FormBuilder, private giftCardService: GiftCardService) { }

  ngOnInit(): void {
    this.giftCardForm = this.fb.group({
      amount: ['']
    });
  }

  closeModal() {
    this.activeModal.close();
  }

  addActivity() {
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
        this.activeModal.close(true);
      }
    });
  }

}
