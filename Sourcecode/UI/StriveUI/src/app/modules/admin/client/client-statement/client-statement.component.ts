import { Component, OnInit, Input } from '@angular/core';
import { NgbModal, NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ClientService } from 'src/app/shared/services/data-service/client.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { MessageConfig } from 'src/app/shared/services/messageConfig';
import { ToastrService } from 'ngx-toastr';
import { ApplicationConfig } from 'src/app/shared/services/ApplicationConfig';

@Component({
  selector: 'app-client-statement',
  templateUrl: './client-statement.component.html',
  styles: [`
  .table-ellipsis {
    overflow: hidden;
    white-space: nowrap;
    text-overflow: ellipsis;
    max-width: 150px;
  }
  `]
})
export class ClientStatementComponent implements OnInit {
  @Input() clientId?: any;
  @Input() statementData?: any;
  statementGrid: any = [];
  page: number;
  pageSize: number;
  collectionSize: number;
  sort = { column: 'Date', descending: true };
  sortColumn: { column: string; descending: boolean; };
  constructor(
    private modalService: NgbModal,
    private activeModal: NgbActiveModal,
    private spinner: NgxSpinnerService,
private toastr : ToastrService,
    private client: ClientService
  ) { }

  ngOnInit(): void {
    this.page = ApplicationConfig.PaginationConfig.page;
    this.pageSize = ApplicationConfig.PaginationConfig.TableGridSize;
    this.statementGrid = this.statementData;
    this.collectionSize = Math.ceil(this.statementGrid.length / this.pageSize) * 10;
    //this.getStatement();
  }

  closeDocumentModel() {
    this.activeModal.close();
  }

  getStatement() {
    this.client.getStatementByClientId(this.clientId).subscribe( res => {
      if (res.status === 'Success') {
        const statement = JSON.parse(res.resultData);
        this.statementGrid = statement.VehicleStatement;
        this.collectionSize = Math.ceil(this.statementGrid.length / this.pageSize) * 10;
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }
}
