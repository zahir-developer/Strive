import { Component, OnInit } from '@angular/core';
import { GiftCardService } from 'src/app/shared/services/data-service/gift-card.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgbModalOptions, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AddGiftCardComponent } from '../gift-card/add-gift-card/add-gift-card.component';
import { AddActivityComponent } from '../gift-card/add-activity/add-activity.component';
import * as moment from 'moment';
import { ToastrService } from 'ngx-toastr';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { ApplicationConfig } from 'src/app/shared/services/ApplicationConfig';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-gift-card',
  templateUrl: './gift-card.component.html',
  styleUrls: ['./gift-card.component.css']
})
export class GiftCardComponent implements OnInit {
  giftCardForm: FormGroup;
  giftCardHistory: any = [];
  isActivity: boolean;
  activeDate: any = 'none';
  totalAmount: any = 0;
  submitted: boolean;
  giftCardID: any;
  isGiftCardCollapsed = false;
  isActivityCollapsed = false;
  giftCardList = [];
  clonedGiftCardList = [];
 
  collectionSize: number;
  sort = { column: 'GiftCardCode', descending: true };
  sortColumn: { column: string; descending: boolean; };
  page: number;
  pageSize: number;
  pageSizeList: number[];
  constructor(
    private giftCardService: GiftCardService,
    private fb: FormBuilder,
    private modalService: NgbModal,
    private toastr: ToastrService,
    private messageService: MessageServiceToastr,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit(): void {
    this.submitted = false;
    this.isActivity = false;
    this.giftCardForm = this.fb.group({
      number: ['', Validators.required]
    });
    this.page = ApplicationConfig.PaginationConfig.page;
    this.pageSize = ApplicationConfig.PaginationConfig.TableGridSize;
    this.pageSizeList = ApplicationConfig.PaginationConfig.Rows;
    this.getAllGiftCard();
  }

  getAllGiftCard() {
    const locationId = +localStorage.getItem('empLocationId');
    this.spinner.show();
    this.giftCardService.getAllGiftCard(locationId).subscribe(res => {
      this.spinner.hide();
      if (res.status === 'Success') {
        const giftcard = JSON.parse(res.resultData);
        console.log(giftcard, 'giftCard');
        this.giftCardList = giftcard.GiftCard;
        this.giftCardList.forEach( item => {
          item.searchName = item.GiftCardCode + '' + item.GiftCardName;
        });
        this.clonedGiftCardList = this.giftCardList.map(x => Object.assign({}, x));
        this.collectionSize = Math.ceil(this.giftCardList.length / this.pageSize) * 10;
      }
    }, (err) => {
      this.spinner.hide();
    });
  }

  paginate(event) {
    this.pageSize = +this.pageSize;
    this.page = event ;
    this.getAllGiftCard();
  }
  paginatedropdown(event) {
    this.pageSize = +event.target.value;
    this.page =  this.page;
    this.getAllGiftCard();
  }
  searchGift(text) {
    if (text.length > 0) {
      this.giftCardList = this.clonedGiftCardList.filter(item => item.searchName.toLowerCase().includes(text));
    } else {
      this.giftCardList = [];
      this.giftCardList = this.clonedGiftCardList;
    }
  }

  getAllGiftCardHistory(giftCardNumber) {
    this.giftCardService.getAllGiftCardHistory(giftCardNumber).subscribe(res => {
      if (res.status === 'Success') {
        const cardHistory = JSON.parse(res.resultData);
        this.giftCardHistory = cardHistory.GiftCardHistory;
      }
    });
  }

  get f() {
    return this.giftCardForm.controls;
  }

  getGiftCardDetail() {
    this.submitted = true;
    if (this.giftCardForm.invalid) {
      this.messageService.showMessage({ severity: 'warning', title: 'Warning', body: 'Please Enter Mandatory fields' });
      return;
    }
    const giftCardNumber = +this.giftCardForm.value.number;
    this.giftCardService.getGiftCard(giftCardNumber).subscribe(res => {
      if (res.status === 'Success') {
        const giftcardDetail = JSON.parse(res.resultData);
        if (giftcardDetail.GiftCardDetail.length > 0) {
          this.activeDate = moment(giftcardDetail.GiftCardDetail[0].ExpiryDate).format('MM/DD/YYYY');
          this.totalAmount = giftcardDetail.GiftCardDetail[0].TotalAmount;
          this.giftCardID = giftcardDetail.GiftCardDetail[0].GiftCardId;
          this.isActivity = true;
          this.updateBalance();
          this.getAllGiftCardHistory(giftCardNumber);
        } else {
          this.messageService.showMessage({ severity: 'info', title: 'Information', body: 'Invalid Card Number' });
          this.isActivity = false;
          this.activeDate = 'none';
          this.totalAmount = 0;
          this.giftCardHistory = [];
        }
      } else {
        this.messageService.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
      }
    });
  }

  addGiftCard() {
    const ngbModalOptions: NgbModalOptions = {
      backdrop: 'static',
      keyboard: false,
      size: 'lg'
    };
    const modalRef = this.modalService.open(AddGiftCardComponent, ngbModalOptions);
    modalRef.result.then((result) => {
      if (result) {
        this.getAllGiftCard();
      }
    });
  }

  statusUpdate(card) {
    const finalObj = {
      giftCardId: card.GiftCardId,
      isActive: card.IsActive ? false : true
    };
    this.giftCardService.updateStatus(finalObj).subscribe(res => {
      if (res.status === 'Success') {
        this.getAllGiftCardHistory(card.GiftCardId);
      }
    });
  }

  addActivity() {
    const ngbModalOptions: NgbModalOptions = {
      backdrop: 'static',
      keyboard: false,
      size: 'lg'
    };
    const modalRef = this.modalService.open(AddActivityComponent, ngbModalOptions);
    modalRef.componentInstance.giftCardNumber = +this.giftCardForm.value.number;
    modalRef.componentInstance.activeDate = this.activeDate;
    modalRef.componentInstance.totalAmount = this.totalAmount;
    modalRef.componentInstance.giftCardId = this.giftCardID;
    modalRef.result.then((result) => {
      if (result) {
        this.getAllGiftCardHistory(+this.giftCardForm.value.number);
        this.updateBalance();
      }
    });
  }

  cancelActvity() {
    this.activeDate = 'none';
    this.totalAmount = 0;
    this.isActivity = false;
    this.giftCardForm.reset();
  }

  updateBalance() {
    this.giftCardService.getBalance(+this.giftCardForm.value.number).subscribe(res => {
      if (res.status === 'Success') {
        const giftcardBalance = JSON.parse(res.resultData);
        if (giftcardBalance.GiftCardDetail.length > 0) {
          const balanceAmount = giftcardBalance.GiftCardDetail[0].BalaceAmount;
          //if (balanceAmount > 0.00) {
            this.totalAmount = balanceAmount;
          //}
        }
      }
    });
  }

  giftCardCollapsed() {
    this.isGiftCardCollapsed = !this.isGiftCardCollapsed;
  }

  activityCollapsed() {
    this.isActivityCollapsed = !this.isActivityCollapsed;
  }

  changeSorting(column) {
    this.changeSortingDescending(column, this.sort);
    this.sortColumn = this.sort;
  }

  changeSortingDescending(column, sortingInfo) {
    if (sortingInfo.column === column) {
      sortingInfo.descending = !sortingInfo.descending;
    } else {
      sortingInfo.column = column;
      sortingInfo.descending = false;
    }
    return sortingInfo;
  }

  sortedColumnCls(column, sortingInfo) {
    if (column === sortingInfo.column && sortingInfo.descending) {
      return 'fa-sort-desc';
    } else if (column === sortingInfo.column && !sortingInfo.descending) {
      return 'fa-sort-asc';
    }
    return '';
  }

  selectedCls(column) {
    return this.sortedColumnCls(column, this.sort);
  }

}
