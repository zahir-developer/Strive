import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ApplicationConfig } from 'src/app/shared/services/ApplicationConfig';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { CheckoutService } from 'src/app/shared/services/data-service/checkout.service';
import { MessageConfig } from 'src/app/shared/services/messageConfig';
import { LandingService } from 'src/app/shared/services/common-service/landing.service';

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
  isDesc: boolean = false;
  search = '';
  sort = { column: 'TicketNumber', descending: false };
  sortColumn: { column: string; descending: boolean; }; 
   query = '';

  constructor(
    private checkout: CheckoutService,
    private message: MessageServiceToastr,
    private toastr: ToastrService,
private landingservice: LandingService,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit() {
    this.page = ApplicationConfig.PaginationConfig.page;
    this.pageSize = ApplicationConfig.PaginationConfig.TableGridSize;
    this.pageSizeList = ApplicationConfig.PaginationConfig.Rows;
    this.getAllUncheckedVehicleDetails();
  }
  landing(){
    this.landingservice.loadTheLandingPage()
  }
  // Get All Unchecked Vehicles
  getAllUncheckedVehicleDetails() {
    const obj = {
      locationId : localStorage.getItem('empLocationId'),
      startDate: null,
      endDate: null,
      pageNo: this.page,
      pageSize: this.pageSize,
      query: this.search,
      sortOrder: this.sort.descending ? 'DESC' : 'ASC',
      sortBy: this.sort.column,
      status: true
    };
    this.spinner.show();
    this.checkout.getUncheckedVehicleDetails(obj).subscribe(data => {
      this.spinner.hide();
      if (data.status === 'Success') {
        const uncheck = JSON.parse(data.resultData);
        this.uncheckedVehicleDetails = uncheck.GetCheckedInVehicleDetails.checkOutViewModel;
        if (this.uncheckedVehicleDetails == null) {
          this.isTableEmpty = true;
        } else {
          this.collectionSize = Math.ceil(uncheck.GetCheckedInVehicleDetails.Count.Count / this.pageSize) * 10;
          this.isTableEmpty = false;
        }
      } else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.spinner.hide();
    });
  }
  paginate(event) {
    this.pageSize = +this.pageSize;
    this.page = event;
    this.getAllUncheckedVehicleDetails();
  }
  checkOutSearch() {
    this.search =this.query 
    
    this.getAllUncheckedVehicleDetails();
  }
  paginatedropdown(event) {
    this.pageSize = +event.target.value;
    this.page = this.page;
    this.getAllUncheckedVehicleDetails();
  }
  changeSorting(column) {
    this.changeSortingDescending(column, this.sort);
    this.sortColumn = this.sort;
    this.getAllUncheckedVehicleDetails();
  }

  changeSortingDescending(column, sortingInfo) {
    if (sortingInfo.column === column) {
      sortingInfo.descending = !sortingInfo.descending;
    } else {
      sortingInfo.column = column;
      sortingInfo.descending = false;
    }
    return sortingInfo;
  }

  sortedColumnCls(column, sortingInfo) {
    if (column === sortingInfo.column && sortingInfo.descending) {
      return 'fa-sort-desc';
    } else if (column === sortingInfo.column && !sortingInfo.descending) {
      return 'fa-sort-asc';
    }
    return '';
  }

  selectedCls(column) {
    return this.sortedColumnCls(column, this.sort);
  }
   
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
        this.checkout.checkoutVehicle(finalObj).subscribe(res => {
          if (res.status === 'Success') {
            this.toastr.success( MessageConfig.checkOut.Add,'Success!');
            this.getAllUncheckedVehicleDetails();
          }
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
    this.checkout.holdVehicle(finalObj).subscribe(res => {
      if (res.status === 'Success') {
        this.toastr.success(MessageConfig.checkOut.Hold, 'Success!');
        this.getAllUncheckedVehicleDetails();
      }
    });
  }

  complete(checkout) {
    if (checkout.MembershipNameOrPaymentStatus !== 'Completed') {
      const finalObj = {
        id: checkout.JobId
      };
      this.checkout.completedVehicle(finalObj).subscribe(res => {
        if (res.status === 'Success') {
          this.toastr.success(MessageConfig.checkOut.Complete , 'Success!');
          this.getAllUncheckedVehicleDetails();
        }
      });
    }
  }

}
