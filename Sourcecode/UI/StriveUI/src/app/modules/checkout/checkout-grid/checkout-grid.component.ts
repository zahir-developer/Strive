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
  uncheckedVehicleDetails: any = [];
  isTableEmpty: boolean;
  page = 1;
  pageSize = 25;
  collectionSize: number = 0;
  query = '';
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

  checkoutVehicle(checkout) {
    if (checkout.JobPaymentId === 0) {
      this.toastr.showMessage({ severity: 'info', title: 'Info', body: 'Checkout can be done only for paid tickets.' });
    } else {
      if ( checkout.valuedesc === 'Completed') {
        const finalObj = {
          id: checkout.JobId,
          checkOut: true,
          actualTimeOut: new Date()
        };
        this.checkout.checkoutVehicle(finalObj).subscribe(res => {
          if (res.status === 'Success') {
            this.toastr.showMessage({ severity: 'success', title: 'Success', body: 'Checkout successfully' });
            this.getAllUncheckedVehicleDetails();
          }
        });
      }
      else {
        this.toastr.showMessage({ severity: 'info', title: 'Info', body: 'Checkout should allow only completed tickets.' });
      }
    }
  }

  hold(checkout) {
    const finalObj = {
      id: checkout.JobId
    };
    if (checkout.MembershipNameOrPaymentStatus === 'Hold') {
      return;
    }
    this.checkout.holdVehicle(finalObj).subscribe(res => {
      if (res.status === 'Success') {
        this.toastr.showMessage({ severity: 'success', title: 'Success', body: 'Hold successfully' });
        this.getAllUncheckedVehicleDetails();
      }
    });
  }

  complete(checkout) {
    if (checkout.MembershipNameOrPaymentStatus !== 'Completed') {
      const finalObj = {
        id: checkout.JobId
      };
      this.checkout.completedVehicle(finalObj).subscribe(res => {
        if (res.status === 'Success') {
          this.toastr.showMessage({ severity: 'success', title: 'Success', body: 'Completed Successfully' });
          this.getAllUncheckedVehicleDetails();
        }
      });
    }
  }

}
