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
  noOfBadReviews: any;
  badReviewDeductionAmount: any;
  noOfCollisions: any;
  collisionDeductionAmount: any;
  badReviewDeduction: any;
  collisionDeduction: any;
  totalBonusAmount: any;
  selectedDate: any = new Date();
  submitted: boolean;
  isValueMax: boolean;
  isValueObj = { isValueMax: false, index: null };
  isMinValueObj = { isMinValue: false, index: null };
  noOfWashes: any;
  isEdit: boolean;
  bonusId: number;
  constructor(
    private confirmationService: ConfirmationUXBDialogService,
    private bonusSetupService: BonusSetupService,
    private datePipe: DatePipe
  ) { }

  ngOnInit(): void {
    this.bonusId = 0;
    this.submitted = false;
    this.isValueMax = false;
    this.locationId = localStorage.getItem('empLocationId');
    this.isEdit = false;
    // this.noOfBadReviews = 0;
    // this.badReviewDeductionAmount = 0;
    // this.noOfCollisions = 0;
    // this.collisionDeductionAmount = 0;
    this.badReviewDeduction = 0;
    this.collisionDeduction = 0;
    this.totalBonusAmount = 0;
    // this.getBonusFirstList();
  }

  onLocationChange(event) {
    this.locationId = +event;
  }

  getBonusFirstList() {
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
      if (item.Min === '' || item.Max === '' || item.BonusAmount === '') {
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
      if (+this.monthBonusList[i].Min > +this.monthBonusList[i].Max || +this.monthBonusList[i].Min === +this.monthBonusList[i].Max) {
        this.isValueObj = { isValueMax: true, index: i };
        return;
      } else {
        this.isValueObj = { isValueMax: false, index: i };
      }
    }
    this.monthBonusList.push({
      BonusRangeId: 0,
      BonusId: 0,
      Min: '',
      Max: '',
      noOfWashes: '',
      BonusAmount: '',
      Total: '',
      IsActive: true,
      IsDeleted: false
    });
  }

  validateMaxValue(bonus, ind) {
    this.submitted = false;
    this.isValueMax = false;
    this.isValueObj = null;
    if (+bonus.Min > +bonus.Max || +bonus.Min === +bonus.Max) {
      this.isValueObj = { isValueMax: true, index: ind };
    } else {
      this.isValueObj = { isValueMax: false, index: ind };
    }
  }

  validateMinValue(bonus, ind) {
    this.submitted = false;
    this.isValueMax = false;
    this.isMinValueObj = null;
    if (this.monthBonusList.length > 1) {
      if (+bonus.Min < +this.monthBonusList[ind - 1].Max ||
        +bonus.Min === +this.monthBonusList[ind - 1].Max) {
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
    this.collisionDeduction = +this.noOfCollisions * +this.collisionDeductionAmount;
  }

  totalBadReviewAmount() {
    this.badReviewDeduction = +this.noOfBadReviews * +this.badReviewDeductionAmount;
  }

  saveBonus() {
    console.log(this.monthBonusList, this.selectedDate, 'multi');
    this.submitted = false;
    this.isValueMax = false;
    let checkValue = true;
    this.monthBonusList.forEach(item => {
      if (item.Min === '' || item.Max === '' || item.BonusAmount === '') {
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
      if (+this.monthBonusList[i].Min > +this.monthBonusList[i].Max || +this.monthBonusList[i].Min === +this.monthBonusList[i].Max) {
        this.isValueObj = { isValueMax: true, index: i };
        return;
      } else {
        this.isValueObj = { isValueMax: false, index: i };
      }
    }
    const bonus = {
      bonusId: this.bonusId,
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
    console.log(finalObj, 'finalObj');
    if (this.isEdit === false) {
      this.bonusSetupService.saveBonus(finalObj).subscribe(res => {
        console.log(res, 'save');
        this.getBonusList();
      });
    } else {
      this.bonusSetupService.editBonus(finalObj).subscribe( res => {
        if (res.status === 'Success') {

        }
      });
    }
  }

  getBonusList() {
    const finalObj = {
      bonusMonth: this.datePipe.transform(this.selectedDate, 'MM'),
      bonusYear: this.datePipe.transform(this.selectedDate, 'yyyy'),
      locationId: this.locationId
    };
    this.bonusSetupService.getBonusList(finalObj).subscribe(res => {
      if (res.status === 'Success') {
        const bonus = JSON.parse(res.resultData);
        console.log(bonus, 'bonusList');
        if (bonus.BonusDetails.Bonus !== null) {
          this.bonusId = bonus.BonusDetails.Bonus.BonusId;
          this.noOfBadReviews = bonus.BonusDetails.Bonus.NoOfBadReviews;
          this.noOfCollisions = bonus.BonusDetails.Bonus.NoOfCollisions;
          this.badReviewDeductionAmount = bonus.BonusDetails.Bonus.BadReviewDeductionAmount;
          this.collisionDeductionAmount = bonus.BonusDetails.Bonus.CollisionDeductionAmount;
          this.collisionDeduction = this.noOfCollisions * this.collisionDeductionAmount;
          this.badReviewDeduction = this.noOfBadReviews * this.badReviewDeductionAmount;
        } else {
          this.noOfBadReviews = '';
          this.noOfCollisions = '';
          this.badReviewDeductionAmount = '';
          this.collisionDeductionAmount = '';
          this.collisionDeduction = 0;
          this.badReviewDeduction = 0;
        }
        if (bonus.BonusDetails.BonusRange !== null) {
          this.isEdit = true;
          this.monthBonusList = bonus.BonusDetails.BonusRange;
        } else {
          this.isEdit = false;
          this.monthBonusList = [
            {
              BonusRangeId: 0,
              BonusId: 0,
              Min: '',
              Max: '',
              noOfWashes: '',
              BonusAmount: '',
              Total: '',
              IsActive: true,
              IsDeleted: false
            }
          ];
        }
        if (bonus.BonusDetails.LocationBasedWashCount !== null) {
          this.noOfWashes = bonus.BonusDetails.LocationBasedWashCount.WashCount;
          this.noOfWashes = 1;
          let totalAmount = 0;
          for(let i = 0 ; i < this.monthBonusList.length ; i++) {
            if (this.monthBonusList[i].Min >= this.noOfWashes <= this.monthBonusList[i].Max) {
              this.monthBonusList[i].noOfWashes = this.noOfWashes;
              this.monthBonusList[i].Total = this.monthBonusList[i].BonusAmount;
              totalAmount = this.monthBonusList[i].BonusAmount;
              return;
            }
          }
        } else {
          this.totalBonusAmount = 0;
        }
      }
    });
  }

}
