import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import * as moment from 'moment';
import { ApplicationConfig } from 'src/app/shared/services/ApplicationConfig';
import { CheckoutService } from 'src/app/shared/services/data-service/checkout.service';
import { LandingService } from 'src/app/shared/services/common-service/landing.service';
import { MessageConfig } from 'src/app/shared/services/messageConfig';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-customer-history',
  templateUrl: './customer-history.component.html',
  styleUrls: ['./customer-history.component.css']
})
export class CustomerHistoryComponent implements OnInit {
  date = new Date();
  month: any;
  year: number;
  locationId = +localStorage.getItem('empLocationId');
  page: any;
  pageSize: any;
  pageSizeList: any;
  collectionSize: number;
  historyList: any = [];
  offset1 = false;
  searchQery: any = '';
  months = [
    { val: '0', name: 'All' },
    { val: '1', name: 'Jan' },
    { val: '2', name: 'Feb' },
    { val: '3', name: 'Mar' },
    { val: '4', name: 'Apr' },
    { val: '5', name: 'May' },
    { val: '6', name: 'Jun' },
    { val: '7', name: 'Jul' },
    { val: '8', name: 'Aug' },
    { val: '9', name: 'Sep' },
    { val: '10', name: 'Oct' },
    { val: '11', name: 'Nov' },
    { val: '12', name: 'Dec' }
  ];
  sortColumn: { sortBy: any; sortOrder: string; };
  constructor(
    private checkout: CheckoutService,
    private router: Router
    , private landingservice: LandingService,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.month = '0';
    this.page = ApplicationConfig.PaginationConfig.page;
    this.pageSize = ApplicationConfig.PaginationConfig.TableGridSize;
    this.pageSizeList = ApplicationConfig.PaginationConfig.Rows;
    this.year = this.date.getFullYear();
    this.sortColumn = {
      sortBy: ApplicationConfig.Sorting.SortBy.customerHistory,
      sortOrder: ApplicationConfig.Sorting.SortOrder.customerHistory.order
    };
    this.getCustomerHistory();

  }
  landing() {
    this.landingservice.loadTheLandingPage()
  }
  onYearChange(event) {
    this.year = event;
  }

  getCustomerHistory() {
    // 2053 ,2020-12-01,2021-01-21
    let finalObj: any = {};

    if (this.month === '0') {
      const fromDate = new Date();
      fromDate.setFullYear(this.year);
      fromDate.setMonth(0);
      fromDate.setDate(1);
      const toDate = new Date();
      toDate.setFullYear(this.year);
      toDate.setMonth(11);
      toDate.setDate(31);
      finalObj = {
        locationId: +this.locationId, // 2053, // +this.locationId,
        fromDate: moment(fromDate).format('yyyy-MM-DD'),
        endDate: moment(toDate).format('yyyy-MM-DD'),
        pageNo: this.page,
        pageSize: this.pageSize,
        query: this.searchQery !== '' ? this.searchQery : null,
        sortOrder: this.sortColumn.sortOrder,
        sortBy: this.sortColumn.sortBy
      };
    } else {
      const fromDate = new Date();
      fromDate.setFullYear(this.year);
      fromDate.setMonth(0);
      fromDate.setDate(1);
      const toDate = new Date();
      toDate.setFullYear(this.year);
      const month = +this.month - 1;
      toDate.setMonth(month);
      const lastDate = moment(new Date(this.year, month + 1, 0)).format('DD');
      toDate.setDate(+lastDate);
      finalObj = {
        locationId: +this.locationId, // 2053, // +this.locationId,
        fromDate: moment(fromDate).format('yyyy-MM-DD'),
        endDate: moment(toDate).format('yyyy-MM-DD'),
        pageNo: this.page,
        pageSize: this.pageSize,
        query: this.searchQery !== '' ? this.searchQery : null,
        sortOrder: this.sortColumn.sortOrder,
        sortBy: this.sortColumn.sortBy
      };
    }
    this.checkout.getCustomerHistory(finalObj).subscribe(res => {
      if (res.status === 'Success') {
        const history = JSON.parse(res.resultData);
        console.log(history, 'history');
        this.historyList = history.CustomerHistory.customerHistoryViewModel;
        const totalCount = history.CustomerHistory.Count.Count;
        this.historyList.filter( item => {
          if (item.MembershipName === '') {
            item.MembershipName = 'No';
          }
        });
        this.collectionSize = Math.ceil(totalCount / this.pageSize) * 10;
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  paginate(event) {
    this.pageSize = +this.pageSize;
    this.page = event;
    this.getCustomerHistory();
  }
  paginatedropdown(event) {
    this.pageSize = +event.target.value;
    this.page = this.page;
    this.getCustomerHistory();
  }

  preview() {
    this.getCustomerHistory();
  }

  monthChange(event) {

  }

  navigateToClient() {
    this.router.navigate(['/admin/client'], { queryParams: { clientId: 88 } });  // need to change
  }

  navigateToVehicle() {
    this.router.navigate(['/admin/vehicle'], { queryParams: { vehicleId: 36 } });  // need to change
  }

  search() {
    this.getCustomerHistory();
  }
  changeSorting(column) {
    this.sortColumn = {
      sortBy: column,
      sortOrder: this.sortColumn.sortOrder == 'ASC' ? 'DESC' : 'ASC'
    }

    this.selectedCls(this.sortColumn)
    this.getCustomerHistory();
  }



  selectedCls(column) {
    if (column === this.sortColumn.sortBy && this.sortColumn.sortOrder === 'DESC') {
      return 'fa-sort-desc';
    } else if (column === this.sortColumn.sortBy && this.sortColumn.sortOrder === 'ASC') {
      return 'fa-sort-asc';
    }
    return '';
  }

}
