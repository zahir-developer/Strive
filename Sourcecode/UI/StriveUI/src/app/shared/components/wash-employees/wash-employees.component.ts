import { Component, OnInit } from '@angular/core';
import { WashService } from '../../services/data-service/wash.service';
import { ToastrService } from 'ngx-toastr';
import { MessageConfig } from '../../services/messageConfig';

@Component({
  selector: 'app-wash-employees',
  templateUrl: './wash-employees.component.html'
})
export class WashEmployeesComponent implements OnInit {
  employeeCount: any;

  constructor(private wash: WashService,
    private toastr: ToastrService) { }

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
    },
     (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }
}
