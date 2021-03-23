import { Component, OnInit, ViewChild } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ApplicationConfig } from 'src/app/shared/services/ApplicationConfig';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { CheckoutService } from 'src/app/shared/services/data-service/checkout.service';
import { MessageConfig } from 'src/app/shared/services/messageConfig';
import { LandingService } from 'src/app/shared/services/common-service/landing.service';
import { BsDaterangepickerDirective, BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { ConfirmationUXBDialogService } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.service';

@Component({
  selector: 'app-checkout-grid',
  templateUrl: './checkout-grid.component.html',
  styleUrls: ['./checkout-grid.component.css']
})
export class CheckoutGridComponent implements OnInit {
  uncheckedVehicleDetails: any = [];
  isTableEmpty: boolean;
  page: any;
  pageSize: any;
  pageSizeList: any;
  collectionSize: number = 0;
  search = '';

  query = '';
  startDate: Date;
  endDate: Date;
  daterangepickerModel = new Date();
  @ViewChild('dp', { static: false }) datepicker: BsDaterangepickerDirective;
  bsConfig: Partial<BsDatepickerConfig>;
  sortColumn: { sortBy: string; sortOrder: string; };
  constructor(
    private checkout: CheckoutService,
    private message: MessageServiceToastr,
    private toastr: ToastrService,
    private confirmationService: ConfirmationUXBDialogService,

    private landingservice: LandingService,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit() {
    this.sortColumn =  { sortBy: ApplicationConfig.Sorting.SortBy.CheckOut, sortOrder: ApplicationConfig.Sorting.SortOrder.CheckOut.order };

    this.startDate = new Date();
    this.endDate = new Date();
    this.page = ApplicationConfig.PaginationConfig.page;
    this.pageSize = ApplicationConfig.PaginationConfig.TableGridSize;
    this.pageSizeList = ApplicationConfig.PaginationConfig.Rows;
  }
  landing() {
    this.landingservice.loadTheLandingPage();
  }
  onValueChange(event) {
    if (event !== null) {
      this.startDate = event;
      this.endDate = event;
      this.getAllUncheckedVehicleDetails();
    }
    else {
      this.startDate = null;
      this.endDate = null;
    }
  }
  // Get All Unchecked Vehicles
  getAllUncheckedVehicleDetails() {
    const obj = {
      locationId: localStorage.getItem('empLocationId'),
      startDate: this.startDate,
      endDate: this.endDate,
      pageNo: this.page,
      pageSize: this.pageSize,
      query: this.search == "" ? null : this.search,
      sortOrder: this.sortColumn.sortOrder,
      sortBy: this.sortColumn.sortBy,
      status: true
    };
    this.spinner.show();
    this.checkout.getUncheckedVehicleDetails(obj).subscribe(data => {
      if (data.status === 'Success') {
        this.spinner.hide();

        const uncheck = JSON.parse(data.resultData);
        this.uncheckedVehicleDetails = uncheck.GetCheckedInVehicleDetails.checkOutViewModel;
        if (this.uncheckedVehicleDetails == null) {
          this.isTableEmpty = true;
        } else {
          this.collectionSize = Math.ceil(uncheck.GetCheckedInVehicleDetails.Count.Count / this.pageSize) * 10;
          this.isTableEmpty = false;
        }
      } else {
        this.spinner.hide();

        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.spinner.hide();
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }
  paginate(event) {
    this.pageSize = +this.pageSize;
    this.page = event;
    this.getAllUncheckedVehicleDetails();
  }
  checkOutSearch() {
    this.search = this.query;
    this.getAllUncheckedVehicleDetails();
  }
  paginatedropdown(event) {
    this.pageSize = +event.target.value;
    this.page = this.page;
    this.getAllUncheckedVehicleDetails();
  }
  changeSorting(column) {
    this.sortColumn ={
     sortBy: column,
     sortOrder: this.sortColumn.sortOrder == 'ASC' ? 'DESC' : 'ASC'
    }

    this.selectedCls(this.sortColumn)
   this.getAllUncheckedVehicleDetails();
 }

 

 selectedCls(column) {
   if (column ===  this.sortColumn.sortBy &&  this.sortColumn.sortOrder === 'DESC') {
     return 'fa-sort-desc';
   } else if (column ===  this.sortColumn.sortBy &&  this.sortColumn.sortOrder === 'ASC') {
     return 'fa-sort-asc';
   }
   return '';
 }
 statusConfirmation(data,checkout) {
  this.confirmationService.confirm(data, `Are you sure want to change the status to` + ' ' +data, 'Yes', 'No')
    .then((confirmed) => {
      if (confirmed === true && data === 'Check Out') {
        
        this.checkoutVehicle(checkout);
      }
      else if (confirmed === true && data === 'Hold') {
        this.hold(checkout);
      }
      else  if (confirmed === true && data === 'Complete') {
        this.complete(checkout);
      }
    })
    .catch(() => { });
}

// Delete Product

  checkoutVehicle(checkout) {
    if (checkout.JobPaymentId === 0) {
      this.message.showMessage({ severity: 'info', title: 'Info', body: MessageConfig.checkOut.paidTicket });
    } else {
      if (checkout.valuedesc === 'Completed') {
        const finalObj = {
          id: checkout.JobId,
          checkOut: true,
          actualTimeOut: new Date()
        };
        this.spinner.show();
        this.checkout.checkoutVehicle(finalObj).subscribe(res => {
          if (res.status === 'Success') {
            this.spinner.hide();

            this.toastr.success(MessageConfig.checkOut.Add, 'Success!');
            this.sortColumn =  { sortBy: ApplicationConfig.Sorting.SortBy.Vehicle, sortOrder: ApplicationConfig.Sorting.SortOrder.Vehicle.order };

            this.getAllUncheckedVehicleDetails();
          }
          else{
            this.spinner.hide();

          }
        }, (err) => {
          this.spinner.hide();
          this.toastr.error(MessageConfig.CommunicationError, 'Error!');
        });
      }
      else {
        this.message.showMessage({ severity: 'info', title: 'Info', body: MessageConfig.checkOut.checkoutRestriction });
      }
    }
  }

  hold(checkout) {
    const finalObj = {
      id: checkout.JobId
    };
    if (checkout.MembershipNameOrPaymentStatus === 'Hold') {
      return;
    }
    this.spinner.show();
    this.checkout.holdVehicle(finalObj).subscribe(res => {
      if (res.status === 'Success') {
        this.spinner.hide();
        this.toastr.success(MessageConfig.checkOut.Hold, 'Success!');
        this.sortColumn =  { sortBy: ApplicationConfig.Sorting.SortBy.CheckOut, sortOrder: ApplicationConfig.Sorting.SortOrder.CheckOut.order };

        this.getAllUncheckedVehicleDetails();
      }
      else{
        this.spinner.hide();
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');

      }
    }, (err) => {
      this.spinner.hide();

      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  complete(checkout) {
    if (checkout.MembershipNameOrPaymentStatus !== 'Completed') {
      const finalObj = {
        id: checkout.JobId
      };
      this.spinner.show();
      this.checkout.completedVehicle(finalObj).subscribe(res => {
        if (res.status === 'Success') {
          this.spinner.hide();

          this.toastr.success(MessageConfig.checkOut.Complete, 'Success!');
          this.sortColumn =  { sortBy: ApplicationConfig.Sorting.SortBy.CheckOut, sortOrder: ApplicationConfig.Sorting.SortOrder.CheckOut.order };

          this.getAllUncheckedVehicleDetails();
        }
        else{
          this.spinner.hide();
          this.toastr.error(MessageConfig.CommunicationError, 'Error!');

        }
      }, (err) => {
        this.spinner.hide();

        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      });
    }
  }

}
