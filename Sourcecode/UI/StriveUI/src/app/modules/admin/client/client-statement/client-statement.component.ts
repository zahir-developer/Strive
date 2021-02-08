import { Component, OnInit, Input } from '@angular/core';
import { NgbModal, NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ClientService } from 'src/app/shared/services/data-service/client.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ApplicationConfig } from 'src/app/shared/services/ApplicationConfig';

@Component({
  selector: 'app-client-statement',
  templateUrl: './client-statement.component.html',
  styleUrls: ['./client-statement.component.css']
})
export class ClientStatementComponent implements OnInit {
  @Input() clientId?: any;
  statementGrid: any = [];

  collectionSize: number;
  sort = { column: 'Date', descending: true };
  sortColumn: { column: string; descending: boolean; };
  page: number;
  pageSize: number;
  pageSizeList: number[];
  constructor(
    private modalService: NgbModal,
    private activeModal: NgbActiveModal,
    private spinner: NgxSpinnerService,

    private client: ClientService
  ) { }

  ngOnInit(): void {
    this.page = ApplicationConfig.PaginationConfig.page;
    this.pageSize = ApplicationConfig.PaginationConfig.TableGridSize;
    this.pageSizeList = ApplicationConfig.PaginationConfig.Rows;
    this.getStatement();
  }

  closeDocumentModel() {
    this.activeModal.close();
  }
  paginate(event) {
    this.pageSize = +this.pageSize;
    this.page = event;
    this.getStatement();
  }
  paginatedropdown(event) {
    this.pageSize = +event.target.value;
    this.page = this.page;
    this.getStatement();
  }
  getStatement() {
    const obj = {
      clientId: this.clientId,
      PageNo: this.page,
      PageSize: this.pageSize,
     
    };
    this.client.getStatementByClientId(obj).subscribe( res => {
      if (res.status === 'Success') {
        const statement = JSON.parse(res.resultData);
        this.statementGrid = statement.VehicleStatement.clientStatementViewModel;
        const totalRowCount = statement.VehicleStatement.Count.Count;

        this.collectionSize = Math.ceil(totalRowCount / this.pageSize) * 10;
        console.log(statement, 'statement');
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
