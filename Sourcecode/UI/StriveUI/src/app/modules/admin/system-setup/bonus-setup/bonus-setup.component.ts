import { Component, OnInit } from '@angular/core';
import { ConfirmationUXBDialogService } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.service';
import { BonusSetupService } from 'src/app/shared/services/data-service/bonus-setup.service';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-bonus-setup',
  templateUrl: './bonus-setup.component.html',
  styleUrls: ['./bonus-setup.component.css']
})
export class BonusSetupComponent implements OnInit {
  locationId: any;
  monthBonusList: any = [];
  noOfBadReviews: number;
  badReviewDeductionAmount: number;
  noOfCollisions: number;
  collisionDeductionAmount: number;
  badReviewDeduction: any;
  collisionDeduction: any;
  totalBonusAmount: any;
  selectedDate: any;
  submitted: boolean;
  isValueMax: boolean;
  isValueObj = { isValueMax: false, index: null };
  isMinValueObj = { isMinValue: false, index: null };
  constructor(
    private confirmationService: ConfirmationUXBDialogService,
    private bonusSetupService: BonusSetupService,
    private datePipe: DatePipe
  ) { }

  ngOnInit(): void {
    this.submitted = false;
    this.isValueMax = false;
    this.locationId = localStorage.getItem('empLocationId');
    this.noOfBadReviews = 0;
    this.badReviewDeductionAmount = 0;
    this.noOfCollisions = 0;
    this.collisionDeductionAmount = 0;
    this.badReviewDeduction = 0;
    this.collisionDeduction = 0;
    this.totalBonusAmount = 0;
    this.getBonusList();
  }

  onLocationChange(event) {
    this.locationId = +event;
  }

  getBonusList() {
    this.monthBonusList = [
      {
        bonusRangeId: 0,
        bonusId: 0,
        min: '',
        max: '',
        noOfWashes: '',
        bonusAmount: '',
        total: '',
        isActive: true,
        isDeleted: false
      }
    ];
  }

  addBonus() {
    this.isValueMax = false;
    this.submitted = false;
    let checkValue = true;
    this.monthBonusList.forEach(item => {
      if (item.min === '' || item.max === '' || item.bonusAmount === '') {
        checkValue = true;
        return true;
      } else {
        checkValue = false;
      }
    });
    if (checkValue) {
      this.isValueMax = true;
      return;
    }
    for (let i = 0; i < this.monthBonusList.length; i++) {
      if (+this.monthBonusList[i].min > +this.monthBonusList[i].max || +this.monthBonusList[i].min === +this.monthBonusList[i].max) {
        this.isValueObj = { isValueMax: true, index: i };
        return;
      } else {
        this.isValueObj = { isValueMax: false, index: i };
      }
    }
    this.monthBonusList.push({
      bonusRangeId: 0,
      bonusId: 0,
      min: '',
      max: '',
      noOfWashes: '',
      bonusAmount: '',
      total: '',
      isActive: true,
      isDeleted: false
    });
  }

  validateMaxValue(bonus, ind) {
    this.isValueObj = null;
    if (+bonus.min > +bonus.max || +bonus.min === +bonus.max) {
      this.isValueObj = { isValueMax: true, index: ind };
    } else {
      this.isValueObj = { isValueMax: false, index: ind };
    }
  }

  validateMinValue(bonus, ind) {
    this.isMinValueObj = null;
    if (this.monthBonusList.length > 1) {
      if (+bonus.min < +this.monthBonusList[ind - 1].max ||
        +bonus.min === +this.monthBonusList[ind - 1].max) {
        this.isMinValueObj = { isMinValue: true, index: ind };
      } else {
        this.isMinValueObj = { isMinValue: false, index: ind };
      }
    }
  }

  deleteBonusRange(bonus) {
    this.confirmationService.confirm('Delete', `Are you sure you want to delete this row?`, 'Confirm', 'Cancel')
      .then((confirmed) => {
        if (confirmed === true) {
          this.confirmDelete(bonus);
        }
      })
      .catch(() => { });
  }

  confirmDelete(bonus) {

  }

  totalCollisionAmount() {
    this.collisionDeduction = this.noOfCollisions * this.collisionDeductionAmount;
  }

  totalBadReviewAmount() {
    this.badReviewDeduction = this.noOfBadReviews * this.badReviewDeductionAmount;
  }

  saveBonus() {
    console.log(this.monthBonusList, this.selectedDate, 'multi');
    this.submitted = false;
    this.isValueMax = false;
    let checkValue = true;
    this.monthBonusList.forEach(item => {
      if (item.min === '' || item.max === '' || item.bonusAmount === '') {
        checkValue = true;
        return true;
      } else {
        checkValue = false;
      }
    });
    if (checkValue) {
      this.submitted = true;
      return;
    }
    for (let i = 0; i < this.monthBonusList.length; i++) {
      if (+this.monthBonusList[i].min > +this.monthBonusList[i].max || +this.monthBonusList[i].min === +this.monthBonusList[i].max) {
        this.isValueObj = { isValueMax: true, index: i };
        return;
      } else {
        this.isValueObj = { isValueMax: false, index: i };
      }
    }
    const bonus = {
      bonusId: 0,
      locationId: this.locationId,
      bonusStatus: 1,
      bonusMonth: this.datePipe.transform(this.selectedDate, 'MM'),
      bonusYear: this.datePipe.transform(this.selectedDate, 'yyyy'),
      noOfBadReviews: this.noOfBadReviews,
      badReviewDeductionAmount: this.badReviewDeductionAmount,
      noOfCollisions: this.noOfCollisions,
      collisionDeductionAmount: this.collisionDeductionAmount,
      totalBonusAmount: this.totalBonusAmount,
      isActive: true,
      isDeleted: false,
      createdBy: 0,
      createdDate: new Date(),
      updatedBy: 0,
      updatedDate: new Date()
    };
    const finalObj = {
      bonus,
      bonusRange: this.monthBonusList
    };
    this.bonusSetupService.saveBonus(finalObj).subscribe(res => {
      console.log(finalObj, 'final');
    });
  }

}