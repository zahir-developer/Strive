import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationUXBDialogService } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.service';
import { AdSetupService } from 'src/app/shared/services/data-service/ad-setup.service';

@Component({
  selector: 'app-ad-setup-list',
  templateUrl: './ad-setup-list.component.html',
  styleUrls: ['./ad-setup-list.component.css']
})
export class AdSetupListComponent implements OnInit {

  adSetupDetails = [];
  showDialog = false;
  selectedData: any;
  headerData: string;
  isEdit: boolean;
  isTableEmpty: boolean;
  isLoading = true;
  search: any = '';
  searchStatus: any;
  recordCount: any;
  pageSize: any = 10;
  collectionSize: number = 0;
  Status: any;
  query = '';
  constructor(private adSetup: AdSetupService, 
    private toastr: ToastrService, 
    private confirmationService: ConfirmationUXBDialogService) { }

  ngOnInit() {
    this.isLoading = false;
    this.pageSize = 10;

    this.Status = [{id : 0,Value :"InActive"}, {id :1 , Value:"Active"}, {id :2 , Value:"All"}];
    this.searchStatus = "";
    this.getAlladSetupDetails();
  }

  // Get All Services
  getAlladSetupDetails() {
    this.isLoading = true;
    this.adSetup.getAdSetup().subscribe(data => {
      this.isLoading = false;
      if (data.status === 'Success') {
        const serviceDetails = JSON.parse(data.resultData);
        this.adSetupDetails = serviceDetails.ServiceSetup;
        if (this.adSetupDetails.length === 0) {
          this.isTableEmpty = true;
        } else {
          this.collectionSize = Math.ceil(this.adSetupDetails.length/this.pageSize) * 10;
          this.recordCount = this.adSetupDetails.length;
          this.pageSize = 10;

          this.isTableEmpty = false;
        }
      } else {
        this.toastr.error('Communication Error', 'Error!');
      }
    });
  }
  getAlladSetupDetailsevent(event) {
    this.isLoading = true;
    this.adSetup.getAdSetup().subscribe(data => {
      this.isLoading = false;
      if (data.status === 'Success') {
        const serviceDetails = JSON.parse(data.resultData);
        this.adSetupDetails = serviceDetails.ServiceSetup;
        if (this.adSetupDetails.length === 0) {
          this.isTableEmpty = true;
        } else {
          this.collectionSize = Math.ceil(this.adSetupDetails.length/event) * 10;
          this.recordCount = this.adSetupDetails.length;

          this.isTableEmpty = false;
        }
      } else {
        this.toastr.error('Communication Error', 'Error!');
      }
    });
  }

  paginate(event) {
    this.pageSize = event.rows;
    
    this.getAlladSetupDetails()
  }
  edit(data) {
    this.selectedData = data;
    this.showDialog = true;
  }
  delete(data) {
    this.confirmationService.confirm('Delete Service', `Are you sure you want to delete this service? All related 
  information will be deleted and the service cannot be retrieved?`, 'Yes', 'No')
      .then((confirmed) => {
        if (confirmed === true) {
          this.confirmDelete(data);
        }
      })
      .catch(() => { });
  }

  // Delete Service
  confirmDelete(data) {
    this.adSetup.deleteAdSetup(data.ServiceId).subscribe(res => {
      if (res.status === "Success") {
        this.toastr.success('Record Deleted Successfully!!', 'Success!');
        this.getAlladSetupDetails();
      } else {
        this.toastr.error('Communication Error', 'Error!');
      }
    });
  }

  closePopupEmit(event) {
    if (event.status === 'saved') {
      this.getAlladSetupDetails();
    }
    this.showDialog = event.isOpenPopup;
  }
  add(data, serviceDetails?) {
    if (data === 'add') {
      this.headerData = 'New AdSetup';
      this.selectedData = serviceDetails;
      this.isEdit = false;
      this.showDialog = true;
    } else {
      this.headerData = 'Edit AdSetup';
      this.selectedData = serviceDetails;
      this.isEdit = true;
      this.showDialog = true;
    }
  }
  


}

