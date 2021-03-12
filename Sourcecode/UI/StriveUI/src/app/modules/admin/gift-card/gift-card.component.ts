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
import { MessageConfig } from 'src/app/shared/services/messageConfig';

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
  search = '';
  collectionSize: number;
 
  page: number;
  pageSize: number;
  pageSizeList: number[];
  query = '';
  sortColumn: { sortBy: string; sortOrder: string; };
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
    this.sortColumn =  { sortBy: ApplicationConfig.Sorting.SortBy.GiftCard, sortOrder: ApplicationConfig.Sorting.SortOrder.GiftCard.order };

    this.page = ApplicationConfig.PaginationConfig.page;
    this.pageSize = ApplicationConfig.PaginationConfig.TableGridSize;
    this.pageSizeList = ApplicationConfig.PaginationConfig.Rows;
    this.getAllGiftCard();
  }

  getAllGiftCard() {
    const locationId = +localStorage.getItem('empLocationId');
    const obj = {
      locationId: localStorage.getItem('empLocationId'),
      startDate: null,
      endDate: null,
      pageNo: this.page,
      pageSize: this.pageSize,
      query: this.search == '' ? null : this.search ,
      sortOrder: this.sortColumn.sortOrder,
      sortBy: this.sortColumn.sortBy,
      status: true
    };
    this.giftCardList = [];
    this.spinner.show();
    this.giftCardService.getAllGiftCard(obj).subscribe(res => {
      this.spinner.hide();
      if (res.status === 'Success') {
        const giftcard = JSON.parse(res.resultData);
        if (giftcard.GiftCard.GiftCardViewModel !== null) {
          this.giftCardList = giftcard.GiftCard.GiftCardViewModel;
          const totalCount = giftcard.GiftCard.Count.Count;
          this.giftCardList.forEach(item => {
            item.searchName = item.GiftCardCode + '' + item.GiftCardName;
          });
          this.clonedGiftCardList = this.giftCardList.map(x => Object.assign({}, x));
          this.collectionSize = Math.ceil(totalCount / this.pageSize) * 10;
        }

      }
    }, (err) => {
      this.spinner.hide();
    });
  }

  paginate(event) {
    this.pageSize = +this.pageSize;
    this.page = event;
    this.getAllGiftCard();
  }
  paginatedropdown(event) {
    this.pageSize = +event.target.value;
    this.page = this.page;
    this.getAllGiftCard();
  }
  searchGift() {
    this.search = this.query;
    this.getAllGiftCard();
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
      this.messageService.showMessage({ severity: 'warning', title: 'Warning', body: MessageConfig.Mandatory });
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
          this.messageService.showMessage({ severity: 'info', title: 'Information', body: MessageConfig.Admin.GiftCard.invalidCard });
          this.isActivity = false;
          this.activeDate = 'none';
          this.totalAmount = 0;
          this.giftCardHistory = [];
        }
      } else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
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
          this.totalAmount = balanceAmount;
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
    this.sortColumn ={
     sortBy: column,
     sortOrder: this.sortColumn.sortOrder == 'ASC' ? 'DESC' : 'ASC'
    }

    this.selectedCls(this.sortColumn)
   this.getAllGiftCard();
 }

 

 selectedCls(column) {
   if (column ===  this.sortColumn.sortBy &&  this.sortColumn.sortOrder === 'DESC') {
     return 'fa-sort-desc';
   } else if (column ===  this.sortColumn.sortBy &&  this.sortColumn.sortOrder === 'ASC') {
     return 'fa-sort-asc';
   }
   return '';
 }

}
