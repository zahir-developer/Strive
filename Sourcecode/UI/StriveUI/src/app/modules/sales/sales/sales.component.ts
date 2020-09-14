import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { MembershipService } from 'src/app/shared/services/data-service/membership.service';
import { SalesService } from 'src/app/shared/services/data-service/sales.service';
import { ConfirmationUXBDialogService } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.service';
import { NgbModalOptions, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { EditItemComponent } from './edit-item/edit-item.component';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-sales',
  templateUrl: './sales.component.html',
  styleUrls: ['./sales.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class SalesComponent implements OnInit {
  services: any;
  filteredItem = [];
  selected = false;
  giftCardForm: FormGroup;
  addItemForm: FormGroup;
  itemList: any;
  originalGrandTotal: string;
  JobId: any;
  constructor(private membershipService: MembershipService, private salesService: SalesService,
              private confirmationService: ConfirmationUXBDialogService, private modalService: NgbModal, private fb: FormBuilder) { }
  ItemName = '';
  ticketNumber = '';
  count = 0;
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
  cash = '';
  credit = '';
  giftCard = '';
  account = '';
  totalPaid = '';
  balanceDue = '';
  Cashback = '';
  ngOnInit(): void {
    this.giftCardFromInit();
    this.addItemFormInit();
    this.getAllService();
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
    })
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
  selectedItem(event) {
    this.selectedItem = event;
    console.log(event.name);
  }
  getDetailByTicket() {
    if (this.ticketNumber !== undefined && this.ticketNumber !== '') {
      this.salesService.getItemByTicketNumber(+this.ticketNumber).subscribe(data => {
        console.log(data, 'ticket');
        if (data.status === 'Success') {
          this.enableAdd = true;
          this.itemList = JSON.parse(data.resultData);
          if (this.itemList.Status.ScheduleItemViewModel !== null) {
            if (this.itemList.Status.ScheduleItemViewModel.length !== 0) {
              this.JobId = this.itemList.Status.ScheduleItemViewModel[0].JobId;
              this.washes = this.itemList.Status.ScheduleItemViewModel.filter(item => item.ServiceType === 'Washes');
              this.details = this.itemList.Status.ScheduleItemViewModel.filter(item => item.ServiceType === 'Details');
              this.additionalService = this.itemList.Status.ScheduleItemViewModel.filter(item => item.ServiceType === 'AdditionalService');
            }
          }
          if (this.itemList?.Status?.ScheduleItemSummaryViewModels !== null ) {
this.grandTotal = this.itemList?.Status?.ScheduleItemSummaryViewModels?.GrandTotal;
this.originalGrandTotal = this.grandTotal;
          }
        }
      }, (err) => {
        this.enableAdd = false;
      });
    }
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
    // this.salesService.deleteItemById(data.LocationId).subscribe(res => {
    //   if (res.status === 'Success') {

    //   } else {

    //   }
    // });
  }
  opengiftcard() {
    document.getElementById('Giftcardpopup').style.width = '450px';
    document.getElementById('creditcardpopup').style.width = '0';
  }

  closegiftcard() {
    document.getElementById('Giftcardpopup').style.width = '0';
  }

  opencreditcard() {
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
    modalRef.componentInstance.isModal = true;
  }
  deletegiftcard(event) {
    console.log(event.id);
    const index = this.giftcards.findIndex(item => item.id === +event.id);
    this.giftcards.splice(index, 1);

  }
  addGiftCard() {
    this.giftcardsubmitted = true;
    const giftCardNumber = this.giftCardForm.value.giftCardNumber;
    const giftCardAmount = this.giftCardForm.value.giftCardAmount;
    if (this.giftCardForm.valid) {
      this.giftcards.push({ id: this.count++, number: giftCardNumber, amount: giftCardAmount });
      this.giftCardForm.reset();
      this.giftcardsubmitted = false;
    } else {
      return;
    }
  }
  giftCardProcess() {
    console.log(this.giftcards);
  }
  addItem() {
  }
  getNumAndUpdate(num) {
    this.addItemForm.patchValue({ quantity: this.addItemForm.value.quantity.toString() + num.toString() });
  }
  clear() {
    this.addItemForm.patchValue({ itemName: '', quantity: '' });
  }
  backspace() {
    const quantity = this.addItemForm.value.quantity;
    this.addItemForm.patchValue({ quantity: quantity.substring(0, quantity.length - 1) });
  }
  addCashBack(cashback) {
this.grandTotal = this.grandTotal + cashback;
  }
  reset() {
    this.grandTotal = this.originalGrandTotal;
  }
}
