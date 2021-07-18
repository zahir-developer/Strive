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
import { DatePipe } from '@angular/common';
import * as moment from 'moment';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

@Component({
  selector: 'app-checkout-grid',
  templateUrl: './checkout-grid.component.html'
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
  @ViewChild('dp', { static: false }) datepicker: BsDaterangepickerDirective;
  bsConfig: Partial<BsDatepickerConfig>;
  sortColumn: { sortBy: string; sortOrder: string; };
  currentWeek: Date;
  daterangepickerModel: Date[];
  timeOut: Date;
  searchUpdate = new Subject<string>();
  constructor(
    private checkout: CheckoutService,
    private message: MessageServiceToastr,
    private toastr: ToastrService,
    private confirmationService: ConfirmationUXBDialogService,
    private datePipe: DatePipe,
    private landingservice: LandingService,
    private spinner: NgxSpinnerService
  ) {
    // Debounce search.
    this.searchUpdate.pipe(
      debounceTime(ApplicationConfig.debounceTime.sec),
      distinctUntilChanged())
      .subscribe(value => {
        this.getAllUncheckedVehicleDetails();
      });
  }

  ngOnInit() {
    this.sortColumn = { sortBy: ApplicationConfig.Sorting.SortBy.CheckOut, sortOrder: ApplicationConfig.Sorting.SortOrder.CheckOut.order };
    this.timeOut = new Date();
    const currentDate = new Date();
    const first = currentDate.getDate();
    const last = currentDate.getDate();
    this.startDate = new Date(currentDate.setDate(last));
    this.currentWeek = this.startDate;
    const lastDate = new Date();
    this.endDate = new Date();
    this.daterangepickerModel = [this.startDate, this.endDate];
    this.page = ApplicationConfig.PaginationConfig.page;
    this.pageSize = ApplicationConfig.PaginationConfig.TableGridSize;
    this.pageSizeList = ApplicationConfig.PaginationConfig.Rows;
  }
  landing() {
    this.landingservice.loadTheLandingPage();
  }
  onValueChange(event) {
    if (event !== null) {
      this.startDate = event[0];
      this.endDate = event[1];
      this.getAllUncheckedVehicleDetails();
    }
    else {
      this.startDate = null;
      this.endDate = null;
    }
  }
  // Get All Unchecked Vehicles
  getAllUncheckedVehicleDetails() {
    this.search = this.query;
    const obj = {
      locationId: localStorage.getItem('empLocationId'),
      StartDate: this.datePipe.transform(this.startDate, 'yyyy-MM-dd'),
      EndDate: this.datePipe.transform(this.endDate, 'yyyy-MM-dd'),
      pageNo: this.page,
      pageSize: this.pageSize,
      query: this.search === '' ? null : this.search,
      sortOrder: this.sortColumn.sortOrder,
      sortBy: this.sortColumn.sortBy,
      status: true
    };
    this.spinner.show();
    this.checkout.getUncheckedVehicleDetails(obj).subscribe(data => {
      if (data.status === 'Success') {
        this.spinner.hide();

        const uncheck = JSON.parse(data.resultData);
        if (uncheck.GetCheckedInVehicleDetails.checkOutViewModel !== null) {
          this.uncheckedVehicleDetails = uncheck.GetCheckedInVehicleDetails.checkOutViewModel;
          if (this.uncheckedVehicleDetails?.length > 0) {
            for (let i = 0; i < this.uncheckedVehicleDetails.length; i++) {
              this.uncheckedVehicleDetails[i].VehicleModel === 'None' ?
               this.uncheckedVehicleDetails[i].VehicleModel = 'Unk' : this.uncheckedVehicleDetails[i].VehicleModel;
            }
          }
          this.collectionSize = Math.ceil(uncheck.GetCheckedInVehicleDetails.Count.Count / this.pageSize) * 10;
        }

        if (this.uncheckedVehicleDetails == null) {
          this.isTableEmpty = true;
        } else {
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
    this.page = 1;
    this.getAllUncheckedVehicleDetails();
  }
  changeSorting(column) {
    this.sortColumn = {
      sortBy: column,
      sortOrder: this.sortColumn.sortOrder == 'ASC' ? 'DESC' : 'ASC'
    };

    this.selectedCls(this.sortColumn)
    this.getAllUncheckedVehicleDetails();
  }



  selectedCls(column) {
    if (column === this.sortColumn.sortBy && this.sortColumn.sortOrder === 'DESC') {
      return 'fa-sort-desc';
    } else if (column === this.sortColumn.sortBy && this.sortColumn.sortOrder === 'ASC') {
      return 'fa-sort-asc';
    }
    return '';
  }
  statusConfirmation(data, checkout) {

    if (data === 'Check Out') {
      this.checkoutVehicle(data, checkout);
    }
    else if (data === 'Hold') {
      this.hold(data, checkout);
    }
    else if (data === 'Complete') {
      this.complete(data, checkout);
    }

  }

  // Delete Product

  checkoutVehicle(data, checkout) {

    if (checkout.valuedesc === 'Completed') {

      if (checkout.MembershipNameOrPaymentStatus == 'Paid') {

        this.confirmationService.confirm(data, `Are you sure want to change the status to` + ' ' + data, 'Yes', 'No')
          .then((confirmed) => {
            const checkoutTime = new Date();
            const finalObj = {
              jobId: checkout.JobId,
              checkOut: true,
              checkOutTime: checkoutTime
            };
            this.spinner.show();
            this.checkout.checkoutVehicle(finalObj).subscribe(res => {
              if (res.status === 'Success') {
                this.spinner.hide();
                this.toastr.success(MessageConfig.checkOut.Add, 'Success!');
                this.sortColumn = {
                  sortBy: ApplicationConfig.Sorting.SortBy.Vehicle,
                  sortOrder: ApplicationConfig.Sorting.SortOrder.Vehicle.order
                };

                this.getAllUncheckedVehicleDetails();
              }
              else {
                this.spinner.hide();

              }
            }, (err) => {
              this.spinner.hide();
              this.toastr.error(MessageConfig.CommunicationError, 'Error!');
            });

          })
          .catch(() => { });
      }
      else {
        this.message.showMessage({ severity: 'warning', title: 'Not Paid', body: MessageConfig.checkOut.unPaidTicket });
      }
    }
    else {
      this.message.showMessage({ severity: 'info', title: 'In-Progress', body: MessageConfig.checkOut.checkoutRestriction });
    }

  }

  hold(data, checkout) {
    this.confirmationService.confirm(data, `Are you sure want to change the status to` + ' ' + data, 'Yes', 'No')
      .then((confirmed) => {
        debugger
        const finalObj = {
          id: checkout.JobId,
          IsHold: checkout.IsHold == true ? true : false

        };
        if (checkout.MembershipNameOrPaymentStatus === 'Hold') {
          return;
        }
        this.spinner.show();
        this.checkout.holdVehicle(finalObj).subscribe(res => {
          if (res.status === 'Success') {
            this.spinner.hide();
            this.toastr.success(MessageConfig.checkOut.Hold, 'Success!');
            this.sortColumn = { sortBy: ApplicationConfig.Sorting.SortBy.CheckOut, sortOrder: ApplicationConfig.Sorting.SortOrder.CheckOut.order };

            this.getAllUncheckedVehicleDetails();
          }
          else {
            this.spinner.hide();
            this.toastr.error(MessageConfig.CommunicationError, 'Error!');

          }
        }, (err) => {
          this.spinner.hide();

          this.toastr.error(MessageConfig.CommunicationError, 'Error!');
        });
      })
      .catch(() => { });

  }

  complete(data, checkout) {

    this.confirmationService.confirm(data, `Are you sure want to change the status to` + ' ' + data, 'Yes', 'No')
      .then((confirmed) => {

        if (checkout.MembershipNameOrPaymentStatus !== 'Completed') {
          const finalObj = {
            jobId: checkout.JobId,
            ActualTimeOut: moment(this.timeOut).format()
          };
          this.spinner.show();
          this.checkout.completedVehicle(finalObj).subscribe(res => {
            if (res.status === 'Success') {
              this.spinner.hide();

              this.toastr.success(MessageConfig.checkOut.Complete, 'Success!');
              this.sortColumn = { sortBy: ApplicationConfig.Sorting.SortBy.CheckOut, sortOrder: ApplicationConfig.Sorting.SortOrder.CheckOut.order };

              this.getAllUncheckedVehicleDetails();
            }
            else {
              this.spinner.hide();
              this.toastr.error(MessageConfig.CommunicationError, 'Error!');

            }
          }, (err) => {
            this.spinner.hide();

            this.toastr.error(MessageConfig.CommunicationError, 'Error!');
          });
        }
      })
      .catch(() => { });

  }
}
