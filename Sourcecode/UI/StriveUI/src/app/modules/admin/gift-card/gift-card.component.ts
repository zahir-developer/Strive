import { Component, OnInit, ViewChild } from '@angular/core';
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
import { TabsetComponent } from 'ngx-bootstrap/tabs';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-gift-card',
  templateUrl: './gift-card.component.html'
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
  @ViewChild(AddGiftCardComponent) addGiftCardComponent: AddGiftCardComponent;
  @ViewChild('staticTabs', { static: false }) staticTabs: TabsetComponent;
  page: number;
  pageSize: number;
  pageSizeList: number[];
  sortColumn: { sortBy: string; sortOrder: string; };
  startDate: Date;
  getGiftCardDetails = [];
  searchUpdate = new Subject<string>();

  constructor(
    private giftCardService: GiftCardService,
    private fb: FormBuilder,
    private modalService: NgbModal,
    private toastr: ToastrService,
    private messageService: MessageServiceToastr,
    private spinner: NgxSpinnerService
  ) {
    // Debounce search.
    this.searchUpdate.pipe(
      debounceTime(ApplicationConfig.debounceTime.sec),
      distinctUntilChanged())
      .subscribe(value => {
        this.getAllGiftCard();
      });
   }

  ngOnInit(): void {
    this.startDate = new Date();
    var amountOfYearsRequired = 5;
    this.startDate.setFullYear(this.startDate.getFullYear() - amountOfYearsRequired);
    this.submitted = false;
    this.isActivity = false;
    this.giftCardForm = this.fb.group({
      number: ['', Validators.required]
    });
    this.sortColumn = { sortBy: ApplicationConfig.Sorting.SortBy.GiftCard, sortOrder: ApplicationConfig.Sorting.SortOrder.GiftCard.order };

    this.page = ApplicationConfig.PaginationConfig.page;
    this.pageSize = ApplicationConfig.PaginationConfig.TableGridSize;
    this.pageSizeList = ApplicationConfig.PaginationConfig.Rows;
    this.getAllGiftCard();
  }

  getAllGiftCard() {
    const obj = {
      locationId: localStorage.getItem('empLocationId'),
      startDate: this.startDate,
      endDate: new Date(),
      pageNo: this.page,
      pageSize: this.pageSize,
      query: this.search == '' ? null : this.search,
      sortOrder: this.sortColumn.sortOrder,
      sortBy: this.sortColumn.sortBy,
      status: true
    };
    this.giftCardList = [];
    this.spinner.show();
    this.giftCardService.getAllGiftCard(obj).subscribe(res => {
      if (res.status === 'Success') {
        this.spinner.hide();

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
      else {
        this.spinner.hide();

      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
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
    this.page =1;
    this.search = this.search;
    this.getAllGiftCard();
  }

  getGiftCardHistoryByTicketNumer() {
    this.submitted = true;
    if (this.giftCardForm.invalid) {
      this.messageService.showMessage({ severity: 'warning', title: 'Warning', body: MessageConfig.Mandatory });
      return;
    }

    this.spinner.show();
    const giftCardNumber = this.giftCardForm.value.number;
    this.giftCardService.getGiftCardHistoryByTicketNumber(giftCardNumber).subscribe(res => {
      if (res.status === 'Success') {
        this.spinner.hide();

        const giftcard = JSON.parse(res.resultData);

        if (giftcard.GiftCardDetail !== null) {

          if (giftcard.GiftCardDetail.GiftCardHistoryViewModel !== null) {
            this.giftCardHistory = giftcard.GiftCardDetail.GiftCardHistoryViewModel;
            this.isActivity = true;

          } else {
            this.messageService.showMessage({ severity: 'info', title: 'Information', body: MessageConfig.Admin.GiftCard.invalidCard });
            this.isActivity = false;
            this.activeDate = 'none';
            this.totalAmount = 0;
            this.giftCardHistory = [];
          }
          this.activeDate = moment(giftcard.GiftCardDetail.GiftCardBalanceViewModel?.ActivationDate).format('MM/DD/YYYY');
          this.totalAmount = giftcard.GiftCardDetail.GiftCardBalanceViewModel?.BalanceAmount;
          this.giftCardID = giftcard.GiftCardDetail.GiftCardBalanceViewModel?.GiftCardId;
        } else {
          this.isActivity = false;
          this.activeDate = 'none';
          this.totalAmount = 0;
          this.giftCardHistory = [];
        }

      }
      else {
        this.spinner.hide();
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      this.spinner.hide();
    });
  }

  get f() {
    return this.giftCardForm.controls;
  }

  // getGiftCardDetail() {
  //   this.submitted = true;
  //   if (this.giftCardForm.invalid) {
  //     this.messageService.showMessage({ severity: 'warning', title: 'Warning', body: MessageConfig.Mandatory });
  //     return;
  //   }
  //   const giftCardNumber = this.giftCardForm.value.number;
  //   // this.giftCardService.getGiftCard(giftCardNumber).subscribe(res => {
  //   //   if (res.status === 'Success') {
  //   //     const giftcardDetail = JSON.parse(res.resultData);
  //   //     if (giftcardDetail.GiftCardDetail.length > 0) {
  //   //       this.activeDate = moment(giftcardDetail.GiftCardDetail[0].ExpiryDate).format('MM/DD/YYYY');
  //   //       this.totalAmount = giftcardDetail.GiftCardDetail[0].TotalAmount;
  //   //       this.giftCardID = giftcardDetail.GiftCardDetail[0].GiftCardId;
  //   //       this.isActivity = true;
  //   //       this.updateBalance();
  //   //       this.getAllGiftCardHistory(giftCardNumber);
  //   //     } else {
  //   //       this.messageService.showMessage({ severity: 'info', title: 'Information', body: MessageConfig.Admin.GiftCard.invalidCard });
  //   //       this.isActivity = false;
  //   //       this.activeDate = 'none';
  //   //       this.totalAmount = 0;
  //   //       this.giftCardHistory = [];
  //   //     }
  //   //   } else {
  //   //     this.toastr.error(MessageConfig.CommunicationError, 'Error!');
  //   //   }
  //   // }, (err) => {
  //   //   this.toastr.error(MessageConfig.CommunicationError, 'Error!');
  //   // });
  //   const obj = {
  //     locationId: localStorage.getItem('empLocationId'),
  //     startDate: this.startDate,
  //     endDate: new Date(),
  //     pageNo: null,
  //     pageSize: null,
  //     query: giftCardNumber == '' ? null : this.search ,
  //     sortOrder: this.sortColumn.sortOrder,
  //     sortBy: this.sortColumn.sortBy,
  //     status: true
  //   };
  //   this.giftCardList = [];
  //   this.spinner.show();
  //   this.giftCardService.getAllGiftCard(obj).subscribe(res => {
  //     if (res.status === 'Success') {
  //       this.spinner.hide();

  //         this.isActivity = true;
  //       const giftcard = JSON.parse(res.resultData);
  //       if (giftcard.GiftCard.GiftCardViewModel !== null) {
  //         this.getGiftCardDetails = giftcard.GiftCard.GiftCardViewModel;



  //         //  this.getGiftCardDetails.forEach(item => {
  //         //   if(  item.GiftCardCode == giftCardNumber){
  //         //     this.updateBalance();

  //         //     this.getGiftCardDetails.push(
  //         //       item


  //         //     )
  //         //   }
  //         //   this.activeDate = moment(this.getGiftCardDetails[0]?.ActivationDate).format('MM/DD/YYYY');
  //         //   this.totalAmount = this.getGiftCardDetails[0]?.TotalAmount;
  //         //   this.giftCardID = this.getGiftCardDetails[0]?.GiftCardId;
  //         // });
  //       }

  //     }
  //     else{
  //       this.spinner.hide();

  //     }
  //   }, (err) => {
  //     this.toastr.error(MessageConfig.CommunicationError, 'Error!');
  //     this.spinner.hide();
  //   });
  // }

  addGiftCard() {
    const ngbModalOptions: NgbModalOptions = {
      backdrop: 'static',
      keyboard: false,
      size: 'lg'
    };
    const modalRef = this.modalService.open(AddGiftCardComponent, ngbModalOptions);
    modalRef.result.then((result) => {
      if (result) {
        this.giftCardForm.patchValue({
          number: result
        })
        this.getGiftCardHistoryByTicketNumer();
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
        this.getGiftCardHistoryByTicketNumer();
      }
    });
  }

  cancelActvity() {
    this.activeDate = 'none';
    this.totalAmount = 0;
    this.isActivity = false;
    this.giftCardForm.reset();
    this.giftCardHistory = [];
  }

  getGiftCardDetail(card) {
    this.giftCardForm.patchValue({
      number: card.GiftCardCode
    });
    this.selectTab(1);
    this.getGiftCardHistoryByTicketNumer();
  }

  selectTab(tabId: number) {
    this.staticTabs.tabs[tabId].active = true;
  }

  updateBalance() {
    this.giftCardService.getBalance(+this.giftCardForm.value.number).subscribe(res => {
      if (res.status === 'Success') {
        const giftcardBalance = JSON.parse(res.resultData);
        if (giftcardBalance.GiftCardDetail.length > 0) {
          const balanceAmount = giftcardBalance.GiftCardDetail[0].BalanceAmount;
          this.totalAmount = balanceAmount;
        }
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  giftCardCollapsed() {
    this.isGiftCardCollapsed = !this.isGiftCardCollapsed;
  }

  activityCollapsed() {
    this.isActivityCollapsed = !this.isActivityCollapsed;
  }

  changeSorting(column) {
    this.sortColumn = {
      sortBy: column,
      sortOrder: this.sortColumn.sortOrder == 'ASC' ? 'DESC' : 'ASC'
    }

    this.selectedCls(this.sortColumn)
    this.getAllGiftCard();
  }



  selectedCls(column) {
    if (column === this.sortColumn.sortBy && this.sortColumn.sortOrder === 'DESC') {
      return 'fa-sort-desc';
    } else if (column === this.sortColumn.sortBy && this.sortColumn.sortOrder === 'ASC') {
      return 'fa-sort-asc';
    }
    return '';
  }

}
