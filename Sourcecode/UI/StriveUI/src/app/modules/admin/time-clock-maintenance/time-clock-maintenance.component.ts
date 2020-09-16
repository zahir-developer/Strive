import { Component, OnInit } from '@angular/core';
import { TimeClockMaintenanceService } from 'src/app/shared/services/data-service/time-clock-maintenance.service';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationUXBDialogService } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.service';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import { element } from 'protractor';

@Component({
  selector: 'app-time-clock-maintenance',
  templateUrl: './time-clock-maintenance.component.html',
  styleUrls: ['./time-clock-maintenance.component.css']
})
export class TimeClockMaintenanceComponent implements OnInit {

  timeClockEmployeeDetails = [];
  isLoading = true;
  page = 1;
  pageSize = 5;
  collectionSize: number = 0;
  isTimeClockEmpty = false;
  timeClockEmployeeDetailDto =
  {
      locationId: 0,
      startDate: '2020-08-15',
      endDate: '2020-09-09'
  }
  objDelete: any;

  constructor(private timeClockMaintenanceService: TimeClockMaintenanceService, private toastr: ToastrService,
    private confirmationService: ConfirmationUXBDialogService, private uiLoaderService: NgxUiLoaderService) { }

  ngOnInit(): void {
    this.getTimeClockEmployeeDetails()
  }

  getTimeClockEmployeeDetails() {

    const obj = this.timeClockEmployeeDetailDto;
    this.isLoading = true;
    this.timeClockMaintenanceService.getTimeClockEmployeeDetails(obj).subscribe(data => {
      if (data.status === 'Success') {
        const timeClock = JSON.parse(data.resultData);
        this.timeClockEmployeeDetails = timeClock.Result;

        if (this.timeClockEmployeeDetails.length === 0) {
          this.isTimeClockEmpty = true;
          console.log(this.timeClockEmployeeDetails);
        }
        else {
          this.collectionSize = Math.ceil(this.timeClockEmployeeDetails.length / this.pageSize) * 10;
          this.isTimeClockEmpty = false;
          console.log(this.timeClockEmployeeDetails);
        }
      }
      else {
        this.toastr.error('Communication Error', 'Error !!!')
      }
    });
  }

  deleteConfirm(obj)
  {
      this.confirmationService.confirm('Delete Location', `Are you sure you want to delete this location? All related 
      information will be deleted and the location cannot be retrieved?`, 'Yes', 'No')
        .then((confirmed) => {
          if (confirmed === true) {
            this.deleteTimeClockEmployee(obj);
          }
        })
        .catch(() => { });
    } 


    deleteTimeClockEmployee(obj) 
    {
      this.objDelete =
      {
      locationId: obj.LocationId,
      employeeId: obj.EmployeeId
      }

      this.timeClockMaintenanceService.deleteTimeClockEmployee(this.objDelete).subscribe( data => 
        {
          if (data.status === 'Success') {
            this.toastr.success('Employee record deleted successfully!!', 'Success!');
            this.getTimeClockEmployeeDetails();
          }
          else {
            this.toastr.error('Communication Error', 'Error !!!')
          }
        });
    }

  

}
