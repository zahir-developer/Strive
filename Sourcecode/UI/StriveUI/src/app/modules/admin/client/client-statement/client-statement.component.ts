import { Component, OnInit, Input } from '@angular/core';
import { NgbModal, NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ClientService } from 'src/app/shared/services/data-service/client.service';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-client-statement',
  templateUrl: './client-statement.component.html',
  styleUrls: ['./client-statement.component.css']
})
export class ClientStatementComponent implements OnInit {
  @Input() clientId?: any;
  statementGrid: any = [];
  page = 1;
  pageSize = 5;
  collectionSize: number;
  sort = { column: 'Date', descending: true };
  sortColumn: { column: string; descending: boolean; };
  constructor(
    private modalService: NgbModal,
    private activeModal: NgbActiveModal,
    private spinner: NgxSpinnerService,

    private client: ClientService
  ) { }

  ngOnInit(): void {
    this.getStatement();
  }

  closeDocumentModel() {
    this.activeModal.close();
  }

  getStatement() {
    this.spinner.show()
    this.client.getStatementByClientId(this.clientId).subscribe( res => {
      this.spinner.hide()
      if (res.status === 'Success') {
        const statement = JSON.parse(res.resultData);
        this.statementGrid = statement.VehicleStatement;
        this.collectionSize = Math.ceil(this.statementGrid.length / this.pageSize) * 10;
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
