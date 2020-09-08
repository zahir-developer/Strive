import { Component, OnInit, Input } from '@angular/core';
import { NgbModal, NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ClientService } from 'src/app/shared/services/data-service/client.service';

@Component({
  selector: 'app-client-statement',
  templateUrl: './client-statement.component.html',
  styleUrls: ['./client-statement.component.css']
})
export class ClientStatementComponent implements OnInit {
  @Input() clientId?: any;
  statementGrid: any = [];
  constructor(
    private modalService: NgbModal,
    private activeModal: NgbActiveModal,
    private client: ClientService
  ) { }

  ngOnInit(): void {
    this.getStatement();
  }

  closeDocumentModel() {
    this.activeModal.close();
  }

  getStatement() {
    this.client.getStatementByClientId(this.clientId).subscribe( res => {
      if (res.status === 'Success') {
        const statement = JSON.parse(res.resultData);
        this.statementGrid = statement.VehicleStatement;
        console.log(statement, 'statement');
      }
    });
  }

}
