import { Component, OnInit } from '@angular/core';
import { VendorService } from 'src/app/shared/services/data-service/vendor.service';
import { ConfirmationUXBDialogService } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-vendor-setup-list',
  templateUrl: './vendor-setup-list.component.html',
  styleUrls: ['./vendor-setup-list.component.css']
})
export class VendorSetupListComponent implements OnInit {
  vendorSetupDetails = [];
  showDialog = false;
  selectedData: any;
  headerData: string;
  isEdit: boolean;
  isTableEmpty: boolean;
  constructor(private vendorService: VendorService, private toastr: ToastrService, private confirmationService: ConfirmationUXBDialogService) { }

  ngOnInit() {
    this.getAllvendorSetupDetails();
  }
  getAllvendorSetupDetails() {
    this.vendorService.getVendor().subscribe(data => {
      if (data.status === 'Success') {
        const vendor = JSON.parse(data.resultData);
        this.vendorSetupDetails = vendor.Vendor.filter(item => item.IsActive === true);
        if (this.vendorSetupDetails.length === 0) {
          this.isTableEmpty = true;
        } else {
          this.isTableEmpty = false;
        }
      } else {
        this.toastr.error('Communication Error', 'Error!');
      }
    });
  }
  edit(data) {
    this.selectedData = data;
    this.showDialog = true;
  }
  delete(data) {
    this.confirmationService.confirm('Delete Vendor', `Are you sure you want to delete this vendor? All related 
  information will be deleted and the vendor cannot be retrieved?`, 'Yes', 'No')
      .then((confirmed) => {
        if (confirmed === true) {
          this.confirmDelete(data);
        }
      })
      .catch(() => { });
  }

  confirmDelete(data) {
    this.vendorService.deleteVendor(data.VendorId).subscribe(res => {
      if (res.status === "Success") {
        this.toastr.success('Record Deleted Successfully!!', 'Success!');
        this.getAllvendorSetupDetails();
      } else {
        this.toastr.error('Communication Error', 'Error!');
      }
    });
  }
  closePopupEmit(event) {
    if (event.status === 'saved') {
      this.getAllvendorSetupDetails();
    }
    this.showDialog = event.isOpenPopup;
  }
  add(data, vendorDetails?) {
    if (data === 'add') {
      this.headerData = 'Add Vendor Setup';
      this.selectedData = vendorDetails;
      this.isEdit = false;
      this.showDialog = true;
    } else {
      this.getVendorById(vendorDetails);
    }
  }

  getVendorById(data) {
    this.vendorService.getVendorById(data.VendorId).subscribe(data => {
      if (data.status === 'Success') {
        const vendor = JSON.parse(data.resultData);        
        this.headerData = 'Edit Vendor';
        this.selectedData = vendor.VendorDetail[0];
        this.isEdit = true;
        this.showDialog = true;
      } else {
        this.toastr.error('Communication Error', 'Error!');
      }
    });
  }
}
