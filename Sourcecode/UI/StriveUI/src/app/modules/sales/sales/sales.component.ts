import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { MembershipService } from 'src/app/shared/services/data-service/membership.service';
import { SalesService } from 'src/app/shared/services/data-service/sales.service';
import { ConfirmationUXBDialogService } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.service';
import { NgbModalOptions, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { EditItemComponent } from './edit-item/edit-item.component';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { ThemeService } from 'src/app/shared/common-service/theme.service';
import { ServiceSetupService } from 'src/app/shared/services/data-service/service-setup.service';
import { GiftCardService } from 'src/app/shared/services/data-service/gift-card.service';
import * as moment from 'moment';
import insertTextAtCursor from 'insert-text-at-cursor';
import { NgxSpinnerService } from 'ngx-spinner';
@Component({
  selector: 'app-sales',
  templateUrl: './sales.component.html',
  styleUrls: ['./sales.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class SalesComponent implements OnInit {
  services: any;
  validGiftcard: any;
  targetId = '';
  showPopup = false;
  isInvalidGiftcard = false;
  discount = '';
  discounts = [];
  selectedDiscount = [];
  filteredItem = [];
  creditcashback = 0;
  cashback = 0;
  selected = false;
  isSelected = true;
  creditTotal = 0;
  cashTotal = 0;
  giftCardForm: FormGroup;
  addItemForm: FormGroup;
  itemList: any;
  originalGrandTotal = 0;
  JobId: any;
  newTicketNumber: any;
  selectedService: any;
  balance: number;
  constructor(private membershipService: MembershipService, private salesService: SalesService,
    private confirmationService: ConfirmationUXBDialogService, private modalService: NgbModal, private fb: FormBuilder,
    private messageService: MessageServiceToastr, private service: ServiceSetupService, 
    private giftcardService: GiftCardService, private spinner: NgxSpinnerService) { }
  ItemName = '';
  ticketNumber = '';
  count = 2;
  giftcards = [];
  giftCardNumber = '';
  giftCardAmount = '';
  washes = [];
  additionalService = [];
  details = [];
  giftcardsubmitted = false;
  total = '';
  tax = '';
  enableAdd = false;
  grandTotal = '';
  cash = 0;
  credit = 0;
  giftCard = 0;
  account = 0;
  totalPaid = 0;
  balanceDue = 0;
  Cashback = '';
  ngOnInit(): void {
    this.giftCardFromInit();
    this.addItemFormInit();
    this.getAllService();
    this.getServiceForDiscount();
  }
  giftCardFromInit() {
    this.giftCardForm = this.fb.group({
      giftCardNumber: ['', Validators.required],
      giftCardAmount: ['', Validators.required]
    });
  }
  addItemFormInit() {
    this.addItemForm = this.fb.group({
      itemName: [''],
      quantity: ['', Validators.required]
    });
  }
  get f() { return this.giftCardForm.controls; }
  getAllService() {
    this.salesService.getService().subscribe(data => {
      if (data.status === 'Success') {
        console.log(data.status, 'getService');
        const services = JSON.parse(data.resultData);
        if (services.ServicesWithPrice !== null && services.ServicesWithPrice.length > 0) {
          this.services = services.ServicesWithPrice.map(item => {
            return {
              id: item.ServiceId,
              name: item.ServiceName.trim(),
              price: item.Price
            };
          });
          console.log(this.services);
        }
      }
    });
  }
  getServiceForDiscount() {
    this.service.getServiceSetup().subscribe(data => {
      if (data.status === 'Success') {
        const services = JSON.parse(data.resultData);
        if (services.ServiceSetup !== null && services.ServiceSetup.length !== 0) {
          this.discounts = services.ServiceSetup.filter(item => item.ServiceType === 'Discounts');
        }
      }
    });
  }
  selectedItem(event) {
    this.selectedService = event;
  }
  clearpaymentField() {
    this.cash = 0;
    this.credit = 0;
    this.giftCard = 0;
    this.selectedDiscount = [];
    this.giftcards = [];
  }
  getDetailByTicket() {
    this.washes = [];
    this.details = [];
    this.additionalService = [];
    this.clearpaymentField();
    if ((this.ticketNumber !== undefined && this.ticketNumber !== '') ||
      (this.newTicketNumber !== undefined && this.newTicketNumber !== '')) {
      const ticketNumber = this.ticketNumber ? this.ticketNumber : this.newTicketNumber ? this.newTicketNumber : 0;
      this.spinner.show();
      this.salesService.getItemByTicketNumber(+ticketNumber).subscribe(data => {
        this.spinner.hide();
        if (data.status === 'Success') {
          this.enableAdd = true;
          this.clearform();
          this.addItemFormInit();
          this.itemList = JSON.parse(data.resultData);
          if (this.itemList.Status.ScheduleItemViewModel !== null) {
            if (this.itemList.Status.ScheduleItemViewModel.length !== 0) {
              this.showPopup = true;
              this.JobId = this.itemList.Status.ScheduleItemViewModel[0].JobId;
              this.washes = this.itemList.Status.ScheduleItemViewModel.filter(item => item.ServiceType === 'Washes');
              this.details = this.itemList.Status.ScheduleItemViewModel.filter(item => item.ServiceType === 'Details');
              this.additionalService = this.itemList.Status.ScheduleItemViewModel.filter(item => 
                item.ServiceType === 'Additional Services');
            }
          } else {
            this.showPopup = false;
          }
          if (this.itemList?.Status?.ScheduleItemSummaryViewModels !== null) {
            const summary = this.itemList?.Status?.ScheduleItemSummaryViewModels;
            this.cashback = this.itemList?.Status?.ScheduleItemSummaryViewModels?.Cashback;
            this.grandTotal = summary?.GrandTotal ? summary?.GrandTotal : summary?.Total ? (summary?.Total + summary?.Tax) : 0;
            this.cashTotal = +this.grandTotal;
            this.creditTotal = +this.grandTotal;
            this.originalGrandTotal = +this.grandTotal;
          }
        }
      }, (err) => {
        this.enableAdd = false;
        this.spinner.hide();
      });
    }
  }
  clearform() {
    this.cash = this.giftCard = this.credit = 0;
  }
  filterItem(event) {
    const filtered: any[] = [];
    const query = event.query;
    for (const i of this.services) {
      const client = i;
      if (client.name.toLowerCase().indexOf(query.toLowerCase()) === 0) {
        filtered.push(client);
      }
    }
    this.filteredItem = filtered;
  }
  deleteItem(data) {
    this.confirmationService.confirm('Delete Location', `Are you sure you want to delete this location? All related 
    information will be deleted and the location cannot be retrieved?`, 'Yes', 'No')
      .then((confirmed) => {
        if (confirmed === true) {
          this.confirmDelete(data);
        }
      })
      .catch(() => { });
  }

  // Delete location
  confirmDelete(data) {
    this.salesService.deleteItemById(data.JobItemId).subscribe(res => {
      if (res.status === 'Success') {
        this.messageService.showMessage({ severity: 'success', title: 'Success', body: 'Item deleted successfully' });
        this.getDetailByTicket();
      } else {
        this.messageService.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
      }
    }, (err) => {
      this.messageService.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
    });
  }
  openCash() {
    this.cashTotal = this.cash !== 0 ? this.cash : this.grandTotal ? +this.grandTotal : 0;
    document.getElementById('cashpopup').style.width = '300px';
    document.getElementById('Giftcardpopup').style.width = '0';
    document.getElementById('creditcardpopup').style.width = '0';
  }
  opengiftcard() {
    // this.giftcards = [];
    document.getElementById('Giftcardpopup').style.width = '450px';
    document.getElementById('creditcardpopup').style.width = '0';
  }
  closecash() {
    document.getElementById('cashpopup').style.width = '0';
  }

  closegiftcard() {
    document.getElementById('Giftcardpopup').style.width = '0';
  }
  opendiscount() {
    // this.selectedDiscount = [];
    this.discount = '';
    document.getElementById('discountpopup').style.width = '450px';
    document.getElementById('cashpopup').style.width = '0';
    document.getElementById('Giftcardpopup').style.width = '0';
    document.getElementById('creditcardpopup').style.width = '0';
  }
  closediscount() {
    document.getElementById('discountpopup').style.width = '0';
  }
  opencreditcard() {
    this.creditTotal = this.grandTotal ? +this.grandTotal : 0;
    this.creditcashback = 0;
    document.getElementById('creditcardpopup').style.width = '300px';
    document.getElementById('Giftcardpopup').style.width = '0';
  }

  closecreditcard() {
    document.getElementById('creditcardpopup').style.width = '0';
  }
  editItem(event) {
    const itemId = event.JobId;
    const ngbModalOptions: NgbModalOptions = {
      backdrop: 'static',
      keyboard: false,
      size: 'lg'
    };
    const modalRef = this.modalService.open(EditItemComponent, ngbModalOptions);
    modalRef.componentInstance.JobId = itemId;
    modalRef.componentInstance.ItemDetail = event;
    modalRef.componentInstance.isModal = true;
    modalRef.result.then(
      (data: any) => {
        this.getDetailByTicket();
      },
      (reason: any) => { }
    );
  }
  deletegiftcard(event) {
    const index = this.giftcards.findIndex(item => item.id === +event.id);
    this.giftcards.splice(index, 1);

  }
  addGiftCard() {
    this.giftcardsubmitted = true;
    const giftCardNumber = this.giftCardForm.value.giftCardNumber;
    const giftCardAmount = this.giftCardForm.value.giftCardAmount;
    if (this.giftCardForm.valid) {
      this.giftcards.push({ id: this.validGiftcard?.GiftCardDetail[0]?.GiftCardId, number: giftCardNumber, amount: giftCardAmount });
      console.log(this.selectedDiscount);
      this.giftCardForm.reset();
      this.balance = 0;
      this.validGiftcard.GiftCardDetail[0].BalaceAmount = 0;
      this.giftcardsubmitted = false;
    } else {
      return;
    }
  }
  giftCardProcess() {
    let gc = 0;
    this.giftcards.reduce(item => +item.amount);
    gc = this.giftcards.reduce((accum, item) => accum + (+item.amount), 0);
    this.giftCard = gc;
    this.totalPaid = this.totalPaid + this.giftCard;
    document.getElementById('Giftcardpopup').style.width = '0';
  }
  addItem() {
    if (this.addItemForm.invalid) {
      return;
    } else if (this.addItemForm.value.itemName === '' || this.filteredItem.length === 0) {
      return;
    }
    const formObj = {
      job: {
        jobId: this.isSelected ? this.JobId : 0,
        ticketNumber: this.isSelected ? this.ticketNumber.toString() : this.newTicketNumber.toString(),
        locationId: +localStorage.getItem('empLocationId'),
        clientId: 1,
        vehicleId: 1,
        make: 0,
        model: 0,
        color: 0,
        jobType: 1,
        jobDate: new Date(),
        timeIn: new Date(),
        estimatedTimeOut: new Date(),
        actualTimeOut: new Date(),
        jobStatus: 1,
        isActive: true,
        isDeleted: this.isSelected ? false : true,
        createdBy: 1,
        createdDate: new Date(),
        updatedBy: 1,
        updatedDate: new Date(),
        notes: 'checking'
      },
      jobItem: {
        jobItemId: 0,
        jobId: this.isSelected ? this.JobId : 0,
        serviceId: this.selectedService?.id,
        commission: 0,
        price: this.selectedService?.price,
        quantity: +this.addItemForm.value.quantity,
        reviewNote: 'test',
        isActive: true,
        isDeleted: this.isSelected ? false : true,
        createdBy: 1,
        createdDate: new Date(),
        updatedBy: 1,
        updatedDate: new Date(),
        employeeId: +localStorage.getItem('empId')
      }
    };
    if (this.isSelected) {
      this.salesService.updateListItem(formObj).subscribe(data => {
        if (data.status === 'Success') {
          this.messageService.showMessage({ severity: 'success', title: 'Success', body: 'Item added successfully' });
          this.getDetailByTicket();
        } else {
          this.messageService.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
        }
      });
    } else {
      this.salesService.addItem(formObj).subscribe(data => {
        if (data.status === 'Success') {
          this.messageService.showMessage({ severity: 'success', title: 'Success', body: 'Item added successfully' });
          this.isSelected = true;
          this.ticketNumber = this.newTicketNumber;
          this.getDetailByTicket();
        } else {
          this.messageService.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
        }
      });
    }
  }
  getNumAndUpdate(num) {
    if (this.targetId !== '') {
      const el = document.getElementById(this.targetId);
      insertTextAtCursor(el, num.toString());
    }
    // this.addItemForm.patchValue({ quantity: this.addItemForm.value.quantity.toString() + num.toString() });
  }
  clear() {
    this.addItemForm.patchValue({ itemName: '', quantity: '' });
    this.ticketNumber = '';
  }
  backspace() {
    if (this.targetId === 'quantity') {
      const quantity = this.addItemForm.value.quantity.toString();
      this.addItemForm.patchValue({ quantity: quantity.substring(0, quantity.length - 1) });
    } else if (this.targetId === 'ticketNumber') {
      const ticketNumber = this.ticketNumber? this.ticketNumber.toString() : '';
      this.ticketNumber = ticketNumber.substring(0, ticketNumber.length - 1);
    } else {
      return;
    }
  }
  addCashBack(cashback) {
    // this.creditTotal
    this.creditcashback = this.creditcashback + cashback;
    this.creditTotal = +this.creditTotal + cashback;
  }
  reset() {
    this.creditTotal = (this.creditTotal - this.creditcashback);
    this.creditcashback = 0;
  }
  getTicketNumber() {
    this.isSelected = false;
    this.salesService.getTicketNumber().subscribe(data => {
      this.newTicketNumber = data;
      this.enableAdd = true;
      this.washes = [];
      this.details = [];
      this.additionalService = [];
      if (this.itemList?.Status?.ScheduleItemSummaryViewModels) {
        this.itemList.Status.ScheduleItemSummaryViewModels = {};
      }
    });
  }
  creditProcess() {
    this.credit = this.creditTotal - this.creditcashback;
    this.totalPaid = this.totalPaid + this.credit;
    this.cashback = this.cashback + this.creditcashback;
    document.getElementById('creditcardpopup').style.width = '0';
  }
  addCash(cash) {
    this.cashTotal = +this.cashTotal + cash;
  }
  cashProcess() {
    this.cash = this.cashTotal;
    this.totalPaid = this.totalPaid + this.cash;
    document.getElementById('cashpopup').style.width = '0';
  }
  discountProcess() {
    document.getElementById('discountpopup').style.width = '0';
  }
  discountChange(event) {
    this.discounts.forEach(item => {
      if (item.ServiceId === +event.target.value) {
        this.selectedDiscount.push(item);
      }
    });
  }
  deletediscount(event) {
    const index = this.selectedDiscount.findIndex(item => item.ServiceId === +event.ServiceId);
    this.selectedDiscount.splice(index, 1);
  }
  addPayment() {
    let giftcard = null;
    let discount = null;
    giftcard = this.giftcards.map(item => {
      return {
        giftCardHistoryId: 1,
        giftCardId: item.id,
        locationId: +localStorage.getItem('empLocationId'),
        transactionType: 1,
        transactionAmount: -(+item.amount),
        transactionDate: new Date(),
        comments: 'string',
        isActive: true,
        isDeleted: true,
        createdBy: 1,
        createdDate: new Date(),
        updatedBy: 1,
        updatedDate: new Date(),
        jobPaymentId: 4
      };
    });
    discount = this.selectedDiscount.map(item => {
      return {
        jobPaymentDiscountId: 0,
        jobPaymentId: 0,
        serviceDiscountId: +item.ServiceId,
        amount: item.Cost,
        isActive: true,
        isDeleted: true,
        createdBy: 1,
        createdDate: new Date(),
        updatedBy: 1,
        updatedDate: new Date()
      }
    });
    const paymentObj = {
      jobPayment: {
        jobPaymentId: 0,
        jobId: this.isSelected ? +this.JobId : 0,
        drawerId: +localStorage.getItem('drawerId'),
        paymentType: 109,
        amount: this.cash ? +this.cash : 0,
        taxAmount: 0,
        cashback: 0,
        approval: true,
        checkNumber: '',
        signature: '',
        paymentStatus: 1,
        comments: '',
        isActive: true,
        isDeleted: true,
        createdBy: 1,
        createdDate: new Date(),
        updatedBy: 1,
        updatedDate: new Date()
      },
      giftCardHistory: giftcard.length === 0 ? null : giftcard,
      jobPaymentCreditCard: {
        jobPaymentCreditCardId: 0,
        jobPaymentId: 0,
        cardTypeId: 1,
        cardCategoryId: 1,
        cardNumber: '',
        creditCardTransactionTypeId: 1,
        amount: this.credit ? +this.credit : 0,
        tranRefNo: '',
        tranRefDetails: '',
        isActive: true,
        isDeleted: false,
        createdBy: 1,
        createdDate: new Date(),
        updatedBy: 1,
        updatedDate: new Date()
      },
      jobPaymentDiscount: discount.length === 0 ? null : discount
    };
    this.spinner.show();
    this.salesService.addPayemnt(paymentObj).subscribe(data => {
      this.spinner.hide();
      if (data.status === 'Success') {
        this.messageService.showMessage({ severity: 'success', title: 'Success', body: 'Payment completed successfully' });
        this.getDetailByTicket();
      } else {
        this.messageService.showMessage({ severity: 'error', title: 'Error', body: 'Unable to complete payment, please try again.' });
      }
    }, (err) => {
      this.messageService.showMessage({ severity: 'error', title: 'Error', body: 'Unable to complete payment, please try again.' });
      this.spinner.hide();
    });
  }
  deleteTicket() {
    if (this.ticketNumber !== '' && this.ticketNumber !== undefined) {
      this.salesService.deleteJob(+this.ticketNumber).subscribe(data => {
        if (data.status === 'Success') {
          this.messageService.showMessage({ severity: 'success', title: 'Success', body: 'Deleted job successfully' });
          this.getDetailByTicket();
        } else {
          this.messageService.showMessage({ severity: 'error', title: 'Error', body: 'Communication error' });
        }
      }, (err) => {
        this.messageService.showMessage({ severity: 'error', title: 'Error', body: 'Communication error' });
      });
    }
  }
  validateGiftcard() {
    const gNo = this.giftCardForm.value.giftCardNumber;
    this.giftcardService.getBalance(gNo).subscribe(data => {
      if (data.status === 'Success') {
        this.validGiftcard = JSON.parse(data.resultData);
        if (this.validGiftcard.GiftCardDetail.length === 0) {
          this.isInvalidGiftcard = true;
        } else {
          this.isInvalidGiftcard = false;
        }
      }
      //console.log(data);
    });
  }
  validateAmount() {
    this.isInvalidGiftcard = false;
    const enteredAmount = this.giftCardForm.value.giftCardAmount;
    const currentAmount = this.validGiftcard?.GiftCardDetail[0]?.BalaceAmount;
    const today = new Date();
    const giftcardexpiryDate = this.validGiftcard?.GiftCardDetail[0]?.ActiveDate;
    if (enteredAmount !== undefined && currentAmount !== undefined) {
      if (currentAmount < enteredAmount) {
        this.giftCardForm.patchValue({ giftCardAmount: '' });
        this.balance = 0;
      } else {
        this.balance = currentAmount - enteredAmount;
      }
    }
    // if (!moment(today).isBefore(giftcardexpiryDate)) {
    //   this.isInvalidGiftcard = true;
    // }
  }
  rollBack() {
    if (this.ticketNumber !== '' && this.ticketNumber !== undefined) {
    this.salesService.rollback(+this.ticketNumber).subscribe(data => {
      if (data.status === 'Success') {
        this.getDetailByTicket();
        this.messageService.showMessage({ severity: 'success', title: 'Success', body: 'Rollbaced Successfully' });
      } else {
        this.messageService.showMessage({ severity: 'error', title: 'Error', body: 'Communication error' });
      }
    }, (err) => {
      this.messageService.showMessage({ severity: 'error', title: 'Error', body: 'Communication error' });
    });
    }
  }
  quantityFocus(event) {
    this.targetId = event.target.id;
  }
}
