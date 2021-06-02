import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal, NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { ClientService } from 'src/app/shared/services/data-service/client.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { MessageConfig } from 'src/app/shared/services/messageConfig';
import { ToastrService } from 'ngx-toastr';
import { ServiceListComponent } from '../service-list/service-list.component';
import { ApplicationConfig } from 'src/app/shared/services/ApplicationConfig';

@Component({
  selector: 'app-client-history',
  templateUrl: './client-history.component.html',
  styles: [`
  .table-ellipsis {
    overflow: hidden;
    white-space: nowrap;
    text-overflow: ellipsis;
    max-width: 150px;
  }
  `]
})
export class ClientHistoryComponent implements OnInit {
  @Input() clientId?: any;
  historyGrid: any = [];
  page: number;
  pageSize: number;
  collectionSize: number;
  sort = { column: 'Date', descending: true };
  sortColumn: { column: string; descending: boolean; };
  clonedHistoryGrid = [];
  constructor(
    private activeModal: NgbActiveModal,
    private client: ClientService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService,
    private modalService: NgbModal
  ) { }

  ngOnInit(): void {
    this.page = ApplicationConfig.PaginationConfig.page;
    this.pageSize = ApplicationConfig.PaginationConfig.TableGridSize;
    this.getHistory();
  }

  closeHistoryModel() {
    this.activeModal.close();
  }

  getHistory() {
    this.client.getHistoryByClientId(this.clientId).subscribe(res => {
      if (res.status === 'Success') {
        const history = JSON.parse(res.resultData);
        this.historyGrid = history.VehicleHistory;
        this.clonedHistoryGrid = this.historyGrid.map(x => Object.assign({}, x));
        this.historyGrid = this.historyGrid.filter(item => item.ServiceType === ApplicationConfig.Enum.ServiceType.WashPackage ||
          item.ServiceType === ApplicationConfig.Enum.ServiceType.DetailPackage);
        this.collectionSize = Math.ceil(this.historyGrid.length / this.pageSize) * 10;
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  paginate(event) {
    this.pageSize = +this.pageSize;
    this.page = event;
  }

  openHistory(data) {
    const ngbModalOptions: NgbModalOptions = {
      backdrop: 'static',
      keyboard: false,
      size: 'lg'
    };
    const ticketDetail = this.clonedHistoryGrid.filter(item => item.TicketNumber === data.TicketNumber);
    const modalRef = this.modalService.open(ServiceListComponent, ngbModalOptions);
    modalRef.componentInstance.historyGrid = ticketDetail;
    modalRef.componentInstance.ticketNumber = data.TicketNumber;
  }


}
