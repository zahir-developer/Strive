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
import { SaleGiftCardComponent } from './sale-gift-card/sale-gift-card.component';
import { ApplicationConfig } from 'src/app/shared/services/ApplicationConfig';
import { MessageConfig } from 'src/app/shared/services/messageConfig';
import { PaymentProcessComponent } from 'src/app/shared/components/payment-process/payment-process.component';
import { DecimalPipe } from '@angular/common';
import { CodeValueService } from 'src/app/shared/common-service/code-value.service';
import { ToastrComponentlessModule, ToastrService } from 'ngx-toastr';
import { LocationService } from 'src/app/shared/services/data-service/location.service';
import { VehicleService } from 'src/app/shared/services/data-service/vehicle.service';

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
  tips = 0;
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
  JobId: number;
  newTicketNumber: any = '';
  selectedService: any;
  balance: number;
  PaymentType: any;
  PaymentStatus: any;
  accountDetails: any;
  isAccount: any;
  isMembership: boolean;
  discountList: any = [];
  isAccountButton = false;
  ticketNumberGeneration: boolean;
  allService = [];
  clientId: number;
  giftCardList = [];
  giftCardID: any;
  multipleTicketSequence: boolean = false;
  printTicketNumber: string;
  jobTypeId: number;
  captureObj: any = {};
  isDiscountAdded: boolean;
  addedDiscount = [];
  isMultipleTicket: boolean;
  merchantUserName: any = '';
  password: any = '';
  isValidMember: boolean = false;
  totalAmount = 0;
  filterRecord = [];
  totalChargeService: any;
  washPackage: any;

  constructor(
    private toastr: ToastrService, private membershipService: MembershipService, private salesService: SalesService, private router: Router,
    private confirmationService: ConfirmationUXBDialogService, private modalService: NgbModal, private fb: FormBuilder,
    private messageService: MessageServiceToastr, private service: ServiceSetupService,
    private giftcardService: GiftCardService, private spinner: NgxSpinnerService,
    private locationService: LocationService, private vehicle: VehicleService,
    private route: ActivatedRoute, private codes: GetCodeService, private decimalPipe: DecimalPipe, private codeService: CodeValueService) { }
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
  serviceType: any = [];
  locationId: number;
  isCreditPay: boolean;
  isGiftCard: boolean;
  giftcardNumber: any = '';
  totalWashService: any;
  vehicleIds: any;
  accountEnable = false;

  ngOnInit(): void {
    this.isMultipleTicket = false;
    this.isTenTicketNumber = false;
    this.isCreditPay = false;
    this.isGiftCard = false;
    this.locationId = +localStorage.getItem('empLocationId');
    this.isDiscountAdded = false;
    this.giftCardFromInit();
    this.addItemFormInit();
    const paramsData = this.route.snapshot.queryParamMap.get('ticketNumber');
    if (paramsData !== null) {
      this.ticketNumber = paramsData;
      this.printTicketNumber = paramsData;
      this.addTicketNumber();
    }
    this.getServiceType();
    this.getPaymentType();
    this.getPaymentStatus();
    this.getServiceForDiscount();
    // this.getAllServiceandProduct();
    this.getJobType();

  }
  print() {
    const ngbModalOptions: NgbModalOptions = {
      backdrop: 'static',
      keyboard: false,
      size: 'lg'
    };
    const modalRef = this.modalService.open(PrintComponent, ngbModalOptions);
    modalRef.componentInstance.isModal = true;
    modalRef.componentInstance.printTicketNumber = this.printTicketNumber;
    modalRef.componentInstance.itemList = this.itemList.Status;
  }
  getServiceType() {
    const serviceTypeValue = this.codeService.getCodeValueByType(ApplicationConfig.CodeValue.serviceType);
    if (serviceTypeValue.length > 0) {
      this.serviceType = serviceTypeValue;
    } else {
      this.codes.getCodeByCategory(ApplicationConfig.Category.serviceType).subscribe(res => {
        if (res.status === 'Success') {
          const sType = JSON.parse(res.resultData);
          this.serviceType = sType.Codes;
        } else {
          this.messageService.showMessage({ severity: 'error', title: 'Error', body: MessageConfig.CommunicationError });
        }
      });
    }
  }


  getPaymentType() {
    const paymentTypeValue = this.codeService.getCodeValueByType(ApplicationConfig.CodeValue.paymentType);
    if (paymentTypeValue.length > 0) {
      this.PaymentType = paymentTypeValue;
    } else {
      this.codes.getCodeByCategory(ApplicationConfig.Category.paymentType).subscribe(data => {
        if (data.status === 'Success') {
          const sType = JSON.parse(data.resultData);
          this.PaymentType = sType.Codes;
        } else {
          this.messageService.showMessage({ severity: 'error', title: 'Error', body: MessageConfig.CommunicationError });
        }
      });
    }
  }

  getPaymentStatus() {
    const paymentstatusValue = this.codeService.getCodeValueByType(ApplicationConfig.CodeValue.paymentStatus);
    if (paymentstatusValue.length > 0) {
      this.PaymentStatus = paymentstatusValue;
    } else {
      this.codes.getCodeByCategory(ApplicationConfig.Category.paymentStatus).subscribe(data => {
        if (data.status === 'Success') {
          const sType = JSON.parse(data.resultData);
          this.PaymentStatus = sType.Codes;
        } else {
          this.messageService.showMessage({ severity: 'error', title: 'Error', body: MessageConfig.CommunicationError });
        }
      });
    }
  }

  getAllServiceandProduct() {
    const locID = this.locationId;
    this.salesService.getServiceAndProduct(locID, '').subscribe(data => {
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
    const serviceObj = {
      locationId: this.locationId,
      pageNo: null,
      pageSize: null,
      query: null,
      sortOrder: null,
      sortBy: null,
      status: null
    };
    this.service.getAllServiceDetail(this.locationId).subscribe(data => {
      if (data.status === 'Success') {
        const services = JSON.parse(data.resultData);
        if (services.AllServiceDetail !== null) {
          this.discounts = services.AllServiceDetail.filter(item =>
            item.ServiceTypeName === ApplicationConfig.Enum.ServiceType.ServiceDiscounts);
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
    this.allService = [];
    this.Products = [];
    this.selectedDiscount = [];
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
    this.selectedService = [];
  }

  addTicketNumber() {
    if (this.ticketNumber === '') {
      return;
    }
    const alreadyAdded = this.multipleTicketNumber.filter(item => item === this.ticketNumber);
    if (alreadyAdded.length === 0) {
      this.multipleTicketNumber.push(this.ticketNumber);
      this.printTicketNumber = this.ticketNumber;
      this.ticketNumber = '';
    } else {
      this.ticketNumber = '';
      this.messageService.showMessage({ severity: 'info', title: 'Information', body: MessageConfig.Sales.Ticket });
      return;
    }

    if (this.multipleTicketNumber.length > 10) {
      this.isTenTicketNumber = true;
    } else {
      this.isTenTicketNumber = false;
    }
    this.getDetailByTicket(false);
  }

  removeTicketNumber(ticket) {
    this.newTicketNumber = '';
    if (this.multipleTicketNumber.length > 1) {
      this.multipleTicketSequence = false;
    }
    this.multipleTicketNumber = this.multipleTicketNumber.filter(item => item !== ticket);
    if (this.multipleTicketNumber.length > 10) {
      this.isTenTicketNumber = true;
    } else {
      this.isTenTicketNumber = false;
    }
    if (this.multipleTicketNumber.length === 0) {
      this.discountList = [];
      this.enableAdd = false;
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
          this.vehicleIds = accountDetails.Account.SalesAccountViewModel?.VehicleId
          this.accountDetails = accountDetails.Account;
          this.clientId = this.accountDetails.SalesAccountViewModel?.ClientId ? this.accountDetails.SalesAccountViewModel?.ClientId : 0;
          if (this.accountDetails.SalesAccountCreditViewModel?.IsCreditAccount) {
            this.isAccount = true;
            this.accountEnable = true;
            this.isMembership = false;
          }
          else if (this.accountDetails.SalesAccountViewModel?.MembershipId !== 0 || this.accountDetails.SalesAccountViewModel?.MembershipId !== null) {
            this.isAccount = true;
            this.accountEnable = false;
            this.isMembership = true
          }
        }
      });
      this.spinner.show();
      const salesObj =
      {
        TicketNumber: ticketNumber,
        LocationId: this.locationId
      };
      this.giftCardList = [];
      this.washes = [];
      this.details = [];
      this.additionalService = [];
      this.upCharges = [];
      this.outsideServices = [];
      this.discountService = [];
      this.airfreshnerService = [];
      this.discountList = [];
      this.salesService.getItemByTicketNumber(salesObj).subscribe(data => {
        if (data.status === 'Success') {
          this.spinner.hide();
          this.enableAdd = true;
          this.itemList = JSON.parse(data.resultData);
          if (this.itemList.Status.SalesItemViewModel !== null) {
            const jobDetail = this.itemList.Status.JobDetailViewModel;
            const invalidTicket = jobDetail.filter(item => item.JobId === +this.multipleTicketNumber[this.multipleTicketNumber.length - 1]);
            if (invalidTicket.length === 0) {
              // this.removeTicketNumber(this.multipleTicketNumber[this.multipleTicketNumber.length - 1]);
              this.multipleTicketNumber = this.multipleTicketNumber.filter(item =>
                item !== this.multipleTicketNumber[this.multipleTicketNumber.length - 1]);
              this.messageService.showMessage({ severity: 'error', title: 'Error', body: MessageConfig.Sales.InvalidTicket });
              this.showPopup = false;
            }
            if (this.multipleTicketNumber.length > 1) {
              this.multipleTicketSequence = true;
            }
            if (this.itemList.Status.SalesItemViewModel.length !== 0) {
              this.showPopup = true;
              this.allService = this.itemList.Status.SalesItemViewModel;
              this.washes = this.itemList.Status.SalesItemViewModel.filter(item =>
                item.ServiceType === ApplicationConfig.Enum.ServiceType.WashPackage
              );

              const washPackageList = this.itemList.Status.SalesItemViewModel.filter(item =>
                item.ServiceType === ApplicationConfig.Enum.ServiceType.WashPackage
              );
              this.washPackage = washPackageList;
              const washPackageGroup = this.itemList.Status.SalesItemViewModel.filter(item =>
                item.ServiceType === ApplicationConfig.Enum.ServiceType.WashUpcharge
              );
              this.totalChargeService = washPackageGroup;
              const servicePackage = this.itemList.Status.SalesItemViewModel.filter(item =>
                item.ServiceType === ApplicationConfig.Enum.ServiceType.AdditonalServices
              );
              this.totalWashService = servicePackage;
              this.details = this.itemList.Status.SalesItemViewModel.filter(item =>
                item.ServiceType === ApplicationConfig.Enum.ServiceType.DetailPackage);
              this.additionalService = this.itemList.Status.SalesItemViewModel.filter(item =>
                item.ServiceType === ApplicationConfig.Enum.ServiceType.AdditonalServices);
              this.upCharges = this.itemList.Status.SalesItemViewModel.filter(item =>
                item.ServiceType === ApplicationConfig.Enum.ServiceType.WashUpcharge ||
                item.ServiceType === ApplicationConfig.Enum.ServiceType.DetailUpcharge);
              this.outsideServices = this.itemList.Status.SalesItemViewModel.filter(item =>
                item.ServiceType === ApplicationConfig.Enum.ServiceType.OutsideServices);
              this.airfreshnerService = this.itemList.Status.SalesItemViewModel.filter(item =>
                item.ServiceType === ApplicationConfig.Enum.ServiceType.AirFresheners);
              this.discountService = this.itemList.Status.SalesItemViewModel.filter(item =>
                item.ServiceType === ApplicationConfig.Enum.ServiceType.ServiceDiscounts);
              this.giftCardList = this.itemList.Status.SalesItemViewModel.filter(item =>
                item.ServiceType === ApplicationConfig.Enum.ServiceType.GiftCard);
              this.itemList.Status.SalesItemViewModel.map(item => {
                if (item.ServiceType === ApplicationConfig.Enum.ServiceType.WashUpcharge) {

                }
              });
            }
          } else {
            // this.removeTicketNumber(this.multipleTicketNumber[this.multipleTicketNumber.length - 1]);
            this.multipleTicketNumber = this.multipleTicketNumber.filter(item =>
              item !== this.multipleTicketNumber[this.multipleTicketNumber.length - 1]);
            this.messageService.showMessage({ severity: 'error', title: 'Error', body: MessageConfig.Sales.InvalidTicket });
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
            this.tips = +summary?.Tips;
            this.discountAmount = summary?.Discount;
            this.originalGrandTotal = +this.grandTotal;
            this.giftCard = Math.abs(+summary?.GiftCard);
            this.balance = +summary?.Balance;
            this.totalPaid = +summary?.TotalPaid;
            if (+this.account === 0.00) {
              this.account = this.accountDetails?.SalesAccountViewModel?.IsAccount === true &&
                this.accountDetails?.SalesAccountViewModel?.CodeValue === ApplicationConfig.CodeValue.Comp ? +this.grandTotal : 0;
              this.calculateTotalpaid(+this.account);
            }
          }
          if (this.itemList?.Status?.ProductItemViewModel !== null && this.itemList?.Status?.ProductItemViewModel !== undefined) {
            this.Products = this.itemList?.Status?.ProductItemViewModel;
            console.log(this.Products, 'products');
          }
          if (this.itemList?.Status?.PaymentStatusViewModel?.IsProcessed === true) {
            this.showPopup = false;
            this.enableButton = true;
            this.getTicketsByPaymentId(this.itemList?.Status?.PaymentStatusViewModel?.JobPaymentId);
          } else {
            this.showPopup = true;
            this.enableButton = false;
          }
        }
        else {
          this.spinner.hide();
          this.messageService.showMessage({ severity: 'error', title: 'Error', body: MessageConfig.CommunicationError });
        }
      }, (err) => {
        this.enableAdd = false;
        this.spinner.hide();
      });
    }
  }

  getTicketsByPaymentId(id) {
    this.salesService.getTicketsByPaymentId(id).subscribe(res => {
      if (res.status === 'Success') {
        const paymentTicket = JSON.parse(res.resultData);
        if (paymentTicket.TicketsbyJobPaymentId.length >= 2) {
          let count = 0;
          this.multipleTicketNumber.forEach(item => {
            const ticket = paymentTicket.TicketsbyJobPaymentId.filter(jobTicket => jobTicket.TicketNumber === +item);
            if (ticket.length > 0) {
              count = count + 1;
            }
          });
          if (paymentTicket.TicketsbyJobPaymentId.length === count) {
            this.isMultipleTicket = false;
          } else {
            this.isMultipleTicket = true;
          }
        } else {
          this.isMultipleTicket = false;
        }
      }
    });
  }

  clearform() {
    this.cash = this.giftCard = this.credit = 0;
  }
  filterItem(event) {
    const filtered: any[] = [];
    const query = event.query;
    const locID = this.locationId;
    this.salesService.getServiceAndProduct(locID, query).subscribe(res => {
      if (res.status === 'Success') {
        const services = JSON.parse(res.resultData);
        if (services.ServiceAndProductList.Service !== null) {
          this.services = services.ServiceAndProductList.Service.map(item => {
            return {
              id: item.ServiceId,
              name: item.ServiceName.trim(),
              price: item.Cost,
              type: 'service'
            };
          });
        } else {
          this.services = [];
        }
        if (services.ServiceAndProductList.Product !== null) {
          this.products = services.ServiceAndProductList.Product.map(item => {
            return {
              id: item.ProductId,
              name: item.ProductName.trim(),
              price: item.Price,
              type: 'product'
            };
          });
        } else {
          this.products = [];
        }
        this.serviceAndProduct = [];
        this.serviceAndProduct = this.services.concat(this.products);
      }
    });
    // for (const i of this.serviceAndProduct) {
    //   const client = i;
    //   if (client.name.toLowerCase().indexOf(query.toLowerCase()) === 0) {
    //     filtered.push(client);
    //   }
    // }
    // this.filteredItem = filtered;
  }
  deleteItem(data, type) {
    // if (this.enableButton === true) {
    //   return;
    // }
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
    this.spinner.show();
    this.salesService.deleteItemById(deleteItem).subscribe(res => {
      if (res.status === 'Success') {
        this.spinner.hide();
        this.deleteGiftCard();
        this.messageService.showMessage({ severity: 'success', title: 'Success', body: MessageConfig.Sales.ItemDelete });
        this.getDetailByTicket(false);
      } else {
        this.spinner.hide();
        this.messageService.showMessage({ severity: 'error', title: 'Error', body: MessageConfig.CommunicationError });
      }
    }, (err) => {
      this.spinner.hide();
      this.messageService.showMessage({ severity: 'error', title: 'Error', body: MessageConfig.CommunicationError });
    });
  }

  deleteGiftCard() {
    if (this.giftCardID !== undefined) {
      const cardId = this.giftCardID;
      this.giftcardService.deleteGiftCard(cardId).subscribe(res => {
      });
    }
  }

  openCash() {
    const cashTotal = this.cash !== 0 ? this.cash : this.getBalanceDue();
    this.cashTotal = cashTotal >= 0 ? Number(cashTotal.toFixed(2)) : 0;
    document.getElementById('cashpopup').style.width = '300px';
    document.getElementById('Giftcardpopup').style.width = '0';
    document.getElementById('creditcardpopup').style.width = '0';
    document.getElementById('discountpopup').style.width = '0';
  }
  opengiftcard() {
    this.giftCardForm.reset();
    this.isInvalidGiftcard = false;
    if (this.validGiftcard !== undefined) {
      this.validGiftcard.GiftCardDetail[0].BalanceAmount = 0;
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

  openMembershipLogin() {
    document.getElementById('Giftcardpopup').style.width = '0';
    document.getElementById('discountpopup').style.width = '0';
    document.getElementById('cashpopup').style.width = '0';

    if (this.isValidMember == true) {
      document.getElementById('creditcardpopup').style.width = '300px';
      document.getElementById('verifyMembership').style.width = '0';
      this.creditProcess();
    } else {
      document.getElementById('verifyMembership').style.width = '400px';
      // this.ValidateMembership();
    }
  }

  opencreditcard() {
    const creditTotal = this.credit !== 0 ? this.credit : this.getBalanceDue();
    this.creditTotal = creditTotal >= 0 ? creditTotal : 0;
    this.creditcashback = 0;
    this.cashback = this.initialcashback;
    document.getElementById('creditcardpopup').style.width = '300px';
    document.getElementById('verifyMembership').style.width = '0';
    document.getElementById('Giftcardpopup').style.width = '0';
    document.getElementById('discountpopup').style.width = '0';
    document.getElementById('cashpopup').style.width = '0';
  }

  closecreditcard() {
    document.getElementById('creditcardpopup').style.width = '0';
  }

  closeVerifyMembership() {
    document.getElementById('verifyMembership').style.width = '0';
  }

  ValidateMembership() {

    const obj = {
      locationId: this.locationId,
      userName: this.merchantUserName,
      password: this.password
    };

    this.locationService.getMerchantSearch(obj).subscribe(data => {
      if (data.status === 'Success') {
        const membership = JSON.parse(data.resultData);
        if (membership.MerchantValidation.length == 0) {
          this.messageService.showMessage({ severity: 'warning', title: 'Warning', body: MessageConfig.Sales.InValidMember });
        } else {
          this.isValidMember = true;
          document.getElementById('verifyMembership').style.width = '0';
          this.messageService.showMessage({ severity: 'success', title: 'Success', body: MessageConfig.Sales.ValidMember });
          this.creditProcess();
        }
      } else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  editItem(event) {
    if (this.enableButton == true) {
      return
    }
    const itemId = event.JobId;
    const ngbModalOptions: NgbModalOptions = {
      backdrop: 'static',
      keyboard: false,
      size: 'md'
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
      this.validGiftcard.GiftCardDetail[0].BalanceAmount = 0;
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
    // this.originalGrandTotal = this.originalGrandTotal + this.giftCard;
    this.calculateTotalpaid(this.giftCard);
    document.getElementById('Giftcardpopup').style.width = '0';
  }
  addItem() {
    if (this.selectedService.name === 'Gift Card') {
      const ngbModalOptions: NgbModalOptions = {
        backdrop: 'static',
        keyboard: false,
        size: 'lg'
      };
      const productObj = {
        ticketNumber: this.multipleTicketNumber.length > 0 ? this.multipleTicketNumber[0] : this.newTicketNumber,
        quantity: +this.addItemForm.controls.quantity.value,
        selectedService: this.selectedService
      };
      const modalRef = this.modalService.open(SaleGiftCardComponent, ngbModalOptions);
      modalRef.componentInstance.ItemDetail = productObj;
      modalRef.result.then((result) => {
        if (result.status) {
          this.isSelected = true;
          this.ticketNumber = this.newTicketNumber;
          this.giftCardID = result.cardId;
          this.giftcardNumber = result.cardNumber;
          this.getDetailByTicket(false);
          this.addItemForm.controls.quantity.enable();
          this.addItemFormInit();
          this.submitted = false;
        }
      });
    } else {
      this.submitted = true;
      if (+this.addItemForm.controls.quantity.value === 0) {
        this.addItemForm.patchValue({ quantity: '' });
        return;
      }
      if (this.addItemForm.invalid) {
        this.messageService.showMessage({ severity: 'warning', title: 'Warning', body: MessageConfig.Sales.quantity });
        return;
      } else if (this.addItemForm.value.itemName === '' || this.serviceAndProduct.length === 0) {
        this.messageService.showMessage({ severity: 'warning', title: 'Warning', body: MessageConfig.Sales.validItem });
        return;
      }
      const formObj = {
        job: {
          jobId: this.isSelected ? this.itemList.Status.SalesItemViewModel[0].JobId : this.JobId,
          ticketNumber: this.isSelected ? this.itemList.Status.SalesItemViewModel[0].JobId : this.JobId,
          locationId: this.locationId,
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
          jobId: this.isSelected ? this.itemList.Status.SalesItemViewModel[0].JobId : this.JobId,
          serviceId: this.selectedService?.id,
          commission: 0,
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
          jobId: this.isSelected ? this.itemList.Status.SalesItemViewModel[0].JobId : this.JobId,
          productId: this.selectedService?.id,
          commission: 0,
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
        formObj.job.jobType = this.jobTypeId;
      } else {
        formObj.jobItem = null;
      }

      if (this.isSelected) {
        this.updateListItem(formObj, false);
      } else {
        this.salesService.addListItem(formObj).subscribe(data => {
          if (data.status === 'Success') {
            this.messageService.showMessage({ severity: 'success', title: 'Success', body: MessageConfig.Sales.Add });
            this.isSelected = true;
            // this.ticketNumber = this.newTicketNumber;
            this.multipleTicketNumber.push(this.newTicketNumber);
            this.getDetailByTicket(false);
            this.addItemForm.controls.quantity.enable();
            this.addItemFormInit();
            this.submitted = false;
          } else {
            this.messageService.showMessage({ severity: 'error', title: 'Error', body: MessageConfig.CommunicationError });
          }
        });
      }
    }
  }
  updateListItem(formObj, flag) {
    this.salesService.updateListItem(formObj).subscribe(data => {
      if (data.status === 'Success') {
        this.messageService.showMessage({ severity: 'success', title: 'Success', body: MessageConfig.Sales.Add });
        this.getDetailByTicket(flag);
        this.addItemForm.controls.quantity.enable();
        this.addItemFormInit();
        this.submitted = false;
      } else {
        this.messageService.showMessage({ severity: 'error', title: 'Error', body: MessageConfig.CommunicationError });
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
      const ticket = JSON.parse(data.resultData);
      if (data.status === 'Success') {
        const ticket = JSON.parse(data.resultData);
        this.ticketNumberGeneration = true;
        this.newTicketNumber = ticket.GetTicketNumber.JobId;
        this.JobId = ticket.GetTicketNumber.JobId;
        // this.multipleTicketNumber.push(this.newTicketNumber);
        //this.getTicketDetail(this.JobId);
      }
      else {
        this.messageService.showMessage({ severity: 'error', title: 'Error', body: MessageConfig.TicketNumber });
      }
    });
    this.enableAdd = true;
    this.clearpaymentField();
    this.clearGridItems();
  }
  creditProcess() {
    this.removAddedAmount(this.credit);
    this.credit = this.creditTotal - this.creditcashback;
    if (this.credit > (this.originalGrandTotal - this.totalPaid - this.discountAmount + this.credit)) {
      this.credit = 0;
      this.creditcashback = 0;
      this.messageService.showMessage({ severity: 'warning', title: 'Warning', body: MessageConfig.Sales.creditAmount });
      return;
    }
    this.calculateTotalpaid(this.credit);
    this.cashback = this.cashback + this.creditcashback;
    document.getElementById('creditcardpopup').style.width = '0';
    this.paymentProcess();
  }

  paymentProcess() {
    const ngbModalOptions: NgbModalOptions = {
      backdrop: 'static',
      keyboard: false,
      size: 'lg'
    };
    const modalRef = this.modalService.open(PaymentProcessComponent, ngbModalOptions);
    modalRef.componentInstance.clientId = this.clientId;
    modalRef.componentInstance.totalAmount = this.credit;
    modalRef.result.then((result) => {
      if (result.status) {
        this.isCreditPay = true;
        this.tips = result.tipAmount;
        this.captureObj = result.authObj;
        // this.addPayment();
        // this.paymentCapture(result.authObj);
      } else {
        this.getDetailByTicket(false);
      }
    });
  }

  paymentCapture() {
    const auth = this.captureObj;
    const totalAmount = this.credit + (+this.tips);
    const amount = this.decimalPipe.transform(totalAmount, '.2-2');
    const capObj = {
      authCode: auth.authcode,
      amount: amount.toString(),
      retRef: auth.retref,
      invoiceId: {}
    };
    this.salesService.paymentCapture(capObj).subscribe(res => {
      if (res.status === 'Success') {
        const capture = JSON.parse(res.resultData);
        console.log(capture, 'auth');
        this.addPayment();
      } else {
        this.messageService.showMessage({ severity: 'warning', title: 'Warning', body: res.errorMessage });
      }
    });
  }

  processpayment() {
    if (this.credit !== 0) {
      this.paymentCapture();
    } else {
      this.addPayment();
    }
  }

  removAddedAmount(amount) {
    this.totalPaid = this.totalPaid - amount;
  }
  calculateTotalpaid(amount) {
    this.totalPaid = 0;
    this.totalPaid = this.totalPaid + amount;
  }
  addCash(cash) {
    this.cashTotal = +this.cashTotal + cash;
  }
  cashProcess() {
    this.removAddedAmount(+this.cash);
    this.cash = this.cashTotal;
    this.calculateTotalpaid(+this.cash);
    if (this.totalPaid > +this.grandTotal) {
      this.cashback = this.totalPaid - (+this.grandTotal);
    }
    document.getElementById('cashpopup').style.width = '0';
  }
  discountProcess() {
    let discountValue = 0;
    if (this.isDiscountAdded) {
      this.messageService.showMessage({ severity: 'warning', title: 'Warning', body: MessageConfig.Sales.duplicate });
      return;
    }
    this.addedDiscount = this.selectedDiscount.map(x => Object.assign({}, x));
    this.discountList = [];
    if (this.addedDiscount.length > 0) {
      let washDiscountPrice = 0;
      let detailDiscountPrice = 0;
      let additionalDiscountPrice = 0;
      let upchargeDiscountPrice = 0;
      let airfreshnerDiscountPrice = 0;
      let outsideDiscountPrice = 0;
      let noServiceTypePrice = 0;
      let allServiceDiscountPrice = 0;
      this.addedDiscount.forEach(item => {
        const serviceType = this.serviceType.filter(type => +type.CodeId === +item.DiscountServiceType);
        if (serviceType.length > 0) {
          let washCost = 0;
          let detailCost = 0;
          let additionalCost = 0;
          let upchargeCost = 0;
          let airfreshnerCost = 0;
          let outsideCost = 0;
          let allServiceCost = 0;
          if (serviceType[0].CodeValue === ApplicationConfig.Enum.ServiceType.WashPackage) {
            this.washes.forEach(wash => {
              washCost = washCost + wash.Price;
            });
            item.Price = String(item.Price).replace('-', '');
            item.Price = +item.Price;
            if (item.DiscountType === 'Flat Fee') {
              washDiscountPrice = washDiscountPrice + item.Price;
            } else if (item.DiscountType === 'Percentage') {
              washDiscountPrice = washDiscountPrice + (washCost * item.Price / 100);
              item.Price = (washCost * item.Price / 100);
            }
          } else if (serviceType[0].CodeValue === ApplicationConfig.Enum.ServiceType.DetailPackage) {
            this.details.forEach(detail => {
              detailCost = detailCost + detail.Price;
            });
            item.Price = String(item.Price).replace('-', '');
            item.Price = +item.Price;
            if (item.DiscountType === 'Flat Fee') {
              detailDiscountPrice = detailDiscountPrice + item.Price;
            } else if (item.DiscountType === 'Percentage') {
              detailDiscountPrice = detailDiscountPrice + (detailCost * item.Price / 100);
              item.Price = (detailCost * item.Price / 100);
            }
          } else if (serviceType[0].CodeValue === ApplicationConfig.Enum.ServiceType.AdditonalServices) {
            this.additionalService.forEach(additional => {
              additionalCost = additionalCost + additional.Price;
            });
            item.Price = String(item.Price).replace('-', '');
            item.Price = +item.Price;
            if (item.DiscountType === 'Flat Fee') {
              additionalDiscountPrice = additionalDiscountPrice + item.Price;
            } else if (item.DiscountType === 'Percentage') {
              additionalDiscountPrice = additionalDiscountPrice + (additionalCost * item.Price / 100);
              item.Price = (additionalCost * item.Price / 100);
            }
          } else if (serviceType[0].CodeValue === ApplicationConfig.Enum.ServiceType.AirFresheners) {
            this.airfreshnerService.forEach(airFreshner => {
              airfreshnerCost = airfreshnerCost + airFreshner.Price;
            });
            item.Price = String(item.Price).replace('-', '');
            item.Price = +item.Price;
            if (item.DiscountType === 'Flat Fee') {
              airfreshnerDiscountPrice = airfreshnerDiscountPrice + item.Price;
            } else if (item.DiscountType === 'Percentage') {
              airfreshnerDiscountPrice = airfreshnerDiscountPrice + (airfreshnerCost * item.Price / 100);
              item.Price = (airfreshnerCost * item.Price / 100);
            }
          } else if (serviceType[0].CodeValue === ApplicationConfig.Enum.ServiceType.OutsideServices) {
            this.outsideServices.forEach(outside => {
              outsideCost = outsideCost + outside.Price;
            });
            item.Price = String(item.Price).replace('-', '');
            item.Price = +item.Price;
            if (item.DiscountType === 'Flat Fee') {
              outsideDiscountPrice = outsideDiscountPrice + item.Price;
            } else if (item.DiscountType === 'Percentage') {
              outsideDiscountPrice = outsideDiscountPrice + (outsideCost * item.Price / 100);
              item.Price = (outsideCost * item.Price / 100);
            }
          } else if (serviceType[0].CodeValue === ApplicationConfig.Enum.ServiceType.Upcharges) {
            this.upCharges.forEach(upcharge => {
              upchargeCost = upchargeCost + upcharge.Price;
            });
            item.Price = String(item.Price).replace('-', '');
            item.Price = +item.Price;
            if (item.DiscountType === 'Flat Fee') {
              upchargeDiscountPrice = upchargeDiscountPrice + item.Price;
            } else if (item.DiscountType === 'Percentage') {
              upchargeDiscountPrice = upchargeDiscountPrice + (upchargeCost * item.Price / 100);
              item.Price = (upchargeCost * item.Price / 100);
            }
          } else if (item.DiscountServiceType === null) {
            noServiceTypePrice = noServiceTypePrice + item.Price;
          } else if (serviceType[0].CodeValue === ApplicationConfig.Enum.ServiceType.ServiceDiscounts) {
            this.allService.forEach(service => {
              allServiceCost = allServiceCost + service.Price;
            });
            item.Price = String(item.Price).replace('-', '');
            item.Price = +item.Price;
            if (item.DiscountType === 'Flat Fee') {
              allServiceDiscountPrice = allServiceDiscountPrice + item.Price;
            } else if (item.DiscountType === 'Percentage') {
              allServiceDiscountPrice = allServiceDiscountPrice + (allServiceCost * item.Price / 100);
              item.Price = (allServiceCost * item.Price / 100);
            }
          }
        } else if (item.DiscountServiceType === null || +item.DiscountServiceType === 0) {
          item.Price = String(item.Price).replace('-', '');
          noServiceTypePrice = noServiceTypePrice + (+item.Price);
        }
        discountValue = washDiscountPrice + detailDiscountPrice + additionalDiscountPrice + airfreshnerDiscountPrice
          + upchargeDiscountPrice + outsideDiscountPrice + noServiceTypePrice + allServiceDiscountPrice;
      });
      this.discountAmount = discountValue;
    } else {
      this.discountAmount = 0;
    }
    this.addedDiscount.forEach(item => {
      this.discountList.push(item);
    });
    document.getElementById('discountpopup').style.width = '0';
  }
  discountChange(event) {
    if (this.selectedDiscount.length > 0) {
      const dup = this.selectedDiscount.filter(item => +item.ServiceId === +this.discount);
      if (dup.length > 0) {
        this.isDiscountAdded = true;
        this.discount = '';
        this.messageService.showMessage({ severity: 'warning', title: 'Warning', body: MessageConfig.Sales.duplicate });
        return;
      } else {
        this.isDiscountAdded = false;
      }
    }
    if (this.discountService.length > 0) {
      const duplicatecheck = this.discountService.filter(selectedDis => +selectedDis.ServiceId === +this.discount);
      if (duplicatecheck.length > 0) {
        this.isDiscountAdded = true;
        this.messageService.showMessage({ severity: 'warning', title: 'Warning', body: MessageConfig.Sales.discountExist });
        return;
      } else {
        this.isDiscountAdded = false;
      }
    }
    for (const i of this.discounts) {
      if (i.ServiceId === +event.target.value) {
        if (i.DiscountServiceType === 0 || i.DiscountServiceType === null) {
          this.discount = '';
          this.messageService.showMessage({ severity: 'error', title: 'Error', body: MessageConfig.Sales.invalidDiscount });
          return;
        } else {
          // this.addedDiscount.push(i);
          this.selectedDiscount.push(i);
        }
      }
    }
    // if (this.addedDiscount.length > 0) {
    //   this.addedDiscount.forEach(item => {
    //     if (item.DiscountType === 'Flat Fee') {
    //       item.Price = String(item.Price).replace('-', '');
    //       item.Price = +item.Price;
    //     } else {
    //       item.Price = item.Price;
    //     }
    //   });
    // }
  }

  deletediscount(event) {
    const index = this.selectedDiscount.findIndex(item => item.ServiceId === +event.ServiceId);
    this.selectedDiscount.splice(index, 1);
    this.discountList = this.discountList.filter(item => item.ServiceId !== +event.ServiceId);
    let discountAmount = 0;
    this.selectedDiscount.forEach(item => {
      discountAmount = discountAmount + (+item.Price);
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
      this.messageService.showMessage({ severity: 'warning', title: 'Warning', body: MessageConfig.Sales.payment });
      return;
    }
    if (0 < balancedue) {
      this.messageService.showMessage({ severity: 'warning', title: 'Warning', body: MessageConfig.Sales.total });
      return;
    }
    if (this.ticketNumberGeneration === false) {
      this.messageService.showMessage({ severity: 'error', title: 'Error', body: MessageConfig.TicketNumber });
      return;
    }

    // if (this.credit !== 0 && !this.isCreditPay) {
    //   this.isCreditPay = true;
    //   const ngbModalOptions: NgbModalOptions = {
    //     backdrop: 'static',
    //     keyboard: false,
    //     size: 'lg'
    //   };
    //   const modalRef = this.modalService.open(PaymentProcessComponent, ngbModalOptions);
    //   modalRef.componentInstance.clientId = this.clientId;
    //   modalRef.componentInstance.totalAmount = this.credit;
    //   modalRef.result.then((result) => {
    //     if (result.status) {
    //       this.isCreditPay = true;
    //       this.tips = result.tipAmount;
    //       this.paymentCapture(result.authObj);
    //     }
    //   });
    //   return;
    // }

    let giftcard = null;
    let discount = null;
    giftcard = this.giftcards.map(item => {
      return {
        giftCardHistoryId: 0,
        giftCardId: item.id,
        locationId: this.locationId,
        transactionType: 1,
        transactionAmount: -(+item.amount),
        transactionDate: new Date(),
        comments: null,
        isActive: true,
        isDeleted: false,
        createdBy: null,
        createdDate: new Date(),
        updatedBy: null,
        updatedDate: new Date(),
        jobPaymentId: 0
      };
    });
    discount = this.selectedDiscount.map(item => {
      return {
        jobPaymentDiscountId: 0,
        jobPaymentId: null,
        serviceDiscountId: +item.ServiceId,
        amount: item.Cost,
        isActive: true,
        isDeleted: false,
        createdBy: null,
        createdDate: new Date(),
        updatedBy: null,
        updatedDate: new Date()
      }
    });
    const discountPayType = this.PaymentType.filter(i => i.CodeValue === ApplicationConfig.PaymentType.Discount)[0].CodeId;
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
      console.log(this.tips)

      const cashPayType = this.PaymentType.filter(i => i.CodeValue === ApplicationConfig.PaymentType.Cash)[0].CodeId;
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
    if (+this.tips !== 0) {
      const TipsPayType = this.PaymentType.filter(i => i.CodeValue === ApplicationConfig.PaymentType.Tips)[0].CodeId;
      const Tips = {
        jobPaymentDetailId: 0,
        jobPaymentId: 0,
        paymentType: TipsPayType,
        amount: this.tips ? +this.tips : 0,
        taxAmount: 0,
        signature: '',
        isActive: true,
        isDeleted: false,
        createdBy: null,
        createdDate: new Date(),
        updatedBy: null,
        updatedDate: new Date()
      };
      paymentDetailObj.push(Tips);
    }
    if (this.account !== 0) {
      let accountPayType = this.PaymentType.filter(i => i.CodeValue === ApplicationConfig.PaymentType.Account)[0].CodeId;
      if (this.accountDetails?.SalesAccountViewModel?.CodeValue !== ApplicationConfig.CodeValue.Comp) {
        accountPayType = this.PaymentType.filter(i => i.CodeValue === ApplicationConfig.PaymentType.Membership)[0].CodeId;
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
      const creditPayType = this.PaymentType.filter(i => i.CodeValue === ApplicationConfig.PaymentType.Card)[0].CodeId;
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
      const giftPayType = this.PaymentType.filter(i => i.CodeValue === ApplicationConfig.PaymentType.GiftCard)[0].CodeId;
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
        membershipId: this.isAccountButton ? this.accountDetails !== undefined ? this.accountDetails?.MembershipId : null : null,
        jobId: this.isSelected ? this.itemList.Status.SalesItemViewModel[0].JobId : this.JobId,
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
        isProcessed: true,
        cashback: this.cashback
      },
      jobPaymentDetail: paymentDetailObj,
      giftCardHistory: giftcard.length === 0 ? null : giftcard,
      jobPaymentCreditCard: null
    };

    const jobProductItem = [];
    this.Products.forEach(item => {
      jobProductItem.push({
        productId: item.ProductId,
        quantity: item.Quantity,
        productName: item.ProductName
      });
    });
    const jobId = [];
    this.multipleTicketNumber.forEach(item => {
      jobId.push(item.toString());
    });
    // jobId.push(this.JobId);
    const paymentDetail = {
      SalesPaymentDto: paymentObj,
      SalesProductItemDto: jobProductItem.length > 0 ? { jobProductItem } : null,
      locationId: +localStorage.getItem('empLocationId'),
      jobId: this.multipleTicketNumber.toString()
    };

    this.spinner.show();
    this.salesService.addPayemnt(paymentDetail).subscribe(data => {
      if (data.status === 'Success') {
        this.spinner.hide();

        if (this.accountDetails !== null && this.accountDetails?.SalesAccountViewModel?.CodeValue === ApplicationConfig.CodeValue.Comp) {
          const amt = (+this.accountDetails?.SalesAccountViewModel?.Amount.toFixed(2) - +this.account.toFixed(2)).toFixed(2);
          const obj = {
            clientId: this.accountDetails?.SalesAccountViewModel?.ClientId,
            amount: amt
          };
          this.salesService.updateAccountBalance(obj).subscribe(res => {
          });
        }
        this.messageService.showMessage({ severity: 'success', title: 'Success', body: MessageConfig.Sales.paymentSave });
        this.getDetailByTicket(false);
        if (this.newTicketNumber === '') {
          this.router.navigate([`/checkout`], { relativeTo: this.route });
        }
      } else {
        this.spinner.hide();

        this.messageService.showMessage({ severity: 'error', title: 'Error', body: MessageConfig.Sales.paymentComplete });
      }
    }, (err) => {
      this.messageService.showMessage({ severity: 'error', title: 'Error', body: MessageConfig.Sales.paymentComplete });
      this.spinner.hide();
    });

  }
  deleteTicket() {
    if (this.multipleTicketNumber.length > 0) {
      this.salesService.deleteJob(this.multipleTicketNumber.toString()).subscribe(data => {
        if (data.status === 'Success') {
          this.messageService.showMessage({ severity: 'success', title: 'Success', body: MessageConfig.Sales.jobDelete });
          this.getDetailByTicket(false);
          this.ticketNumber = '';
        } else {
          this.messageService.showMessage({ severity: 'error', title: 'Error', body: MessageConfig.CommunicationError });
        }
      }, (err) => {
        this.messageService.showMessage({ severity: 'error', title: 'Error', body: MessageConfig.CommunicationError });
      });
    }
  }
  validateGiftcard() {
    const gNo = this.giftCardForm.value.giftCardNumber;
    if (this.giftcardNumber !== '') {
      if (gNo === this.giftcardNumber) {
        this.messageService.showMessage({ severity: 'warning', title: 'Warning', body: MessageConfig.Sales.purchasedGiftcard });
        return;
      }
    }
    if (gNo !== '') {
      let isAlreadyadded = false;
      this.giftcards.forEach(item => {
        if (gNo === item.number) {
          isAlreadyadded = true;
        }
      });
      if (isAlreadyadded) {
        this.messageService.showMessage({ severity: 'warning', title: 'Warning', body: MessageConfig.Sales.alreadyAdded });
        return;
      }
    }

    this.giftcardService.getBalance(gNo).subscribe(data => {
      if (data.status === 'Success') {
        this.validGiftcard = JSON.parse(data.resultData);
        if (this.validGiftcard?.GiftCardDetail[0]?.GiftCardId !== null) {
          this.isInvalidGiftcard = false;
        } else {
          this.isInvalidGiftcard = true;
        }
      }
    });
  }
  validateAmount() {
    this.isInvalidGiftcard = false;
    const enteredAmount = this.giftCardForm.value.giftCardAmount;
    const currentAmount = this.validGiftcard?.GiftCardDetail[0]?.BalanceAmount;
    const today = new Date();
    const giftcardexpiryDate = this.validGiftcard?.GiftCardDetail[0]?.ActiveDate;
    if (enteredAmount !== undefined && currentAmount !== undefined) {
      if (currentAmount < enteredAmount) {
        this.giftCardForm.patchValue({ giftCardAmount: '' });
        this.balance = 0;
        this.messageService.showMessage({ severity: 'info', title: 'Information', body: MessageConfig.Admin.GiftCard.redeemAmount });
      } else {
        this.balance = currentAmount - enteredAmount;
      }
    }

  }
  rollBack() {
    if (this.multipleTicketNumber.length > 0) {

      const rollbackObj =
      {
        TicketNumber: this.multipleTicketNumber.toString(),
        LocationId: this.locationId
      };

      this.salesService.rollback(rollbackObj).subscribe(data => {
        if (data.status === 'Success') {
          this.getDetailByTicket(false);
          this.messageService.showMessage({ severity: 'success', title: 'Success', body: MessageConfig.Sales.rollback });
        } else {
          this.messageService.showMessage({ severity: 'error', title: 'Error', body: MessageConfig.CommunicationError });
        }
      }, (err) => {
        this.messageService.showMessage({ severity: 'error', title: 'Error', body: MessageConfig.CommunicationError });
      });
    }
  }

  getJobType() {
    this.salesService.getJobType().subscribe(res => {
      if (res.status === 'Success') {
        const jobtype = JSON.parse(res.resultData);
        if (jobtype.GetJobType.length > 0) {
          jobtype.GetJobType.forEach(item => {
            if (item.valuedesc === ApplicationConfig.Enum.JobType.WashJob) {
              this.jobTypeId = item.valueid;
            }
          });
        }
      }
    }, (err) => {
      this.messageService.showMessage({ severity: 'error', title: 'Error', body: MessageConfig.CommunicationError });
    });
  }

  processAccount() {
    this.totalAmount = 0;
    this.isAccountButton = !this.isAccountButton;
    if (this.isAccount) {
      if (this.accountEnable) {
        this.removAddedAmount(this.account);
        this.allService.forEach(ele => {
          this.totalAmount += ele.Price
        });
        this.account = this.totalAmount;
        this.calculateTotalpaid(this.account);
        this.messageService.showMessage({ severity: 'info', title: 'Information', body: MessageConfig.Sales.CreditAccountApplied });
      }
      else if (this.isMembership) {
        if (this.vehicleIds) {
          this.vehicle.getVehicleMembershipDetailsByVehicleId(this.vehicleIds).subscribe(res => {
            if (res.status === 'Success') {
              const vehicle = JSON.parse(res.resultData);
              if (vehicle.VehicleMembershipDetails.ClientVehicleMembershipService?.length !== 0) {
                const totalService = vehicle.VehicleMembershipDetails.ClientVehicleMembershipService
                this.filterRecord = [];
                totalService.forEach(list => {
                  const lists = this.totalWashService.filter(ele => ele.ServiceId === list.ServiceId)
                  const dels = lists[0]
                  this.filterRecord.push(dels);
                });
                this.filterRecord = [...this.totalChargeService, ...this.filterRecord, ...this.washPackage]
                console.log(this.filterRecord);
                if (this.filterRecord.length !== 0) {
                  this.filterRecord.forEach(ele => {
                    this.totalAmount += ele.Price
                  });
                  this.account = this.totalAmount;
                  this.calculateTotalpaid(this.account);
                  this.messageService.showMessage({ severity: 'info', title: 'Information', body: MessageConfig.Sales.MembershipApplied });
                }
              }
            }
          }, (err) => {
            this.toastr.error(MessageConfig.CommunicationError, 'Error!');
          });

        }
      }
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
