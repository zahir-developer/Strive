import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { MembershipService } from 'src/app/shared/services/data-service/membership.service';
import { SalesService } from 'src/app/shared/services/data-service/sales.service';
import { ConfirmationUXBDialogService } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.service';
import { NgbModalOptions, NgbModal } from '@ng-bootstrap/ng-bootstrap';
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
import { element } from 'protractor';
import { GetCodeService } from 'src/app/shared/services/data-service/getcode.service';
import { SaleGiftCardComponent } from '../sales/sales/sale-gift-card/sale-gift-card.component';
import { EditItemComponent } from '../sales/sales/edit-item/edit-item.component';
import { PrintComponent } from '../sales/sales/print/print.component';
import { ApplicationConfig } from 'src/app/shared/services/ApplicationConfig';
import { ToastrService } from 'ngx-toastr';
import { MessageConfig } from 'src/app/shared/services/messageConfig';
@Component({
  selector: 'app-customer-sales',
  templateUrl: './customer-sales.component.html'
})
export class CustomerSalesComponent implements OnInit {
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
  discountList: any = [];
  constructor(private membershipService: MembershipService, private salesService: SalesService, private router: Router,
    private confirmationService: ConfirmationUXBDialogService, private modalService: NgbModal, private fb: FormBuilder,
    private messageService: MessageServiceToastr, private service: ServiceSetupService,
    private giftcardService: GiftCardService, private spinner: NgxSpinnerService,
    private route: ActivatedRoute, private codes: GetCodeService
    ,private toastr: ToastrService) { }
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
  ngOnInit(): void {
    this.isTenTicketNumber = false;
  
    const paramsData = "987821"

    this.router.navigate(['/Customer-sales'], { queryParams: { ticketNumber: paramsData } });
    // const paramsData = this.route.snapshot.queryParamMap.get('ticketNumber');
    if (paramsData !== null) {
      this.ticketNumber = paramsData;
      this.addTicketNumber();
    }
  
   this.getServiceForDiscount();
    this.getAllServiceandProduct();
  }

 

  getAllServiceandProduct() {
    const locID = +localStorage.getItem('empLocationId');
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
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }
  
  getServiceForDiscount() {
    this.service.getAllServiceDetail(+localStorage.getItem('empLocationId')).subscribe(data => {
      if (data.status === 'Success') {
        const services = JSON.parse(data.resultData);
        if (services.ServiceSetup !== null && services.ServiceSetup.length !== 0) {
          this.discounts = services.ServiceSetup.filter(item => item.ServiceType === ApplicationConfig.Enum.ServiceType.Discounts);
        }
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }
 
  

  addTicketNumber() {
    const alreadyAdded = this.multipleTicketNumber.filter(item => item === this.ticketNumber);
    if (alreadyAdded.length === 0) {
      this.multipleTicketNumber.push(this.ticketNumber);
      this.ticketNumber = '';
    } else {
      this.ticketNumber = '';
      this.messageService.showMessage({ severity: 'info', title: 'Information', body: MessageConfig.Sales.Ticket });
    }

    if (this.multipleTicketNumber.length > 10) {
      this.isTenTicketNumber = true;
    } else {
      this.isTenTicketNumber = false;
    }
    this.getDetailByTicket(false);
  }

 
  getDetailByTicket(flag) {
    this.enableButton = false;
  
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
          this.isAccount = this.accountDetails?.CodeValue !== ApplicationConfig.CodeValue.Comp ? this.accountDetails?.IsAccount : false;
        }
      }, (err) => {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      });
      this.spinner.show();
      this.salesService.getItemByTicketNumber(ticketNumber).subscribe(data => {
        if (data.status === 'Success') {
          this.spinner.hide();

          this.enableAdd = true;
          this.itemList = JSON.parse(data.resultData);
          if (this.itemList.Status.SalesItemViewModel !== null) {
            if (this.itemList.Status.SalesItemViewModel.length !== 0) {
              this.showPopup = true;
              this.washes = this.itemList.Status.SalesItemViewModel.filter(item => item.ServiceType === 'Washes');
              this.details = this.itemList.Status.SalesItemViewModel.filter(item => item.ServiceType === 'Details');
              this.additionalService = this.itemList.Status.SalesItemViewModel.filter(item =>
                item.ServiceType === ApplicationConfig.Enum.ServiceType.AdditonalServices);
              this.upCharges = this.itemList.Status.SalesItemViewModel.filter(item =>
                item.ServiceType === ApplicationConfig.Enum.ServiceType.Upcharges);
              this.outsideServices = this.itemList.Status.SalesItemViewModel.filter(item =>
                item.ServiceType === ApplicationConfig.Enum.ServiceType.OutsideServices );
              this.airfreshnerService = this.itemList.Status.SalesItemViewModel.filter(item =>
                item.ServiceType === ApplicationConfig.Enum.ServiceType.AirFresheners);
              this.discountService = this.itemList.Status.SalesItemViewModel.filter(item =>
                item.ServiceType === ApplicationConfig.Enum.ServiceType.Discounts );
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
              this.account = this.accountDetails?.IsAccount === true && this.accountDetails?.CodeValue === ApplicationConfig.CodeValue.Comp ? +this.grandTotal : 0;
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
        else{
          this.spinner.hide();
          this.toastr.error(MessageConfig.CommunicationError, 'Error!');

        }
      }, (err) => {
        this.enableAdd = false;
        this.spinner.hide();
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      });
    }
  }
 
  discountProcess() {
    let discountValue = 0;
    this.discountList = [];
    if (this.selectedDiscount.length > 0) {
      let washDiscountPrice = 0;
      let detailDiscountPrice = 0;
      let additionalDiscountPrice = 0;
      let upchargeDiscountPrice = 0;
      let airfreshnerDiscountPrice = 0;
      let outsideDiscountPrice = 0;
      this.selectedDiscount.forEach(item => {
        const serviceType = this.serviceType.filter(type => +type.CodeId === +item.DiscountServiceType);
        if (serviceType.length > 0) {
          let washCost = 0;
          let detailCost = 0;
          let additionalCost = 0;
          let upchargeCost = 0;
          let airfreshnerCost = 0;
          let outsideCost = 0;
          if (serviceType[0].CodeValue ===ApplicationConfig.CodeValue.Washes) {
            this.washes.forEach(wash => {
              washCost = washCost + wash.Price;
            });
            if (item.DiscountType === 'Flat Fee') {
              washDiscountPrice = washDiscountPrice + item.Cost;
            } else if (item.DiscountType === 'Percentage') {
              washDiscountPrice = washDiscountPrice + (washCost * item.Cost / 100);
              item.Cost = (washCost * item.Cost / 100);
            }
          } else if (serviceType[0].CodeValue === ApplicationConfig.CodeValue.Details) {
            this.details.forEach(detail => {
              detailCost = detailCost + detail.Price;
            });
            if (item.DiscountType === 'Flat Fee') {
              detailDiscountPrice = detailDiscountPrice + item.Cost;
            } else if (item.DiscountType === 'Percentage') {
              detailDiscountPrice = detailDiscountPrice + (detailCost * item.Cost / 100);
              item.Cost = (detailCost * item.Cost / 100);
            }
          } else if (serviceType[0].CodeValue === ApplicationConfig.CodeValue.additionalServices) {
            this.additionalService.forEach(additional => {
              additionalCost = additionalCost + additional.Price;
            });
            if (item.DiscountType === 'Flat Fee') {
              additionalDiscountPrice = additionalDiscountPrice + item.Cost;
            } else if (item.DiscountType === 'Percentage') {
              additionalDiscountPrice = additionalDiscountPrice + (additionalCost * item.Cost / 100);
              item.Cost = (additionalCost * item.Cost / 100);
            }
          } else if (serviceType[0].CodeValue === ApplicationConfig.Enum.ServiceType.AdditonalServices) {
            this.airfreshnerService.forEach(airFreshner => {
              airfreshnerCost = airfreshnerCost + airFreshner.Price;
            });
            if (item.DiscountType === 'Flat Fee') {
              airfreshnerDiscountPrice = airfreshnerDiscountPrice + item.Cost;
            } else if (item.DiscountType === 'Percentage') {
              airfreshnerDiscountPrice = airfreshnerDiscountPrice + (airfreshnerCost * item.Cost / 100);
              item.Cost = (airfreshnerCost * item.Cost / 100);
            }
          } else if (serviceType[0].CodeValue === ApplicationConfig.Enum.ServiceType.OutsideServices) {
            this.outsideServices.forEach(outside => {
              outsideCost = outsideCost + outside.Price;
            });
            if (item.DiscountType === 'Flat Fee') {
              outsideDiscountPrice = outsideDiscountPrice + item.Cost;
            } else if (item.DiscountType === 'Percentage') {
              outsideDiscountPrice = outsideDiscountPrice + (outsideCost * item.Cost / 100);
              item.Cost = (outsideCost * item.Cost / 100);
            }
          } else if (serviceType[0].CodeValue === ApplicationConfig.Enum.ServiceType.Upcharges) {
            this.upCharges.forEach(upcharge => {
              upchargeCost = upchargeCost + upcharge.Price;
            });
            if (item.DiscountType === 'Flat Fee') {
              upchargeDiscountPrice = upchargeDiscountPrice + item.Cost;
            } else if (item.DiscountType === 'Percentage') {
              upchargeDiscountPrice = upchargeDiscountPrice + (upchargeCost * item.Cost / 100);
              item.Cost = (upchargeCost * item.Cost / 100);
            }
          }
        }
        discountValue = washDiscountPrice + detailDiscountPrice + additionalDiscountPrice + airfreshnerDiscountPrice
          + upchargeDiscountPrice + outsideDiscountPrice;
      });
      this.discountAmount = discountValue;
    } else {
      this.discountAmount = 0;
    }
    this.selectedDiscount.forEach( item => {
      this.discountList.push(item);
    });
   
    document.getElementById('discountpopup').style.width = '0';
  }
 
}
