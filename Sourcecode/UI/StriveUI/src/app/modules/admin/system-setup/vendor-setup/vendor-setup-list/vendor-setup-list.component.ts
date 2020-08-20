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
  isLoading = true;
  search : any = '';
  page = 1;
  pageSize = 5;
  collectionSize: number = 0;
  constructor(private vendorService: VendorService, private toastr: ToastrService, private confirmationService: ConfirmationUXBDialogService) { }

  ngOnInit() {
    this.getAllvendorSetupDetails();
  }

  vendorSearch(){
    this.page = 1;
    const obj ={
      vendorSearch: this.search
   }
   this.vendorService.VendorSearch(obj).subscribe(data => {
     if (data.status === 'Success') {
       const location = JSON.parse(data.resultData);
       this.vendorSetupDetails = location.VendorSearch;
       if (this.vendorSetupDetails.length === 0) {
         this.isTableEmpty = true;
       } else {
         this.collectionSize = Math.ceil(this.vendorSetupDetails.length / this.pageSize) * 10;
         this.isTableEmpty = false;
       }
     } else {
       this.toastr.error('Communication Error', 'Error!');
     }
   });
  }

  // Get All Vendors
  getAllvendorSetupDetails() {
    this.isLoading = true;
    this.vendorService.getVendor().subscribe(data => {
      this.isLoading = false;
      if (data.status === 'Success') {
        const vendor = JSON.parse(data.resultData);
        this.vendorSetupDetails = vendor.Vendor.filter(item => item.IsActive === 'True');
        console.log(this.vendorSetupDetails, 'vendor');
        if (this.vendorSetupDetails.length === 0) {
          this.isTableEmpty = true;
        } else {
          this.collectionSize = Math.ceil(this.vendorSetupDetails.length / this.pageSize) * 10;
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

  // Delete Vendor
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

  // Get vendor By Id
  getVendorById(data) {
    this.vendorService.getVendorById(data.VendorId).subscribe(res => {
      if (res.status === 'Success') {
        const vendor = JSON.parse(res.resultData);
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
