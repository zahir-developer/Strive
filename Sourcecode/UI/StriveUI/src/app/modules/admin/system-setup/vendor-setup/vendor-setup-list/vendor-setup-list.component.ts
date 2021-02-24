import { Component, OnInit } from '@angular/core';
import { VendorService } from 'src/app/shared/services/data-service/vendor.service';
import { ConfirmationUXBDialogService } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.service';
import { ToastrService } from 'ngx-toastr';
import { ApplicationConfig } from 'src/app/shared/services/ApplicationConfig';
import { NgxSpinnerService } from 'ngx-spinner';
import { MessageConfig } from 'src/app/shared/services/messageConfig';

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
  search: any = '';
  collectionSize: number = 0;
  page: any;
  pageSize: number;
  pageSizeList: number[];
  isDesc: boolean = false;
  column: string = 'VendorName';
  EmitPopup: boolean = true;
  constructor(
    private vendorService: VendorService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService,
    private confirmationService: ConfirmationUXBDialogService) { }

  ngOnInit() {
    this.page = ApplicationConfig.PaginationConfig.page;
    this.pageSize = ApplicationConfig.PaginationConfig.TableGridSize;
    this.pageSizeList = ApplicationConfig.PaginationConfig.Rows;
    this.getAllvendorSetupDetails();
  }

  vendorSearch() {
    this.page = 1;
    const obj = {
      vendorSearch: this.search
    };
    this.isLoading = true;
    this.vendorService.VendorSearch(obj).subscribe(data => {
      this.isLoading = false;
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
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.isLoading = false;
    });
  }
  sort(property) {
    if (this.EmitPopup === false) {
      this.isDesc = false;
    }
    this.isDesc = !this.isDesc; // change the direction    
    this.column = property;
    let direction = this.isDesc ? 1 : -1;

    this.vendorSetupDetails.sort(function (a, b) {
      if (a[property] < b[property]) {
        return -1 * direction;
      }
      else if (a[property] > b[property]) {
        return 1 * direction;
      }
      else {
        return 0;
      }
    });
  }
  // Get All Vendors
  getAllvendorSetupDetails() {
    this.vendorService.getVendor().subscribe(data => {
      this.isLoading = false;
      if (data.status === 'Success') {
        const vendor = JSON.parse(data.resultData);
        this.vendorSetupDetails = vendor.Vendor;
        if (this.vendorSetupDetails.length === 0) {
          this.isTableEmpty = true;
        } else {
          this.sort('VendorName');
          if (this.EmitPopup === false) {
            this.isDesc = true;

          }
          this.collectionSize = Math.ceil(this.vendorSetupDetails.length / this.pageSize) * 10;
          this.isTableEmpty = false;
        }
      } else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.isLoading = false;
    });
  }
  paginate(event) {
    this.pageSize = +this.pageSize;
    this.page = event;
    this.getAllvendorSetupDetails();
  }
  paginatedropdown(event) {
    this.pageSize = +event.target.value;
    this.page = this.page;
    this.getAllvendorSetupDetails();
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
        this.toastr.success(MessageConfig.Admin.SystemSetup.Vendor.Delete, 'Success!');
        this.getAllvendorSetupDetails();
      } else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    });
  }
  closePopupEmit(event) {
    if (event.status === 'saved') {
      this.EmitPopup = false;
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
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    });
  }
}
