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
import { Router } from '@angular/router';
import insertTextAtCursor from 'insert-text-at-cursor';
import { NgxSpinnerService } from 'ngx-spinner';
import { ActivatedRoute } from '@angular/router';
import { PrintComponent } from './print/print.component';
import { element } from 'protractor';
import { GetCodeService } from 'src/app/shared/services/data-service/getcode.service';
@Component({
  selector: 'app-sales',
  templateUrl: './sales.component.html',
  styleUrls: ['./sales.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class SalesComponent implements OnInit {
  services: any;
  paymentStatusId: number;
  validGiftcard: any;
  targetId = '';
  enableButton = true;
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
  PaymentType: any;
  PaymentStatus: any;
  accountDetails: any;
  isAccount: any;
  constructor(private membershipService: MembershipService, private salesService: SalesService, private router: Router,
    private confirmationService: ConfirmationUXBDialogService, private modalService: NgbModal, private fb: FormBuilder,
    private messageService: MessageServiceToastr, private service: ServiceSetupService,
    private giftcardService: GiftCardService, private spinner: NgxSpinnerService,
    private route: ActivatedRoute, private codes: GetCodeService) { }
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
  paymentStatus: any = [];
  multipleTicketNumber = [];
  isTenTicketNumber: boolean;
  ngOnInit(): void {
    this.isTenTicketNumber = false;
    this.giftCardFromInit();
    this.addItemFormInit();
    const paramsData = this.route.snapshot.queryParamMap.get('ticketNumber');
    if (paramsData !== null) {
      this.ticketNumber = paramsData;
      this.getDetailByTicket(false);
    }
    this.getPaymentType();
    this.getPaymentStatus();
    this.getServiceForDiscount();
    this.getAllServiceandProduct();
  }

  getPaymentType() {
    this.codes.getCodeByCategory("PAYMENTTYPE").subscribe(data => {
      if (data.status === 'Success') {
        const sType = JSON.parse(data.resultData);
        this.PaymentType = sType.Codes;
      } else {
        this.messageService.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
      }
    });
  }

  getPaymentStatus() {
    this.codes.getCodeByCategory("PAYMENTSTATUS").subscribe(data => {
      if (data.status === 'Success') {
        const sType = JSON.parse(data.resultData);
        this.PaymentStatus = sType.Codes;
      } else {
        this.messageService.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
      }
    });
  }

  getAllServiceandProduct() {
    this.salesService.getServiceAndProduct().subscribe(data => {
      if (data.status === 'Success') {
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
    if (this.itemList?.Status?.SalesSummaryViewModel) {
      this.itemList.Status.SalesSummaryViewModel = {};
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
    this.discountAmount = 0;
    this.selectedDiscount = [];
    this.selectedService = [];
  }

  addTicketNumber() {
    const alreadyAdded = this.multipleTicketNumber.filter(item => item === this.ticketNumber);
    if (alreadyAdded.length === 0) {
      this.multipleTicketNumber.push(this.ticketNumber);
      this.ticketNumber = '';
    } else {
      this.ticketNumber = '';
      this.messageService.showMessage({ severity: 'info', title: 'Infor', body: 'Ticket Already Added' });
    }

    if (this.multipleTicketNumber.length > 10) {
      this.isTenTicketNumber = true;
    } else {
      this.isTenTicketNumber = false;
    }
    this.getDetailByTicket(false);
  }

  removeTicketNumber(ticket) {
    this.multipleTicketNumber = this.multipleTicketNumber.filter(item => item !== ticket);
    if (this.multipleTicketNumber.length > 10) {
      this.isTenTicketNumber = true;
    } else {
      this.isTenTicketNumber = false;
    }
    this.getDetailByTicket(false);
  }
  getDetailByTicket(flag) {
    this.enableButton = false;
    if (flag !== true) {
      this.clearGridItems();
      this.clearpaymentField();
    } else {
      this.clearGridItems();
    }
    if ((this.multipleTicketNumber.length > 0) ||
      (this.newTicketNumber !== undefined && this.newTicketNumber !== '')) {
      const ticketNumber = this.multipleTicketNumber.length > 0 ? this.multipleTicketNumber.toString()
        : this.newTicketNumber ? this.newTicketNumber : 0;
      const obj = {
        ticketNumber
      };
      this.salesService.getAccountDetails(obj).subscribe(data => {
        if (data.status === 'Success') {
          const accountDetails = JSON.parse(data.resultData);
          this.accountDetails = accountDetails.Account[0];
          this.isAccount = this.accountDetails?.CodeValue !== 'Comp' ? this.accountDetails?.IsAccount : false;
          console.log(this.accountDetails);
        }
      });
      this.spinner.show();
      this.salesService.getItemByTicketNumber(ticketNumber).subscribe(data => {
        this.spinner.hide();
        if (data.status === 'Success') {
          this.enableAdd = true;
          this.itemList = JSON.parse(data.resultData);
          console.log(this.itemList, 'item');
          // if (this.itemList.Status.PaymentStatusViewModel === null) {
          //   this.messageService.showMessage({ severity: 'error', title: 'Error', body: 'Invalid Ticket Number' });
          //   return;
          // } else {
          //   this.JobId = this.itemList?.Status?.PaymentStatusViewModel?.JobId;
          // }
          if (this.itemList.Status.SalesItemViewModel !== null) {
            if (this.itemList.Status.SalesItemViewModel.length !== 0) {
              this.showPopup = true;
              this.washes = this.itemList.Status.SalesItemViewModel.filter(item => item.ServiceType === 'Washes');
              this.details = this.itemList.Status.SalesItemViewModel.filter(item => item.ServiceType === 'Details');
              this.additionalService = this.itemList.Status.SalesItemViewModel.filter(item =>
                item.ServiceType === 'Additional Services');
              this.upCharges = this.itemList.Status.SalesItemViewModel.filter(item =>
                item.ServiceType === 'Upcharges');
              this.outsideServices = this.itemList.Status.SalesItemViewModel.filter(item =>
                item.ServiceType === 'Outside Services');
              this.airfreshnerService = this.itemList.Status.SalesItemViewModel.filter(item =>
                item.ServiceType === 'Air Fresheners');
              this.discountService = this.itemList.Status.SalesItemViewModel.filter(item =>
                item.ServiceType === 'Discounts');
              console.log(this.washes);
            }
          } else {
            this.showPopup = false;
          }
          if (this.itemList?.Status?.SalesSummaryViewModel !== null) {
            const summary = this.itemList?.Status?.SalesSummaryViewModel;
            this.initialcashback = summary?.Cashback ? summary?.Cashback : 0;
            this.cashback = summary?.CashBack ? summary?.CashBack : 0;
            this.grandTotal = summary?.GrandTotal ? summary?.GrandTotal : summary?.Total ? (summary?.Total + summary?.Tax) : 0;
            this.cashTotal = +this.grandTotal;
            this.creditTotal = +this.grandTotal;
            this.cash = +summary?.Cash;
            this.account = +summary?.Account;
            this.credit = +summary?.Credit;
            this.discountAmount = summary?.Discount;
            this.originalGrandTotal = +this.grandTotal;
            this.giftCard = Math.abs(+summary?.GiftCard);
            this.balance = +summary?.Balance;
            this.totalPaid = +summary?.TotalPaid;
            if (+this.account === 0.00) {
              this.account = this.accountDetails?.IsAccount === true && this.accountDetails?.CodeValue === 'Comp' ? +this.grandTotal : 0;
              this.calculateTotalpaid(+this.account);
            }
          }
          if (this.itemList?.Status?.ProductItemViewModel !== null && this.itemList?.Status?.ProductItemViewModel !== undefined) {
            this.Products = this.itemList?.Status?.ProductItemViewModel;
          }
          if (this.itemList?.Status?.PaymentStatusViewModel?.IsProcessed === true) {
            this.showPopup = false;
            this.enableButton = true;
          } else {
            this.showPopup = true;
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
    this.confirmationService.confirm(title, message, 'Yes', 'No', '', '500px')
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
    const deleteItem = {
      ItemId: itemId,
      IsJobItem: data?.JobItemId ? true : false
    };
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
    const cashTotal = this.cash !== 0 ? this.cash : this.getBalanceDue();
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
    const creditTotal = this.credit !== 0 ? this.credit : this.getBalanceDue();
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
      this.giftCardForm.reset();
      this.balance = 0;
      this.validGiftcard.GiftCardDetail[0].BalaceAmount = 0;
      this.giftcardsubmitted = false;
    } else {
      return;
    }
  }
  giftCardProcess() {
    this.removAddedAmount(this.giftCard);
    let gc = 0;
    this.giftcards.reduce(item => +item.amount);
    gc = this.giftcards.reduce((accum, item) => accum + (+item.amount), 0);
    this.giftCard = gc;
    this.calculateTotalpaid(this.giftCard);
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
        clientId: null,
        vehicleId: null,
        make: 0,
        model: 0,
        color: 0,
        jobType: null,
        jobDate: new Date(),
        timeIn: new Date(),
        estimatedTimeOut: new Date(),
        actualTimeOut: new Date(),
        jobStatus: null,
        isActive: true,
        isDeleted: false,
        createdBy: null,
        createdDate: new Date(),
        updatedBy: null,
        updatedDate: new Date(),
        notes: null
      },
      jobItem: [{
        jobItemId: 0,
        jobId: this.isSelected ? this.JobId : 0,
        serviceId: this.selectedService?.id,
        // itemTypeId: this.selectedService.type === 'product' ? 6 : 3,
        commission: null,
        price: this.selectedService?.price,
        quantity: +this.addItemForm.controls.quantity.value,
        reviewNote: null,
        isActive: true,
        isDeleted: false,
        createdBy: null,
        createdDate: new Date(),
        updatedBy: null,
        updatedDate: new Date(),
        employeeId: +localStorage.getItem('empId')
      }],
      JobProductItem: {
        jobProductItemId: 0,
        jobId: this.isSelected ? this.JobId : 0,
        productId: this.selectedService?.id,
        commission: null,
        price: this.selectedService?.price,
        quantity: +this.addItemForm.controls.quantity.value,
        reviewNote: null,
        isActive: true,
        isDeleted: false,
        createdBy: null,
        createdDate: new Date(),
        updatedBy: null,
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
    this.removAddedAmount(this.credit);
    this.credit = this.creditTotal - this.creditcashback;
    if (this.credit > (this.originalGrandTotal - this.totalPaid - this.discountAmount + this.credit)) {
      this.credit = 0;
      this.creditcashback = 0;
      this.messageService.showMessage({ severity: 'warning', title: 'Warning', body: 'Credit amount exceeds the balance amount!' });
      return;
    }
    this.calculateTotalpaid(this.credit);
    this.cashback = this.cashback + this.creditcashback;
    document.getElementById('creditcardpopup').style.width = '0';
  }
  removAddedAmount(amount) {
    this.totalPaid = this.totalPaid - amount;
  }
  calculateTotalpaid(amount) {
    this.totalPaid = this.totalPaid + amount;
  }
  addCash(cash) {
    this.cashTotal = +this.cashTotal + cash;
  }
  cashProcess() {
    this.removAddedAmount(+this.cash);
    this.cash = this.cashTotal;
    this.calculateTotalpaid(+this.cash);
    document.getElementById('cashpopup').style.width = '0';
  }
  discountProcess() {

    let discountValue = 0;
    if (this.selectedDiscount.length > 0) {
      discountValue = this.selectedDiscount.reduce((accum, item) => accum + (+item.Cost), 0);
      this.discountAmount = discountValue;
    } else {
      this.discountAmount = 0;
    }
    //this.updateListItem(formObj, false);
    document.getElementById('discountpopup').style.width = '0';
  }
  discountChange(event) {
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
  // getPaymentStatus() {
  //   this.salesService.getPaymentStatus('PAYMENTSTATUS').subscribe(res => {
  //     if (res.status === 'Success') {
  //       const status = JSON.parse(res.resultData);
  //       this.paymentStatus = status.Codes.filter(item => item.CodeValue === 'Success');
  //       this.paymentStatusId = this.paymentStatus[0].CodeId;
  //     }
  //   });
  // }
  deletediscount(event) {
    const index = this.selectedDiscount.findIndex(item => item.ServiceId === +event.ServiceId);
    this.selectedDiscount.splice(index, 1);
    let discountAmount = 0;
    this.selectedDiscount.forEach(item => {
      discountAmount = discountAmount + (+item.Cost);
    });
    this.discountAmount = discountAmount;
  }
  getBalanceDue() {
    const balancedue = (this.originalGrandTotal - this.totalPaid - this.discountAmount) !== 0 ?
      Number((this.originalGrandTotal - this.totalPaid - this.discountAmount).toFixed(2)) : 0;
    return balancedue;
  }
  addPayment() {
    let paymentDetailObj = [];
    const balancedue = this.getBalanceDue();
    if (this.cash === 0 && this.credit === 0 && this.giftCard === 0 && this.account === 0) {
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
        comments: null,
        isActive: true,
        isDeleted: false,
        createdBy: 1,
        createdDate: new Date(),
        updatedBy: 1,
        updatedDate: new Date(),
        jobPaymentId: 0
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
    let discountPayType = this.PaymentType.filter(i => i.CodeValue === "Discount")[0].CodeId;
    const discountDet = this.selectedDiscount.map(item => {
      return {
        jobPaymentDetailId: 0,
        jobPaymentId: 0,
        paymentType: discountPayType,
        amount: item.Cost,
        taxAmount: 0,
        signature: '',
        isActive: true,
        isDeleted: false,
        createdBy: null,
        createdDate: new Date(),
        updatedBy: null,
        updatedDate: new Date()
      }
    });
    discountDet.forEach(element => {
      paymentDetailObj.push(element);
    })
    if (this.cash !== 0) {
      let cashPayType = this.PaymentType.filter(i => i.CodeValue === "Cash")[0].CodeId;
      const det = {
        jobPaymentDetailId: 0,
        jobPaymentId: 0,
        paymentType: cashPayType,
        amount: this.cash ? +this.cash : 0,
        taxAmount: 0,
        signature: '',
        isActive: true,
        isDeleted: false,
        createdBy: null,
        createdDate: new Date(),
        updatedBy: null,
        updatedDate: new Date()
      };
      paymentDetailObj.push(det);
    }
    if (this.account !== 0) {
      let accountPayType = this.PaymentType.filter(i => i.CodeValue === "Account")[0].CodeId;
      if (this.accountDetails?.CodeValue !== "Comp") {
        accountPayType = this.PaymentType.filter(i => i.CodeValue === "Membership")[0].CodeId;
      }
      const accountDet = {
        jobPaymentDetailId: 0,
        jobPaymentId: 0,
        paymentType: accountPayType,
        amount: this.account ? +this.account : 0,
        taxAmount: 0,
        signature: '',
        isActive: true,
        isDeleted: false,
        createdBy: null,
        createdDate: new Date(),
        updatedBy: null,
        updatedDate: new Date()
      };
      paymentDetailObj.push(accountDet);
    }
    if (this.credit !== 0) {
      let creditPayType = this.PaymentType.filter(i => i.CodeValue === "Credit")[0].CodeId;
      const credit = {
        jobPaymentDetailId: 0,
        jobPaymentId: 0,
        paymentType: creditPayType,
        amount: this.credit ? +this.credit : 0,
        taxAmount: 0,
        signature: '',
        isActive: true,
        isDeleted: false,
        createdBy: null,
        createdDate: new Date(),
        updatedBy: null,
        updatedDate: new Date()
      };
      paymentDetailObj.push(credit);
    }
    if (this.giftCard !== 0) {
      let giftPayType = this.PaymentType.filter(i => i.CodeValue === "GiftCard")[0].CodeId;
      const gift = {
        jobPaymentDetailId: 0,
        jobPaymentId: 0,
        paymentType: giftPayType,
        amount: this.giftCard ? +this.giftCard : 0,
        taxAmount: 0,
        signature: '',
        isActive: true,
        isDeleted: false,
        createdBy: null,
        createdDate: new Date(),
        updatedBy: null,
        updatedDate: new Date()
      };
      paymentDetailObj.push(gift);
    }
    const paymentObj = {
      jobPayment: {
        jobPaymentId: 0,
        membershipId: this.accountDetails !== undefined ? this.accountDetails?.MembershipId : null,
        jobId: this.isSelected ? +this.JobId : 0,
        drawerId: +localStorage.getItem('drawerId'),
        amount: this.cash ? +this.cash : 0,
        taxAmount: 0,
        approval: true,
        paymentStatus: +this.PaymentStatus.filter(i => i.CodeValue === 'Success')[0].CodeId,
        comments: '',
        isActive: true,
        isDeleted: false,
        createdBy: 1,
        createdDate: new Date(),
        updatedBy: 1,
        updatedDate: new Date(),
        isProcessed: true
      },
      jobPaymentDetail: paymentDetailObj,
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
        createdBy: null,
        createdDate: new Date(),
        updatedBy: null,
        updatedDate: new Date()
      },
      //jobPaymentDiscount: discount.length === 0 ? null : discount,

    };
    this.spinner.show();
    this.salesService.addPayemnt(paymentObj).subscribe(data => {
      this.spinner.hide();
      if (data.status === 'Success') {
        if (this.accountDetails !== null && this.accountDetails?.CodeValue === "Comp") {
          const amt = (+this.accountDetails?.Amount.toFixed(2) - +this.account.toFixed(2)).toFixed(2);
          const obj = {
            clientId: this.accountDetails?.ClientId,
            amount: amt
          }
          this.salesService.updateAccountBalance(obj).subscribe(data => {
          });
        }
        this.messageService.showMessage({ severity: 'success', title: 'Success', body: 'Payment completed successfully' });
        this.getDetailByTicket(false);
        this.router.navigate([`/checkout`], { relativeTo: this.route });
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
          this.router.navigate([`/checkout`], { relativeTo: this.route });
        } else {
          this.messageService.showMessage({ severity: 'error', title: 'Error', body: 'Communication error' });
        }
      }, (err) => {
        this.messageService.showMessage({ severity: 'error', title: 'Error', body: 'Communication error' });
      });
    }
  }

  processAccount() {
    if (this.isAccount) {
      this.removAddedAmount(+this.account);
      this.account = +this.washes[0].Price;
      this.calculateTotalpaid(+this.account);
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
  print() {
    const ngbModalOptions: NgbModalOptions = {
      backdrop: 'static',
      keyboard: false,
      size: 'lg'
    };
    const modalRef = this.modalService.open(PrintComponent, ngbModalOptions);
    modalRef.componentInstance.isModal = true;
    modalRef.componentInstance.ticketNumber = this.ticketNumber;
    modalRef.componentInstance.itemList = this.itemList.Status;
  }
}
