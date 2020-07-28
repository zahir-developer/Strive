import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';


@Component({
  selector: 'app-employee-collision',
  templateUrl: './employee-collision.component.html',
  styleUrls: ['./employee-collision.component.css']
})
export class EmployeeCollisionComponent implements OnInit {

  constructor(private activeModal: NgbActiveModal) { }

  ngOnInit(): void {
  }

  closeModal() {
    this.activeModal.close();
  }

}
