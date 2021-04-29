import { Component, Input, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { EmployeeService } from 'src/app/shared/services/data-service/employee.service';

@Component({
  selector: 'app-employee-hourly-rate',
  templateUrl: './employee-hourly-rate.component.html',
  styleUrls: ['./employee-hourly-rate.component.css']
})
export class EmployeeHourlyRateComponent implements OnInit {
  @Input() employeeId?: any;
  constructor(
    private employeeService: EmployeeService,
    private modalService: NgbModal
  ) { }

  ngOnInit(): void {
    this.getEmployeeHourlyRateById();
  }

  close() {
    this.modalService.dismissAll();
  }

  getEmployeeHourlyRateById() {
    const id = this.employeeId;
    this.employeeService.getEmployeeHourlyRateById(id).subscribe( res => {

    });
  }

}
