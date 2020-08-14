import { Component, OnInit } from '@angular/core';
import { WashService } from '../../services/data-service/wash.service';

@Component({
  selector: 'app-wash-employees',
  templateUrl: './wash-employees.component.html',
  styleUrls: ['./wash-employees.component.css']
})
export class WashEmployeesComponent implements OnInit {
  employeeCount: any;

  constructor(private wash: WashService) { }

  ngOnInit() {
    this.getDashboardDetails();
  }

  // Get Employee Count
  getDashboardDetails = () => {
    this.wash.data.subscribe((data: any) => {
      if (data.EmployeeCount !== undefined) {
        this.employeeCount = data.EmployeeCount.EmployeeCount;
      }
    });
  }
}
