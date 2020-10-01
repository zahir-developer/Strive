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
import { ActivatedRoute } from '@angular/router';
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
  enableButton = false;
  showPopup = false;
  products = [];
  discountService = [];
  serviceAndProduct = [];
  Products = [];
  isInvalidGiftcard = false;
  discount = '';
  discountCash = 0;
  isDisableService = false;
  discounts = [];
  outsideServices = [];
  upCharges = [];
  selectedDiscount = [];
  filteredItem = [];
  airfreshnerService = [];
  creditcashback = 0;
  cashback = 0;
  initialcashback = 0;
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
    private giftcardService: GiftCardService, private spinner: NgxSpinnerService,
    private route: ActivatedRoute) { }
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
  submitted = false;
  enableAdd = false;
  grandTotal = '';
  cash = 0;
  credit = 0;
  giftCard = 0;
  account = 0;
  totalPaid = 0;
  balanceDue = 0;
  Cashback = '';
  discountAmount = 0;
  ngOnInit(): void {
    this.giftCardFromInit();
    this.addItemFormInit();
    const paramsData = this.route.snapshot.queryParamMap.get('ticketNumber');
    if (paramsData !== null) {
      this.ticketNumber = paramsData;
      this.getDetailByTicket(false);
    }
    // this.getAllService();
    this.getServiceForDiscount();
    this.getAllServiceandProduct();
  }
  getAllServiceandProduct() {
    this.salesService.getServiceAndProduct().subscribe(data => {
      if (data.status === 'Success') {
        console.log(data.status, 'getService');
        const services = JSON.parse(data.resultData);
        if (services.ServiceAndProductList !== null && services.ServiceAndProductList.Service.length > 0) {
          this.services = services.ServiceAndProductList.Service.map(item => {
            return {
              id: item.ServiceId,
              name: item.ServiceName.trim(),
              price: item.Cost,
              type: 'service'
            };
          });
        }
        if (services.ServiceAndProductList !== null && services.ServiceAndProductList.Product.length > 0) {
          this.products = services.ServiceAndProductList.Product.map(item => {
            return {
              id: item.ProductId,
              name: item.ProductName.trim(),
              price: item.Price,
              type: 'product'
            };
          });
        }
        this.serviceAndProduct = this.services.concat(this.products);
      }
    });
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
  get a() { return this.addItemForm.controls; }
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
              price: item.Price,
              type: 'service'
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
    if (this.selectedService.type === 'service') {
      this.addItemForm.patchValue({ quantity: 1 });
      this.addItemForm.controls.quantity.disable();
      this.isDisableService = true;
    } else {
      this.addItemForm.controls.quantity.enable();
      this.addItemForm.patchValue({ quantity: 1 });
      this.isDisableService = false;
    }
  }
  clearGridItems() {
    this.washes = [];
    this.details = [];
    this.additionalService = [];
    this.upCharges = [];
    this.airfreshnerService = [];
    this.outsideServices = [];
    this.discountService = [];
    this.Products = [];
    if (this.itemList?.Status?.ScheduleItemSummaryViewModels) {
      this.itemList.Status.ScheduleItemSummaryViewModels = {};
    }
    this.showPopup = false;
    this.giftCardForm.reset();
    this.isInvalidGiftcard = false;
  }
  clearpaymentField() {
    this.cash = 0;
    this.credit = 0;
    this.giftCard = 0;
    this.selectedDiscount = [];
    this.giftcards = [];
    this.totalPaid = 0;
    this.balanceDue = 0;
    this.originalGrandTotal = 0;
    this.creditcashback = 0;
    this.cashback = 0;
    this.selectedDiscount = [];
    this.selectedService = [];
  }
  getDetailByTicket(flag) {
    this.enableButton = false;
    if (flag !== true) {
      this.clearGridItems();
      this.clearpaymentField();
    } else {
      this.clearGridItems();
    }
    if ((this.ticketNumber !== undefined && this.ticketNumber !== '') ||
      (this.newTicketNumber !== undefined && this.newTicketNumber !== '')) {
      const ticketNumber = this.ticketNumber ? this.ticketNumber : this.newTicketNumber ? this.newTicketNumber : 0;
      this.spinner.show();
      this.salesService.getItemByTicketNumber(+ticketNumber).subscribe(data => {
        this.spinner.hide();
        if (data.status === 'Success') {
          this.enableAdd = true;
          this.itemList = JSON.parse(data.resultData);
          if (this.itemList.Status.JobPaymentViewModel === null) {
            this.messageService.showMessage({ severity: 'error', title: 'Error', body: 'Invalid Ticket Number' });
            return;
          } else {
            this.JobId = this.itemList?.Status?.JobPaymentViewModel?.JobId;
          }
          if (this.itemList.Status.ScheduleItemViewModel !== null) {
            if (this.itemList.Status.ScheduleItemViewModel.length !== 0) {
              this.showPopup = true;
              this.washes = this.itemList.Status.ScheduleItemViewModel.filter(item => item.ServiceType === 'Washes');
              this.details = this.itemList.Status.ScheduleItemViewModel.filter(item => item.ServiceType === 'Details');
              this.additionalService = this.itemList.Status.ScheduleItemViewModel.filter(item =>
                item.ServiceType === 'Additional Services');
              this.upCharges = this.itemList.Status.ScheduleItemViewModel.filter(item =>
                item.ServiceType === 'Upcharges');
              this.outsideServices = this.itemList.Status.ScheduleItemViewModel.filter(item =>
                item.ServiceType === 'Outside Services');
              this.airfreshnerService = this.itemList.Status.ScheduleItemViewModel.filter(item =>
                item.ServiceType === 'Air Fresheners');
              this.discountService = this.itemList.Status.ScheduleItemViewModel.filter(item =>
                item.ServiceType === 'Discounts');
            }
          } else {
            this.showPopup = false;
          }
          if (this.itemList?.Status?.ScheduleItemSummaryViewModels !== null) {
            const summary = this.itemList?.Status?.ScheduleItemSummaryViewModels;
            this.initialcashback = summary?.Cashback ? summary?.Cashback : 0;
            this.cashback = summary?.CashBack ? summary?.CashBack : 0;
            this.grandTotal = summary?.GrandTotal ? summary?.GrandTotal : summary?.Total ? (summary?.Total + summary?.Tax) : 0;
            this.cashTotal = +this.grandTotal;
            this.creditTotal = +this.grandTotal;
            this.cash = +summary?.Cash;
            this.credit = +summary?.Credit;
            this.discountAmount = summary?.Discount;
            this.originalGrandTotal = +this.grandTotal;
            this.giftCard = Math.abs(+summary?.GiftCard);
            this.balance = +summary?.Balance;
            this.totalPaid = +summary?.TotalPaid;

          }
          if (this.itemList?.Status?.ProductItemViewModel !== null && this.itemList?.Status?.ProductItemViewModel !== undefined) {
            this.Products = this.itemList?.Status?.ProductItemViewModel;
          }
          if (this.itemList?.Status?.JobPaymentViewModel?.IsProcessed === true) {
            this.enableButton = true;
          } else {
            this.enableButton = false;
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
    for (const i of this.serviceAndProduct) {
      const client = i;
      if (client.name.toLowerCase().indexOf(query.toLowerCase()) === 0) {
        filtered.push(client);
      }
    }
    this.filteredItem = filtered;
  }
  deleteItem(data, type) {
    const title = type === 'deleteItem' ? 'Delete Item' : type === 'rollback' ? 'RollBacK' : 'Delete Ticket';
    const message = type === 'deleteItem' ? 'Are you sure you want to delete the selected Item?' : type === 'rollback' ? 'Are you sure you want to Rollback the transaction?' : 'Are you sure you want to delete the Ticket?';
    this.confirmationService.confirm(title, message, 'Yes', 'No')
      .then((confirmed) => {
        if (confirmed === true) {
          if (type === 'deleteItem') {
            this.confirmDelete(data);
          } else if (type === 'rollback') {
            this.rollBack();
          } else {
            this.deleteTicket();
          }
        }
      })
      .catch(() => { });
  }

  // Delete location
  confirmDelete(data) {
    const itemId = data?.JobItemId ? data?.JobItemId : data?.JobProductItemId;

    const deleteItem = 
    {
      ItemId : itemId,
      IsJobItem : data?.JobItemId ? true : false
    }


    this.salesService.deleteItemById(deleteItem).subscribe(res => {
      
      if (res.status === 'Success') {
        this.messageService.showMessage({ severity: 'success', title: 'Success', body: 'Item deleted successfully' });
        this.getDetailByTicket(false);
      } else {
        this.messageService.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
      }
    }, (err) => {
      this.messageService.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
    });
  }
  openCash() {
    const cashTotal = (this.originalGrandTotal - this.totalPaid - this.discountAmount) !== 0 ?
      Number((this.originalGrandTotal - this.totalPaid - this.discountAmount).toFixed(2)) : 0;
    this.cashTotal = cashTotal >= 0 ? cashTotal : 0;
    document.getElementById('cashpopup').style.width = '300px';
    document.getElementById('Giftcardpopup').style.width = '0';
    document.getElementById('creditcardpopup').style.width = '0';
    document.getElementById('discountpopup').style.width = '0';
  }
  opengiftcard() {
    this.giftCardForm.reset();
    this.isInvalidGiftcard = false;
    if (this.validGiftcard !== undefined) {
      this.validGiftcard.GiftCardDetail[0].BalaceAmount = 0;
    }
    this.balance = 0;
    document.getElementById('Giftcardpopup').style.width = '450px';
    document.getElementById('creditcardpopup').style.width = '0';
    document.getElementById('cashpopup').style.width = '0';
    document.getElementById('discountpopup').style.width = '0';
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
    // if (this.discountService.length > 0) {
    //   this.messageService.showMessage({ severity: 'warning', title: 'Warning', body: 'Discount already applied' });
    //   return;
    // }
    document.getElementById('discountpopup').style.width = '450px';
    document.getElementById('cashpopup').style.width = '0';
    document.getElementById('Giftcardpopup').style.width = '0';
    document.getElementById('creditcardpopup').style.width = '0';
  }
  closediscount() {
    document.getElementById('discountpopup').style.width = '0';
  }
  opencreditcard() {
    const creditTotal = (this.originalGrandTotal - this.totalPaid - this.discountAmount) !== 0 ?
      Number((this.originalGrandTotal - this.totalPaid - this.discountAmount).toFixed(2)) : 0;
    this.creditTotal = creditTotal >= 0 ? creditTotal : 0;
    this.creditcashback = 0;
    this.cashback = this.initialcashback;
    document.getElementById('creditcardpopup').style.width = '300px';
    document.getElementById('Giftcardpopup').style.width = '0';
    document.getElementById('discountpopup').style.width = '0';
    document.getElementById('cashpopup').style.width = '0';
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
        this.getDetailByTicket(false);
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
    this.totalPaid = this.totalPaid - this.giftCard;
    let gc = 0;
    this.giftcards.reduce(item => +item.amount);
    gc = this.giftcards.reduce((accum, item) => accum + (+item.amount), 0);
    this.giftCard = gc;
    this.totalPaid = this.totalPaid + this.giftCard;
    document.getElementById('Giftcardpopup').style.width = '0';
  }
  addItem() {
    this.submitted = true;
    if (+this.addItemForm.controls.quantity.value === 0) {
      this.addItemForm.patchValue({ quantity: '' });
      return;
    }
    if (this.addItemForm.invalid) {
      this.messageService.showMessage({ severity: 'warning', title: 'Warning', body: 'Please enter quantity' });
      return;
    } else if (this.addItemForm.value.itemName === '' || this.filteredItem.length === 0) {
      this.messageService.showMessage({ severity: 'warning', title: 'Warning', body: 'Please enter valid ItemName' });
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
        isDeleted: false,
        createdBy: 1,
        createdDate: new Date(),
        updatedBy: 1,
        updatedDate: new Date(),
        notes: 'checking'
      },
      jobItem: [{
        jobItemId: 0,
        jobId: this.isSelected ? this.JobId : 0,
        serviceId: this.selectedService?.id,
        // itemTypeId: this.selectedService.type === 'product' ? 6 : 3,
        commission: 0,
        price: this.selectedService?.price,
        quantity: +this.addItemForm.controls.quantity.value,
        reviewNote: null,
        isActive: true,
        isDeleted: false,
        createdBy: 1,
        createdDate: new Date(),
        updatedBy: 1,
        updatedDate: new Date(),
        employeeId: +localStorage.getItem('empId')
      }],
      JobProductItem: {
        jobProductItemId: 0,
        jobId: this.isSelected ? this.JobId : 0,
        productId: this.selectedService?.id,
        commission: 0,
        price: this.selectedService?.price,
        quantity: +this.addItemForm.controls.quantity.value,
        reviewNote: null,
        isActive: true,
        isDeleted: false,
        createdBy: 1,
        createdDate: new Date(),
        updatedBy: 1,
        updatedDate: new Date()
      }
    };
    if (this.selectedService.type === 'service') {
      formObj.JobProductItem = null;
    } else {
      formObj.jobItem = null;
    }
    if (this.isSelected) {
      this.updateListItem(formObj, false);
    } else {
      this.salesService.addItem(formObj).subscribe(data => {
        if (data.status === 'Success') {
          this.messageService.showMessage({ severity: 'success', title: 'Success', body: 'Item added successfully' });
          this.isSelected = true;
          this.ticketNumber = this.newTicketNumber;
          this.getDetailByTicket(false);
          this.addItemForm.controls.quantity.enable();
          this.addItemFormInit();
          this.submitted = false;
        } else {
          this.messageService.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
        }
      });
    }
  }
  updateListItem(formObj, flag) {
    this.salesService.updateListItem(formObj).subscribe(data => {
      if (data.status === 'Success') {
        this.messageService.showMessage({ severity: 'success', title: 'Success', body: 'Item added successfully' });
        this.getDetailByTicket(flag);
        this.addItemForm.controls.quantity.enable();
        this.addItemFormInit();
        this.submitted = false;
      } else {
        this.messageService.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
      }
    });
  }
  getNumAndUpdate(num) {
    if (this.targetId !== '') {
      const el = document.getElementById(this.targetId);
      insertTextAtCursor(el, num.toString());
    }
    // this.addItemForm.patchValue({ quantity: this.addItemForm.value.quantity.toString() + num.toString() });
  }
  clear() {
    if (this.targetId === 'quantity') {
      this.addItemForm.patchValue({ quantity: '' });
    } else if (this.targetId === 'ticketNumber') {
      this.ticketNumber = '';
      this.clearpaymentField();
      this.clearGridItems();
    }
  }
  backspace() {
    if (this.targetId === 'quantity') {
      const quantity = this.addItemForm.value.quantity.toString();
      this.addItemForm.patchValue({ quantity: quantity.substring(0, quantity.length - 1) });
    } else if (this.targetId === 'ticketNumber') {
      const ticketNumber = this.ticketNumber ? this.ticketNumber.toString() : '';
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
    this.ticketNumber = '';
    this.salesService.getTicketNumber().subscribe(data => {
      this.newTicketNumber = data;
      this.enableAdd = true;
      this.clearpaymentField();
      this.clearGridItems();
    });
  }
  creditProcess() {
    this.totalPaid = this.totalPaid - this.credit;
    this.credit = this.creditTotal - this.creditcashback;
    if (this.credit > (this.originalGrandTotal - this.totalPaid - this.discountAmount + this.credit)) {
      this.credit = 0;
      this.creditcashback = 0;
      this.messageService.showMessage({ severity: 'warning', title: 'Warning', body: 'Credit amount exceeds the balance amount!' });
      return;
    }
    this.totalPaid = this.totalPaid + this.credit;
    this.cashback = this.cashback + this.creditcashback;
    document.getElementById('creditcardpopup').style.width = '0';
  }
  addCash(cash) {
    this.cashTotal = +this.cashTotal + cash;
  }
  cashProcess() {
    this.totalPaid = (+this.totalPaid) - (+this.cash);
    this.cash = this.cashTotal;
    this.totalPaid = (+this.totalPaid) + (+this.cash);
    document.getElementById('cashpopup').style.width = '0';
  }
  discountProcess() {

    let discountValue = 0;
    this.selectedDiscount.reduce(item => +item.amount);
    discountValue = this.selectedDiscount.reduce((accum, item) => accum + (+item.Cost), 0);
    this.discountAmount = discountValue;
    //this.updateListItem(formObj, false);
    document.getElementById('discountpopup').style.width = '0';
  }
  discountChange(event) {
    console.log(this.discount);
    if (this.selectedDiscount.length > 0) {
    const dup = this.selectedDiscount.filter(item => +item.ServiceId === +this.discount);
    if (dup.length > 0) {
      this.messageService.showMessage({ severity: 'warning', title: 'Warning', body: 'Duplicate discount' });
      return;
    }
  }
    if (this.discountService.length > 0) {
      const duplicatecheck = this.discountService.filter(selectedDis => +selectedDis.ServiceId === +this.discount);
      if (duplicatecheck.length > 0) {
        this.messageService.showMessage({ severity: 'warning', title: 'Warning', body: 'selected discount already applied!' });
        return;
      }
    }
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
    const balancedue = this.originalGrandTotal - this.totalPaid - this.discountAmount;
    if (this.cash === 0 && this.credit === 0 && this.giftCard === 0) {
      this.messageService.showMessage({ severity: 'warning', title: 'Warning', body: 'Add any cash/credit payment and proceed' });
      return;
    }
    if (balancedue !== 0) {
      this.messageService.showMessage({ severity: 'warning', title: 'Warning', body: 'Total paid amount not matching with Total amount.' });
      return;
    }
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
        isDeleted: false,
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
        isDeleted: false,
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
        cashback: this.cashback ? this.cashback : 0,
        approval: true,
        checkNumber: '',
        signature: '',
        paymentStatus: 1,
        comments: '',
        isActive: true,
        isDeleted: false,
        createdBy: 1,
        createdDate: new Date(),
        updatedBy: 1,
        updatedDate: new Date(),
        isProcessed: true
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
      jobPaymentDiscount: discount.length === 0 ? null : discount,

    };
    this.spinner.show();
    this.salesService.addPayemnt(paymentObj).subscribe(data => {
      this.spinner.hide();
      if (data.status === 'Success') {
        this.messageService.showMessage({ severity: 'success', title: 'Success', body: 'Payment completed successfully' });
        this.getDetailByTicket(false);
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
          this.messageService.showMessage({ severity: 'success', title: 'Success', body: 'Job deleted successfully' });
          this.getDetailByTicket(false);
          this.ticketNumber = '';
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
        if (this.validGiftcard?.GiftCardDetail[0]?.GiftCardId === 0) {
          this.isInvalidGiftcard = true;
        } else {
          this.isInvalidGiftcard = false;
        }
      }
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
          this.getDetailByTicket(false);
          this.messageService.showMessage({ severity: 'success', title: 'Success', body: 'Rollbacked Successfully' });
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
  allowNumbersOnly(e) {
    const code = (e.which) ? e.which : e.keyCode;
    if (code > 31 && (code < 48 || code > 57)) {
      e.preventDefault();
    }
  }
}
