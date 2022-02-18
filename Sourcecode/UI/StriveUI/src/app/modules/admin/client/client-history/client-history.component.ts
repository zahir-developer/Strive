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
  @Input() historyData?: any;
  historyGrid: any = [];
  page: number;
  pageSize: number;
  openingBalance: number;
  closingBalance: number;
  collectionSize: number;
  sort = { column: 'Date', descending: true };
  sortColumn: { column: string; descending: boolean; };
  clonedHistoryGrid = [];  
  historyCloned =[];
  fromDate = new Date();
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
    const capObj = {
      clientId: this.clientId,
      year: 0,
      month: this.fromDate.getMonth() + 1
    };
    this.getHistory(capObj);
    this.historyGrid = this.historyData;
    this.historyCloned = this.historyData;
  }

  closeHistoryModel() {
    this.activeModal.close();
  }

  getHistory(capObj) {
    this.client.getClientAccountBalance(capObj).subscribe(res => {
      if (res.status === 'Success') {
        const bal = JSON.parse(res.resultData);
        this.openingBalance = bal.AccountBalance[0].OpeningBalance;
        this.closingBalance = bal.AccountBalance[0].ClosingBalance;
      
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

  onMonthChange(event) {
    const date = new Date();
    date.setMonth(event - 1);
    this.fromDate = new Date(date.getFullYear(), date.getMonth(), 1);
  }
  onYearChange(event) {
    this.fromDate.setFullYear(event);
  }

  Print(){
    const printContent = document.getElementById("clientHistory");
    const WindowPrt = window.open('', '', 'left=0,top=0,width=900,height=900,toolbar=0,scrollbars=0,status=0');
    WindowPrt.document.write(printContent.innerHTML);
    WindowPrt.document.close();
    WindowPrt.focus();
    WindowPrt.print();
  }
  FilterRecords(){ 
    const capObj = {
      clientId: this.clientId,
      year: this.fromDate.getFullYear(),
      month: this.fromDate.getMonth() + 1
    };
   this.getHistory(capObj);
    this.historyCloned = this.historyGrid
    .filter(x => new Date(x.CreatedDate).getMonth() == this.fromDate.getMonth() && new Date(x.CreatedDate).getFullYear() == this.fromDate.getFullYear())
  }
}
