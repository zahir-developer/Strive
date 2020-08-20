import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ClientService } from 'src/app/shared/services/data-service/client.service';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-client-history',
  templateUrl: './client-history.component.html',
  styleUrls: ['./client-history.component.css']
})
export class ClientHistoryComponent implements OnInit {
  @Input() clientId?: any;
  constructor(
    private activeModal: NgbActiveModal,
    private client: ClientService,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit(): void {
    this.getHistory();
  }

  closeHistoryModel() {
    this.activeModal.close();
  }

  getHistory() {
    this.spinner.show();
    this.client.getHistoryByClientId(this.clientId).subscribe(res => {
      this.spinner.hide();
      if (res.status === 'Success') {
        const history = JSON.parse(res.resultData);
        console.log(history, 'history');
      }
    });
  }

}
