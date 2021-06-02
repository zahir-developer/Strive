import { Component, OnInit } from '@angular/core';
import { DetailService } from 'src/app/shared/services/data-service/detail.service';

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html'
})
export class CustomerComponent implements OnInit {
  dashboardScreen: boolean;
  servicesScreen: boolean;
  locationScreen: boolean;
  appointmentScreen: boolean;
  previewScreen: boolean;
  confirmationScreen: boolean;
  scheduleDetailObj: any = {};
  selectedData: any;
  constructor(
    private detailService: DetailService
  ) { }

  ngOnInit(): void {
    this.dashboardScreen = true;
    this.servicesScreen = false;
    this.locationScreen = false;
    this.appointmentScreen = false;
    this.previewScreen = false;
    this.confirmationScreen = false;
  }

  selectServcie(event) {
    this.dashboardScreen = false;
    this.servicesScreen = true;
  }

  dashboardPage(event) {
    this.scheduleDetailObj.isEdit = false;
    this.dashboardScreen = true;
    this.servicesScreen = false;
    this.locationScreen = false;
    this.appointmentScreen = false;
    this.previewScreen = false;
    this.confirmationScreen = false;
    this.scheduleDetailObj = {};
  }

  selectLocation(event) {
    this.locationScreen = true;
    this.servicesScreen = false;
  }

  selectionPage(event) {
    this.locationScreen = false;
    this.servicesScreen = true;
  }

  selectAppointment(event) {
    this.locationScreen = false;
    this.appointmentScreen = true;
  }

  previewAppointment(event) {
    this.appointmentScreen = false;
    this.previewScreen = true;
  }

  locationPage(event) {
    this.appointmentScreen = false;
    this.locationScreen = true;
  }

  confirmation(event) {
    this.previewScreen = false;
    this.confirmationScreen = true;
  }

  appointmentPage(event) {
    this.previewScreen = false;
    this.appointmentScreen = true;
  }

  editSchedule(event) {
    this.detailService.getDetailById(event).subscribe(res => {
      if (res.status === 'Success') {
        this.scheduleDetailObj.isEdit = true;
        const details = JSON.parse(res.resultData);
        this.selectedData = details.DetailsForDetailId;
        if (this.selectedData.Details !== null) {
          this.selectedData.Details.VechicleName = this.selectedData.Details.MakeName + ' ' + this.selectedData.Details.ModelName
           + ' ' + this.selectedData.Details.ColorName;
        }
        this.servicesScreen = true;
        this.dashboardScreen = false;
      }
    });
  }

}
