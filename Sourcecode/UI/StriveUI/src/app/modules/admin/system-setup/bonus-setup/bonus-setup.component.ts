import { Component, OnInit } from '@angular/core';
import { ConfirmationUXBDialogService } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.service';
import { BonusSetupService } from 'src/app/shared/services/data-service/bonus-setup.service';
import { DatePipe } from '@angular/common';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { MessageConfig } from 'src/app/shared/services/messageConfig';

@Component({
  selector: 'app-bonus-setup',
  templateUrl: './bonus-setup.component.html'
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
  selectedMonth: any = new Date().getMonth() + 1;
  selectedYear: any = new Date().getFullYear();
  submitted: boolean;
  isValueMax: boolean;
  isValueObj = { isValueMax: false, index: null };
  isMinValueObj = { isMinValue: false, index: null };
  noOfWashes: any;
  isEdit: boolean;
  bonusId: number;
  deletedID: any = [];
  negativecollisionDeduction: string;
  negativebadReviewDeduction: string;
  employeeId: number;
  isLoading: boolean;
  constructor(
    private confirmationService: ConfirmationUXBDialogService,
    private bonusSetupService: BonusSetupService,
    private datePipe: DatePipe,
     private toastr: ToastrService,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit(): void {
    this.isLoading = false;
    this.employeeId = +localStorage.getItem('empId');
    this.bonusId = 0;
    this.submitted = false;
    this.isValueMax = false;
    this.locationId = localStorage.getItem('empLocationId');
    this.isEdit = false;
    this.badReviewDeduction = 0;
    this.collisionDeduction = 0;
    this.totalBonusAmount = 0;
    this.getBonusList();
  }

  onLocationChange(event) {
    this.locationId = +event;
    this.getBonusList();
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
    }
    if (this.isMinValueObj?.isMinValue || this.isValueObj?.isValueMax) {
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

    // let washCount = '';
    // if (this.monthBonusList.length === 1) {
    //   washCount = '0';
    // }
    this.monthBonusList.push({
      BonusRangeId: 0,
      BonusId: this.bonusId,
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

  deleteBonusRange(bonus, ind) {
    this.confirmationService.confirm('Delete', `Are you sure you want to delete this row?`, 'Confirm', 'Cancel')
      .then((confirmed) => {
        if (confirmed === true) {
          this.confirmDelete(bonus, ind);

        }
      })
      .catch(() => { });
  }

  confirmDelete(bonus, ind) {
    if (bonus.BonusRangeId === 0) {
      this.monthBonusList = this.monthBonusList.filter((item, i) => i !== ind);
      if (this.monthBonusList.length === 0) {
        this.addBonus();
      }
    } else {
      this.monthBonusList = this.monthBonusList.filter(item => item.BonusRangeId !== bonus.BonusRangeId);
      bonus.IsDeleted = true;
      this.deletedID.push(bonus);
      if (this.monthBonusList.length === 0) {
        this.addBonus();
      }
    }
    this.isMinValueObj = { isMinValue: false, index: ind };
    this.isValueObj = { isValueMax: false, index: ind };
  }

  totalCollisionAmount() {
    this.negativecollisionDeduction = '';
    if (this.collisionDeductionAmount !== '') {
      this.collisionDeduction = +this.noOfCollisions * +this.collisionDeductionAmount;
      if (this.collisionDeduction === 0) {
        this.negativecollisionDeduction = this.collisionDeduction;
      } else if (this.collisionDeduction == NaN) {
        this.negativecollisionDeduction = '';
      }
      else if (this.collisionDeduction !== 0) {
        this.negativecollisionDeduction = `-${this.collisionDeduction}`;
      }
    }
    this.total();
  }

  totalBadReviewAmount() {
    this.negativebadReviewDeduction = ''
    if (this.badReviewDeductionAmount !== '') {
      this.badReviewDeduction = +this.noOfBadReviews * +this.badReviewDeductionAmount;
      if (this.badReviewDeduction === 0) {
        this.negativebadReviewDeduction = this.badReviewDeduction;
      }
      else if (this.badReviewDeduction == NaN) {
        this.negativebadReviewDeduction = '';
      } else if (this.badReviewDeduction !== 0) {
        this.negativebadReviewDeduction = `-${this.badReviewDeduction}`;

      }
    }
    this.total();
  }
  total() {
    let totalAmount = 0;
    let deduction: any;
    for (let i = 0; i < this.monthBonusList.length; i++) {
      totalAmount += (+this.monthBonusList[i].Total);
      deduction = Math.abs(this.collisionDeduction + this.badReviewDeduction)
      this.totalBonusAmount = totalAmount - deduction;
    }

  }
  onMonthChange(event) {
    this.selectedMonth = +event;
    this.getBonusList();
  }
  onYearChange(event) {
    this.selectedYear = +event;
    this.getBonusList();
  }
  saveBonus() {
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
    if (this.isMinValueObj?.isMinValue || this.isValueObj?.isValueMax) {
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
    if (this.deletedID.length > 0) {
      this.deletedID.forEach(item => {
        this.monthBonusList.push(item);
      });
    }

    const bounsRange = this.monthBonusList.map(item => {
      return {
        BonusRangeId: item.BonusRangeId,
        BonusId: item.BonusId,
        Min: item.Min,
        Max: item.Max,
        noOfWashes: item.noOfWashes,
        BonusAmount: item.BonusAmount,
        Total: item.Total,
        IsActive: true,
        IsDeleted: item.IsDeleted
      };
    });
    const bonus = {
      bonusId: this.bonusId,
      locationId: this.locationId,
      bonusStatus: null,
      bonusMonth: this.selectedMonth,
      bonusYear: this.selectedYear,
      noOfBadReviews: this.noOfBadReviews,
      badReviewDeductionAmount: this.badReviewDeductionAmount,
      noOfCollisions: this.noOfCollisions,
      collisionDeductionAmount: this.collisionDeductionAmount,
      totalBonusAmount: this.totalBonusAmount,
      isActive: true,
      isDeleted: false,
      createdBy: this.employeeId,
      createdDate: new Date(),
      updatedBy: this.employeeId,
      updatedDate: new Date()
    };
    const finalObj = {
      bonus,
      bonusRange: bounsRange
    };
    if (this.isEdit === false) {
      this.spinner.show();
      this.bonusSetupService.saveBonus(finalObj).subscribe(res => {
        if (res.status === 'Success') {
          this.spinner.hide();

          this.toastr.success(MessageConfig.Admin.SystemSetup.BonusSetup.Add, 'Success!');
        } else {
          this.spinner.hide();

          this.toastr.error(MessageConfig.CommunicationError, 'Error!');
        }
        this.getBonusList();
      },  (err) => {
        this.spinner.hide();
                this.toastr.error(MessageConfig.CommunicationError, 'Error!');
              });
    } else {
      this.spinner.show();
      this.bonusSetupService.editBonus(finalObj).subscribe(res => {
        if (res.status === 'Success') {
          this.spinner.hide();

          this.toastr.success(MessageConfig.Admin.SystemSetup.BonusSetup.Update, 'Success!');
          this.getBonusList();
        } else {
          this.spinner.hide();

          this.toastr.error(MessageConfig.CommunicationError, 'Error!');
        }
      },  (err) => {
        this.spinner.hide();
                this.toastr.error(MessageConfig.CommunicationError, 'Error!');
              });
    }
  }

  getBonusList() {
    const finalObj = {
      bonusMonth: this.selectedMonth,
      bonusYear: this.selectedYear,
      locationId: this.locationId
    };
    this.isLoading = true;
    this.bonusSetupService.getBonusList(finalObj).subscribe(res => {
      this.isLoading = false;
      if (res.status === 'Success') {
        const bonus = JSON.parse(res.resultData);
        if (bonus?.BonusDetails?.Bonus !== null) {
          this.bonusId = bonus.BonusDetails.Bonus.BonusId;
          this.noOfBadReviews = bonus.BonusDetails.Bonus.NoOfBadReviews;
          this.noOfCollisions = bonus.BonusDetails.Bonus.NoOfCollisions;
          this.badReviewDeductionAmount = bonus.BonusDetails.Bonus.BadReviewDeductionAmount;
          this.collisionDeductionAmount = bonus.BonusDetails.Bonus.CollisionDeductionAmount;
          this.collisionDeduction = this.noOfCollisions * this.collisionDeductionAmount;
          if (this.collisionDeduction === 0) {
            this.negativecollisionDeduction = this.collisionDeduction;
          } else if (this.collisionDeduction == NaN) {
            this.negativecollisionDeduction = '';
          }
          else if (this.collisionDeduction !== 0) {
            this.negativecollisionDeduction = `-${this.collisionDeduction}`;
          }
          this.badReviewDeduction = this.noOfBadReviews * this.badReviewDeductionAmount;
          if (this.badReviewDeduction === 0) {
            this.negativebadReviewDeduction = this.badReviewDeduction;
          }
          else if (this.badReviewDeduction == NaN) {
            this.negativebadReviewDeduction = '';
          }
          else if (this.collisionDeduction !== 0) {
            this.negativebadReviewDeduction = `-${this.badReviewDeduction}`;
          }
        } else {
          this.noOfBadReviews = '';
          this.noOfCollisions = '';
          this.badReviewDeductionAmount = '';
          this.collisionDeductionAmount = '';
          this.collisionDeduction = 0;
          this.badReviewDeduction = 0;
        }
        if (bonus?.BonusDetails?.LocationBasedWashCount !== null) {
          this.noOfWashes = bonus.BonusDetails.LocationBasedWashCount.WashCount;
        } else {
          this.noOfWashes = '';
        }
        if (bonus?.BonusDetails?.BonusRange !== null) {
          // this.noOfWashes = 0;
          this.isEdit = true;
          this.monthBonusList = bonus?.BonusDetails?.BonusRange;
        } else {
          this.isEdit = false;
          this.monthBonusList = [
            {
              BonusRangeId: 0,
              BonusId: this.bonusId, 
              Min: '',
              Max: '',
              noOfWashes: this.noOfWashes,   // ''
              BonusAmount: '',
              Total: '',
              IsActive: true,
              IsDeleted: false
            }
          ];
        }
        let isWashBetween = false;
        for (const list of this.monthBonusList) {
          if (+(list.Min) <= +this.noOfWashes && +this.noOfWashes <= +(list.Max)) {
            list.noOfWashes = this.noOfWashes;
            list.Total = list.BonusAmount;
            isWashBetween = false;
            break;
          } else {
            isWashBetween = true;
            list.Total = '';
          }
        }
        if (isWashBetween) {
          this.toastr.info(MessageConfig.Admin.SystemSetup.BonusSetup.washesMsg, 'Information');
        }
        let totalAmount = 0;
        let deduction: any;
        for (const list of this.monthBonusList) {
          if (+(list.Min) <= +this.noOfWashes && +this.noOfWashes >= +(list.Max)) {
            totalAmount = list.BonusAmount;
          }
        }
        deduction = Math.abs(this.collisionDeduction + this.badReviewDeduction);
        this.totalBonusAmount = totalAmount - deduction;
      }
      else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
        this.getBonusFirstList();
      }
    }, (error) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      this.isLoading = false;
      this.getBonusFirstList();
    });
  }

  cancel() {
    this.getBonusList();
  }

}
