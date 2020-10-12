import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { CheckoutService } from 'src/app/shared/services/data-service/checkout.service';

@Component({
  selector: 'app-checkout-grid',
  templateUrl: './checkout-grid.component.html',
  styleUrls: ['./checkout-grid.component.css']
})
export class CheckoutGridComponent implements OnInit {
  uncheckedVehicleDetails: any;
  isTableEmpty: boolean;
  page = 1;
  pageSize = 5;
  collectionSize: number = 0;

  constructor(private checkout: CheckoutService, private toastr: MessageServiceToastr) { }

  ngOnInit() {
    this.getAllUncheckedVehicleDetails();
  }

  // Get All Unchecked Vehicles
  getAllUncheckedVehicleDetails() {
    this.checkout.getUncheckedVehicleDetails().subscribe(data => {
      if (data.status === 'Success') {
        const uncheck = JSON.parse(data.resultData);
        this.uncheckedVehicleDetails = uncheck.GetCheckedInVehicleDetails;
        console.log(this.uncheckedVehicleDetails);
        if (this.uncheckedVehicleDetails.length === 0) {
          this.isTableEmpty = true;
        } else {
          this.collectionSize = Math.ceil(this.uncheckedVehicleDetails.length / this.pageSize) * 10;
          this.isTableEmpty = false;
        }
      } else {
        this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error!' });
      }
    });
  }

}
