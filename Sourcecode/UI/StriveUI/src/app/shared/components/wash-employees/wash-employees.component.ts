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
    const obj = {
      id: +localStorage.getItem('empLocationId'),
      date: new Date()
    };
    this.wash.getDashBoard(obj);
    this.wash.dashBoardData.subscribe((data: any) => {
        this.employeeCount = data.EmployeeCount;
    });
  }
}
