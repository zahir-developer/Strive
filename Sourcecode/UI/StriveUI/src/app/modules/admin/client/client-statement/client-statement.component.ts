import { Component, OnInit } from '@angular/core';
import { NgbModal, NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-client-statement',
  templateUrl: './client-statement.component.html',
  styleUrls: ['./client-statement.component.css']
})
export class ClientStatementComponent implements OnInit {

  constructor(
    private modalService: NgbModal,
    private activeModal: NgbActiveModal
  ) { }

  ngOnInit(): void {
  }

  closeDocumentModel() {
    this.activeModal.close();
  }

}
