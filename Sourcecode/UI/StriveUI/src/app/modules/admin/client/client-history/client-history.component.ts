import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ClientService } from 'src/app/shared/services/data-service/client.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { MessageConfig } from 'src/app/shared/services/messageConfig';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-client-history',
  templateUrl: './client-history.component.html',
  styleUrls: ['./client-history.component.css']
})
export class ClientHistoryComponent implements OnInit {
  @Input() clientId?: any;
  historyGrid: any = [];
  page = 1;
  pageSize = 5;
  collectionSize: number;
  sort = { column: 'Date', descending: true };
  sortColumn: { column: string; descending: boolean; };
  constructor(
    private activeModal: NgbActiveModal,
    private client: ClientService,
    private spinner: NgxSpinnerService,
    private toastr : ToastrService
  ) { }

  ngOnInit(): void {
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
        this.collectionSize = Math.ceil(this.historyGrid.length / this.pageSize) * 10;
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

 
}
