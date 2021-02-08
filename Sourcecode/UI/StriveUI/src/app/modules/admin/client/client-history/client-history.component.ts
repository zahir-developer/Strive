import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ClientService } from 'src/app/shared/services/data-service/client.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ApplicationConfig } from 'src/app/shared/services/ApplicationConfig';

@Component({
  selector: 'app-client-history',
  templateUrl: './client-history.component.html',
  styleUrls: ['./client-history.component.css']
})
export class ClientHistoryComponent implements OnInit {
  @Input() clientId?: any;
  historyGrid: any = [];
 
  collectionSize: number;
  sort = { column: 'Date', descending: true };
  sortColumn: { column: string; descending: boolean; };
  page: number;
  pageSize: number;
  pageSizeList: number[];
  constructor(
    private activeModal: NgbActiveModal,
    private client: ClientService,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit(): void {
    this.page = ApplicationConfig.PaginationConfig.page;
    this.pageSize = ApplicationConfig.PaginationConfig.TableGridSize;
    this.pageSizeList = ApplicationConfig.PaginationConfig.Rows;
    this.getHistory();
  }

  closeHistoryModel() {
    this.activeModal.close();
  }
  paginate(event) {
    this.pageSize = +this.pageSize;
    this.page = event;
    this.getHistory();
  }
  paginatedropdown(event) {
    this.pageSize = +event.target.value;
    this.page = this.page;
    this.getHistory();
  }
  getHistory() {
    const obj = {
      clientId: this.clientId,
      PageNo: this.page,
      PageSize: this.pageSize,
     
    };
    this.client.getHistoryByClientId(obj).subscribe(res => {
      if (res.status === 'Success') {
        const history = JSON.parse(res.resultData);
        this.historyGrid = history.VehicleHistory.clientVehicleHistoryViewModel;
        const totalRowCount = history.VehicleHistory.Count.Count;

        this.collectionSize = Math.ceil(totalRowCount / this.pageSize) * 10;
        console.log(history, 'history');
      }
    });
  }

  changeSorting(column) {
    this.changeSortingDescending(column, this.sort);
    this.sortColumn = this.sort;
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

}
