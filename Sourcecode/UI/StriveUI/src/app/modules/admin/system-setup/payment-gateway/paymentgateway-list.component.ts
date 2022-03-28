import { Component, OnInit } from '@angular/core';
import { NgbModal, NgbActiveModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { CollisionListComponent } from '../../employees/collision-list/collision-list.component';
import { Router } from '@angular/router';

import { ToastrService } from 'ngx-toastr';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { PaymentService } from 'src/app/shared/services/common-service/payment.service';
import { MessageConfig } from 'src/app/shared/services/messageConfig';
import { PayrollsService } from 'src/app/shared/services/data-service/payrolls.service';
import { DatePipe } from '@angular/common';

import { ApplicationConfig } from 'src/app/shared/services/ApplicationConfig';

import { EmployeeCollisionComponent } from '../../employees/employee-collision/employee-collision.component';
import { PaymentGatewayCreateEditComponent } from '../payment-gateway/payment-gateway-create-edit/payment-gateway-create-edit.component';
import { NgxSpinnerService } from 'ngx-spinner';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { Subject } from 'rxjs';
import { ConfirmationUXBDialogService } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.service';

@Component({
  selector: 'app-paymentgateway-list',
  templateUrl: './paymentgateway-list.component.html'
})
export class PaymentGatewayComponent implements OnInit {
    paymentGatewayDetails = [];
    showDialog = false;
    selectedData: any;
    headerData: string;
    search: any = '';
    isEdit: boolean;
    isTableEmpty: boolean;
    selectedLocation: any;
    collectionSize: number = 0;
    pageSize=10;
    page=1;
    pageSizeList=[];
    isDesc: boolean = false;
    column: string = 'LocationName';
    isLoading: boolean;
    sortColumn: { sortBy: string; sortOrder: string; };
  constructor(
    private payrollsService: PayrollsService,
    private MessageServiceToastr: MessageServiceToastr,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService,
    private confirmationService: ConfirmationUXBDialogService, 
    private payment:PaymentService, private modalService: NgbModal,
  ){ }
  ngOnInit(): void {
  //  this.sortColumn = { sortBy: ApplicationConfig.Sorting.SortBy.Employee, sortOrder: ApplicationConfig.Sorting.SortOrder.Employee.order };
    this.page = ApplicationConfig.PaginationConfig.page;
    this.pageSize = ApplicationConfig.PaginationConfig.TableGridSize;
    this.pageSizeList = ApplicationConfig.PaginationConfig.Rows;
    this.paymentGatewayList();
  }

  paginate(event) {
    this.pageSize = +this.pageSize;
    this.page = event;
  }
  paginatedropdown(event) {
    this.pageSize = +event.target.value;
    this.page = 1;
  }

  paymentGatewayList() {
    this.isLoading = true;
   this.payment.getPaymentList().subscribe(data => {
      this.isLoading = false;
      if (data.status === 'Success') {
       
        const oPayment = JSON.parse(data.resultData);
       
        this.paymentGatewayDetails = oPayment.PaymentGateway
        
      } else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    },
      (err) => {
        this.isLoading = false;
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      });
      
  }

  add(){
    const ngbModalOptions: NgbModalOptions = {
      backdrop: 'static',
      keyboard: false,
      size: 'lg'
    };
   const modalRef = this.modalService.open(PaymentGatewayCreateEditComponent, ngbModalOptions);
   modalRef.componentInstance.header = "Add New Payment Gateway";
}
edit(oPay){
  const ngbModalOptions: NgbModalOptions = {
    backdrop: 'static',
    keyboard: false,
    size: 'lg'
  };
  const modalRef = this.modalService.open(PaymentGatewayCreateEditComponent, ngbModalOptions);
  
  modalRef.componentInstance.PaymentGatewayId = oPay.PaymentGatewayId;
  modalRef.componentInstance.PaymentGatewayName = oPay.PaymentGatewayName;
  modalRef.componentInstance.BaseURL = oPay.BaseURL;
  modalRef.componentInstance.APIKey = oPay.APIKey;
  modalRef.componentInstance.header = "Edit Payment Gateway ";
}
delete(oPay){
  this.confirmationService.confirm('Delete Payment Gateway', `Are you sure you want to delete this Payment Gateway? All related 
  information will be deleted and the Payment Gateway cannot be retrieved?`, 'Yes', 'No')
    .then((confirmed) => {
      if (confirmed === true) {
        this.confirmDelete(oPay);
      }
    })
    .catch(() => { });
}

confirmDelete(data) {
  this.spinner.show();
  this.payrollsService.deletePayment(data.PaymentGatewayId).subscribe(res => {
    if (res.status === 'Success') {
      this.spinner.hide();

      this.toastr.success(MessageConfig.Admin.SystemSetup.BasicSetup.Delete, 'Success!');
      this.paymentGatewayList();
    } else {
      this.spinner.hide();

      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    }
  }
    ,
    (err) => {
      this.spinner.hide();
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
}

changesort(property){

}
closePopupEmit(event) {
  if (event.status === 'saved') {
  }
  this.showDialog = event.isOpenPopup;
}
}